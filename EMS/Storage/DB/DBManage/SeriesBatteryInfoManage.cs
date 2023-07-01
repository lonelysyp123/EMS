using EMS.Storage.DB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace EMS.Storage.DB.DBManage
{
    public class SeriesBatteryInfoManage : IManage<SeriesBatteryInfoModel>
    {
        public bool Delete(SeriesBatteryInfoModel entity)
        {
            return false;
        }

        public bool DeleteAll()
        {
            return false;
        }

        public List<SeriesBatteryInfoModel> Get()
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.SeriesBatteryModelInfos.ToList();
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        public bool Insert(SeriesBatteryInfoModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.SeriesBatteryModelInfos.Add(entity);
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool Update(SeriesBatteryInfoModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.SeriesBatteryModelInfos.Attach(entity);
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
