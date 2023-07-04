using EMS.Storage.DB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Storage.DB.DBManage
{
    public class TotalBatteryInfoManage : IManage<TotalBatteryInfoModel>
    {
        public bool Delete(TotalBatteryInfoModel entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAll()
        {
            throw new NotImplementedException();
        }

        public List<TotalBatteryInfoModel> Get()
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.TotalBatteryInfos.ToList();
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<TotalBatteryInfoModel> Find(DateTime StartTime, DateTime EndTime)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.TotalBatteryInfos.Where(p => p.HappenTime >= StartTime && p.HappenTime <= EndTime).ToList();
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<TotalBatteryInfoModel> Find(string BCMUID, DateTime StartTime, DateTime EndTime)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.TotalBatteryInfos.Where(p => p.BCMUID == BCMUID&&p.HappenTime >= StartTime && p.HappenTime <= EndTime).ToList();
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        public bool Insert(TotalBatteryInfoModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.TotalBatteryInfos.Add(entity);
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool Update(TotalBatteryInfoModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.TotalBatteryInfos.Attach(entity);
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
