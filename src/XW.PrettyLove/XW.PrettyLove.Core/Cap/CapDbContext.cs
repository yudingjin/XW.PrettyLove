using Microsoft.EntityFrameworkCore;

namespace XW.PrettyLove.Core
{
    /// <summary>
    /// CAP数据库上下文
    /// </summary>
    public class CapDbContext : DbContext
    {
        public CapDbContext(DbContextOptions<CapDbContext> options)
            : base(options)
        {

        }
    }
}
