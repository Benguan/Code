using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NEG.Website.Models.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}