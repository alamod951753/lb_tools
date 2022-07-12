using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.Service.Entity.RealtimeService
{
    public class RequestUpdateBatchProcessEndEntity
    {
        public int Seq { get; set; }
        public string Memo { get; set; }
        public int? DataCount { get; set; }
    }
}
