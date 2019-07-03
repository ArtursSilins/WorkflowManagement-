using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IItemsOperationsConnectionRepository
    {
        void AddConnectionWithDuplicate(string itemId, string operationId, string time, string setupTime, string duplicateId );
        void AddConnection(string itemId, string operationId, string time, string setupTime);
    }
}
