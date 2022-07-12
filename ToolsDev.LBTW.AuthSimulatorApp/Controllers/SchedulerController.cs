using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ToolsDev.Service.Entity.RealtimeService;
using ToolsDev.Service.Interface;
using ToolsDev.Service.Service;
using ToolsDev.Utilities.Helper;
using ToolsDev.Utilities.Library;
using ToolsDev.Utilities.Model;
using ToolsDev.Utilities.Model.Library.TFBEpg;

namespace ToolsDev.LBTW.AuthSimulatorApp.Controllers
{
    public class SchedulerController : ApiController
    {
        private TFBEpgService tfbService;
        private IRealtimeService realtimeService;
        private Logger logger = LogManager.GetCurrentClassLogger();
        private HttpContext context;

        public SchedulerController()
        {
            tfbService = new TFBEpgService();
            realtimeService = new RealtimeService();
            context = HttpContext.Current;
        }

        [HttpGet]
        public IHttpActionResult ImportRealtimeAuthFile(string message = "")
        {
            return Ok("Please use POST method.");
        }

        [HttpPost]
        public IHttpActionResult ImportRealtimeAuthFile()
        {
            var resultEntity = realtimeService.GetBatchToBeProcessedList();
            if (resultEntity == null || !resultEntity.Success)
            {
                logger.Error($"ImportRealtimeAuthFile.GetBatchToBeProcessedList failed, {resultEntity.ToJsonString()}");
                return Ok($"ImportRealtimeAuthFile.GetBatchToBeProcessedList failed, {resultEntity.ToJsonString()}");
            }
            
            var batchToBeProcessedList = ((List<RealtimeAuthBatchEntity>)resultEntity?.ResultObject);
            if (batchToBeProcessedList != null && batchToBeProcessedList.Count == 0)
            {
                return Ok($"ImportRealtimeAuthFile: No Data.");
            }

            #region update ProcessStartDate
            var seqList = batchToBeProcessedList.Select(x => x.Seq).ToList();
            Task.Run(() =>
            {
                realtimeService.UpdateBatchProcessingStart(new RequestUpdateBatchProcessingStartEntity
                {
                    SeqList = seqList
                });
            });
            #endregion

            var sleepTime = int.Parse(UtilityHelper.GetAppSetting("ImportRealtimeAuth_SleepTime"));
            var sleepTimePeriodCnt = int.Parse(UtilityHelper.GetAppSetting("ImportRealtimeAuth_SleepTimePeriodCnt"));
            var sleepTimePerPeriod = int.Parse(UtilityHelper.GetAppSetting("ImportRealtimeAuth_SleepTimePerPeriod"));
            foreach (var data in batchToBeProcessedList)
            {
                var dataCnt = 0;
                var seq = data.Seq;
                var filePath = data.FilePath;
                List<RealtimeAuthApiEntity> entityList = new List<RealtimeAuthApiEntity>();
                
                if (!File.Exists(filePath))
                {
                    UpdateBatchProcessEnd(seq, dataCnt, "File doesn't exist");
                    continue;
                }

                List<Task> tasks = new List<Task>();
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs, encoding: System.Text.Encoding.Default))
                    {
                        //特店代號,子特店代號,端末機代號,特店訂單編號,交易碼,原購貨授權碼,卡號,到期日,CVV2,請款金額,交易模式,分期期數
                        string header = sr.ReadLine();
                        while (!sr.EndOfStream)
                        {
                            dataCnt++;
                            var line = sr.ReadLine();
                            var dataArray = line.Split(',');
                            if (dataArray.Length < 12)
                            {
                                entityList.Add(new RealtimeAuthApiEntity()
                                {
                                    BatchSeq = seq,
                                    RequestDate = DateTime.Now,
                                    ResponseCode = "9990",
                                    ResponseMsg = "data length error."
                                });
                                continue;
                            }

                            //task run call api
                            tasks.Add(DoAuthApiAsync(seq, dataArray, context).ContinueWith(c =>
                            {
                                entityList.Add(c.Result);
                            }));

                            if (sleepTimePeriodCnt > 0 && dataCnt % sleepTimePeriodCnt == 0 && sleepTimePerPeriod > 0)
                            {
                                Thread.Sleep(sleepTimePerPeriod);
                            }
                            else if (sleepTime > 0)
                            {
                                Thread.Sleep(sleepTime);
                            }
                        }
                    }
                }

                //wait all tasks complete
                Task.WaitAll(tasks.ToArray());

