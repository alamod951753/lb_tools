using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.Utilities.Model.Library.EAI
{
    /// <summary>
    /// 
    /// </summary>
    public class ResponseMessage
    {
        #region Message Header
        /// <summary>
        /// Main message ID
        /// </summary>
        public string msgId { get; set; }
        /// <summary>
        /// Main message content
        /// </summary>
        public string msgCont { get; set; }
        /// <summary>
        /// Count of additional message
        /// </summary>
        public int adtlMsgCnt { get; set; }
        #endregion
    }
}
