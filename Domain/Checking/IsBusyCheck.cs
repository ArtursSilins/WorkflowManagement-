using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Checking
{
    public class IsBusyCheck
    {
        private readonly IIsBusyCheckRepository _isBusyCheckRepository;
        public IsBusyCheck(IIsBusyCheckRepository isBusyCheckRepository)
        {
            _isBusyCheckRepository = isBusyCheckRepository;
        }
        public bool GetOperationBool(string operation)
        {
            bool check = false;
           
                foreach (DataRow row in _isBusyCheckRepository.GetTable(operation).Rows)
                {
                    if (row["Busy"].ToString() == true.ToString())
                    {
                        check = true;
                    }

                }
            
            return check;
        }
        public bool GetItemBool(string item)
        {
            bool check = false;

            foreach (DataRow row in _isBusyCheckRepository.GetItemTable(item).Rows)
            {
                if (row["Busy"].ToString() == true.ToString())
                {
                    check = true;
                }

            }

            return check;
        }
        public bool GetDuplicateOperationBool(string duplicateId)
        {
            bool check = false;

            foreach (DataRow row in _isBusyCheckRepository.GetDuplicateTable(duplicateId).Rows)
            {
                if (row["Busy"].ToString() == true.ToString())
                {
                    check = true;
                }

            }

            return check;
        }
    }
}
