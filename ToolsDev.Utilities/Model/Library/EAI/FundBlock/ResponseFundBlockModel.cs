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
    public class ResponseFundBlockModel
    {
        /// <summary>
         /// 回應代碼 : (請依交易結果回覆)(please response block result)
         /// 0000 : 圈存成功(Success)
         /// F001 : 圈存帳號金額不足(Failed, Insufficient amount)
         /// F002 : 圈存帳號已失效(Failed, Account inactive)
         /// F999 : 其他(others)
         /// </summary>
        public string rspsRsltCont { get; set; } = string.Empty;
        /// <summary>
        /// 圈存序號 : 交易結果為圈存成功時提供，供後續改圈/解圈/解圈扣帳使用
        /// </summary>
        public Int64 arrSrvcBlcknSeqNbr { get; set; } = 0;
        /// <summary>
        /// 實際圈存金額 : 兩位小數
        /// </summary>
        public decimal arrSrvcBlcknAmt { get; set; } = 0;
    }
}
