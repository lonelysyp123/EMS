using EMS.Common.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DBManage
{
    public class BatteryManage
    {
        public void InsertBatteryModelInfo(BatteryModel item)
        {
            using (var db = new ORMContext())
            {
                db.BatteryModelInfos.Add(item);
            }
        }

        //public async List<BatteryModel> QueryBatteryModelInfo()
        //{
        //    using (var db = new ORMContext())
        //    {
        //        var infos = await db.BatteryModelInfos.ToListAsync();
        //        foreach (var item in infos)
        //        {

        //        }
        //    }
        //}
    }
}
