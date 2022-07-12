using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.Repository.Dto.AuthSimulatorApp
{
    public class RequestUpdateBatchProcessEnd_Dto
    {
        public int Seq { get; set; }
        public string Memo { get; set; }
        public int? DataCount { get; set; }
    }
}
