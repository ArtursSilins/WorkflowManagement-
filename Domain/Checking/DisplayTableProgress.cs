using Domain.Interfaces;
using Domain.PauseTimes;
using Domain.ProgressPicture;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Checking
{
    public class DisplayTableProgress
    {
        private CheckingForPause CheckingForPause { get; set; }

        private Picture Picture { get; set; } = new Picture();
        public byte[] GetProcentagePicture(DataColumn column, DateTime StartTime, DateTime EndTime)
        {
            float result = 0;
            float startLinePosition = 0;
            float endLinePosition = 0;

            byte[] picture = Picture.Create(0,0);

            if (column.ColumnName != "Id")
            {
                DateTime EndTimeDate = Convert.ToDateTime(EndTime.ToString("yyyy-MM-dd "));
                DateTime startTimeDate = Convert.ToDateTime(StartTime.ToString("yyyy-MM-dd "));
                DateTime columnDate = Convert.ToDateTime(column.ColumnName);

                if (startTimeDate < columnDate && EndTimeDate == columnDate)
                {
                    endLinePosition = (timeSpanSeconds(EndTime) / 86400) * 100;
                    picture = Picture.Create(0, endLinePosition);
                }
                else if (startTimeDate < columnDate && EndTimeDate > columnDate)
                {

                    picture = Picture.Create(0, 100);

                }
                else if (startTimeDate > columnDate)
                {
                    picture = Picture.Create(0, 0);
                }
                else if (startTimeDate == columnDate && EndTimeDate != columnDate)
                {
                    startLinePosition = (timeSpanSeconds(StartTime) / 86400) * 100;
                    picture = Picture.Create(startLinePosition, 100);
                }

                if (EndTimeDate < columnDate)
                {
                    picture = Picture.Create(0, 0);
                }


                if (startTimeDate == columnDate && EndTimeDate == columnDate)
                {
                    startLinePosition = (timeSpanSeconds(StartTime) / 86400) * 100;

                    endLinePosition = (timeSpanSeconds(EndTime) / 86400) * 100;

                    result = endLinePosition - startLinePosition;

                    picture = Picture.Create(startLinePosition, result);


                }
            }

            return picture;
        }
        private float timeSpanSeconds(DateTime dateTime)
        {
            float seconds = 0;
            DateTime startTime = Convert.ToDateTime(dateTime.ToString("yyyy-MM-dd 00:00:00"));
            TimeSpan timeSpan = dateTime.Subtract(startTime);
            seconds = float.Parse(timeSpan.TotalSeconds.ToString());

            return seconds;
        }
        public byte[] GetProcentagePictureForViewForm2(IPauseTimesRepository pauseTimesRepository, DataRow row, double completedTime)
        {
            CheckingForPause = new CheckingForPause(pauseTimesRepository);

            byte[] picture = Picture.Create(0, 0);

            TimeSpan timeSp = Convert.ToDateTime(row["EndTime"]).Subtract(Convert.ToDateTime(row["StartTime"]));
            double processDuration = timeSp.TotalSeconds;

            DateTime timeNow = DateTime.Now;

            double pauseTime = double.Parse(CheckingForPause.PauseTimes(getStartTimeInPause, Convert.ToDateTime(row["StartTime"].ToString()), Convert.ToDateTime(row["EndTime"].ToString())).Minutes.ToString()); //pause.testPauseIftrueAddTime(getStartTimeInPause, Convert.ToDateTime(item["StartaLaiks"].ToString()), Convert.ToDateTime(item["BeiguLaiks"].ToString())).Minutes.ToString());

            if (processDuration > completedTime)
            {

                int procentage = Convert.ToInt32((completedTime / processDuration) * 100);

                picture = Picture.CreateWithProcentage(procentage);

                if (procentage == 100)
                {
                  picture = Picture.Create(0, 0);
                }
            }

            return picture;

        }
        private static void getStartTimeInPause(bool time)
        {

        }
    }
}
