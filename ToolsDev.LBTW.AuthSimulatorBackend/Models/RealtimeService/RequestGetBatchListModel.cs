using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToolsDev.LBTW.AuthSimulatorBackend.Models.RealtimeService
{
    public class RequestGetBatchListModel
    {
        public string TerminalID { get; set; }
        public string UploadDate { get; set; }
        public string UploadUser { get; set; }
    }
}