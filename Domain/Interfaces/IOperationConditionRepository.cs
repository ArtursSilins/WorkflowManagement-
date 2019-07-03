using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOperationConditionRepository
    {
       
        void OperationIsBusy(string comboBoxItems, string comboBoxOperations);
        void OperationIsNotBusy(string comboBoxItems, string comboBoxOperations);
    }
}
