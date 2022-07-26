using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ToolsDev.LBTW.AuthSimulatorBackend.Models.RealtimeService;
using ToolsDev.Repository.Dto.AuthSimulatorApp;
using ToolsDev.Repository.Interface.AuthSimulatorApp;
using ToolsDev.Repository.Repo.AuthSimulatorApp;
using ToolsDev.Utilities.Helper;
using ToolsDev.Utilities.Model;

namespace ToolsDev.LBTW.AuthSimulatorBackend.Controllers
{
    public class RealtimeServiceController : ApiController
    {
        private IRepositoryRealtimeAuth repositoryRealtimeAuth;
        private Logger logger = LogManager.GetCurrentClassLogger();
        private string apiListCacheKey = "RealtimeService_ApiList_";
        private string batchListCacheKey = "RealtimeService_BatchList_";
        private string batchFailDetailsCacheKey = "RealtimeService_BatchFailDetails_";

        public RealtimeServiceController()
        {
            repositoryRealtimeAuth = new RepositoryRealtimeAuth();
        }

        public IHttpActionResult Get()
        {
            return Ok("Please use POST method.");
        }

        [HttpPost]
        public IHttpActionResult InsertApi(InsertApiModel model)
        {
            var id = repositoryRealtimeAuth.InsertApi(new RealtimeAuthApi_Dto()
            {
                BATCH_SEQ = model.BatchSeq,
                MERCHANT_ID = model.MerchantID,
                SUBMERCHANT_ID = model.SubMerchantID,
                TERMINAL_ID = model.TerminalID,
                ORDER_ID = model.OrderID,
                TRANS_CODE = model.TransCode,
                CARD_NBR = model.CardNbr,
                EXPIRE_DATE = model.ExpireDate,
                CVV2 = model.CVV2,
                TRANS_AMT = model.TransAmt,
                TRANS_MODE = model.TransMode,
                INSTALL_COUNT = model.InstallCount,
                CREATE_USER = model.CreateUser,
                REQUEST_DATE = model.RequestDate,
                MESSAGE_TYPE = model.MessageType,
                REVERSAL_FLAG = model.ReversalFlag,
                APPROVE_CODE = model.ApproveCode,
                BATCH_NO = model.BatchNo,
                RRN = model.RRN,
                RESPONSE_CODE = model.ResponseCode,
                RESPONSE_MSG = model.ResponseMsg,
                RESPONSE_DATE = model.ResponseDate,
                TRANS_TYPE = model.TransType,
                INSTALL_TYPE = model.InstallType,
                FIRST_AMT = model.FirstAmt,
                EACH_AMT = model.EachAmt,
                FEE = model.Fee,
                REDEEM_TYPE = model.RedeemType,
                REDEEM_USED = model.RedeemUsed,
                REDEEM_BALANCE = model.RedeemBalance,
                CREDIT_AMT = model.CreditAmt,
                USER_DEFINE = model.UserDefine
            });

            CacheHelper.Remove(apiListCacheKey);
            CacheHelper.RemoveKeysLike($"{apiListCacheKey}{DateTime.Now.ToString("yyyyMMdd")}");

            return Ok(id);
        }

