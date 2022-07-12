using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToolsDev.LBTW.AuthSimulatorBackend.Models.RealtimeService
{
    public class InsertApiListModel
    {
        public List<InsertApiModel> ApiList { get; set; }

        public InsertApiListModel()
        {
            ApiList = new List<InsertApiModel>();
        }
    }
}