namespace AspNetCore.DistributedCache.Tools
{
    public interface IDistributedCacheRepository
    {
        T GetCache<T>(string cacheKey);

        bool InsertCache<T>(string cacheKey, T data);

        void UpdateStateCache(string cacheKey, ECacheStatus cacheStatus);

        CacheStatus GetStateCache(string cacheKey);
    }
}
