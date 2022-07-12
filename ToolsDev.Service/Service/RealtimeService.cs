using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolsDev.Repository.Dto.AuthSimulatorApp;
using ToolsDev.Service.Entity;
using ToolsDev.Service.Entity.RealtimeService;
using ToolsDev.Service.Interface;
using ToolsDev.Utilities.Helper;

namespace ToolsDev.Service.Service
{
    public class RealtimeService : IRealtimeService
    {
        private ApiHelper apiHelper;
        private string authSimulatorBackendUrl;
        private string authSimulatorBackendToken;
        private Dictionary<string, string> headers;
        private Logger logger = LogManager.GetCurrentClassLogger();

        public RealtimeService()
        {
            apiHelper = new ApiHelper();
            authSimulatorBackendUrl = UtilityHelper.GetAppSetting("AuthSimulatorBackendUrl");
            authSimulatorBackendToken = UtilityHelper.GetAppSetting("AuthSimulatorBackendToken");
            headers = new Dictionary<string, string>
            {
                { "Authorization", $"{authSimulatorBackendToken}" }
            };
        }

        public async Task<ResultEntity> InsertApi(RealtimeAuthApiEntity entity)
        {
            var requestUri = $"{authSimulatorBackendUrl}/api/RealtimeService/InsertApi";

            ResultEntity resultEntity;
            try
            {
                //result = insert seq id
                var result = await apiHelper.PostAsync(requestUri, entity, headers: headers);
                resultEntity = new ResultEntity()
                {
                    Success = true,
                    ResultCode = "0000",
                    ResultMsg = result
                };
            }
            catch (Exception ex)
            {
                logger.Error($"InsertApi Exception: {ex.ToJsonString()}");
                resultEntity = new ResultEntity()
                {
                    Success = false,
                    ResultCode = "9999",
                    ResultMsg = ex.Message
                };
            }

            return resultEntity;
        }

        public async Task<ResultEntity> InsertApiList(RealtimeAuthApiListEntity entity)
        {
            var requestUri = $"{authSimulatorBackendUrl}/api/RealtimeService/InsertApiList";

            ResultEntity resultEntity;
            try
            {
                //result = insert seq id
                var result = await apiHelper.PostAsync(requestUri, entity, headers: headers);
                resultEntity = new ResultEntity()
                {
                    Success = true,
                    ResultCode = "0000",
                    ResultMsg = result
                };
            }
            catch (Exception ex)
            {
                logger.Error($"InsertApiList Exception: {ex.ToJsonString()}");
                resultEntity = new ResultEntity()
                {
                    Success = false,
                    ResultCode = "9999",
                    ResultMsg = ex.Message
                };
            }

            return resultEntity;
        }

        public async Task<ResultEntity> InsertBatchApi(RealtimeAuthBatchEntity entity)
        {
            var requestUri = $"{authSimulatorBackendUrl}/api/RealtimeService/InsertBatchApi";

            ResultEntity resultEntity;
            try
            {
                //result = insert seq id
                var result = await apiHelper.PostAsync(requestUri, entity, headers: headers);
                resultEntity = new ResultEntity()
                {
                    Success = true,
                    ResultCode = "0000",
                    ResultMsg = result
                };
            }
            catch (Exception ex)
            {
                logger.Error($"InsertBatchApi Exception: {ex.ToJsonString()}");
                resultEntity = new ResultEntity()
                {
                    Success = false,
                    ResultCode = "9999",
                    ResultMsg = ex.Message
                };
            }

            return resultEntity;
        }

