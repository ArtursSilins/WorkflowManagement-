using Domain.Interfaces;
using Domain.ProgressPicture;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IsOperationBusy
{
    public class SetItemsOperationsToNull
    {
        private Picture Picture { get; } = new Picture();
        public void InserNull(IItemsOperationsNullRepository iSetItemsOperationsToNullRepository, DataRow row)
        {
            iSetItemsOperationsToNullRepository.InsertNull(Picture.Create(0,0), row);
        }
    }
}
