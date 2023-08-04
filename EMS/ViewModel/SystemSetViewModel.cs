using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.ViewModel
{
    public class SystemSetViewModel : ViewModelBase
    {
        private string _daqTimeSpan;
        /// <summary>
        /// 采集间隔
        /// </summary>
        public string DaqTimeSpan 
        {
            get
            {
                return _daqTimeSpan;
            } 
            set
            {
                SetProperty(ref _daqTimeSpan, value);
            }
        }

        public SystemSetViewModel() 
        {

        }
    }
}
