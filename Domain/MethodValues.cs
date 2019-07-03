using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class MethodValues
    {
        public int key { get; set; }
        public DateTime dateTime { get; set; }
        public int selectedNr { get; set; }

        public MethodValues(int Key, DateTime DateTime, int SelectedNr)
        {
            key = Key;
            dateTime = DateTime;
            selectedNr = SelectedNr;
        }
    }
}
