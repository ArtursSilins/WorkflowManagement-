using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.View
{
    public class All
    {
        private readonly IAll _all;
        public All(IAll All)
        {
            _all = All;
        }
        public DataTable Items()
        {
            return _all.Items();
        }
        public DataTable Operations()
        {
            return _all.Operations();
        }
        public DataTable PauseView()
        {
            return _all.pauseView();
        }
        public DataTable DuplicateName()
        {
            return _all.DuplicateName();
        }
    }
}
