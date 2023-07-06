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

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <returns>数据列表</returns>
        public List<SeriesBatteryInfoModel> Find(DateTime StartTime, DateTime EndTime)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.SeriesBatteryModelInfos.Where(p=>p.HappenTime>=StartTime&&p.HappenTime<=EndTime).ToList();
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="BCMUID">BCMUID</param>
        /// <param name="BMUID">BMUID</param>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <returns>数据列表</returns>
        public List<SeriesBatteryInfoModel> Find(string BCMUID, string BMUID, DateTime StartTime, DateTime EndTime)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.SeriesBatteryModelInfos.Where(p => p.BCMUID==BCMUID&&p.BMUID==BMUID&&p.HappenTime >= StartTime && p.HappenTime <= EndTime).ToList();
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
