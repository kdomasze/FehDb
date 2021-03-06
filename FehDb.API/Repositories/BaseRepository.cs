﻿using FehDb.API.Contexts;
using FehDb.API.Extensions;
using FehDb.API.Models;
using FehDb.API.Models.Binding;
using FehDb.API.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Repositories
{
    public class BaseRepository<T> where T : BaseEntity
    {
        internal readonly FehContext _context;

        internal readonly DbSet<T> _entities;

        public BaseRepository(FehContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public virtual PagedResult<T> GetAll(Query query, BaseFilter filter)
        {
            return _entities.GetPaged(query.Page, query.PageSize);
        }

        public virtual T GetById(int id)
        {
            return _entities.SingleOrDefault(s => s.ID == id);
        }

        public async Task Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException("Input data is null.");

            await _entities.AddAsync(entity);
        }

        public virtual async Task Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("Input data is null.");

            var oldEntity = await _context.FindAsync<T>(entity.ID);

            entity.DateAdded = oldEntity.DateAdded;
            entity.DateModified = oldEntity.DateModified;

            _context.Entry(oldEntity).CurrentValues.SetValues(entity);
        }

        public void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("Input data is null.");

            _entities.Remove(entity);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
