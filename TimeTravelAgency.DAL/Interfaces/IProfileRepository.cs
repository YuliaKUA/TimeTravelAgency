﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.Domain.Entity;

namespace TimeTravelAgency.DAL.Interfaces
{
    public interface IProfileRepository : IBaseRepository<Uprofile>
    {
        Task<Uprofile> GetById(int id);
    }
}
