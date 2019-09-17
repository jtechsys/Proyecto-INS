using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace doMain.Utils
{
    public class EncryptorUtils
    {
        #region Main Method for Encryption using RijndaelManaged

        private static byte[] saltByte = Encoding.ASCII.GetBytes("o8101982dAtApAtH");

        /// <summary>
        /// Method to encrypt plain text using Triple DES algorithm
        /// Key file to encrypt the text is derived from app configuration file
        /// </summary>
        /// <param name="plainText">plain text</param>
        /// <returns>returns encrypted text</returns>
        public static string EncryptText(string plainText)
        {
            string encryptedText = plainText;
            if (!string.IsNullOrEmpty(plainText))
            {
                string secretValue = "S101qaqz!QAZ";
                using (Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(secretValue, saltByte))
                {
                    using (RijndaelManaged encryptionAlgo = new RijndaelManaged())
                    {
                        encryptionAlgo.Key = key.GetBytes(encryptionAlgo.KeySize / 8);
                        using (ICryptoTransform encryptor = encryptionAlgo.CreateEncryptor(encryptionAlgo.Key, encryptionAlgo.IV))
                        {
                            MemoryStream stream = new MemoryStream();

                            // prepend the IV
                            stream.Write(BitConverter.GetBytes(encryptionAlgo.IV.Length), 0, sizeof(int));
                            stream.Write(encryptionAlgo.IV, 0, encryptionAlgo.IV.Length);
                            CryptoStream cryptoStream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write);

                            using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                            {
                                //Write all data to the stream.
                                streamWriter.Write(plainText);
                            }
                            encryptedText = Convert.ToBase64String(stream.ToArray());
                            stream.Dispose();
                            cryptoStream.Dispose();


                        }
                        encryptionAlgo.Clear();
                    }
                }
            }
            return encryptedText;
        }

        /// <summary>
        /// Method to encrypt plain text using Triple DES algorithm
        /// Key file to encrpyt the text is derived from app configuration file
        /// </summary>
        /// <param name="encryptedText">encrypted text</param>
        /// <returns>returns decrypted text</returns>
        public static string DecryptText(string encryptedText)
        {
            string plainText = encryptedText;

            if (!string.IsNullOrEmpty(encryptedText))
            {
                //generate the key from the shared secret and the salt
                string secretValue = "S101qaqz!QAZ";
                using (Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(secretValue, saltByte))
                {
                    // Create the streams used for decryption.                
                    byte[] bytes = Convert.FromBase64String(encryptedText);
                    MemoryStream msdecrypt = new MemoryStream(bytes);

                    // Create a RijndaelManaged object
                    // with the specified key and IV.
                    using (RijndaelManaged encryptionAlgo = new RijndaelManaged())
                    {
                        encryptionAlgo.Key = key.GetBytes(encryptionAlgo.KeySize / 8);
                        // Get the initialization vector from the encrypted stream
                        encryptionAlgo.IV = ReadByteArray(msdecrypt);

                        // Create a decrytor to perform the stream transform.
                        using (ICryptoTransform decryptor = encryptionAlgo.CreateDecryptor(encryptionAlgo.Key, encryptionAlgo.IV))
                        {
                            CryptoStream stream = new CryptoStream(msdecrypt, decryptor, CryptoStreamMode.Read);

                            using (StreamReader reader = new StreamReader(stream))
                            {
                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plainText = reader.ReadToEnd();
                            }
                            msdecrypt.Dispose();
                            stream.Dispose();
                        }


                        encryptionAlgo.Clear();
                    }

                }
            }
            return plainText;
        }
        public static string EncryptTextWithSameString(string clearText)
        {
            string EncryptionKey = "abc123";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            MemoryStream ms = null;
            CryptoStream cs = null;
            Rfc2898DeriveBytes pdb = null;
            try
            {
                using (Aes encryptor = Aes.Create())
                {
                    pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    ms = new MemoryStream();

                    cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write);
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                    clearText = Convert.ToBase64String(ms.ToArray());

                }
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                }
                if (cs != null)
                {
                    cs.Close();
                }
                if (pdb != null)
                {
                    pdb.Dispose();
                }
            }
            return clearText;
        }

        /// <summary>
        /// Method to encrypt plain text using Triple DES algorithm
        /// Key file to encrpyt the text is derived from app configuration file
        /// </summary>
        /// <param name="encryptedText">encrypted text</param>
        /// <returns>returns decrypted text</returns>
        public static string DecryptTextWithSameString(string cipherText)
        {
            string EncryptionKey = "abc123";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            MemoryStream ms = null;
            CryptoStream cs = null;
            Rfc2898DeriveBytes pdb = null;
            try
            {
                using (Aes encryptor = Aes.Create())
                {
                    pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    ms = new MemoryStream();

                    cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write);

                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();

                    cipherText = Encoding.Unicode.GetString(ms.ToArray());

                }
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                }
                if (cs != null)
                {
                    cs.Close();
                }
                if (pdb != null)
                {
                    pdb.Dispose();
                }
            }
            return cipherText;
        }
        public static string EncryptQueryString(string content)
        {
            string con = content;
            byte[] clearBytes = Encoding.Unicode.GetBytes(con);
            using (Aes encryptor = Aes.Create())
            {
                string encryptionKey = "EntedcryS10Yek";
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    con = Convert.ToBase64String(ms.ToArray());
                }
            }
            return HttpUtility.UrlEncode(con);
        }

        /// <summary>
        /// Decrypt the encrypted content
        /// </summary>
        /// <param name="encryptedContent">Encrypted content to be decrypted</param>
        /// <returns>Returns, decrypted content</returns>
        public static string DecryptQueryString(string encryptedContent)
        {
            string encryptContent = encryptedContent;
            string encryptionKey = "EntedcryS10Yek";
            encryptContent = HttpUtility.UrlDecode(encryptContent).Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(encryptContent);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                MemoryStream ms = new MemoryStream();

                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                encryptContent = Encoding.Unicode.GetString(ms.ToArray());
                ms.Dispose();
            }

            return encryptContent;
        }

        /// <summary>
        /// Method to readet byte array text
        /// </summary>
        /// <param name="stream">stream</param>
        /// <returns>returns byte array</returns>
        private static byte[] ReadByteArray(Stream stream)
        {
            byte[] rawLength = new byte[sizeof(int)];
            if (stream.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                //throw new SystemException("Stream did not contain properly formatted byte array");
            }

            byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (stream.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                //throw new SystemException("Did not read byte array properly");
            }

            return buffer;
        }

        #endregion

        private static byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
        private static byte[] iv = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

        /// <summary>
        /// Method to encrypt text
        /// </summary>
        /// <param name="_CadenaEncriptar">stream</param>
        /// <returns>returns byte array</returns>
        public static string Encriptar(string _CadenaEncriptar)
        {
            string result = string.Empty;
            try
            {
                byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_CadenaEncriptar);
                result = Convert.ToBase64String(encryted);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Method to desencrypt text
        /// </summary>
        /// <param name="_CadenaDesencriptar">stream</param>
        /// <returns>returns byte array</returns>
        public static string DesEncriptar(string _CadenaDesencriptar)
        {
            string result = string.Empty;
            try
            {
                byte[] decryted = Convert.FromBase64String(_CadenaDesencriptar);
                result = System.Text.Encoding.Unicode.GetString(decryted);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }

}
