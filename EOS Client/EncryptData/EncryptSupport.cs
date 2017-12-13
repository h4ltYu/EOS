using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace EncryptData
{
    public class EncryptSupport
    {
        public static byte[] ObjectToByteArray(object obj)
        {
            byte[] result;
            if (obj == null)
            {
                result = null;
            }
            else
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                MemoryStream memoryStream = new MemoryStream();
                binaryFormatter.Serialize(memoryStream, obj);
                result = memoryStream.ToArray();
            }
            return result;
        }

        public static object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            memoryStream.Write(arrBytes, 0, arrBytes.Length);
            memoryStream.Seek(0L, SeekOrigin.Begin);
            return binaryFormatter.Deserialize(memoryStream);
        }

        public static bool EncryptQuestions_SaveToFile(string fname, byte[] data, string key)
        {
            bool result;
            try
            {
                FileStream fileStream = new FileStream(fname, FileMode.Create, FileAccess.Write);
                CryptoStream cryptoStream = new CryptoStream(fileStream, new DESCryptoServiceProvider
                {
                    Key = Encoding.ASCII.GetBytes(key),
                    IV = Encoding.ASCII.GetBytes(key)
                }.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.Close();
                fileStream.Close();
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static byte[] DecryptQuestions_FromFile(string fname, string key)
        {
            byte[] result;
            try
            {
                FileStream fileStream = new FileStream(fname, FileMode.Open, FileAccess.Read);
                CryptoStream cryptoStream = new CryptoStream(fileStream, new DESCryptoServiceProvider
                {
                    Key = Encoding.ASCII.GetBytes(key),
                    IV = Encoding.ASCII.GetBytes(key)
                }.CreateDecryptor(), CryptoStreamMode.Read);
                byte[] array = new byte[fileStream.Length];
                int num = cryptoStream.Read(array, 0, (int)fileStream.Length);
                cryptoStream.Close();
                fileStream.Close();
                result = array;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static string Encryption(byte[] data, string key)
        {
            DES des = new DESCryptoServiceProvider();
            des.Key = Encoding.ASCII.GetBytes(key);
            des.IV = des.Key;
            des.Padding = PaddingMode.PKCS7;
            MemoryStream memoryStream = new MemoryStream();
            ICryptoTransform transform = des.CreateEncryptor();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
            cryptoStream.Write(data, 0, data.Length);
            cryptoStream.FlushFinalBlock();
            memoryStream.Position = 0L;
            string result = string.Empty;
            result = Convert.ToBase64String(memoryStream.ToArray());
            cryptoStream.Close();
            return result;
        }

        public static string Decryption(byte[] data, string key)
        {
            DES des = new DESCryptoServiceProvider();
            des.Key = Encoding.ASCII.GetBytes(key);
            des.IV = des.Key;
            des.Padding = PaddingMode.PKCS7;
            MemoryStream stream = new MemoryStream(data);
            CryptoStream cryptoStream = new CryptoStream(stream, des.CreateDecryptor(), CryptoStreamMode.Read);
            byte[] array = new byte[data.Length];
            cryptoStream.Read(array, 0, array.Length);
            cryptoStream.Close();
            return Encoding.Unicode.GetString(array);
        }

        public static string GetMD5(string msg)
        {
            MD5 md = new MD5CryptoServiceProvider();
            byte[] bytes = md.ComputeHash(Encoding.Unicode.GetBytes(msg));
            return Encoding.Unicode.GetString(bytes);
        }
    }
}
