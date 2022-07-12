using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ToolsDev.Utilities.Helper
{
    /// <summary>
    /// DES Encrypt/Decrypt
    /// </summary>
    public class DESHelper
    {
        private byte[] _iv;
        private byte[] _key;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IV"></param>
        /// <param name="KEY"></param>
        public DESHelper(string IV, string KEY)
        {
            _iv = Convert.FromBase64String(IV);
            _key = Convert.FromBase64String(KEY);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string EncryptDES(string plainText)
        {
            try
            {
                if (plainText == null)
                {
                    throw new ArgumentNullException("plainText");
                }
                if (_iv == null)
                {
                    throw new ArgumentNullException("IV");
                }
                if (_key == null)
                {
                    throw new ArgumentNullException("KEY");
                }

                byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);
                using (DESCryptoServiceProvider csp = new DESCryptoServiceProvider())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, csp.CreateEncryptor(_key, _iv), CryptoStreamMode.Write))
                        {
                            cs.Write(inputByteArray, 0, inputByteArray.Length);
                            cs.FlushFinalBlock();

                            var outputData = Convert.ToBase64String(ms.ToArray());
                            return outputData;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"DESHelper.Encrypt Exception, {ex.ToJsonString()}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string DecryptDES(string plainText)
        {
            try
            {
                if (plainText == null)
                {
                    throw new ArgumentNullException("plainText");
                }
                if (_iv == null)
                {
                    throw new ArgumentNullException("IV");
                }
                if (_key == null)
                {
                    throw new ArgumentNullException("KEY");
                }

                byte[] inputByteArray = Convert.FromBase64String(plainText);
                using (DESCryptoServiceProvider csp = new DESCryptoServiceProvider())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, csp.CreateDecryptor(_key, _iv), CryptoStreamMode.Write))
                        {
                            cs.Write(inputByteArray, 0, inputByteArray.Length);
                            cs.FlushFinalBlock();

                            var outputData = Encoding.UTF8.GetString(ms.ToArray());
                            return outputData;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"DESHelper.Encrypt Exception, {ex.ToJsonString()}");
            }
        }
    }
}
