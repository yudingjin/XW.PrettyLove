using Microsoft.Extensions.DependencyInjection;
using Yitter.IdGenerator;

namespace XW.PrettyLove.Core
{
    public static class SnowflakeIdServiceExtensions
    {
        /// <summary>
        /// 雪花算法注册
        /// </summary>
        /// <param name="services"></param>
        public static void AddSnowflakeId(this IServiceCollection services)
        {
            // 设置雪花Id的workerId，确保每个实例workerId都应不同
            var workerId = ushort.Parse(App.Configuration["SnowId:WorkerId"] ?? "1");
            //Random random = new Random();
            //var workerId = random.Next(1, 63);
            //Console.WriteLine($"当前服务实例的WorkerID是：{workerId}");
            YitIdHelper.SetIdGenerator(new IdGeneratorOptions { WorkerId = (ushort)workerId, SeqBitLength = 15 });
        }
    }
}
