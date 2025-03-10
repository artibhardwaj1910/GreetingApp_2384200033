﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Service;

namespace RepositoryLayer.Interface
{
    public interface IGreetingRL
    {
        public string getGreetMessage(UsernameRequestModel userModel);
        GreetingEntity AddGreetings(GreetingEntity greeting);

        GreetingEntity GetGreetingById(int id);

        List<GreetingEntity> GetAllGreetings();

        GreetingEntity UpdateGreeting(int id, string newMssge);

        bool DeleteGreeting(int id);
    }
}
