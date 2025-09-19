﻿using Microsoft.Extensions.DependencyInjection;

namespace LS.ERP.Infrastructure
{
    /// <summary>
    /// 以Scoped方式注册到容器中
    /// </summary>
    public class ScopedDependencyAttribute : DependencyAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ScopedDependencyAttribute() : base(ServiceLifetime.Scoped)
        {

        }
    }
}
