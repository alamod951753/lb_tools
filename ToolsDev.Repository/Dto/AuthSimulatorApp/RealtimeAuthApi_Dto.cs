using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.Repository.Dto.AuthSimulatorApp
{
    public class RealtimeAuthApi_Dto
    {
        public int SEQ { get; set; }
        public int? BATCH_SEQ { get; set; }
        public string MERCHANT_ID { get; set; }
		public string SUBMERCHANT_ID { get; set; }
		public string TERMINAL_ID { get; set; }
        public string ORDER_ID { get; set; }
        public string TRANS_CODE { get; set; }
        public string CARD_NBR { get; set; }
        public string EXPIRE_DATE { get; set; }
        public string CVV2 { get; set; }
        public decimal TRANS_AMT { get; set; }
        public int TRANS_MODE { get; set; }
        public int INSTALL_COUNT { get; set; }
        public string CREATE_USER { get; set; }
        public DateTime? REQUEST_DATE { get; set; }
        public string MESSAGE_TYPE { get; set; }
        public string REVERSAL_FLAG { get; set; }
        public string APPROVE_CODE { get; set; }
        public string RESPONSE_CODE { get; set; }
        public string RESPONSE_MSG { get; set; }
        public string BATCH_NO { get; set; }
        public string RRN { get; set; }
        public string TRANS_TYPE { get; set; }
        public string INSTALL_TYPE { get; set; }
        public decimal? FIRST_AMT { get; set; }
        public decimal? EACH_AMT { get; set; }
        public decimal? FEE { get; set; }
        public string REDEEM_TYPE { get; set; }
        public int? REDEEM_USED { get; set; }
        public int? REDEEM_BALANCE { get; set; }
        public decimal? CREDIT_AMT { get; set; }
        public DateTime? RESPONSE_DATE { get; set; }
        public string USER_DEFINE { get; set; }
    }
}
