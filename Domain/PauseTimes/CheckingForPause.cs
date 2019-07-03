using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PauseTimes
{
    public class CheckingForPause
    {
        public bool isStartTimeInPause { get; set; }
        public delegate void getStartTimeInPause(bool isStartTimeInPause);
        private readonly IPauseTimesRepository _pauseTimesRepository;
        public CheckingForPause(IPauseTimesRepository pauseTimesRepository)
        {
            _pauseTimesRepository = pauseTimesRepository;
        }
        public TimeSpan PauseTimes(getStartTimeInPause getStart, DateTime startTime, DateTime endTime)
        {
            isStartTimeInPause = false;

            double day = 0;
            int counter = 0;
            int dayCount = 0;

            DateTime start = new DateTime(startTime.Year, startTime.Month, startTime.Day, 00, 00, 00);
            DateTime end = new DateTime(endTime.Year, endTime.Month, endTime.Day, 00, 00, 00);

            TimeSpan timeSpan = new TimeSpan();
            TimeSpan allDayPauses = new TimeSpan();

            DataTable pauseTable = _pauseTimesRepository.GetPauseTimes();

            DateTime startDayEnd = new DateTime(startTime.Year, startTime.Month, startTime.Day, 23, 59, 59);
            DateTime endDayStart = new DateTime(endTime.Year, endTime.Month, endTime.Day, 00, 00, 00);

            DateTime fakeTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, 00, 00, 00);
            while (start != end)
            {
                counter++;
                day++;
                start = fakeTime.AddDays(day);
            }

            dayCount = counter;
            if (counter > 1) counter = 2;

            DateTime endTime1 = new DateTime();

            foreach (DataRow item in pauseTable.Rows)
            {

                switch (counter)
                {
                    case 0:
                        if (startTime.TimeOfDay < Convert.ToDateTime(item["PauseStart"]).TimeOfDay && endTime.TimeOfDay > Convert.ToDateTime(item["PauseEnd"]).TimeOfDay)
                        {
                            timeSpan += Convert.ToDateTime(item["PauseEnd"]).TimeOfDay.Subtract(Convert.ToDateTime(item["PauseStart"]).TimeOfDay);
                            endTime1 = endTime.Add(timeSpan);
                        }
                        else if (startTime.TimeOfDay > Convert.ToDateTime(item["PauseStart"]).TimeOfDay && startTime.TimeOfDay < Convert.ToDateTime(item["PauseEnd"]).TimeOfDay)
                        {
                            timeSpan += Convert.ToDateTime(item["PauseEnd"]).TimeOfDay.Subtract(startTime.TimeOfDay);
                            endTime1 = endTime.Add(timeSpan);

                            isStartTimeInPause = true;
                            getStart(isStartTimeInPause);

                        }
                      
                        else if (startTime.TimeOfDay < Convert.ToDateTime(item["PauseStart"]).TimeOfDay && endTime.TimeOfDay > Convert.ToDateTime(item["PauseStart"]).TimeOfDay && endTime.TimeOfDay < Convert.ToDateTime(item["PauseEnd"]).TimeOfDay)
                        {
                            timeSpan += endTime.TimeOfDay.Subtract(Convert.ToDateTime(item["PauseStart"]).TimeOfDay);
                            endTime1 = endTime.Add(timeSpan);
                        }
                        break;
                    case 1:
                        if (startTime.TimeOfDay < Convert.ToDateTime(item["PauseStart"]).TimeOfDay && startDayEnd.TimeOfDay > Convert.ToDateTime(item["PauseEnd"]).TimeOfDay)
                        {
                            timeSpan += Convert.ToDateTime(item["PauseEnd"]).TimeOfDay.Subtract(Convert.ToDateTime(item["PauseStart"]).TimeOfDay);
                            endTime1 = endTime.Add(timeSpan);
                        }
                        else if (startTime.TimeOfDay > Convert.ToDateTime(item["PauseStart"]).TimeOfDay && startTime.TimeOfDay < Convert.ToDateTime(item["PauseEnd"]).TimeOfDay)
                        {
                            timeSpan += Convert.ToDateTime(item["PauseEnd"]).TimeOfDay.Subtract(startTime.TimeOfDay);
                            endTime1 = endTime.Add(timeSpan);
                        }
                        else if (endDayStart.TimeOfDay < Convert.ToDateTime(item["PauseStart"]).TimeOfDay && endTime.TimeOfDay > Convert.ToDateTime(item["PauseEnd"]).TimeOfDay)
                        {
                            timeSpan += Convert.ToDateTime(item["PauseEnd"]).TimeOfDay.Subtract(Convert.ToDateTime(item["PauseStart"]).TimeOfDay);
                            endTime1 = endTime.Add(timeSpan);
                        }
                        else if (endTime.TimeOfDay > Convert.ToDateTime(item["PauseStart"]).TimeOfDay && endTime.TimeOfDay < Convert.ToDateTime(item["PauseEnd"]).TimeOfDay)
                        {
                            timeSpan += Convert.ToDateTime(item["PauseEnd"]).TimeOfDay.Subtract(Convert.ToDateTime(item["PauseStart"]).TimeOfDay);
                            endTime1 = endTime.Add(timeSpan);
                        }
                        break;
                    case 2:
                        int helper = 0;

                        if (startTime.TimeOfDay < Convert.ToDateTime(item["PauseStart"]).TimeOfDay && startDayEnd.TimeOfDay > Convert.ToDateTime(item["PauseEnd"]).TimeOfDay)
                        {
                            timeSpan += Convert.ToDateTime(item["PauseEnd"]).TimeOfDay.Subtract(Convert.ToDateTime(item["PauseStart"]).TimeOfDay);
                            endTime1 = endTime.Add(timeSpan);
                        }
                        else if (startTime.TimeOfDay > Convert.ToDateTime(item["PauseStart"]).TimeOfDay && startTime.TimeOfDay < Convert.ToDateTime(item["PauseEnd"]).TimeOfDay)
                        {
                            timeSpan += Convert.ToDateTime(item["PauseEnd"]).TimeOfDay.Subtract(startTime.TimeOfDay);
                            endTime1 = endTime.Add(timeSpan);
                            isStartTimeInPause = true;
                            getStart(isStartTimeInPause);
                        }
                        else if (endDayStart.TimeOfDay < Convert.ToDateTime(item["PauseStart"]).TimeOfDay && endTime.TimeOfDay > Convert.ToDateTime(item["PauseEnd"]).TimeOfDay)
                        {
                            timeSpan += Convert.ToDateTime(item["PauseEnd"]).TimeOfDay.Subtract(Convert.ToDateTime(item["PauseStart"]).TimeOfDay);
                            endTime1 = endTime.Add(timeSpan);
                        }
                        else if (endTime.TimeOfDay > Convert.ToDateTime(item["PauseStart"]).TimeOfDay && endTime.TimeOfDay < Convert.ToDateTime(item["PauseEnd"]).TimeOfDay)
                        {
                            timeSpan += Convert.ToDateTime(item["PauseEnd"]).TimeOfDay.Subtract(endTime.TimeOfDay);
                            endTime1 = endTime.Add(timeSpan);
                            isStartTimeInPause = true;
                            getStart(isStartTimeInPause);
                        }
                        while (helper != dayCount - 1)
                        {
                            helper++;
                            allDayPauses += Convert.ToDateTime(item["PauseEnd"]).TimeOfDay.Subtract(Convert.ToDateTime(item["PauseStart"]).TimeOfDay);
                            endTime1 = endTime.Add(timeSpan);
                        }

                        break;

                }

            }
            TimeSpan pauseTime = new TimeSpan();
            if (!chekIfDayChange(endTime, endTime1) && endTime1.Year.ToString() != "1")
            {
                pauseTime = pauseTimes2(getStart, startTime, endTime1);
            }
            else
            {
                pauseTime = timeSpan.Add(allDayPauses);
            }


            return pauseTime;
        }
        private bool chekIfDayChange(DateTime endTime, DateTime endTime1)
        {
            bool change = false;
            if (endTime < endTime1)
            {
                change = true;
            }
            return change;
        }
        private TimeSpan pauseTimes2(getStartTimeInPause getStart, DateTime startTime, DateTime endTime)
        {
            isStartTimeInPause = false;

            double day = 0;
            int counter = 0;
            int dayCount = 0;

            DateTime start = new DateTime(startTime.Year, startTime.Month, startTime.Day, 00, 00, 00);
            DateTime end = new DateTime(endTime.Year, endTime.Month, endTime.Day, 00, 00, 00);

            TimeSpan timeSpan = new TimeSpan();
            TimeSpan allDayPauses = new TimeSpan();

            DataTable pauseTable = _pauseTimesRepository.GetPauseTimes();

            DateTime startDayEnd = new DateTime(startTime.Year, startTime.Month, startTime.Day, 23, 59, 59);
            DateTime endDayStart = new DateTime(endTime.Year, endTime.Month, endTime.Day, 00, 00, 00);

            DateTime fakeTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, 00, 00, 00);
            while (start != end)
            {
                counter++;
                day++;
                start = fakeTime.AddDays(day);
            }

            dayCount = counter;
            if (counter > 1) counter = 2;

            foreach (DataRow item in pauseTable.Rows)
            {

                switch (counter)
                {
                    case 0:
                        if (startTime.TimeOfDay < Convert.ToDateTime(item["PauseStart"]).TimeOfDay && endTime.TimeOfDay > Convert.ToDateTime(item["PauseEnd"]).TimeOfDay)
                        {
                            timeSpan += Convert.ToDateTime(item["PauseEnd"]).TimeOfDay.Subtract(Convert.ToDateTime(item["PauseStart"]).TimeOfDay);
                        }
                        else if (startTime.TimeOfDay > Convert.ToDateTime(item["PauseStart"]).TimeOfDay && startTime.TimeOfDay < Convert.ToDateTime(item["PauseEnd"]).TimeOfDay && endTime.TimeOfDay < Convert.ToDateTime(item["PauseEnd"]).TimeOfDay)
                        {
                            timeSpan += Convert.ToDateTime(item["PauseEnd"]).TimeOfDay.Subtract(startTime.TimeOfDay);

                            isStartTimeInPause = true;
                            getStart(isStartTimeInPause);
                        }
                        else if (startTime.TimeOfDay > Convert.ToDateTime(item["PauseStart"]).TimeOfDay && startTime.TimeOfDay < Convert.ToDateTime(item["PauseEnd"]).TimeOfDay && endTime.TimeOfDay > Convert.ToDateTime(item["PauseEnd"]).TimeOfDay)
                        {
                            timeSpan += endTime.TimeOfDay.Subtract(Convert.ToDateTime(item["PauseStart"]).TimeOfDay);
;
                            isStartTimeInPause = true;
                            getStart(isStartTimeInPause);
                        }
                        else if (startTime.TimeOfDay < Convert.ToDateTime(item["PauseStart"]).TimeOfDay && endTime.TimeOfDay > Convert.ToDateTime(item["PauseStart"]).TimeOfDay && endTime.TimeOfDay < Convert.ToDateTime(item["PauseEnd"]).TimeOfDay)
                        {
                            timeSpan += endTime.TimeOfDay.Subtract(Convert.ToDateTime(item["PauseStart"]).TimeOfDay);
                        }
                        break;
                    case 1:
                        if (startTime.TimeOfDay < Convert.ToDateTime(item["PauseStart"]).TimeOfDay && startDayEnd.TimeOfDay > Convert.ToDateTime(item["PauseEnd"]).TimeOfDay)
                        {
                            timeSpan += Convert.ToDateTime(item["PauseEnd"]).TimeOfDay.Subtract(Convert.ToDateTime(item["PauseStart"]).TimeOfDay);
                        }
                        else if (startTime.TimeOfDay > Convert.ToDateTime(item["PauseStart"]).TimeOfDay && startTime.TimeOfDay < Convert.ToDateTime(item["PauseEnd"]).TimeOfDay)
                        {
                            timeSpan += Convert.ToDateTime(item["PauseEnd"]).TimeOfDay.Subtract(startTime.TimeOfDay);
                        }
                        else if (endDayStart.TimeOfDay < Convert.ToDateTime(item["PauseStart"]).TimeOfDay && endTime.TimeOfDay > Convert.ToDateTime(item["PauseEnd"]).TimeOfDay)
                        {
                            timeSpan += Convert.ToDateTime(item["PauseEnd"]).TimeOfDay.Subtract(Convert.ToDateTime(item["PauseStart"]).TimeOfDay);
                        }
                        else if (endTime.TimeOfDay > Convert.ToDateTime(item["PauseStart"]).TimeOfDay && endTime.TimeOfDay < Convert.ToDateTime(item["PauseEnd"]).TimeOfDay)
                        {
                            timeSpan += Convert.ToDateTime(item["PauseEnd"]).TimeOfDay.Subtract(Convert.ToDateTime(item["PauseStart"]).TimeOfDay);
                        }
                        break;
                    case 2:
                        int helper = 0;

                        if (startTime.TimeOfDay < Convert.ToDateTime(item["PauseStart"]).TimeOfDay && startDayEnd.TimeOfDay > Convert.ToDateTime(item["PauseEnd"]).TimeOfDay)
                        {
                            timeSpan += Convert.ToDateTime(item["PauseEnd"]).TimeOfDay.Subtract(Convert.ToDateTime(item["PauseStart"]).TimeOfDay);
                        }
                        else if (startTime.TimeOfDay > Convert.ToDateTime(item["PauseStart"]).TimeOfDay && startTime.TimeOfDay < Convert.ToDateTime(item["PauseEnd"]).TimeOfDay)
                        {
                            timeSpan += Convert.ToDateTime(item["PauseEnd"]).TimeOfDay.Subtract(startTime.TimeOfDay);
                            isStartTimeInPause = true;
                            getStart(isStartTimeInPause);
                        }
                        else if (endDayStart.TimeOfDay < Convert.ToDateTime(item["PauseStart"]).TimeOfDay && endTime.TimeOfDay > Convert.ToDateTime(item["PauseEnd"]).TimeOfDay)
                        {
                            timeSpan += Convert.ToDateTime(item["PauseEnd"]).TimeOfDay.Subtract(Convert.ToDateTime(item["PauseStart"]).TimeOfDay);
                        }
                        else if (endTime.TimeOfDay > Convert.ToDateTime(item["PauseStart"]).TimeOfDay && endTime.TimeOfDay < Convert.ToDateTime(item["PauseEnd"]).TimeOfDay)
                        {
                            timeSpan += Convert.ToDateTime(item["PauseEnd"]).TimeOfDay.Subtract(endTime.TimeOfDay);
                            isStartTimeInPause = true;
                            getStart(isStartTimeInPause);
                        }
                        while (helper != dayCount - 1)
                        {
                            helper++;
                            allDayPauses += Convert.ToDateTime(item["PauseEnd"]).TimeOfDay.Subtract(Convert.ToDateTime(item["PauseStart"]).TimeOfDay);
                        }

                        break;

                }

            }

            TimeSpan pauseTime = timeSpan.Add(allDayPauses);
            return pauseTime;
        }
    }
}
