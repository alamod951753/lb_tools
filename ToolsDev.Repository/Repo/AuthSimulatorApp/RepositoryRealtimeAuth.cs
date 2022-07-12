using Dapper;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using ToolsDev.Repository.Dto.AuthSimulatorApp;
using ToolsDev.Repository.Extensions;
using ToolsDev.Repository.Interface.AuthSimulatorApp;
using ToolsDev.Repository.Provider;
using ToolsDev.Utilities.Helper;

namespace ToolsDev.Repository.Repo.AuthSimulatorApp
{
    public class RepositoryRealtimeAuth : IRepositoryRealtimeAuth
	{
		private Logger logger = LogManager.GetCurrentClassLogger();

		public int InsertApi(RealtimeAuthApi_Dto api)
		{
			try
			{
				string sql = $@"	INSERT INTO REALTIME_AUTH_API ( BATCH_SEQ, MERCHANT_ID, SUBMERCHANT_ID, TERMINAL_ID, ORDER_ID, TRANS_CODE, 
																	CARD_NBR, EXPIRE_DATE, CVV2, TRANS_AMT, TRANS_MODE, INSTALL_COUNT, CREATE_USER, REQUEST_DATE, 
																	MESSAGE_TYPE, REVERSAL_FLAG, APPROVE_CODE, RESPONSE_CODE, RESPONSE_MSG, BATCH_NO, RRN, 
																	TRANS_TYPE, INSTALL_TYPE, FIRST_AMT, EACH_AMT, FEE, REDEEM_TYPE, REDEEM_USED, REDEEM_BALANCE, 
																	CREDIT_AMT, RESPONSE_DATE, USER_DEFINE )
									OUTPUT INSERTED.SEQ
									VALUES( @BATCH_SEQ, @MERCHANT_ID, @SUBMERCHANT_ID, @TERMINAL_ID, @ORDER_ID, @TRANS_CODE, @CARD_NBR, @EXPIRE_DATE, @CVV2
											, @TRANS_AMT, @TRANS_MODE, @INSTALL_COUNT, @CREATE_USER, @REQUEST_DATE
											, @MESSAGE_TYPE, @REVERSAL_FLAG,@APPROVE_CODE, @RESPONSE_CODE, @RESPONSE_MSG, @BATCH_NO, @RRN 
											, @TRANS_TYPE, @INSTALL_TYPE, @FIRST_AMT, @EACH_AMT, @FEE, @REDEEM_TYPE, @REDEEM_USED, @REDEEM_BALANCE, @CREDIT_AMT
											, @RESPONSE_DATE, @USER_DEFINE )
							";

				DbProvider dbProvider = new DbProvider("CARDToolConnectionString");
				var result = dbProvider.Connection.ExecuteScalar<int>(sql, param: api);

				return result;
			}
			catch (Exception ex)
			{
				logger.Error($"InsertApi Exception: {ex.ToJsonString()}, request: {api.ToJsonString()}");
				return -1;
			}
		}

		public bool InsertApiList(List<RealtimeAuthApi_Dto> apis)
		{
			try
			{
				DynamicParameters dynamicParameter = new DynamicParameters();
				dynamicParameter.Add("@api", apis.AsTableValuedParameter("dbo.REALTIME_AUTH_API", new[] { "BATCH_SEQ", "MERCHANT_ID", "SUBMERCHANT_ID", "TERMINAL_ID", "ORDER_ID", "TRANS_CODE", "CARD_NBR", "EXPIRE_DATE", "CVV2", "TRANS_AMT", "TRANS_MODE", "INSTALL_COUNT", "CREATE_USER", "REQUEST_DATE", "MESSAGE_TYPE", "REVERSAL_FLAG", "APPROVE_CODE", "RESPONSE_CODE", "RESPONSE_MSG", "BATCH_NO", "RRN", "TRANS_TYPE", "INSTALL_TYPE", "FIRST_AMT", "EACH_AMT", "FEE", "REDEEM_TYPE", "REDEEM_USED", "REDEEM_BALANCE", "CREDIT_AMT", "RESPONSE_DATE", "USER_DEFINE" }));

				DbProvider dbProvider = new DbProvider("CARDToolConnectionString");
				var result = dbProvider.Connection.Query("REALTIME_AUTH_API_Add", dynamicParameter, commandType: System.Data.CommandType.StoredProcedure);

				return true;
			}
			catch (Exception ex)
			{
				logger.Error($"InsertApiList Exception: {ex.ToJsonString()}, request: {apis.ToJsonString()}");
				return false;
			}
		}

