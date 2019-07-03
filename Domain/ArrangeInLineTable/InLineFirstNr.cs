using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.ArrangeInLineTable
{
    public class InLineFirstNr
    {
        private readonly IInLineFirstNrRepository _inLineFirstNrRepository;
        private readonly IOperationTableRepository _operationTableRepository;
        private readonly IElapsedInLineTimesRepository _elapsedInLineTimesRepository;
        private readonly INumerationInLineRepository _numerationInLineRepository;
        public InLineFirstNr(IInLineFirstNrRepository inLineFirstNrRepository,
            IOperationTableRepository operationTableRepository,
            IElapsedInLineTimesRepository elapsedInLineTimesRepository,
            INumerationInLineRepository numerationInLineRepository)
        {
            _inLineFirstNrRepository = inLineFirstNrRepository;
            _operationTableRepository = operationTableRepository;
            _elapsedInLineTimesRepository = elapsedInLineTimesRepository;
            _numerationInLineRepository = numerationInLineRepository;
        }
        public void ArrangeNr()
        {
            foreach (DataRow row in _inLineFirstNrRepository.GetTable().Rows)
            {

                DateTime timeNow = DateTime.Now;
                if (Convert.ToDateTime(row["StartTime"].ToString()) <= timeNow)
                {
                    _operationTableRepository.AddToOperations(row);

                    _elapsedInLineTimesRepository.DeleteRow(row);
 
                    _numerationInLineRepository.ArrangeOneItemOperations(row);
                }

            }
        }
    }
}
