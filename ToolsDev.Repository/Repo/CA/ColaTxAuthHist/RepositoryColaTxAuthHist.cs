using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using ToolsDev.Repositories.Dto.CA.ColaTxAuthHist;
using ToolsDev.Repository.Interface.CA;
using ToolsDev.Repository.Provider;
using ToolsDev.Utilities.Helper;

namespace ToolsDev.Repository.Repo.CA.ColaTxAuthHist
{
    public class RepositoryColaTxAuthHist : IRepositoryColaTxAuthHist
	{
		private readonly string _env;
		public RepositoryColaTxAuthHist(string env = "stg")
		{
			_env = env;
		}

		public List<string> GetColaTxAuthHistReceiveDte(int selectNum = 14)
		{
			try
			{
				DynamicParameters dynamicParameter = new DynamicParameters();
				dynamicParameter.Add("SELECTNUM", selectNum);

				string sql = $@"	SELECT DISTINCT TOP(@SELECTNUM) CONVERT(VARCHAR, RECEIVE_DTE, 111) AS RECEIVE_DTE
									FROM COLA_TX_AUTH_HIST WITH (NOLOCK)
									WHERE RECEIVE_DTE > DATEADD(DAY, -14, GETDATE())		--交易後圈存14天
									AND AMT > 0 AND RESP = 'A' AND SETTLE_FLAG=''
									ORDER BY 1 DESC
							";
				DbProvider dbProvider = new DbProvider(env: _env);
				return dbProvider.Connection.Query<string>(sql, dynamicParameter, commandTimeout: 120).ToList();
			}
			catch (Exception ex)
            {
				Console.Write($"{ex.ToJsonString()}");
				throw ex;
            }
		}

		public List<Rsp_GetColaTxAuthHistForNCCC_Dto> GetColaTxAuthHistForNCCC(Req_GetColaTxAuthHistForNCCC_Dto request)
		{
			try
			{
				DynamicParameters dynamicParameter = new DynamicParameters();
				dynamicParameter.Add("RECEIVE_DTE", request.RECEIVE_DTE);

				string sql = $@"	SELECT	CASE WHEN REVERSAL_FLAG='R' THEN 'REFUND' ELSE 'SALE' END AS TransactionCode
										, CARD_NBR AS AccountNumber
										, REPLACE(CONVERT(VARCHAR, RECEIVE_DTE, 10), '-', '') AS PurchaseDate
										, AMT AS DestinationAmount
										, ORIG_AMT AS SourceAmount
										, MER_NAME AS MerchantName
										, CITY AS MerchantCity
										, COUNTRY_CODE AS MerchantCountryCode
										, MCC_CODE AS MCC
										, ENTRY_MODE AS POSEntryMode
										, AUTH_CODE AS AuthorizationCode
										, MER_NAME AS MerchantChineseName
										, POS_NBR AS TerminalID
										, MER_NBR AS MerchantCode
									FROM COLA_TX_AUTH_HIST WITH (NOLOCK)
									WHERE CONVERT(VARCHAR, RECEIVE_DTE, 111) = @RECEIVE_DTE
									AND AMT > 0 AND RESP = 'A' AND SETTLE_FLAG=''
							";
				DbProvider dbProvider = new DbProvider(env: _env);
				return dbProvider.Connection.Query<Rsp_GetColaTxAuthHistForNCCC_Dto>(sql, dynamicParameter, commandTimeout: 120).ToList();
			}
			catch (Exception ex)
			{
				Console.Write($"{ex.ToJsonString()}");
				throw ex;
			}
		}
    }
}
