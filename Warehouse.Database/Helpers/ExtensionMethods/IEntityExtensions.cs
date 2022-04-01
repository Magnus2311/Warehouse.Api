using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using Warehouse.Database.Interfaces;
using Warehouse.Database.Models.Attributes;

namespace Warehouse.Database.Helpers.ExtensionMethods
{
    public static class IEntityExtensions
    {
        public static void UpdateHistoryForChanges(this IEntity entity, IEntity newEntity)
        {
            var props = entity.GetType().GetProperties();
            var versionedProps = props.Where(prop => prop.GetCustomAttributes(true)
                .Any(att => att.GetType().FullName == typeof(BindedPropAttribute).FullName));

            foreach (var prop in versionedProps)
            {
                if (prop.PropertyType.IsClass && prop.PropertyType.Name != "String")
                {
                    var oldProp = prop.GetValue(entity) as IEntity;
                    var newProp = prop.GetValue(newEntity) as IEntity;
                    oldProp.UpdateHistoryForChanges(newProp);
                }
                else
                {
                    var oldValue = prop.GetValue(entity);
                    var newValue = prop.GetValue(newEntity);
                    var historyProp = props.FirstOrDefault(current => current.Name == prop.Name + "_history");
                    var historyVal = historyProp?.GetValue(entity);
                    var valList = historyVal != null ? (historyVal as IEnumerable<VersionedProp<dynamic>>).OrderBy(prop => prop.Version) : Enumerable.Empty<VersionedProp<dynamic>>();

                    if (!Equals(oldValue, newValue))
                    {
                        historyVal = valList.Append(new VersionedProp<dynamic>
                        {
                            UpdatedDate = DateTime.Now,
                            Version = (long)entity.GetType().GetProperties().FirstOrDefault(prop => prop.Name == "Version").GetValue(entity),
                            Value = newValue
                        });
                    }
                    historyProp.SetValue(newEntity, historyVal);
                }
            }
        }

        public static void InitHistory(this IEntity entity)
        {
            entity.Version = 1;
            var props = entity.GetType().GetProperties();
            var versionedProps = props.Where(prop => prop.GetCustomAttributes(true)
                .Any(att => att.GetType().FullName == typeof(BindedPropAttribute).FullName));

            foreach (var prop in versionedProps)
            {
                if (prop.PropertyType.IsClass && prop.PropertyType.Name != "String")
                {
                    var subProp = prop.GetValue(entity) as IEntity;
                    subProp.InitHistory();
                }
                else
                {
                    var historyProp = props.FirstOrDefault(current => current.Name == prop.Name + "_history");
                    IEnumerable<VersionedProp<dynamic>> historyVal = new List<VersionedProp<dynamic>> { new VersionedProp<dynamic>
                    {
                        Version = entity.Version,
                        UpdatedDate = DateTime.Now,
                        Value = prop.GetValue(entity)
                    } };

                    historyProp.SetValue(entity, historyVal);
                }
            }
        }
    }
}
