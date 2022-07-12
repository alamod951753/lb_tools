using System;
using System.Web;
using ToolsDev.Utilities.Helper;
using ToolsDev.Utilities.Model;
using ToolsDev.Utilities.Model.Library.TFBEpg;

namespace ToolsDev.Utilities.Library
{
    /// <summary>
    /// 
    /// </summary>
    public class TFBEpgService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Result STAuthApi(RequestTFBEpgAuthModel model, HttpContext context = null)
        {
            Result result;

            var logDir = UtilityHelper.GetAppSetting("TFB_EPG_LogDir");
            var initialFile = UtilityHelper.GetAppSetting("TFB_EPG_InitialFile");
            if (context == null)
            {
                context = HttpContext.Current;
            }

            try
            {
                ePG_Net_API.TFB_ePG ePG_api = new ePG_Net_API.TFB_ePG(logDir, context.Server.MapPath(initialFile))
                {
                    MerchantID = model.MerchantID,
                    SubMID = model.SubMerchantID,
                    TerminalID = model.TerminalID,
                    OrderID = model.OrderID,
                    TransDate = model.TransDate,
                    TransTime = model.TransTime,
                    TransCode = model.TransCode,
                    UserDefine = model.UserDefine,
                    PAN = model.CardNbr,
                    ExpireDate = model.ExpireDate,
                    CVV2 = model.CVV2,
                    TransAmt = model.TransAmt,
                    TransMode = model.TransMode,
                    InstallCount = model.InstallCount
                };
                
                var responseCode = ePG_api.STAuth();
                if (responseCode != 0)
                {
                    result = new Result
                    {
                        Success = false,
                        ResultCode = responseCode.ToString(),
                        ResultMsg = "執行授權交易失敗"
                    };
                    return result;
                }

                /*一般授權交易*/
                //交易有送至授權主機取回ResponseCode判斷是否授權成功
                if ((ePG_api.ResponseCode == "000") == false)
                {
                    result = new Result
                    {
                        Success = false,
                        ResultCode = ePG_api.ResponseCode,
                        ResultMsg = ePG_api.ResponseMsg
                    };
                    return result;
                }

                var responseModel = new ResponseTBFEpgAuthModel()
                {
                    ApproveCode = ePG_api.ApproveCode,      //授權碼
                    MessageType = ePG_api.MessageType,
                    ReversalFlag = ePG_api.ReversalFlag,
                    ResponseCode = ePG_api.ResponseCode,
                    ResponseMsg = ePG_api.ResponseMsg,
                    BatchNo = ePG_api.BatchNo,
                    RRN = ePG_api.RRN,
                    TransType = ePG_api.TransType,
                    //TransMode='1': installment
                    InstallType = ePG_api.InstallType,
                    FirstAmt = ePG_api.FirstAmt,
                    EachAmt = ePG_api.EachAmt,
                    Fee = ePG_api.Fee,
                    //TransMode='2': point
                    RedeemType = ePG_api.RedemType,
                    RedeemUsed = ePG_api.RedemUsed,
                    RedeemBalance = ePG_api.RedemBalance,
                    CreditAmt = ePG_api.CreditAmt
                };

                bool blnResult = false;
                //交易授權成功
                if (ePG_api.ResponseMsg.Length >= 8 && ePG_api.ResponseMsg.ToUpper().Substring(0, 8) == "APPROVED" && ePG_api.ApproveCode != "")
                {
                    blnResult = true;
                }

                result = new Result
                {
                    Success = blnResult,
                    ResultCode = ePG_api.ResponseCode,
                    ResultMsg = ePG_api.ResponseMsg,
                    ResultObject = responseModel
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new Result
                {
                    Success = false,
                    ResultCode = "9999",
                    ResultMsg = $"Library.TFBEpgService.STAuthApi exception, {ex.Message}"
                };
                return result;
            }
        }
    }
}
