using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RetireHappy.Models;
using System.Data.Entity.Infrastructure;

namespace RetireHappy.DAL
{
    public class ReportMapper
    {
        //private IObjectContextAdapter ctx;
        //do overloading, one to return int another, double
              

        private static int currentYear = DateTime.Now.Year;
        private static int previousYear = currentYear - 1;
        private static int nextYear = currentYear + 1;

        List<string> sql = new List<string>(
            new string[] {
                "SELECT COUNT(*) FROM UserProfile where Gender='Male' AND timestamp > '" + previousYear + "' AND timestamp < '" + nextYear + "'",
                "SELECT COUNT(*) FROM UserProfile where Gender='Female' AND timestamp > '" + previousYear + "' AND timestamp < '" + nextYear + "'",
                "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE calcRetSavings < 1000",
                "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE calcRetSavings BETWEEN 1001 and 2000",
                "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE calcRetSavings BETWEEN 2001 and 3000",
                "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE calcRetSavings BETWEEN 3001 and 4000",
                "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE calcRetSavings BETWEEN 4001 and 5000",
                "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE calcRetSavings BETWEEN 5001 and 6000",
                "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE calcRetSavings > 6000",
                "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE age BETWEEN 25 AND 34",
                "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE age BETWEEN 25 AND 34",
                "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE age BETWEEN 35 AND 44",
                "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE age BETWEEN 45 AND 54",
                "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE age BETWEEN 55 AND 64"
            });

        //public Report retrieveInfo()
        public String retrieveInfo(int ind)
        {
            RetireHappyContext db = new RetireHappyContext();
       

            using (db) {
                if (ind == -1)
                {
                    return currentYear.ToString();
                }
                else if (ind >= 0 && ind <= 1)
                {

                    int res = db.Database.SqlQuery<int>(sql.ElementAt(ind)).Single();
                    return res.ToString();

                }
                else
                {
                    var temp = db.Database.SqlQuery<double?>(sql.ElementAt(ind)).FirstOrDefault();
                    double res = 0.0;

                    if (temp != null)
                    {
                        res = Convert.ToDouble(temp);
                    }
                    
                    res= Math.Round(res, 2);
                    return res.ToString();
                }
            }

           
           
           
        }
        
    }
}