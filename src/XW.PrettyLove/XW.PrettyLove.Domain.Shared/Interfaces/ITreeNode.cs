namespace LS.ERP.Domain.Shared
{
    /// <summary>
    /// 树形节点接口（支持泛型ID类型）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface ITreeNode<T, TKey> where TKey : struct
    {
        TKey? Id { get; set; }
        TKey? ParentId { get; set; }
        int? Sort { get; set; }
        List<T> Children { get; set; }
        bool HasChildren { get; }
    }
}