		public int InsertBatch(RealtimeAuthBatch_Dto batch)
		{
			try
			{
				string sql = $@"	INSERT INTO REALTIME_AUTH_BATCH ( FILE_PATH, UPLOAD_DATE, UPLOAD_USER, DATA_CNT, PROCESS_START_DATE, PROCESS_END_DATE )
									OUTPUT INSERTED.SEQ
									VALUES( @FILE_PATH, @UPLOAD_DATE, @UPLOAD_USER, @DATA_CNT, @PROCESS_START_DATE, @PROCESS_END_DATE )
							";

				DbProvider dbProvider = new DbProvider("CARDToolConnectionString");
				var result = dbProvider.Connection.ExecuteScalar<int>(sql, param: batch);

				return result;
			}
			catch (Exception ex)
			{
				logger.Error($"InsertBatch Exception: {ex.ToJsonString()}, request: {batch.ToJsonString()}");
				return -1;
			}
		}

		public List<RealtimeAuthApi_Dto> GetApiList(RequestGetApiList_Dto request)
        {
            try
            {
				string sqlCondition = "";
                DynamicParameters dynamicParameter = new DynamicParameters();
				if (!string.IsNullOrEmpty(request.RequestDate))
                {
					dynamicParameter.Add("RequestDate", request.RequestDate);
					sqlCondition += " AND CONVERT(VARCHAR, REQUEST_DATE, 111) = @RequestDate ";
				}
				if (!string.IsNullOrEmpty(request.TerminalID))
				{
					dynamicParameter.Add("TerminalID", request.TerminalID);
					sqlCondition += " AND TERMINAL_ID = @TerminalID ";
				}
				if (!string.IsNullOrEmpty(request.CreateUser))
				{
					dynamicParameter.Add("CreateUser", request.CreateUser);
					sqlCondition += " AND CREATE_USER = @CreateUser ";
				}
				if (!string.IsNullOrEmpty(request.ApproveCode))
				{
					dynamicParameter.Add("ApproveCode", request.ApproveCode);
					sqlCondition += " AND APPROVE_CODE = @ApproveCode ";
				}
				if (!string.IsNullOrEmpty(request.CardNbr))
				{
					dynamicParameter.Add("CardNbr", request.CardNbr);
					sqlCondition += " AND CARD_NBR = @CardNbr ";
				}
				if (request.TransAmt != null && Math.Abs((decimal)request.TransAmt) > 0)
				{
					dynamicParameter.Add("TransAmt", request.TransAmt);
					sqlCondition += " AND TRANS_AMT = @TransAmt ";
				}
				if (request.TransMode != null && request.TransMode > -1)
				{
					dynamicParameter.Add("TransMode", request.TransMode);
					sqlCondition += " AND TRANS_MODE = @TransMode ";
				}
				if (!string.IsNullOrEmpty(request.TransCode) && request.TransCode != "-1")
				{
					dynamicParameter.Add("TransCode", request.TransCode);
					sqlCondition += " AND TRANS_CODE = @TransCode ";
				}

				string sql = $@"	SELECT	SEQ, MERCHANT_ID, SUBMERCHANT_ID, TERMINAL_ID, ORDER_ID, TRANS_CODE, CARD_NBR, TRANS_AMT
											, TRANS_MODE, INSTALL_COUNT, CREATE_USER, REQUEST_DATE
											, APPROVE_CODE, RESPONSE_DATE, A.RESPONSE_CODE
											, CASE WHEN ISNULL(B.RESPONSE_MESSAGE, '')='' THEN A.RESPONSE_MSG ELSE B.RESPONSE_MESSAGE END AS RESPONSE_MSG
											, BATCH_NO, RRN, TRANS_TYPE
											, INSTALL_TYPE, FIRST_AMT, EACH_AMT, FEE
											, REDEEM_TYPE, REDEEM_USED, REDEEM_BALANCE, CREDIT_AMT, USER_DEFINE
									FROM REALTIME_AUTH_API A WITH (NOLOCK)
									LEFT JOIN TFB_AUTH_RESPONSE_CODE_LIST B
										ON A.RESPONSE_CODE=B.RESPONSE_CODE
									WHERE ISNULL(BATCH_SEQ, '')=''		--QUERY SINGLE
									{sqlCondition}
							";
                using (DbProvider dbProvider = new DbProvider("CARDToolConnectionString"))
                {
                    return dbProvider.Connection.Query<RealtimeAuthApi_Dto>(sql, dynamicParameter).ToList();
                }
            }
            catch (Exception ex)
            {
				logger.Error($"GetApiList Exception: {ex.ToJsonString()}, request: {request.ToJsonString()}");
                throw ex;
            }
		}

