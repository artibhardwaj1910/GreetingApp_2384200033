using ModelLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IGreetingBL
    {
        string getGreetMessage(UsernameRequestModel userModel);
        GreetingEntity AddGreeting(GreetingEntity greeting);

        GreetingEntity GetGreetingById(int id);

        List<GreetingEntity> GetAllGreetings();

        GreetingEntity UpdateGreeting(int id, string newMssge);
    }
}
