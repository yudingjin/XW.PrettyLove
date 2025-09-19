using CSRedis;

namespace LS.ERP.Infrastructure
{
    public abstract class RedisUtil
    {
        #region 变量与构造

        /// <summary>
        /// redis 客户端
        /// </summary>
        protected CSRedisClient redisClient;

        /// <summary>
        /// 抽象方法
        /// </summary>
        public abstract void Init();

        #endregion

        #region Key

        /// <summary>
        /// 搜索key
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public string[] Keys(string pattern)
        {
            return redisClient.Keys(pattern);
        }

        /// <summary>
        /// 搜索key异步方法
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public async Task<string[]> KeysAsync(string pattern)
        {
            return await redisClient.KeysAsync(pattern);
        }

        /// <summary>
        /// 判断是否存在key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            return redisClient.Exists(key);
        }

        /// <summary>
        /// 判断是否存在key异步方法
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<bool> ExistsAsync(string key)
        {
            return redisClient.ExistsAsync(key);
        }

        /// <summary>
        /// 删除key
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public long Del(params string[] keys)
        {
            return redisClient.Del(keys);
        }

        /// <summary>
        /// 删除key
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<long> DelAsync(params string[] keys)
        {
            return await redisClient.DelAsync(keys);
        }

        /// <summary>
        /// 设置key的有效时长
        /// </summary>
        /// <param name="key"></param>
        /// <param name="exipresSec"></param>
        /// <returns></returns>
        public bool Expire(string key, int exipresSec = -1)
        {
            return redisClient.Expire(key, exipresSec);
        }

        /// <summary>
        /// 设置key的有效时长
        /// </summary>
        /// <param name="key"></param>
        /// <param name="exipresSec"></param>
        /// <returns></returns>
        public Task<bool> ExpireAsync(string key, int exipresSec = -1)
        {
            return redisClient.ExpireAsync(key, exipresSec);
        }

        /// <summary>
        /// 设置key的有效时长
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public bool ExpireAt(string key, DateTime dateTime)
        {
            return redisClient.ExpireAt(key, dateTime);
        }

        /// <summary>
        /// 设置key的有效时长异步方法
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public Task<bool> ExpireAtAsync(string key, DateTime dateTime)
        {
            return redisClient.ExpireAtAsync(key, dateTime);
        }

        /// <summary>
        /// 清空db中的key
        /// </summary>
        public void RemoveAll()
        {
            var keys = Keys("*");
            if (keys != null && keys.Count() > 0)
                redisClient.Del(keys.ToArray());
        }

        /// <summary>
        /// 清空db中的key
        /// </summary>
        /// <returns></returns>
        public async Task RemoveAllAsync()
        {
            var keys = Keys("*");
            if (keys != null && keys.Count() > 0)
                await redisClient.DelAsync(keys.ToArray());
        }

        /// <summary>
        /// 指定key对应的值自增或自减
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public long IncrBy(string key, long value = 1)
        {
            return redisClient.IncrBy(key, value);
        }

        /// <summary>
        /// 指定key对应的值自增或自减
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public Task<long> IncrByAsync(string key, long value = 1)
        {
            return redisClient.IncrByAsync(key, value);
        }

        /// <summary>
        /// 指定key对应的值自增或自减
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public decimal IncrByFloat(string key, decimal value = 1)
        {
            return redisClient.IncrByFloat(key, value);
        }

        /// <summary>
        /// 指定key对应的值自增或自减
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public Task<decimal> IncrByFloatAsync(string key, decimal value = 1)
        {
            return redisClient.IncrByFloatAsync(key, value);
        }

        #endregion

        #region String

