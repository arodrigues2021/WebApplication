using System;
using System.Collections.Generic;
using System.Text;

namespace cache
{
    public interface ICacheService<TEntity>
    {



        bool IsExists(string key);


        object Get<TEntity>(string key);

        void Set<TEntity>(string key, object value);




    }
}
