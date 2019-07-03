using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Checking;
using Domain.Interfaces;

namespace Domain.Form2
{
    public class CombiningDataForGrid
    {
        private readonly IItemsForForm2Repository _itemsForForm2Repository;
        private readonly IItemsOperationsNullRepository _itemsOperationsNullRepository;
        private readonly IForm2ProgressPictureRepository _form2ProgressPictureRepository;
        private readonly IDataForForm2ProgressTxtRepository _dataForForm2ProgressTxtRepository;
        private readonly IPauseTimesRepository _pauseTimesRepository;
        private Delete.Form2ExpirdTimeCountPicture Form2ExpirdTimeCountPicture { get; set; } = new Delete.Form2ExpirdTimeCountPicture();
        private DisplayTableProgress DisplayTableProgress { get; set; } = new DisplayTableProgress();
        private ProgressPicture.Picture Picture { get; } = new ProgressPicture.Picture();

        public CombiningDataForGrid(IItemsForForm2Repository itemsForForm2Repository, IItemsOperationsNullRepository itemsOperationsNullRepository,
            IForm2ProgressPictureRepository form2ProgressPictureRepository, IDataForForm2ProgressTxtRepository dataForForm2ProgressTxtRepository,
            IPauseTimesRepository pauseTimesRepository)
        {
            _itemsForForm2Repository = itemsForForm2Repository;
            _itemsOperationsNullRepository = itemsOperationsNullRepository;
            _form2ProgressPictureRepository = form2ProgressPictureRepository;
            _dataForForm2ProgressTxtRepository = dataForForm2ProgressTxtRepository;
            _pauseTimesRepository = pauseTimesRepository;
        }

        public void Update(string itemId)
        {
            Form2ExpirdTimeCountPicture.Delete(_itemsForForm2Repository, _itemsOperationsNullRepository, itemId);

            foreach (DataRow row in _itemsForForm2Repository.Get(itemId).Rows)
            {
                if (row["EndTime"] != System.DBNull.Value)
                {
                    DateTime startTime = Convert.ToDateTime(row["StartTime"]);
                    DateTime timeNow = DateTime.Now;

                    TimeSpan completedTime = timeNow.Subtract(startTime);

                    double completedTimeInSeconds = completedTime.TotalSeconds;

                    byte[] pic = DisplayTableProgress.GetProcentagePictureForViewForm2(_pauseTimesRepository, row, completedTimeInSeconds);

                    DateTime endTiem = Convert.ToDateTime(row["EndTime"]);

                    _form2ProgressPictureRepository.UpdatePicture(pic, row, itemId);
                }
                else
                {
                 
                    byte[] pic = Picture.Create(0,0);

                    _form2ProgressPictureRepository.UpdatePicture(pic, row, itemId);
                  
                }
                if (row["EndTime"] == DBNull.Value)
                {
                    foreach (DataRow row2 in _dataForForm2ProgressTxtRepository.WaitingData(itemId).Rows)
                    {
                        byte[] pic = Picture.CreatePictureWaiting();
                        _form2ProgressPictureRepository.UpdatePicture(pic, row2);
                    }

                }
                if (_dataForForm2ProgressTxtRepository.CompletedData(itemId, row["OperationId"].ToString()).Rows.Count != 0)
                {
                    byte[] pic = Picture.CreatePictureCompleted();
                    _form2ProgressPictureRepository.UpdatePicture(pic, row, itemId);
                }
            }
        }
    }
}
