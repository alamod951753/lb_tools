using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.Service.Entity.RealtimeService
{
    public class RealtimeAuthApiEntity
    {
        public int Seq { get; set; }
        public int? BatchSeq { get; set; }
        public string MerchantID { get; set; }
        public string SubMerchantID { get; set; }
        public string TerminalID { get; set; }
        public string OrderID { get; set; }
        public string TransCode { get; set; }
        public string CardNbr { get; set; }
        public string ExpireDate { get; set; }
        public string CVV2 { get; set; }
        public decimal TransAmt { get; set; }
        public int TransMode { get; set; }
        public int InstallCount { get; set; }
        public DateTime? RequestDate { get; set; }
        public string CreateUser { get; set; }

        public string MessageType { get; set; }
        public string ReversalFlag { get; set; }
        public string ApproveCode { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        public string BatchNo { get; set; }
        public string RRN { get; set; }
        public string TransType { get; set; }
        public string InstallType { get; set; }
        public decimal? FirstAmt { get; set; }
        public decimal? EachAmt { get; set; }
        public decimal? Fee { get; set; }
        public string RedeemType { get; set; }
        public int? RedeemUsed { get; set; }
        public int? RedeemBalance { get; set; }
        public decimal? CreditAmt { get; set; }
        public DateTime? ResponseDate { get; set; }
        public string UserDefine { get; set; }
    }
}
