using Domain.Interfaces;
using Domain.PauseTimes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Domain.FindTime
{
    public class Time
    {
        public bool isStartTimeInPause { get; set; }
     
        private CheckingForPause checkingForPause { get; set; }
       
        public DateTime getEndTime(DateTime startTime, double necessaryTime, int count, double SetupTime)
        {

            DateTime time = new DateTime();

            time = startTime.AddSeconds(((necessaryTime*60) * count) + (SetupTime*60));

            return time;
        }
        public DateTime getEndTimeWithPause(IPauseTimesRepository pauseTimesRepository, DateTime newStartTime, double necessaryTime, string textBoxRunItemsCount, double sagatavošanasLasiks)
        {
            checkingForPause = new CheckingForPause(pauseTimesRepository);

            DateTime endTimeWithoutPauses = getEndTime(newStartTime, necessaryTime, int.Parse(textBoxRunItemsCount), sagatavošanasLasiks);

            DateTime endTime = endTimeWithoutPauses.Add(checkingForPause.PauseTimes(getStartTimeInPause, newStartTime, endTimeWithoutPauses));
            return endTime;
        }
        public DateTime getEndTimeWithPause(IPauseTimesRepository pauseTimesRepository, ITimeRepository timeRepository, DataRow row, string textBoxItemsCount)
        {
            checkingForPause = new CheckingForPause(pauseTimesRepository);
            DateTime time = DateTime.Now;
            DateTime endTimeWithoutPauses = getEndTime(time, timeRepository.GetTime(row), Int32.Parse(textBoxItemsCount), timeRepository.GetSetupTime(row));

            DateTime endTime = endTimeWithoutPauses.Add(checkingForPause.PauseTimes(getStartTimeInPause, time, endTimeWithoutPauses));
            return endTime;
        }
        public DateTime getEndTimeWithPause(IPauseTimesRepository pauseTimesRepository, ITimeRepository timeRepository, string comboBoxItems, string comboBoxOperations, string textBoxCount)
        {
            checkingForPause = new CheckingForPause(pauseTimesRepository);
            DateTime time = DateTime.Now;
            DateTime endTimeWithoutPauses = getEndTime(time, timeRepository.GetTime(comboBoxItems, comboBoxOperations), Int32.Parse(textBoxCount), timeRepository.GetSetupTime(comboBoxItems, comboBoxOperations));

            DateTime endTime = endTimeWithoutPauses.Add(checkingForPause.PauseTimes(getStartTimeInPause, time, endTimeWithoutPauses)); 
            return endTime;
        }
        private void getStartTimeInPause(bool time)
        {
            isStartTimeInPause = time;
        }
        public DateTime GetMinStartTime(ITimeRepository timeRepository, DataRow dataRow)
        {
            DateTime time = new DateTime();

            foreach (DataRow row in timeRepository.GetMinStartTime(dataRow).Rows)
            {
                if (row["time"].ToString() == "")
                {
                    time = timeRepository.GetMinStartTimeFromInLine(dataRow);
                }
                else
                {
                    time = Convert.ToDateTime(row["time"]);
                }

            }

            return time;

        }
        public DateTime GetMaxEndTime(ITimeRepository timeRepository, DataRow dataRow)
        {
            DateTime time = new DateTime();

            foreach (DataRow row in timeRepository.GetMaxEndTimeFromInLine(dataRow).Rows)
            {
                if (row["time"].ToString() == "")
                {
                    time = timeRepository.GetMaxEndTimeFromItemsOperationsDisplay(dataRow);
                }
                else
                {
                    time = Convert.ToDateTime(row["time"]);
                }

            }

            return time;
        }
        public DateTime GetMaxEndTimeFromItemsOperations(ITimeRepository timeRepository, DataRow dataRow)
        {
        
               DateTime time = timeRepository.GetMaxEndTimeFromItemsOperations(dataRow);
         
            return time;
        }
        public DateTime GetMaxEndTimeFromInLine(ITimeRepository timeRepository, DataRow dataRow)
        {
            DateTime time = new DateTime();

            foreach (DataRow row in timeRepository.GetMaxEndTimeFromInLine(dataRow).Rows)
            {              
                    time = Convert.ToDateTime(row["time"]);
            }

            return time;
        }
    }
}
