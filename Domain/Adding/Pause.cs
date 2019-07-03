using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Adding
{
    public class Pause
    {
        private readonly IPauseRepository _pauseRepository;
        public Pause(IPauseRepository pauseRepository)
        {
            _pauseRepository = pauseRepository;
        }
        public void AddPause(string pauseName, string startHour, string startMinutes, string endHour, string endMinutes)
        {
            DateTime pauseStart = new DateTime(2000, 1, 1, int.Parse(startHour), int.Parse(startMinutes), 00);
            DateTime pauseEnd = new DateTime(2000, 1, 1, int.Parse(endHour), int.Parse(endMinutes), 00);

            _pauseRepository.addPause(pauseName, pauseStart, pauseEnd);
        }
    }
}
