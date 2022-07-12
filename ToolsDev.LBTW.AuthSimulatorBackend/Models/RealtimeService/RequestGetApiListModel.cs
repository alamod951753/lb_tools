using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToolsDev.LBTW.AuthSimulatorBackend.Models.RealtimeService
{
    public class RequestGetApiListModel
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