using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Insureme.Core.v1.Entities;
using Insureme.Core.v1.Entities.Dipn;

namespace Insureme.DataAccess.Interfaces
{
    public interface IDataContext : IDisposable
    {
        long Id { get; }
        long Instances { get; }
        int SaveChanges();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry Entry<TEntity>(TEntity entity) where TEntity : class;

        DbSet<UserEntity> Users { get; set; }
        DbSet<RoleEntity> Roles { get; set; }
        DbSet<FamilyTypeEntity> FamilyTypes { get; set; }
        DbSet<IncomeRangeEntity> IncomeRanges { get; set; }
        DbSet<StateEntity> States { get; set; }
        DbSet<TokenEntity> Tokens { get; set; }
        DbSet<KeyEntity> Keys { get; set; }
        DbSet<ProductTypeEntity> ProductTypes { get; set; }
        DbSet<ZoneEntity> Zones { get; set; }
        DbSet<ClientEntity> Clients { get; set; }
        DbSet<ConfigurationEntity> Configurations { get; set; }
        DbSet<RequestLogEntity> RequestLogs { get; set; }
        
        DbSet<AppTypeEntity> DipnAppTypes { get; set; }
        DbSet<AppEntity> DipnApps { get; set; }
        DbSet<DeviceTypeEntity> DipnDeviceTypes { get; set; }
        DbSet<DeviceEntity> DipnDevices { get; set; }
    }
}