                InsertApiList(entityList);
                UpdateBatchProcessEnd(seq, dataCnt);
            }

            return Ok($"ImportRealtimeAuthFile Process {batchToBeProcessedList.Count} file(s) done.");
        }

        private async Task<RealtimeAuthApiEntity> DoAuthApiAsync(int seq, string[] dataArray, HttpContext context)
        {
            var task = await Task.Run(() =>
            {
                try
                {
                    RequestTFBEpgAuthModel request = new RequestTFBEpgAuthModel();
                    var requestDate = DateTime.Now;

                    try
                    {
                        request = new RequestTFBEpgAuthModel()
                        {
                            MerchantID = dataArray[0],
                            SubMerchantID = (string.IsNullOrEmpty(dataArray[1]) ? "" : dataArray[1]),
                            TerminalID = dataArray[2],
                            OrderID = dataArray[3],
                            TransCode = dataArray[4],
                            TransDate = requestDate.ToString("yyyyMMdd"),
                            TransTime = requestDate.ToString("HHmmss"),
                            UserDefine = (string.IsNullOrEmpty(dataArray[5]) ? "" : dataArray[5]),
                            CardNbr = dataArray[6],
                            ExpireDate = dataArray[7],
                            CVV2 = dataArray[8],
                            TransAmt = decimal.Parse(dataArray[9]),
                            TransMode = int.Parse(dataArray[10]),
                            InstallCount = int.Parse(dataArray[11])
                        };
                    }
                    catch (Exception ex)
                    {
                        return new RealtimeAuthApiEntity()
                        {
                            BatchSeq = seq,
                            RequestDate = DateTime.Now,
                            ResponseCode = "9991",
                            ResponseMsg = ex.Message
                        };
                    }

                    Result result = new Result();
                    RealtimeAuthApiEntity realtimeAuthApiEntity = new RealtimeAuthApiEntity();
                    DateTime responseDate = DateTime.Now;

                    try
                    {
                        //TFB Auth
                        result = tfbService.STAuthApi(request, context);
                        responseDate = DateTime.Now;

                        realtimeAuthApiEntity = new RealtimeAuthApiEntity()
                        {
                            BatchSeq = seq,
                            MerchantID = request.MerchantID,
                            SubMerchantID = request.SubMerchantID,
                            TerminalID = request.TerminalID,
                            OrderID = request.OrderID,
                            TransCode = request.TransCode,
                            UserDefine = request.UserDefine,
                            CardNbr = request.CardNbr,
                            ExpireDate = request.ExpireDate,
                            CVV2 = request.CVV2,
                            TransAmt = request.TransAmt,
                            TransMode = request.TransMode,
                            InstallCount = request.InstallCount,
                            CreateUser = "sys",
                            RequestDate = requestDate
                        };
                    }
                    catch (Exception ex)
                    {
                        return new RealtimeAuthApiEntity()
                        {
                            BatchSeq = seq,
                            RequestDate = DateTime.Now,
                            ResponseCode = "9992",
                            ResponseMsg = ex.Message
                        };
                    }

                    try
                    {
                        if (result == null)
                        {
                            realtimeAuthApiEntity.ResponseCode = "999";
                            realtimeAuthApiEntity.ResponseMsg = "TFBService STAuthApi result null";

                            return realtimeAuthApiEntity;
                        }
                        realtimeAuthApiEntity.ResponseCode = result.ResultCode;
                        realtimeAuthApiEntity.ResponseMsg = result.ResultMsg;
                        realtimeAuthApiEntity.ResponseDate = responseDate;
                    }
                    catch (Exception ex)
                    {
                        return new RealtimeAuthApiEntity()
                        {
                            BatchSeq = seq,
                            RequestDate = DateTime.Now,
                            ResponseCode = "9993",
                            ResponseMsg = ex.Message
                        };
                    }

                    try
                    {
                        if (result.Success && result.ResultObject != null)
                        {
                            var rspObject = ((ResponseTBFEpgAuthModel)result?.ResultObject);
                            if (rspObject != null)
                            {
                                realtimeAuthApiEntity.ApproveCode = rspObject.ApproveCode;
                                realtimeAuthApiEntity.BatchNo = rspObject.BatchNo;
                                realtimeAuthApiEntity.RRN = rspObject.RRN;
                                realtimeAuthApiEntity.MessageType = rspObject.MessageType;
                                realtimeAuthApiEntity.ReversalFlag = rspObject.ReversalFlag;
                                realtimeAuthApiEntity.TransType = rspObject.TransType;
                                realtimeAuthApiEntity.InstallType = rspObject.InstallType;
                                realtimeAuthApiEntity.FirstAmt = rspObject.FirstAmt;
                                realtimeAuthApiEntity.EachAmt = rspObject.EachAmt;
                                realtimeAuthApiEntity.Fee = rspObject.Fee;
                                realtimeAuthApiEntity.RedeemType = rspObject.RedeemType;
                                realtimeAuthApiEntity.RedeemUsed = rspObject.RedeemUsed;
                                realtimeAuthApiEntity.RedeemBalance = rspObject.RedeemBalance;
                                realtimeAuthApiEntity.CreditAmt = rspObject.CreditAmt;
                                realtimeAuthApiEntity.UserDefine = rspObject.UserDefine;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return new RealtimeAuthApiEntity()
                        {
                            BatchSeq = seq,
                            RequestDate = DateTime.Now,
                            ResponseCode = "9994",
                            ResponseMsg = ex.Message
                        };
                    }

                    return realtimeAuthApiEntity;
                }
                catch (Exception ex)
                {
                    return new RealtimeAuthApiEntity()
                    {
                        BatchSeq = seq,
                        RequestDate = DateTime.Now,
                        ResponseCode = "9995",
                        ResponseMsg = ex.Message
                    };
                }
            }).ConfigureAwait(false);

            return task;
        }

        private void InsertApiList(List<RealtimeAuthApiEntity> entitys)
        {
            var perSelectedCount = int.Parse(UtilityHelper.GetAppSetting("ImportRealtimeAuth_InsertApisCount"));

            try
            {
                for (int i = 0; i * perSelectedCount < entitys.Count; i++)
                {
                    var entitySelected = entitys.Skip(i * perSelectedCount).Take(perSelectedCount).ToList();
                    realtimeService.InsertApiList(new RealtimeAuthApiListEntity() { ApiList = entitySelected });
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToJsonString());
            }
        }

        private void UpdateBatchProcessEnd(int seq, int dataCnt, string memo = "")
        {
            Task.Run(() =>
            {
                realtimeService.UpdateBatchProcessEnd(new RequestUpdateBatchProcessEndEntity
                {
                    Seq = seq,
                    DataCount = dataCnt,
                    Memo = memo
                });
            });
        }

        [HttpGet]
        public IHttpActionResult ImportBatchAuthFile(string message = "")
        {
            return Ok("Please use POST method.");
        }
    }
}
