using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IStartTimeRepository
    {
        void AddStartTime(DateTime time, DateTime endTime, string textBoxCount, string comboBoxItems, string comboBoxOperations);
        void AddInLineStartTime(int lineNr, DateTime startTime, DateTime endTime, string count, string item, string operation);
        void AddStartTime(DateTime time, DateTime endTime, DataRow row, string count);
    }
}
