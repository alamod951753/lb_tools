using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.Repository.Dto.AuthSimulatorApp
{
    public class ResponseGetBatchList_Dto
    {
        public int BATCH_SEQ { get; set; }
        public DateTime UPLOAD_DATE { get; set; }
        public string UPLOAD_USER { get; set; }
        public int? DATA_CNT { get; set; }
        public DateTime? PROCESS_START_DATE { get; set; }
        public DateTime? PROCESS_END_DATE { get; set; }
        public string PROCESS_MEMO { get; set; }
        public string FILE_PATH { get; set; }
        public int? PROCESS_CNT { get; set; }
        public int? SUCCESS_CNT { get; set; }
        public int? FAILED_CNT { get; set; }
        public string TERMINAL_ID  { get; set; }
    }
}
