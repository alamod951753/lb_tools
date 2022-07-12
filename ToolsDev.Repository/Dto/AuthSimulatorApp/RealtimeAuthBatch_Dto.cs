using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.Repository.Dto.AuthSimulatorApp
{
    public class RealtimeAuthBatch_Dto
    {
        public int SEQ { get; set; }
        public string FILE_PATH { get; set; }
		public DateTime UPLOAD_DATE { get; set; }
		public string UPLOAD_USER { get; set; }
        public int? DATA_CNT { get; set; }
        public DateTime? PROCESS_START_DATE { get; set; }
        public DateTime? PROCESS_END_DATE { get; set; }
	}
}
