using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.ArrangeInLineTable
{
    public class SortInLineTableNr
    {
        private readonly ISortedInLineTableASCRepository _sortedInLineTableASCRepository;
        private readonly INumerationInLineRepository _numerationInLineRepository;
        public SortInLineTableNr(ISortedInLineTableASCRepository sortedInLineTableASCRepository, INumerationInLineRepository numerationInLineRepository)
        {
            _sortedInLineTableASCRepository = sortedInLineTableASCRepository;
            _numerationInLineRepository = numerationInLineRepository;
        }
        public void ArrangeAllInLineNr()
        {
            int counter = 0;
            int counter2 = 0;
            int a = 0;
            int b = 0;


            foreach (DataRow row in _sortedInLineTableASCRepository.Get().Rows)
            {
                a = int.Parse(row["OperationId"].ToString());

                if (a == b)
                {
                    counter2++;
                    _numerationInLineRepository.AddNewNr(row, counter2);
                }
                else
                {
                    counter2 = 1;
                    counter = 1;
                    _numerationInLineRepository.AddNewNr(row, counter);
                }

                b = a;

            }

        }
    }
}
