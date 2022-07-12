using System;
using System.Data;
using System.Data.SqlClient;
using ToolsDev.Utilities.Helper;

namespace ToolsDev.Repository.Provider
{
    public class DbProvider : IDisposable
    {
        private string _connectionString;
        private IDbConnection _idbConnection;

        public IDbConnection Connection
        {
            get { return _idbConnection; }
            set { _idbConnection = value; }
        }

        public DbProvider()
        {
            //default CardConnectionString
            _connectionString = UtilityHelper.GetConnectionString("CARDConnectionString");
            Open();
        }

        public DbProvider(string connectionString = "", string env = "stg")
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = "CARDConnectionString";
            }

            switch (env.ToLower())
            {
                case "uat":
                    connectionString = $"{connectionString}_UAT";
                    break;
            }
            _connectionString = UtilityHelper.GetConnectionString(connectionString);
            Open();
        }

        public void Open()
        {
            try
            {
                Connection = new SqlConnection(_connectionString);
                if (Connection.State != ConnectionState.Open)
                {
                    Connection.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Close()
        {
            if (Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Close();
            Connection = null;
        }
    }
}
