using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAdd
    {    
        void Item(string id, string name);
        void Operation(string id, string name);
        void ItemToDisplay(string id, string name);
        void DuplicateName(string name);
        void DuplicateOperation(string id, string name, string duplicateId);
    }
}
