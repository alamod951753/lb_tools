using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.Repository.Dto.AuthSimulatorApp
{
    public class RequestGetBatchList_Dto
    {
        public string TerminalID { get; set; }
        public string UploadDate { get; set; }
        public string UploadUser { get; set; }
    }
}
