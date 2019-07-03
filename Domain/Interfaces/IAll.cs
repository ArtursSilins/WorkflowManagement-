using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAll
    {
        DataTable Items();
        DataTable Operations();
        DataTable pauseView();
        DataTable DuplicateName();
    }
}
