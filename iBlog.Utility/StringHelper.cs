using System;
using System.Linq;

namespace iBlog.Utility
{
    public class StringHelper
    {
        /// <summary>
        /// 生成16位的短Guid字符串
        /// </summary>
        /// <returns></returns>
        public static string GenerateShortGuid()
        {
            long i = Guid.NewGuid().ToByteArray().Aggregate<byte, long>(1, (current, b) => current*((int) b + 1));
            var shortGuid = string.Format("{0:x}", i - DateTime.Now.Ticks);
            return shortGuid;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="strSource"></param>
        /// <param name="strEncode"></param>
        /// <returns></returns>
        public static string GetMd5(string strSource, string strEncode = "UTF-8")
        {
            //实例化md5类 
            var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();

            //获取密文字节数组
            var bytResult = md5.ComputeHash(System.Text.Encoding.GetEncoding(strEncode).GetBytes(strSource));

            //转换成字符串
            string strResult = BitConverter.ToString(bytResult);

            //BitConverter转换出来的字符串会在每个字符中间产生一个分隔符，需要去除掉 
            strResult = strResult.Replace("-", "");

            return strResult.ToLower();
        }
    }
}
