using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.Repository.Dto.AuthSimulatorApp
{
    public class RequestGetApiList_Dto
    {
        public string TerminalID { get; set; }
        public string RequestDate { get; set; }
        public string CreateUser { get; set; }
        public string ApproveCode { get; set; }
        public string CardNbr { get; set; }
        public decimal? TransAmt { get; set; }
        public int? TransMode { get; set; }
        public string TransCode { get; set; }
    }
}