		public List<ResponseGetBatchList_Dto> GetBatchList(RequestGetBatchList_Dto request)
		{
			try
			{
				string sqlCondition = "";
				DynamicParameters dynamicParameter = new DynamicParameters();
				if (!string.IsNullOrEmpty(request.TerminalID))
				{
					dynamicParameter.Add("TerminalID", request.TerminalID);
					sqlCondition += " AND TERMINAL_ID = @TerminalID ";
				}
				if (!string.IsNullOrEmpty(request.UploadDate))
				{
					dynamicParameter.Add("UploadDate", request.UploadDate);
					sqlCondition += " AND CONVERT(VARCHAR, UPLOAD_DATE, 111) = @UploadDate ";
				}
				if (!string.IsNullOrEmpty(request.UploadUser))
				{
					dynamicParameter.Add("UploadUser", request.UploadUser);
					sqlCondition += " AND UPLOAD_USER = @UploadUser ";
				}

				string sql = $@"SELECT	BATCH_SEQ, FILE_PATH, UPLOAD_DATE, UPLOAD_USER, DATA_CNT, PROCESS_START_DATE, PROCESS_END_DATE, PROCESS_MEMO
										, SUM(PROCESS_DETAIL) AS PROCESS_CNT
										, COUNT(CASE WHEN PROCESS_DETAIL = 1 AND APPROVE_RESULT=1 THEN 1 END) AS SUCCESS_CNT
										, COUNT(CASE WHEN PROCESS_DETAIL = 1 AND APPROVE_RESULT=0 THEN 1 END) AS FAILED_CNT
								FROM (	SELECT	B.BATCH_SEQ, A.FILE_PATH, A.UPLOAD_DATE, A.UPLOAD_USER, A.DATA_CNT
												, A.PROCESS_START_DATE, A.PROCESS_END_DATE, A.PROCESS_MEMO
											, CASE WHEN B.SEQ IS NOT NULL THEN 1 ELSE 0 END AS PROCESS_DETAIL		--Process or not
											, CASE WHEN B.RESPONSE_CODE='000' AND B.RESPONSE_MSG LIKE '%APPROVED%' AND ISNULL(B.APPROVE_CODE, '') <> ''	--approve or not
												THEN 1 ELSE 0 END AS APPROVE_RESULT
										FROM REALTIME_AUTH_BATCH A WITH (NOLOCK)
										LEFT JOIN (
											SELECT SEQ, BATCH_SEQ, APPROVE_CODE, RESPONSE_CODE, RESPONSE_MSG
											FROM REALTIME_AUTH_API WITH (NOLOCK) 
											WHERE ISNULL(BATCH_SEQ, '')<>''		--QUERY BATCH
										) B
										ON A.SEQ=B.BATCH_SEQ
										WHERE 1=1
										{sqlCondition}
								) R
								GROUP BY BATCH_SEQ, FILE_PATH, UPLOAD_DATE, UPLOAD_USER, DATA_CNT, PROCESS_START_DATE, PROCESS_END_DATE, PROCESS_MEMO
							";
				using (DbProvider dbProvider = new DbProvider("CARDToolConnectionString"))
				{
					return dbProvider.Connection.Query<ResponseGetBatchList_Dto>(sql, dynamicParameter).ToList();
				}
			}
			catch (Exception ex)
			{
				logger.Error($"GetBatchList Exception: {ex.ToJsonString()}, request: {request.ToJsonString()}");
				throw ex;
			}
		}

