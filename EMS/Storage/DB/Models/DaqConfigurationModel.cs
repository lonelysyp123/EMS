using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Storage.DB.Models
{
    public class DaqConfigurationModel
    {
        [Key]
        public int ID { get; set; }
        public int DaqTimeSpan { get; set; }
    }
}
