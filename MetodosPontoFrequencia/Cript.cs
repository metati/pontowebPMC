﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace MetodosPontoFrequencia
{
    public class Cript
    {
        private TripleDESCryptoServiceProvider des3;
        private const string actionKey = "EA81AA1D5FC1EC53E84F30AA746139EEBAFF8A9B76638895";
        private const string actionIv = "87AF7EA221F3FFF5";

        public Cript()
        {
            des3 = new TripleDESCryptoServiceProvider();
            des3.Mode = CipherMode.CBC;
        }

        private static Byte[] ConvertStringToByArray(string s)
        {
            return (new UnicodeEncoding()).GetBytes(s);
        }

        public static string MD5(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }
            Byte[] toHash = ConvertStringToByArray(s);
            byte[] hashValue = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(toHash);
            return BitConverter.ToString(hashValue);
        }

        public static string Base64Encode(string key)
        {
            if (string.IsNullOrEmpty(key))
                return string.Empty;

            byte[] buffer = Encoding.UTF8.GetBytes(key);
            return Convert.ToBase64String(buffer);
        }

        public static string Base64Decode(string key)
        {
            if (string.IsNullOrEmpty(key))
                return "";

            byte[] buffer = Convert.FromBase64String(key);
            return Encoding.UTF8.GetString(buffer);
        }

        public string GenerateKey()
        {
            des3.GenerateKey();
            return BytesToHex(des3.Key);
        }

        public string GenerateIV()
        {
            des3.GenerateIV();
            return BytesToHex(des3.IV);
        }

        private byte[] HexToBytes(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length / 2; i++)
            {
                string code = hex.Substring(i * 2, 2);
                bytes[i] = byte.Parse(code, System.Globalization.NumberStyles.HexNumber);
            }
            return bytes;
        }

        private string BytesToHex(byte[] bytes)
        {
            StringBuilder hex = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
                hex.AppendFormat("{0:X2}", bytes[i]);
            return hex.ToString();
        }

        public string Criptografar(string data, string key, string iv)
        {
            byte[] bdata = Encoding.UTF8.GetBytes(data);
            byte[] bkey = HexToBytes(key);
            byte[] biv = HexToBytes(iv);

            MemoryStream stream = new MemoryStream();
            CryptoStream encStream = new CryptoStream(stream,
             des3.CreateEncryptor(bkey, biv), CryptoStreamMode.Write);

            encStream.Write(bdata, 0, bdata.Length);
            encStream.FlushFinalBlock();
            encStream.Close();

            return BytesToHex(stream.ToArray());
        }


        public string Descriptografar(string data, string key, string iv)
        {
            byte[] bdata = HexToBytes(data);
            byte[] bkey = HexToBytes(key);
            byte[] biv = HexToBytes(iv);

            MemoryStream stream = new MemoryStream();
            CryptoStream encStream = new CryptoStream(stream,
             des3.CreateDecryptor(bkey, biv), CryptoStreamMode.Write);

            encStream.Write(bdata, 0, bdata.Length);
            encStream.FlushFinalBlock();
            encStream.Close();

            return Encoding.UTF8.GetString(stream.ToArray());
        }

        public string ActionEncrypt(string data)
        {
            return Criptografar(data, actionKey, actionIv);
        }

        public string ActionDecrypt(string data)
        {
            return Descriptografar(data, actionKey, actionIv);
        }
    }
}
