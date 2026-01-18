namespace PrettyLove.Core
{
    /// <summary>
    /// Object扩展类
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 从另一个对象中复制数据(属性的值)到本对象，不会复制子表(属性不能写、框架实体关键字段、数据类型不一致跳过)
        /// </summary>
        /// <param name="a">数据目标对象</param>
        /// <param name="b">数据来源对象(不含列表属性)</param>
        /// <param name="skipFileds">要跳过的字段</param>
        public static void CopyFrom(this object a, object b, params string[] skipFileds)
        {
            if (b == null) return;
            var aProperties = a.GetType().GetProperties().Select(p => p);
            if (skipFileds != null && skipFileds.Length > 0)
            {
                aProperties = aProperties.Where(p => !skipFileds.Any(t => t.Equals(p.Name, StringComparison.OrdinalIgnoreCase)));
            }
            var bProperties = b.GetType().GetProperties().Select(p => p);
            var ignoreProperties = new List<string>();
            foreach (var pro in aProperties)
            {
                //属性不能读跳过
                if (!pro.CanWrite) continue;
                if (ignoreProperties.Any(p => pro.Name.Equals(p, StringComparison.OrdinalIgnoreCase))) continue;
                var bp = bProperties.FirstOrDefault(p => p.Name.Equals(pro.Name, StringComparison.OrdinalIgnoreCase) && p.PropertyType == pro.PropertyType);
                if (bp != null)
                {
                    var val = bp.GetValue(b, null);
                    pro.SetValue(a, val, null);
                }
            }
        }

        /// <summary>
        /// 比较(两值或对象是否相同)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsEquals<T>(this T obj, T target)
        {
            if (obj == null)
            {
                if (target == null) return false;
                else return false;
            }
            return EqualityComparer<T>.Default.Equals(obj, target);
        }

        /// <summary>
        /// 对象条件判断执行 消除IF  else  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static ConditionContext<T> Condition<T>(this T obj, Func<T, bool> condition)
        {
            return new ConditionContext<T>(obj, condition);
        }
    }

    public class ConditionContext<T>
    {
        private readonly T _obj;
        private readonly Func<T, bool> _condition;

        public ConditionContext(T obj, Func<T, bool> condition)
        {
            _obj = obj;
            _condition = condition;
        }

        /// <summary>
        /// 如果条件成立 执行这个方法
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public ConditionContext<T> Execute(Action<T> action)
        {
            if (_condition(_obj))
            {
                action(_obj);
            }
            return this; // 返回当前上下文对象，支持链式调用
        }

        /// <summary>
        /// 如果条件不成立 执行这个方法
        /// </summary>
        /// <param name="elseAction"></param>
        /// <returns></returns>
        public ConditionContext<T> Else(Action<T> elseAction)
        {
            if (!_condition(_obj))
            {
                elseAction(_obj);
            }
            return this; // 返回当前上下文对象，支持链式调用
        }
    }
}
