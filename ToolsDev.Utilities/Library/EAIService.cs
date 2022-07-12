using System;
using System.Threading.Tasks;
using ToolsDev.Utilities.Helper;
using ToolsDev.Utilities.Model;
using ToolsDev.Utilities.Model.Library.EAI;
using ToolsDev.Utilities.Model.Library.EAI.FundBlock;

namespace ToolsDev.Utilities.Library
{
    /// <summary>
    /// 
    /// </summary>
    public class EAIService
    {
        private readonly string _env;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        public EAIService(string env)
        {
            _env = env;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result AuthFundBlockApi(RequestFundBlockModel model)
        {
            Result result;
            var apiHelper = new ApiHelper();
            var postUrl = "";
            switch (_env.ToLower())
            {
                case "stg":
                    postUrl = UtilityHelper.GetAppSetting("EAI_Url");
                    break;
                case "uat":
                    postUrl = UtilityHelper.GetAppSetting("EAI_Url_UAT");
                    break;
            }

            try
            {
                var request = new RequestModal();
                request.SetInterfaceID("EDECCBKA023001");
                request.data = model;

                var rsp = apiHelper.PostAsync(postUrl, request, skipCertValidationCheck: true);
                if (rsp == null || rsp.Status != TaskStatus.RanToCompletion)
                {
                    result = new Result
                    {
                        Success = false,
                        ResultCode = "9994",
                        ResultMsg = $"Library.EAIService.AuthFundBlockApi Post response {((rsp == null) ? "null." : $"error: {rsp?.Exception.ToJsonString()}")}"
                    };
                }

                var response = rsp.Result.ToJsonFormat<ResponseModel>();
                if (response == null || response.header == null || response.data == null || response.header.rspsRsltCd != "0")
                {
                    result = new Result
                    {
                        Success = false,
                        ResultCode = "9995",
                        ResultMsg = $"Library.EAIService.AuthFundBlockApi Post response {((response == null) ? "null." : $"error: {response?.ToJsonString()}")}"
                    };
                }

                var data = response.data?.ToJsonString().ToJsonFormat<ResponseFundBlockModel>();
                if (data != null && data.rspsRsltCont == "0000")
                {
                    result = new Result
                    {
                        Success = true,
                        ResultCode = "0000",
                        ResultMsg = "AuthFundBlockApi Success!",
                        ResultObject = data.arrSrvcBlcknSeqNbr.ToString()
                    };
                }
                else
                {
                    result = new Result
                    {
                        Success = false,
                        ResultCode = data.rspsRsltCont,
                        ResultMsg = $"AuthFundBlockApi Fail! header: {response.header}, message: {response.message}",
                        ResultObject = ""
                    };
                }
                return result;
            }
            catch (Exception ex)
            {
                result = new Result
                {
                    Success = false,
                    ResultCode = "9999",
                    ResultMsg = $"Library.EAIService.AuthFundBlockApi exception, {ex.Message}"
                };
                return result;
            }
        }
    }
}
