using CQX.DataModel.Entites;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQX.DataAccess.Dao
{
    public class BaseDao<T> where T : EntityBase, new()
    {
        public bool Create(T entity)
        {
            using (var db = new CQXDbContext())
            {
                db.Entry(entity).State = EntityState.Added;
                return db.SaveChanges() > 0;
            }
        }

        public bool Update(T entity)
        {
            using (var db = new CQXDbContext())
            {
                db.Entry(entity).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }

        public bool Delete(T entity)
        {
            using (var db = new CQXDbContext())
            {
                db.Entry(entity).State = EntityState.Deleted;
                return db.SaveChanges() > 0;
            }
        }
        public bool Delete(int id)
        {
            using (var db = new CQXDbContext())
            {
                var entity = new T();
                entity.Id = id;
                db.Entry(entity).State = EntityState.Deleted;
                return db.SaveChanges() > 0;
            }
        }

        public T Get(int id)
        {
            using (var db = new CQXDbContext())
            {
                var entity = db.Set<T>().FirstOrDefault(u => u.Id == id);
                return entity;
            }
        }
        public List<T> GetAll()
        {
            using (var db = new CQXDbContext())
            {
                var list = db.Set<T>().ToList();
                return list;
            }
        }
    }
}
