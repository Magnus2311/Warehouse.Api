using System;

namespace Warehouse.Database.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class BindedPropAttribute : Attribute
    {
    }
}