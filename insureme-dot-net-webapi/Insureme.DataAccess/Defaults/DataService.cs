using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Text;
using System.Transactions;
using Insureme.Configuration.Interfaces;
using Insureme.Core.v1.Entities;
using Insureme.Core.v1.Helpers;
using Insureme.Core.v1.Objects.Requests;
using Insureme.DataAccess.Interfaces;
using Insureme.Core.v1.Objects.Exceptions;
using Insureme.Core.v1.Entities.Dipn;
using Insureme.Core.v1.Objects.Requests.Dipn;
using IsolationLevel = System.Data.IsolationLevel;

namespace Insureme.DataAccess.Defaults
{
    public class DataService : IDataService
    {
        internal static class Constants
        {
            
        }

        private static long objectsCounter;

        public readonly IConfig Config;
        public readonly IUnitOfWork UnitOfWork;
        public readonly IDataContext DataContext;   

        public readonly IRepository<ClientEntity> ClientRepository;
        public readonly IRepository<ConfigurationEntity> ConfigurationsRepository;
        public readonly IRepository<FamilyTypeEntity> FamilyTypesRepository;
        public readonly IRepository<GroupEntity> GroupsRepository;
        public readonly IRepository<IncomeRangeEntity> IncomeRangesRepository;
        public readonly IRepository<InputTypeEntity> InputTypesRepository;
        public readonly IRepository<LinkEntity> LinksRepository;
        public readonly IRepository<LinkTypeEntity> LinkTypesRepository;
        public readonly IRepository<ListEntity> ListsRepository;
        public readonly IRepository<ListItemEntity> ListItemsRepository;
        public readonly IRepository<ProductEntity> ProductsRepository;
        public readonly IRepository<ProductTypeEntity> ProductTypesRepository;
        public readonly IRepository<PropertyEntity> PropertiesRepository;
        public readonly IRepository<RoleEntity> RolesRepository;
        public readonly IRepository<StateEntity> StatesRepository;
        public readonly IRepository<TokenEntity> TokensRepository;
        public readonly IRepository<UserEntity> UsersRepository;
        public readonly IRepository<ValueEntity> ValuesRepository;
        public readonly IRepository<ValueTypeEntity> ValueTypesRepository;
        public readonly IRepository<ZoneEntity> ZonesRepository;

        public readonly IRepository<AppEntity> AppsRepository;
        public readonly IRepository<AppTypeEntity> AppTypesRepository;
        public readonly IRepository<DeviceEntity> DevicesRepository;
        public readonly IRepository<DeviceTypeEntity> DeviceTypesRepository;
        public readonly IRepository<IPEntity> IPsRepository;

        static DataService()
        {

        }

        public DataService(IConfig config, IUnitOfWork unitOfWork, IDataContext dataContext,
            IRepository<ClientEntity> clientRepository,
            IRepository<ConfigurationEntity> configurationsRepository,
            IRepository<FamilyTypeEntity> familyTypesRepository,
            IRepository<GroupEntity> groupsRepository,
            IRepository<IncomeRangeEntity> incomeRangesRepository,
            IRepository<InputTypeEntity> inputTypesRepository,
            IRepository<LinkEntity> linksRepository,
            IRepository<LinkTypeEntity> linkTypesRepository,
            IRepository<ListEntity> listsRepository,
            IRepository<ListItemEntity> listItemsRepository,
            IRepository<ProductEntity> productsRepository,
            IRepository<ProductTypeEntity> productTypesRepository,
            IRepository<PropertyEntity> propertiesRepository,
            IRepository<RoleEntity> rolesRepository, 
            IRepository<StateEntity> statesRepository,
            IRepository<TokenEntity> tokensRepository,
            IRepository<UserEntity> usersRepository, 
            IRepository<ZoneEntity> zonesRepository,
            IRepository<ValueEntity> valuesRepository,
            IRepository<ValueTypeEntity> valueTypesRepository,

            IRepository<AppTypeEntity> appTypesRepository,
            IRepository<AppEntity> appsRepository,
            IRepository<DeviceTypeEntity> deviceTypesRepository,
            IRepository<DeviceEntity> devicesRepository,
            IRepository<IPEntity> iPsRepository
        )
        {
            Id = ++objectsCounter;

            this.Config = config;
            this.UnitOfWork = unitOfWork;
            this.DataContext = dataContext;                          
            this.ClientRepository = clientRepository;
            this.ConfigurationsRepository = configurationsRepository;
            this.FamilyTypesRepository = familyTypesRepository;
            this.GroupsRepository = groupsRepository;
            this.IncomeRangesRepository = incomeRangesRepository;
            this.InputTypesRepository = inputTypesRepository;
            this.LinksRepository = linksRepository;
            this.LinkTypesRepository = linkTypesRepository;
            this.ListsRepository = listsRepository;
            this.ListItemsRepository = listItemsRepository;
            this.ProductsRepository = productsRepository;
            this.ProductTypesRepository = productTypesRepository;
            this.PropertiesRepository = propertiesRepository;
            this.RolesRepository = rolesRepository;
            this.StatesRepository = statesRepository;
            this.TokensRepository = tokensRepository;
            this.UsersRepository = usersRepository;
            this.ValuesRepository = valuesRepository;
            this.ValueTypesRepository = valueTypesRepository;
            this.ZonesRepository = zonesRepository;

            this.AppTypesRepository = appTypesRepository;
            this.AppsRepository = appsRepository;
            this.DeviceTypesRepository = deviceTypesRepository;
            this.DevicesRepository = devicesRepository;
            this.IPsRepository = iPsRepository;
        }

