using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace internetcommerce01.Repository
{
    public interface IRepository<TableEntity> where TableEntity : class
    {
        IEnumerable<TableEntity> GetProduct();
        IEnumerable<TableEntity> GetAllRecords();
        IQueryable<TableEntity> GetAllRecordsIQueryable();
        int GetAllRecordCount();
        void Add(TableEntity entity);
        void Update(TableEntity entity);
        void UpdateByWhereClause(Expression<Func<TableEntity, bool>> wherePredict, Action<TableEntity> ForEachPredict);
        TableEntity GetFirstOrDefault(int recordID);
        void Remove(TableEntity entity);
        void RemoveByWhereClause(Expression<Func<TableEntity, bool>> wherePredict);
        void RemoveRangeByWhereClause(Expression<Func<TableEntity, bool>> wherePredict);
        void InactiveAndDeleteMarkByWhereClause(Expression<Func<TableEntity, bool>> wherePredict, Action<TableEntity> ForEachPredict);
        TableEntity GetFirstOrDefaultByParameter(Expression<Func<TableEntity, bool>> wherePredict);
        IEnumerable<TableEntity> GetListParameter(Expression<Func<TableEntity, bool>> wherePredict);
        IEnumerable<TableEntity> GetResultBySQLProcedure(string query, params object[] parameters);
        IEnumerable<TableEntity> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<TableEntity, bool>> wherePredict, Expression<Func<TableEntity, int>> orderByPredict);
    }
}