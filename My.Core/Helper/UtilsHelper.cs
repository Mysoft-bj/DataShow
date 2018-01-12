using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace My.Core
{
    public class UtilsHelper
    {
        const string passphrase = "nopasswordissafe!";
        static MD5 md5Provider = new MD5CryptoServiceProvider();
      
        private static RNGCryptoServiceProvider _global =
            new RNGCryptoServiceProvider();
        [ThreadStatic]
        private static Random _local;
        public static int Random()
        {
            Random inst = _local;
            if (inst == null)
            {
                byte[] buffer = new byte[4];
                _global.GetBytes(buffer);
                _local = inst = new Random(
                    BitConverter.ToInt32(buffer, 0));
            }
            return inst.Next();
        }
        public static Guid GenerateGuid()
        {

            byte[] guidArray = Guid.NewGuid().ToByteArray();
            var baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;
            var days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = now.TimeOfDay;
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);
            return new Guid(guidArray);
        }
        public static string Encrypt(string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(passphrase));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(Message);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }


        public static string Decrypt(string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(passphrase));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToDecrypt = Convert.FromBase64String(Message);
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }

     
        //private const string path = @"E:\GIT\";

        //public static void GenerateKey()
        //{
        //    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1112);
        //    string publickKey = rsa.ToXmlString(false);
        //    string privateKey = rsa.ToXmlString(true);

        //    WriteStringToFile(publickKey, path + "publickey.xml");
        //    WriteStringToFile(privateKey, path + "privatekey.xml");
        //}

        //public static void WriteStringToFile(string value, string filename)
        //{
        //    using (FileStream stream = File.Open(filename, FileMode.Create, FileAccess.Write, FileShare.Read))
        //    using (StreamWriter writer = new StreamWriter(stream))
        //    {
        //        writer.Write(value);
        //        writer.Flush();
        //        stream.Flush();
        //    }
        //}

        public static string RSAEncrypt(string data2Encrypt,string publicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);  
            byte[] plainbytes = System.Text.Encoding.UTF8.GetBytes(data2Encrypt);
            byte[] cipherbytes = rsa.Encrypt(plainbytes, false);
            return Convert.ToBase64String(cipherbytes);
        }

        public static string RSADecrypt(string data2Decrypt,string privatekey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();          
            rsa.FromXmlString(privatekey);  
            byte[] plainbytes = rsa.Decrypt(Convert.FromBase64String(data2Decrypt), false);
            return System.Text.Encoding.UTF8.GetString(plainbytes);

        }
    }
    
}
