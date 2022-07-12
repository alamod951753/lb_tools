using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToolsDev.LBTW.AuthSimulatorApp.Models.RealtimeAuth
{
    public class RealtimeCreateViewModel
    {
        [Required]
        [Display(Name = "特店代號")]
        public string MerchantID { get; set; }
        [Display(Name = "子特店代號")]
        public string SubMerchantID { get; set; }
        [Required]
        [Display(Name = "端末機代號")]
        public string TerminalID { get; set; }
        [Required]
        [Display(Name = "特店訂單編號")]
        public string OrderID { get; set; }
        [Required]
        [Display(Name = "交易碼")]
        public string TransCode { get; set; }
        [Display(Name = "原購貨授權碼")]
        public string UserDefine { get; set; }
        [Required]
        [Display(Name = "卡號")]
        public string CardNbr { get; set; }
        [Required]
        [Display(Name = "到期日")]
        public string ExpireDate { get; set; }
        [Required]
        [Display(Name = "CVV2")]
        public string CVV2 { get; set; }
        [Required]
        [Display(Name = "請款金額")]
        public decimal TransAmt { get; set; }
        [Required]
        [Display(Name = "交易模式")]
        public int TransMode { get; set; }
        [Display(Name = "分期期數")]
        public int? InstallCount { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        [Display(Name = "授權碼")]
        public string ApproveCode { get; set; }
        public string BatchNo { get; set; }
        public string RRN { get; set; }

        public string MessgeType { get; set; }
        public string ReversalFlag { get; set; }
        public string TransType { get; set; }
        public string InstallType { get; set; }
        public decimal? FirstAmt { get; set; }
        public decimal? EachAmt { get; set; }
        public decimal? Fee { get; set; }
        public string RedeemType { get; set; }
        public int? RedeemUsed { get; set; }
        public int? RedeemBalance { get; set; }
        public decimal? CreditAmt { get; set; }
    }
}