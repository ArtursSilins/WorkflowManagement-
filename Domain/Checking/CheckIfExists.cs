using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Domain.Interfaces;

namespace Domain.Checking
{
    public class CheckIfExists
    {
        private readonly ICheckIfExistRepository _checkIfExistRepository;
        public CheckIfExists(ICheckIfExistRepository checkIfExistRepository)
        {
            _checkIfExistRepository = checkIfExistRepository;
        }
        public bool IfItemExists(string id)
        {
            bool exists = false;

            foreach (DataRow row in _checkIfExistRepository.GetItemTable(id).Rows)
            {
                exists = true;
            }
            return exists;
        }
        public bool IfOperationExists(string id)
        {
            bool exists = false;

            foreach (DataRow row in _checkIfExistRepository.GetOperationTable(id).Rows)
            {
                exists = true;
            }
            return exists;
        }
        public bool IfInLineItemExists(string itemId, string operationId)
        {
            bool exists = false;

            foreach (DataRow row in _checkIfExistRepository.GetInLineItemOperation(itemId, operationId).Rows)
            {
                exists = true;
            }
            return exists;
        }
    }
}
