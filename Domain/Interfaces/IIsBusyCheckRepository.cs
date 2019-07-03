using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IIsBusyCheckRepository
    {
        DataTable GetTable(string operation);
        DataTable GetItemTable(string item);
        DataTable GetDuplicateTable(string duplicateId);
    }
}
