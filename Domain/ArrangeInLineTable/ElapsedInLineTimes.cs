using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ArrangeInLineTable
{
    public class ElapsedInLineTimes
    {
        private readonly IElapsedInLineTimesRepository _elapsedInLineTimesRepository;
        public ElapsedInLineTimes(IElapsedInLineTimesRepository elapsedInLineTimesRepository)
        {
            _elapsedInLineTimesRepository = elapsedInLineTimesRepository;
        }
        public void DeleteEndTimes()
        {
            _elapsedInLineTimesRepository.DeleteEndTimes();
        }
        public void DeleteStartTimes()
        {
            _elapsedInLineTimesRepository.DeleteStarTimes();
        }
    }
}
