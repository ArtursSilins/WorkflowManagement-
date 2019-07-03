using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IForm2ProgressPictureRepository
    {   
        void UpdatePicture(byte[] picture, DataRow row, string itemId);
        void UpdatePicture(byte[] picture, DataRow row);
    }
}
