using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Adding
{
    public class ChangeTime
    {
        private readonly IChangeTimeRepository _changeTimeRepository;
        public ChangeTime(IChangeTimeRepository changeTimeRepository)
        {
            _changeTimeRepository = changeTimeRepository;
        }
        public void SetTime(string time, string item, string operation)
        {
            _changeTimeRepository.SetTime(time, item, operation);
        }
        public void SetSetupTime(string setupTime, string item, string operation)
        {
            _changeTimeRepository.SetSetupTime(setupTime, item, operation);
        }
    }
}
