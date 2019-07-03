using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RetrieveSpecific
{
    public class InLineOperationsId
    {
        public int Get(IDataForInLineTable dataForInLineTable, int operation)
        {
            int nr = 0;
            foreach (DataRow row in dataForInLineTable.GetOperationsId(operation).Rows)
            {
                if (int.Parse(row["OperationId"].ToString()) == int.Parse(operation.ToString()))
                {
                    nr += 1;
                }
            }
            return nr + 1;
        }
    }
}
