using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CallForwarding.Web.Models.Repository
{

    public class ZipcodesRepository : IRepository<Zipcode>
    {
        private readonly CallForwardingContext _context = new CallForwardingContext();

        public Zipcode Find(int id)
        {
            return _context.Zipcodes.Find(id);
        }

        public Zipcode FirstOrDefault(Func<Zipcode, bool> predicate)
        {
            return _context.Zipcodes.FirstOrDefault(predicate);
        }
    }

   
}