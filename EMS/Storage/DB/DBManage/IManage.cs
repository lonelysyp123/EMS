using EMS.Storage.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Storage.DB.DBManage
{
    public interface IManage<TEntity> where TEntity : class
    {
        bool Insert(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        bool DeleteAll();
        List<TEntity> Get();
    }
}
