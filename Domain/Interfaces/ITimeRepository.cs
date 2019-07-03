using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITimeRepository
    {
        double GetTime(DataRow row);
        double GetTime(string itemId, string operationId);
        double GetSetupTime(DataRow row);
        double GetSetupTime(string itemId, string operationId);
        DataTable GetMinStartTime(DataRow dataRow);
        DataTable GetMaxEndTimeFromInLine(DataRow dataRow);
        DateTime GetMinStartTimeFromInLine(DataRow dataRow);
        DateTime GetMaxEndTimeFromItemsOperations(DataRow dataRow);
        DateTime GetMaxEndTimeFromItemsOperationsDisplay(DataRow dataRow);
    }
}
