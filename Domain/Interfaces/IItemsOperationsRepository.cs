using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IItemsOperationsRepository
    {
        void DeleteOperationFromItemsOperations(string operation);

        void DeleteOperationFromOperations(string operation);

        void DeleteOperationFeomInLine(string operation);

        void DeleteItemFromDisplay(string item);

        void DeleteItemFromInLine(string item);

        void DeleteItemFromItemsOperations(string item);

        void DeleteItemFromItems(string item);
        void DeleteConnection(string item, string operation);
        void DeleteDuplicateOperationFromItemsOperations(string duplicateId);
        void DeleteDuplicateOperationFromOperations(string duplicateId);
        void DeleteFromDuplicate(string duplicateId);
        void DeleteDuplicateConnection(string item, string duplicateId);
    }
}
