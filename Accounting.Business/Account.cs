using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DataLayer.Context;
using Accounting.ViewModel.Accounting;

namespace Accounting.Business
{
    public class Account
    {
        public static ReportViewModel ReportMain()
        {
            ReportViewModel rp = new ReportViewModel();
            using (UnitofWork db = new UnitofWork())
            {
                DateTime Startdate = new DateTime (DateTime.Now.Year,DateTime.Now.Month,01);
                DateTime Enddate = new DateTime (DateTime.Now.Year,DateTime.Now.Month,30);
                var income = db.AccountingRepository.Get(a=> a.TypeID==1 && a.DateTime>=Startdate && a.DateTime<=Enddate).Select(a=>a.Amount).ToList();
                var outcome = db.AccountingRepository.Get(a=> a.TypeID==2 && a.DateTime>=Startdate && a.DateTime<=Enddate).Select(a=>a.Amount).ToList();
                rp.Income = income.Sum();
                rp.Outcome = outcome.Sum();
                rp.Balance = (income.Sum() - outcome.Sum());
            }
            return rp;
        }
    }
}
