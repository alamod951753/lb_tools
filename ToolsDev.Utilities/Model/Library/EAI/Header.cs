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
    public class Header
    {
        #region Standard System Header
        /// <summary>
        /// Version of standard telegram
        /// </summary>
        public int stdTgmVer { get; set; } = 57;
        /// <summary>
        /// Number the GUID and record it as a telegram unique number in the Initial system that generates the transaction, or in the system that creates the asynchronous transaction.
        /// </summary>
        public string guid { get; set; }
        /// <summary>
        /// For Trace the GUID of going through multiple nodes
        /// </summary>
        public int guidSeqNbr { get; set; }
        /// <summary>
        /// Initial GUIDs are recorded as telegram unique numbers in the initial system that generates the transaction.
        /// </summary>
        public string italGuid { get; set; }
        /// <summary>
        /// System code to receive request telegram
        /// </summary>
        public string rcvSysCd { get; set; }
        /// <summary>
        /// Distinguish whether the telegram is a request or a response
        /// </summary>
        public string rcvSrvcId { get; set; }
        /// <summary>
        /// Distinguish whether telegrams are Sync or Async
        /// </summary>
        public string rqstRspsDsCd { get; set; }
        /// <summary>
        /// Distinguish whether telegrams are Sync or Async
        /// </summary>
        public string tgmTpCd { get; set; } = "S";
        /// <summary>
        /// Interface ID for channel-linked calls
        /// </summary>
        public string ifId { get; set; }
        /// <summary>
        /// Distinguish Push Targets
        /// </summary>
        public object pushTrgtDsCd { get; set; }
        /// <summary>
        /// Specific user ID
        /// </summary>
        public object pushTrgtStaffId { get; set; }
        /// <summary>
        /// About Routing of external channel in transactions
        /// </summary>
        public string extrInstCd { get; set; }
        /// <summary>
        /// External Institution & Business Distinction Code
        /// </summary>
        public string extrBizDsCd { get; set; }
        /// <summary>
        /// Service ID for call-back processing of outbound async transaction
        /// </summary>
        public object clbkSrvcId { get; set; }
        /// <summary>
        /// For unit testing Y/N in development 
        /// </summary>
        public object unitTstYn { get; set; }
        /// <summary>
        /// For external simulation testing Y/N in development 
        /// </summary>
        public string extrSmlnTstYn { get; set; }
        /// <summary>
        /// IPv4 address
        /// </summary>
        public string clntIpv4IpAddr { get; set; }
        /// <summary>
        /// IPv6 address
        /// </summary>
        public string clntIpv6IpAddr { get; set; }
        /// <summary>
        /// Screen ID
        /// </summary>
        public string scrnId { get; set; } = "00000000000";
        /// <summary>
        /// The employee number of the operator that caused the transaction.
        /// </summary>
        public string staffId { get; set; } = "DECSystem";
        /// <summary>
        /// Department code to which the user belongs when logging in for the first time
        /// </summary>
        public string deptId { get; set; } = "99999";
        /// <summary>
        /// ID for session management
        /// </summary>
        public string sssnId { get; set; }
        /// <summary>
        /// TODO 57版才加的欄位
        /// </summary>
        public string asynClbkNodeNbr { get; set; } = "01";
        /// <summary>
        /// TODO 57版才加的欄位
        /// </summary>
        public string asynClbkIstcNbr { get; set; } = "01";
        /// <summary>
        /// System code that generated the first request telegram
        /// </summary>
        public string frstTnsnSysCd { get; set; }
        /// <summary>
        /// Node number setted in the system that generated the first request telegram
        /// </summary>
        public string frstTnsnNodeNbr { get; set; } = "01";
        /// <summary>
        /// Instance number setted in the system that generated the first request telegram
        /// </summary>
        public string frstTnsnIstcNbr { get; set; } = "01";
        /// <summary>
        /// Channel code that generated the first request telegram
        /// </summary>
        public string frstChnlDsCd { get; set; }
        /// <summary>
        /// Interface ID at the time the telegram was first created
        /// </summary>
        public string italIfId { get; set; }
        /// <summary>
        /// Date and time when the first request telegram was created
        /// </summary>
        public string frstTnsnTmstmp { get; set; }
        /// <summary>
        /// User location & language environment
        /// </summary>
        public string lclCd { get; set; } = "zh-TW";
        /// <summary>
        /// Environment code for telegram generation system
        /// </summary>
        public string rntmEnvDsCd { get; set; }
        /// <summary>
        /// System code for sending request/response telegram
        /// </summary>
        public string tnsnSysCd { get; set; }
        /// <summary>
        /// Node number setted in the system
        /// </summary>
        public string tnsnNodeNbr { get; set; } = "01";
        /// <summary>
        /// Instance number setted in the system
        /// </summary>
        public string tnsnIstcNbr { get; set; } = "01";
        /// <summary>
        /// Date and time when the first request telegram was created
        /// </summary>
        public string tnsnTmstmp { get; set; }
        /// <summary>
        /// Result Code of response telegram
        /// </summary>
        public string rspsRsltCd { get; set; }
        /// <summary>
        /// Response Type
        /// </summary>
        public string rspsTpCd { get; set; }
        #endregion
        #region Standard Business Header
        /// <summary>
        /// The employee number of the operator that caused the transaction.
        /// </summary>
        public object aprvlStaffId { get; set; }
        /// <summary>
        /// Department code to which the user belongs when logging in for the first time
        /// </summary>
        public object aprvlDeptId { get; set; }
        /// <summary>
        /// Approval ID
        /// </summary>
        public string aprvlId { get; set; }
        /// <summary>
        /// Approval Auto Processing Y/N
        /// </summary>
        public object aprvlAutoPrcgYn { get; set; }
        /// <summary>
        /// Unmask ID
        /// </summary>
        public string umskId { get; set; }
        /// <summary>
        /// Popup Screen ID
        /// </summary>
        public string ppupScrnId { get; set; }
        /// <summary>
        /// Customer ID
        /// </summary>
        public string custId { get; set; }
        /// <summary>
        /// After closing Y/N
        /// </summary>
        public string afClsgYn { get; set; } = "0";
        /// <summary>
        /// Transaction of previous day Y/N
        /// </summary>
        public string prvsDayTxYn { get; set; } = "0";
        /// <summary>
        /// Transaction Date
        /// </summary>
        public string txDt { get; set; }
        /// <summary>
        /// Center-cut ID when processed as a center-cut
        /// </summary>
        public object ccId { get; set; }
        #endregion
    }
}
