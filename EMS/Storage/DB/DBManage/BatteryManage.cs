using EMS.Storage.DB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Storage.DB.DBManage
{
    public class BatteryManage
    {
        public bool InsertBatteryModelInfo(BatteryModel item)
        {
            try
            {
                using (var db = new ORMContext())
                {
                    var entity = db.BatteryModelInfos.Add(item);
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public List<BatteryModel> QueryBatteryModelInfo(string BatteryID)
        {
            try
            {
                List<BatteryModel> batteries = new List<BatteryModel>();
                using (var db = new ORMContext())
                {
                    var infos = db.BatteryModelInfos.ToList();
                    foreach (var item in infos)
                    {
                        if (item.BatteryID == BatteryID)
                        {
                            batteries.Add(item);
                        }
                    }
                }
                return batteries;
            }
            catch
            {
                return null;
            }
        }
    }
}