        public ResultEntity GetApiList(RequestGetApiListEntity entity)
        {
            var requestUri = $"{authSimulatorBackendUrl}/api/RealtimeService/ApiList";

            ResultEntity resultEntity;
            try
            {
                var result = apiHelper.PostAsync(requestUri, entity, headers: headers);
                if (result == null || string.IsNullOrEmpty(result.Result))
                {
                    resultEntity = new ResultEntity()
                    {
                        Success = false,
                        ResultCode = "9998",
                        ResultMsg = $"GetApiList apiHelper result empty."
                    };
                    return resultEntity;
                }

                resultEntity = result.Result.ToJsonFormat<ResultEntity>();
                if (resultEntity != null && resultEntity.Success)
                {
                    var apiResult = ((JArray)resultEntity.ResultObject).ToObject<List<RealtimeAuthApi_Dto>>();
                    var apiList = apiResult.ConvertAll(x => new RealtimeAuthApiEntity
                    {
                        Seq = x.SEQ,
                        MerchantID = x.MERCHANT_ID,
                        SubMerchantID = x.SUBMERCHANT_ID,
                        TerminalID = x.TERMINAL_ID,
                        OrderID = x.ORDER_ID,
                        TransCode = x.TRANS_CODE,
                        CardNbr = x.CARD_NBR,
                        TransAmt = x.TRANS_AMT,
                        TransMode = x.TRANS_MODE,
                        InstallCount = x.INSTALL_COUNT,
                        CreateUser = x.CREATE_USER,
                        RequestDate = x.REQUEST_DATE,
                        ApproveCode = x.APPROVE_CODE,
                        ResponseDate = x.RESPONSE_DATE,
                        ResponseCode = x.RESPONSE_CODE,
                        ResponseMsg = x.RESPONSE_MSG,
                        BatchNo = x.BATCH_NO,
                        RRN = x.RRN,
                        TransType = x.TRANS_TYPE,
                        InstallType = x.INSTALL_TYPE,
                        FirstAmt = x.FIRST_AMT,
                        EachAmt = x.EACH_AMT,
                        Fee = x.FEE,
                        RedeemType = x.REDEEM_TYPE,
                        RedeemUsed = x.REDEEM_USED,
                        RedeemBalance = x.REDEEM_BALANCE,
                        CreditAmt = x.CREDIT_AMT,
                        UserDefine = x.USER_DEFINE
                    });
                    resultEntity.ResultObject = apiList;
                }
            }
            catch (Exception ex)
            {
                logger.Error($"GetApiList Exception: {ex.ToJsonString()}");
                resultEntity = new ResultEntity()
                {
                    Success = false,
                    ResultCode = "9999",
                    ResultMsg = ex.Message
                };
            }

            return resultEntity;
        }

        public ResultEntity GetBatchList(RequestGetBatchListEntity entity)
        {
            var requestUri = $"{authSimulatorBackendUrl}/api/RealtimeService/BatchList";

            ResultEntity resultEntity;
            try
            {
                var result = apiHelper.PostAsync(requestUri, entity, headers: headers);
                if (result == null || string.IsNullOrEmpty(result.Result))
                {
                    resultEntity = new ResultEntity()
                    {
                        Success = false,
                        ResultCode = "9998",
                        ResultMsg = $"GetBatchList apiHelper result empty."
                    };
                    return resultEntity;
                }

                resultEntity = result.Result.ToJsonFormat<ResultEntity>();
                if (resultEntity != null && resultEntity.Success)
                {
                    var apiResult = ((JArray)resultEntity.ResultObject).ToObject<List<ResponseGetBatchList_Dto>>();
                    var apiList = apiResult.ConvertAll(x => new RealtimeAuthBatchQueryEntity
                    {
                        BatchSeq = x.BATCH_SEQ,
                        FilePath = x.FILE_PATH,
                        UploadDate = x.UPLOAD_DATE,
                        UploadUser = x.UPLOAD_USER,
                        DataCnt = x.DATA_CNT,
                        ProcessStartDate = x.PROCESS_START_DATE,
                        ProcessEndDate = x.PROCESS_END_DATE,
                        ProcessMemo = x.PROCESS_MEMO,
                        ProcessCnt = x.PROCESS_CNT,
                        SuccessCnt = x.SUCCESS_CNT,
                        FailedCnt = x.FAILED_CNT
                    });
                    resultEntity.ResultObject = apiList;
                }
            }
            catch (Exception ex)
            {
                logger.Error($"GetBatchList Exception: {ex.ToJsonString()}");
                resultEntity = new ResultEntity()
                {
                    Success = false,
                    ResultCode = "9999",
                    ResultMsg = ex.Message
                };
            }

            return resultEntity;
        }

