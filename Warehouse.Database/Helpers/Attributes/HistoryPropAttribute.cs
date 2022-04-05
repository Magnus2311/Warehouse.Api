using System;

namespace Warehouse.Database.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class HistoryPropAttribute : Attribute
    {
        public HistoryPropAttribute(string bindedProp)
        {
            BindedProp = bindedProp;
        }

        public string BindedProp { get; set; }
    }
}