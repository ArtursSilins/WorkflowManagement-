using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IItemsOperationsNullRepository
    {
        void InsertNull(byte[] picture, DataRow row);
        void InsertNullWhereEndTimeExpired(byte[] picture, DataRow row);
    }
}
