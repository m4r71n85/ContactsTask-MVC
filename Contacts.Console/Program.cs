using Contacts.Data;
using Contacts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Cons
{
    class Program
    {
        static void Main(string[] args)
        {
            UowData Data = new UowData();

            Console.WriteLine("Generate data...");
            GenerateAndSetData(Data);

            Console.WriteLine("Save 1000 contact informactions...");
            Data.SaveChanges();
            
            Console.WriteLine("Done!");
        }

        private static void GenerateAndSetData(UowData Data)
        {
            for (int i = 0; i < 1000; i++)
            {
                Data.ContactInformations.Add(new ContactInformation()
                {
                    FirstName = "FirstName" + i,
                    LastName = "LastName" + i,
                    Phone = "+359 " + i,
                    Sex = GetSex(i),
                    Status = GetStatus(i)
                });
            }
        }

        private static Status GetStatus(int i)
        {
            switch (i % 3)
            {
                case 0:
                    return Status.Active;
                case 1:
                    return Status.Delete;
                case 2:
                    return Status.Inactive;
                default:
                    return Status.Active;
            }
        }

        private static Sex GetSex(int i)
        {
            switch (i % 2)
            {
                case 0:
                    return Sex.Female;
                case 1:
                    return Sex.Male;
                default:
                    return Sex.Male;
            }
        }
    }
}
