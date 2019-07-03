using Domain.Interfaces;
using Domain.PauseTimes;
using Domain.ProgressPicture;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Delete
{
    public class Form2ExpirdTimeCountPicture
    {
        private Picture Picture { get; } = new Picture();
        public void Delete(IItemsForForm2Repository itemsForForm2Repository, IItemsOperationsNullRepository itemsOperationsNullRepository, string itemId)
        {
            foreach (DataRow row in itemsForForm2Repository.Get(itemId).Rows)
            {
                double SetupTime = double.Parse(row["SetupTime"].ToString());

                if (row["StartTime"] != System.DBNull.Value)
                {
                    if (row["Count"] == System.DBNull.Value)
                    {
                        row["Count"] = "1";
                    }
                    DateTime laiksTagad = DateTime.Now;

                    DateTime time = Convert.ToDateTime(row["EndTime"]);

                    if (time < laiksTagad)
                    {
                        byte[] pic = Picture.CreatePictureComplited();

                        itemsOperationsNullRepository.InsertNullWhereEndTimeExpired(pic, row);
                    }

                }
            }
        }
        private static void getStartTimeInPause(bool time)
        {

        }
    }
}
