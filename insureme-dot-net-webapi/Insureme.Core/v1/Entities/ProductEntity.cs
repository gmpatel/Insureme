using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insureme.Core.v1.Objects.Responses;
using Newtonsoft.Json;

namespace Insureme.Core.v1.Entities
{
    public class ProductEntity
    {
        [JsonProperty(PropertyName = "id", Order = 1, DefaultValueHandling = DefaultValueHandling.Include)]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "title", Order = 2, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "description", Order = 3, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "name", Order = 4, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "variation", Order = 5, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Variation { get; set; }

        [NotMapped]
        [JsonIgnore]
        //[JsonProperty(PropertyName = "client", Order = 6, DefaultValueHandling = DefaultValueHandling.Include)]
        public string ClientString => Client != null ? string.Format("{0} ({1})", Client.Name, Client.Code) : null;

        [JsonProperty(PropertyName = "clientId", Order = 7, DefaultValueHandling = DefaultValueHandling.Include)]
        public long ClientId { get; set; }

        [NotMapped]
        [JsonProperty(PropertyName = "type", Order = 8, DefaultValueHandling = DefaultValueHandling.Include)]
        public string TypeString => Type?.Title;

        [JsonProperty(PropertyName = "typeId", Order = 9, DefaultValueHandling = DefaultValueHandling.Include)]
        public long TypeId { get; set; }

        [NotMapped]
        [JsonProperty(PropertyName = "familyType", Order = 10, DefaultValueHandling = DefaultValueHandling.Include)]
        public string FamilyTypeString => FamilyType?.Name;

        [JsonProperty(PropertyName = "familyTypeId", Order = 11, DefaultValueHandling = DefaultValueHandling.Include)]
        public long FamilyTypeId { get; set; }

        [JsonProperty(PropertyName = "price", Order = 12, DefaultValueHandling = DefaultValueHandling.Include)]
        public float Price { get; set; }

        [JsonProperty(PropertyName = "priceDescription", Order = 13, DefaultValueHandling = DefaultValueHandling.Include)]
        public string PriceDescription { get; set; }

        [NotMapped]
        [JsonProperty(PropertyName = "priceDescriptionSuggestions", Order = 14, DefaultValueHandling = DefaultValueHandling.Include)]
        public IList<string> PriceDescriptionSuggestions { get; set; }
                                                  
        [JsonProperty(PropertyName = "gstOnPrice", Order = 15, DefaultValueHandling = DefaultValueHandling.Include)]
        public bool IsGstApplicableOnPrice { get; set; }

        [JsonProperty(PropertyName = "shipping", Order = 16, DefaultValueHandling = DefaultValueHandling.Include)]
        public float Shipping { get; set; }

        [JsonProperty(PropertyName = "shippingDescription", Order = 17, DefaultValueHandling = DefaultValueHandling.Include)]
        public string ShippingDescription { get; set; }

        [NotMapped]
        [JsonProperty(PropertyName = "shippingDescriptionSuggestions", Order = 18, DefaultValueHandling = DefaultValueHandling.Include)]
        public IList<string> ShippingDescriptionSuggestions { get; set; }

        [JsonProperty(PropertyName = "gstOnShipping", Order = 19, DefaultValueHandling = DefaultValueHandling.Include)]
        public bool IsGstApplicableOnShipping { get; set; }

        [JsonProperty(PropertyName = "states", Order = 20, DefaultValueHandling = DefaultValueHandling.Include)]
        public string AvailableToPurchaseInStates { get; set; }

        [NotMapped]
        [JsonIgnore]
        //[JsonProperty(PropertyName = "states", Order = 21, DefaultValueHandling = DefaultValueHandling.Include)]
        public IList<string> AvailableToPurchaseInStatesList => (AvailableToPurchaseInStates ?? string.Empty).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim().ToUpper()).OrderBy(x => x).ToList();

        [JsonProperty(PropertyName = "client", Order = 22, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual ClientEntity Client { get; set; }

        [JsonProperty(PropertyName = "links", Order = 23, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual ICollection<LinkEntity> Links { get; set; }

        [NotMapped]
        [JsonProperty(PropertyName = "dataSheet", Order = 24, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual DataSheetEntity DataSheet { get; set; }

        [JsonIgnore]
        public bool Enabled { get; set; }

        [JsonIgnore]
        public DateTime DateTimeCreated { get; set; }

        [JsonIgnore]
        public DateTime? DateTimeLastModified { get; set; }

        [JsonIgnore]
        public virtual ProductTypeEntity Type { get; set; }

        [JsonIgnore]
        public virtual FamilyTypeEntity FamilyType { get; set; }

        [JsonIgnore]
        public virtual ICollection<ValueEntity> Values { get; set; }

        [JsonIgnore]
        public virtual ICollection<StateEntity> States { get; set; }
    }
}