using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.Utilities.Model.Library.EAI.FundBlock
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestFundBlockModel
    {
        /// <summary>
        /// Customer ID
        /// </summary>
        public string custId { get; set; } = string.Empty;
        /// <summary>
        /// 授權序號 Authorization Transaction GUID 
        /// </summary>
        public string autzTxGuid { get; set; } = string.Empty;
        /// <summary>
        /// 交易支付帳戶號碼 : 預設是MainAccount，客戶有選定則以選定Account
        /// </summary>
        public string acctNbr { get; set; } = string.Empty;
        /// <summary>
        /// 交易金額 : 兩位小數
        /// </summary>
        public decimal arrSrvcBlcknAmt { get; set; } = 0;
        /// <summary>
        /// 交易日期時間 : yyyyMMddHHmmss
        /// </summary>
        public string txDtm { get; set; } = string.Empty;
        /// <summary>
        /// 原始圈存序號 : (一般交易調整/退貨調整/預現調整時使用)
        /// </summary>
        public Int64 arrSrvcBlcknSeqNbr { get; set; } = 0;
        /// <summary>
        /// 調整圈存金額 : 兩位小數 (一般交易調整/退貨調整/預現調整時使用)
        /// </summary>
        public decimal rpmtBlcknAmt { get; set; } = 0;
    }
}
