using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insureme.Core.v1.Entities;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Insureme.Core.v1.Entities
{
    public class DataSheetEntity
    {
        [JsonProperty("zones", Order = 1, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IList<ZoneResponse> Zones { get; set; }
        [JsonProperty("lists", Order = 1, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IDictionary<long, IList<string>> Lists { get; set; }

        public static DataSheetEntity Load(ProductEntity product)
        {
            var valuesDictionary = new Dictionary<long, ValueEntity>();
            var result = default(DataSheetEntity);

            if (product?.Type != null && product.Type.Zones.Any())
            {                
                foreach (var value in product.Values)
                {
                    if (!valuesDictionary.ContainsKey(value.PropertyId))
                    {
                        valuesDictionary.Add(value.PropertyId, value);
                    }
                }

                result = new DataSheetEntity
                {
                    Zones = new List<ZoneResponse>(),
                    Lists = new Dictionary<long, IList<string>>()
                };

                foreach (var zone in product.Type.Zones)
                {
                    if (zone.Enabled)
                    {
                        var z = new ZoneResponse
                        {
                            Id = zone.Id,
                            Title = zone.Title,
                            Groups = new List<GroupResponse>()
                        };

                        if (zone.Groups.Any())
                        {
                            foreach (var group in zone.Groups)
                            {
                                if (group.Enabled)
                                {
                                    var g = new GroupResponse
                                    {
                                        Id = group.Id,
                                        Title = group.Title,
                                        Properties = new List<PropertyResponse>()
                                    };

                                    if (group.Properties.Any())
                                    {
                                        g.Properties = GetProperties(group.Properties, result, valuesDictionary);
                                    }

                                    z.Groups.Add(g);
                                }
                            }
                        }

                        result.Zones.Add(z);
                    }
                }
            }

            return result;
        }

        public static DataSheetEntity Load(ProductTypeEntity productType)
        {
            var valuesDictionary = new Dictionary<long, ValueEntity>();
            var result = default(DataSheetEntity);

            if (productType != null && productType.Zones.Any())
            {
                result = new DataSheetEntity
                {
                    Zones = new List<ZoneResponse>(),
                    Lists = new Dictionary<long, IList<string>>()
                };

                foreach (var zone in productType.Zones)
                {
                    if (zone.Enabled)
                    {
                        var z = new ZoneResponse
                        {
                            Id = zone.Id,
                            Title = zone.Title,
                            Groups = new List<GroupResponse>()
                        };

                        if (zone.Groups.Any())
                        {
                            foreach (var group in zone.Groups)
                            {
                                if (group.Enabled)
                                {
                                    var g = new GroupResponse
                                    {
                                        Id = group.Id,
                                        Title = group.Title,
                                        Properties = new List<PropertyResponse>()
                                    };

                                    if (group.Properties.Any())
                                    {
                                        g.Properties = GetProperties(group.Properties, result, valuesDictionary);
                                    }

                                    z.Groups.Add(g);
                                }
                            }
                        }

                        result.Zones.Add(z);
                    }                    
                }
            }

            return result;
        }

        private static IList<PropertyResponse> GetProperties(IEnumerable<PropertyEntity> properties, DataSheetEntity infoResponse, Dictionary<long, ValueEntity> valuesDictionary)
        {
            var result = new List<PropertyResponse>();

            foreach (var property in properties)
            {
                if (property.Enabled)
                {
                    var prop = new PropertyResponse
                    {
                        Id = property.Id,
                        Title = property.Title,
                        Type = property.TypeString,
                        TypeId = property.TypeId,
                        Types = property.TypeString.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList(),
                        InputType = property.InputTypeString,
                        InputTypeId = property.InputTypeId,
                        Mandatory = property.Mandatory,
                        PossibleNullNumber = property.PossibleNullNumber,
                        PossibleComments = property.PossibleComments,
                        ValueType = valuesDictionary.ContainsKey(property.Id) ? valuesDictionary[property.Id].TypeString : default(string),
                        ValueTypeId = valuesDictionary.ContainsKey(property.Id) ? valuesDictionary[property.Id].TypeId : default(long),
                        Value = ConvertToType(valuesDictionary.ContainsKey(property.Id) ? valuesDictionary[property.Id] : null),
                        ValueUnit = valuesDictionary.ContainsKey(property.Id) ? valuesDictionary[property.Id].Unit : default(string),
                        ValueComments = valuesDictionary.ContainsKey(property.Id) ? valuesDictionary[property.Id].Comments : default(string),
                        Hint = property.Hint,
                        ValuesListId = property.ValuesListId,
                        UnitsListId = property.UnitsListId,
                        ValidationRegex = property.ValidationRegex,
                        ValidationRegexHint = property.ValidationRegexHint
                    };

                    /*
                    if (prop.TypeId.Equals(prop.ValueTypeId) && prop.Type.Equals(prop.ValueType))
                    {
                        prop.ValueTypeId = default(long);
                        prop.ValueType = default(string);
                    }
                    */
                        
                    if (property.ValuesListId > 0 && property.ValuesList != null)
                        UpdateLists(property.ValuesList, infoResponse);

                    if (property.UnitsListId > 0 && property.UnitsList != null)
                        UpdateLists(property.UnitsList, infoResponse);

                    if (property.Children != null && property.Children.Any())
                    {
                        prop.Children = GetProperties(property.Children, infoResponse, valuesDictionary);
                    }

                    result.Add(prop);
                }
            }

            if (!result.Any())
                result = null;

            return result;
        }

        private static void UpdateLists(ListEntity list, DataSheetEntity infoResponse)
        {
            var result = new List<string>();

            if (list != null && list.Id > 0 && list.Enabled && !infoResponse.Lists.ContainsKey(list.Id))
            {
                foreach (var item in list.ListItems)
                {
                    if(item.Enabled) result.Add(item.Name);
                }
                
                infoResponse.Lists.Add(new KeyValuePair<long, IList<string>>(list.Id, result));
            }
        }

        private static dynamic ConvertToType(ValueEntity value)
        {
            dynamic result = default (dynamic);

            if (!string.IsNullOrEmpty(value?.Value))
            {
                switch (value.TypeId)
                {
                    case 1: //Boolean
                        {
                            var r = bool.Parse(value.Value);
                            result = r;
                            break;
                        }
                    case 2: //Currency
                        {
                            var r = float.Parse(float.Parse(value.Value).ToString("#0.00"));
                            result = r;
                            break;
                        }
                    case 3: //DateTime
                        {
                            var r = DateTime.Parse(value.Value);
                            result = r;
                            break;
                        }
                    case 4: //Decimal
                        {
                            var r = float.Parse(value.Value);
                            result = r;
                            break;
                        }
                    case 5: //Number
                        {
                            var r = long.Parse(value.Value);
                            result = r;
                            break;
                        }
                    case 6: //NumberWithSeparator
                        {
                            var r = long.Parse(value.Value);
                            result = r;
                            break;
                        }
                    case 7: //Percentage
                        {
                            var r = float.Parse(value.Value);
                            result = r;
                            break;
                        }
                    case 8: //Text
                        {
                            result = value.Value?.Trim();
                            break;
                        }
                    case 9: //DecimalWithSeparator
                        {
                            var r = float.Parse(value.Value);
                            result = r;
                            break;
                        }
                }
            }

            return result ?? null;
        }
    }

    public class ZoneResponse
    {
        [JsonProperty("id", Order = 1, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long Id { get; set; }

        [JsonProperty("title", Order = 2, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("groups", Order = 3, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IList<GroupResponse> Groups { get; set; }
    }

    public class GroupResponse
    {
        [JsonProperty("id", Order = 1, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long Id { get; set; }

        [JsonProperty("title", Order = 2, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("properties", Order = 3, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IList<PropertyResponse> Properties { get; set; }
    }

    public class PropertyResponse
    {
        [JsonProperty("id", Order = 1, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long Id { get; set; }

        [JsonProperty("title", Order = 2, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonIgnore]
        //[JsonProperty(PropertyName = "type", Order = 3, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonIgnore]
        //[JsonProperty(PropertyName = "typeId", Order = 4, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long TypeId { get; set; }

        [JsonProperty(PropertyName = "types", Order = 5, DefaultValueHandling = DefaultValueHandling.Include)]
        public IList<string> Types { get; set; }

        [JsonProperty("inputType", Order = 6, DefaultValueHandling = DefaultValueHandling.Include)]
        public string InputType { get; set; }

        [JsonIgnore]
        //[JsonProperty("inputTypeId", Order = 7, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long InputTypeId { get; set; }

        [JsonProperty(PropertyName = "mandatory", Order = 8, DefaultValueHandling = DefaultValueHandling.Include)]
        public bool Mandatory { get; set; }

        [JsonIgnore]
        //[JsonProperty(PropertyName = "possibleNullNumber", Order = 9, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool PossibleNullNumber { get; set; }

        [JsonProperty(PropertyName = "possibleComments", Order = 10, DefaultValueHandling = DefaultValueHandling.Include)]
        public bool PossibleComments { get; set; }

        [JsonProperty(PropertyName = "valueType", Order = 11, DefaultValueHandling = DefaultValueHandling.Include)]
        public string ValueType { get; set; }

        [JsonIgnore]
        //[JsonProperty(PropertyName = "valueTypeId", Order = 12, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long ValueTypeId { get; set; }

        [JsonProperty("value", Order = 13, DefaultValueHandling = DefaultValueHandling.Include)]
        public dynamic Value { get; set; }

        [JsonProperty(PropertyName = "valueUnit", Order = 14, DefaultValueHandling = DefaultValueHandling.Include)]
        public string ValueUnit { get; set; }

        [JsonProperty(PropertyName = "valueComments", Order = 15, DefaultValueHandling = DefaultValueHandling.Include)]
        public string ValueComments { get; set; }
        
        [JsonProperty("hint", Order = 16, DefaultValueHandling = DefaultValueHandling.Include)]
        public string Hint { get; set; }

        [JsonProperty("valuesListId", Order = 17, DefaultValueHandling = DefaultValueHandling.Include)]
        public long ValuesListId { get; set; }

        [JsonProperty("unitsListId", Order = 18, DefaultValueHandling = DefaultValueHandling.Include)]
        public long UnitsListId { get; set; }

        [JsonProperty("regex", Order = 19, DefaultValueHandling = DefaultValueHandling.Include)]
        public string ValidationRegex { get; set; }

        [JsonProperty("regexHint", Order = 20, DefaultValueHandling = DefaultValueHandling.Include)]
        public string ValidationRegexHint { get; set; }

        [JsonProperty("children", Order = 21, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IList<PropertyResponse> Children { get; set; }
    }
}