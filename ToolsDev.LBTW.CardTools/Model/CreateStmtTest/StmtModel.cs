using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.LBTW.CardTools.Model.CreateStmtTest
{
    public class StmtModel
    {
        public class StmtDataRecord01
        {
            /// <summary>
            /// 05: sales draft
            /// 06: credit voucher
            /// </summary>
            public string TransactionCode { get; set; }
            /// <summary>
            /// CardNumber
            /// </summary>
            public string AccountNumber { get; set; }
            /// <summary>
            /// MMDDYY
            /// </summary>
            public string PurchaseDate { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal DestinationAmount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal SourceAmount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string MerchantName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string MerchantCity { get; set; }
            /// <summary>
            /// TW
            /// </summary>
            public string MerchantCountryCode { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string MCC { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string POSEntryMode { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string AuthorizationCode { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string MerchantChineseName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string TerminalID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string MerchantCode { get; set; }
        }
    }
}
