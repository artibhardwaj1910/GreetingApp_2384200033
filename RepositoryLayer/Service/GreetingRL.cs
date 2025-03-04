using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using System.ComponentModel.DataAnnotations;

namespace RepositoryLayer.Service
{
    public class GreetingRL:IGreetingRL
    {

        public string getGreetMessage(UserModel userModel)
        {
            var name = $"{userModel.FirstName} {userModel.LastName}".Trim();
            return string.IsNullOrEmpty(name) ? "Hello, World!" : $"Hello, {name}!";
        }
        
    }
}
