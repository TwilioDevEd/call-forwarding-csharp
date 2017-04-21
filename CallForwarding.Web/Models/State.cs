using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallForwarding.Web.Models
{
    public class State
    {
        public int Id { get; set; }
        public string name { get; set; }
        public virtual ICollection<Senator> Senators { get; set; }

    }
}