using System;

namespace Warehouse.Database.Helpers
{
    public class VersionedProp
    {
        public long Version { get; set; }
        public DateTime UpdatedDate { get; set; }
        public dynamic Value { get; set; }
    }
}
