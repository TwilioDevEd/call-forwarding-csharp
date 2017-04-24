using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CallForwarding.Web.Models.Repository
{

    public class SenatorsRepository : IRepository<Senator>
    {
        private readonly CallForwardingContext _context = new CallForwardingContext();

        public Senator Find(int id)
        {
            return _context.Senators.Find(id);
        }

        public Senator FirstOrDefault(Func<Senator, bool> predicate)
        {
            return _context.Senators.FirstOrDefault(predicate);
        }

       
    }

   
}