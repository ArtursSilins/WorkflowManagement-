using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PauseDropLists
{
    public static class PauseTimeSet
    {
        public static string[] Hour()
        {
            string[] comboBoxHour = new string[24];
            for (int i = 0; i < comboBoxHour.Length; i++)
            {
                if (i == 0)
                {
                    comboBoxHour[i] = "00";
                }
                else
                {
                    comboBoxHour[i] = i.ToString();
                }
            }

            return comboBoxHour;
        }
        public static string[] Minutes()
        {
            string[] comboBoxMinutes = new string[60];
            for (int i = 0; i < comboBoxMinutes.Length; i++)
            {
                if (i == 0)
                {
                    comboBoxMinutes[i] = "00";
                }
                else
                {
                    comboBoxMinutes[i] = i.ToString();
                }
            }
            return comboBoxMinutes;
        }
    }
}