        /// <summary>
        /// 获取key值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            return redisClient.Get<T>(key);
        }

        /// <summary>
        /// 异步获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<T> GetAsync<T>(string key)
        {
            return redisClient.GetAsync<T>(key);
        }

        /// <summary>
        /// 批量获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        public IEnumerable<T> GetList<T>(IEnumerable<string> keys)
        {
            return redisClient.MGet<T>(keys.ToArray()).ToList();
        }

        /// <summary>
        /// 异步批量获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<T[]> GetListAsync<T>(IEnumerable<string> keys)
        {
            return await redisClient.MGetAsync<T>(keys.ToArray());
        }

        /// <summary>
        /// 单个存储
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="expiresSec"></param>
        public bool Set<T>(string key, T t, int expiresSec = -1)
        {
            return redisClient.Set(key, t, expiresSec);
        }

        /// <summary>
        /// 异步存储
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="expireSeconds"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync<T>(string key, T t, int expireSeconds = -1)
        {
            return await redisClient.SetAsync(key, t, expireSeconds);
        }

        /// <summary>
        /// 异步存储
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync<T>(string key, T t, TimeSpan expire)
        {
            return await redisClient.SetAsync(key, t, expire);
        }

        /// <summary>
        /// SetNx
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetNx(string key, object value)
        {
            return redisClient.SetNx(key, value);
        }

        /// <summary>
        /// SetNx异步方法
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> SetNxAsync(string key, object value)
        {
            return await redisClient.SetNxAsync(key, value);
        }

        /// <summary>
        /// 批量存储
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public bool MSet<T>(IDictionary<string, T> keyValues)
        {
            return redisClient.MSet(keyValues);
        }

        /// <summary>
        /// 批量存储
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public async Task<bool> MSetAsync<T>(IDictionary<string, T> keyValues)
        {
            return await redisClient.MSetAsync(keyValues);
        }

        /// <summary>
        /// 批量存储
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public bool MSetNx<T>(IDictionary<string, T> keyValues)
        {
            return redisClient.MSetNx(keyValues);
        }

        /// <summary>
        /// 批量存储
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public async Task<bool> MSetNxAsync<T>(IDictionary<string, T> keyValues)
        {
            return await redisClient.MSetNxAsync(keyValues);
        }

        #endregion

        #region Hash

        /// <summary>
        /// 设置单个field-value (域-值)
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool HashSet(string hashId, string field, object value)
        {
            return redisClient.HSet(hashId, field, value);
        }

        /// <summary>
        /// 设置单个field-value (域-值)
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> HashSetAsync(string hashId, string field, object value)
        {
            return await redisClient.HSetAsync(hashId, field, value);
        }

        /// <summary>
        /// 设置单个field-value (域-值)
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool HashSetNx(string hashId, string field, object value)
        {
            return redisClient.HSetNx(hashId, field, value);
        }

        /// <summary>
        /// 设置单个field-value (域-值)
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> HashSetNxAsync(string hashId, string field, object value)
        {
            return await redisClient.HSetNxAsync(hashId, field, value);
        }

        /// <summary>
        /// 同时将多个 field-value (域-值)对设置到哈希表中
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="fieldValues"></param>
        /// <returns></returns>
        public bool HashMSet(string hashId, params object[] fieldValues)
        {
            return redisClient.HMSet(hashId, fieldValues);
        }

        /// <summary>
        /// 同时将多个 field-value (域-值)对设置到哈希表中
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="fieldValues"></param>
        /// <returns></returns>
        public async Task<bool> HashMSetAsync(string hashId, params object[] fieldValues)
        {
            return await redisClient.HMSetAsync(hashId, fieldValues);
        }

        /// <summary>
        /// 获取在哈希表中指定 key 的所有字段和值
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetAllEntriesFromHash(string hashId)
        {
            return redisClient.HGetAll(hashId);
        }

        /// <summary>
        /// 获取存储在哈希表中多个字段的值
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public IEnumerable<string> GetValuesFromHash(string hashId, params string[] keys)
        {
            return redisClient.HMGet(hashId, keys);
        }

        /// <summary>
        /// 获取存储在哈希表中多个字段的值
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetValuesFromHashAsync(string hashId, params string[] keys)
        {
            return await redisClient.HMGetAsync(hashId, keys);
        }

        /// <summary>
        /// 删除一个或多个哈希表字段
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public bool HDel(string hashId, params string[] fields)
        {
            return redisClient.HDel(hashId, fields) > 0;
        }

        /// <summary>
        /// 删除一个或多个哈希表字段
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public async Task<bool> HDelAsync(string hashId, params string[] fields)
        {
            return await redisClient.HDelAsync(hashId, fields) > 0;
        }

        #endregion

        #region Set

        /// <summary>
        /// 向集合中添加元素
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="items"></param>
        public void AddItemsToSet<T>(string setId, params T[] items)
        {
            redisClient.SAdd<T>(setId, items);
        }

        /// <summary>
        /// 向集合中添加元素
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<long> AddItemsToSetAsync<T>(string setId, params T[] items)
        {
            return await redisClient.SAddAsync<T>(setId, items);
        }

        /// <summary>
        /// 从Set中移除元素
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="item"></param>
        public long RemoveItemsFromSet<T>(string setId, params T[] items)
        {
            return redisClient.SRem<T>(setId, items);
        }

        /// <summary>
        /// 从Set中移除单个元素
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="item"></param>
        public async Task<long> RemoveItemsFromSetAsync<T>(string setId, T item)
        {
            return await redisClient.SRemAsync<T>(setId, item);
        }

        /// <summary>
        /// 获取集合元素数量
        /// </summary>
        /// <param name="setId"></param>
        /// <returns></returns>
        public long GetSetCount(string setId)
        {
            return redisClient.SCard(setId);
        }

        /// <summary>
        /// 获取集合中的所有元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="setId"></param>
        /// <returns></returns>
        public T[] GetAllItemsFromSet<T>(string setId)
        {
            return redisClient.SMembers<T>(setId);
        }

        /// <summary>
        /// 获取集合中的所有元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="setId"></param>
        /// <returns></returns>
        public async Task<T[]> GetAllItemsFromSetAsync<T>(string setId)
        {
            return await redisClient.SMembersAsync<T>(setId);
        }

        #endregion

        #region Sorted Set

        /// <summary>
        /// 获取有序集合长度
        /// </summary>
        /// <param name="sortedSetId"></param>
        /// <returns></returns>
        public long GetSortedSetCount(string sortedSetId)
        {
            return redisClient.ZCard(sortedSetId);
        }

        /// <summary>
        /// 向有序集合中添加元素
        /// </summary>
        /// <param name="sortedSetId">有序集合ID</param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public bool AddItemToSortedSet(string sortedSetId, string value, decimal score)
        {
            return redisClient.ZAdd(sortedSetId, (score, value)) > 0;
        }

        /// <summary>
        /// 向有序集中中批量添加元素
        /// </summary>
        /// <param name="sortedSetId">有序集合ID</param>
        /// <param name="scoreMembers">元素</param>
        /// <returns></returns>
        public async Task<long> AddRangeToSortedSet(string sortedSetId, params (decimal, object)[] scoreMembers)
        {
            return await redisClient.ZAddAsync(sortedSetId, scoreMembers);
        }

        /// <summary>
        /// 从有序集合中移除元素
        /// </summary>
        /// <param name="sortedSetId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool RemoveItemFromSortedSet(string sortedSetId, string value)
        {
            return redisClient.ZRem(sortedSetId, value) > 0;
        }

        /// <summary>
        /// 从有序集合中移除多个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sortedSetId"></param>
        /// <param name="members"></param>
        /// <returns></returns>
        public async Task<long> RemoveRangeFromSortedSet<T>(string sortedSetId, params T[] members)
        {
            return await redisClient.ZRemAsync<T>(sortedSetId, members);
        }

        #endregion 05 有序集合

        #region List

        /// <summary>
        /// 同步方法
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long PrependItemsToList<T>(string listId, params T[] values)
        {
            return redisClient.LPush<T>(listId, values);
        }

        /// <summary>
        /// 异步方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listId"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public async Task<long> PrependItemsToListAsync<T>(string listId, params T[] values)
        {
            return await redisClient.LPushAsync<T>(listId, values);
        }

        /// <summary>
        /// 尾部移除
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        public string PopItemFromList(string listId)
        {
            return redisClient.RPop(listId);
        }

        /// <summary>
        /// 尾部移除异步方法
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        public async Task<string> PopItemFromListAsync(string listId)
        {
            return await redisClient.RPopAsync(listId);
        }

        /// <summary>
        /// 从链表的顶部移除元素，并返回移除的元素信息
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        public async Task<string> RemoveStartFromListAsync(string listId)
        {
            return await redisClient.LPopAsync(listId);
        }

        /// <summary>
        /// 同步方法
        /// 根据ListId，获取内置的List的项数
        /// </summary>
        /// <param name="listId"></param>
        public long GetListCount(string listId)
        {
            return redisClient.LLen(listId);
        }

        /// <summary>
        /// 异步方法
        /// 根据ListId，获取内置的List的项数
        /// </summary>
        /// <param name="listId"></param>
        public async Task<long> GetListCountAsync(string listId)
        {
            return await redisClient.LLenAsync(listId);
        }

        /// <summary>
        /// 同步方法
        /// 删除元素
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="value"></param>
        public long RemoveItemFromList(string listId, object value)
        {
            return redisClient.LRem(listId, 0, value);
        }

        /// <summary>
        /// 异步方法
        /// 删除元素
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="value"></param>
        public async Task<long> RemoveItemFromListAsync(string listId, object value)
        {
            return await redisClient.LRemAsync(listId, 0, value);
        }

        /// <summary>
        /// 获取链表中的全部元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listId"></param>
        /// <returns></returns>
        public IEnumerable<string> GetAllItemsFromList(string listId)
        {
            var multiDataList = redisClient.LRange(listId, 0, -1);
            return multiDataList;
        }

        /// <summary>
        /// 获取链表中的全部元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listId"></param>
        /// <returns></returns>
        public async Task<string[]> GetAllItemsFromListAsync(string listId)
        {
            var multiDataList = await redisClient.LRangeAsync(listId, 0, -1);
            return multiDataList;
        }

        /// <summary>
        /// 按照起始位置查找
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="startingFrom"></param>
        /// <param name="endingAt"></param>
        /// <returns></returns>

        public IEnumerable<T> GetRangeFromList<T>(string listId, long startingFrom, long endingAt)
        {
            var multiDataList = redisClient.LRange<T>(listId, startingFrom, endingAt);
            return multiDataList;
        }

        /// <summary>
        /// 按照起始位置查找
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="startingFrom"></param>
        /// <param name="endingAt"></param>
        /// <returns></returns>

        public async Task<T[]> GetRangeFromListAsync<T>(string listId, long startingFrom, long endingAt)
        {
            var multiDataList = await redisClient.LRangeAsync<T>(listId, startingFrom, endingAt);
            return multiDataList;
        }

        #endregion

        #region 分布式锁

        /// <summary>
        /// 加锁毫秒级
        /// </summary>
        /// <param name="key">锁key</param>
        /// <param name="value">锁值</param>
        /// <param name="expireMilliSeconds">缓存时间  单位/毫秒   默认1000毫秒</param>
        /// <returns></returns>
        public bool RedisLock(string key, object value, int expireMilliSeconds = 1000)
        {
            var script = @"local isNX = redis.call('SETNX',KEYS[1],ARGV[1])
                           if isNX == 1 then 
                                redis.call('EXPIRE',KEYS[1],ARGV[2])
                                return 1
                           end
                           return 0";
            return redisClient.Eval(script, key, value, expireMilliSeconds)?.ToString() == "1";
        }

        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="selfMark"></param>
        /// <returns></returns>
        public bool RedisUnlock(string key, string selfMark)
        {
            var script = @"local getLock = redis.call('GET',KEYS[1])
                           if getLock == ARGV[1] then
                                redis.call('DEL',KEYS[1])
                                return 1
                           end               
                           return 0";
            return redisClient.Eval(script, key, selfMark)?.ToString() == "1";
        }

        #endregion

        #region 发布订阅

        /// <summary>
        /// 普通订阅消息 允许多端收到消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="onMessage"></param>
        public void Subscribe(string channel, Action<string> onMessage)
        {
            redisClient.Subscribe((channel, data => onMessage?.Invoke(data.Body)));
        }

        /// <summary>
        /// 发布消息 同步
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        public void Publish(string channel, string message)
        {
            redisClient.Publish(channel, message);
        }

        /// <summary>
        /// 发布消息 异步
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task PublishAsync(string channel, string message)
        {
            await redisClient.PublishAsync(channel, message);
        }

        #endregion
    }
}
