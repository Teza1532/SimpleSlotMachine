using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSlotMachine.Logger
{
    public interface ILogger
    {
        public void AddCriticalLogMessage(string message);
        public void AddWarningLogMessage(string message);
        public void AddInformationLogMessage(string message);
        public void InitLogger(string LogName);

    }
}
