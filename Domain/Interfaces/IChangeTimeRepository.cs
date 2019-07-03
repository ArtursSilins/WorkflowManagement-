using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IChangeTimeRepository
    {
        void SetTime(string time, string item, string operation);
        void SetSetupTime(string setupTime, string item, string operation);
    }
}
