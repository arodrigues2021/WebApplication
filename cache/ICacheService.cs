namespace cache
{
    public interface ICacheService<TEntity>
    {



        bool IsExists(string key);


        object Get<TEntity>(string key);

        void Set<TEntity>(string key, object value);

         bool IsExistsDelete(string key);


    }
}
