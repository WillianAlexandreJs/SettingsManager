using System;

namespace AspNetCore.DistributedCache.Tools
{
    public class CacheStatus
    {
        public ECacheStatus Status { get; set; }
        public DateTime Date { get; set; }
    }

    public enum ECacheStatus
    {
        Updating,
        Updated
    }
}
