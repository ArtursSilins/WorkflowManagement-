using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDataForForm2ProgressTxtRepository
    {
        DataTable WaitingData(string itemId);
        DataTable CompletedData(string itemId, string operationId);
    }
}
