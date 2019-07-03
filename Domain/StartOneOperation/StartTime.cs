using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.FindTime;
using Domain.Interfaces;
using Domain.PauseTimes;
using Domain.RetrieveSpecific;

namespace Domain.StartOneOperation
{
    public class StartTime
    {
        private IStartTimeRepository _startTimeRepository;
        private readonly IPauseTimesRepository _pauseTimesRepository;
        private readonly ITimeRepository _timeRepository;
        private readonly IDataForInLineTable _dataForInLineTable;
        private Time timeOfEnd { get; set; } = new Time();
        private CheckingForPause checkingForPause { get; set; } 
        private InLineOperationsId inLineOperationsId  { get; set; }
        public StartTime(IStartTimeRepository startTimeRepository, IPauseTimesRepository pauseTimesRepository, ITimeRepository timeRepository, IDataForInLineTable dataForInLineTable)
        {
            _startTimeRepository = startTimeRepository;
            _pauseTimesRepository = pauseTimesRepository;
            _timeRepository = timeRepository;
            _dataForInLineTable = dataForInLineTable;
        }

        public void AddStartTime(string comboBoxItems, string comboBoxOperations, string textBoxCount)
        {
            checkingForPause = new CheckingForPause(_pauseTimesRepository);

            DateTime time = DateTime.Now;
            DateTime endTime = timeOfEnd.getEndTimeWithPause(_pauseTimesRepository, _timeRepository, comboBoxItems, comboBoxOperations, textBoxCount);

            if (timeOfEnd.isStartTimeInPause)
            {
                TimeSpan timeSpan = checkingForPause.PauseTimes(getStartTimeInPause, time, timeOfEnd.getEndTimeWithPause(_pauseTimesRepository, _timeRepository, comboBoxItems, comboBoxOperations, textBoxCount));
                time = time.Add(timeSpan);
                timeOfEnd.isStartTimeInPause = false;
            }

            _startTimeRepository.AddStartTime(time, endTime, textBoxCount, comboBoxItems, comboBoxOperations);

        }
        public void AddStartTime(DataRow row, string textBoxCount)
        {
            checkingForPause = new CheckingForPause(_pauseTimesRepository);

            DateTime time = DateTime.Now;
            DateTime endTime = timeOfEnd.getEndTimeWithPause(_pauseTimesRepository, _timeRepository, row, textBoxCount);

            if (timeOfEnd.isStartTimeInPause)
            {
                TimeSpan timeSpan = checkingForPause.PauseTimes(getStartTimeInPause, time, timeOfEnd.getEndTimeWithPause(_pauseTimesRepository, _timeRepository, row, textBoxCount));
                time = time.Add(timeSpan);
                timeOfEnd.isStartTimeInPause = false;
            }

            _startTimeRepository.AddStartTime(time, endTime, row, textBoxCount);

        }
        public void AddInLineStartTime(string item, string operation, string count)
        {
            checkingForPause = new CheckingForPause(_pauseTimesRepository);

            DateTime newStartTime = new DateTime();
            DateTime endTime = new DateTime();

            double setupTime = 0;
            double time = 0;

                foreach (DataRow row in _dataForInLineTable.GetFromItemsOperations(operation).Rows)
                {                 
                        setupTime = double.Parse((row["SetupTime"]).ToString());
                        time = double.Parse((row["Time"]).ToString());
                newStartTime = Convert.ToDateTime(row["EndTime"]);
                        endTime = timeOfEnd.getEndTimeWithPause(_pauseTimesRepository, newStartTime, time, count, setupTime);

                    if (timeOfEnd.isStartTimeInPause)
                    {
                        TimeSpan timeSpan = checkingForPause.PauseTimes(getStartTimeInPause, newStartTime, timeOfEnd.getEndTimeWithPause(_pauseTimesRepository, _timeRepository, item, operation, count));
                        newStartTime = newStartTime.Add(timeSpan);
                        timeOfEnd.isStartTimeInPause = false;
                    }

                }
            _startTimeRepository.AddInLineStartTime(1, newStartTime, endTime, count, item, operation);
        }
        public void AddInLineStartTime(DataRow row, string count)
        {
            checkingForPause = new CheckingForPause(_pauseTimesRepository);

            DateTime newStartTime = new DateTime();
            DateTime endTime = new DateTime();

            double setupTime = 0;
            double time = 0;

            foreach (DataRow row1 in _dataForInLineTable.GetFromItemsOperations(row["OperationId"].ToString()).Rows)
            {
                setupTime = double.Parse((row["SetupTime"]).ToString());
                time = double.Parse((row["Time"]).ToString());
                newStartTime = Convert.ToDateTime(row1["EndTime"]);
                endTime = timeOfEnd.getEndTimeWithPause(_pauseTimesRepository, newStartTime, time, count, setupTime);

                if (timeOfEnd.isStartTimeInPause)
                {
                    TimeSpan timeSpan = checkingForPause.PauseTimes(getStartTimeInPause, newStartTime, timeOfEnd.getEndTimeWithPause(_pauseTimesRepository, _timeRepository, row["ItemId"].ToString(), row["OperationId"].ToString(), count));
                    newStartTime = newStartTime.Add(timeSpan);
                    timeOfEnd.isStartTimeInPause = false;
                }

            }
            _startTimeRepository.AddInLineStartTime(1, newStartTime, endTime, count, row["ItemId"].ToString(), row["OperationId"].ToString());
        }
        public void AddInLineStartTimeIfEntryExist(string item, string operation, string count)
        {
            checkingForPause = new CheckingForPause(_pauseTimesRepository);
            inLineOperationsId = new InLineOperationsId();

            DateTime newStartTime = new DateTime();
            DateTime endTime = new DateTime();

            double setupTime = 0;
            double time = 0;

            foreach (DataRow row in _dataForInLineTable.GetFromInLine(item, operation).Rows)
                {
                    setupTime = double.Parse((row["SetupTime"]).ToString());
                    time = double.Parse((row["Time"]).ToString());
                newStartTime = Convert.ToDateTime(row["EndTime"]);

                    endTime = timeOfEnd.getEndTimeWithPause( _pauseTimesRepository, newStartTime, time, count, setupTime);

                    if (timeOfEnd.isStartTimeInPause)
                    {
                        TimeSpan timeSpan = checkingForPause.PauseTimes(getStartTimeInPause, newStartTime, timeOfEnd.getEndTimeWithPause(_pauseTimesRepository, newStartTime, time, count, setupTime));
                        newStartTime = newStartTime.Add(timeSpan);
                        timeOfEnd.isStartTimeInPause = false;

                    }

                }
            _startTimeRepository.AddInLineStartTime(inLineOperationsId.Get(_dataForInLineTable, int.Parse(operation.ToString())), newStartTime, endTime, count, item, operation);

        }
        public void AddInLineStartTimeIfEntryExist(DataRow row, string count)
        {
            checkingForPause = new CheckingForPause(_pauseTimesRepository);
            inLineOperationsId = new InLineOperationsId();

            DateTime newStartTime = new DateTime();
            DateTime endTime = new DateTime();

            double setupTime = 0;
            double time = 0;

            foreach (DataRow row1 in _dataForInLineTable.GetFromInLine(row["ItemId"].ToString(), row["OperationId"].ToString()).Rows)
            {
                setupTime = double.Parse((row1["SetupTime"]).ToString());
                time = double.Parse((row1["Time"]).ToString());
                newStartTime = Convert.ToDateTime(row1["EndTime"]);

                endTime = timeOfEnd.getEndTimeWithPause(_pauseTimesRepository, newStartTime, time, count, setupTime);

                if (timeOfEnd.isStartTimeInPause)
                {
                    TimeSpan timeSpan = checkingForPause.PauseTimes(getStartTimeInPause, newStartTime, timeOfEnd.getEndTimeWithPause(_pauseTimesRepository, newStartTime, time, count, setupTime));
                    newStartTime = newStartTime.Add(timeSpan);
                    timeOfEnd.isStartTimeInPause = false;

                }

            }
            _startTimeRepository.AddInLineStartTime(inLineOperationsId.Get(_dataForInLineTable, int.Parse(row["OperationId"].ToString())), newStartTime, endTime, count, row["ItemId"].ToString(), row["OperationId"].ToString());

        }
        private void getStartTimeInPause(bool time)
        {
            timeOfEnd.isStartTimeInPause = time;
        }
    }
}
