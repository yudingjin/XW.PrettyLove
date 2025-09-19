using DotNetCore.CAP;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace LS.ERP.Infrastructure
{
    /// <summary>
    /// 生产者
    /// </summary>
    public static class RabbitMQProducer
    {
        #region 01 工作队列

        /// <summary>
        /// 01 简单队列 生产者
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="message"></param>
        public static async Task PublishSmple(string queueName, string message, int? priority = 10)
        {
            //1.建立连接
            using var connection = await RabbitMQHelper.GetConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            //设置队列优先级，取值范围在0~255之间。
            Dictionary<string, object> dict = new Dictionary<string, object>
            {
                { "x-max-priority", priority??10 }
            };
            //2.定义队列
            await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: dict);

            //3.发送消息
            var body = Encoding.UTF8.GetBytes(message);
            var properties = new BasicProperties();
            properties.Persistent = true;       //设置消息持久化
            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, mandatory: false, basicProperties: properties, body: body);

            //4.关闭连接
            await channel.CloseAsync();
            await connection.CloseAsync();
        }

        /// <summary>
        /// 01 简单队列 生产者
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queueName"></param>
        /// <param name="t"></param>
        public static async Task PublishSmple<T>(string queueName, T t) where T : class
        {
            //1.建立连接
            using var connection = await RabbitMQHelper.GetConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            //2.定义队列
            await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            //3.发送消息
            var json = JsonConvert.SerializeObject(t);
            var body = Encoding.UTF8.GetBytes(json);
            var properties = new BasicProperties();
            properties.Persistent = true;       //设置消息持久化
            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, mandatory: false, basicProperties: properties, body: body);

            //4.关闭连接
            await channel.CloseAsync();
            await connection.CloseAsync();
        }

        #endregion

        #region 02 订阅发布(广播消费) 扇形交换机  一个生产者的消息同时给多个消费者

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="message"></param>
        public static async Task PublishFanout(string exchange, string message)
        {
            using (var connection = await RabbitMQHelper.GetConnectionAsync())
            using (var channel = await connection.CreateChannelAsync())
            {
                await channel.ExchangeDeclareAsync(exchange: exchange, type: ExchangeType.Fanout);
                var body = Encoding.UTF8.GetBytes(message);
                var properties = new BasicProperties();
                properties.Persistent = true; // 设置消息持久化
                await channel.BasicPublishAsync(exchange: exchange, routingKey: string.Empty, mandatory: false, basicProperties: properties, body: body);
                await channel.CloseAsync();
                await connection.CloseAsync();
            }
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="options"></param>
        /// <param name="exchange"></param>
        /// <param name="message"></param>
        public static async Task PublishFanout(RabbitMQOptions options, string exchange, string message)
        {
            using (var connection = await RabbitMQHelper.GetConnectionAsync(options))
            using (var channel = await connection.CreateChannelAsync())
            {
                await channel.ExchangeDeclareAsync(exchange: exchange, type: ExchangeType.Fanout);
                var body = Encoding.UTF8.GetBytes(message);
                var properties = new BasicProperties();
                properties.Persistent = true; // 设置消息持久化
                await channel.BasicPublishAsync(exchange: exchange, routingKey: string.Empty, mandatory: false, basicProperties: properties, body: body);
                await channel.CloseAsync();
                await connection.CloseAsync();
            }
        }

        #endregion

        #region 03 直连交换机

        public static async Task PublishDirect(string exchange, string message, string routingKey)
        {
            //1、建立连接
            using var connection = await RabbitMQHelper.GetConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            //2、定义交换机
            await channel.ExchangeDeclareAsync(exchange: exchange, type: ExchangeType.Direct);

            //3、发送消息
            var body = Encoding.UTF8.GetBytes(message);
            var properties = new BasicProperties();
            properties.Persistent = true; // 设置消息持久化
            await channel.BasicPublishAsync(exchange: exchange, routingKey: routingKey, mandatory: false, basicProperties: properties, body: body);

            //4、关闭连接
            await channel.CloseAsync();
            await connection.CloseAsync();
        }

        #endregion

        #region 04 主题交换机

        public static async Task PublishTopic(string exchange, string message, string routingKey)
        {
            //1、建立连接
            using var connection = await RabbitMQHelper.GetConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            //2、定义交换机
            var queue = await channel.QueueDeclareAsync();
            await channel.QueueBindAsync(queue.QueueName, exchange, routingKey: routingKey);

            //3、发送消息
            var body = Encoding.UTF8.GetBytes(message);
            var properties = new BasicProperties();
            properties.Persistent = true; // 设置消息持久化
            await channel.BasicPublishAsync(exchange: exchange, routingKey: routingKey, mandatory: false, basicProperties: properties, body: body);

            //4、关闭连接
            await channel.CloseAsync();
            await connection.CloseAsync();
        }

        /// <summary>
        /// 指定队列
        /// </summary>
        /// <param name="options"></param>
        /// <param name="queueName"></param>
        /// <param name="exchange"></param>
        /// <param name="routingKey"></param>
        /// <param name="message"></param>
        public static async Task PublishTopic(RabbitMQOptions options, string exchange, string queueName, string routingKey, string message)
        {
            //1、建立连接
            using var connection = await RabbitMQHelper.GetConnectionAsync(options);
            using var channel = await connection.CreateChannelAsync();

            //2、定义交换机
            await channel.ExchangeDeclareAsync(exchange: exchange, type: ExchangeType.Topic, true);
            await channel.QueueDeclareAsync(queueName, true, false, false, null);
            await channel.QueueBindAsync(queueName, exchange, routingKey);

            //3、发送消息
            var body = Encoding.UTF8.GetBytes(message);
            var properties = new BasicProperties();
            properties.Persistent = true; // 设置消息持久化
            await channel.BasicPublishAsync(exchange: exchange, routingKey: routingKey, mandatory: false, basicProperties: properties, body: body);

            //4.关闭连接
            await channel.CloseAsync();
            await connection.CloseAsync();
        }

        #endregion
    }
}
