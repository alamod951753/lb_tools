using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.Utilities.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ResultCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ResultMsg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object ResultObject { get; set; }
    }
}
