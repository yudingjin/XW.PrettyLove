using FreeSql;
using FreeSql.DataAnnotations;
using LS.ERP.Domain.Shared;
using System.Reflection;

namespace LS.ERP.Infrastructure
{
    public static class FreeSqlExtensions
    {
        /// <summary>
        /// 全局过滤器
        /// </summary>
        /// <param name="freeSql"></param>
        /// <returns></returns>
        public static IFreeSql AddGobalFilter(this IFreeSql freeSql)
        {
            freeSql.GlobalFilter.Apply<ISoftDelete>("condition1", p => p.IsDeleted == false);
            //.ApplyIf<ITenant<long>>("condition2", () => Principal.User != null, p => p.TenantId == Principal.User.TenantId);
            return freeSql;
        }

        /// <summary>
        /// 实体拦截 统一处理表名
        /// </summary>
        /// <param name="freeSql"></param>
        /// <returns></returns>
        public static IFreeSql AddConfigEntity(this IFreeSql freeSql)
        {
            freeSql.Aop.ConfigEntity += (s, e) =>
            {
                var attribute = e.EntityType.GetCustomAttribute<TableAttribute>(true);
                if (attribute != null)
                {
                    var tableAttribute = attribute as TableAttribute;
                    e.ModifyResult.Name = tableAttribute.Name;
                }
                else
                {
                    var name = $"{e.EntityType.Name.Replace("Entity", "").ToLower()}s";
                }
            };
            return freeSql;
        }

        /// <summary>
        /// 实体属性拦截  统一处理字段名
        /// </summary>
        /// <param name="freeSql"></param>
        public static IFreeSql AddConfigEntityProperty(this IFreeSql freeSql)
        {
            freeSql.Aop.ConfigEntityProperty += (s, e) =>
            {
                var enumCandidiate = Nullable.GetUnderlyingType(e.Property.PropertyType) ?? e.Property.PropertyType;
                //枚举类型特殊处理
                if (enumCandidiate.IsEnum)
                {
                    e.ModifyResult.MapType = typeof(short?);
                }

                //因为默认 decimal 只支持 decimal(10,2)，范围太小，我们可以全局修改 decimal 类型的支持范围，比如支持 decimal(18,6)
                if (e.Property.PropertyType == typeof(decimal) || e.Property.PropertyType == typeof(decimal?))
                {
                    e.ModifyResult.Precision = 18;
                    e.ModifyResult.Scale = 2;
                }
                else if (e.Property.PropertyType == typeof(Guid))
                {
                    switch (freeSql.Ado.DataType)
                    {
                        case DataType.MySql:
                        case DataType.OdbcMySql:
                        case DataType.CustomMySql:
                            e.ModifyResult.DbType = "char(36)";
                            break;
                        case DataType.Oracle:
                        case DataType.OdbcOracle:
                        case DataType.CustomOracle:
                            e.ModifyResult.DbType = "char(36 CHAR)";
                            break;
                        case DataType.PostgreSQL:
                        case DataType.CustomPostgreSQL:
                        case DataType.OdbcPostgreSQL:
                            e.ModifyResult.DbType = "uuid";
                            break;
                        case DataType.Sqlite:
                            e.ModifyResult.DbType = "character(36)";
                            break;
                        case DataType.Dameng:
                            e.ModifyResult.DbType = "char(36)";
                            break;
                        default:
                            break;
                    }
                }
            };
            return freeSql;
        }

        /// <summary>
        /// 表达式拦截
        /// </summary>
        /// <param name="freeSql"></param>
        /// <returns></returns>
        public static IFreeSql AddParseExpression(this IFreeSql freeSql)
        {
            freeSql.Aop.ParseExpression += (s, e) =>
            {

            };
            return freeSql;
        }

        /// <summary>
        /// 监视SQL
        /// </summary>
        /// <param name="freeSql"></param>
        public static IFreeSql AddCurdBefore(this IFreeSql freeSql)
        {
            freeSql.Aop.CurdBefore += (s, e) =>
            {
                if (e.CurdType == FreeSql.Aop.CurdType.Update)
                {

                }
            };
            return freeSql;
        }

        /// <summary>
        /// 监视SQL
        /// </summary>
        /// <param name="freeSql"></param>
        public static IFreeSql AddCurdAfter(this IFreeSql freeSql)
        {
            freeSql.Aop.CurdAfter += (s, e) =>
            {

            };
            return freeSql;
        }

        /// <summary>
        /// ADO.NET读取拦截
        /// </summary>
        /// <param name="freeSql"></param>
        /// <returns></returns>
        public static IFreeSql AddAuditDataReader(this IFreeSql freeSql)
        {
            freeSql.Aop.AuditDataReader += (s, e) =>
            {

            };
            return freeSql;
        }
    }
}