        [HttpPost]
        public IHttpActionResult InsertApiList(InsertApiListModel model)
        {
            var apis = model.ApiList.Select(x => new RealtimeAuthApi_Dto()
            {
                BATCH_SEQ = x.BatchSeq,
                MERCHANT_ID = x.MerchantID,
                SUBMERCHANT_ID = x.SubMerchantID,
                TERMINAL_ID = x.TerminalID,
                ORDER_ID = x.OrderID,
                TRANS_CODE = x.TransCode,
                CARD_NBR = x.CardNbr,
                EXPIRE_DATE = x.ExpireDate,
                CVV2 = x.CVV2,
                TRANS_AMT = x.TransAmt,
                TRANS_MODE = x.TransMode,
                INSTALL_COUNT = x.InstallCount,
                CREATE_USER = x.CreateUser,
                REQUEST_DATE = x.RequestDate,
                MESSAGE_TYPE = x.MessageType,
                REVERSAL_FLAG = x.ReversalFlag,
                APPROVE_CODE = x.ApproveCode,
                BATCH_NO = x.BatchNo,
                RRN = x.RRN,
                RESPONSE_CODE = x.ResponseCode,
                RESPONSE_MSG = x.ResponseMsg,
                RESPONSE_DATE = x.ResponseDate,
                TRANS_TYPE = x.TransType,
                INSTALL_TYPE = x.InstallType,
                FIRST_AMT = x.FirstAmt,
                EACH_AMT = x.EachAmt,
                FEE = x.Fee,
                REDEEM_TYPE = x.RedeemType,
                REDEEM_USED = x.RedeemUsed,
                REDEEM_BALANCE = x.RedeemBalance,
                CREDIT_AMT = x.CreditAmt,
                USER_DEFINE = x.UserDefine
            }).ToList();

            CacheHelper.Remove(apiListCacheKey);
            CacheHelper.RemoveKeysLike($"{apiListCacheKey}{DateTime.Now.ToString("yyyyMMdd")}");

            var result = repositoryRealtimeAuth.InsertApiList(apis);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult InsertBatchApi(InsertBatchApiModel model)
        {
            var id = repositoryRealtimeAuth.InsertBatch(new RealtimeAuthBatch_Dto()
            {
                FILE_PATH = model.FilePath,
                UPLOAD_DATE = model.UploadDate,
                UPLOAD_USER = model.UploadUser,
                DATA_CNT = model.DataCnt,
                PROCESS_START_DATE = model.ProcessStartDate,
                PROCESS_END_DATE = model.ProcessEndDate
            });

            CacheHelper.Remove(batchListCacheKey);
            CacheHelper.RemoveKeysLike($"{batchListCacheKey}{DateTime.Now.ToString("yyyyMMdd")}");

            return Ok(id);
        }

        [HttpPost]
        public IHttpActionResult ApiList(RequestGetApiListModel model)
        {
            Result result;

            if (model == null)
            {
                return Ok(new Result()
                {
                    Success = false,
                    ResultCode = "9999",
                    ResultMsg = "request model null error"
                }); 
            }

            try
            {
                string cacheDate = "";
                if (!string.IsNullOrEmpty(model.CreateDate))
                {
                    cacheDate = model.CreateDate.Replace(@"/", "");
                }
                string cacheKey = $"{apiListCacheKey}{cacheDate}";
                var apiList = new List<RealtimeAuthApi_Dto>();

                if (CacheHelper.Exists(cacheKey))
                {
                    apiList = CacheHelper.Get<List<RealtimeAuthApi_Dto>>(cacheKey);
                }
                else
                {
                    apiList = repositoryRealtimeAuth.GetApiList(new RequestGetApiList_Dto()
                    {
                        RequestDate = model.CreateDate
                    });

                    CacheHelper.Set(cacheKey, apiList);
                }

                Func<RealtimeAuthApi_Dto, bool> predicate = (x =>
                    (!string.IsNullOrEmpty(model.TerminalID) ? x.TERMINAL_ID == model.TerminalID : true) &&
                    (!string.IsNullOrEmpty(model.CreateUser) ? x.CREATE_USER == model.CreateUser : true) &&
                    (!string.IsNullOrEmpty(model.ApproveCode) ? x.APPROVE_CODE == model.ApproveCode : true) &&
                    (!string.IsNullOrEmpty(model.CardNbr) ? x.CARD_NBR == model.CardNbr : true) &&
                    ((model.TransAmt != null && Math.Abs((decimal)model.TransAmt) > 0) ? x.TRANS_AMT == model.TransAmt : true) &&
                    ((model.TransMode != null && model.TransMode > -1) ? x.TRANS_MODE == model.TransMode : true) &&
                    ((!string.IsNullOrEmpty(model.TransCode) && model.TransCode != "-1") ? x.TRANS_CODE == model.TransCode : true)
                );
                apiList = apiList.Where(predicate).Skip(model.PageIndex * model.PageSize).Take(model.PageSize).ToList();

                result = new Result()
                {
                    Success = true,
                    ResultCode = "0000",
                    ResultObject = apiList
                };
            }
            catch (Exception ex)
            {
                logger.Error($"GetApiList Exception: {ex.ToJsonString()}");
                result = new Result()
                {
                    Success = false,
                    ResultCode = "9999",
                    ResultMsg = ex.Message
                };
            }

            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult BatchList(RequestGetBatchListModel model)
        {
            Result result;

            if (model == null)
            {
                return Ok(new Result()
                {
                    Success = false,
                    ResultCode = "9999",
                    ResultMsg = "request model null error"
                });
            }

            try
            {
                string cacheDate = "";
                if (!string.IsNullOrEmpty(model.UploadDate))
                {
                    cacheDate = model.UploadDate.Replace(@"/", "");
                }
                string cacheKey = $"{batchListCacheKey}{cacheDate}";
                var batchList = new List<ResponseGetBatchList_Dto>();

                if (CacheHelper.Exists(cacheKey))
                {
                    batchList = CacheHelper.Get<List<ResponseGetBatchList_Dto>>(cacheKey);
                }
                else
                {
                    batchList = repositoryRealtimeAuth.GetBatchList(new RequestGetBatchList_Dto()
                    {
                        UploadDate = model.UploadDate
                    });

                    CacheHelper.Set(cacheKey, batchList);
                }

                //Func<ResponseGetBatchList_Dto, bool> predicate = (x =>
                //    (!string.IsNullOrEmpty(model.TerminalID) ? x.TERMINAL_ID == model.TerminalID : true) &&
                //    (!string.IsNullOrEmpty(model.UploadUser) ? x.UPLOAD_USER == model.UploadUser : true)
                //);
                //batchList = batchList.Where(predicate).ToList();

                result = new Result()
                {
                    Success = true,
                    ResultCode = "0000",
                    ResultObject = batchList
                };
            }
            catch (Exception ex)
            {
                logger.Error($"GetBatchList Exception: {ex.ToJsonString()}");
                result = new Result()
                {
                    Success = false,
                    ResultCode = "9999",
                    ResultMsg = ex.Message
                };
            }

            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult BatchFailDetails(RequestGetBatchFailDetailsModel model)
        {
            Result result;

            if (model == null)
            {
                return Ok(new Result()
                {
                    Success = false,
                    ResultCode = "9999",
                    ResultMsg = "request model null error"
                });
            }

            try
            {
                string cacheKey = $"{batchFailDetailsCacheKey}{model.BatchSeq}";
                var batchFailDetailList = new List<RealtimeAuthApi_Dto>();

                if (CacheHelper.Exists(cacheKey))
                {
                    batchFailDetailList = CacheHelper.Get<List<RealtimeAuthApi_Dto>>(cacheKey);
                }
                else
                {
                    batchFailDetailList = repositoryRealtimeAuth.GetBatchFailDetails(model.BatchSeq);

                    CacheHelper.Set(cacheKey, batchFailDetailList);
                }

                result = new Result()
                {
                    Success = true,
                    ResultCode = "0000",
                    ResultObject = batchFailDetailList
                };
            }
            catch (Exception ex)
            {
                logger.Error($"BatchFailDetail Exception: {ex.ToJsonString()}");
                result = new Result()
                {
                    Success = false,
                    ResultCode = "9999",
                    ResultMsg = ex.Message
                };
            }

            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult BatchToBeProcessedList()
        {
            Result result;

            try
            {
                var batchList = repositoryRealtimeAuth.GetBatchToBeProcessedList();

                result = new Result()
                {
                    Success = true,
                    ResultCode = "0000",
                    ResultObject = batchList
                };
            }
            catch (Exception ex)
            {
                logger.Error($"GetBatchToBeProcessedList Exception: {ex.ToJsonString()}");
                result = new Result()
                {
                    Success = false,
                    ResultCode = "9999",
                    ResultMsg = ex.Message
                };
            }

            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult UpdateBatchProcessingStart(RequestUpdateBatchProcessingStartModel model)
        {
            var updateCnt = repositoryRealtimeAuth.UpdateBatchProcessingStart(new RequestUpdateBatchProcessingStart_Dto()
            {
                SeqList = model.SeqList
            });

            CacheHelper.Remove(batchListCacheKey);
            CacheHelper.RemoveKeysLike($"{batchListCacheKey}{DateTime.Now.ToString("yyyyMMdd")}");
            foreach (var seq in model.SeqList)
            {
                CacheHelper.Remove($"{batchFailDetailsCacheKey}{seq}");
            }

            return Ok(updateCnt);
        }

        [HttpPost]
        public IHttpActionResult UpdateBatchProcessEnd(RequestUpdateBatchProcessEndModel model)
        {
            var updateCnt = repositoryRealtimeAuth.UpdateBatchProcessEnd(new RequestUpdateBatchProcessEnd_Dto()
            {
                Seq = model.Seq,
                Memo = model.Memo,
                DataCount = model.DataCount
            });

            CacheHelper.Remove(batchListCacheKey);
            CacheHelper.RemoveKeysLike($"{batchListCacheKey}{DateTime.Now.ToString("yyyyMMdd")}");
            CacheHelper.Remove($"{batchFailDetailsCacheKey}{model.Seq}");

            return Ok(updateCnt);
        }
    }
}
