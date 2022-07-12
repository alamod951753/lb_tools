using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using ToolsDev.Repositories.Dto.CARD_INF;
using ToolsDev.Repository.Interface.CARD_INF;
using ToolsDev.Repository.Provider;
using ToolsDev.Utilities.Helper;

namespace ToolsDev.Repository.Repo.CARD_INF
{
    public class RepositoryCardInf : IRepositoryCardInf
	{
		private readonly string _env;
		public RepositoryCardInf(string env = "stg")
		{
			_env = env;
		}

		public List<CardInf_Dto> GetCardInfList(int selectTop = 1000)
		{
			try
			{
				DynamicParameters dynamicParameter = new DynamicParameters();
				dynamicParameter.Add("SelectTop", selectTop);

				string sql = $@"	SELECT	TOP(@SelectTop) a.ACCT_NBR, a.CARD_NBR, a.LINK_BANK_NBR
											, SUBSTRING(CONVERT(VARCHAR(08), a.EXPIR_DTE, 112), 3, 4) AS EXPIRE_DTE
											, SUBSTRING(CONVERT(VARCHAR(08), a.EXPIR_DTE_LAST, 112), 3, 4) AS EXPIRE_DTE_LAST
									FROM CARD_INF a WITH (NOLOCK)
									INNER JOIN CARD_LIMIT b ON a.CARD_NBR=b.CARD_NBR 
										AND a.BU=b.BU 
										AND a.ACCT_NBR=b.ACCT_NBR 
										AND a.PRODUCT=b.PRODUCT 
										AND a.CURRENCY=b.CURRENCY
									INNER JOIN CUST_INF c ON a.CARDHOLDER_NBR = c.CUST_NBR  
										AND a.BU = c.BU
									INNER JOIN SETUP_BU d ON a.BU = d.BU
									WHERE A.ACCT_NBR NOT IN ( SELECT DISTINCT ACCT_NBR FROM COLA_TX_AUTH_TODAY WHERE RESP='D' )
							";
				using (DbProvider dbProvider = new DbProvider(env: _env))
				{
					return dbProvider.Connection.Query<CardInf_Dto>(sql, dynamicParameter).ToList();
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
