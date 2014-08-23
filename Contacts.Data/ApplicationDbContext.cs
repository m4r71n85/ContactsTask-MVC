using Contacts.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("ContactInfoDb")
        {
        }

        public IDbSet<ContactInformation> ContactInformations { get; set; }

    }
}
