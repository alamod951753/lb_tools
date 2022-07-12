using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.Utilities.Model.Library.EAI
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestModal
    {
        /// <summary>
        /// 
        /// </summary>
        public Header header { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public RequestModal()
        {
            GetInitedHeader();
        }

        /// <summary>
        /// 
        /// </summary>
        public void GetInitedHeader()
        {
            string datetimeNow = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            #region 取Guid
            //Random rnd = new Random(Guid.NewGuid().GetHashCode());
            System.Text.StringBuilder randstring = new System.Text.StringBuilder();
            //char[] chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            char[] chars = Guid.NewGuid().ToString().Replace("-", "").ToCharArray();

            for (int i = 0; i < 8; i++)
            {
                //randstring.Append(chars[CMCDES001.Next(chars.Length - 1)]);
                randstring.Append(chars[chars.Length - (i + 1)]);
            }
            #endregion

            string guidString = datetimeNow + "DEC" + "01" + "01" + randstring.ToString();

            #region 取IP V4
            // 取得本機名稱
            string ipv4String = string.Empty;
            //string strHostName = Dns.GetHostName();
            string strHostName = System.Environment.MachineName;
            // 取得本機的IpHostEntry類別實體，MSDN建議新的用法
            IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);
            // 取得所有 IP 位址
            foreach (IPAddress ipaddress in iphostentry.AddressList)
            {
                // 只取得IP V4的Address
                if (ipaddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipv4String = ipaddress.ToString();
                    break;
                }
            }
            #endregion
            header = new Header
            {
                guid = guidString,
                guidSeqNbr = 1,
                italGuid = guidString,
                rcvSysCd = "CBK",
                rqstRspsDsCd = "S",
                clntIpv4IpAddr = ipv4String,
                frstTnsnSysCd = "DEC",
                frstTnsnNodeNbr = "01",
                frstTnsnIstcNbr = "01",
                frstChnlDsCd = "ZZZ",
                frstTnsnTmstmp = datetimeNow,
                rntmEnvDsCd = "P",
                tnsnSysCd = "DEC",
                tnsnNodeNbr = "01",
                tnsnIstcNbr = "01",
                tnsnTmstmp = datetimeNow,
                txDt = DateTime.Now.ToString("yyyyMMdd")
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InterfaceID"></param>
        public void SetInterfaceID(string InterfaceID)
        {
            this.header.rcvSrvcId = InterfaceID;
            this.header.ifId = InterfaceID;
            if (string.IsNullOrEmpty(this.header.italIfId))
            {
                this.header.italIfId = InterfaceID;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        public void ParsingRequest(dynamic req)
        {
            header = new Header
            {
                stdTgmVer = req.header.stdTgmVer ?? 0,
                guid = req.header.guid ?? "",
                guidSeqNbr = req.header.guidSeqNbr ?? 0,
                italGuid = req.header.italGuid ?? "",
                rcvSysCd = req.header.rcvSysCd ?? "",
                rcvSrvcId = req.header.rcvSrvcId ?? "",
                rqstRspsDsCd = req.header.rqstRspsDsCd ?? "",
                tgmTpCd = req.header.tgmTpCd ?? "",
                ifId = req.header.ifId ?? "",
                pushTrgtDsCd = req.header.pushTrgtDsCd,
                pushTrgtStaffId = req.header.pushTrgtStaffId,
                extrInstCd = req.header.extrInstCd ?? "",
                extrBizDsCd = req.header.extrBizDsCd ?? "",
                clbkSrvcId = req.header.clbkSrvcId,
                unitTstYn = req.header.unitTstYn,
                extrSmlnTstYn = req.header.extrSmlnTstYn ?? "",
                clntIpv4IpAddr = req.header.clntIpv4IpAddr ?? "",
                clntIpv6IpAddr = req.header.clntIpv6IpAddr ?? "",
                scrnId = req.header.scrnId ?? "",
                staffId = req.header.staffId ?? "",
                deptId = req.header.deptId ?? "",
                sssnId = req.header.sssnId ?? "",
                asynClbkNodeNbr = req.header.asynClbkNodeNbr ?? "",
                asynClbkIstcNbr = req.header.asynClbkIstcNbr ?? "",
                frstTnsnSysCd = req.header.frstTnsnSysCd ?? "",
                frstTnsnNodeNbr = req.header.frstTnsnNodeNbr ?? "",
                frstTnsnIstcNbr = req.header.frstTnsnIstcNbr ?? "",
                frstChnlDsCd = req.header.frstChnlDsCd ?? "",
                italIfId = req.header.italIfId ?? "",
                frstTnsnTmstmp = req.header.frstTnsnTmstmp ?? "",
                lclCd = req.header.lclCd ?? "",
                rntmEnvDsCd = req.header.rntmEnvDsCd ?? "",
                tnsnSysCd = req.header.tnsnSysCd ?? "",
                tnsnNodeNbr = req.header.tnsnNodeNbr ?? "",
                tnsnIstcNbr = req.header.tnsnIstcNbr ?? "",
                tnsnTmstmp = req.header.tnsnTmstmp ?? "",
                rspsRsltCd = req.header.rspsRsltCd ?? "",
                rspsTpCd = req.header.rspsTpCd ?? "",
                aprvlStaffId = req.header.aprvlStaffId,
                aprvlDeptId = req.header.aprvlDeptId,
                aprvlId = req.header.aprvlId ?? "",
                aprvlAutoPrcgYn = req.header.aprvlAutoPrcgYn,
                umskId = req.header.umskId ?? "",
                ppupScrnId = req.header.ppupScrnId ?? "",
                custId = req.header.custId ?? "",
                afClsgYn = req.header.afClsgYn ?? "",
                prvsDayTxYn = req.header.prvsDayTxYn ?? "",
                txDt = req.header.txDt ?? "",
                ccId = req.header.ccId
            };
            data = req.data;
        }
    }
}
