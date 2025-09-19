using CSRedis;
using Microsoft.Extensions.Options;

namespace XW.PrettyLove.Core
{
    public class RedisInstance : RedisUtil
    {
        private readonly RedisOptions _options;
        public RedisInstance(IOptions<RedisOptions> options)
        {
            _options = options.Value;
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            redisClient = new CSRedisClient(_options.ToString());
            RedisHelper.Initialization(redisClient);
        }
    }
}
