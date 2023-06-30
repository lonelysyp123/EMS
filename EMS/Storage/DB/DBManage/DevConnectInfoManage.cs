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
    public class DevConnectInfoManage
    {
        public bool Insert(DevConnectInfoModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.DevConnectInfos.Add(entity);
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool Delete(DevConnectInfoModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.DevConnectInfos.Where(p=>p.IP == entity.IP&&p.Port == entity.Port).ToList();
                    for (int i = 0; i < result.Count; i++)
                    {
                        db.DevConnectInfos.Remove(result[i]);
                    }
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
                    var result = db.DevConnectInfos.RemoveRange(db.DevConnectInfos);
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool Update(DevConnectInfoModel entity)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.DevConnectInfos.Attach(entity);
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

        public List<DevConnectInfoModel> Get()
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var result = db.DevConnectInfos.ToList();
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
