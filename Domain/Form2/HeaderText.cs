using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.Form2
{
    public class HeaderText
    {
        private readonly IForm2TextRepository _form2TextRepository;
        public HeaderText(IForm2TextRepository form2TextRepository)
        {
            _form2TextRepository = form2TextRepository;
        }
        public string Get(string itemId)
        {
            string text = "";
            foreach (DataRow row in _form2TextRepository.Get(itemId).Rows)
            {
                text = row["Name"].ToString();
            }

            return text;
        }
    }
}
