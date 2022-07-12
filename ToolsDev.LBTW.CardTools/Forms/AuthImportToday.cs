using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToolsDev.Repository.Interface.CA;
using ToolsDev.Repository.Interface.CARD_INF;
using ToolsDev.Repository.Repo.CA.Cola_0200AuthProcess;
using ToolsDev.Repository.Repo.CA.ColaTxAuthToday;
using ToolsDev.Repository.Repo.CARD_INF;
using ToolsDev.Utilities.Helper;
using ToolsDev.Utilities.Library;
using ToolsDev.Utilities.Model.Library.EAI.FundBlock;

namespace ToolsDev.LBTW.CardTools.Forms
{
    public partial class AuthImportToday : Form
    {
        private IRepositoryCardInf repositoryCardInf;
        private IRepository_SP_Cola0200AuthProcess repository_SP_Cola0200AuthProcess;
        private IRepositoryColaTxAuthToday repositoryColaTxAuthToday;

        public AuthImportToday()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                int batchSize = 10;
                string env = "";
                if (radioButton_stg.Checked)
                {
                    env = "stg";
                }
                else if (radioButton_uat.Checked)
                {
                    env = "uat";
                }

                //declare interface
                repositoryCardInf = new RepositoryCardInf(env);
                repository_SP_Cola0200AuthProcess = new Repository_SP_Cola0200AuthProcess(env);
                repositoryColaTxAuthToday = new RepositoryColaTxAuthToday(env);

                for (int i = 0; i < 10; i++)
                {
                    var cards = repositoryCardInf.GetCardInfList(batchSize);
                    foreach (var card in cards)
                    {
                        var txKeySeq = repository_SP_Cola0200AuthProcess.ExecSP_Cola0200_AuthProcess(card.CARD_NBR, card.EXPIRE_DTE);
                        Console.Write(txKeySeq);

                        var model = new RequestFundBlockModel()
                        {
                            custId = card.ACCT_NBR,
                            autzTxGuid = txKeySeq,
                            acctNbr = card.LINK_BANK_NBR,
                            arrSrvcBlcknAmt = 1,
                            txDtm = DateTime.Now.ToString("yyyyMMddHHmmss"),
                            arrSrvcBlcknSeqNbr = 0,
                            rpmtBlcknAmt = 0
                        };
                        EAIService eaiService = new EAIService(env);
                        var result = eaiService.AuthFundBlockApi(model);
                        var descr = result.ResultObject.ToString();
                        var updateResult = repositoryColaTxAuthToday.UpdateDESCR(descr, txKeySeq);
                    }
                    tbOutput.Text = $"Import auth data {(i + 1) * batchSize} done.";
                }
            }
            catch (Exception ex)
            {
                tbOutput.Text = ex.ToJsonString();
            }
        }
    }
}
