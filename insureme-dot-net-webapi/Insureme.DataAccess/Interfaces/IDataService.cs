using System;
using System.Collections.Generic;
using Insureme.Core.v1.Entities;
using Insureme.Core.v1.Objects;
using Insureme.Core.v1.Objects.Requests;
using Insureme.Core.v1.Entities.Dipn;

namespace Insureme.DataAccess.Interfaces
{
    public interface IDataService : IDisposable
    {
        long Id { get; }
        long Instances { get; }

        AppEntity DipnHandshakeApp(Guid id, string ip);
        AppEntity DipnRegisterApp(Guid id, string name, string type, string ip);
        DeviceEntity DipnRegisterDevice(Guid id, string registrationToken, string type);
        bool DipnLinkDeviceWithApp(Guid id, string connectionCode, string key);
        void SetReadCommittedSnapshotIsolation();
        ConfigurationEntity GetConfigurations();
        IList<ClientEntity> GetClients(string email);
        ClientEntity GetClient(string email, long clientId);
        IList<string> GetPriceDescriptionSuggestions(long? prodctTypeId);
        IList<string> GetShippingDescriptionSuggestions(long? prodctTypeId);
        IList<ProductEntity> GetProducts(string email, long clientId);
        ProductEntity GetProduct(string email, long clientId, long productId);
        IList<ProductTypeEntity> GetProductTypes(string email, long clientId);
        ProductTypeEntity GetProductType(string email, long clientId, long productTypeId);
        ConfigurationEntity GetDatabaseConfiguration();
        IList<StateEntity> GetStates();
        IList<RoleEntity> GetRoles();
        IList<UserEntityTrimmed> GetUsers(int detailsLevel);
        IList<UserEntity> GetUsers();
        IList<LinkTypeEntity> GetLinkTypes();
        IList<FamilyTypeEntity> GetFamiltTypes();
        IList<IncomeRangeEntity> GetIncomeRanges();
        ProductEntity DeleteProduct(string email, long id, long? clientId);
        ProductEntity CreateOrUpdateProduct(string email, ProductEntity product);
        UserEntity RegisterUser(RegisterUserRequest request);
        UserEntity LoginUser(LoginUserRequest request);
        TokenEntity GetToken(string userEmail);
        TokenEntity GetToken(UserEntity request);
        UserEntity VerifyUser(string key, string code, out bool verified);
        UserEntity GetUser(string address);
        UserEntity GetUser(long id);
    }
}