		public List<RealtimeAuthApi_Dto> GetBatchFailDetails(int batchSeq)
		{
			try
			{
				string sql = $@"IF (OBJECT_ID(N'TEMPDB..#TEMP_BATCH_SEQ', N'U') IS NOT NULL)
									DROP TABLE #TEMP_BATCH_SEQ
								SELECT	DISTINCT A.ORDER_ID, A.RESPONSE_CODE
										, CASE WHEN ISNULL(B.RESPONSE_MESSAGE, '')='' THEN A.RESPONSE_MSG ELSE B.RESPONSE_MESSAGE END AS RESPONSE_MSG
								INTO #TEMP_BATCH_SEQ
								FROM REALTIME_AUTH_API A
								LEFT JOIN TFB_AUTH_RESPONSE_CODE_LIST B
								ON A.RESPONSE_CODE=B.RESPONSE_CODE
								WHERE BATCH_SEQ=@BatchSeq
								AND A.RESPONSE_CODE<>'000'

								SELECT	D.RESPONSE_CODE, D.RESPONSE_MSG, (
										SELECT ORDER_ID + ', '
										FROM #TEMP_BATCH_SEQ A
										WHERE A.RESPONSE_CODE=D.RESPONSE_CODE
										FOR XML PATH('')
								) AS ORDER_ID
								FROM #TEMP_BATCH_SEQ D
								GROUP BY D.RESPONSE_CODE, D.RESPONSE_MSG 
							";
				using (DbProvider dbProvider = new DbProvider("CARDToolConnectionString"))
				{
					return dbProvider.Connection.Query<RealtimeAuthApi_Dto>(sql, new { BatchSeq = batchSeq }).ToList();
				}
			}
			catch (Exception ex)
			{
				logger.Error($"GetBatchFailDetail Exception: {ex.ToJsonString()}, request batchSeq: {batchSeq}");
				throw ex;
			}
		}

		public List<RealtimeAuthBatch_Dto> GetBatchToBeProcessedList()
		{
			try
			{
				string sql = $@"SELECT SEQ, FILE_PATH, DATA_CNT, PROCESS_START_DATE, PROCESS_END_DATE
								FROM REALTIME_AUTH_BATCH
								WHERE PROCESS_START_DATE IS NULL
								ORDER BY 1 
							";
				using (DbProvider dbProvider = new DbProvider("CARDToolConnectionString"))
				{
					return dbProvider.Connection.Query<RealtimeAuthBatch_Dto>(sql).ToList();
				}
			}
			catch (Exception ex)
			{
				logger.Error($"GetBatchToBeProcessedList Exception: {ex.ToJsonString()}");
				throw ex;
			}
		}

		public int UpdateBatchProcessingStart(RequestUpdateBatchProcessingStart_Dto request)
        {
			try
			{
				string sql = $@"	UPDATE A
									SET PROCESS_START_DATE=GETDATE()
									FROM REALTIME_AUTH_BATCH A
									WHERE SEQ IN @SeqList
							";

				DbProvider dbProvider = new DbProvider("CARDToolConnectionString");
				var result = dbProvider.Connection.ExecuteScalar<int>(sql, new { SeqList = request.SeqList });

				return result;
			}
			catch (Exception ex)
			{
				logger.Error($"UpdateBatchProcessingStart Exception: {ex.ToJsonString()}, reqeust: {request.ToJsonString()}");
				return -1;
			}
		}

		public int UpdateBatchProcessEnd(RequestUpdateBatchProcessEnd_Dto request)
		{
			try
			{
				string sqlCondition = "";
				DynamicParameters dynamicParameter = new DynamicParameters();
				dynamicParameter.Add("Seq", request.Seq);

				if (!string.IsNullOrEmpty(request.Memo))
				{
					dynamicParameter.Add("Memo", request.Memo);
					sqlCondition += ", PROCESS_MEMO=@Memo ";
				}
				if (request.DataCount != null)
				{
					dynamicParameter.Add("DataCount", request.DataCount);
					sqlCondition += " , DATA_CNT=@DataCount ";
				}

				string sql = $@"	UPDATE A
									SET PROCESS_END_DATE=GETDATE() {sqlCondition}
									FROM REALTIME_AUTH_BATCH A
									WHERE SEQ = @Seq
							";

				DbProvider dbProvider = new DbProvider("CARDToolConnectionString");
				var result = dbProvider.Connection.ExecuteScalar<int>(sql, dynamicParameter);

				return result;
			}
			catch (Exception ex)
			{
				logger.Error($"UpdateBatchProcessEnd Exception: {ex.ToJsonString()}, reqeust: {request.ToJsonString()}");
				return -1;
			}
		}
	}
}
