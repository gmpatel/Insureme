using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using Insureme.Core.v1.Entities;
using Insureme.DataAccess.Defaults.EF;
using Insureme.DataAccess.Interfaces;
using Insureme.Core.v1.Entities.Dipn;

namespace Insureme.DataAccess.Defaults
{
    public class DataContext : DbContext, IDataContext
    {
        private static long counter;

        public DataContext() : base("InsureMe")
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;

            Database.SetInitializer<DataContext>
            (
                new MigrateDatabaseToLatestVersionExtended<DataContext, DBMigrationConfiguration>()
            );

            Id = ++counter;
        }

        public long Id { get; private set; }
        public long Instances => counter;

        public new virtual DbEntityEntry Entry<TEntity>(TEntity entity) where TEntity : class => base.Entry(entity);

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<FamilyTypeEntity> FamilyTypes { get; set; }
        public DbSet<IncomeRangeEntity> IncomeRanges { get; set; }
        public DbSet<StateEntity> States { get; set; }
        public DbSet<TokenEntity> Tokens { get; set; }
        public DbSet<KeyEntity> Keys { get; set; }
        public DbSet<ProductTypeEntity> ProductTypes { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ZoneEntity> Zones { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<InputTypeEntity> InputTypes { get; set; }
        public DbSet<PropertyEntity> Properties { get; set; }
        public DbSet<ValueTypeEntity> ValueTypes { get; set; }
        public DbSet<ValueEntity> Values { get; set; }
        public DbSet<ListEntity> Lists { get; set; }
        public DbSet<ListItemEntity> ListItems { get; set; }
        public DbSet<LinkTypeEntity> LinkTypes { get; set; }
        public DbSet<LinkEntity> Links { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<ConfigurationEntity> Configurations { get; set; }
        public DbSet<RequestLogEntity> RequestLogs { get; set; }
        
        public DbSet<AppTypeEntity> DipnAppTypes { get; set; }
        public DbSet<AppEntity> DipnApps { get; set; }
        public DbSet<DeviceTypeEntity> DipnDeviceTypes { get; set; }
        public DbSet<DeviceEntity> DipnDevices { get; set; }
        public DbSet<IPEntity> DipnIPs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<AppTypeEntity>().ToTable("DipnAppTypes");
            modelBuilder.Entity<AppTypeEntity>().Property(f => f.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<AppTypeEntity>().Property(f => f.Name).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<AppTypeEntity>().Property(k => k.Enabled).IsRequired();
            modelBuilder.Entity<AppTypeEntity>().Property(f => f.DateTimeCreated).IsRequired();
            modelBuilder.Entity<AppTypeEntity>().HasKey(r => new { r.Id });

            modelBuilder.Entity<AppEntity>().ToTable("DipnApps");
            modelBuilder.Entity<AppEntity>().Property(f => f.Id).IsRequired();
            modelBuilder.Entity<AppEntity>().Property(f => f.Name).HasColumnType("nvarchar").HasMaxLength(512).IsRequired();
            modelBuilder.Entity<AppEntity>().Property(f => f.TypeId).IsRequired();
            modelBuilder.Entity<AppEntity>().Property(f => f.ConnectionCode).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<AppEntity>().Property(f => f.Key).HasColumnType("nvarchar").HasMaxLength(256).IsOptional();
            modelBuilder.Entity<AppEntity>().Property(f => f.PreviousKey).HasColumnType("nvarchar").HasMaxLength(256).IsOptional();
            modelBuilder.Entity<AppEntity>().Property(f => f.DateTimeCreated).IsRequired();
            modelBuilder.Entity<AppEntity>().HasKey(r => new { r.Id });

            modelBuilder.Entity<DeviceTypeEntity>().ToTable("DipnDeviceTypes");
            modelBuilder.Entity<DeviceTypeEntity>().Property(f => f.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<DeviceTypeEntity>().Property(f => f.Name).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<DeviceTypeEntity>().Property(k => k.Enabled).IsRequired();
            modelBuilder.Entity<DeviceTypeEntity>().Property(f => f.DateTimeCreated).IsRequired();
            modelBuilder.Entity<DeviceTypeEntity>().HasKey(r => new { r.Id });

            modelBuilder.Entity<DeviceEntity>().ToTable("DipnDevices");
            modelBuilder.Entity<DeviceEntity>().Property(f => f.Id).IsRequired();
            modelBuilder.Entity<DeviceEntity>().Property(f => f.RegistrationToken).HasColumnType("nvarchar").HasMaxLength(1024).IsRequired();
            modelBuilder.Entity<DeviceEntity>().Property(f => f.TypeId).IsRequired();
            modelBuilder.Entity<DeviceEntity>().Property(f => f.Enabled).IsRequired();
            modelBuilder.Entity<DeviceEntity>().Property(f => f.DateTimeCreated).IsRequired();
            modelBuilder.Entity<DeviceEntity>().HasKey(r => new { r.Id });

            modelBuilder.Entity<IPEntity>().ToTable("DipnIPs");
            modelBuilder.Entity<IPEntity>().Property(f => f.Id).IsRequired();
            modelBuilder.Entity<IPEntity>().Property(f => f.IPAddress).IsRequired();
            modelBuilder.Entity<IPEntity>().Property(f => f.AppId).IsRequired();
            modelBuilder.Entity<IPEntity>().Property(f => f.DateTimeCreated).IsRequired();
            modelBuilder.Entity<IPEntity>().HasKey(r => new { r.Id });

            modelBuilder.Entity<RequestLogEntity>().ToTable("RequestLogs");
            modelBuilder.Entity<RequestLogEntity>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<RequestLogEntity>().Property(r => r.Uri).HasColumnType("nvarchar").HasMaxLength(2000).IsOptional();
            modelBuilder.Entity<RequestLogEntity>().Property(r => r.Key).HasColumnType("nvarchar").HasMaxLength(50).IsOptional();
            modelBuilder.Entity<RequestLogEntity>().Property(r => r.Token).HasColumnType("nvarchar").HasMaxLength(2000).IsOptional();
            modelBuilder.Entity<RequestLogEntity>().Property(r => r.IpAddress).HasColumnType("nvarchar").HasMaxLength(256).IsOptional();
            modelBuilder.Entity<RequestLogEntity>().Property(r => r.ContentType).HasColumnType("nvarchar").HasMaxLength(256).IsOptional();
            modelBuilder.Entity<RequestLogEntity>().Property(r => r.ContentBody).HasColumnType("nvarchar").IsOptional();
            modelBuilder.Entity<RequestLogEntity>().Property(r => r.Method).HasColumnType("nvarchar").HasMaxLength(256).IsOptional();
            modelBuilder.Entity<RequestLogEntity>().Property(r => r.Headers).HasColumnType("nvarchar").IsOptional();
            modelBuilder.Entity<RequestLogEntity>().Property(r => r.DateTimeRequested).IsOptional();
            modelBuilder.Entity<RequestLogEntity>().Property(r => r.ResponseStatusCode).IsOptional();
            modelBuilder.Entity<RequestLogEntity>().Property(r => r.ResponseContentType).HasColumnType("nvarchar").HasMaxLength(256).IsOptional();
            modelBuilder.Entity<RequestLogEntity>().Property(r => r.ResponseContentBody).HasColumnType("nvarchar").IsOptional();
            modelBuilder.Entity<RequestLogEntity>().Property(r => r.ResponseHeaders).HasColumnType("nvarchar").IsOptional();
            modelBuilder.Entity<RequestLogEntity>().Property(r => r.DateTimeResponded).IsOptional();

            modelBuilder.Entity<UserEntity>().ToTable("Users");
            modelBuilder.Entity<UserEntity>().Property(u => u.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<UserEntity>().Property(u => u.FirstName).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<UserEntity>().Property(u => u.LastName).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<UserEntity>().Property(u => u.BirthDate).IsOptional();
            modelBuilder.Entity<UserEntity>().Property(u => u.FamilyTypeId).IsRequired();
            modelBuilder.Entity<UserEntity>().Property(u => u.StateId).IsRequired();
            modelBuilder.Entity<UserEntity>().Property(u => u.PostCode).HasColumnType("nvarchar").HasMaxLength(50).IsOptional();
            modelBuilder.Entity<UserEntity>().Property(u => u.Mobile).HasColumnType("nvarchar").HasMaxLength(50).IsOptional();
            modelBuilder.Entity<UserEntity>().Property(u => u.Email).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<UserEntity>().Property(u => u.Password).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<UserEntity>().Property(u => u.Key).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<UserEntity>().Property(u => u.Code).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<UserEntity>().Property(u => u.RoleId).IsRequired();
            modelBuilder.Entity<UserEntity>().Property(u => u.Enabled).IsRequired();
            modelBuilder.Entity<UserEntity>().Property(u => u.IncomeRangeId).IsRequired();
            modelBuilder.Entity<UserEntity>().Property(u => u.Verified).IsRequired();
            modelBuilder.Entity<UserEntity>().Property(u => u.DateTimeCreated).IsRequired();
            modelBuilder.Entity<UserEntity>().HasKey(u => new { u.Id });

            modelBuilder.Entity<KeyEntity>().ToTable("Keys");
            modelBuilder.Entity<KeyEntity>().Property(k => k.Id).HasColumnType("uniqueidentifier").IsRequired();
            modelBuilder.Entity<KeyEntity>().Property(k => k.Name).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<KeyEntity>().Property(k => k.Enabled).IsRequired();
            modelBuilder.Entity<KeyEntity>().Property(k => k.DateTimeCreated).IsRequired();
            modelBuilder.Entity<KeyEntity>().HasKey(r => new { r.Id });

            modelBuilder.Entity<RoleEntity>().ToTable("Roles");
            modelBuilder.Entity<RoleEntity>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<RoleEntity>().Property(r => r.Name).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<RoleEntity>().Property(r => r.Code).HasColumnType("nvarchar").HasMaxLength(10).IsRequired();
            modelBuilder.Entity<RoleEntity>().Property(k => k.Enabled).IsRequired();
            modelBuilder.Entity<RoleEntity>().Property(r => r.DateTimeCreated).IsRequired();
            modelBuilder.Entity<RoleEntity>().HasKey(r => new { r.Id });

            modelBuilder.Entity<FamilyTypeEntity>().ToTable("FamilyTypes");
            modelBuilder.Entity<FamilyTypeEntity>().Property(f => f.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<FamilyTypeEntity>().Property(f => f.Name).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<FamilyTypeEntity>().Property(f => f.Code).HasColumnType("nvarchar").HasMaxLength(10).IsRequired();
            modelBuilder.Entity<FamilyTypeEntity>().Property(k => k.Enabled).IsRequired();
            modelBuilder.Entity<FamilyTypeEntity>().Property(f => f.DateTimeCreated).IsRequired();
            modelBuilder.Entity<FamilyTypeEntity>().HasKey(r => new { r.Id });
            
            modelBuilder.Entity<StateEntity>().ToTable("States");
            modelBuilder.Entity<StateEntity>().Property(f => f.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<StateEntity>().Property(f => f.Name).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<StateEntity>().Property(f => f.Code).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();
            modelBuilder.Entity<StateEntity>().Property(k => k.Enabled).IsRequired();
            modelBuilder.Entity<StateEntity>().Property(f => f.DateTimeCreated).IsRequired();
            modelBuilder.Entity<StateEntity>().HasKey(r => new { r.Id });

            modelBuilder.Entity<IncomeRangeEntity>().ToTable("IncomeRanges");
            modelBuilder.Entity<IncomeRangeEntity>().Property(f => f.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<IncomeRangeEntity>().Property(f => f.Name).HasColumnType("nvarchar").HasMaxLength(256);
            modelBuilder.Entity<IncomeRangeEntity>().Property(f => f.Description).HasColumnType("nvarchar").HasMaxLength(256);
            modelBuilder.Entity<IncomeRangeEntity>().Property(k => k.Enabled).IsRequired();
            modelBuilder.Entity<IncomeRangeEntity>().Property(f => f.DateTimeCreated).IsRequired();
            modelBuilder.Entity<IncomeRangeEntity>().HasKey(r => new { r.Id });

            modelBuilder.Entity<TokenEntity>().ToTable("Tokens");
            modelBuilder.Entity<TokenEntity>().Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<TokenEntity>().Property(t => t.Token).HasColumnType("nvarchar").IsRequired(); //.HasMaxLength(256)
            modelBuilder.Entity<TokenEntity>().Property(t => t.DateTimeCreated).IsRequired();
            modelBuilder.Entity<TokenEntity>().Property(t => t.DateTimeExpire).IsRequired();
            modelBuilder.Entity<TokenEntity>().HasKey(r => new { r.Id });

            modelBuilder.Entity<ProductTypeEntity>().ToTable("ProductTypes");
            modelBuilder.Entity<ProductTypeEntity>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ProductTypeEntity>().Property(r => r.Name).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<ProductTypeEntity>().Property(r => r.Title).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();
            modelBuilder.Entity<ProductTypeEntity>().Property(k => k.Enabled).IsRequired();
            modelBuilder.Entity<ProductTypeEntity>().Property(r => r.DateTimeCreated).IsRequired();
            modelBuilder.Entity<ProductTypeEntity>().HasKey(r => new { r.Id });
            
            modelBuilder.Entity<ZoneEntity>().ToTable("Zones");
            modelBuilder.Entity<ZoneEntity>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ZoneEntity>().Property(r => r.Name).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<ZoneEntity>().Property(r => r.Title).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<ZoneEntity>().Property(k => k.Enabled).IsRequired();
            modelBuilder.Entity<ZoneEntity>().Property(r => r.DateTimeCreated).IsRequired();
            modelBuilder.Entity<ZoneEntity>().HasKey(r => new { r.Id });

            modelBuilder.Entity<GroupEntity>().ToTable("Groups");
            modelBuilder.Entity<GroupEntity>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<GroupEntity>().Property(r => r.Name).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<GroupEntity>().Property(r => r.Title).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<GroupEntity>().Property(k => k.Enabled).IsRequired();
            modelBuilder.Entity<GroupEntity>().Property(r => r.DateTimeCreated).IsRequired();
            modelBuilder.Entity<GroupEntity>().HasKey(r => new { r.Id });

            modelBuilder.Entity<InputTypeEntity>().ToTable("InputTypes");
            modelBuilder.Entity<InputTypeEntity>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<InputTypeEntity>().Property(r => r.Name).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<InputTypeEntity>().Property(r => r.Enabled).IsRequired();
            modelBuilder.Entity<InputTypeEntity>().Property(r => r.DateTimeCreated).IsRequired();
            modelBuilder.Entity<InputTypeEntity>().HasKey(r => new { r.Id });

            modelBuilder.Entity<PropertyEntity>().ToTable("Properties");
            modelBuilder.Entity<PropertyEntity>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<PropertyEntity>().Property(r => r.Name).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<PropertyEntity>().Property(r => r.Title).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<PropertyEntity>().Property(r => r.InputTypeId).IsRequired();
            modelBuilder.Entity<PropertyEntity>().Property(r => r.TypeId).IsRequired();
            modelBuilder.Entity<PropertyEntity>().Property(r => r.Mandatory).IsRequired(); // .HasColumnAnnotation("Default", 1);
            modelBuilder.Entity<PropertyEntity>().Property(r => r.PossibleNullNumber).IsRequired();
            modelBuilder.Entity<PropertyEntity>().Property(r => r.PossibleComments).IsRequired();
            modelBuilder.Entity<PropertyEntity>().Property(r => r.ValuesListId).IsRequired();
            modelBuilder.Entity<PropertyEntity>().Property(r => r.UnitsListId).IsRequired();
            modelBuilder.Entity<PropertyEntity>().Property(r => r.ValidationRegex).HasColumnType("nvarchar").HasMaxLength(512).IsOptional();
            modelBuilder.Entity<PropertyEntity>().Property(r => r.ParentId).IsOptional();
            modelBuilder.Entity<PropertyEntity>().Property(r => r.Enabled).IsRequired();
            modelBuilder.Entity<PropertyEntity>().Property(r => r.DateTimeCreated).IsRequired();
            modelBuilder.Entity<PropertyEntity>().HasKey(r => new { r.Id });

            modelBuilder.Entity<ValueTypeEntity>().ToTable("ValueTypes");
            modelBuilder.Entity<ValueTypeEntity>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<ValueTypeEntity>().Property(r => r.Name).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<ValueTypeEntity>().Property(r => r.Enabled).IsRequired();
            modelBuilder.Entity<ValueTypeEntity>().Property(r => r.DateTimeCreated).IsRequired();
            modelBuilder.Entity<ValueTypeEntity>().HasKey(r => new { r.Id });

            modelBuilder.Entity<ValueEntity>().ToTable("Values");
            modelBuilder.Entity<ValueEntity>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ValueEntity>().Property(r => r.ProductId).IsRequired();
            modelBuilder.Entity<ValueEntity>().Property(r => r.PropertyId).IsRequired();
            modelBuilder.Entity<ValueEntity>().Property(r => r.TypeId).IsRequired();
            modelBuilder.Entity<ValueEntity>().Property(r => r.Value).HasColumnType("nvarchar").IsOptional();
            modelBuilder.Entity<ValueEntity>().Property(r => r.Unit).HasColumnType("nvarchar").HasMaxLength(512).IsOptional();
            modelBuilder.Entity<ValueEntity>().Property(r => r.Comments).HasColumnType("nvarchar").HasMaxLength(512).IsOptional();
            modelBuilder.Entity<ValueEntity>().Property(r => r.DateTimeCreated).IsRequired();
            modelBuilder.Entity<ValueEntity>().HasKey(r => new { r.Id });

            modelBuilder.Entity<ClientEntity>().ToTable("Clients");
            modelBuilder.Entity<ClientEntity>().Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ClientEntity>().Property(c => c.Name).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<ClientEntity>().Property(c => c.Code).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();
            modelBuilder.Entity<ClientEntity>().Property(c => c.Url).HasColumnType("nvarchar").HasMaxLength(256).IsOptional();
            modelBuilder.Entity<ClientEntity>().Property(c => c.LogoUrl).HasColumnType("nvarchar").HasMaxLength(256).IsOptional();
            modelBuilder.Entity<ClientEntity>().Property(c => c.Enabled).IsRequired();
            modelBuilder.Entity<ClientEntity>().Property(c => c.DateTimeCreated).IsRequired();
            modelBuilder.Entity<ClientEntity>().HasKey(c => new { c.Id });

            modelBuilder.Entity<ListEntity>().ToTable("Lists");
            modelBuilder.Entity<ListEntity>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ListEntity>().Property(r => r.Name).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<ListEntity>().Property(r => r.Enabled).IsRequired();
            modelBuilder.Entity<ListEntity>().Property(r => r.DateTimeCreated).IsRequired();
            modelBuilder.Entity<ListEntity>().HasKey(r => new { r.Id });
            
            modelBuilder.Entity<ListItemEntity>().ToTable("ListItems");
            modelBuilder.Entity<ListItemEntity>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ListItemEntity>().Property(r => r.ItemId).IsRequired();
            modelBuilder.Entity<ListItemEntity>().Property(r => r.Name).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<ListItemEntity>().Property(r => r.Enabled).IsRequired();
            modelBuilder.Entity<ListItemEntity>().Property(r => r.DateTimeCreated).IsRequired();
            modelBuilder.Entity<ListItemEntity>().HasKey(r => new { r.Id });

            modelBuilder.Entity<LinkTypeEntity>().ToTable("LinkTypes");
            modelBuilder.Entity<LinkTypeEntity>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<LinkTypeEntity>().Property(r => r.Name).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<LinkTypeEntity>().Property(r => r.Enabled).IsRequired();
            modelBuilder.Entity<LinkTypeEntity>().Property(r => r.DateTimeCreated).IsRequired();
            modelBuilder.Entity<LinkTypeEntity>().HasKey(r => new { r.Id });

            modelBuilder.Entity<LinkEntity>().ToTable("Links");
            modelBuilder.Entity<LinkEntity>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<LinkEntity>().Property(r => r.TypeId).IsRequired();
            modelBuilder.Entity<LinkEntity>().Property(r => r.ProductId).IsRequired();
            modelBuilder.Entity<LinkEntity>().Property(r => r.Url).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<LinkEntity>().Property(r => r.Hint).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<LinkEntity>().Property(r => r.Enabled).IsRequired();
            modelBuilder.Entity<LinkEntity>().Property(r => r.DateTimeCreated).IsRequired();
            modelBuilder.Entity<LinkEntity>().HasKey(r => new { r.Id });
                                                                        
            modelBuilder.Entity<ProductEntity>().ToTable("Products");
            modelBuilder.Entity<ProductEntity>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ProductEntity>().Property(r => r.Title).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<ProductEntity>().Property(r => r.Description).HasColumnType("nvarchar(max)").IsOptional();
            modelBuilder.Entity<ProductEntity>().Property(r => r.Name).HasColumnType("nvarchar").HasMaxLength(256).IsOptional();
            modelBuilder.Entity<ProductEntity>().Property(r => r.Variation).HasColumnType("nvarchar").HasMaxLength(256).IsOptional();
            modelBuilder.Entity<ProductEntity>().Property(r => r.ClientId).IsRequired();
            modelBuilder.Entity<ProductEntity>().Property(r => r.TypeId).IsRequired();
            modelBuilder.Entity<ProductEntity>().Property(r => r.FamilyTypeId).IsRequired();
            modelBuilder.Entity<ProductEntity>().Property(r => r.Price).IsRequired();
            modelBuilder.Entity<ProductEntity>().Property(r => r.PriceDescription).HasColumnType("nvarchar").HasMaxLength(512).IsOptional();
            modelBuilder.Entity<ProductEntity>().Property(r => r.IsGstApplicableOnPrice).IsRequired();
            modelBuilder.Entity<ProductEntity>().Property(r => r.Shipping).IsRequired();
            modelBuilder.Entity<ProductEntity>().Property(r => r.ShippingDescription).HasColumnType("nvarchar").HasMaxLength(512).IsOptional();
            modelBuilder.Entity<ProductEntity>().Property(r => r.IsGstApplicableOnShipping).IsRequired();
            modelBuilder.Entity<ProductEntity>().Property(r => r.AvailableToPurchaseInStates).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<ProductEntity>().Property(k => k.Enabled).IsRequired();
            modelBuilder.Entity<ProductEntity>().Property(r => r.DateTimeCreated).IsRequired();
            modelBuilder.Entity<ProductEntity>().HasKey(r => new { r.Id });

            modelBuilder.Entity<ConfigurationEntity>().ToTable("Configurations");
            modelBuilder.Entity<ConfigurationEntity>().Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ConfigurationEntity>().Property(c => c.DateTimeCreated).IsRequired();
            modelBuilder.Entity<ConfigurationEntity>().Property(c => c.BackEndKey).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            modelBuilder.Entity<ConfigurationEntity>().Property(c => c.TokenLifeSpanMinutes).IsRequired();
            modelBuilder.Entity<ConfigurationEntity>().HasKey(c => new { c.Id });

            modelBuilder.Entity<AppTypeEntity>()
                .HasMany(r => r.Apps)
                .WithRequired()
                .HasForeignKey(x => x.TypeId);

            modelBuilder.Entity<DeviceTypeEntity>()
                .HasMany(r => r.Devices)
                .WithRequired()
                .HasForeignKey(x => x.TypeId);

            modelBuilder.Entity<AppEntity>()
                .HasMany(r => r.IPs)
                .WithRequired()
                .HasForeignKey(x => x.AppId);

            modelBuilder.Entity<RoleEntity>()
                .HasMany(r => r.Users)
                .WithRequired()
                .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<FamilyTypeEntity>()
                .HasMany(f => f.Users)
                .WithRequired()
                .HasForeignKey(x => x.FamilyTypeId);

            modelBuilder.Entity<FamilyTypeEntity>()
                .HasMany(f => f.Products)
                .WithRequired()
                .HasForeignKey(x => x.FamilyTypeId);

            modelBuilder.Entity<StateEntity>()
                .HasMany(f => f.Users)
                .WithRequired()
                .HasForeignKey(x => x.StateId);

            modelBuilder.Entity<IncomeRangeEntity>()
                .HasMany(i => i.Users)
                .WithRequired()
                .HasForeignKey(x => x.IncomeRangeId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.Tokens)
                .WithRequired()
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<ListEntity>()
                .HasMany(u => u.ListItems)
                .WithRequired()
                .HasForeignKey(x => x.ListId);

            modelBuilder.Entity<ListEntity>()
                .HasMany(u => u.Properties)
                .WithRequired()
                .HasForeignKey(x => x.ValuesListId);

            modelBuilder.Entity<ListEntity>()
                .HasMany(u => u.Properties)
                .WithRequired()
                .HasForeignKey(x => x.UnitsListId);

            modelBuilder.Entity<InputTypeEntity>()
                .HasMany(u => u.Properties)
                .WithRequired()
                .HasForeignKey(x => x.InputTypeId);

            modelBuilder.Entity<ValueTypeEntity>()
                .HasMany(u => u.Properties)
                .WithRequired()
                .HasForeignKey(x => x.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ValueTypeEntity>()
                .HasMany(u => u.Values)
                .WithRequired()
                .HasForeignKey(x => x.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PropertyEntity>()
                .HasMany(u => u.Children)
                .WithOptional(i => i.Parent)
                .HasForeignKey(x => x.ParentId);

            modelBuilder.Entity<ClientEntity>()
                .HasMany(u => u.Products)
                .WithRequired()
                .HasForeignKey(x => x.ClientId);

            modelBuilder.Entity<ProductTypeEntity>()
                .HasMany(u => u.Products)
                .WithRequired()
                .HasForeignKey(x => x.TypeId);

            modelBuilder.Entity<ProductEntity>()
                .HasMany(u => u.Values)
                .WithRequired()
                .HasForeignKey(x => x.ProductId);

            modelBuilder.Entity<ProductEntity>()
                .HasMany(u => u.Links)
                .WithRequired()
                .HasForeignKey(x => x.ProductId);

            modelBuilder.Entity<LinkTypeEntity>()
                .HasMany(u => u.Links)
                .WithRequired()
                .HasForeignKey(x => x.TypeId);

            modelBuilder.Entity<PropertyEntity>()
                .HasMany(u => u.Values)
                .WithRequired()
                .HasForeignKey(x => x.PropertyId);

            modelBuilder.Entity<AppEntity>()
                .HasMany<DeviceEntity>(u => u.Devices)
                .WithMany(c => c.Apps)
                .Map(cs =>
                {
                    cs.MapLeftKey("DipnAppId");
                    cs.MapRightKey("DipnDeviceId");
                    cs.ToTable("DipnAppsDevices");
                });

            modelBuilder.Entity<UserEntity>()
                .HasMany<ClientEntity>(u => u.Clients)
                .WithMany(c => c.Users)
                .Map(cs =>
                {
                    cs.MapLeftKey("UserId");
                    cs.MapRightKey("ClientId");
                    cs.ToTable("UsersClients");
                });

            modelBuilder.Entity<ProductTypeEntity>()
                .HasMany<ZoneEntity>(u => u.Zones)
                .WithMany(c => c.ProductTypes)
                .Map(cs =>
                {
                    cs.MapLeftKey("ProductTypeId");
                    cs.MapRightKey("ZoneId");
                    cs.ToTable("ProductTypesZones");
                });

            modelBuilder.Entity<ClientEntity>()
                .HasMany<ProductTypeEntity>(u => u.ProductTypes)
                .WithMany(c => c.Clients)
                .Map(cs =>
                {
                    cs.MapLeftKey("ClientId");
                    cs.MapRightKey("ProductTypeId");
                    cs.ToTable("ClientsProductTypes");
                });

            modelBuilder.Entity<ZoneEntity>()
                .HasMany<GroupEntity>(u => u.Groups)
                .WithMany(c => c.Zones)
                .Map(cs =>
                {
                    cs.MapLeftKey("ZoneId");
                    cs.MapRightKey("GroupId");
                    cs.ToTable("ZonesGroups");
                });

            modelBuilder.Entity<GroupEntity>()
                .HasMany<PropertyEntity>(u => u.Properties)
                .WithMany(c => c.Groups)
                .Map(cs =>
                {
                    cs.MapLeftKey("GroupId");
                    cs.MapRightKey("PropertyId");
                    cs.ToTable("GroupsProperties");
                });

            modelBuilder.Entity<ProductEntity>()
                .HasMany<StateEntity>(u => u.States)
                .WithMany(c => c.Products)
                .Map(cs =>
                {
                    cs.MapLeftKey("ProductId");
                    cs.MapRightKey("StateId");
                    cs.ToTable("ProductsStates");
                });

            modelBuilder.Entity<AppEntity>().HasIndex(
                "IX_NC_UNIQ_DipnApp_Id_Name",
                IndexOptions.Unique,
                k => k.Property(x => x.Id),
                k => k.Property(x => x.Name)
            );

            modelBuilder.Entity<AppTypeEntity>().HasIndex(
                "IX_NC_UNIQ_DipnAppType_Name",
                IndexOptions.Unique,
                k => k.Property(x => x.Name)
            );

            modelBuilder.Entity<DeviceEntity>().HasIndex(
                "IX_NC_UNIQ_DipnDevice_RegistrationToken",
                IndexOptions.Unique,
                k => k.Property(x => x.RegistrationToken)
            );

            modelBuilder.Entity<DeviceTypeEntity>().HasIndex(
                "IX_NC_UNIQ_DipnDeviceType_Name",
                IndexOptions.Unique,
                k => k.Property(x => x.Name)
            );

            modelBuilder.Entity<IPEntity>().HasIndex(
                "IX_NC_UNIQ_DipnIP_Id",
                IndexOptions.Unique,
                k => k.Property(x => x.Id)
            );

            modelBuilder.Entity<KeyEntity>().HasIndex(
                "IX_NC_UNIQ_Key_Name_Enabled",
                IndexOptions.Unique,
                k => k.Property(x => x.Name),
                e => e.Property(x => x.Enabled)
            );

            modelBuilder.Entity<UserEntity>().HasIndex(
                "IX_NC_UNIQ_User_Email",
                IndexOptions.Unique,
                e => e.Property(x => x.Email)
            );

            modelBuilder.Entity<ClientEntity>().HasIndex(
                "IX_NC_UNIQ_Client_Name_Enabled",
                IndexOptions.Unique,
                e => e.Property(x => x.Name),
                e => e.Property(x => x.Enabled)
            );

            modelBuilder.Entity<ZoneEntity>().HasIndex(
                "IX_NC_UNIQ_Zone_Name_Enabled",
                IndexOptions.Unique,
                e => e.Property(x => x.Name),
                e => e.Property(x => x.Enabled)
            );

            modelBuilder.Entity<GroupEntity>().HasIndex(
                "IX_NC_UNIQ_Group_Name_Enabled",
                IndexOptions.Unique,
                e => e.Property(x => x.Name),
                e => e.Property(x => x.Enabled)
            );

            modelBuilder.Entity<PropertyEntity>().HasIndex(
                "IX_NC_UNIQ_Property_Name_Enabled",
                IndexOptions.Unique,
                e => e.Property(x => x.Name),
                e => e.Property(x => x.Enabled)
            );

            modelBuilder.Entity<ProductTypeEntity>().HasIndex(
                "IX_NC_UNIQ_ProductType_Name_Enabled",
                IndexOptions.Unique,
                e => e.Property(x => x.Name),
                e => e.Property(x => x.Enabled)
            );

            modelBuilder.Entity<ListEntity>().HasIndex(
                "IX_NC_UNIQ_List_Name_Enabled",
                IndexOptions.Unique,
                e => e.Property(x => x.Name),
                e => e.Property(x => x.Enabled)
            );

            modelBuilder.Entity<ListItemEntity>().HasIndex(
                "IX_NC_UNIQ_ListItem_ListId_Name_Enabled",
                IndexOptions.Unique,
                e => e.Property(x => x.ListId),
                e => e.Property(x => x.Name),
                e => e.Property(x => x.Enabled)
            );

            modelBuilder.Entity<InputTypeEntity>().HasIndex(
                "IX_NC_UNIQ_InputType_Name_Enabled",
                IndexOptions.Unique,
                e => e.Property(x => x.Name),
                e => e.Property(x => x.Enabled)
            );

            modelBuilder.Entity<ValueTypeEntity>().HasIndex(
                "IX_NC_UNIQ_ValueType_Name_Enabled",
                IndexOptions.Unique,
                e => e.Property(x => x.Name),
                e => e.Property(x => x.Enabled)
            );

            modelBuilder.Entity<ListItemEntity>().HasIndex(
                "IX_NC_UNIQ_ListItem_ListId_ItemId_Enabled",
                IndexOptions.Unique,
                e => e.Property(x => x.ListId),
                e => e.Property(x => x.ItemId),
                e => e.Property(x => x.Enabled)
            );

            modelBuilder.Entity<ProductEntity>().HasIndex(
                "IX_NC_UNIQ_Product_Title_ClientId_ProductTypeId_FamilyTypeId_States_Enabled",
                IndexOptions.Unique,
                e => e.Property(x => x.Title),
                e => e.Property(x => x.ClientId),
                e => e.Property(x => x.TypeId),
                e => e.Property(x => x.FamilyTypeId),
                e => e.Property(x => x.AvailableToPurchaseInStates),
                e => e.Property(x => x.Enabled)
            );

            modelBuilder.Entity<LinkTypeEntity>().HasIndex(
                "IX_NC_UNIQ_LinkType_Name_Enabled",
                IndexOptions.Unique,
                e => e.Property(x => x.Name),
                e => e.Property(x => x.Enabled)
            );

            modelBuilder.Entity<LinkEntity>().HasIndex(
                "IX_NC_UNIQ_Link_Url_ProductId_Enabled",
                IndexOptions.Unique,
                e => e.Property(x => x.Url),
                e => e.Property(x => x.ProductId),
                e => e.Property(x => x.Enabled)
            );

            modelBuilder.Entity<ValueEntity>().HasIndex(
                "IX_NC_UNIQ_Value_ProductId_PropertyId",
                IndexOptions.Unique,
                e => e.Property(x => x.ProductId),
                e => e.Property(x => x.PropertyId)
            );
        }
    }
}