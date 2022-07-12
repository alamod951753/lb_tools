using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.Repositories.Dto.CA.ColaTxAuthHist
{
    /// <summary>
    /// 歷史授權記錄檔 query response for NCCC 帳單
    /// </summary>
    public class Rsp_GetColaTxAuthHistForNCCC_Dto
    {
        public string TransactionCode { get; set; }
        public string AccountNumber { get; set; }
        public string PurchaseDate { get; set; }
        public decimal DestinationAmount { get; set; }
        public decimal SourceAmount { get; set; }
        public string MerchantName { get; set; }
        public string MerchantCity { get; set; }
        public string MerchantCountryCode { get; set; }
        public string MCC { get; set; }
        public string POSEntryMode { get; set; }
        public string AuthorizationCode { get; set; }
        public string MerchantChineseName { get; set; }
        public string TerminalID { get; set; }
        public string MerchantCode { get; set; }
    }
}
