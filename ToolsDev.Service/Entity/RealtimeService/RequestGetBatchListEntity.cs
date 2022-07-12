using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.Service.Entity.RealtimeService
{
    public class RequestGetBatchListEntity
    {
        public string TerminalID { get; set; }
        public string UploadDate { get; set; }
        public string UploadUser { get; set; }
    }
}
