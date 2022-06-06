﻿using System.Collections.Generic;
using System.Windows.Documents;

namespace TechHealth.Repository
{
    public interface IRepository<TEntity,TKey>
    {
        List<TEntity> GetAllToList();
        bool Create(TEntity entity);
        bool Update(TEntity entity);
        TEntity GetById(TKey key);
        bool Delete(TKey key);




    }
}