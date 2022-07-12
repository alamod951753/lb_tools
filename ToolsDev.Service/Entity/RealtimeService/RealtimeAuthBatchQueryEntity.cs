using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.Service.Entity.RealtimeService
{
    public class RealtimeAuthBatchQueryEntity
    {
        public int BatchSeq { get; set; }
        public DateTime UploadDate { get; set; }
        public string UploadUser { get; set; }
        public int? DataCnt { get; set; }
        public DateTime? ProcessStartDate { get; set; }
        public DateTime? ProcessEndDate { get; set; }
        public string ProcessMemo { get; set; }
        public string FilePath { get; set; }
        public int? ProcessCnt { get; set; }
        public int? SuccessCnt { get; set; }
        public int? FailedCnt { get; set; }
        public List<RealtimeAuthApiEntity> BatchFailDetailList { get; set; }
        public RealtimeAuthBatchQueryEntity()
        {
            BatchFailDetailList = new List<RealtimeAuthApiEntity>();
        }
    }
}
