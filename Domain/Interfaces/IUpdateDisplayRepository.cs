using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUpdateDisplayRepository
    {
        void AddPictureToDisplay(DataRow row, DataColumn column, byte[] picture, int i, int columnCount);
    }
}
