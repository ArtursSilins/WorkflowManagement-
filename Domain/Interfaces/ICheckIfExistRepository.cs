using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICheckIfExistRepository
    {
        DataTable GetItemTable(string id);

        DataTable GetOperationTable(string id);
        DataTable GetInLineItemOperation(string itemId, string operationId);
    }
}
