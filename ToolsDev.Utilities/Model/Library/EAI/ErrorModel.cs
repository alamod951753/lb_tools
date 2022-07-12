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
    public class ErrorModel
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
        public ErrorModel()
        {
            string datetimeNow = DateTime.Now.ToString("yyyyMMddHHmmssfff");

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
                rqstRspsDsCd = "R",
                clntIpv4IpAddr = ipv4String,
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
                msgCont = ""
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
        /// <param name="Error_Code"></param>
        /// <param name="Error_Message"></param>
        public void SetResultCode(string Error_Code, ResponseMessage Error_Message)
        {
            //rspsRsltCd:
            //'0' = Normal processing
            //'1' = System Error
            //'2' = Biz Error
            //'3' = DB Error

            string _msgId = string.Empty;
            string _Parm = string.Empty;
            if (Error_Code.Contains(","))
            {
                _msgId = Error_Code.Split(',')[0];
                _Parm = Error_Code.Split(',')[1];
            }
            else
            {
                _msgId = Error_Code;
            }

            this.message.msgId = _msgId;

            switch (_msgId)
            {
                case "":
                    this.header.rspsRsltCd = "0";
                    this.message.msgId = "LBNA000001";
                    this.message.msgCont = "successfully approved.";
                    break;
                case "LBNA000001":
                    this.header.rspsRsltCd = "0";
                    this.message.msgCont = "successfully approved.";
                    break;
                case "LBEA001953": //Biz Error
                    this.header.rspsRsltCd = "2";
                    this.message.msgCont = "業務邏輯檢核錯誤，請聯絡本行客服中心。";
                    break;
                case "LBEA001954": //field check Error
                    this.header.rspsRsltCd = "2";
                    if (!"".Equals(_Parm))
                    {
                        //this.message.msgCont = String.Format("欄位確認錯誤, Field name is '{0}'", _Parm);
                        this.message.msgCont = String.Format("欄位確認錯誤。");
                    }
                    else
                    {
                        this.message.msgCont = "欄位確認錯誤。";
                    }
                    break;
                case "LBEA002044": //field check Error
                    this.header.rspsRsltCd = "2";
                    if (!"".Equals(_Parm))
                    {
                        this.message.msgCont = _Parm;
                    }
                    else
                    {
                        this.message.msgCont = "Field check failed.";
                    }
                    break;
                case "LBEA001956": //DB Error
                    this.header.rspsRsltCd = "3";
                    this.message.msgCont = "很抱歉，因目前交易量大，造成系統壅塞，請您稍後再試。";
                    break;
                case "LBEA001908": //客戶資料不存在
                    this.header.rspsRsltCd = "2";
                    this.message.msgCont = "您尚未申請LINE Bank 簽帳金融卡。";
                    break;
                case "LBEA002001": //帳戶資料不存在 回傳 rspsRsltCd 0
                    this.header.rspsRsltCd = "0";
                    this.message.msgCont = "該帳號不存在，請洽客服中心。";
                    break;
                case "LBEA001957": //No Data => TO CHECK
                    this.header.rspsRsltCd = "0";
                    this.message.msgCont = "查無相關資料。";
                    break;
                case "LBEA001959":
                    this.header.rspsRsltCd = "0";
                    this.message.msgCont = "資料無異動。";
                    break;
                case "LBEA001963":
                    this.header.rspsRsltCd = "2";
                    this.message.msgCont = "金融卡功能檢核錯誤，請聯絡本行客服中心。";
                    break;
                case "LBEA001958": //System Error
                    this.header.rspsRsltCd = "1";
                    this.message.msgCont = "很抱歉，因目前交易量大，造成系統壅塞，請您稍後再試。";
                    break;
                case "LBEA001967": //卡片已補發過
                    this.header.rspsRsltCd = "1";
                    this.message.msgCont = "已申請卡片換補發。";
                    break;
                case "LBEA001970": //客戶異常狀態不可補發卡片
                    this.header.rspsRsltCd = "1";
                    this.message.msgCont = "無法換補發卡片。";
                    break;
                case "LBEA001994":
                    this.message.msgCont = "密碼輸入不一致，請重新確認。";
                    this.header.rspsRsltCd = "1";
                    break;
                case "LBEA001995":
                    this.message.msgCont = "密碼不能與目前使用的密碼相同。";
                    this.header.rspsRsltCd = "1";
                    break;
                case "LBEA001996":
                    this.message.msgCont = "新密碼設定不符合規範，請重新輸入。";
                    this.header.rspsRsltCd = "1";
                    break;
                case "LBEA001997":
                    this.message.msgCont = "由於您連續輸入錯誤卡號達5次，您的卡片已暫時鎖住。若要解除鎖定，請聯絡本行客戶服務中心。";
                    this.header.rspsRsltCd = "1";
                    break;
                case "LBEA001998":
                    this.message.msgCont = "海外提款密碼輸入錯誤。";
                    this.header.rspsRsltCd = "1";
                    if (!"".Equals(_Parm))
                    {
                        //this.message.msgCont = String.Format("海外密碼輸入錯誤 {0} 次!", _Parm);
                        this.message.msgCont = String.Format("海外提款密碼輸入錯誤。");
                    }
                    else
                    {
                        this.message.msgCont = "海外提款密碼輸入錯誤。";
                    }
                    break;
                case "LBEA001999":
                    this.message.msgCont = "起始日不得晚於截止日";
                    this.header.rspsRsltCd = "1";
                    break;
                case "LBEA002002":
                    this.message.msgCont = "iPass資料不存在，請洽客服中心。";
                    this.header.rspsRsltCd = "1";
                    break;
                case "LBEA002006":
                    this.header.rspsRsltCd = "2";
                    if (!"".Equals(_Parm))
                    {
                        this.message.msgCont = String.Format(_Parm);
                    }
                    else
                    {
                        this.message.msgCont = "無法綁定LINE Pay。";
                    }
                    break;
                case "LBEB000613":
                    this.header.rspsRsltCd = "2";
                    this.message.msgCont = "您輸入的卡號錯誤。";
                    break;
                case "LBEA002019"://因密碼輸入錯誤，已終止您的簽帳金融卡
                    this.header.rspsRsltCd = "2";
                    this.message.msgCont = "因卡片驗證已達次數上限，為確保交易安全，將立即終止您的簽帳金融卡。";
                    break;
                case "LBEA002025"://Cust doesn't have shipping address
                    this.header.rspsRsltCd = "2";
                    this.message.msgCont = "查無寄送地址資料，請洽客服中心。";
                    break;
                case "PINCODE": //PIN CODE 驗證
                    this.header.rspsRsltCd = "2";
                    this.message.msgId = Error_Message.msgId;
                    this.message.msgCont = Error_Message.msgCont;
                    break;
                default:
                    this.header.rspsRsltCd = "1";
                    if (_msgId.Contains("LBEA") || _msgId.Contains("LBEB"))
                    {
                        this.message.msgId = _msgId;
                    }
                    else
                    {
                        this.message.msgId = "LBEA001958";
                    }

                    if (!string.IsNullOrEmpty(Error_Message.msgCont))
                    {
                        this.message.msgCont = Error_Message.msgCont;
                    }
                    else
                    {
                        this.message.msgCont = "很抱歉，因目前交易量大，造成系統壅塞，請您稍後再試。";
                    }
                    break;
            }
        }
    }
}
