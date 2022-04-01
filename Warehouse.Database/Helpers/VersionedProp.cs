using System;

namespace Warehouse.Database.Helpers
{
    public class VersionedProp<T>
    {
        public long Version { get; set; }
        public DateTime UpdatedDate { get; set; }
        public T Value { get; set; }
    }
}
