using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using System.ComponentModel.DataAnnotations;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Service
{
    public class GreetingRL:IGreetingRL
    {
        private readonly GreetingDbContext _context;
        public GreetingRL(GreetingDbContext context)
        {

            _context = context;

        }

        public GreetingEntity AddGreetings(GreetingEntity greeting)
        {
            _context.Greetings.Add(greeting);
            _context.SaveChanges();
            return greeting;
        }

        //UC5

        public GreetingEntity? GetGreetingById(int id)
        {
            return _context.Greetings.FirstOrDefault(x => x.Id == id);
        }
        public string getGreetMessage(UsernameRequestModel userModel)
        {
            var name = $"{userModel.FirstName} {userModel.LastName}".Trim();
            return string.IsNullOrEmpty(name) ? "Hello, World!" : $"Hello, {name}!";
        }

        public List<GreetingEntity> GetAllGreetings()
        {
            return _context.Greetings.ToList();
        }

    }
}
