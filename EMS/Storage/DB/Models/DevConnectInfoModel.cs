using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Storage.DB.Models
{
    public class DevConnectInfoModel
    {
        [Key]
        public int BCMUID { set; get; }
        public string IP { set; get; }
        public string Port { set; get; }
    }
}
