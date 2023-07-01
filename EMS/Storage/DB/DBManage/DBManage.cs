using EMS.Storage.DB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Storage.DB.DBManage
{
    internal class DBManage<TEntity> : IManage<TEntity> where TEntity : class
    {
        public bool Delete(TEntity entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.Set<TEntity>().Find(entity);
                    db.Set<TEntity>().Remove(result);
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool DeleteAll()
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.Set<TEntity>().RemoveRange(db.Set<TEntity>());
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public List<TEntity> Get()
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.Set<TEntity>().ToList();
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        public bool Insert(TEntity entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.Set<TEntity>().Add(entity);
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool Update(TEntity entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.Set<TEntity>().Attach(entity);
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
