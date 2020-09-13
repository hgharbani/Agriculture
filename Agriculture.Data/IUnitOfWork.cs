using System.Data.Entity;

namespace Agriculture.Data
{
    /// <summary>
    /// 
    /// </summary>
   public interface IUnitOfWork
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
    }
}
