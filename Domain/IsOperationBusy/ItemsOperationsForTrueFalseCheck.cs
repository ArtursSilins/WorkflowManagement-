using Domain.Interfaces;
using Domain.ProgressPicture;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IsOperationBusy
{
    public class ItemsOperationsForTrueFalseCheck
    {
        private Picture Picture { get; set; } = new Picture();

        private readonly IItemsOperationsForTrueFalseCheckReposytory _itemsOperationsForTrueFalseCheckReposytory;
        private readonly IOperationConditionRepository _operationConditionRepository;
        private readonly IItemsOperationsNullRepository _itemsOperationsNullRepository;
        public ItemsOperationsForTrueFalseCheck(IItemsOperationsForTrueFalseCheckReposytory itemsOperationsForTrueFalseCheckReposytory,
            IOperationConditionRepository operationConditionRepository,
            IItemsOperationsNullRepository itemsOperationsNullRepository)
        {
            _itemsOperationsForTrueFalseCheckReposytory = itemsOperationsForTrueFalseCheckReposytory;
            _operationConditionRepository = operationConditionRepository;
            _itemsOperationsNullRepository = itemsOperationsNullRepository;
        }

        public void Checking()
        {
            foreach (DataRow row in _itemsOperationsForTrueFalseCheckReposytory.GetItemsOperationsTable().Rows)
            {
                if (row["EndTime"] == DBNull.Value)
                {
                    _operationConditionRepository.OperationIsNotBusy(row["ItemId"].ToString(), row["OperationId"].ToString());
                }
                if (row["StartTime"] != System.DBNull.Value && row["Count"] != System.DBNull.Value && row["SetupTime"] != System.DBNull.Value)
                {
                    DateTime TimeNow = DateTime.Now;

                    DateTime EndTime = Convert.ToDateTime(row["EndTime"]);

                    if (EndTime > TimeNow)
                    {
                        _operationConditionRepository.OperationIsBusy(row["ItemId"].ToString(), row["OperationId"].ToString());
                    }
                    else
                    {
                        _operationConditionRepository.OperationIsNotBusy(row["ItemId"].ToString(), row["OperationId"].ToString());
                        _itemsOperationsNullRepository.InsertNull(Picture.Create(0,0), row);
                    }
                }
                else
                {
                    _operationConditionRepository.OperationIsNotBusy(row["ItemId"].ToString(), row["OperationId"].ToString());
                    _itemsOperationsNullRepository.InsertNull(Picture.Create(0, 0), row);
                }
            }
        }
        public void RunAllChecking(string item)
        {
            foreach (DataRow row in _itemsOperationsForTrueFalseCheckReposytory.GetItemsOperationsTableRunAll(item).Rows)
            {
                if (row["EndTime"] == DBNull.Value)
                {
                    _operationConditionRepository.OperationIsNotBusy(row["ItemId"].ToString(), row["OperationId"].ToString());
                }
                if (row["StartTime"] != System.DBNull.Value && row["Count"] != System.DBNull.Value && row["SetupTime"] != System.DBNull.Value)
                {
                    DateTime TimeNow = DateTime.Now;

                    DateTime EndTime = Convert.ToDateTime(row["EndTime"]);

                    if (EndTime > TimeNow)
                    {
                        _operationConditionRepository.OperationIsBusy(row["ItemId"].ToString(), row["OperationId"].ToString());
                    }
                    else
                    {
                        _operationConditionRepository.OperationIsNotBusy(row["ItemId"].ToString(), row["OperationId"].ToString());
                        _itemsOperationsNullRepository.InsertNull(Picture.Create(0, 0), row);
                    }
                }
                else
                {
                    _operationConditionRepository.OperationIsNotBusy(row["ItemId"].ToString(), row["OperationId"].ToString());
                    _itemsOperationsNullRepository.InsertNull(Picture.Create(0, 0), row);
                }
            }
        }
    }
}
