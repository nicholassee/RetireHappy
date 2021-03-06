﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace RetireHappy.DAL
{
    public class ReportMapper : IReportMapper
    {

        
        private static int currentYear = DateTime.Now.Year;
        private static int previousYear = currentYear - 1;
        private static int nextYear = currentYear + 1;
        RetireHappyContext db = new RetireHappyContext();
        List<string> sql = new List<string>(
            new string[] {
                "SELECT COUNT(*) FROM UserProfile where Gender='Male' AND timestamp > '" + previousYear + "' AND timestamp < '" + nextYear + "'",
                "SELECT COUNT(*) FROM UserProfile where Gender='Female' AND timestamp > '" + previousYear + "' AND timestamp < '" + nextYear + "'",
                "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE monIncome < 1000",
                "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE monIncome BETWEEN 1001 and 2000",
                "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE monIncome BETWEEN 2001 and 3000",
                "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE monIncome BETWEEN 3001 and 4000",
                "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE monIncome BETWEEN 4001 and 5000",
                "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE monIncome BETWEEN 5001 and 6000",
                "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE monIncome > 6000",
                "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE age BETWEEN 25 AND 34",
                "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE age BETWEEN 25 AND 34",
                "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE age BETWEEN 35 AND 44",
                "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE age BETWEEN 45 AND 54",
                "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE age BETWEEN 55 AND 64"
            });
        
        //public Report retrieveInfo()
        public String retrieveInfo(int ind)
        {
            if (ind == -1)
            {
                return currentYear.ToString();
            }
            else if (ind >= 0 && ind <= 1)
            {
                try {
                    int res = db.Database.SqlQuery<int>(sql.ElementAt(ind)).Single();
                    return res.ToString();
                }
                catch (Exception e)
                {
                    Console.Write(e);
                    return "0.0";
                }  
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