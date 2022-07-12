using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToolsDev.LBTW.AuthSimulatorBackend.Models.RealtimeService
{
    public class RequestUpdateBatchProcessEndModel
    {
        public int Seq { get; set; }
        public string Memo { get; set; }
        public int? DataCount { get; set; }
    }
}