using PrettyLove.Domain.Shared;

namespace PrettyLove.Core
{
    /// <summary>
    /// 部门树形结构构建工具
    /// </summary>
    public static class TreeBuilder
    {
        /// <summary>
        /// 将扁平的列表转换为树形结构
        /// </summary>
        /// <param name="dataList">扁平的列表</param>
        /// <param name="rootParentId">根的父ID（默认null表示顶级部门）</param>
        /// <returns>树形结构的根部门集合</returns>
        public static List<T> BuildTree<T>(List<T> dataList, long? rootParentId = null) where T : ITreeNode<T, long>
        {
            // 空值处理
            if (dataList == null || !dataList.Any())
                return new List<T>();

            // 1. 构建ID到部门的映射，提高查找效率
            var listDict = dataList.ToDictionary(d => d.Id);

            // 2. 初始化所有部门的Children集合，避免空引用
            foreach (var dept in dataList)
            {
                dept.Children ??= new List<T>();
            }

            // 3. 构建树形结构
            foreach (var dept in dataList)
            {
                // 跳过根节点
                if (dept.ParentId == rootParentId)
                    continue;

                // 查找父部门并添加到子级集合
                if (dept.ParentId.HasValue && listDict.TryGetValue(dept.ParentId.Value, out var parentDept))
                {
                    // 按Sort字段排序子部门
                    var insertIndex = parentDept.Children.FindIndex(c => c.Sort >= dept.Sort);
                    if (insertIndex == -1)
                    {
                        parentDept.Children.Add(dept);
                    }
                    else
                    {
                        parentDept.Children.Insert(insertIndex, dept);
                    }
                }
            }

            // 4. 返回根集合，并按Sort排序
            return dataList.Where(dept => dept.ParentId == rootParentId).OrderBy(dept => dept.Sort).ToList();
        }

        /// <summary>
        /// 递归获取指定部门下的所有子部门（包含所有层级）
        /// </summary>
        /// <param name="department">起始部门</param>
        /// <returns>所有子部门的扁平列表</returns>
        public static List<T> GetAllChildren<T>(T department) where T : ITreeNode<T, long>
        {
            if (department == null || department.Children == null || !department.Children.Any())
                return new List<T>();

            var allChildren = new List<T>();
            foreach (var child in department.Children)
            {
                allChildren.Add(child);
                // 递归添加子部门的子部门
                allChildren.AddRange(GetAllChildren(child));
            }
            return allChildren;
        }
    }
}
