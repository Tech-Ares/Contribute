using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KUX.Infrastructure.TextJson
{
    /// <summary>
    /// Long 类型Json 序列化重写
    /// 在js中传输回导致精度丢失，需要转换成字符串类型
    /// </summary>
    public class LongJsonConverter : JsonConverter<long>
    {
        /// <summary>
        /// 读取json
        /// </summary>
        /// <param name="reader">json Reader</param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (long.TryParse(reader.GetString(), out long data))
                {
                    return data;
                }
                return 0;
            }
            return reader.GetInt64();
        }

        /// <summary>
        /// 写json
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}