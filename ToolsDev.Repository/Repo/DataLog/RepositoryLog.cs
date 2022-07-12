using Dapper;
using System;
using System.Collections.Generic;
using ToolsDev.Repositories.Dto.Log;
using ToolsDev.Repository.Extensions;
using ToolsDev.Repository.Interface.CA;
using ToolsDev.Repository.Provider;
using ToolsDev.Utilities.Helper;

namespace ToolsDev.Repository.Repo.Log
{
    public class RepositoryLog : IRepositoryLog
	{
		public bool InsertList(List<Log_Dto> logs)
		{
			try
			{
				DynamicParameters dynamicParameter = new DynamicParameters();
				dynamicParameter.Add("@log", logs.AsTableValuedParameter("dbo.LogData", new[] { "LogTime", "Level", "Message" }));

				DbProvider dbProvider = new DbProvider("CARDHistConnectionString");
				var result = dbProvider.Connection.Query("LogDataAdd", dynamicParameter, commandType: System.Data.CommandType.StoredProcedure);

				return true;
			}
			catch (Exception ex)
            {
				Console.Write($"{ex.ToJsonString()}");
				return false;
            }
		}
    }
}
