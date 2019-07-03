using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.RetrieveSpecific
{
    public class ItemsWithOperations
    {
        private readonly IItemsWithOperationsRepository _itemsWithOperationsRepository;
        public ItemsWithOperations(IItemsWithOperationsRepository itemsWithOperationsRepository)
        {
            _itemsWithOperationsRepository = itemsWithOperationsRepository;
        }
        public DataTable Items()
        {
            return _itemsWithOperationsRepository.Items();
        }
    }
}
