using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallForwarding.Web.Models.Repository
{
    public interface IRepository<T>
    {
        T FirstOrDefault(Func<T, bool> predicate);
        T Find(int id);
    }
}