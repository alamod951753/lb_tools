using System;
using System.Text;
using System.Threading.Tasks;
using ToolsDev.Utilities.Helper;
using ToolsDev.Utilities.Model;
using ToolsDev.Utilities.Model.Library;

namespace ToolsDev.Utilities.Library
{
    /// <summary>
    /// Service for RCSP Credential
    /// </summary>
    public class RCSPCredentialService
    {
        /// <summary>
        /// RCSP Credential Get SensitiveData (password)
        /// </summary>
        /// <param name="ip">Domain or ip, except port 9002</param>
        /// <param name="uid">The account that AP server owner register on RKMS</param>
        /// <param name="hashDID">Value key held by each AP account owner (64 bytes, Base64)</param>
        /// <returns></returns>
        public Result GetSensitiveData(string ip, string uid, string hashDID)
        {
            var result = new Result();
            var random = new Random();
            var apiHelper = new ApiHelper();
            var postUrl = $@"https://{ip}:9002/RCSP_CREDENTIAL";

            try
            {
                if (string.IsNullOrEmpty(ip))
                {
                    result = new Result
                    {
                        Success = false,
                        ResultCode = "9991",
                        ResultMsg = $"Library.RCSPCredentialService.GetSensitiveData parameter (IP) error."
                    };
                    return result;
                }
                if (string.IsNullOrEmpty(uid))
                {
                    result = new Result
                    {
                        Success = false,
                        ResultCode = "9992",
                        ResultMsg = $"Library.RCSPCredentialService.GetSensitiveData parameter (UID) error."
                    };
                    return result;
                }
                if (string.IsNullOrEmpty(hashDID))
                {
                    result = new Result
                    {
                        Success = false,
                        ResultCode = "9993",
                        ResultMsg = $"Library.RCSPCredentialService.GetSensitiveData parameter (DID) error."
                    };
                    return result;
                }

                //step.0 decode DID
                var did = Encoding.UTF8.GetString(Convert.FromBase64String(hashDID));

                #region step.1 Post UID to get RDD
                var b = new byte[3];
                random.NextBytes(b);

                var request = new RequestRcspCredentialModel()
                {
                    UID = uid,
                    FuncNum = "0",
                    FuncData = b.ByteArrayToString().Replace("-", "")
                };
                var retryCount = 0;
                var postResult = false;
                var RDD_HexString = "";

                //if post error, retry at most 3 times
                while (retryCount < 3 && !postResult)
                {
                    var rsp = apiHelper.PostAsync(postUrl, request, skipCertValidationCheck: true);
                    if (rsp == null || rsp.Status != TaskStatus.RanToCompletion)
                    {
                        retryCount++;
                        result = new Result
                        {
                            Success = false,
                            ResultCode = "9994",
                            ResultMsg = $"Library.RCSPCredentialService.GetSensitiveData Post UID to get RDD api response {((rsp == null) ? "null." : $"error: {rsp?.Exception.ToJsonString()}")}"
                        };
                        continue;
                    }

                    var response = rsp.Result.ToJsonFormat<ResponseRcspCredentialModel>();
                    if (response == null || response?.RESULT.Trim().ToUpper() != "SUCCESS")
                    {
                        retryCount++;
                        result = new Result
                        {
                            Success = false,
                            ResultCode = "9995",
                            ResultMsg = $"Library.RCSPCredentialService.GetSensitiveData Post UID to get RDD response {((response == null) ? "null." : $"error: {response?.ToJsonString()}")}"
                        };
                        continue;
                    }

                    RDD_HexString = response.RESULT_DATA;
                    postResult = true;
                }

                if (!postResult)
                {
                    return result;
                }
                #endregion

                #region step.2 Post MAC to get Password
                var IV = new byte[16];
                random.NextBytes(IV);
                var IV_HexString = IV.ByteArrayToString().Replace("-", "");

                var aesHelper = new AESHelper(IV, did);
                var encryptRDD = aesHelper.Encrypt(RDD_HexString);
                var encryptRDD_HexString = encryptRDD.ByteArrayToString().Replace("-", "");

                request = new RequestRcspCredentialModel()
                {
                    UID = uid,
                    FuncNum = "1",
                    FuncData = $"{IV_HexString}:{encryptRDD_HexString}"
                };
                retryCount = 0;
                postResult = false;
                var resultData = "";

                //if post error, retry at most 3 times
                while (retryCount < 3 && !postResult)
                {
                    var rsp = apiHelper.PostAsync(postUrl, request, skipCertValidationCheck: true);
                    if (rsp == null || rsp.Status != TaskStatus.RanToCompletion)
                    {
                        retryCount++;
                        result = new Result
                        {
                            Success = false,
                            ResultCode = "9996",
                            ResultMsg = $"Library.RCSPCredentialService.GetSensitiveData Post MAC to get Password api response {((rsp == null) ? "null." : $"error: {rsp?.Exception.ToJsonString()}")}"
                        };
                        continue;
                    }

                    var response = rsp.Result.ToJsonFormat<ResponseRcspCredentialModel>();
                    if (response.RESULT.Trim().ToUpper() != "SUCCESS" || string.IsNullOrEmpty(response.RESULT_DATA))
                    {
                        retryCount++;
                        result = new Result
                        {
                            Success = false,
                            ResultCode = "9997",
                            ResultMsg = $"Library.RCSPCredentialService.GetSensitiveData Post MAC to get Password response {((response == null) ? "null." : $"error: {response?.ToJsonString()}")}"
                        };
                        continue;
                    }

                    resultData = response.RESULT_DATA;
                    postResult = true;
                }

                if (!postResult)
                {
                    return result;
                }
                #endregion

                //Decrypt resultData
                var resultDataArray = resultData.Split(':');
                var IV_RCSP = resultDataArray[0].StringToByteArray();
                aesHelper = new AESHelper(IV_RCSP, did);
                var decryptData = aesHelper.Decrypt(resultDataArray[1].StringToByteArray());

                result = new Result
                {
                    Success = true,
                    ResultCode = "0000",
                    ResultMsg = "GetSensitiveData Success!",
                    ResultObject = decryptData
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new Result
                {
                    Success = false,
                    ResultCode = "9999",
                    ResultMsg = $"Library.RCSPCredentialService.GetSensitiveData exception, {ex.Message}"
                };
                return result;
            }
        }
    }
}
