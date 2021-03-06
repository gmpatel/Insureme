﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Insureme.DataAccess.Interfaces;

namespace Insureme.DataAccess.Defaults
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private static long counter;

        public IUnitOfWork UnitOfWork { get; private set; }
        public IDataContext DbContext { get; private set; }

        public Repository(IUnitOfWork unitOfWork)
        {
            Id = ++counter;

            this.UnitOfWork = unitOfWork;
            this.DbContext = unitOfWork.DbContext;
        }

        public long Id { get; private set; }
        public long Instances => counter;

        public IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();
            return query;
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>().Where(predicate);
            return query;
        }

        public TEntity Add(TEntity entity)
        {
            return DbContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}