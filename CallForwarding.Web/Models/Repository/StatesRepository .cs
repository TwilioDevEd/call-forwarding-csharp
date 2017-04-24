using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CallForwarding.Web.Models.Repository
{

    public class StatesRepository : IRepository<State>
    {
        private readonly CallForwardingContext _context = new CallForwardingContext();


        public State Find(int id)
        {
            return _context.States.Find(id);
        }

        public State FirstOrDefault(Func<State, bool> predicate)
        {
            return _context.States.FirstOrDefault(predicate);
        }

      
    }

   
}