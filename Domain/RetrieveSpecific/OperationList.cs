using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RetrieveSpecific
{
    public class OperationList
    {
        private readonly IOperationList _operationList;
        public OperationList(IOperationList operationList)
        {
            _operationList = operationList;
        }
        public DataTable List(string id)
        {
            return _operationList.List(id); 
        }
        public DataTable DuplicateList(string duplicateId)
        {
            return _operationList.DuplicateList(duplicateId);
        }
    }
}
