using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolsDev.Utilities.Helper;

namespace ToolsDev.Service.Service
{
    public static class TokenService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static bool IsValidToken(string token)
        {
            try
            {
                string iv = UtilityHelper.GetAppSetting("DES_IV");
                string key = UtilityHelper.GetAppSetting("DES_Key");
                DESHelper desHelper = new DESHelper(iv, key);

                var tokenDecrypt = desHelper.DecryptDES(token);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error($"IsValidToken Exception: {ex.ToJsonString()}");
                new Exception($"Unauthorized. {ex.Message}");
            }

            return false;
        }
    }
}
