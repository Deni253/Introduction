﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Model
{
    public class Login
    {  
            public string Username { get; set; }
            public string Password { get; set; }
            public TokenRequest? token { get; set; }
        
    }
}
