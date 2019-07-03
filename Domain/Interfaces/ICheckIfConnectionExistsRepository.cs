using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICheckIfConnectionExistsRepository
    {
        DataTable GetTable(string item, string alloperations);
        DataTable GetDuplicateTable(string item, string duplicateId);
    }
}
