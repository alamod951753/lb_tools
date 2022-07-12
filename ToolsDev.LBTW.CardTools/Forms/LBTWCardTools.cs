using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ToolsDev.Repositories.Dto.CA.ColaTxAuthHist;
using ToolsDev.Repositories.Dto.Log;
using ToolsDev.Repository.Interface.CA;
using ToolsDev.Repository.Interface.CARD_INF;
using ToolsDev.Repository.Repo.CA.Cola_0200AuthProcess;
using ToolsDev.Repository.Repo.CA.ColaTxAuthHist;
using ToolsDev.Repository.Repo.CA.ColaTxAuthToday;
using ToolsDev.Repository.Repo.CARD_INF;
using ToolsDev.Repository.Repo.Log;
using ToolsDev.Utilities.Helper;
using ToolsDev.Utilities.Library;
using ToolsDev.Utilities.Model;
using ToolsDev.Utilities.Model.Library.EAI.FundBlock;
using static ToolsDev.LBTW.CardTools.Model.CreateStmtTest.StmtModel;

namespace ToolsDev.LBTW.CardTools.Forms
{
    public partial class LBTWCardTools : Form
    {
        private readonly IMapper _mapper;
        private IRepositoryColaTxAuthHist repositoryColaTxAuth;
        private IRepositoryLog repositoryLog;
        private IRepositoryCardInf repositoryCardInf;
        private IRepository_SP_Cola0200AuthProcess repository_SP_Cola0200AuthProcess;
        private IRepositoryColaTxAuthToday repositoryColaTxAuthToday;

        /// <summary>
        /// Date: 2021.12.07 Jerry Liu
        /// Description: LBTW Card Tools => tab1: NCCC 帳單產生器
        /// </summary>
        public LBTWCardTools()
        {
            InitializeComponent();
            InitializeBackgroundWorker();

            //entity mapper configuration
            var configuration = new MapperConfiguration(cfg => { cfg.CreateMap<Rsp_GetColaTxAuthHistForNCCC_Dto, StmtDataRecord01>(); });
            _mapper = configuration.CreateMapper();

            //declare interface
            repositoryColaTxAuth = new RepositoryColaTxAuthHist();
            repositoryLog = new RepositoryLog();

            //tab1
            InitializeTab1Controls();
        }

