using PrettyLove.Domain.Shared;

namespace PrettyLove.Core
{
    public class RedisOptions : IConfigurableOptions
    {
        /// <summary>
        /// 服务器
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 库
        /// </summary>
        public int Db { get; set; } = 0;

        /// <summary>
        /// 连接池数量
        /// </summary>
        public int PoolSize { get; set; } = 50;

        /// <summary>
        /// 缓冲区
        /// </summary>
        public int WriteBuffer { get; set; } = 10240;

        /// <summary>
        /// 是否启用SSL
        /// </summary>
        public bool SSL { get; set; }

        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { get; set; } = string.Empty;

        /// <summary>
        /// 集群
        /// </summary>
        public bool TestCluster { get; set; } = false;

        /// <summary>
        /// 返回连接字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (!this.TestCluster)
                return $"{this.Host}:{this.Port},password={this.Password},defaultDatabase = {this.Db},poolsize = {this.PoolSize},ssl = {this.SSL},writeBuffer = {this.WriteBuffer},prefix={this.Prefix}";
            else
                return $"{this.Host}:{this.Port},password={this.Password},defaultDatabase = {this.Db},poolsize = {this.PoolSize},ssl = {this.SSL},writeBuffer = {this.WriteBuffer},testcluster=true";
        }
    }
}
