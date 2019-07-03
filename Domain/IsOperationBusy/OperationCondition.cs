using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IsOperationBusy
{
    public class OperationCondition
    {
        private readonly IOperationConditionRepository _operationConditionRepository;
        public OperationCondition(IOperationConditionRepository operationCondition)
        {
            _operationConditionRepository = operationCondition;
        }      
        public void OperationIsBusy(string comboBoxItems, string comboBoxOperations)
        {
            _operationConditionRepository.OperationIsBusy(comboBoxItems, comboBoxOperations);
        }
        public void OperationIsNotBusy(string comboBoxItems, string comboBoxOperations)
        {
            _operationConditionRepository.OperationIsNotBusy(comboBoxItems, comboBoxOperations);
        }
    }
}
