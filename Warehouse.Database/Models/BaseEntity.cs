using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using Warehouse.Database.Helpers;
using Warehouse.Database.Interfaces;
using Warehouse.Database.Models.Attributes;

namespace Warehouse.Database.Models
{
    [BsonIgnoreExtraElements]
    public abstract class BaseEntity : IEntity
    {
        public BaseEntity()
        {
            var props = GetType().GetProperties();
            foreach (var prop in props)
            {
                if (prop.GetCustomAttributes(true).Any(attribute => attribute.GetType().FullName == typeof(BindedPropAttribute).FullName))
                {
                    var type = prop.GetType();
                    var historyValue = props.FirstOrDefault(current => current.Name == prop.Name + "_history")?.GetValue(this);
                    var historyProp = historyValue != null ? (historyValue as IEnumerable<VersionedProp>).OrderBy(prop => prop.Version) : Enumerable.Empty<VersionedProp>();
                    var latestVersion = historyProp.LastOrDefault();
                    if (latestVersion != null)
                    {
                        if (latestVersion.Version > Version)
                            Version = latestVersion.Version;

                        prop.SetValue(this, Convert.ChangeType(latestVersion.Value, prop.PropertyType));
                    }
                }
            }
        }

        [BsonId]
        public ObjectId Id { get; set; }
        public bool IsDeleted { get; set; }
        public long Version { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
