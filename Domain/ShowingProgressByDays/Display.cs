using Domain.Checking;
using Domain.FindTime;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ShowingProgressByDays
{
    public class Display
    {
        private readonly IDisplayRepository _displayRepository;
        private readonly ITimeRepository _timeRepository;
        private readonly IUpdateDisplayRepository _updateDisplayRepository;
        private DisplayTableProgress progress { get; set; } = new DisplayTableProgress();
        private Time time { get; } = new Time();  
        public Display(IDisplayRepository displayRepository, ITimeRepository timeRepository, IUpdateDisplayRepository updateDisplayRepository)
        {
            _displayRepository = displayRepository;
            _timeRepository = timeRepository;
            _updateDisplayRepository = updateDisplayRepository;
        }

        public DataTable SetItemsForDisplay()
        {
            DataTable month = _displayRepository.ItemForDisplay();
            addDataToDisplayTable(month);

            return month;            
        }
        private void addDataToDisplayTable(DataTable month)
        {
            DateTime dateTime = DateTime.Now;
            double day = 0;
            int columnsCount = 1;
            int rowCount = 0;

            DateTime startTime = new DateTime();
            DateTime endTime = new DateTime();

            foreach (DataRow row in month.Rows)
            {
                day = 0;
                columnsCount = 1;
                rowCount = 0;
                foreach (DataColumn dc in month.Columns)
                {
                    if (dc.ColumnName == "Id")
                    {
                        startTime = time.GetMinStartTime(_timeRepository, row);
                        endTime = time.GetMaxEndTime(_timeRepository, row);
                    }

                    DayOfWeek dayOfWeek = dateTime.AddDays(day).DayOfWeek;
                    if (dc.ColumnName != "Id" && dc.ColumnName != "Item")
                    {
                        if (dayOfWeek == DayOfWeek.Sunday || dayOfWeek == DayOfWeek.Saturday)
                        {
                            dc.ColumnName = (dateTime.AddDays(day)).ToString("    yyyy-MM-dd");

                            byte[] picture = progress.GetProcentagePicture(dc, startTime, endTime);


                            _updateDisplayRepository.AddPictureToDisplay(row, dc, picture, rowCount, columnsCount);

                            dc.ColumnName = (dateTime.AddDays(day)).ToString("   #yyyy-MM-dd#");

                        }
                        else
                        {
                            dc.ColumnName = (dateTime.AddDays(day)).ToString("    yyyy-MM-dd");

                            byte[] picture = progress.GetProcentagePicture(dc, startTime, endTime);

                            _updateDisplayRepository.AddPictureToDisplay(row, dc, picture, rowCount, columnsCount);
                        }
                        columnsCount++;
                        day++;

                    }

                }

                rowCount++;
            }

        }
      
    }
}