        public long Id { get; private set; }
        public long Instances => objectsCounter;

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }

        public AppEntity DipnHandshakeApp(Guid id, string ip)
        {
            if (!id.Equals(Guid.Empty))
            {
                var dateTime = DateTime.UtcNow;
                var app = AppsRepository.FindBy(x => x.Id.Equals(id)).FirstOrDefault();

                if (app != null)
                {
                    var lastIp = app.IPs.OrderByDescending(x => x.DateTimeCreated).FirstOrDefault();
                    var key = RandomString.Generate(4, 4, true, false, false, false);

                    if (string.IsNullOrEmpty(app.Key))
                    {
                        app.Key = key;
                        app.DateTimeLastModified = dateTime;
                    }
                    else
                    {
                        app.PreviousKey = app.Key;
                        app.Key = key;
                        app.DateTimeLastModified = dateTime;
                    }

                    this.AppsRepository.Update(app);
                    
                    if (lastIp == null)
                    {
                        app.IPs.Add(new IPEntity
                        {
                            IPAddress = ip,
                            AppId = app.Id,
                            DateTimeCreated = dateTime,
                            DateTimeLastModified = dateTime
                        });
                    }
                    else
                    {
                        if(lastIp.IPAddress.Equals(ip, StringComparison.CurrentCultureIgnoreCase))
                        {
                            lastIp.DateTimeLastModified = dateTime;
                        }
                        else
                        {
                            app.IPs.Add(new IPEntity
                            {
                                IPAddress = ip,
                                AppId = app.Id,
                                DateTimeCreated = dateTime,
                                DateTimeLastModified = dateTime
                            });
                        }
                    }

                    this.UnitOfWork.Save();

                    return app;
                }

                throw new GeneralException(string.Format("Invalid value of the 'id' parameter. There is no app found with the id '{0}' for handshaking.", id));
            }

            throw new GeneralException("Invalid value of the 'id' parameter OR the value of the 'id' parameter is null or empty for handshaking.");
        }

        public bool DipnLinkDeviceWithApp(Guid id, string connectionCode, string key)
        {
            if (!id.Equals(Guid.Empty))
            {
                if (!string.IsNullOrEmpty(connectionCode))
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        var dateTime = DateTime.UtcNow;
                        var device = DevicesRepository.FindBy(x => x.Id.Equals(id)).FirstOrDefault();

                        if (device != null)
                        {
                            var app = AppsRepository.FindBy(x => x.ConnectionCode.Equals(connectionCode, StringComparison.CurrentCultureIgnoreCase) && (x.Key.Equals(key, StringComparison.CurrentCultureIgnoreCase)) || x.PreviousKey.Equals(key, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                            if(app != null)
                            {
                                if (!app.Devices.Any(x => x.Id.Equals(device.Id)))
                                {
                                    app.Devices.Add(device);
                                    this.UnitOfWork.Save();
                                }

                                return true;
                            }

                            throw new GeneralException("Invalid value of the 'id' parameter OR the value of the 'id' parameter is null or empty for the device registration.");
                        }

                        throw new GeneralException("Invalid value of the 'id' parameter OR the value of the 'id' parameter is null or empty for the device registration.");
                    }

                    throw new GeneralException("Invalid value of the 'id' parameter OR the value of the 'id' parameter is null or empty for the device registration.");
                }

                throw new GeneralException("Invalid value of the 'id' parameter OR the value of the 'id' parameter is null or empty for the device registration.");
            }

            throw new GeneralException("Invalid value of the 'registrationToken' parameter OR the value of the 'registrationToken' parameter is null or empty for the device registration.");
        }

