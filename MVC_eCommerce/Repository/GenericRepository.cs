using MVC_eCommerce.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MVC_eCommerce.Repository
{
    public class GenericRepository<Tbl_Entity> : IRepository<Tbl_Entity> where Tbl_Entity : class
    {
        DbSet<Tbl_Entity> _dbSet;

        private EFContext _DBContext;

        public GenericRepository(EFContext DBContext)
        {
            _DBContext = DBContext;
            _dbSet = _DBContext.Set<Tbl_Entity>();
        }

        public IEnumerable<Tbl_Entity> GetProductList()
        {
            return _dbSet.ToList();
        }

        public void Add(Tbl_Entity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(Tbl_Entity entity)
        {
            _dbSet.Attach(entity);
            _DBContext.Entry(entity).State = EntityState.Modified;
        }

        public int GetAllrecordCount()
        {
            return _dbSet.Count();
        }

        public IEnumerable<Tbl_Entity> GetAllRecords()
        {
            return _dbSet.ToList();
        }

        public IQueryable<Tbl_Entity> GetAllRecordsIQueryable()
        {
            return _dbSet;
        }

        public Tbl_Entity GetFirstorDefault(int recordId)
        {
            return _dbSet.Find(recordId);
        }

        public Tbl_Entity GetFirstorDefaultByParameter(Expression<Func<Tbl_Entity, bool>> wherePredict)
        {
            return _dbSet.Where(wherePredict).FirstOrDefault();
        }

        public IEnumerable<Tbl_Entity> GetListParameter(Expression<Func<Tbl_Entity, bool>> wherePredict)
        {
            return _dbSet.Where(wherePredict).ToList();
        }

        public IEnumerable<Tbl_Entity> GetResultBySqlprocedure(string query, params object[] parameters)
        {
            if (parameters != null)
                return _DBContext.Database.SqlQuery<Tbl_Entity>(query, parameters).ToList();
            else
                return _DBContext.Database.SqlQuery<Tbl_Entity>(query).ToList();
        }

        public void Remove(int recordId)
        {
            Tbl_Entity e = _dbSet.Find(recordId);
            if (e != null)
            {
                _dbSet.Remove(e);
            }
        }

        public void UpdateByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict, Action<Tbl_Entity> ForEachPredict)
        {
            _dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }

        public IEnumerable<Tbl_Entity> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<Tbl_Entity, bool>> wherePredict, Expression<Func<Tbl_Entity, int>> orderByPredict)
        {
            if (wherePredict != null)
                return _dbSet.OrderBy(orderByPredict).Where(wherePredict).ToList();
            else
                return _dbSet.OrderBy(orderByPredict).ToList();
        }
    }
}
