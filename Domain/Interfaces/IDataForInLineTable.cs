using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDataForInLineTable
    {
        DataTable GetFromItemsOperations(string operation);
        DataTable GetFromInLine(string item, string operation);
        DataTable GetOperationsId(int operation);
    }
}
