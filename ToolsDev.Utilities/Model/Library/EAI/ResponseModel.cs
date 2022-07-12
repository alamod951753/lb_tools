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
    public class ResponseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Header header { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ResponseMessage message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ResponseModel()
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
                rqstRspsDsCd = "R",
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
                txDt = DateTime.Now.ToString("yyyyMMdd"),
                rspsRsltCd = "0",
                rspsTpCd = "0"
            };
            this.message = new ResponseMessage
            {
                msgId = "",
                msgCont = "",
                adtlMsgCnt = 0
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reqHeader"></param>
        public void SetHeaderByRequestHeader(Header reqHeader)
        {
            this.header.guid = reqHeader.guid;
            this.header.italGuid = reqHeader.italGuid;
            this.header.guidSeqNbr = reqHeader.guidSeqNbr + 1;
            this.header.rcvSrvcId = reqHeader.rcvSrvcId;
            this.header.ifId = reqHeader.ifId;
            this.header.italIfId = reqHeader.italIfId;
            this.header.frstTnsnTmstmp = reqHeader.frstTnsnTmstmp;
            this.header.frstTnsnSysCd = reqHeader.frstTnsnSysCd;
            this.header.frstTnsnNodeNbr = reqHeader.frstTnsnNodeNbr;
            this.header.frstTnsnIstcNbr = reqHeader.frstTnsnIstcNbr;
            this.header.frstChnlDsCd = reqHeader.frstChnlDsCd;
            this.header.custId = reqHeader.custId;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetNormalMessage()
        {
            this.header.rspsRsltCd = "0";
            this.message.msgId = "LBNA000001";
            this.message.msgCont = "successfully approved.";
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetMessageHeader()
        {

        }

        /// <summary>
        /// Set Return Response Information from JObject.Parse
        /// </summary>
        /// <param name="response">JObject.Parse Result</param>
        public void SetReturnHeaderandResponseMessage(object response)
        {
            if (response is Newtonsoft.Json.Linq.JObject)
            {
                dynamic resp = response as dynamic;
                //buildup response header
                this.header.guid = (string)resp.header.guid ?? "";
                this.header.italGuid = (string)resp.header.italGuid ?? "";
                if (int.TryParse((string)resp.header.guidSeqNbr ?? "", out int guidseqnbr))
                {
                    this.header.guidSeqNbr = guidseqnbr;
                }
                this.header.rcvSrvcId = (string)resp.header.rcvSrvcId ?? "";
                this.header.ifId = (string)resp.header.ifId ?? "";
                this.header.italIfId = (string)resp.header.italIfId ?? "";
                this.header.frstTnsnTmstmp = (string)resp.header.frstTnsnTmstmp ?? "";
                this.header.frstTnsnSysCd = (string)resp.header.frstTnsnSysCd ?? "";
                this.header.frstTnsnNodeNbr = (string)resp.header.frstTnsnNodeNbr ?? "";
                this.header.frstTnsnIstcNbr = (string)resp.header.frstTnsnIstcNbr ?? "";
                this.header.frstChnlDsCd = (string)resp.header.frstChnlDsCd ?? "";
                this.header.custId = (string)resp.header.custId ?? "";
                //buildup ResponseMessage
                this.message.msgId = (string)resp.message.msgId ?? "";
                this.message.msgCont = (string)resp.message.msgCont ?? "";
                if (int.TryParse((string)resp.message.adtlMsgCnt ?? "", out int adtlmsgcnt))
                {
                    this.message.adtlMsgCnt = adtlmsgcnt;
                }
                this.message.msgId = (string)resp.message.msgId ?? "";
            }
        }
    }
}
