using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace EMS.ViewModel
{
    public class StateContentViewModel : ViewModelBase
    {
        private string _operationContent;
        public string OperationContent
        {
            get => _operationContent;
            set
            {
                SetProperty(ref _operationContent, value);
            }
        }

        private string _operationType;
        public string OperationType
        {
            get => _operationType;
            set
            {
                SetProperty(ref _operationType, value);
            }
        }

        private string _currentTime;
        public string CurrentTime
        {
            get => _currentTime;
            set
            {
                SetProperty(ref _currentTime, value);
            }
        }
        
        public StateContentViewModel()
        {
            ShowTime();
        }

        private bool IsRun = true;
        private void ShowTime()
        {
            Thread thread = new Thread(() =>
            {
                while (IsRun)
                {
                    Thread.Sleep(1000);
                    CurrentTime = DateTime.Now.ToString("HH:mm:ss");
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
