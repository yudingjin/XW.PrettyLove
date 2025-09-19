using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LS.ERP.Infrastructure
{
    public static class CollectionsExtensions
    {
        /// <summary>
        /// 判断集合是否为空或者为null
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<TSource>(this ICollection<TSource>? source)
        {
            return source == null || source.Count <= 0;
        }

        /// <summary>
        /// 获取与指定的键相关联的值。
        /// </summary>
        /// <typeparam name="TKey">字典中的键的类型。</typeparam>
        /// <typeparam name="TValue">字典中的值的类型。</typeparam>
        /// <param name="dict">字典。</param>
        /// <param name="key">字典的键。</param>
        /// <returns>如果字典包含具有指定键的元素则返回对应的值，否则返回默认值。</returns>
        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            if (dict is null) 
                throw new ArgumentNullException(nameof(dict));
            return dict.TryGetValue(key, out var value) ? value : default;
        }

        /// <summary>
        /// 把集合中的所有元素结合分隔符后放入一个字符串。
        /// </summary>
        /// <typeparam name="TSource">数据类型。</typeparam>
        /// <param name="source">原集合。</param>
        /// <param name="separator">分隔符。</param>
        /// <param name="start">起始文本。如果集合为空，不包含此数据。</param>
        /// <param name="end">结尾文本。如果集合为空，不包含此数据。</param>
        /// <param name="ignoreEmptyItem">指示是否忽略集合中为 null 或 <see cref="string.Empty"/> 值的项。</param>
        /// <returns>拼接后的字符串。</returns>
        public static string Join<TSource>(this IEnumerable<TSource> source, string separator = ",", string start = null, string end = null, bool ignoreEmptyItem = true)
            => Join(source, t => Convert.ToString(t), separator, start, end, ignoreEmptyItem);

        /// <summary>
        /// 把集合中的所有元素结合分隔符后放入一个字符串。
        /// </summary>
        /// <typeparam name="TSource">数据类型。</typeparam>
        /// <param name="source">原集合。</param>
        /// <param name="callback">回调方法。</param>
        /// <param name="separator">分隔符。</param>
        /// <param name="start">起始文本。如果集合为空，不包含此数据。</param>
        /// <param name="end">结尾文本。如果集合为空，不包含此数据。</param>
        /// <param name="ignoreEmptyItem">指示是否忽略集合中为 null 或 <see cref="string.Empty"/> 值的项。</param>
        /// <returns>拼接后的字符串。</returns>
        public static string Join<TSource>(this IEnumerable<TSource> source, Func<TSource, string> callback, string separator = ",", string start = null, string end = null, bool ignoreEmptyItem = true)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            var builder = new StringBuilder();
            var sourceType = typeof(TSource);
            var isStringType = sourceType == typeof(string);
            var isSimpleType = sourceType.IsSimpleType();
            foreach (var item in source)
            {
                if (isSimpleType && ignoreEmptyItem && (Equals(item, null) || (isStringType && item.ToString().Length == 0))) continue;
                if (builder.Length > 0) builder.Append(separator);
                builder.Append(callback(item));
            }
            if (builder.Length == 0) return string.Empty;
            if (start != null) builder.Insert(0, start);
            if (end != null) builder.Append(end);
            return builder.ToString();
        }

        /// <summary>
        /// 获取与 <see cref="System.Collections.Specialized.NameValueCollection"/> 中的指定键关联的值，并将值转换为指定的类型。
        /// </summary>
        /// <typeparam name="T">值的数据类型。</typeparam>
        /// <param name="source">可通过键或索引访问的关联 <see cref="string"/> 键和 <see cref="string"/> 值的集合。</param>
        /// <param name="name">项的 <see cref="string"/> 键，该项包含要获取的值。键可以是 null。</param>
        /// <param name="defaultValue">默认值。</param>
        /// <returns>如果找到，则为一个 <see cref="string"/>，包含与 <see cref="Collections.Specialized.NameValueCollection"/> 中的指定键关联的值的列表（此列表以逗号分隔）；否则为 null。</returns>
        public static T Get<T>(this System.Collections.Specialized.NameValueCollection source, string name, T defaultValue = default)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            var value = source.Get(name);
            if (value is null) return defaultValue;
            return (T)typeof(T).Parse(value);
        }

        /// <summary>
        /// 对当前集合的每个元素执行指定操作。
        /// </summary>
        /// <typeparam name="TSource">集合的数据类型。</typeparam>
        /// <param name="source">当前集合。</param>
        /// <param name="action">执行的委托。</param>
        public static void Each<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (action is null) throw new ArgumentNullException(nameof(action));

            foreach (var item in source) action(item);
        }

        /// <summary>
        /// 对当前集合的每个元素执行指定操作，并返回一个特定的结果集合。
        /// </summary>
        /// <typeparam name="TSource">集合的数据类型。</typeparam>
        /// <typeparam name="TResult">返回的数据类型。</typeparam>
        /// <param name="source">当前集合。</param>
        /// <param name="func">执行的委托。</param>
        /// <returns>集合。</returns>
        public static IEnumerable<TResult> Each<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> func)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (func is null) throw new ArgumentNullException(nameof(func));

            foreach (var item in source) yield return func(item);
        }

        /// <summary>
        /// 从序列的指定索引处添加不定数量的元素。
        /// </summary>
        /// <typeparam name="TSource"><paramref name="source"/> 中的元素的类型。</typeparam>
        /// <param name="source">一个 <see cref="IEnumerable{TSource}"/>，用于从中返回元素。</param>
        /// <param name="index">添加元素的起始索引。</param>
        /// <param name="items">添加的元素集合。</param>
        /// <returns>一个 <see cref="IEnumerable{TSource}"/> 的实例。</returns>
        public static IEnumerable<TSource> InsertWith<TSource>(this IEnumerable<TSource> source, int index, params TSource[] items)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (items is null) throw new ArgumentNullException(nameof(items));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

            int sourceIndex = 0;
            foreach (var sourceItem in source)
            {
                yield return sourceItem;
                if (sourceIndex++ == index)
                {
                    foreach (var item in items) yield return item;
                }
            }
            if (index >= sourceIndex)
            {
                foreach (var item in items) yield return item;
            }
        }

        /// <summary>
        /// 从序列的起始处添加不定数量的元素。
        /// </summary>
        /// <typeparam name="TSource"><paramref name="source"/> 中的元素的类型。</typeparam>
        /// <param name="source">一个 <see cref="IEnumerable{TSource}"/>，用于从中返回元素。</param>
        /// <param name="items">添加的元素集合。</param>
        /// <returns>一个 <see cref="IEnumerable{TSource}"/>，包含两个输入序列的已合并元素。</returns>
        public static IEnumerable<TSource> InsertBefore<TSource>(this IEnumerable<TSource> source, params TSource[] items)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (items is null) throw new ArgumentNullException(nameof(items));

            foreach (var item in items)
            {
                yield return item;
            }
            foreach (var item in source)
            {
                yield return item;
            }
        }

        /// <summary>
        /// 从序列的结尾处添加不定数量的元素。
        /// </summary>
        /// <typeparam name="TSource"><paramref name="source"/> 中的元素的类型。</typeparam>
        /// <param name="source">一个 <see cref="IEnumerable{TSource}"/>，用于从中返回元素。</param>
        /// <param name="items">添加的元素集合。</param>
        /// <returns>一个 <see cref="IEnumerable{TSource}"/>，包含两个输入序列的已合并元素。</returns>
        public static IEnumerable<TSource> InsertAfter<TSource>(this IEnumerable<TSource> source, params TSource[] items)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (items is null) throw new ArgumentNullException(nameof(items));

            foreach (var item in source)
            {
                yield return item;
            }
            foreach (var item in items)
            {
                yield return item;
            }
        }

        public static Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return ExecuteAsync();

            async Task<List<T>> ExecuteAsync()
            {
                var list = new List<T>();

                await foreach (var element in source)
                {
                    list.Add(element);
                }

                return list;
            }
        }

        /// <summary>
        /// 添加一个对象，当对象不存在时。
        /// </summary>
        public static bool AddIfNotContains<T>([NotNull] this ICollection<T> source, T item)
        {
            if (source.Contains(item))
            {
                return false;
            }
            source.Add(item);
            return true;
        }

        /// <summary>
        /// 排序集合中的元素，使得依赖项在被依赖项之前。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="getDependencies"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static List<T> SortByDependencies<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies, IEqualityComparer<T> comparer = null)
        {
            var sorted = new List<T>();
            var visited = new Dictionary<T, bool>(comparer);
            foreach (var item in source)
            {
                SortByDependenciesVisit(item, getDependencies, sorted, visited);
            }
            return sorted;
        }

        /// <summary>
        /// 排序集合中的元素，使得依赖项在被依赖项之前。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="getDependencies"></param>
        /// <param name="sorted"></param>
        /// <param name="visited"></param>
        /// <exception cref="ArgumentException"></exception>
        private static void SortByDependenciesVisit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted, Dictionary<T, bool> visited)
        {
            bool inProcess;
            var alreadyVisited = visited.TryGetValue(item, out inProcess);

            if (alreadyVisited)
            {
                if (inProcess)
                {
                    throw new ArgumentException("Cyclic dependency found! Item: " + item);
                }
            }
            else
            {
                visited[item] = true;
                var dependencies = getDependencies(item);
                if (dependencies != null)
                {
                    foreach (var dependency in dependencies)
                    {
                        SortByDependenciesVisit(dependency, getDependencies, sorted, visited);
                    }
                }
                visited[item] = false;
                sorted.Add(item);
            }
        }

        /// <summary>
        /// 从集合中移除满足条件的元素。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <param name="targetIndex"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static void MoveItem<T>(this List<T> source, Predicate<T> selector, int targetIndex)
        {
            if (!targetIndex.IsBetween(0, source.Count - 1))
            {
                throw new IndexOutOfRangeException("targetIndex should be between 0 and " + (source.Count - 1));
            }

            var currentIndex = source.FindIndex(0, selector);
            if (currentIndex == targetIndex)
            {
                return;
            }

            var item = source[currentIndex];
            source.RemoveAt(currentIndex);
            source.Insert(targetIndex, item);
        }

        /// <summary>
        /// Checks a value is between a minimum and maximum value.
        /// </summary>
        /// <param name="value">The value to be checked</param>
        /// <param name="minInclusiveValue">Minimum (inclusive) value</param>
        /// <param name="maxInclusiveValue">Maximum (inclusive) value</param>
        public static bool IsBetween<T>(this T value, T minInclusiveValue, T maxInclusiveValue) where T : IComparable<T>
        {
            return value.CompareTo(minInclusiveValue) >= 0 && value.CompareTo(maxInclusiveValue) <= 0;
        }
    }
}
