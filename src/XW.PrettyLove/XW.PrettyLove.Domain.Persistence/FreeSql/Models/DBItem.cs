﻿using FreeSql;
using LS.ERP.Domain.Shared;

namespace LS.ERP.Infrastructure
{
    /// <summary>
    /// 数据库配置项
    /// </summary>
    public class DBItem : IConfigurableOptions
    {
        public DBEnum DBEnum { get; set; }
        public DataType DataType { get; set; }
        public string MasterConnection { get; set; }
        public string[] SlaveConnectionList { get; set; }
    }
}
