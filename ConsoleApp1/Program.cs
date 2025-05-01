using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DataLayer;
using Accounting.DataLayer.Context;
using Accounting.DataLayer.Repositories;
using Accounting.DataLayer.Services;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UnitofWork db = new UnitofWork();
            var list = db.CustomerRepository.GetAllCustomers();
            db.Dispose();


            //Accounting_DBEntities1 db = new Accounting_DBEntities1();
            //ICostomerRepository customer = new CustomerRepository(db);
            //Customers AddCustomer = new Customers()
            //{
            //    FullName="علی محمدی",
            //    Mobile="09366762552",
            //    CustomerImage="No Image"
            //};
            //customer.InsertCustomer(AddCustomer);
            ////customer.Save();
            //var list = customer.GetAllCustomers();

            

        }
    }
}
