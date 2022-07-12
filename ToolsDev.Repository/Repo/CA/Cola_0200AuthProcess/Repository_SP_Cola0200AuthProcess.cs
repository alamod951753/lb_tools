using Dapper;
using System;
using System.Linq;
using ToolsDev.Repository.Interface.CA;
using ToolsDev.Repository.Provider;
using ToolsDev.Utilities.Helper;

namespace ToolsDev.Repository.Repo.CA.Cola_0200AuthProcess
{
    public class Repository_SP_Cola0200AuthProcess : IRepository_SP_Cola0200AuthProcess
	{
		private readonly string _env;
		public Repository_SP_Cola0200AuthProcess(string env = "stg")
		{
			_env = env;
		}

		public string ExecSP_Cola0200_AuthProcess(string cardNbr, string expireDte)
		{
			try
			{
				DynamicParameters dynamicParameter = new DynamicParameters();
				dynamicParameter.Add("CardNBR", cardNbr);
				dynamicParameter.Add("ExpireDte", expireDte);

				string sql = $@"	DECLARE @p18 VARCHAR(1000)
									DECLARE @p19 VARCHAR(1000)
									DECLARE @p41 VARCHAR(1000)
									DECLARE @p42 VARCHAR(1000)
									DECLARE @p53 VARCHAR(1000)
									DECLARE @p57 VARCHAR(1000)
									DECLARE @p61 VARCHAR(1000)
									DECLARE @p62 VARCHAR(1000)
									DECLARE @p65 VARCHAR(1000)
									DECLARE @p66 VARCHAR(1000)
									DECLARE @p67 VARCHAR(1000)
									DECLARE @p68 VARCHAR(1000)
									DECLARE @p69 VARCHAR(1000)
									DECLARE @p70 VARCHAR(1000)
									DECLARE @p71 VARCHAR(1000)
									DECLARE @p72 VARCHAR(1000)
									DECLARE @p73 VARCHAR(1000)
									DECLARE @p74 VARCHAR(1000)
									DECLARE @p75 VARCHAR(1000)
									DECLARE @p78 VARCHAR(1000)
									DECLARE @p79 VARCHAR(1000)
									DECLARE @p80 VARCHAR(1000)
									DECLARE @p81 VARCHAR(1000)
									DECLARE @p82 VARCHAR(1000)
									DECLARE @p83 VARCHAR(1000)
									DECLARE @p84 VARCHAR(1000)
									DECLARE @p85 VARCHAR(1000)
									DECLARE @p86 VARCHAR(1000)
									DECLARE @p87 VARCHAR(1000)
									DECLARE @p88 VARCHAR(1000)

									DECLARE @TX_YMD VARCHAR(10) = CONVERT(VARCHAR, GETDATE(), 112)
									DECLARE @TX_DATE VARCHAR(10) = SUBSTRING(@TX_YMD, 5, 4)
									DECLARE @TX_TIME VARCHAR(10) = REPLACE(CONVERT(VARCHAR, GETDATE(), 108), ':', '')
									DECLARE @GUID VARCHAR(100) = REPLACE(NEWID(), '-', '')
									DECLARE @RAND_NBR VARCHAR(100) = REPLACE(CAST(RAND()*1000000 AS VARCHAR), '.', '')
									DECLARE @KEY_SEQ VARCHAR(100) = (@TX_YMD+@TX_TIME+SUBSTRING(@GUID, 0, 8))
									DECLARE @REF_NBR VARCHAR(100) = @TX_TIME +@RAND_NBR
									--SELECT @TX_YMD, @TX_DATE, @TX_TIME, @GUID, @RAND_NBR, @KEY_SEQ, @REF_NBR
									SET @p18=@RAND_NBR

									exec dbo.Cola_0200AuthProcess 
									@txKeySeq=@KEY_SEQ,
									@isoMessageType='0200',
									@isoP2CardNbr=@CardNBR,
									@isoP3ProcessCde='000030',
									@isoP4Amt=1.00,
									@isoP7TxDte=@TX_DATE,
									@isoP7TxTime=@TX_TIME,
									@isoP11TraceNo=@RAND_NBR,
									@isoP18MccCode='1256',
									@isoP22Em='012',
									@isoP25CondCode='59',
									@isoP32BinIca='484094',
									@isoP35ExpirDte=@ExpireDte,
									@isoP35Svc=NULL,
									@isoP35Pvv=NULL,
									@isoP35Pvki=NULL,
									@isoP37RefNbr=@REF_NBR,
									@isoP38AuthCode=@p18 output,
									@isoP39Rsp=@p19 output,
									@isop41TermNbr='A2000903        ',
									@isoP42Merchant='6200803695     ',
									@isoP43MerName='NCCC TEST NAME        ',
									@isoP43MerCity='TAINAN CITY  ',
									@isoP43LocCountry='TW',
									@isoP52PinBlock=NULL,
									@isoS95ReplaceAmt=0.00,
									@isoS127Pws=NULL,
									@isoS127EdcFunction='11',
									@isoS127MpFlag=NULL,
									@isoS127Id=NULL,
									@arqcCheck='N',
									@arqcChkResult='N',
									@cvvCheck='N',
									@cvvChkResult='N',
									@cvv2Check='N',
									@cvv2ChkResult='N',
									@cavv2Check='N',
									@cavv2ChkResult='N',
									@pvvCheck='N',
									@pvvChkResult=NULL,
									@t04ErrorFlg=@p41 output,
									@t04CrdVeryFlg=@p42 output,
									@tB2TVR=NULL,
									@tB2ATC=NULL,
									@tB2CVR=NULL,
									@tB2AQRC=NULL,
									@tB2UN=NULL,
									@tB2DerivKeyIndex=NULL,
									@tB2CryptoVerNum=NULL,
									@tB4PtSrvEm=NULL,
									@tB4TermEntryCap=NULL,
									@tB4LastEmvStat=NULL,
									@tB5ARPC=@p53 output,
									@tB4ROC=NULL,
									@tC0EcFlag='7',
									@tC0UcafAuthInd='0',
									@tC0CavvResult=@p57 output,
									@tC4TermAttendInd=NULL,
									@tC4ChActVtInd=NULL,
									@tC4TermInputCap=NULL,
									@tC5InstTxInd=@p61 output,
									@tC5InstTxResp=@p62 output,
									@tC5SettlementFlag=NULL,
									@tC5InstPeriod=NULL,
									@tC5DownPayment=@p65 output,
									@tC5InstPayment=@p66 output,
									@tC5InstFee=@p67 output,
									@tC5PointRedemption=@p68 output,
									@tC5SignPoint=@p69 output,
									@tC5PointBalance=@p70 output,
									@tC5PaidCreditAmt=@p71 output,
									@tC5LoyaltyTxInd=@p72 output,
									@tB2CVRMchipAdvance=@p73 output,
									@tB5MchipAdvanceResp=@p74 output,
									@isoP38OutPutInd=@p75 output,
									@isoCheckCellPhone='          ',
									@isoCheckDob='        ',
									@retpayAcctNo=@p78 output,
									@retfundBlockTXNNo=@p79 output,
									@retmonetaryflag=@p80 output,
									@retprocP4Amt=@p81 output,
									@retprocS95mt=@p82 output,
									@retamt=@p83 output,
									@retdatetime=@p84 output,
									@retresp=@p85 output,
									@retacctNbr=@p86 output,
									@retrspInner=@p87 output,
									@retreason=@p88 output

									SELECT @KEY_SEQ
							";
				using (DbProvider dbProvider = new DbProvider(env: _env))
				{
					var result = dbProvider.Connection.Query<string>(sql, dynamicParameter, commandTimeout: 120).FirstOrDefault();
					return result;      //output txKeySeq
				}
			}
			catch (Exception ex)
			{
				Console.Write($"{ex.ToJsonString()}");
				throw ex;
			}
		}
    }
}
