using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.ArrangeInLineTable
{
    public class SortedInLineTeableASC
    {
        private readonly ISortedInLineTableASCRepository _sortedInLineTeableASCRepository; 
        public SortedInLineTeableASC(ISortedInLineTableASCRepository sortedInLineTeableASCRepository)
        {
            _sortedInLineTeableASCRepository = sortedInLineTeableASCRepository;
        }
        public DataTable Get()
        {
            return _sortedInLineTeableASCRepository.Get();
        }
    }
}
