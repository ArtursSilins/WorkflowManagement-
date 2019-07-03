using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.RetrieveSpecific
{
    public class ItemForGrid
    {
        private readonly IItemForGridRepository _itemForGridRepository;
        
        public ItemForGrid(IItemForGridRepository itemForGridRepository)
        {
            _itemForGridRepository = itemForGridRepository;
        }
        public DataTable View(string item)
        {
            return _itemForGridRepository.View(item);
        }
        public DataTable ItemsWhenDisplayGridClicked(string item)
        {
           return _itemForGridRepository.ItemsWhenDisplayGridClicked(item);
        }
        public DataTable DuplicateView(string item)
        {
            return _itemForGridRepository.DuplicateView(item);
        }
    }
}
