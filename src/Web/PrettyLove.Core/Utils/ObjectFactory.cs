using System.Linq.Expressions;

namespace PrettyLove.Core
{
    /// <summary>
    /// 对象工厂类
    /// </summary>
    public class ObjectFactory
    {
        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static object CreateInstance(string typeName)
        {
            return Activator.CreateInstance(Type.GetType(typeName));
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameterTypes"></param>
        /// <returns></returns>
        public static Func<object[], T> CreateInstanceWithArgs<T>(Type[] parameterTypes)
        {
            var constructor = typeof(T).GetConstructor(parameterTypes);
            var argsParam = Expression.Parameter(typeof(object[]), "args");
            var parameters = parameterTypes.Select((t, i) => Expression.Convert(Expression.ArrayIndex(argsParam, Expression.Constant(i)), t)).ToArray();

            var newExpr = Expression.New(constructor, parameters);
            var lambda = Expression.Lambda<Func<object[], T>>(newExpr, argsParam);
            return lambda.Compile();
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Func<T> CreateInstance<T>()
        {
            var constructor = typeof(T).GetConstructor(Type.EmptyTypes);
            var newExpr = Expression.New(constructor);
            var lambda = Expression.Lambda<Func<T>>(newExpr);
            return lambda.Compile();
        }
    }
}
