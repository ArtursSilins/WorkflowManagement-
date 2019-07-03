using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Domain.Interfaces;


namespace Domain.Adding
{
    public class AddNew
    {
        private readonly IAdd _iAdd;
        private readonly IDuplicateOperationsRepository _duplicateOperationsRepository;
        private readonly IItemsOperationsConnectionRepository _itemsOperationsConnectionRepository;
        public AddNew(IAdd add, IDuplicateOperationsRepository duplicateOperationsRepository, IItemsOperationsConnectionRepository itemsOperationsConnectionRepository)
        {        
            _iAdd = add;
            _duplicateOperationsRepository = duplicateOperationsRepository;
            _itemsOperationsConnectionRepository = itemsOperationsConnectionRepository;
        }
        public void AddItem(string id, string name)
        {         
            _iAdd.Item(id, name);
        }
        public void AddOperation(string id, string name)
        {
            _iAdd.Operation(id, name);
        }
        public void ItemToDisplay(string id, string name)
        {
            _iAdd.ItemToDisplay(id, name);
        }
        public void AddDuplicateName(string name)
        {
            _iAdd.DuplicateName(name);
        }
        public void AddDuplicateOperation(string id, string name, string duplicateId)
        {
            foreach (DataRow row in _duplicateOperationsRepository.ExistingDuplicateConnections(duplicateId).Rows)
            {
                _itemsOperationsConnectionRepository.AddConnectionWithDuplicate(row["ItemId"].ToString(), id, row["Time"].ToString(), row["SetupTime"].ToString(), duplicateId);
            }
            _iAdd.DuplicateOperation(id, name, duplicateId);
        }
    }
}
