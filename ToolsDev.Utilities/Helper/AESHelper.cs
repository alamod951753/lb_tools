using System;
using System.Security.Cryptography;
using System.Text;

namespace ToolsDev.Utilities.Helper
{
    /// <summary>
    /// AES Encrypt/Decrypt
    /// </summary>
    public class AESHelper
    {
        private byte[] _iv;
        private byte[] _key;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IV"></param>
        /// <param name="KEY"></param>
        public AESHelper(string IV, string KEY)
        {
            _iv = Convert.FromBase64String(IV);
            _key = Convert.FromBase64String(KEY);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IV"></param>
        /// <param name="KEY"></param>
        public AESHelper(byte[] IV, string KEY)
        {
            _iv = IV;
            _key = Encoding.UTF8.GetBytes(KEY);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public byte[] Encrypt(string plainText)
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

                Aes aes = Aes.Create();
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aes.CreateEncryptor(_key, _iv);
                byte[] bPlainText = plainText.StringToByteArray();
                byte[] outputData = encryptor.TransformFinalBlock(bPlainText, 0, bPlainText.Length);

                return outputData;
            }
            catch (Exception ex)
            {
                throw new Exception($"AESHelper.Encrypt Exception, {ex.ToJsonString()}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public string Decrypt(byte[] cipherText)
        {
            try
            {
                if (cipherText == null || cipherText.Length <= 0)
                {
                    throw new ArgumentNullException("cipherText");
                }
                if (_iv == null)
                {
                    throw new ArgumentNullException("IV");
                }
                if (_key == null)
                {
                    throw new ArgumentNullException("KEY");
                }

                Aes aes = Aes.Create();
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aes.CreateDecryptor(_key, _iv);
                var output = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
                var plainText = Encoding.UTF8.GetString(output);

                return plainText;
            }
            catch (Exception ex)
            {
                throw new Exception($"AESHelper.Decrypt Exception, {ex.ToJsonString()}");
            }
        }
    }
}
