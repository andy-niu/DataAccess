namespace Cache.RadisCache
{
    public class RadisManagerFactory
    {
        /// <summary>
        /// 枚举值，指示操作类型（Read只读；Write只写）
        /// </summary>
        public enum ReadOrWrite
        {
            Read,
            Write
        }
        public static ServiceStack.Redis.IRedisClient GetRedisClient(ReadOrWrite rw)
        {
            ServiceStack.Redis.IRedisClient client = null;
            switch (rw)
            {
                //读写
                case ReadOrWrite.Write:
                    client = RadisManager.GetClient();
                    break;
                //只读
                case ReadOrWrite.Read:
                    client = RadisManager.GetReadOnlyClient();
                    break;
            }

            return client;
        }
    }
}
