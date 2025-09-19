using Newtonsoft.Json;

namespace LS.ERP.Infrastructure
{
    /// <summary>
    /// Long类型转成String
    /// </summary>
    public class LongJsonConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // 从字符串反序列化为long
            if (reader.Value == null)
            {
                if (objectType == typeof(long?))
                    return null;
                return 0L;
            }
            if (long.TryParse(reader.Value.ToString(), out long result))
            {
                return result;
            }
            return existingValue;
        }

        public override bool CanConvert(Type objectType)
        {
            // 处理long和long?类型
            return objectType == typeof(long) || objectType == typeof(long?);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // 序列化时，将 long 转为字符串
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteValue(value.ToString());
        }
    }
}