        public DeviceEntity DipnRegisterDevice(Guid id, string registrationToken, string type)
        {
            var deviceType = DeviceTypesRepository.FindBy(x => x.Name.Equals(type, StringComparison.CurrentCultureIgnoreCase) && x.Enabled).FirstOrDefault();

            if (deviceType != null)
            {
                if (!id.Equals(Guid.Empty))
                {
                    if (!string.IsNullOrEmpty(registrationToken))
                    {
                        var dateTime = DateTime.UtcNow;
                        var device = DevicesRepository.FindBy(x => x.Id.Equals(id)).FirstOrDefault();

                        if (device == null)
                        {
                            var newDevice = new DeviceEntity
                            {
                                Id = id,
                                RegistrationToken = registrationToken,
                                TypeId = deviceType.Id,
                                Type = deviceType,
                                Enabled = true,
                                DateTimeCreated = dateTime,
                            };

                            this.DevicesRepository.Add(newDevice);
                            this.UnitOfWork.Save();

                            return newDevice;
                        }
                        else
                        {
                            device.RegistrationToken = registrationToken;
                            device.DateTimeLastModified = dateTime;

                            this.DevicesRepository.Update(device);
                            this.UnitOfWork.Save();

                            return device;
                        }
                    }

                    throw new GeneralException("Invalid value of the 'id' parameter OR the value of the 'id' parameter is null or empty for the device registration.");
                }

                throw new GeneralException("Invalid value of the 'registrationToken' parameter OR the value of the 'registrationToken' parameter is null or empty for the device registration.");
            }

            throw new GeneralException(string.Format("Invalid value of the 'type' parameter. There is no DeviceType found with the value '{0}'.", type));
        }

