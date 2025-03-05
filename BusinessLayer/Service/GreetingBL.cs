﻿using BusinessLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class GreetingBL : IGreetingBL
    {
        IGreetingRL _greetingRL;
        public GreetingBL(IGreetingRL greetingRL)
        {
            _greetingRL = greetingRL;
        }

        public string getGreetMessage(UsernameRequestModel userModel)
        {
            var result = _greetingRL.getGreetMessage(userModel);
            return result;
        }
        //UC4
        public GreetingEntity AddGreeting(GreetingEntity greeting)
        {
            return _greetingRL.AddGreetings(greeting);
        }
    }
}
