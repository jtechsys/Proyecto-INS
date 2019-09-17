using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

using System.Configuration;
using doMain.Utils;
using doMain.Utils;

namespace doMain.Utils
{
    public class CryptoUtils
    {

        public static string Encrypt(string clearText)
        {

            if (clearText == null)
                return null;

            string EncryptionKey = Resource.key;
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string Decrypt(string cipherText)
        {
            if (cipherText == null)
                return null;

            string EncryptionKey = Resource.key;
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }


        //private static byte[] _salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");

        ///// <summary>
        ///// Encrypt the given string using AES.  The string can be decrypted using 
        ///// DecryptStringAES().  The sharedSecret parameters must match.
        ///// </summary>
        ///// <param name="plainText">The text to encrypt.</param>
        ///// <param name="sharedSecret">A password used to generate a key for encryption.</param>
        //public static string EncryptStringAES(string plainText)
        //{
        //    string sharedSecret = Resource.key;

        //    if (string.IsNullOrEmpty(plainText))
        //        throw new ArgumentNullException("plainText");
        //    if (string.IsNullOrEmpty(sharedSecret))
        //        throw new ArgumentNullException("sharedSecret");

        //    string outStr = null;                       // Encrypted string to return
        //    RijndaelManaged aesAlg = null;              // RijndaelManaged object used to encrypt the data.

        //    try
        //    {
        //        // generate the key from the shared secret and the salt
        //        Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);

        //        // Create a RijndaelManaged object
        //        aesAlg = new RijndaelManaged();
        //        aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

        //        // Create a decryptor to perform the stream transform.
        //        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        //        // Create the streams used for encryption.
        //        using (MemoryStream msEncrypt = new MemoryStream())
        //        {
        //            // prepend the IV
        //            msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
        //            msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
        //            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        //            {
        //                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
        //                {
        //                    //Write all data to the stream.
        //                    swEncrypt.Write(plainText);
        //                }
        //            }
        //            outStr = Convert.ToBase64String(msEncrypt.ToArray());
        //        }
        //    }
        //    finally
        //    {
        //        // Clear the RijndaelManaged object.
        //        if (aesAlg != null)
        //            aesAlg.Clear();
        //    }

        //    // Return the encrypted bytes from the memory stream.
        //    return outStr;
        //}

        ///// <summary>
        ///// Decrypt the given string.  Assumes the string was encrypted using 
        ///// EncryptStringAES(), using an identical sharedSecret.
        ///// </summary>
        ///// <param name="cipherText">The text to decrypt.</param>
        ///// <param name="sharedSecret">A password used to generate a key for decryption.</param>
        //public static string DecryptStringAES(string cipherText)
        //{
        //    string sharedSecret = Resource.key;

        //    if (string.IsNullOrEmpty(cipherText))
        //        throw new ArgumentNullException("cipherText");
        //    if (string.IsNullOrEmpty(sharedSecret))
        //        throw new ArgumentNullException("sharedSecret");

        //    // Declare the RijndaelManaged object
        //    // used to decrypt the data.
        //    RijndaelManaged aesAlg = null;

        //    // Declare the string used to hold
        //    // the decrypted text.
        //    string plaintext = null;

        //    try
        //    {
        //        // generate the key from the shared secret and the salt
        //        Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);

        //        // Create the streams used for decryption.                
        //        byte[] bytes = Convert.FromBase64String(cipherText);
        //        using (MemoryStream msDecrypt = new MemoryStream(bytes))
        //        {
        //            // Create a RijndaelManaged object
        //            // with the specified key and IV.
        //            aesAlg = new RijndaelManaged();
        //            aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
        //            // Get the initialization vector from the encrypted stream
        //            aesAlg.IV = ReadByteArray(msDecrypt);
        //            // Create a decrytor to perform the stream transform.
        //            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
        //            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
        //            {
        //                using (StreamReader srDecrypt = new StreamReader(csDecrypt))

        //                    // Read the decrypted bytes from the decrypting stream
        //                    // and place them in a string.
        //                    plaintext = srDecrypt.ReadToEnd();
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        // Clear the RijndaelManaged object.
        //        if (aesAlg != null)
        //            aesAlg.Clear();
        //    }

        //    return plaintext;
        //}

        //private static byte[] ReadByteArray(Stream s)
        //{
        //    byte[] rawLength = new byte[sizeof(int)];
        //    if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
        //    {
        //        throw new SystemException("Stream did not contain properly formatted byte array");
        //    }

        //    byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
        //    if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
        //    {
        //        throw new SystemException("Did not read byte array properly");
        //    }

        //    return buffer;
        //}


    }
}
