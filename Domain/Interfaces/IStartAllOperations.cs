using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IStartAllOperations
    {
        DataTable Operations(string runItem);
        DataTable OperationsWithDuplicates(string runItem);
        DataTable SpecificOperationsWithDuplicates(DataRow row, string runItem);
        DataTable GetSpecificDuplicateItem(string runItem, int operationId);
    }
}
