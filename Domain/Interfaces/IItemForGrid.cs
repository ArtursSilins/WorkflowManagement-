﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IItemForGridRepository
    {
        DataTable View(string item);
        DataTable ItemsWhenDisplayGridClicked(string item);
        DataTable DuplicateView(string item);
    }
}
