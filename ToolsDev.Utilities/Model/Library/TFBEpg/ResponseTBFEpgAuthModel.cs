using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.Utilities.Model.Library.TFBEpg
{
    /// <summary>
    /// Taipei Fubon Auth Api Response model
    /// </summary>
    public class ResponseTBFEpgAuthModel : RequestTFBEpgAuthModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string MessageType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ReversalFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ApproveCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ResponseCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ResponseMsg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BatchNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RRN { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TransType { get; set; }
        /// <summary>
        /// below with TransMode='1'
        /// </summary>
        public string InstallType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? FirstAmt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? EachAmt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Fee { get; set; }
        /// <summary>
        /// below with TransMode='2'
        /// </summary>
        public string RedeemType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? RedeemUsed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? RedeemBalance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? CreditAmt { get; set; }
    }
}
