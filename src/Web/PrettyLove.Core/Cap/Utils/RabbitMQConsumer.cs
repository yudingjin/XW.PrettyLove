using DotNetCore.CAP;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace PrettyLove.Core
{
    /// <summary>
    /// 消费者
    /// </summary>
    public static class RabbitMQConsumer
    {
        #region 01 工作队列

        /// <summary>
        /// 工作队列 消费者
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="action"></param>
        public static async Task ReceiveSimple(string queueName, Action<string> action)
        {
            //1.建立连接
            var connection = await RabbitMQHelper.GetConnectionAsync();

            //2.创建channel和队列
            var channel = await connection.CreateChannelAsync();
            //创建队列【如果先启动的是消费端，会有异常发生】
            await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            //3.接收消息
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                //获取消息
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                var routingKey = ea.RoutingKey;
                action?.Invoke(message);

                //自动确认机制有缺陷：消息是否正常添加到数据库中，所以需要使用手工确认
                await channel.BasicAckAsync(ea.DeliveryTag, true);
            };

            //Qos（防止多个消费者，能力不一致，导致任务堆积；这样设置  每一次一个消费者只成功消费一个）
            //Qos：谁强谁处理的就多
            await channel.BasicQosAsync(0, 1, false);

            //消息确认（防止消息重新消费）
            await channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer);
        }

        #endregion

        #region 02 订阅发布(广播消费) 扇形交换机

        /// <summary>
        /// 扇形交换机 消费者
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="action"></param>
        public static async Task ReceiveFanout(string exchange, Action<string> action)
        {
            var connection = await RabbitMQHelper.GetConnectionAsync();
            var channel = await connection.CreateChannelAsync();
            await channel.ExchangeDeclareAsync(exchange: exchange, type: "fanout");

            var queue = await channel.QueueDeclareAsync();
            await channel.QueueBindAsync(queue: queue.QueueName, exchange: exchange, routingKey: "");
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                action?.Invoke(message);
                await channel.BasicAckAsync(ea.DeliveryTag, true);
            };
            await channel.BasicQosAsync(0, 1, false);
            await channel.BasicConsumeAsync(queue: queue.QueueName, autoAck: false, consumer: consumer);
        }

        #endregion

        #region 03 直连交换机

        /// <summary>
        /// 直连交换机 消费者
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="routingKey"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task ReceiveDirect(string exchange, string routingKey, Action<string> action)
        {
            var connection = await RabbitMQHelper.GetConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            // 1、定义交换机
            await channel.ExchangeDeclareAsync(exchange: exchange, type: ExchangeType.Direct);

            // 2、定义随机队
            var queue = await channel.QueueDeclareAsync();

            // 3、队列要和交换机绑定起来
            await channel.QueueBindAsync(queue.QueueName, exchange, routingKey: routingKey);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body;
                var key = ea.RoutingKey;
                var message = Encoding.UTF8.GetString(body.ToArray());
                action?.Invoke(message);
                await channel.BasicAckAsync(ea.DeliveryTag, true);
            };
            // 4、消费消息
            await channel.BasicQosAsync(0, 1, false);
            await channel.BasicConsumeAsync(queue: queue.QueueName, autoAck: false, consumer: consumer);
        }

        #endregion

        #region 04 主题交换机      

        /// <summary>
        /// 主题交换机 消费者
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="routingKey"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static async Task ReceiveTopic(string exchange, string routingKey, Func<string, bool?> func)
        {
            var connection = await RabbitMQHelper.GetConnectionAsync();
            var channel = await connection.CreateChannelAsync();
            // 1、定义交换机
            await channel.ExchangeDeclareAsync(exchange: exchange, type: ExchangeType.Topic);
            // 2、定义随机队列(用完之后立马删除)
            var queue = await channel.QueueDeclareAsync();
            // 3、队列要和交换机绑定起来(#：匹配一个词和多个词，*：匹配一个词。)
            await channel.QueueBindAsync(queue.QueueName, exchange, routingKey: routingKey);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                var flag = func?.Invoke(message);
                if (flag == true)
                    await channel.BasicAckAsync(ea.DeliveryTag, true);
            };
            // 4、消费消息
            await channel.BasicQosAsync(0, 1, false);
            await channel.BasicConsumeAsync(queue: queue.QueueName, autoAck: false, consumer: consumer);
        }

        /// <summary>
        /// 主题交换机 消费者
        /// </summary>
        /// <param name="options"></param>
        /// <param name="exchange"></param>
        /// <param name="queueName"></param>
        /// <param name="routingKey"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static async Task ReceiveTopic(RabbitMQOptions options, string exchange, string queueName, string routingKey, Func<string, bool?> func)
        {
            var connection = await RabbitMQHelper.GetConnectionAsync(options);
            var channel = await connection.CreateChannelAsync();
            // 1、定义交换机
            await channel.ExchangeDeclareAsync(exchange: exchange, type: ExchangeType.Topic, true);
            // 2、定义队列
            await channel.QueueDeclareAsync(queueName, true, false, false, null);
            // 3、队列要和交换机绑定起来
            await channel.QueueBindAsync(queueName, exchange, routingKey);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                var flag = func?.Invoke(message);
                if (flag == true)
                    await channel.BasicAckAsync(ea.DeliveryTag, true);
            };
            // 4、消费消息
            await channel.BasicQosAsync(0, 1, false);
            await channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer);
        }

        #endregion
    }
}
