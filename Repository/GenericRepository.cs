using internetcommerce01.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace internetcommerce01.Repository
{
    public class GenericRepository<TableEntity> : IRepository<TableEntity> where TableEntity : class
    {
        DbSet<TableEntity> _dbSet;

        private db_internetcommerce01Entities _dbEntity;

        public GenericRepository(db_internetcommerce01Entities dbEntity)
        {
            _dbEntity = dbEntity;
            _dbSet = dbEntity.Set<TableEntity>();
        }

        public IEnumerable<TableEntity> GetProduct()
        {
            return _dbSet.ToList();
        }

        public void Add(TableEntity entity)
        {
            _dbSet.Add(entity);
            _dbEntity.SaveChanges();
        }

        public int GetAllRecordCount()
        {
            return _dbSet.Count();
        }

        public IEnumerable<TableEntity> GetAllRecords()
        {
            return _dbSet.ToList();
        }

        public IQueryable<TableEntity> GetAllRecordsIQueryable()
        {
            return _dbSet;
        }

        public TableEntity GetFirstOrDefault(int recordID)
        {
            return _dbSet.Find(recordID);
        }

        public TableEntity GetFirstOrDefaultByParameter(Expression<Func<TableEntity, bool>> wherePredict)
        {
            return _dbSet.Where(wherePredict).FirstOrDefault();
        }

        public IEnumerable<TableEntity> GetListParameter(Expression<Func<TableEntity, bool>> wherePredict)
        {
            return _dbSet.Where(wherePredict).ToList();
        }

        public IEnumerable<TableEntity> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<TableEntity, bool>> wherePredict, Expression<Func<TableEntity, int>> orderByPredict)
        {
            if (wherePredict != null)
            {
                return _dbSet.OrderBy(orderByPredict).Where(wherePredict).ToList();
            }
            else
            {
                return _dbSet.OrderBy(orderByPredict).ToList();
            }
        }

        public IEnumerable<TableEntity> GetResultBySQLProcedure(string query, params object[] parameters)
        {
            if (parameters != null)
            {
                return _dbEntity.Database.SqlQuery<TableEntity>(query, parameters).ToList();
            }
            else
            {
                return _dbEntity.Database.SqlQuery<TableEntity>(query).ToList();
            }
        }

        public void InactiveAndDeleteMarkByWhereClause(Expression<Func<TableEntity, bool>> wherePredict, Action<TableEntity> ForEachPredict)
        {
            _dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }

        public void Remove(TableEntity entity)
        {
            if (_dbEntity.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }

        public void RemoveByWhereClause(Expression<Func<TableEntity, bool>> wherePredict)
        {
            TableEntity entity = _dbSet.Where(wherePredict).FirstOrDefault();
            Remove(entity);
        }

        public void RemoveRangeByWhereClause(Expression<Func<TableEntity, bool>> wherePredict)
        {
            List<TableEntity> entity = _dbSet.Where(wherePredict).ToList();
            foreach (var ent in entity)
            {
                Remove(ent);
            }
        }

        //public void Update(TableEntity entity)
        //{
        //    _dbSet.Attach(entity);
        //    _dbEntity.Entry(entity).State = EntityState.Modified;
        //}

        public void Update(TableEntity entity)
        {
            _dbSet.Attach(entity);
            _dbEntity.Entry(entity).State = EntityState.Modified;
            _dbEntity.SaveChanges();
        }

        public void UpdateByWhereClause(Expression<Func<TableEntity, bool>> wherePredict, Action<TableEntity> ForEachPredict)
        {
            _dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }
    }
}