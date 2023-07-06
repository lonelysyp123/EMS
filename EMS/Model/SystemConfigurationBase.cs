using EMS.Storage.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Model
{
    public class SystemConfigurationBase
    {
        public DaqConfigurationModel daqConfiguration;
        public SystemConfigurationBase() 
        {
            daqConfiguration = new DaqConfigurationModel();
        }
    }
}
