﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Services.PasswordHasher
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Check(string password, string hash);
    }
}
