using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.Repositories.Dto.Log
{
    /// <summary>
    /// Log File Layout
    /// </summary>
    public class Log_Dto
    {
        public DateTime LogTime { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
    }
}
