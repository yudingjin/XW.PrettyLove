namespace XW.PrettyLove.Domain.Shared
{
    /// <summary>
    /// 实体类接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<TKey> where TKey : struct, IEquatable<TKey>
    {
        TKey? Id { get; }

        EntityStatus? EntityStatus { get; set; }

        DBEnum DBEnum { get; set; }
    }

    /// <summary>
    /// 实体类接口
    /// </summary>
    public interface IEntity : IEntity<long>
    {

    }
}
