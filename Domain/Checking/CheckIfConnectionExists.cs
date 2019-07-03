using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.Checking
{
    public class CheckIfConnectionExists
    {

        private readonly ICheckIfConnectionExistsRepository _checkIfConnectionExistsRepository;
        public CheckIfConnectionExists(ICheckIfConnectionExistsRepository checkIfConnectionExistsRepository)
        {
            _checkIfConnectionExistsRepository = checkIfConnectionExistsRepository;
        }
        public bool Check(string item, string alloperations)
        {
            bool exists = false;
            foreach (DataRow row in _checkIfConnectionExistsRepository.GetTable(item, alloperations).Rows)
            {
                exists = true;
            }
            return exists;
        }
        public bool CheckDuplicate(string item, string duplicateId)
        {
            bool exists = false;
            foreach (DataRow row in _checkIfConnectionExistsRepository.GetDuplicateTable(item, duplicateId).Rows)
            {
                exists = true;
            }
            return exists;
        }
    }
}
