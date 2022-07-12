using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.Service.Entity.RealtimeService
{
    public class RequestGetApiListEntity
    {
        public string TerminalID { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string ApproveCode { get; set; }
        public string CardNbr { get; set; }
        public decimal? TransAmt { get; set; }
        public int? TransMode { get; set; }
        public string TransCode { get; set; }
    }
}
