﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OZK.Infrastructure.Paging
{
    public interface IPaging
    {
        int CurrentPage { get; set; }

        int? TotalOfRecords { get; set; }

        int PageCount { get; set; }
    }
}
