using System.Security.Cryptography;
using System.Text;

namespace LS.ERP.Infrastructure
{
    /// <summary>
    /// 加密解密类
    /// </summary>
    public static class DEncrypt
    {
        private static byte[] desRgbIV = new byte[8] { 18, 52, 86, 120, 144, 171, 205, 239 };

        private static byte[] rc2RgbIV = new byte[8] { 18, 52, 86, 120, 144, 171, 205, 239 };

        private static byte[] aesRgbIV = Convert.FromBase64String("Rkb4jvUy/ye7Cd7k89QQgQ==");

        /// <summary>
        /// 生成MD5摘要
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        private static byte[] MakeMD5(byte[] original)
        {
            using MD5 mD = MD5.Create();
            return mD.ComputeHash(original);
        }

        /// <summary>
        /// 生成MD5摘要
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static byte[] MakeMD5(string original)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(original);
            return MakeMD5(bytes);
        }

        /// <summary>
        /// 生成MD5摘要
        /// </summary>
        /// <param name="orginal"></param>
        /// <returns></returns>
        public static string MakeMD5Str(string orginal)
        {
            byte[] bytes = MakeMD5(orginal);
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// 生成MD5摘要
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Md5Hex(string content, bool uppercase = false)
        {
            using MD5 mD = MD5.Create();
            byte[] array = mD.ComputeHash(Encoding.UTF8.GetBytes(content));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                stringBuilder.Append(array[i].ToString("x2"));
            }
            string text2 = stringBuilder.ToString();
            return (!uppercase) ? text2 : text2.ToUpper();
        }