        public AppEntity DipnRegisterApp(Guid id, string name, string type, string ip)
        {
            var appType = AppTypesRepository.FindBy(x => x.Name.Equals(type, StringComparison.CurrentCultureIgnoreCase) && x.Enabled).FirstOrDefault();

            if(appType != null)
            {
                if (!id.Equals(Guid.Empty))
                {
                    if (!string.IsNullOrEmpty(name))
                    {
                        var dateTime = DateTime.UtcNow;
                        var app = AppsRepository.FindBy(x => x.Id.Equals(id)).FirstOrDefault();

                        if (app == null)
                        {
                            var connectionCode = default(string);

                            do
                            {
                                connectionCode = RandomString.Generate(7, 7, true, true, false, false);
                            }
                            while (AppsRepository.FindBy(x => x.ConnectionCode.Equals(connectionCode)).Any());

                            var key = RandomString.Generate(4, 4, true, false, false, false);

                            var newApp = new AppEntity
                            {
                                Id = id,
                                Name = name.Trim().ToUpper(),
                                ConnectionCode = connectionCode,
                                Key = key,
                                TypeId = appType.Id,
                                Type = appType,
                                DateTimeCreated = dateTime,
                                IPs = new List<IPEntity>
                                {
                                    new IPEntity
                                    {
                                        IPAddress = ip,
                                        DateTimeCreated = dateTime,
                                        DateTimeLastModified = dateTime
                                    }
                                }
                            };

                            this.AppsRepository.Add(newApp);
                            this.UnitOfWork.Save();

                            return newApp;
                        }
                        else
                        {
                            var lastIp = app.IPs.OrderByDescending(x => x.DateTimeCreated).FirstOrDefault();
                            var key = RandomString.Generate(4, 4, true, false, false, false);

                            if (string.IsNullOrEmpty(app.Key))
                            {
                                app.Key = key;
                                app.DateTimeLastModified = dateTime;
                            }
                            else
                            {
                                app.PreviousKey = app.Key;
                                app.Key = key;
                                app.DateTimeLastModified = dateTime;
                            }

                            if (lastIp == null)
                            {
                                app.IPs.Add(new IPEntity
                                {
                                    IPAddress = ip,
                                    AppId = app.Id,
                                    DateTimeCreated = dateTime,
                                    DateTimeLastModified = dateTime
                                });
                            }
                            else
                            {
                                if (lastIp.IPAddress.Equals(ip, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    lastIp.DateTimeLastModified = dateTime;
                                }
                                else
                                {
                                    app.IPs.Add(new IPEntity
                                    {
                                        IPAddress = ip,
                                        AppId = app.Id,
                                        DateTimeCreated = dateTime,
                                        DateTimeLastModified = dateTime
                                    });
                                }
                            }

                            this.AppsRepository.Update(app);
                            this.UnitOfWork.Save();

                            return app;
                        }
                    }

                    throw new GeneralException("Invalid value of the 'id' parameter OR the value of the 'id' parameter is null or empty for the app registration.");
                }
                
                throw new GeneralException("Invalid value of the 'name' parameter OR the value of the 'name' parameter is null or empty for the app registration.");
            }

            throw new GeneralException(string.Format("Invalid value of the 'type' parameter. There is no AppType found with the value '{0}'.", type));
        }

        public IList<ClientEntity> GetClients(string email)
        {
            var clients = UsersRepository.FindBy(x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault()?
                .Clients.ToList();

            if (clients != null && clients.Any())
            {
                return clients;
            }

            throw new GeneralException(string.Format("There are no clients found for the user '{0}'!", email));
        }

        public ConfigurationEntity GetConfigurations()
        {
            return ConfigurationsRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefault();
        }

        public ClientEntity GetClient(string email, long clientId)
        {
            var clients = UsersRepository.FindBy(x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault()?
                .Clients.ToList();

            if (clients != null && clients.Any())
            {
                var client = clients.FirstOrDefault(x => x.Id.Equals(clientId));

                if (client != null)
                {
                    return client;
                }

                throw new GeneralException(string.Format("The client with the id '{0}' not found for the user '{1}'!", clientId, email));
            }

            throw new GeneralException(string.Format("There are no clients found for the user '{0}'!", email));
        }

        public IList<string> GetPriceDescriptionSuggestions(long? prodctTypeId)
        {
            var result = new List<string>();

            var products = ProductsRepository.FindBy(x => (prodctTypeId == null || x.TypeId.Equals(prodctTypeId)));

            if (products.Any())
            {
                foreach (var product in products)
                {
                    if (!string.IsNullOrEmpty(product.PriceDescription) && !result.Contains(product.PriceDescription))
                    {
                        result.Add(product.PriceDescription);
                    }
                }
            }

            return result.OrderBy(q => q).ToList();
        }

        public IList<string> GetShippingDescriptionSuggestions(long? prodctTypeId)
        {
            var result = new List<string>();

            var products = ProductsRepository.FindBy(x => (prodctTypeId == null || x.TypeId.Equals(prodctTypeId)));

            if (products.Any())
            {
                foreach (var product in products)
                {
                    if (!string.IsNullOrEmpty(product.ShippingDescription) && !result.Contains(product.ShippingDescription))
                    {
                        result.Add(product.ShippingDescription);
                    }
                }
            }

            return result.OrderBy(q => q).ToList();
        }

        public IList<ProductEntity> GetProducts(string email, long clientId)
        {
            var client = UsersRepository.FindBy(x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault()?
                .Clients
                .FirstOrDefault(x => x.Id.Equals(clientId)
            );

            if (client != null)
            {
                if (client.Products != null && client.Products.Any(x => x.Enabled))
                {
                    return client.Products.Where(x => x.Enabled).ToList();
                }

                throw new GeneralException(string.Format("There are no Products found for the Client '{0}' with the id '{1}'!", client.Name, clientId));
            }

            throw new GeneralException(string.Format("The client with the id '{0}' not found for the user '{1}'!", clientId, email));
        }

        public ProductEntity GetProduct(string email, long clientId, long productId)
        {
            var client = UsersRepository.FindBy(x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault()?
                .Clients
                .FirstOrDefault(x => x.Id.Equals(clientId)
            );

            if (client != null)
            {
                var prduct = client.Products.FirstOrDefault(x => x.Id.Equals(productId) && x.Enabled);

                if (prduct != null)
                {
                    return prduct;
                }

                throw new GeneralException(string.Format("There is no Product with the id '{0}' found for the user '{1}' and the client '{2}' with the id '{3}'!", productId, email, client.Name, clientId));
            }

            throw new GeneralException(string.Format("The client with the id '{0}' not found for the user '{1}'!", clientId, email));
        }


        public IList<ProductTypeEntity> GetProductTypes(string email, long clientId)
        {
            var client = UsersRepository.FindBy(x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault()?
                .Clients
                .FirstOrDefault(x => x.Id.Equals(clientId)
            );

            if (client != null)
            {
                if (client.ProductTypes != null && client.ProductTypes.Any())
                {
                    return client.ProductTypes.Where(x => x.Enabled).ToList();
                }

                throw new GeneralException(string.Format("There are no ProductTypes found for the Client '{0}' with the id '{1}'!", client.Name, clientId));
            }

            throw new GeneralException(string.Format("The client with the id '{0}' not found for the user '{1}'!", clientId, email));
        }

        public ProductTypeEntity GetProductType(string email, long clientId, long productTypeId)
        {
            var client = UsersRepository.FindBy(x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault()?
                .Clients
                .FirstOrDefault(x => x.Id.Equals(clientId)
            );

            if (client != null)
            {
                var prductType = client.ProductTypes.FirstOrDefault(x => x.Id.Equals(productTypeId));

                if (prductType != null)
                {
                    return prductType;
                }

                throw new GeneralException(string.Format("There is no ProductType with the id '{0}' found for the user '{1}' and the client '{2}' with the id '{3}'!", productTypeId, email, client.Name, clientId));
            }

            throw new GeneralException(string.Format("The client with the id '{0}' not found for the user '{1}'!", clientId, email));
        }

        public void SetReadCommittedSnapshotIsolation()
        {
            var sqlCommand = string.Format("ALTER DATABASE InsureMe SET ALLOW_SNAPSHOT_ISOLATION ON");
            ((DbContext)this.DataContext).Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, sqlCommand);

            sqlCommand = string.Format("ALTER DATABASE InsureMe SET READ_COMMITTED_SNAPSHOT ON");
            ((DbContext)this.DataContext).Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, sqlCommand);
        }

        public ConfigurationEntity GetDatabaseConfiguration()
        {
            return ConfigurationsRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefault();
        }

        public IList<StateEntity> GetStates()
        {
            return this.StatesRepository.FindBy(x => x.Enabled).OrderBy(x => x.Id).ToList();
        }

        public IList<RoleEntity> GetRoles()
        {
            return this.RolesRepository.FindBy(x => x.Enabled).OrderBy(x => x.Id).ToList();
        }

        public IList<FamilyTypeEntity> GetFamiltTypes()
        {
            return this.FamilyTypesRepository.FindBy(x => x.Enabled).OrderBy(x => x.Id).ToList();
        }

        public IList<LinkTypeEntity> GetLinkTypes()
        {
            return this.LinkTypesRepository.FindBy(x => x.Enabled).OrderBy(x => x.Id).ToList();
        }

        public IList<IncomeRangeEntity> GetIncomeRanges()
        {
            return this.IncomeRangesRepository.FindBy(x => x.Enabled).OrderBy(x => x.Id).ToList();
        }

        public ProductEntity DeleteProduct(string email, long id, long? clientId)
        {
            using (var transaction = (this.DataContext as DbContext).BeginTransaction(IsolationLevel.Snapshot))
            {
                try
                {
                    var p = ProductsRepository.FindBy(x => x.Id.Equals(id) && (clientId == null || x.ClientId.Equals(clientId.Value))).FirstOrDefault();

                    if (p != null)
                    {
                        if (UsersRepository.FindBy(x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault()?.Clients.FirstOrDefault(x => x.Id.Equals(p.ClientId)) != null)
                        {
                            p.States.Clear();

                            foreach (var l in LinksRepository.FindBy(x => x.ProductId.Equals(p.Id)))
                            {
                                LinksRepository.DbContext.Entry(l).State = EntityState.Deleted;
                            }

                            foreach (var v in ValuesRepository.FindBy(x => x.ProductId.Equals(p.Id)))
                            {
                                ValuesRepository.DbContext.Entry(v).State = EntityState.Deleted;
                            }

                            ProductsRepository.DbContext.Entry(p).State = EntityState.Deleted;

                            this.UnitOfWork.Save();
                            transaction.Commit();

                            return p;
                        }

                        throw new GeneralException(20101, string.Format("User with the email '{0}' has no authority to delete product(s) for the client '{1} (id: {2})'.", email, p.Client.Name, p.ClientId));
                    }

                    throw new GeneralException(20201, clientId == null ? string.Format("The product with the id '{0}' does not exists.", id) : string.Format("The product with the id '{0}' does not exists for the client with the id '{1}'.", id, clientId));
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public ProductEntity CreateOrUpdateProduct(string email, ProductEntity product)
        {
            using (var transaction = (this.DataContext as DbContext).BeginTransaction(IsolationLevel.Snapshot))
            {
                try
                {
                    if (UsersRepository.FindBy(x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault()?.Clients.FirstOrDefault(x => x.Id.Equals(product.ClientId)) != null)
                    {
                        if (UsersRepository.FindBy(x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault()?.Clients.FirstOrDefault(x => x.Id.Equals(product.ClientId))?.ProductTypes.FirstOrDefault(x => x.Id.Equals(product.TypeId)) != null)
                        {
                            var dateTime = DateTime.UtcNow;
                            var states = new List<StateEntity>();

                            foreach (var state in product.AvailableToPurchaseInStatesList)
                            {
                                var s = StatesRepository.FindBy(x => x.Code.Equals(state, StringComparison.CurrentCultureIgnoreCase) || x.Name.Equals(state, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                                if (s == null)
                                {
                                    throw new GeneralException(20103, string.Format("The state with the code/name '{0}' does not exists.", state));
                                }

                                states.Add(s);
                            }

                            if (!states.Any())
                            {
                                throw new GeneralException(20103, string.Format("The state/s for the product are not provided."));
                            }

                            if (product.Id.Equals(default(long))) // create
                            {
                                var p = ProductsRepository.Add(new ProductEntity
                                {
                                    Title = product.Title,
                                    Description = product.Description,
                                    Name = product.Name,
                                    Variation = product.Variation,
                                    TypeId = product.TypeId,
                                    ClientId = product.ClientId,
                                    FamilyTypeId = product.FamilyTypeId,
                                    Price = product.Price,
                                    PriceDescription = product.PriceDescription,
                                    IsGstApplicableOnPrice = product.IsGstApplicableOnPrice,
                                    Shipping = product.Shipping,
                                    ShippingDescription = product.ShippingDescription,
                                    IsGstApplicableOnShipping = product.IsGstApplicableOnShipping,
                                    AvailableToPurchaseInStates = string.Join(", ", states.OrderBy(x => x.Id).Select(x => x.Code)),
                                    Enabled = true,
                                    DateTimeCreated = dateTime,
                                    States = states,
                                    Links = new List<LinkEntity>()
                                });

                                if (product.Links != null && product.Links.Any())
                                {
                                    foreach (var link in product.Links)
                                    {
                                        var type = LinkTypesRepository.FindBy(x => x.Id.Equals(link.TypeId)).FirstOrDefault();

                                        if (type == null)
                                        {
                                            throw new GeneralException(20104, string.Format("There is no link type with the id '{0}' found.", link.TypeId));
                                        }

                                        p.Links.Add(new LinkEntity
                                        {
                                            ProductId = p.Id,
                                            TypeId = link.TypeId,
                                            Type = type,
                                            Url = link.Url,
                                            Hint = link.Hint,
                                            Enabled = true,
                                            DateTimeCreated = dateTime
                                        });
                                    }
                                }

                                this.UnitOfWork.Save();
                                transaction.Commit();

                                return p;
                            }
                            else // update
                            {
                                var p = ProductsRepository.FindBy(x => x.Id.Equals(product.Id)).FirstOrDefault();

                                if (p != null)
                                {
                                    p.Title = product.Title;
                                    p.Description = product.Description;
                                    p.Name = product.Name;
                                    p.Variation = product.Variation;
                                    p.TypeId = product.TypeId.Equals(default(long)) ? p.TypeId : product.TypeId;
                                    p.ClientId = product.ClientId.Equals(default(long)) ? p.ClientId : product.ClientId;
                                    p.FamilyTypeId = product.FamilyTypeId.Equals(default(long)) ? p.FamilyTypeId : product.FamilyTypeId;
                                    p.Price = product.Price;
                                    p.PriceDescription = product.PriceDescription;
                                    p.IsGstApplicableOnPrice = product.IsGstApplicableOnPrice;
                                    p.Shipping = product.Shipping;
                                    p.ShippingDescription = product.ShippingDescription;
                                    p.IsGstApplicableOnShipping = product.IsGstApplicableOnShipping;
                                    p.Enabled = true;
                                    p.DateTimeLastModified = dateTime;

                                    p.AvailableToPurchaseInStates = string.Join(", ", states.OrderBy(x => x.Id).Select(x => x.Code));
                                    p.States.Clear();

                                    foreach (var state in states)
                                    {
                                        p.States.Add(state);
                                    }

                                    var linksToremove = (from link in p.Links where product.Links.FirstOrDefault(x => x.Id.Equals(link.Id)) == null select link.Id).ToList();

                                    foreach (var link in product.Links)
                                    {
                                        if (link.Id.Equals(default(long)))
                                        {
                                            var type = LinkTypesRepository.FindBy(x => x.Id.Equals(link.TypeId)).FirstOrDefault();

                                            if (type == null)
                                            {
                                                throw new GeneralException(20104, string.Format("There is no link type with the id '{0}' found.", link.TypeId));
                                            }

                                            p.Links.Add(new LinkEntity
                                            {
                                                ProductId = p.Id,
                                                TypeId = link.TypeId,
                                                Type = type,
                                                Url = link.Url,
                                                Hint = link.Hint,
                                                Enabled = true,
                                                DateTimeCreated = dateTime
                                            });
                                        }
                                        else
                                        {
                                            var l = p.Links.FirstOrDefault(x => x.Id.Equals(link.Id));

                                            if (l == null)
                                            {
                                                throw new GeneralException(20105, string.Format("There is no link with the id '{0}' exists associated with product with the id '{1}'.", link.Id, product.Id));
                                            }

                                            var type = LinkTypesRepository.FindBy(x => x.Id.Equals(link.TypeId)).FirstOrDefault();

                                            if (type == null)
                                            {
                                                throw new GeneralException(20104, string.Format("There is no link type with the id '{0}' found.", link.TypeId));
                                            }

                                            l.TypeId = link.TypeId;
                                            l.Type = type;
                                            l.Url = link.Url;
                                            l.Hint = link.Hint;
                                            l.Enabled = true;
                                            l.DateTimeLastModified = dateTime;
                                        }
                                    }

                                    foreach (var l in linksToremove)
                                    {
                                        var entry = LinksRepository.FindBy(x => x.Id.Equals(l)).FirstOrDefault();

                                        if (entry != null)
                                        {
                                            LinksRepository.DbContext.Entry(entry).State = EntityState.Deleted;
                                        }
                                    }

                                    this.UnitOfWork.Save();
                                    transaction.Commit();

                                    return p;
                                }

                                throw new GeneralException(20103, string.Format("There is no product with the id '{0}' exists which can be updated.", product.Id));
                            }
                        }

                        throw new GeneralException(20102, string.Format("There is no product type with the id '{0}' exists for the client with the id '{1}'.", product.TypeId, product.ClientId));
                    }

                    throw new GeneralException(20101, string.Format("User with the email '{0}' has no authority to create/update product(s) for the client with the id '{1}'.", email, product.ClientId));
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public UserEntity VerifyUser(string key, string code, out bool verified)
        {
            using (var transaction = (this.DataContext as DbContext).BeginTransaction(IsolationLevel.Snapshot))
            {
                try
                {
                    verified = false;

                    if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(code))
                    {
                        throw new GeneralException(10609, string.Format("User can't be verified. The provided key '{0}' OR the code '{1}' is null, empty or invalid!", key, code));
                    }

                    var user = this.UsersRepository.FindBy(x => x.Key.Equals(key, StringComparison.CurrentCultureIgnoreCase) && x.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                    if (user != null)
                    {
                        if (!user.Verified)
                        {
                            user.Verified = true;
                            user.DateTimeLastModified = DateTime.Now;

                            this.UsersRepository.Update(user);

                            this.UnitOfWork.Save();
                            transaction.Commit();

                            verified = true;
                        }

                        return user;
                    }

                    throw new GeneralException(10610, string.Format("There was no user found to be verified with the provided key '{0}' OR the code '{1}' has been expired or changed or invalid!", key, code));
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public IList<UserEntityTrimmed> GetUsers(int detailsLevel)
        {
            var users = UsersRepository
                .GetAll()
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Select(user => new UserEntityTrimmed
                    {
                        Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email, Role = detailsLevel == 1 ? default(string) : user.Role.Name, Enabled = detailsLevel == 1 ? default(bool?) : user.Enabled, Verified = detailsLevel == 1 ? default(bool?) : user.Verified
                    }
                ).ToList();

            if (users.Any())
            {
                return users;
            }

            throw new GeneralException(10608, string.Format("There are no users exists!"));
        }

        public IList<UserEntity> GetUsers()
        {
            var users = UsersRepository
                .GetAll()
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ToList();

            if (users.Any())
            {
                return users;
            }

            throw new GeneralException(10608, string.Format("There are no users exists!"));
        }

        public UserEntity GetUser(string address)
        {
            var user = UsersRepository.FindBy(x => x.Email.Equals(address.Trim().ToLower(), StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (user != null)
            {
                return user;
            }

            throw new GeneralException(10601, string.Format("User with the email address '{0}' does not exists!", address.Trim().ToLower()));
        }

        public UserEntity GetUser(long id)
        {
            var user = UsersRepository.FindBy(x => x.Id.Equals(id)).FirstOrDefault();

            if (user != null)
            {
                return user;
            }

            throw new GeneralException(10601, string.Format("User with the id '{0}' does not exists!", id));
        }

        public TokenEntity GetToken(string userEmail)
        {
            return GetToken(UsersRepository.FindBy(x => x.Email.Equals(userEmail.Trim().ToLower(), StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault());
        }

        public TokenEntity GetToken(UserEntity user)
        {
            using (var transaction = (this.DataContext as DbContext).BeginTransaction(IsolationLevel.Snapshot))
            {
                try
                {
                    if (user != null && user.Id >= 0)
                    {
                        if (user.Enabled)
                        {
                            if (user.Verified)
                            {
                                var dateTime = DateTime.UtcNow;
                                var token = TokensRepository.FindBy(x => x.UserId.Equals(user.Id) && x.DateTimeExpire > dateTime).OrderByDescending(x => x.DateTimeCreated).FirstOrDefault();

                                if (token == null)
                                {
                                    var lifeSpan = ConfigurationsRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefault()?.TokenLifeSpanMinutes;
                                    var expire = dateTime.AddMinutes((lifeSpan != default(double?) && !lifeSpan.Value.Equals(0) ? lifeSpan.Value : 5.0)); //Config.TokenRelatedConfiguration.TokenValidityMinutes
                                    var tokenString = user.GenerateToken(GetDatabaseConfiguration()?.BackEndKey, dateTime, expire);

                                    var newToken = TokensRepository.Add(new TokenEntity
                                    {
                                        Token = tokenString,
                                        UserId = user.Id,
                                        DateTimeCreated = dateTime,
                                        DateTimeExpire = expire,
                                        User = user
                                    });

                                    this.UnitOfWork.Save();
                                    transaction.Commit();
                                    
                                    return newToken;
                                }

                                transaction.Commit();
                                return token;
                            }

                            throw new GeneralException(10603, string.Format("User with the email address '{0}' has been marked as not verified!", user.Email.Trim().ToLower()));
                        }

                        throw new GeneralException(10602, string.Format("User with the email address '{0}' has been marked as disabled!", user.Email.Trim().ToLower()));
                    }

                    throw new GeneralException(10601, string.Format("User with the email address '{0}' does not exists!", user?.Email?.Trim().ToLower()));
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public UserEntity LoginUser(LoginUserRequest request)
        {
            var user = UsersRepository.FindBy(x => x.Email.Equals(request.Email.Trim().ToLower(), StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (user != null)
            {
                if (user.Enabled)
                {
                    if (user.Verified)
                    {
                        if (user.Password.Equals(request.Password.Trim().HashMD5(), StringComparison.CurrentCulture))
                        {
                            return user;
                        }

                        throw new GeneralException(10605, string.Format("The password you have provided for the user '{0}' is incorrect!", request.Email.Trim().ToLower()));
                    }

                    throw new GeneralException(10603, string.Format("User '{0}' is exists, but, the email address on the account has not been verified! Please check your email and click on the link provided in email to verify the account!", request.Email.Trim().ToLower()));
                }

                throw new GeneralException(10602, string.Format("User '{0}' is exists, but, the account may be terminated or blocked! Please contact us!", request.Email.Trim().ToLower()));
            }

            throw new GeneralException(10601, string.Format("User with the email address '{0}' does not exists!", request.Email.Trim().ToLower()));
        }

        public UserEntity RegisterUser(RegisterUserRequest request)
        {
            using (var transaction = (this.DataContext as DbContext).BeginTransaction(IsolationLevel.Snapshot))
            {
                try
                {
                    if (!UsersRepository.FindBy(x => x.Email.Equals(request.Email, StringComparison.CurrentCultureIgnoreCase)).Any())
                    {
                        //var role = RolesRepository.FindBy(x => x.Name.Equals(request.Role.Trim(), StringComparison.CurrentCultureIgnoreCase) || x.Code.Equals(request.Role.Trim(), StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                        //var familyType = FamilyTypesRepository.FindBy(x => x.Name.Equals(request.FamilyType.Trim(), StringComparison.CurrentCultureIgnoreCase) || x.Code.Equals(request.FamilyType.Trim(), StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                        //var state = StatesRepository.FindBy(x => x.Name.Equals(request.State.Trim(), StringComparison.CurrentCultureIgnoreCase) || x.Code.Equals(request.State.Trim(), StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                        //var incomeRange = IncomeRangesRepository.FindBy(x => x.Id.Equals(1)).First();

                        var role = RolesRepository.FindBy(x => x.Id.Equals(1)).First();
                        var familyType = FamilyTypesRepository.FindBy(x => x.Id.Equals(0)).First();
                        var state = StatesRepository.FindBy(x => x.Id.Equals(0)).First();
                        var incomeRange = IncomeRangesRepository.FindBy(x => x.Id.Equals(0)).First();

                        var dateTime = DateTime.UtcNow;

                        if (role != null)
                        {
                            if (familyType != null)
                            {
                                if (state != null)
                                {
                                    if (request.Email.IsValidEmail())
                                    {
                                        var user = UsersRepository.Add(new UserEntity
                                        {
                                            FirstName = request.FirstName.Trim(),
                                            LastName = request.LastName.Trim(),
                                            BirthDate = request.BirthDate,
                                            FamilyTypeId = familyType.Id,
                                            FamilyType = familyType,
                                            StateId = state.Id,
                                            State = state,
                                            PostCode = request.PostCode?.Trim().ToUpper(),
                                            Mobile = request.Mobile?.Trim().ToUpper(),
                                            Email = request.Email.Trim().ToLower(),
                                            Password = request.Password.Trim().HashMD5(),
                                            Key = request.Email.Trim().HashMD5(),
                                            Code = string.Empty.GetUniqueString(),
                                            RoleId = role.Id,
                                            Enabled = true,
                                            Verified = false,
                                            IncomeRangeId = incomeRange.Id,
                                            IncomeRange = incomeRange,
                                            DateTimeCreated = dateTime,
                                            Role = role
                                        });

                                        this.UnitOfWork.Save();
                                        transaction.Commit();

                                        return user;
                                    }

                                    throw new GeneralException(10607, string.Format("The email address '{0}' is invalid!", request.Email.Trim().ToLower()));
                                }

                                throw new GeneralException(10202, string.Format("State with the name or code '{0}' not found!", request.State.Trim()));
                            }

                            throw new GeneralException(10302, string.Format("Family type with the name or code '{0}' not found!", request.FamilyType));
                        }

                        throw new GeneralException(10502, string.Format("Role with the name or code '{0}' not found!", request.Role));
                    }

                    throw new GeneralException(10606, string.Format("User with the email address '{0}' is already exists!", request.Email.Trim().ToLower()));
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}