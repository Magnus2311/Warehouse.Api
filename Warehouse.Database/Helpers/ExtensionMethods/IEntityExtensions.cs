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
                    var oldProp = prop.GetValue(entity) as IEntity;
                    var newProp = prop.GetValue(newEntity) as IEntity;
                    oldProp.UpdateHistoryForChanges(newProp);
                }
                else if (prop.PropertyType.Name.Contains("IEnumerable"))
                {
                    if ((prop.GetCustomAttributes(true)[0] as BindedPropAttribute).PropType == BindedPropType.IEnumerable)
                    {
                        var oldCollection = (prop.GetValue(entity) as IEnumerable<object>).ToList();
                        var newCollection = (prop.GetValue(newEntity) as IEnumerable<object>).ToList();

                        if (oldCollection.Count() >= newCollection.Count())
                        {
                            for (int i = 0; i < oldCollection.Count(); i++)
                            {
                                var currentEntity = oldCollection[i] as IEntity;
                                if (newCollection.Count() - 1 <= i)
                                {
                                    currentEntity.UpdateHistoryForChanges(newCollection[i] as IEntity, newEntity.Version);
                                }
                                else
                                {
                                    currentEntity.InitHistory(Version ?? 1);
                                }
                            }
                        }

                        if (oldCollection.Count() < newCollection.Count())
                        {
                            for (int i = 0; i < newCollection.Count(); i++)
                            {
                                var currentEntity = newCollection[i] as IEntity;
                                if (oldCollection.Count() - 1 <= i)
                                {
                                    currentEntity.UpdateHistoryForChanges(oldCollection[i] as IEntity, newEntity.Version);
                                }
                                else
                                {
                                    currentEntity.InitHistory(Version ?? 1);
                                }
                            }
                        }
                    }
                }
                else
                {
                    var oldValue = prop.GetValue(entity);
                    var newValue = prop.GetValue(newEntity);
                    var historyProp = props.Where(curr => curr.GetCustomAttributes(true)
                        .Any(att => att.GetType().FullName == typeof(HistoryPropAttribute).FullName))
                        .FirstOrDefault(curr2 =>
                            (curr2.GetCustomAttributes(true)
                                .FirstOrDefault(att => att.GetType().FullName == typeof(HistoryPropAttribute).FullName) as HistoryPropAttribute)
                                    .BindedProp == prop.Name);
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

        public static void InitHistory(this IEntity entity, long version = 1)
        {
            entity.Version = version;
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
                else if (prop.PropertyType.Name.Contains("IEnumerable"))
                {
                    if ((prop.GetCustomAttributes(true)[0] as BindedPropAttribute).PropType == BindedPropType.IEnumerable)
                    {
                        var collection = prop.GetValue(entity) as IEnumerable<object>;
                        foreach (var item in collection)
                        {
                            (item as IEntity).InitHistory();
                        }
                    }
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
