using Contacts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Data
{
    public interface IUowData
    {
        IRepository<ContactInformation> ContactInformations { get; }

        int SaveChanges();
    }
}
