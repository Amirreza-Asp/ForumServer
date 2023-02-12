﻿using Forum.Application.Models;
using System.Linq.Expressions;

namespace Forum.Application.Services
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TDto> FirstOrDefaultAsync<TDto>(
            Expression<Func<TEntity, bool>> filters = null,
            CancellationToken cancellationToken = default) where TDto : class;


        Task<ListActionResult<TDto>> GetAllAsync<TDto>(
           GridQuery query,
           CancellationToken cancellationToken = default) where TDto : class;


        void Create(TEntity entity);
        void Update(TEntity entity);

        void Remove(TEntity entity);
        void Remove(object id);

        Task<bool> SaveAsync(CancellationToken cancellationToken = default);
    }
}
