using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Insureme.Core.v1.Entities
{
    public class PropertyEntity
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonIgnore]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [NotMapped]
        [JsonProperty(PropertyName = "type")]
        public string TypeString => Type.Name;

        [JsonProperty(PropertyName = "typeId")]
        public long TypeId { get; set; }

        [NotMapped]
        [JsonProperty(PropertyName = "inputType")]
        public string InputTypeString => InputType.Name;

        [JsonProperty(PropertyName = "inputTypeId")]
        public long InputTypeId { get; set; }

        [JsonProperty(PropertyName = "mandatory")]
        public bool Mandatory { get; set; }

        [JsonProperty(PropertyName = "possibleNullNumber")]
        public bool PossibleNullNumber { get; set; }

        [JsonProperty(PropertyName = "possibleComments")]
        public bool PossibleComments { get; set; }

        [JsonProperty(PropertyName = "hint")]
        public string Hint { get; set; }

        [JsonProperty(PropertyName = "valuesListId", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long ValuesListId { get; set; }

        [JsonProperty(PropertyName = "unitsListId", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long UnitsListId { get; set; }

        [JsonProperty(PropertyName = "regex", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ValidationRegex { get; set; }

        [JsonProperty(PropertyName = "regexHint", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ValidationRegexHint { get; set; }

        [JsonProperty(PropertyName = "parentId", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long? ParentId { get; set; }

        [JsonIgnore]
        public bool Enabled { get; set; }

        [JsonIgnore]
        public DateTime DateTimeCreated { get; set; }

        [JsonIgnore]
        public DateTime? DateTimeLastModified { get; set; }

        [JsonIgnore]
        public virtual InputTypeEntity InputType { get; set; }

        [JsonIgnore]
        public virtual ValueTypeEntity Type { get; set; }

        [JsonIgnore]
        public virtual ListEntity ValuesList { get; set; }

        [JsonIgnore]
        public virtual ListEntity UnitsList { get; set; }

        [JsonIgnore]
        public virtual PropertyEntity Parent { get; set; }

        [JsonIgnore]
        public virtual ICollection<ValueEntity> Values { get; set; }

        [JsonIgnore]
        public virtual ICollection<PropertyEntity> Children { get; set; }

        [JsonIgnore]
        public virtual ICollection<GroupEntity> Groups { get; set; }
    }
}