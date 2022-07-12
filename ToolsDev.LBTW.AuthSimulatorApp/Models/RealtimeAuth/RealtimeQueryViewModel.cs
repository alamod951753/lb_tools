using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToolsDev.Service.Entity.RealtimeService;

namespace ToolsDev.LBTW.AuthSimulatorApp.Models.RealtimeAuth
{
    public class RealtimeQueryViewModel
    {
        [Required]
        public int Mode { get; set; }
        public string TerminalID { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string ApproveCode { get; set; }
        public string CardNbr { get; set; }
        public decimal? TransAmt { get; set; }
        public int? TransMode { get; set; }
        public string TransCode { get; set; }
        public List<RealtimeAuthApiEntity> ApiList { get; set; }
        public List<RealtimeAuthBatchQueryEntity> BatchApiList { get; set; }
        public string ErrorMsg { get; set; }

        public RealtimeQueryViewModel()
        {
            ApiList = new List<RealtimeAuthApiEntity>();
            BatchApiList = new List<RealtimeAuthBatchQueryEntity>();
        }
    }
}