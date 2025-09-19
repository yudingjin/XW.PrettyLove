﻿using LS.ERP.Domain.Shared;
using System.Collections.Concurrent;

namespace LS.ERP.Infrastructure
{
    /// <summary>
    /// 数据库表缓存
    /// </summary>
    public class DBCache
    {
        static ConcurrentDictionary<Type, DBEnum> cache = new ConcurrentDictionary<Type, DBEnum>();

        public static void Add(Type type, DBEnum dbEnum)
        {
            if (!cache.Keys.Contains(type))
                cache.TryAdd(type, dbEnum);
        }

        public static ConcurrentDictionary<Type, DBEnum> GetCache() => cache;

        public static DBEnum GetDb(Type type) => cache[type];
    }
}
