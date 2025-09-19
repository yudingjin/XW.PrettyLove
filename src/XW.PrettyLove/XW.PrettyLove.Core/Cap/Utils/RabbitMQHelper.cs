using DotNetCore.CAP;
using RabbitMQ.Client;

namespace XW.PrettyLove.Core
{
    /// <summary>
    /// RabbitMQ连接帮助类
    /// </summary>
    public class RabbitMQHelper
    {
        /// <summary>
        /// 获取RabbitMQ非集群连接的方法
        /// </summary>
        /// <returns></returns>
        public static async Task<IConnection> GetConnectionAsync()
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "192.168.0.34",
                Port = 5672,
                UserName = "admin",
                Password = "admin@123",
                VirtualHost = "/"
            };
            return await connectionFactory.CreateConnectionAsync();
        }

        /// <summary>
        /// 获取RabbitMQ非集群连接的方法
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static async Task<IConnection> GetConnectionAsync(RabbitMQOptions config)
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = config.HostName,
                Port = config.Port,
                UserName = config.UserName,
                Password = config.Password,
                VirtualHost = config.VirtualHost
            };
            return await connectionFactory.CreateConnectionAsync();
        }

        /// <summary>
        /// 获取RabbitMQ集群连接的方法
        /// </summary>
        /// <returns></returns>
        public static async Task<IConnection> GetClusterConnectionAsync()
        {
            List<AmqpTcpEndpoint> endpoints = new List<AmqpTcpEndpoint>()
            {
                new AmqpTcpEndpoint() { HostName = "192.168.3.221", Port = 5672 },
                new AmqpTcpEndpoint() { HostName = "192.168.3.221", Port = 5673 },
                new AmqpTcpEndpoint() { HostName = "192.168.3.221", Port = 5674 }
            };
            var connectionFactory = new ConnectionFactory();
            return await connectionFactory.CreateConnectionAsync(endpoints);
        }

        /// <summary>
        /// 获取RabbitMQ集群连接的方法
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static async Task<IConnection> GetClusterConnectionAsync(List<RabbitMQOptions> list)
        {
            List<AmqpTcpEndpoint> endpoints = new List<AmqpTcpEndpoint>();
            foreach (RabbitMQOptions config in list)
            {
                var endpoint = new AmqpTcpEndpoint() { HostName = config.HostName, Port = config.Port };
                endpoints.Add(endpoint);
            }
            var connectionFactory = new ConnectionFactory();
            return await connectionFactory.CreateConnectionAsync(endpoints);
        }
    }
}
