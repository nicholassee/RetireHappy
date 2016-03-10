using RetireHappy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetireHappy.DAL
{
    public class AvgExpenditureGateway : CommonGateway<AvgExpenditure>
    {
        public void deleteAll()
        {
            string query = "DELETE FROM AvgExpenditure";
            db.Database.ExecuteSqlCommand(query);
        }
    }
}