        /// <summary>
        /// SHA256加密 在中间位置插入源字节数组的最后一个字节 在最后位置插入源字节数组的第一个字节 最后进行Hash计算
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static byte[] MakeHashSalt(byte[] original)
        {
            List<byte> list = new List<byte>(original);
            list.Insert(list.Count / 2, original[^1]);
            list.Add(original[0]);
            using SHA256 sHA = SHA256.Create();
            return sHA.ComputeHash(list.ToArray());
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        public static string DESEncryptStr(string encryptString, string encryptKey)
        {
            byte[] inArray = DESEncrypt(encryptString, encryptKey);
            return Convert.ToBase64String(inArray);
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        public static byte[] DESEncrypt(string encryptString, string encryptKey)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(encryptString);
            using DES dES = DES.Create();
            using MemoryStream memoryStream = new MemoryStream();
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, dES.CreateEncryptor(Encoding.UTF8.GetBytes(encryptKey), desRgbIV), CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            return memoryStream.ToArray();
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="decryptString"></param>
        /// <param name="decryptKey"></param>
        /// <returns></returns>
        public static string DESDecryptStr(string decryptString, string decryptKey)
        {
            byte[] bytes = DESDecrypt(decryptString, decryptKey);
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="decryptString">密文</param>
        /// <param name="decryptKey">密匙（8位）</param>
        /// <returns></returns>
        public static byte[] DESDecrypt(string decryptString, string decryptKey)
        {
            byte[] array = Convert.FromBase64String(decryptString);
            using DES dES = DES.Create();
            using MemoryStream memoryStream = new MemoryStream();
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, dES.CreateDecryptor(Encoding.UTF8.GetBytes(decryptKey), desRgbIV), CryptoStreamMode.Write);
            cryptoStream.Write(array, 0, array.Length);
            cryptoStream.FlushFinalBlock();
            return memoryStream.ToArray();
        }

        /// <summary>
        /// 3DES 加密
        /// </summary>
        /// <param name="encryptString">待加密密文</param>
        /// <param name="encryptKey1">密匙1(长度必须为8位)</param>
        /// <param name="encryptKey2">密匙2(长度必须为8位)</param>
        /// <param name="encryptKey3">密匙3(长度必须为8位)</param>
        /// <returns></returns>
        public static string DES3Encrypt(string encryptString, string encryptKey1, string encryptKey2, string encryptKey3)
        {
            string encryptString2 = DESEncryptStr(encryptString, encryptKey3);
            encryptString2 = DESEncryptStr(encryptString2, encryptKey2);
            return DESEncryptStr(encryptString2, encryptKey1);
        }

        /// <summary>
        /// 3DES 解密
        /// </summary>
        /// <param name="decryptString">待解密密文</param>
        /// <param name="decryptKey1">密匙1(长度必须为8位)</param>
        /// <param name="decryptKey2">密匙2(长度必须为8位)</param>
        /// <param name="decryptKey3">密匙3(长度必须为8位)</param>
        /// <returns></returns>
        public static string DES3Decrypt(string decryptString, string decryptKey1, string decryptKey2, string decryptKey3)
        {
            string decryptString2 = DESDecryptStr(decryptString, decryptKey1);
            decryptString2 = DESDecryptStr(decryptString2, decryptKey2);
            return DESDecryptStr(decryptString2, decryptKey3);
        }

        /// <summary>
        /// RC2加密
        /// </summary>
        /// <param name="encryptString">待加密的密文</param>
        /// <param name="encryptKey">密匙(必须为5-16位)</param>
        /// <returns></returns>
        public static string RC2EncryptStr(string encryptString, string encryptKey)
        {
            byte[] inArray = RC2Encrypt(encryptString, encryptKey);
            return Convert.ToBase64String(inArray);
        }

        /// <summary>
        /// RC2加密
        /// </summary>
        /// <param name="encryptString">待加密的密文</param>
        /// <param name="encryptKey">密匙(必须为5-16位)</param>
        /// <returns></returns>
        public static byte[] RC2Encrypt(string encryptString, string encryptKey)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(encryptString);
            using RC2 rC = RC2.Create();
            using MemoryStream memoryStream = new MemoryStream();
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, rC.CreateEncryptor(Encoding.UTF8.GetBytes(encryptKey), rc2RgbIV), CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            return memoryStream.ToArray();
        }

        /// <summary>
        /// RC2解密
        /// </summary>
        /// <param name="decryptString">密文</param>
        /// <param name="decryptKey">密匙(必须为5-16位)</param>
        /// <returns></returns>
        public static string RC2DecryptStr(string decryptString, string decryptKey)
        {
            byte[] bytes = RC2Decrypt(decryptString, decryptKey);
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// RC2解密
        /// </summary>
        /// <param name="decryptString">密文</param>
        /// <param name="decryptKey">密匙(必须为5-16位)</param>
        /// <returns></returns>
        public static byte[] RC2Decrypt(string decryptString, string decryptKey)
        {
            byte[] array = Convert.FromBase64String(decryptString);
            using RC2 rC = RC2.Create();
            using MemoryStream memoryStream = new MemoryStream();
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, rC.CreateDecryptor(Encoding.UTF8.GetBytes(decryptKey), rc2RgbIV), CryptoStreamMode.Write);
            cryptoStream.Write(array, 0, array.Length);
            cryptoStream.FlushFinalBlock();
            return memoryStream.ToArray();
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="encryptString">待加密的明文，请不要将此参数作Base64转换，内部会转换</param>
        /// <param name="encryptKey">加密密匙128位</param>
        /// <returns></returns>
        public static string AESEncryptStr(string encryptString, string encryptKey)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(encryptString);
            string encryptString2 = Convert.ToBase64String(bytes);
            byte[] inArray = AESEncrypt(encryptString2, encryptKey);
            return Convert.ToBase64String(inArray);
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="encryptString">待加密的明文，请不要将此参数作Base64转换，内部会转换</param>
        /// <param name="encryptKey">加密密匙128位</param>
        /// <returns></returns>
        public static string AESEncryptStr(string encryptString, byte[] rgbKey)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(encryptString);
            string encryptString2 = Convert.ToBase64String(bytes);
            byte[] inArray = AESEncrypt(encryptString2, rgbKey);
            return Convert.ToBase64String(inArray);
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="encryptString">待加密的明文，请不要将此参数作Base64转换，内部会转换</param>
        /// <param name="encryptKey">加密密匙128位</param>
        /// <returns></returns>
        private static byte[] AESEncrypt(string encryptString, string encryptKey)
        {
            byte[] rgbKey = Convert.FromBase64String(encryptKey);
            return AESEncrypt(encryptString, rgbKey);
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="encryptString">待加密的密文</param>
        /// <param name="rgbKey">加密密匙128位</param>
        /// <returns></returns>
        private static byte[] AESEncrypt(string encryptString, byte[] rgbKey)
        {
            using Aes aes = Aes.Create();
            aes.IV = aesRgbIV;
            aes.Key = rgbKey;
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            using ICryptoTransform cryptoTransform = aes.CreateEncryptor();
            byte[] array = Convert.FromBase64String(encryptString);
            return cryptoTransform.TransformFinalBlock(array, 0, array.Length);
        }

        /// <summary>
        /// AES 解密
        /// </summary>
        /// <param name="decryptString">待解密密文</param>
        /// <param name="decryptKey">解密密钥</param>
        /// <returns></returns>
        public static string AESDecryptStr(string decryptString, string decryptKey)
        {
            byte[] bytes = AESDecrypt(decryptString, decryptKey);
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// AES 解密
        /// </summary>
        /// <param name="decryptString">待解密密文</param>
        /// <param name="rgbKey">密钥128位</param>
        /// <returns></returns>
        public static string AESDecryptStr(string decryptString, byte[] rgbKey)
        {
            byte[] bytes = AESDecrypt(decryptString, rgbKey);
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// AES 解密
        /// </summary>
        /// <param name="decryptString">待解密密文</param>
        /// <param name="rgbKey">密钥</param>
        /// <returns></returns>
        public static byte[] AESDecrypt(string decryptString, byte[] rgbKey)
        {
            using Aes aes = Aes.Create();
            aes.IV = aesRgbIV;
            aes.Key = rgbKey;
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            using ICryptoTransform cryptoTransform = aes.CreateDecryptor();
            byte[] array = Convert.FromBase64String(decryptString);
            return cryptoTransform.TransformFinalBlock(array, 0, array.Length);
        }

        /// <summary>
        /// AES 解密
        /// </summary>
        /// <param name="decryptString">待解密密文</param>
        /// <param name="decryptKey">解密密钥</param>
        /// <returns></returns>
        public static byte[] AESDecrypt(string decryptString, string decryptKey)
        {
            byte[] rgbKey = Convert.FromBase64String(decryptKey);
            return AESDecrypt(decryptString, rgbKey);
        }
    }
}