        public ResultEntity GetBatchFailDetails(RequestGetBatchFailDetailsEntity entity)
        {
            var requestUri = $"{authSimulatorBackendUrl}/api/RealtimeService/BatchFailDetails";

            ResultEntity resultEntity;
            try
            {
                var result = apiHelper.PostAsync(requestUri, entity, headers: headers);
                if (result == null || string.IsNullOrEmpty(result.Result))
                {
                    resultEntity = new ResultEntity()
                    {
                        Success = false,
                        ResultCode = "9998",
                        ResultMsg = $"GetBatchFailDetails apiHelper result empty."
                    };
                    return resultEntity;
                }

                resultEntity = result.Result.ToJsonFormat<ResultEntity>();
                if (resultEntity != null && resultEntity.Success)
                {
                    var apiResult = ((JArray)resultEntity.ResultObject).ToObject<List<RealtimeAuthApi_Dto>>();
                    var batchFailDetailList = apiResult.ConvertAll(x => new RealtimeAuthApiEntity
                    {
                        OrderID = x.ORDER_ID,
                        ResponseCode = x.RESPONSE_CODE,
                        ResponseMsg = x.RESPONSE_MSG
                    });
                    resultEntity.ResultObject = batchFailDetailList;
                }
            }
            catch (Exception ex)
            {
                logger.Error($"GetBatchFailDetails Exception: {ex.ToJsonString()}");
                resultEntity = new ResultEntity()
                {
                    Success = false,
                    ResultCode = "9999",
                    ResultMsg = ex.Message
                };
            }

            return resultEntity;
        }

        public ResultEntity GetBatchToBeProcessedList()
        {
            var requestUri = $"{authSimulatorBackendUrl}/api/RealtimeService/BatchToBeProcessedList";

            ResultEntity resultEntity;
            try
            {
                var result = apiHelper.PostAsync(requestUri, "", headers: headers);
                if (result == null || string.IsNullOrEmpty(result.Result))
                {
                    resultEntity = new ResultEntity()
                    {
                        Success = false,
                        ResultCode = "9998",
                        ResultMsg = $"GetBatchToBeProcessedList apiHelper result empty."
                    };
                    return resultEntity;
                }

                resultEntity = result.Result.ToJsonFormat<ResultEntity>();
                if (resultEntity != null && resultEntity.Success)
                {
                    var apiResult = ((JArray)resultEntity.ResultObject).ToObject<List<RealtimeAuthBatch_Dto>>();
                    var apiList = apiResult.ConvertAll(x => new RealtimeAuthBatchEntity
                    {
                        Seq = x.SEQ,
                        FilePath = x.FILE_PATH,
                        UploadDate = x.UPLOAD_DATE,
                        UploadUser = x.UPLOAD_USER,
                        DataCnt = x.DATA_CNT,
                        ProcessStartDate = x.PROCESS_START_DATE,
                        ProcessEndDate = x.PROCESS_END_DATE
                    });
                    resultEntity.ResultObject = apiList;
                }
            }
            catch (Exception ex)
            {
                logger.Error($"GetBatchToBeProcessedList Exception: {ex.ToJsonString()}");
                resultEntity = new ResultEntity()
                {
                    Success = false,
                    ResultCode = "9999",
                    ResultMsg = ex.Message
                };
            }

            return resultEntity;
        }

        public async Task<ResultEntity> UpdateBatchProcessingStart(RequestUpdateBatchProcessingStartEntity entity)
        {
            var requestUri = $"{authSimulatorBackendUrl}/api/RealtimeService/UpdateBatchProcessingStart";

            ResultEntity resultEntity;
            try
            {
                //result = update count
                var result = await apiHelper.PostAsync(requestUri, entity, headers: headers);
                resultEntity = new ResultEntity()
                {
                    Success = true,
                    ResultCode = "0000",
                    ResultMsg = result
                };
            }
            catch (Exception ex)
            {
                logger.Error($"UpdateBatchProcessingStart Exception: {ex.ToJsonString()}");
                resultEntity = new ResultEntity()
                {
                    Success = false,
                    ResultCode = "9999",
                    ResultMsg = ex.Message
                };
            }

            return resultEntity;
        }

        public async Task<ResultEntity> UpdateBatchProcessEnd(RequestUpdateBatchProcessEndEntity entity)
        {
            var requestUri = $"{authSimulatorBackendUrl}/api/RealtimeService/UpdateBatchProcessEnd";

            ResultEntity resultEntity;
            try
            {
                //result = update count
                var result = await apiHelper.PostAsync(requestUri, entity, headers: headers);
                resultEntity = new ResultEntity()
                {
                    Success = true,
                    ResultCode = "0000",
                    ResultMsg = result
                };
            }
            catch (Exception ex)
            {
                logger.Error($"UpdateBatchProcessEnd Exception: {ex.ToJsonString()}");
                resultEntity = new ResultEntity()
                {
                    Success = false,
                    ResultCode = "9999",
                    ResultMsg = ex.Message
                };
            }

            return resultEntity;
        }
    }
}
