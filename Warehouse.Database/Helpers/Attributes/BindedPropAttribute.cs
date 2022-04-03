using System;
using Warehouse.Database.Helpers;

namespace Warehouse.Database.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class BindedPropAttribute : Attribute
    {
        public BindedPropAttribute(BindedPropType propType = BindedPropType.ValueType)
        {
            PropType = propType;
        }

        public BindedPropType PropType { get; set; }
    }
}