        /// <summary>
        /// Initialize
        /// </summary>
        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker1_RunWorkerCompleted);
        }

        #region Tab1 NCCC 帳單產生器
        /// <summary>
        /// Initialize controls in tab1.flowLayoutPanel1
        /// </summary>
        private void InitializeTab1Controls()
        {
            //put button to the last position
            flowLayoutPanel1.Controls.SetChildIndex(btnChooseDate, 99);
        }

        private void SetNoData(string errorMsg = "")
        {
            Label lblNoData = new Label
            {
                Name = "lblNoData",
                Text = $"\r\n\r\n[ No data ]",
                AutoSize = true,
                Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular)
            };
            flowLayoutPanel1.Controls.Add(lblNoData);
            flowLayoutPanel1.Controls.SetChildIndex(lblNoData, 0);

            btnChooseDate.Enabled = false;

            tbOutput.Text = errorMsg;
        }

        /// <summary>
        /// Browse excel (.xlsx, .xls file)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBrowseFile_Click(object sender, EventArgs e)
        {
            //parameter check
            var blnFlag = ParameterCheck_Tab1();
            if (!blnFlag)
            {
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Excel File",

                CheckFileExists = true,
                CheckPathExists= true,

                DefaultExt = "xlsx",
                Filter = "Excel Files|*.xls;*.xlsx;*.xlsm",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbOutput.Text = "";
                pictureBox1.Visible = true;

                string fullPath = Path.GetFullPath(openFileDialog.FileName);
                backgroundWorker1.RunWorkerAsync(new Dictionary<string, string>
                {
                    { "ExcelImport", fullPath }
                });
            }
        }

        /// <summary>
        /// clear control value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_Click(object sender, EventArgs e)
        {
            tbProcessDate.Text = "";
            tbClearDate.Text = "";
            tbOutput.Text = "";

            backgroundWorker1.CancelAsync();
            pictureBox1.Visible = false;

            RadioButton rbSelected = flowLayoutPanel1.Controls.OfType<RadioButton>()?.FirstOrDefault(r => r.Checked);
            if (rbSelected != null)
            {
                rbSelected.Checked = false;
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                List<Control> toBeRemoveds = new List<Control>();
                foreach (var item in flowLayoutPanel1.Controls)
                {
                    if (item is CheckBox)
                    {
                        toBeRemoveds.Add((CheckBox)item);
                    }
                    else if (item is Label)
                    {
                        toBeRemoveds.Add((Label)item);
                    }
                }

                foreach (var item in toBeRemoveds)
                {
                    flowLayoutPanel1.Controls.Remove(item);
                }

                //get auth hist receive date (within 14 days)
                try
                {
                    if (((RadioButton)sender).Text.ToLower().Contains("stg"))
                    {
                        repositoryColaTxAuth = new RepositoryColaTxAuthHist();
                    }
                    else if (((RadioButton)sender).Text.ToLower().Contains("uat"))
                    {
                        repositoryColaTxAuth = new RepositoryColaTxAuthHist("uat");
                    }
                    var rspDate = repositoryColaTxAuth.GetColaTxAuthHistReceiveDte();
                    if (rspDate == null || rspDate.Count == 0)
                    {
                        SetNoData();
                        return;
                    }

                    //add radioButtonGroup for auth hist receive date
                    CheckBox[] cbAuthHistDateList = new CheckBox[rspDate.Count];
                    for (int i = 0; i < rspDate.Count; i++)
                    {
                        var date = rspDate[i];
                        cbAuthHistDateList[i] = new CheckBox
                        {
                            Name = $"cbAuthHistDate{i}",
                            Text = date,
                            AutoSize = true,
                            Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular)
                        };
                        flowLayoutPanel1.Controls.Add(cbAuthHistDateList[i]);
                        flowLayoutPanel1.Controls.SetChildIndex(cbAuthHistDateList[i], i);

                        //7 items break flow, start to another line
                        if (flowLayoutPanel1.Controls.Count % 7 == 1)
                        {
                            if (i + 1 != rspDate.Count)
                            {
                                flowLayoutPanel1.SetFlowBreak(cbAuthHistDateList[i], true);
                            }
                        }
                    }
                    tbOutput.Text = "";
                    btnChooseDate.Enabled = true;
                }
                catch (Exception ex)
                {
                    SetNoData(ex.Message);
                }
            }
        }

        /// <summary>
        /// choose auth hist date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnChooseDate_Click(object sender, EventArgs e)
        {
            //parameter check
            var blnFlag = ParameterCheck_Tab1();
            if (!blnFlag)
            {
                return;
            }

            CheckBox cbSelected = flowLayoutPanel1.Controls.OfType<CheckBox>()?.FirstOrDefault(r => r.Checked);
            if (cbSelected == null)
            {
                tbOutput.Text = $"請選擇授權日期";
                return;
            }

            tbOutput.Text = "";
            pictureBox1.Visible = true;

            backgroundWorker1.RunWorkerAsync(new Dictionary<string, string>
            {
                { "AuthHistDate", cbSelected.Text }
            });
        }

        private bool ParameterCheck_Tab1()
        {
            bool checkControl = false;
            int number = 0;

            if (string.IsNullOrEmpty(tbProcessDate.Text.Trim()))
            {
                tbOutput.Text = $"請填處理日期";
                return false;
            }

            checkControl = int.TryParse(tbProcessDate.Text.Trim(), out number);
            if (tbProcessDate.Text.Trim().Length < 6 || !checkControl)
            {
                tbOutput.Text = $"處理日期格式錯誤";
                return false;
            }

            if (checkControl && int.Parse(tbProcessDate.Text.Trim()) <= 0)
            {
                tbOutput.Text = $"處理日期格式錯誤";
                return false;
            }

            if (string.IsNullOrEmpty(tbClearDate.Text.Trim()))
            {
                tbOutput.Text = $"請填清算日期";
                return false;
            }

            checkControl = int.TryParse(tbClearDate.Text.Trim(), out number);
            if (tbClearDate.Text.Trim().Length < 6 || !checkControl)
            {
                tbOutput.Text = $"清算日期格式錯誤";
                return false;
            }

            if (checkControl && int.Parse(tbClearDate.Text.Trim()) <= 0)
            {
                tbOutput.Text = $"清算日期格式錯誤";
                return false;
            }

            if (string.IsNullOrEmpty(tbSeq.Text.Trim()))
            {
                tbOutput.Text = $"請填檔案序號";
                return false;
            }

            checkControl = int.TryParse(tbSeq.Text.Trim(), out number);
            if (tbSeq.Text.Trim().Length < 2 || !checkControl)
            {
                tbOutput.Text = $"檔案序號格式錯誤";
                return false;
            }

            if (checkControl && int.Parse(tbSeq.Text.Trim()) <= 0)
            {
                tbOutput.Text = $"檔案序號格式錯誤";
                return false;
            }

            return true;
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            e.Result = ProcessData_Tab1((Dictionary<string, string>)e.Argument, worker, e);
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                tbOutput.Text = $"{e.Error.Message}";
            }
            else if (e.Cancelled)
            {
                tbOutput.Text = "Canceled";
            }
            else
            {
                var rsp = (Result)e.Result;
                if (rsp.Success)
                {
                    tbOutput.Text = $"{rsp?.ResultMsg}";
                }
                else
                {
                    tbOutput.Text = $"{rsp.ToJsonString()}";
                }
            }

            pictureBox1.Visible = false;
        }

        private Result ProcessData_Tab1(Dictionary<string, string> dic, BackgroundWorker worker, DoWorkEventArgs e)
        {
            Result result = new Result();

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return result;
            }

            if (dic == null || dic.Count == 0)
            {
                result = new Result
                {
                    Success = false,
                    ResultCode = "9990",
                    ResultMsg = "parameter error"
                };
                return result;
            }

            try
            {
                List<string> outputList = new List<string>();

                #region File Header
                var bankCode = "8242113";
                var exchangeRate = 30.1234;
                var seq = (tbSeq.Text.Trim().Length < 2) ? $"0{tbSeq.Text.Trim()}" : tbSeq.Text.Trim();
                string header = $"FH{bankCode}{tbProcessDate.Text.Trim()}{seq}T{exchangeRate.ToString().Replace(".", "")}{tbClearDate.Text.Trim()}Y{"",-239}";
                outputList.Add(header);
                #endregion

                #region File Data
                //Excel Data
                Error error = new Error();
                var recordList = new List<StmtDataRecord01>();
                switch (dic.FirstOrDefault().Key.ToLower())
                {
                    case "excelimport":
                        string filePath = dic.FirstOrDefault().Value;
                        recordList = ExcelHelper.ReadExcelToList<StmtDataRecord01>(filePath, out error);
                        break;
                    case "authhistdate":
                        var receiveDte = "";
                        foreach (var item in flowLayoutPanel1.Controls)
                        {
                            if (item is CheckBox)
                            {
                                if (((CheckBox)item).Checked)
                                {
                                    receiveDte = ((CheckBox)item).Text;

                                    var rspDatas = repositoryColaTxAuth.GetColaTxAuthHistForNCCC(new Req_GetColaTxAuthHistForNCCC_Dto() { RECEIVE_DTE = receiveDte });
                                    if (rspDatas == null || rspDatas.Count == 0)
                                    {
                                        error.ErrorCode = "9991";
                                        error.ErrorMsg = $"GetColaTxAuthHistForNCCC query null or none.";
                                        break;
                                    }

                                    var records = _mapper.Map<List<StmtDataRecord01>>(rspDatas);
                                    recordList.AddRange(records);
                                }
                            }
                        }
                        break;
                }

                if (error != null && !string.IsNullOrEmpty(error.ErrorMsg))
                {
                    result = new Result
                    {
                        Success = false,
                        ResultCode = error.ErrorCode,
                        ResultMsg = error.ErrorMsg
                    };
                    return result;
                }

                foreach (var data in recordList)
                {
                    var transactionCode = "";
                    switch (data.TransactionCode)
                    {
                        case "REFUND":
                            transactionCode = "06";
                            break;
                        default:
                        case "SALE":
                            transactionCode = "05";
                            break;
                    }

                    var accountNumber = data.AccountNumber.Trim();
                    var purchaseDate = data.PurchaseDate.Trim();
                    var desAmount = Math.Abs(data.DestinationAmount);
                    var sourceAmount = Math.Abs(data.SourceAmount);
                    var merchantName = data.MerchantName.Trim();
                    var merchantCity = data.MerchantCity.Trim();
                    var merchantCountryCode = data.MerchantCountryCode.Trim();
                    var mcc = data.MCC.Trim();
                    var posEntryMode = data.POSEntryMode.Trim();
                    var authorizationCode = data.AuthorizationCode.Trim();
                    var merchantChinese = data.MerchantChineseName.Trim();
                    var terminalId = data.TerminalID.Trim();
                    var merchantCode = data.MerchantCode.Trim();

                    //Record 1
                    var desAmountString = new string('0', 12) + desAmount.ToString("0.00").Replace(".", "");
                    var sourceAmountString = new string('0', 12) + sourceAmount.ToString("0.00").Replace(".", "");
                    string record1 = $"{transactionCode}01{accountNumber,-19}{new string('0', 23)}{new string('0', 11)}{"",-11}{purchaseDate}" +
                                    $"{desAmountString.Substring(desAmountString.Length - 12)}901" +
                                    $"{sourceAmountString.Substring(sourceAmountString.Length - 12)}901" +
                                    $"{merchantName,-25}{merchantCity,-13}{merchantCountryCode,-3}{mcc,-4}{posEntryMode,-3}{"",-3}{"",-1}1{authorizationCode,-6}" +
                                    $"5{"",-1}{"",-2}0{"",-1}{merchantChinese,-40}{terminalId,-15}{merchantCode,-20}F0000000{"",-13}1293V";
                    outputList.Add(record1);

                    //Record 2
                    string record2 = $"{transactionCode}020000 000000                                                                                   1  00123111111112222333344444444444444445555555555                66777777     00    000000000000       0000000000      0000000000000000005800                             V";
                    outputList.Add(record2);

                    //Record 3
                    string record3 = $"{transactionCode}03      000000000000000000000000000000000000000000000000000000000058211130901TW 0000                                                                000000000000                                                                                                           V";
                    outputList.Add(record3);

                    //Record 4
                    string record4 = $"{transactionCode}041234567890ABC                                             0000000000    00000000      0000000000000000ABCZZZZZZZZ       0000    000000000000                                                             000000000000000000000000                                        V";
                    outputList.Add(record4);
                }
                #endregion

                #region File Trailer
                var countTC05 = new string('0', 6) + recordList.Where(x => x.TransactionCode == "SALE").Count();
                var amountTC05 = new string('0', 15) + recordList.Where(x => x.TransactionCode == "SALE").Sum(y => Math.Abs(y.DestinationAmount)).ToString("0.00").Replace(".", "");
                var countTC06 = new string('0', 6) + recordList.Where(x => x.TransactionCode == "REFUND").Count();
                var amountTC06 = new string('0', 15) + recordList.Where(x => x.TransactionCode == "REFUND").Sum(y => Math.Abs(y.DestinationAmount)).ToString("0.00").Replace(".", "");

                //trailer 1
                string trailer1 = $"FT1{countTC05.Substring(countTC05.Length - 6)}{amountTC05.Substring(amountTC05.Length - 15)}" +
                                    $"{countTC06.Substring(countTC06.Length - 6)}{amountTC06.Substring(amountTC06.Length - 15)}" +
                                    $"{new string('0', 168)}{"",-57}";
                outputList.Add(trailer1);

                //trailer 2
                string trailer2 = $"FT2{new string('0', 192)}{"", -75}";
                outputList.Add(trailer2);
                #endregion

                #region Write to file
                var fileName = $"I001824{DateTime.Now.ToString("yyyyMMdd")}{seq}A";
                var directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).Replace("Documents", "Downloads");     //default to downloads folder
                string txtFilePath = $@"{directory}\{fileName}";
                StreamWriter writer = new StreamWriter(txtFilePath);
                foreach (var output in outputList)
                {
                    writer.Write(output + Environment.NewLine);
                }
                writer.Close();
                writer.Dispose();
                #endregion

                result.Success = true;
                result.ResultMsg = $"Processing completed!\r\nOutput file path: {txtFilePath}";
                return result;
            }
            catch (Exception ex)
            {
                result = new Result
                {
                    Success = false,
                    ResultCode = "9999",
                    ResultMsg = $"Processing exception, {ex.ToJsonString()}"
                };
                return result;
            }
        }
        #endregion

        /// <summary>
        /// Upload Log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBrowseLogFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    InitialDirectory = @"D:\",
                    Title = "Browse Log File",

                    CheckFileExists = true,
                    CheckPathExists = true,

                    DefaultExt = "txt",
                    Filter = "Text|*.txt|All|*.*",
                    FilterIndex = 2,
                    RestoreDirectory = true,

                    ReadOnlyChecked = true,
                    ShowReadOnly = true
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fullPath = Path.GetFullPath(openFileDialog.FileName);

                    var logs = File.ReadAllLines(fullPath, encoding: System.Text.Encoding.Default);

                    var logList = new List<Log_Dto>();
                    foreach (var log in logs)
                    {
                        try
                        {
                            string logTime = log.Substring(0, 23).Trim();
                            string logLevel = log.Substring(24, 7).Trim();
                            string logMsg = log.Substring(31).Trim();

                            logList.Add(new Log_Dto()
                            {
                                LogTime = Convert.ToDateTime(logTime),
                                Level = logLevel,
                                Message = logMsg
                            });
                        }
                        catch
                        {

                        }
                    }

                    var blnResult = repositoryLog.InsertList(logList);
                    if (blnResult)
                    {
                        label6.Text = $"Upload File Success! Data Count: {logList.Count()}";
                    }
                    else
                    {
                        label6.Text = $"Upload File Fail!";
                    }
                }
            }
            catch (Exception ex)
            {
                label6.Text = $"Upload File Fail! Exception: {ex.ToJsonString()}";
            }
        }

        /// <summary>
        /// Import Auth Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnImportAuth_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(tbBatchSize_tab3.Text, out int batchSize))
                {
                    tbOutput_Tab3.Text = $"Batch Size is not integer number.";
                    return;
                }
                string env = "";
                if (radioButton_stg.Checked)
                {
                    env = "stg";
                }
                else if (radioButton_uat.Checked)
                {
                    env = "uat";
                }
                else
                {
                    tbOutput_Tab3.Text = $"Please choose environment.";
                    return;
                }

                //declare interface
                repositoryCardInf = new RepositoryCardInf(env);
                repository_SP_Cola0200AuthProcess = new Repository_SP_Cola0200AuthProcess(env);
                repositoryColaTxAuthToday = new RepositoryColaTxAuthToday(env);

                int totalCnt = 0;
                int selectTop = 1000;
                while (totalCnt < batchSize)
                {
                    if (batchSize - totalCnt >= selectTop)
                    {
                        selectTop = 1000;
                    }
                    if (batchSize - totalCnt < selectTop)
                    {
                        selectTop = batchSize - totalCnt;
                    }
                    var cards = repositoryCardInf.GetCardInfList(selectTop);
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

                    totalCnt += selectTop;
                    tbOutput_Tab3.Text = $"Import auth data {totalCnt} done.";
                }
            }
            catch (Exception ex)
            {
                tbOutput_Tab3.Text = ex.ToJsonString();
            }
        }
    }
}
