using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace KUX.Infrastructure.Redis
{
    /// <summary>
    /// Redis 扩展
    /// </summary>
    public static class RedisExtensions
    {
        /// <summary>
        /// json 配置
        /// </summary>
        /// <value></value>
        private static JsonSerializerSettings JsonConfig
            => new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };

        /// <summary>
        /// 注册 Redis 服务
        /// </summary>
        /// <param name="service"></param>
        /// <param name="connectionString"></param>
        public static void RegisterRedisService(this IServiceCollection service, string connectionString)
            => service.AddSingleton<IRedisService, RedisService>(serviceProvider => new RedisService(connectionString));

        /// <summary>
        /// 根据 key 获取数据
        /// </summary>
        /// <param name="redisService"></param>
        /// <param name="key"></param>
        /// <param name="db">默认数据库</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> FindByKeyAsync<T>(this IRedisService redisService, string key, int db = 6)
        {
            //IDatabase database;

            //if (db >= 0 && db <= 15)
            //{
            //    database = redisService.Multiplexer.GetDatabase(db);
            //}
            //else
            //{
            //    database = redisService.Database;
            //}

            var database = redisService.GetDatabase(db);

            var redisPackageContent = await database.StringGetAsync(key);

            if (string.IsNullOrWhiteSpace(redisPackageContent))
            {
                return default;
            }

            var redisPackage = JsonConvert.DeserializeObject<RedisPackage<T>>(redisPackageContent, JsonConfig);

            return redisPackage == null ? default : redisPackage.Body;
        }

        /// <summary>
        /// 根据 key 插入 或者 修改 数据
        /// </summary>
        /// <param name="redisService"></param>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheTime"></param>
        /// <param name="db">默认db</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<bool> AddOrUpdateByKeyAsync<T>(this IRedisService redisService, string key, T data,
            DateTime? cacheTime = null, int db = 6)
        {
            var database = redisService.GetDatabase(db);

            var redisPackage = new RedisPackage<T>(data, cacheTime);

            //缓存内容
            var redisPackageContent = JsonConvert.SerializeObject(redisPackage, JsonConfig);

            TimeSpan Ts;
            if (cacheTime != null && cacheTime >= DateTime.Now)
            {
                TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
                TimeSpan ts2 = new TimeSpan(cacheTime.Value.Ticks);
                Ts = ts1.Subtract(ts2).Duration();
            }
            else
            {
                Ts = TimeSpan.FromSeconds(redisPackage.ExpirationTime);
            }

            // 设置缓存
            return await database.StringSetAsync(key, redisPackageContent, Ts);
        }

        /// <summary>
        /// 根据 key 查看是否存在
        /// </summary>
        /// <param name="redisService"></param>
        /// <param name="key"></param>
        /// <param name="db">默认db</param>
        /// <returns></returns>
        public static async Task<bool> ExistsByKeyAsync(this IRedisService redisService, string key, int db = 6)
        {
            try
            {
                var database = redisService.GetDatabase(db);

                return await database.KeyExistsAsync(key);
            }
            catch (Exception ex)
            {
                return await Task.FromResult<bool>(false);
            }
        }

        /// <summary>
        /// 根据 key 移除数据
        /// </summary>
        /// <param name="redisService"></param>
        /// <param name="key"></param>
        /// <param name="db">默认链接</param>
        /// <returns></returns>
        public static async Task<bool> DeleteByKeyAsync(this IRedisService redisService, string key, int db = 6)
        {
            var database = redisService.GetDatabase(db);

            var keyExists = await database.KeyExistsAsync(key);

            if (keyExists)
            {
                return await database.KeyDeleteAsync(key);
            }

            return true;
        }

        /// <summary>
        /// 删除对应库的所有缓存信息
        /// </summary>
        /// <param name="redisService"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static async Task<bool> FlushDatabase(this IRedisService redisService, int db)
        {
            var iserver = redisService.iServer;

            await iserver.FlushDatabaseAsync(db);

            return true;
        }
    }
}