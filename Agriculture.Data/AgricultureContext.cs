using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agriculture.DomainClass.Models;

namespace Agriculture.Data
{
  public  class AgricultureContext : DbContext, IUnitOfWork
  {

      public AgricultureContext() : base("AgricultureContext")
      {
      }

      public DbSet<Human> Humans { get; set; }
      public DbSet<Role> Roles { get; set; }
      public DbSet<User> Users { get; set; }
      public DbSet<AreaMap> AreaMaps { get; set; }
      public DbSet<MapAreaType> MapAreaTypes { get; set; }
      public DbSet<MapPoint> MapPoints { get; set; }
      protected override void OnModelCreating(DbModelBuilder modelBuilder)
      {
          modelBuilder.Entity<MapPoint>().
              HasRequired<AreaMap>(x => x.AreaMap)
              .WithMany(y => y.MapPoints)
              .HasForeignKey(x => x.AreaMapId);

          modelBuilder.Entity<AreaMap>().
              HasRequired<MapAreaType>(x => x.MapAreaType)
              .WithMany(y => y.AreaMaps)
              .HasForeignKey(x => x.AreaTypeId);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
      }

      

        #region IUnitOfWork Members
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        #endregion
    }
}
