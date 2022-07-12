using Dapper;
using System;
using ToolsDev.Repository.Interface.CA;
using ToolsDev.Repository.Provider;
using ToolsDev.Utilities.Helper;

namespace ToolsDev.Repository.Repo.CA.ColaTxAuthToday
{
    public class RepositoryColaTxAuthToday : IRepositoryColaTxAuthToday
	{
		private readonly string _env;
		public RepositoryColaTxAuthToday(string env = "stg")
		{
			_env = env;
		}

		public bool UpdateDESCR(string descr, string seq)
		{
			try
			{
				string sql = "";
				DynamicParameters dynamicParameter = new DynamicParameters();
				dynamicParameter.Add("DESCR", descr);
				dynamicParameter.Add("SEQ", seq);

				if (!string.IsNullOrEmpty(descr))
				{
					sql = $@"	UPDATE COLA_TX_AUTH_TODAY
									SET DESCR=@DESCR
									WHERE SEQ=@SEQ
							";
				}
				else
                {
					sql = $@"	UPDATE COLA_TX_AUTH_TODAY
									SET RESP='D'
									WHERE SEQ=@SEQ
							";
				}
				using (DbProvider dbProvider = new DbProvider(env: _env))
				{
					var updateCnt = dbProvider.Connection.Execute(sql, dynamicParameter, commandTimeout: 120);
				}
				return true;
			}
			catch (Exception ex)
            {
				Console.Write($"{ex.ToJsonString()}");
				throw ex;
            }
		}
    }
}
