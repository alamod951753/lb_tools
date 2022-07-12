using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToolsDev.LBTW.AuthSimulatorBackend.Models.RealtimeService
{
    public class InsertBatchApiModel
    {
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }
        public string UploadUser { get; set; }
        public int? DataCnt { get; set; }
        public DateTime? ProcessStartDate { get; set; }
        public DateTime? ProcessEndDate { get; set; }
    }
}