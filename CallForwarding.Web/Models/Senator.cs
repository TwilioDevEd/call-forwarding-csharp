using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallForwarding.Web.Models
{
    public class Senator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public virtual State State { get; set; }

    }
}