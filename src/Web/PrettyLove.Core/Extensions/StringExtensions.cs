using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace PrettyLove.Core
{
    public static class StringExtensions
    {
        private static string codeValue = "0123456789ABCDEFGHJKLMNPQRSTUVWXYZ";

        /// <summary>
        /// 将当前字符串转换为指定编码的的字节组。
        /// </summary>
        /// <param name="value">当前字符串。</param>
        /// <param name="encoding">编码。为 null 值表示 UTF8 的编码。</param>
        /// <returns>字节组。</returns>
        public static byte[] ToBytes(this string value, Encoding encoding = null) => (encoding ?? Encoding.UTF8).GetBytes(value);

        /// <summary>
        /// 将当前字节组转换为指定编码的的字符串。
        /// </summary>
        /// <param name="bytes">当前字节组。</param>
        /// <param name="encoding">编码。为 null 值表示 UTF8 的编码。</param>
        /// <returns>字符串。</returns>
        public static string GetString(this byte[] bytes, Encoding encoding = null) => (encoding ?? Encoding.UTF8).GetString(bytes);

        /// <summary>
        /// 将当前字符串转换为智能小写模式。
        /// </summary>
        /// <param name="s">当前字符串。</param>
        /// <returns>新的字符串。</returns>
        public static string ToCamelCase(this string s)
        {
            if (string.IsNullOrWhiteSpace(s) || !char.IsUpper(s[0])) return s;

            var chars = s.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                var hasNext = i + 1 < chars.Length;
                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                    break;
                chars[i] = char.ToLower(chars[i]);
            }

            return new string(chars);
        }

        /// <summary>
        /// 忽略被比较字符串的大小写，确定两个指定的实例是否具有同一值
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsEquals(this string a, string b) => string.Equals(a, b, StringComparison.CurrentCultureIgnoreCase);

        /// <summary>
        /// 忽略被比较字符串的大小写，确定在使用指定的比较选项进行比较时此字符串实例的开头是否与指定的字符串匹配
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsStartsWith(this string a, string b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.StartsWith(b, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 忽略被比较字符串的大小写，确定使用指定的比较选项进行比较时此字符串实例的结尾是否与指定的字符串匹配
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsEndsWith(this string a, string b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.EndsWith(b, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 忽略被比较字符串的大小写，返回一个值，该值指示指定的对象是否出现在此字符串中
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsContains(this string a, string b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.IndexOf(b, StringComparison.CurrentCultureIgnoreCase) > -1;
        }

        /// <summary>
        /// 在当前字符串的前后增加“%”符号。
        /// </summary>
        /// <param name="input">当前字符串。</param>
        /// <returns>新的字符串。</returns>
        public static string ToLiking(this string input) => string.Concat("%", input, "%");

        /// <summary>
        /// 返回表示当前 <see cref="string"/>，如果 <paramref name="input"/> 是一个 null 值，将返回 <see cref="string.Empty"/>。
        /// </summary>
        /// <param name="input">一个字符串。</param>
        /// <returns> <paramref name="input"/> 的 <see cref="string"/> 或 <see cref="string.Empty"/>。</returns>
        public static string ToStringOrEmpty(this string input) => input ?? string.Empty;

        /// <summary>
        /// 判定当前字符串是否是一个空的字符串。
        /// </summary>
        /// <param name="input">当前字符串。</param>
        /// <returns>如果字符串为 null、空 或 空白，将返回 true，否则返回 false。</returns>
        public static bool IsNull(this string input) => string.IsNullOrEmpty(input);

        /// <summary>
        /// 判定当前字符串是否是一个空的字符串。
        /// </summary>
        /// <param name="input">当前字符串。</param>
        /// <returns>如果字符串为 null、空 或 空白，将返回 true，否则返回 false。</returns>
        public static bool IsNullOrWhiteSpace(this string input) => string.IsNullOrWhiteSpace(input);

        /// <summary>
        /// 判定当前字符串是否不是一个空的字符串。
        /// </summary>
        /// <param name="input">当前字符串。</param>
        /// <returns>如果字符串为 null、空 或 空白，将返回 true，否则返回 false。</returns>
        public static bool IsNotNull(this string input) => !string.IsNullOrEmpty(input);

        /// <summary>
        /// 判定当前字符串是否不是一个空的字符串。
        /// </summary>
        /// <param name="input">当前字符串。</param>
        /// <returns>如果字符串为 null、空 或 空白，将返回 true，否则返回 false。</returns>
        public static bool IsNotNullOrWhiteSpace(this string input) => !string.IsNullOrWhiteSpace(input);

        /// <summary>
        /// 指定整串字符串的最大长度，剪裁字符串数据，超出部分将会在结尾添加“...”。
        /// </summary>
        /// <param name="input">一个字符串。</param>
        /// <param name="maxLength">字符串的最大长度（含）。</param>
        /// <param name="ellipsis">指定省略号的字符串，默认为“...”。</param>
        /// <returns>新的字符串 -或- 原字符串，该字符串的最大长度不超过 <paramref name="maxLength"/>。</returns>
        public static string CutString(this string input, int maxLength, string ellipsis = "...")
        {
            if (maxLength <= 0) throw new ArgumentOutOfRangeException(nameof(maxLength));
            if (string.IsNullOrWhiteSpace(ellipsis)) throw new ArgumentNullException(nameof(ellipsis));
            if (input is null || input.Length <= maxLength) return input;
            return input.Substring(0, maxLength) + ellipsis;
        }

        /// <summary>
        /// 截取字符串开头的内容。
        /// </summary>
        /// <param name="input">一个字符串。</param>
        /// <param name="length">获取的字符串长度。</param>
        /// <returns>新的字符串。</returns>
        public static string Starts(this string input, int length)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return length >= input.Length ? input : input.Substring(0, length);
        }

        /// <summary>
        /// 截取字符串结尾的内容。
        /// </summary>
        /// <param name="input">一个字符串。</param>
        /// <param name="length">获取的字符串长度。</param>
        /// <returns>新的字符串。</returns>
        public static string Ends(this string input, int length)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return length >= input.Length ? input : input.Substring(input.Length - length);
        }

        /// <summary>
        /// 删除当前字符串的开头的字符串。
        /// </summary>
        /// <param name="val">目标字符串。</param>
        /// <param name="count">要删除的字长度。</param>
        /// <returns>删除后的字符串。</returns>
        public static string RemoveStarts(this string val, int count = 1)
        {
            if (string.IsNullOrEmpty(val) || val.Length <= count) return val;
            return val.Remove(0, count);
        }

        /// <summary>
        /// 删除当前字符串的结尾的字符串。
        /// </summary>
        /// <param name="val">目标字符串。</param>
        /// <param name="count">要删除的字长度。</param>
        /// <returns>删除后的字符串。</returns>
        public static string RemoveEnds(this string val, int count = 1)
        {
            if (string.IsNullOrEmpty(val) || val.Length <= count) return val;
            return val.Remove(val.Length - count);
        }

        /// <summary>
        /// 获取字符串的字节数。
        /// </summary>
        /// <param name="val">目标字符串。</param>
        /// <returns>字符串的字节数。</returns>
        public static int GetDataLength(this string val)
        {
            if (string.IsNullOrEmpty(val)) return 0;

            int length = 0;
            foreach (var c in val)
            {
                length += (c >= 0 && c <= 128) ? 1 : 2;
            }
            return length;
        }

        /// <summary>
        /// 判断字符串是否只包含数字的主键(默认至少5位)
        /// </summary>
        /// <param name="source">目标字符串。</param>
        /// <param name="min">最少位数,默认5位</param>
        /// <returns></returns>
        public static bool IsIdNumber(this string source, int min = 5)
        {
            Match match = Regex.Match(source, $@"^\d{{{min},}}$");
            return match.Success;
        }

        /// <summary> 
        ///  将查询字符串解析转换为名值集合.
        /// </summary> 
        /// <param name="url"></param> 
        /// <returns></returns> 
        public static NameValueCollection GetQueryString(string url)
        {
            Uri uri = new Uri(url);
            string queryString = uri.Query;
            return GetQueryString(queryString, null, true);
        }

        ///   <summary> 
        ///  将查询字符串解析转换为名值集合.
        ///   </summary> 
        ///   <param name="queryString"></param> 
        ///   <param name="encoding"></param> 
        ///   <param name="isEncoded"></param> 
        ///   <returns></returns> 
        public static NameValueCollection GetQueryString(string queryString, Encoding encoding, bool isEncoded)
        {
            queryString = queryString.Replace("?", "");
            NameValueCollection result = new NameValueCollection(StringComparer.OrdinalIgnoreCase);
            if (!string.IsNullOrEmpty(queryString))
            {
                int count = queryString.Length;
                for (int i = 0; i < count; i++)
                {
                    int startIndex = i;
                    int index = -1;
                    while (i < count)
                    {
                        char item = queryString[i];
                        if (item == '=')
                        {
                            if (index < 0)
                            {
                                index = i;
                            }
                        }
                        else if (item == '&')
                        {
                            break;
                        }
                        i++;
                    }

                    string value = null;
                    string key;
                    if (index >= 0)
                    {
                        key = queryString.Substring(startIndex, index - startIndex);
                        value = queryString.Substring(index + 1, (i - index) - 1);
                    }
                    else
                    {
                        key = queryString.Substring(startIndex, i - startIndex);
                    }
                    if (isEncoded)
                    {
                        result[MyUrlDeCode(key, encoding)] = MyUrlDeCode(value, encoding);
                    }
                    else
                    {
                        result[key] = value;
                    }
                    if ((i == (count - 1)) && (queryString[i] == '&'))
                    {
                        result[key] = string.Empty;
                    }
                }
            }
            return result;
        }

        ///   <summary> 
        ///  解码URL.
        ///   </summary> 
        ///   <param name="encoding"> null为自动选择编码 </param> 
        ///   <param name="str"></param> 
        ///   <returns></returns> 
        public static string MyUrlDeCode(string str, Encoding encoding)
        {
            if (encoding == null)
            {
                Encoding utf8 = Encoding.UTF8;
                // 首先用utf-8进行解码                      
                string code = HttpUtility.UrlDecode(str?.ToUpper(), utf8);
                // 将已经解码的字符再次进行编码. 
                string encode = HttpUtility.UrlEncode(code, utf8)?.ToUpper();
                if (str == encode)
                    encoding = Encoding.UTF8;
                else
                    encoding = Encoding.GetEncoding("gb2312");
            }
            return HttpUtility.UrlDecode(str, encoding);
        }

        /// <summary>
        /// 将当前字符串转换为指定编码的的字节组。
        /// </summary>
        /// <param name="text">当前字符串。</param>
        /// <param name="encoding">编码。为 null 值表示 UTF8 的编码。</param>
        /// <returns>字节组。</returns>
        public static byte[] GetBytes(this string text, Encoding encoding = null)
        {
            return (encoding ?? Encoding.UTF8)!.GetBytes(text);
        }

        /// <summary>
        /// 将长Guid转换为短Guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static string ToShortId(this Guid guid)
        {
            var str = Convert.ToBase64String(guid.ToByteArray());
            return str.Replace("/", "").Replace("+", "").Replace("=", "");
        }

        /// <summary>
        /// MD5 加密(小写)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMd5(this string value)
        {
            byte[] bytes;
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(value));
            }
            var result = new StringBuilder();
            foreach (byte t in bytes)
            {
                result.Append(t.ToString("x2"));
            }
            return result.ToString();
        }

        /// <summary>
        /// 判断字符串是否在指定的字符串数组中
        /// </summary>
        /// <param name="str"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsIn(this string str, params string[] data)
        {
            foreach (var item in data)
            {
                if (str == item)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取字符串的左len个字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Left(this string str, int len)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.Length < len)
            {
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            }

            return str.Substring(0, len);
        }

        /// <summary>
        /// 获取字符串的右len个字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Right(this string str, int len)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.Length < len)
            {
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            }

            return str.Substring(str.Length - len, len);
        }

        /// <summary>
        /// 获取字符串的首个单词，如果首字母是大写，则返回帕斯卡命名的首个单词，否则返回驼峰命名的首个单词
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetCamelCaseFirstWord(this string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (str.Length == 1)
            {
                return str;
            }

            var res = Regex.Split(str, @"(?=\p{Lu}\p{Ll})|(?<=\p{Ll})(?=\p{Lu})");

            if (res.Length < 1)
            {
                return str;
            }
            else
            {
                return res[0];
            }
        }

        /// <summary>
        /// 获取字符串的首个单词，如果首字母是大写，则返回帕斯卡命名的首个单词，否则返回驼峰命名的首个单词
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetPascalCaseFirstWord(this string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (str.Length == 1)
            {
                return str;
            }

            var res = Regex.Split(str, @"(?=\p{Lu}\p{Ll})|(?<=\p{Ll})(?=\p{Lu})");

            if (res.Length < 2)
            {
                return str;
            }
            else
            {
                return res[1];
            }
        }

        /// <summary>
        /// 获取字符串的首个单词，如果首字母是大写，则返回帕斯卡命名的首个单词，否则返回驼峰命名的首个单词
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetPascalOrCamelCaseFirstWord(this string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (str.Length <= 1)
            {
                return str;
            }

            if (str[0] >= 65 && str[0] <= 90)
            {
                return GetPascalCaseFirstWord(str);
            }
            else
            {
                return GetCamelCaseFirstWord(str);
            }
        }

        /// <summary>
        /// 将字符串的首字母转换为小写
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FirstCharToLower(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            string str = s.First().ToString().ToLower() + s.Substring(1);
            return str;
        }

        /// <summary>
        /// 将字符串的首字母转换为大写
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FirstCharToUpper(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            string str = s.First().ToString().ToUpper() + s.Substring(1);
            return str;
        }

        /// <summary>
        /// 将字符串转换为帕斯卡命名
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToPascal(this string str)
        {
            string[] split = str.Split(new char[] { '/', ' ', '_', '.', '-' });
            string newStr = "";
            foreach (var item in split)
            {
                char[] chars = item.ToCharArray();
                chars[0] = char.ToUpper(chars[0]);
                for (int i = 1; i < chars.Length; i++)
                {
                    chars[i] = char.ToLower(chars[i]);
                }
                newStr += new string(chars);
            }
            return newStr;
        }

        /// <summary>
        /// 将字符串转换为驼峰命名
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToCamel(this string str) => str.ToPascal().FirstCharToLower();


        /// <summary>
        /// 将字符串转换为日期时间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string value) => DateTime.TryParse(value, out var result) ? result : default;

        /// <summary>
        /// 将字符串转换为短整型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static short ToInt16(this string value) => short.TryParse(value, out var result) ? result : default;

        /// <summary>
        /// 将字符串转换为整型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt32(this string value) => int.TryParse(value, out var result) ? result : default;

        /// <summary>
        /// 将字符串转换为长整型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ToInt64(this string value) => long.TryParse(value, out var result) ? result : default;

        /// <summary>
        /// 将字符串转换为布尔值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string value) => bool.TryParse(value, out var result) ? result : default;

        /// <summary>
        /// 将字符串转换为单精度浮点数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float ToFloat(this string value) => float.TryParse(value, out var result) ? result : default;

        /// <summary>
        /// 将字符串转换为双精度浮点数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string value) => decimal.TryParse(value, out var result) ? result : default;

        /// <summary>
        /// 将字符串转换为双精度浮点数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToDouble(this string value) => double.TryParse(value, out var result) ? result : default;

        /// <summary>
        /// 判断字符串是否为数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumber(this string value) => Regex.IsMatch(value, @"^\d+$");

        /// <summary>
        /// 判断字符串是否为整数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsWholeNumber(this string value) => long.TryParse(value, out _);

        /// <summary>
        /// 判断字符串是否为小数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDecimalNumber(this string value) => decimal.TryParse(value, out _);

        /// <summary>
        /// 判断字符串是否为布尔值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsBoolean(this string value) => bool.TryParse(value, out var _);

        /// <summary>
        /// 判断字符串是否是有效的Json字符串
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsValidJson(this string text)
        {
            text = text.Trim();
            if ((text.StartsWith("{") && text.EndsWith("}")) || //For object
                (text.StartsWith("[") && text.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(text);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据给定的最大编号获取一个新的编号
        /// </summary>
        /// <param name="maxCode"></param>
        /// <param name="length"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string GetNewCode(this string maxCode, int length = 5, string prefix = null)
        {
            if (!string.IsNullOrEmpty(maxCode) && prefix != null)
            {
                maxCode = maxCode.Substring(prefix.Length);
            }

            if (string.IsNullOrEmpty(maxCode))
            {
                if (prefix != null)
                {
                    return prefix + "1".PadLeft(length - prefix.Length, '0');
                }

                return "1".PadLeft(length, '0');
            }

            length -= prefix?.Length ?? 0;
            char[] array = maxCode.ToCharArray();
            string text = string.Empty;
            int num = 1;
            for (int num2 = array.Length - 1; num2 >= 0; num2--)
            {
                int num3 = codeValue.IndexOf(maxCode[num2]) + num;
                if (num3 == codeValue.Length)
                {
                    num = 1;
                    text = "0" + text;
                }
                else
                {
                    text = codeValue[(num3 >= 0) ? num3 : 0] + text;
                    num = 0;
                }
            }

            return ((prefix == null) ? "" : prefix) + text.PadLeft(length, '0');
        }
    }
}
