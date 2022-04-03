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
        public static void UpdateHistoryForChanges(this IEntity entity, IEntity newEntity, long? Version = null)
        {
            var props = entity.GetType().GetProperties();
            var versionedProps = props.Where(prop => prop.GetCustomAttributes(true)
                .Any(att => att.GetType().FullName == typeof(BindedPropAttribute).FullName));
            var historyProps = props.Where(prop => prop.GetCustomAttributes(true)
                .Any(att => att.GetType().FullName == typeof(HistoryPropAttribute).FullName));

            foreach (var prop in versionedProps)
            {
                if (prop.PropertyType.IsClass && prop.PropertyType.Name != "String")
                {
                    if ((prop.GetCustomAttributes(true)[0] as BindedPropAttribute).PropType == BindedPropType.IEnumerable)
                    {
                        var oldCollection = prop.GetValue(entity) as List<IEntity>;
                        var newCollection = prop.GetValue(newEntity) as List<IEntity>;

                        if (oldCollection.Count() > newCollection.Count())
                        {
                            for (int i = 0; i < oldCollection.Count(); i++)
                            {
                                if (newCollection.Count() - 1 <= i)
                                {
                                    oldCollection[i].UpdateHistoryForChanges(newCollection[i], newEntity.Version);
                                }
                                else
                                {
                                    var nestedProp = oldCollection[i]; ...
                                }
                            }
                        }
                    }
                    else
                    {
                        var oldProp = prop.GetValue(entity) as IEntity;
                        var newProp = prop.GetValue(newEntity) as IEntity;
                        oldProp.UpdateHistoryForChanges(newProp);
                    }
                }
                else
                {
                    var oldValue = prop.GetValue(entity);
                    var newValue = prop.GetValue(newEntity);
                    var historyProp = props
                        .FirstOrDefault(current =>
                            (current.GetCustomAttributes(true)[0] as HistoryPropAttribute).BindedProp == prop.Name);

                    var historyVal = historyProp?.GetValue(entity);
                    var valList = historyVal != null ? (historyVal as IEnumerable<VersionedProp>).OrderBy(prop => prop.Version) : Enumerable.Empty<VersionedProp>();

                    if (!Equals(oldValue, newValue))
                    {
                        historyVal = valList.Append(new VersionedProp
                        {
                            UpdatedDate = DateTime.Now,
                            Version = Version ?? (long)entity.GetType().GetProperties().FirstOrDefault(prop => prop.Name == "Version").GetValue(entity),
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
                    IEnumerable<VersionedProp> historyVal = new List<VersionedProp> { new VersionedProp
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
