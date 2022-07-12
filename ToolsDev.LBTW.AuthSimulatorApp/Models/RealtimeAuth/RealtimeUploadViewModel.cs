using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToolsDev.LBTW.AuthSimulatorApp.Models.RealtimeAuth
{
    public class RealtimeUploadViewModel
    {
        [Required]
        [Display(Name = "檔案上傳")]
        public HttpPostedFileBase UploadFile { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        public DateTime? UploadTime { get; set; }
        public string UploadFileName { get; set; }
    }
}