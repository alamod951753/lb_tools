using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using ToolsDev.LBTW.AuthSimulatorApp.Models.RealtimeAuth;
using ToolsDev.Service.Entity.RealtimeService;
using ToolsDev.Service.Interface;
using ToolsDev.Service.Service;
using ToolsDev.Utilities.Helper;
using ToolsDev.Utilities.Library;
using ToolsDev.Utilities.Model.Library.TFBEpg;

namespace ToolsDev.LBTW.AuthSimulatorApp.Controllers
{
    public class RealtimeAuthController : Controller
    {
        private IRealtimeService realtimeService;
        private Logger logger = LogManager.GetCurrentClassLogger();

        public RealtimeAuthController()
        {
            realtimeService = new RealtimeService();
        }

        public ActionResult Create()
        {
            var model = new RealtimeCreateViewModel()
            {
                MerchantID = "012999990000002",
                TerminalID = "99999020",
                TransCode = "00",
                OrderID = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                TransMode = 0,
                InstallCount = 1
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(RealtimeCreateViewModel model)
        {
            //check model value
            var errorMsg = CheckModelState();
            if (model.TransCode != "00" && string.IsNullOrEmpty(model.UserDefine))
            {
                errorMsg += $"原購貨授權碼 field is required.{Environment.NewLine}";
            }
            if (model.TransMode == 1 && (model.InstallCount == null || model.InstallCount <= 0))
            {
                errorMsg += $"分期期數 field is required.{Environment.NewLine}";
            }
            if (!string.IsNullOrEmpty(errorMsg))
            {
                model.ResponseMsg = errorMsg;
                return View(model);
            }

            //send TFBEPG Api request
            var requestDate = DateTime.Now;
            var request = new RequestTFBEpgAuthModel()
            {
                MerchantID = model.MerchantID,
                SubMerchantID = (string.IsNullOrEmpty(model.SubMerchantID) ? "" : model.SubMerchantID),
                TerminalID = model.TerminalID,
                OrderID = model.OrderID,
                TransCode = model.TransCode,
                UserDefine = (string.IsNullOrEmpty(model.UserDefine) ? "" : model.UserDefine),
                TransDate = requestDate.ToString("yyyyMMdd"),
                TransTime = requestDate.ToString("HHmmss"),
                CardNbr = model.CardNbr,
                ExpireDate = model.ExpireDate,
                CVV2 = model.CVV2,
                TransAmt = model.TransAmt,
                TransMode = model.TransMode,
                InstallCount = (model.InstallCount == null) ? 0 : (int)model.InstallCount
            };

            //TFB Auth
            TFBEpgService service = new TFBEpgService();
            var result = service.STAuthApi(request);
            var responseDate = DateTime.Now;

            if (result == null)
            {
                model.ResponseCode = "LBTW9999";
                model.ResponseMsg = "System process error.";
                return View(model);
            }

            model.ResponseCode = result.ResultCode;
            model.ResponseMsg = result.ResultMsg; 
            logger.Debug($"Create result: {result.ToJsonString()}");

            if (result.Success && result.ResultObject != null)
            {
                var rspObject = ((ResponseTBFEpgAuthModel)result?.ResultObject);
                if (rspObject != null)
                {
                    model.ApproveCode = rspObject.ApproveCode;
                    model.BatchNo = rspObject.BatchNo;
                    model.RRN = rspObject.RRN;
                    model.MessgeType = rspObject.MessageType;
                    model.ReversalFlag = rspObject.ReversalFlag;
                    model.TransType = rspObject.TransType;
                    model.InstallType = rspObject.InstallType;
                    model.FirstAmt = rspObject.FirstAmt;
                    model.EachAmt = rspObject.EachAmt;
                    model.Fee = rspObject.Fee;
                    model.RedeemType = rspObject.RedeemType;
                    model.RedeemUsed = rspObject.RedeemUsed;
                    model.RedeemBalance = rspObject.RedeemBalance;
                    model.CreditAmt = rspObject.CreditAmt;
                    model.UserDefine = (string.IsNullOrEmpty(rspObject.UserDefine) ? model.UserDefine : rspObject.UserDefine);
                }
            }

            #region InsertApi
            Task.Run(() =>
            {
                realtimeService.InsertApi(new RealtimeAuthApiEntity()
                {
                    MerchantID = model.MerchantID,
                    SubMerchantID = model.SubMerchantID,
                    TerminalID = model.TerminalID,
                    OrderID = model.OrderID,
                    TransCode = model.TransCode,
                    CardNbr = model.CardNbr,
                    ExpireDate = model.ExpireDate,
                    CVV2 = model.CVV2,
                    TransAmt = model.TransAmt,
                    TransMode = model.TransMode,
                    InstallCount = (model.InstallCount == null) ? 0 : (int)model.InstallCount,
                    CreateUser = "sys",
                    RequestDate = requestDate,
                    ApproveCode = model.ApproveCode,
                    BatchNo = model.BatchNo,
                    RRN = model.RRN,
                    ResponseCode = model.ResponseCode,
                    ResponseMsg = model.ResponseMsg,
                    ResponseDate = responseDate,
                    TransType = model.TransType,
                    MessageType = model.MessgeType,
                    ReversalFlag = model.ReversalFlag,
                    InstallType = model.InstallType,
                    FirstAmt = model.FirstAmt,
                    EachAmt = model.EachAmt,
                    Fee = model.Fee,
                    RedeemType = model.RedeemType,
                    RedeemUsed = model.RedeemUsed,
                    RedeemBalance = model.RedeemBalance,
                    CreditAmt = model.CreditAmt,
                    UserDefine = model.UserDefine
                });
            });
            #endregion

            return View(model);
        }

        public ActionResult Upload()
        {
            var blnFuncOnline = UtilityHelper.GetAppSetting("RealtimeUploadOnline");
            ViewData["FuncOnline"] = blnFuncOnline;

            var model = new RealtimeUploadViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Upload(RealtimeUploadViewModel model)
        {
            //check model value
            var errorMsg = CheckModelState();
            if (!string.IsNullOrEmpty(errorMsg))
            {
                model.ResponseMsg = errorMsg;
                return View(model);
            }

            string fileName = model.UploadFile.FileName;
            if (!fileName.ToLower().EndsWith(".csv"))
            {
                model.ResponseMsg = $"Please upload a csv file.";
                model.UploadFile = null;
                return View(model);
            }

            string uploadPath = Server.MapPath($@"~\App_Data\Uploads\Realtime\");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string saveFileName = "";
            DateTime uploadDate = DateTime.Now;
            try
            {
                saveFileName = fileName.Replace(".csv", $"_{uploadDate.ToString("yyyyMMddHHmmssfff")}.csv");
                model.UploadFile.SaveAs($@"{uploadPath}{saveFileName}");

                model.ResponseMsg = $"上傳成功!{Environment.NewLine}{Environment.NewLine}*** File Rename ***{Environment.NewLine}{saveFileName}";
                model.UploadTime = uploadDate;
                model.UploadFileName = $"{fileName}";
            }
            catch (Exception ex)
            {
                model.ResponseMsg = $"{ex.Message}";
                logger.Error($"Upload Exception: {ex.ToJsonString()}");
            }

            #region InsertBatchApi
            Task.Run(() =>
            {
                realtimeService.InsertBatchApi(new RealtimeAuthBatchEntity()
                {
                    FilePath = $@"{uploadPath}{saveFileName}",
                    UploadDate = uploadDate,
                    UploadUser = "sys"
                });
            });
            #endregion

            model.UploadFile = null;
            return View(model);
        }

        public ActionResult Query()
        {
            var model = new RealtimeQueryViewModel()
            {
                Mode = 0,
                CreateDate = DateTime.Now.ToString("yyyy/MM/dd"),
                TransCode = "-1",
                TransMode = -1
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Query(RealtimeQueryViewModel model)
        {
            if (model.Mode == 0)
            {
                model.ErrorMsg = $"===== 請選擇 Mode =====";
                return View(model);
            }

            if (model.Mode == 1)
            {
                var resultEntity = realtimeService.GetApiList(new RequestGetApiListEntity()
                {
                    TerminalID = model.TerminalID,
                    CreateDate = model.CreateDate,
                    CreateUser = model.CreateUser,
                    ApproveCode = model.ApproveCode,
                    CardNbr = model.CardNbr,
                    TransAmt = model.TransAmt,
                    TransMode = model.TransMode,
                    TransCode = model.TransCode
                });

                if (resultEntity != null && resultEntity.Success)
                {
                    model.ApiList = ((List<RealtimeAuthApiEntity>)resultEntity?.ResultObject);
                    if (model.ApiList != null && model.ApiList.Count == 0)
                    {
                        model.ErrorMsg = $"查無資料";
                    }
                }
                else
                {
                    model.ErrorMsg = $"查詢失敗 ( ﾟдﾟ) 請再試一次，或找資訊人員救援 (~_~メ)";
                    logger.Error($"Query failed, {resultEntity.ToJsonString()}");
                }
            }

            if (model.Mode == 2)
            {
                var resultEntity = realtimeService.GetBatchList(new RequestGetBatchListEntity()
                {
                    TerminalID = model.TerminalID,
                    UploadDate = model.CreateDate,
                    UploadUser = model.CreateUser
                });

                if (resultEntity != null && resultEntity.Success)
                {
                    model.BatchApiList = ((List<RealtimeAuthBatchQueryEntity>)resultEntity?.ResultObject);
                    if (model.BatchApiList != null && model.BatchApiList.Count == 0)
                    {
                        model.ErrorMsg = $"查無資料";
                    }
                    else
                    {
                        foreach (var item in model.BatchApiList)
                        {
                            if (item.FailedCnt > 0)
                            {
                                var failDetailEntity = realtimeService.GetBatchFailDetails(new RequestGetBatchFailDetailsEntity() { BatchSeq = item.BatchSeq });
                                if (failDetailEntity != null && failDetailEntity.Success)
                                {
                                    item.BatchFailDetailList = ((List<RealtimeAuthApiEntity>)failDetailEntity?.ResultObject);
                                }
                            }
                        }
                    }
                }
                else
                {
                    model.ErrorMsg = $"查詢失敗 ( ﾟдﾟ) 請再試一次，或找資訊人員救援 (~_~メ)";
                    logger.Error($"Query failed, {resultEntity.ToJsonString()}");
                }
            }

            return View(model);
        }

        private string CheckModelState()
        {
            var errorMsg = "";
            if (!ModelState.IsValid)
            {
                foreach (var modelKey in ModelState.Keys)
                {
                    var errors = ModelState[modelKey].Errors;
                    if (errors.Count > 0)
                    {
                        errorMsg += $"{errors[0].ErrorMessage.Replace("The ", "")}{Environment.NewLine}";
                    }
                }
            }
            return errorMsg;
        }
    }
}