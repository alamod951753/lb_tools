using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.Service.Entity.RealtimeService
{
    public class RealtimeAuthApiListEntity
    {
        public List<RealtimeAuthApiEntity> ApiList { get; set; }

        public RealtimeAuthApiListEntity()
        {
            ApiList = new List<RealtimeAuthApiEntity>();
        }
    }
}
