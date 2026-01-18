using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PrettyLove.Core
{
    /// <summary>
    /// Json扩展
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// 对象序列化成Json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj, Formatting formatting = Formatting.None)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            };
            return JsonConvert.SerializeObject(obj, formatting);
        }

        /// <summary>
        /// 对象序列化字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="formatting"></param>
        /// <returns></returns>
        public static string ToJsonCamelCase(this object obj, Formatting formatting = Formatting.None)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            };
            return JsonConvert.SerializeObject(obj, formatting, settings);
        }

        /// <summary>
        /// Json字符串序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }

        /// <summary>
        /// Json字符串序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string str, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<T>(str, settings);
        }

        /// <summary>
        /// Json字符串序列化成对象
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ToObject(this string str, Type type)
        {
            return JsonConvert.DeserializeObject(str, type);
        }

        /// <summary>
        /// Json字符串格式化
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FormatJson(this string str)
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            TextReader reader = new StringReader(str);
            JsonTextReader reader2 = new JsonTextReader(reader);
            object obj = jsonSerializer.Deserialize(reader2);
            if (obj != null)
            {
                StringWriter stringWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(stringWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                jsonSerializer.Serialize(jsonWriter, obj);
                return stringWriter.ToString();
            }
            return str;
        }
    }
}
