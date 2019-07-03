using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Adding
{
    public class ItemsOperationsConnection
    {
        private readonly IItemsOperationsConnectionRepository _itemsOperationsConnectionRepository;
        private readonly IDuplicateOperationsRepository _duplicateOperationsRepository;
        public ItemsOperationsConnection(IItemsOperationsConnectionRepository itemsOperationsConnectionRepository, IDuplicateOperationsRepository duplicateOperationsRepository)
        {
            _itemsOperationsConnectionRepository = itemsOperationsConnectionRepository;
            _duplicateOperationsRepository = duplicateOperationsRepository;
        }
        public void AddWithDuplicate(string itemId, string duplicateId, string time, string setupTime)
        {
            foreach (DataRow row in _duplicateOperationsRepository.Get(duplicateId).Rows)
            {
                _itemsOperationsConnectionRepository.AddConnectionWithDuplicate(itemId, row["Id"].ToString(), time, setupTime, duplicateId);
            }           
        }
        public void Add(string itemId, string operationId, string time, string setupTime)
        {
            _itemsOperationsConnectionRepository.AddConnection(itemId, operationId, time, setupTime);
        }
    }
}
