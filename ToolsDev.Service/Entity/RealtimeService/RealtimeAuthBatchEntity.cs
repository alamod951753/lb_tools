using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.Service.Entity.RealtimeService
{
    public class RealtimeAuthBatchEntity
    {
        public int Seq { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }
        public string UploadUser { get; set; }
        public int? DataCnt { get; set; }
        public DateTime? ProcessStartDate { get; set; }
        public DateTime? ProcessEndDate { get; set; }
    }
}
