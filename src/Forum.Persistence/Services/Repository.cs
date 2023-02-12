﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Forum.Application.Models;
using Forum.Application.Services;
using Forum.Application.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Forum.Persistence.Services
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IMapper _mapper;

        public Repository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
            _mapper = mapper;
        }

        public async Task<TDto> FirstOrDefaultAsync<TDto>(Expression<Func<TEntity, bool>> filters = null, CancellationToken cancellationToken = default) where TDto : class
        {
            return
                await _dbSet
                    .Where(filters)
                    .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<ListActionResult<TDto>> GetAllAsync<TDto>(GridQuery query, CancellationToken cancellationToken = default) where TDto : class
        {
            var actionResult = new ListActionResult<TDto>();

            var queryContext = _dbSet.AsQueryable();

            //filter
            if (query.Filters != null && query.Filters.Any())
            {
                // var filterExpression = QueryUtility.FilterExpression<T>(args.Filtered[0].column, args.Filtered[0].value);
                for (int i = 0; i < query.Filters.Length; i++)
                {
                    var filterExpression = QueryUtility.FilterExpression<TEntity>(query.Filters[i].column, query.Filters[i].value);
                    if (filterExpression != null)
                        queryContext = queryContext.Where(filterExpression);
                }
            }

            //total count
            var total = await queryContext.CountAsync(cancellationToken);

            //sort
            if (query.Sorted != null && query.Sorted.Length > 0)
            {
                for (int i = 0; i < query.Sorted.Length; i++)
                {
                    queryContext = queryContext.SortMeDynamically(query.Sorted[i].column, query.Sorted[i].desc);
                }
            }

            var result = await queryContext
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .Skip((query.Page - 1) * query.Size)
                .Take(query.Size)
                .ToListAsync(cancellationToken);

            actionResult.Data = result.ToList();
            actionResult.Total = total;
            actionResult.Page = query.Page;
            actionResult.Size = query.Size;

            return actionResult;
        }


        public void Create(TEntity entity)
        {
            _dbSet.Add(entity);
        }


        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void Remove(object id)
        {
            var entity = _dbSet.Find(id);

            if (entity != null)
                _dbSet.Remove(entity);
        }

        public async Task<bool> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}