using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.Repositories.Dto.CA
{
    /// <summary>
    /// 歷史授權記錄檔
    /// 尚未更新完畢
    /// </summary>
    public class COLA_TX_AUTH_HIST_Dto
    {
        public string BU { get; set; }
        public string ACCT_NBR { get; set; }
        public string PRODUCT { get; set; }
        public string CURRENCY { get; set; }
        public string CARD_NBR { get; set; }
        public string CARD_PRODUCT { get; set; }
        public string CUST_NBR { get; set; }
        public string SEQ { get; set; }
        public string TX_DTE { get; set; }
        public string TX_TIME { get; set; }
        public DateTime RECEIVE_DTE { get; set; }
        public DateTime RESPONSE_DTW { get; set; }
        public string COUNTRY_CODE { get; set; }
        public string CITY { get; set; }
        public string BIN_ICA { get; set; }
        public string ACQ_BANK_NBR { get; set; }
        public string MER_NBR { get; set; }
        public string MER_STATUS { get; set; }
        public string MER_NAME { get; set; }
        public string POS_NBR { get; set; }
        public string POS_STATUS { get; set; }
        public string MCC_CODE { get; set; }
        public decimal ORIG_TX_AMT { get; set; }
        public decimal PROC_AMT { get; set; }
        public decimal AMT { get; set; }
        public string ORIG_CURRENCY { get; set; }
        public decimal ORIG_AMT { get; set; }
        public string TRACE_NO { get; set; }
        public string REF_NO { get; set; }
        public string MESS_TYPE { get; set; }
        public string PROCESS_CODE { get; set; }
        public string POS_COND { get; set; }
        public string ENTRY_MODE { get; set; }
        public string RESP { get; set; }
        public string AUTH_CODE { get; set; }
        public string RESP_CODE { get; set; }
        public string CHECK_CODE { get; set; }
        public string RSN_CODE { get; set; }
        public string TX_FLAG { get; set; }
        public string BUSINESS_FLAG { get; set; }
        public string ADJ_FLAG { get; set; }
    }
}
