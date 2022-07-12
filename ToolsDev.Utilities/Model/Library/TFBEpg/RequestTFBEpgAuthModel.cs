using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.Utilities.Model.Library.TFBEpg
{
    /// <summary>
    /// Taipei Fubon Auth Api Request model
    /// </summary>
    public class RequestTFBEpgAuthModel
    {
        /// <summary>
        /// 特店代號
        /// </summary>
        public string MerchantID { get; set; }
        /// <summary>
        /// 子特店代號
        /// </summary>
        public string SubMerchantID { get; set; }
        /// <summary>
        /// 端末機代號
        /// </summary>
        public string TerminalID { get; set; }
        /// <summary>
        /// 特店訂單編號
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TransDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TransTime { get; set; }
        /// <summary>
        /// 交易碼
	    /// 00:購貨
	    /// 01:退貨
	    /// 10:購貨取消
	    /// 11:退貨取消
	    /// 81:身份驗證
        /// </summary>
        public string TransCode { get; set; }
        /// <summary>
        /// TransCode='00': 存放商店自訂的自有資訊
        /// TransCode='10','01', '11': 原購貨之授權碼 Mandatory
        /// </summary>
        public string UserDefine { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CardNbr { get; set; }
        /// <summary>
        /// MMYY 西元年後2碼
        /// </summary>
        public string ExpireDate { get; set; }
        /// <summary>
        /// 卡片背後後3碼
        /// </summary>
        public string CVV2 { get; set; }
        /// <summary>
        /// 請款金額
        /// </summary>
        public decimal TransAmt { get; set; }
        /// <summary>
        /// 交易模式 0:一般授權 1:分期 2:紅利
        /// </summary>
        public int TransMode { get; set; }
        /// <summary>
        /// 分期期數
        /// </summary>
        public int InstallCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public RequestTFBEpgAuthModel()
        {
            MerchantID = "";
            SubMerchantID = "";
            TerminalID = "";
            OrderID = "";
            TransCode = "";
            UserDefine = "";
            TransDate = "";
            TransTime = "";
            CardNbr = "";
            ExpireDate = "";
            CVV2 = "";
            TransAmt = 0;
            TransMode = 0;
            InstallCount = 1;
        }
    }
}
