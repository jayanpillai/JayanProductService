﻿using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces
{
    public interface IUserService
    {
        void add(UserDataModel user);

        UserDataModel get(string username);
    }
}
