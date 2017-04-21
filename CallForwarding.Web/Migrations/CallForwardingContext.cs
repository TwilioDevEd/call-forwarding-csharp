using CallForwarding.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CallForwarding.Web.Models
{
    public class CallForwardingContext : DbContext
    {
        public CallForwardingContext()
            : base("CallForwardingConnection") { }

        public DbSet<Zipcode> Zipcodes { get; set; }
        public DbSet<Senator> Senators { get; set; }
        public DbSet<State> States { get; set; }

    }
}