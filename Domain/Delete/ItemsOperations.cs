using Domain.ArrangeInLineTable;
using Domain.Interfaces;
using Domain.IsOperationBusy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Delete
{
    public class ItemsOperations
    {

        private readonly IItemsOperationsRepository _itemsOperationsRepository;
        private readonly IOperationConditionRepository _operationConditionRepository;
        private readonly IItemsOperationsForTrueFalseCheckReposytory _itemsOperationsForTrueFalseCheckReposytory;
        private readonly IItemsOperationsNullRepository _itemsOperationsNullRepository;
        private readonly IElapsedInLineTimesRepository _elapsedInLineTimesRepository;
        private readonly INumerationInLineRepository _numerationInLineRepository;
        private readonly ISortedInLineTableASCRepository _sortedInLineTableASCRepository;
        private readonly IInLineFirstNrRepository _inLineFirstNrRepository;
        private readonly IOperationTableRepository _operationTableRepository;
        public ItemsOperations(IItemsOperationsRepository itemsOperationsRepository, IOperationConditionRepository operationConditionRepository,
            IItemsOperationsForTrueFalseCheckReposytory itemsOperationsForTrueFalseCheckReposytory, IItemsOperationsNullRepository itemsOperationsNullRepository,
            IElapsedInLineTimesRepository elapsedInLineTimesRepository, INumerationInLineRepository numerationInLineRepository,
            ISortedInLineTableASCRepository sortedInLineTableASCRepository, IInLineFirstNrRepository inLineFirstNrRepository, IOperationTableRepository operationTableRepository)
        {
            _itemsOperationsRepository = itemsOperationsRepository;
            _operationConditionRepository = operationConditionRepository;
            _itemsOperationsForTrueFalseCheckReposytory = itemsOperationsForTrueFalseCheckReposytory;
            _itemsOperationsNullRepository = itemsOperationsNullRepository;
            _elapsedInLineTimesRepository = elapsedInLineTimesRepository;
            _numerationInLineRepository = numerationInLineRepository;
            _sortedInLineTableASCRepository = sortedInLineTableASCRepository;
            _inLineFirstNrRepository = inLineFirstNrRepository;
            _operationTableRepository = operationTableRepository;
        }
        public void DeleteOperation(string operation)
        {
            _itemsOperationsRepository.DeleteOperationFromItemsOperations(operation);
            _itemsOperationsRepository.DeleteOperationFeomInLine(operation);
            _itemsOperationsRepository.DeleteOperationFromOperations(operation);

            ItemsOperationsForTrueFalseCheck itemsOperationsForTrueFalseCheck = new ItemsOperationsForTrueFalseCheck(_itemsOperationsForTrueFalseCheckReposytory,
                 _operationConditionRepository, _itemsOperationsNullRepository);

            itemsOperationsForTrueFalseCheck.Checking();
 
            ElapsedInLineTimes elapsedInLineTimes = new ElapsedInLineTimes(_elapsedInLineTimesRepository);
            elapsedInLineTimes.DeleteEndTimes();

            SortInLineTableNr sortedInLineTeableASC = new SortInLineTableNr(_sortedInLineTableASCRepository, _numerationInLineRepository);
            sortedInLineTeableASC.ArrangeAllInLineNr();
           
            InLineFirstNr inLineFirstNr = new InLineFirstNr(_inLineFirstNrRepository, _operationTableRepository, _elapsedInLineTimesRepository, _numerationInLineRepository);
            inLineFirstNr.ArrangeNr();

            elapsedInLineTimes.DeleteStartTimes();

        }
        public void DeleteDuplicate(string duplicateId)
        {
            _itemsOperationsRepository.DeleteDuplicateOperationFromItemsOperations(duplicateId);
            _itemsOperationsRepository.DeleteDuplicateOperationFromOperations(duplicateId);
            _itemsOperationsRepository.DeleteFromDuplicate(duplicateId);

            ItemsOperationsForTrueFalseCheck itemsOperationsForTrueFalseCheck = new ItemsOperationsForTrueFalseCheck(_itemsOperationsForTrueFalseCheckReposytory,
                 _operationConditionRepository, _itemsOperationsNullRepository);

            itemsOperationsForTrueFalseCheck.Checking();

            ElapsedInLineTimes elapsedInLineTimes = new ElapsedInLineTimes(_elapsedInLineTimesRepository);
            elapsedInLineTimes.DeleteEndTimes();

            SortInLineTableNr sortedInLineTeableASC = new SortInLineTableNr(_sortedInLineTableASCRepository, _numerationInLineRepository);
            sortedInLineTeableASC.ArrangeAllInLineNr();

            InLineFirstNr inLineFirstNr = new InLineFirstNr(_inLineFirstNrRepository, _operationTableRepository, _elapsedInLineTimesRepository, _numerationInLineRepository);
            inLineFirstNr.ArrangeNr();

            elapsedInLineTimes.DeleteStartTimes();

        }
        public void DeleteDuplicateOperation(string operationId)
        {
            _itemsOperationsRepository.DeleteDuplicateOperationFromItemsOperations(operationId);
            _itemsOperationsRepository.DeleteDuplicateOperationFromOperations(operationId);

            ItemsOperationsForTrueFalseCheck itemsOperationsForTrueFalseCheck = new ItemsOperationsForTrueFalseCheck(_itemsOperationsForTrueFalseCheckReposytory,
                 _operationConditionRepository, _itemsOperationsNullRepository);

            itemsOperationsForTrueFalseCheck.Checking();

            ElapsedInLineTimes elapsedInLineTimes = new ElapsedInLineTimes(_elapsedInLineTimesRepository);
            elapsedInLineTimes.DeleteEndTimes();

            SortInLineTableNr sortedInLineTeableASC = new SortInLineTableNr(_sortedInLineTableASCRepository, _numerationInLineRepository);
            sortedInLineTeableASC.ArrangeAllInLineNr();

            InLineFirstNr inLineFirstNr = new InLineFirstNr(_inLineFirstNrRepository, _operationTableRepository, _elapsedInLineTimesRepository, _numerationInLineRepository);
            inLineFirstNr.ArrangeNr();

            elapsedInLineTimes.DeleteStartTimes();

        }
        public void DeleteItem(string item)
        {
            _itemsOperationsRepository.DeleteItemFromDisplay(item);
            _itemsOperationsRepository.DeleteItemFromInLine(item);
            _itemsOperationsRepository.DeleteItemFromItemsOperations(item);
            _itemsOperationsRepository.DeleteItemFromItems(item);
        }
        public void DeleteConnection(string item, string operation)
        {
            _itemsOperationsRepository.DeleteConnection(item, operation);
        }
        public void DeleteDuplicateConnection(string item, string duplicateId)
        {
            _itemsOperationsRepository.DeleteDuplicateConnection(item, duplicateId);
        }
    }
}
