using AspectCore.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace XW.PrettyLove.Core
{
    /// <summary>
    /// Redis缓存标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class RedisCacheAttribute : AbstractInterceptorAttribute
    {
        /// <summary>
        /// 参数匹配的类型
        /// </summary>
        static List<Type> typeList = new List<Type> { typeof(int), typeof(int?), typeof(long), typeof(long?), typeof(string) };

        /// <summary>
        /// 缓存键前缀（可选，默认使用方法名）
        /// </summary>
        public string KeyPrefix { get; set; } = "XW_";

        /// <summary>
        /// 数据类型
        /// </summary>
        public Type DataType { get; set; }

        /// <summary>
        /// 缓存过期时间（秒，默认3600秒）
        /// </summary>
        public int Expiration { get; set; } = 3600;

        /// <summary>
        /// 方法拦截
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            var redisInstance = context.ServiceProvider.GetRequiredService<RedisInstance>();
            var redisKey = GenerateCacheKey(context, KeyPrefix);
            // 从Redis读取缓存
            var cachedValue = await redisInstance.GetAsync<string>(redisKey);
            if (cachedValue == null)
            {
                await next(context);
                await redisInstance.SetAsync(redisKey, context.ReturnValue, this.Expiration);
            }
            else
            {
                context.ReturnValue = DeserializeJson(cachedValue, DataType);
            }
        }

        /// <summary>
        /// 生成缓存Key
        /// </summary>
        /// <param name="context"></param>
        /// <param name="keyPrefix"></param>
        /// <returns></returns>
        private string GenerateCacheKey(AspectContext context, string keyPrefix)
        {
            if (context.Parameters?.Length != 1)
                throw new Exception("参数不匹配");

            if (typeList.Contains(context.Parameters[0].GetType()))
            {
                if (context.Parameters[0] == null)
                    throw new Exception("参数值不能为空");
                return $"{keyPrefix}_{context.Parameters[0]}";
            }
            throw new Exception("参数类型不匹配");
        }

        /// <summary>
        /// 将字符串转成特定类型
        /// </summary>
        /// <param name="json"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        private object DeserializeJson(string json, Type targetType)
        {
            return JsonSerializer.Deserialize(json, targetType);
        }
    }
}
