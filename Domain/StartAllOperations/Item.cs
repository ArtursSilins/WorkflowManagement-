using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Checking;
using Domain.FindTime;
using Domain.Interfaces;
using Domain.StartOneOperation;

namespace Domain.StartAllOperations
{
    public class Item
    {
        private readonly IStartAllOperations _startAllOperations;
        private readonly IIsBusyCheckRepository _isBusyCheckRepository;
        private readonly IStartTimeRepository _startTimeRepository;
        private readonly IPauseTimesRepository _pauseTimesRepository;
        private readonly ITimeRepository _timeRepository;
        private readonly IOperationConditionRepository _operationConditionRepository;
        private readonly ICheckIfExistRepository _checkIfExistRepository;
        private readonly IDataForInLineTable _dataForInLineTable;
        private int PlaceNum { get; set; }
        private IsBusyCheck isBusyCheck { get; set; }
        private StartTime startTime { get; set; } 
        private CheckIfExists checkIfExists { get; set; } 
        private Time time { get; set; } = new Time();

        public Item(IStartAllOperations startAllOperations, IIsBusyCheckRepository isBusyCheckRepository, IStartTimeRepository startTimeRepository,
            IPauseTimesRepository pauseTimesRepository, ITimeRepository timeRepository, IOperationConditionRepository operationConditionRepository,
            ICheckIfExistRepository checkIfExistRepository, IDataForInLineTable dataForInLineTable)
        {
            _startAllOperations = startAllOperations;
            _isBusyCheckRepository = isBusyCheckRepository;
            _startTimeRepository = startTimeRepository;
            _pauseTimesRepository = pauseTimesRepository;
            _timeRepository = timeRepository;
            _operationConditionRepository = operationConditionRepository;
            _checkIfExistRepository = checkIfExistRepository;
            _dataForInLineTable = dataForInLineTable;
        }

        public void Run(string runItem, string count)
        {
            checkIfExists = new CheckIfExists(_checkIfExistRepository);
            startTime = new StartTime(_startTimeRepository, _pauseTimesRepository, _timeRepository, _dataForInLineTable);

            foreach (DataRow row in _startAllOperations.Operations(runItem).Rows)
            {

                isBusyCheck = new IsBusyCheck(_isBusyCheckRepository);

                if (isBusyCheck.GetOperationBool(row["OperationId"].ToString()) == false)
                {
                    startTime.AddStartTime(row, count);
                    _operationConditionRepository.OperationIsBusy(runItem, row["OperationId"].ToString());

                }
                else
                {
                    if (checkIfExists.IfInLineItemExists(runItem, row["OperationId"].ToString()) == false)
                    {
                        startTime.AddInLineStartTime(row, count);

                    }
                    else
                    {
                        startTime.AddInLineStartTimeIfEntryExist(row, count);

                    }

                }
            }

        }
    
        public void RunWithDuplicates(string runItem, string count)
        {
            int operationId;
            foreach (DataRow row in _startAllOperations.OperationsWithDuplicates(runItem).Rows)
            {
                operationId = getDuplicateRow(row, runItem, count);

                runDuplicatesItems(PlaceNum, operationId, runItem, count);
            }
        }
        private int getDuplicateRow(DataRow row, string runItem, string count)
        {
            int key = 0;

            int num = 0;

            int placeNum1 = 0;
            DateTime b = new DateTime();

            DateTime z = new DateTime(3000, 1, 1, 1, 1, 1);
            DateTime timeNow = DateTime.Now;
            List<MethodValues> valueList = new List<MethodValues>();

            isBusyCheck = new IsBusyCheck(_isBusyCheckRepository);

            foreach (DataRow row1 in _startAllOperations.SpecificOperationsWithDuplicates(row, runItem).Rows)
            {

                if (isBusyCheck.GetOperationBool(row1["OperationId"].ToString()) == false)
                {

                    placeNum1 = 1;
                    MethodValues methodValues = new MethodValues(int.Parse(row1["OperationId"].ToString()), timeNow, placeNum1);
                    valueList.Add(methodValues);
                }
                else
                {
                    if (checkIfExists.IfInLineItemExists(runItem, row1["OperationId"].ToString()) == false)
                    {

                        placeNum1 = 2;
                        MethodValues methodValues = new MethodValues(int.Parse(row1["OperationId"].ToString()), time.GetMaxEndTimeFromItemsOperations(_timeRepository, row1), placeNum1);
                        valueList.Add(methodValues);
                    }
                    else
                    {

                        placeNum1 = 3;
                        MethodValues methodValues = new MethodValues(int.Parse(row1["OperationId"].ToString()), time.GetMaxEndTimeFromInLine(_timeRepository, row1), placeNum1);
                        valueList.Add(methodValues);
                    }

                }


                foreach (var item in valueList)
                {

                    b = item.dateTime;

                    if ((num = DateTime.Compare(z, b)) > 0)
                    {
                        z = item.dateTime;
                        key = item.key;
                        PlaceNum = item.selectedNr;
                    }

                    z = item.dateTime;

                }

            }

            return key;

        }
        private void runDuplicatesItems(int placeNum, int operationId, string runItem, string count)
        {
            startTime = new StartTime(_startTimeRepository, _pauseTimesRepository, _timeRepository, _dataForInLineTable);
            foreach (DataRow row in _startAllOperations.GetSpecificDuplicateItem(runItem, operationId).Rows)
            {

                switch (placeNum)
                {
                    case 1:
                        startTime.AddStartTime(row, count);
                        _operationConditionRepository.OperationIsBusy(runItem, row["OperationId"].ToString());

                        break;
                    case 2:
                        startTime.AddInLineStartTime(row, count);

                        break;
                    case 3:
                        startTime.AddInLineStartTimeIfEntryExist(row, count);

                        break;

                }

            }
        }
    }
}
