using RetireHappy.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace RetireHappy.Models
{
    public class Report
    {
        private IObjectContextAdapter ctx;

        public int cRID { get; set; }
        public int year { get; set; }
        public DateTime latestModDate { get; set; }
        public int male { get; set; }
        public int female { get; set; }
        public double inc_below_1000 { get; set; }
        public double inc_1001_2000 { get; set; }
        public double inc_2001_3000 { get; set; }
        public double inc_3001_4000 { get; set; }
        public double inc_4001_5000 { get; set; }
        public double inc_5001_6000 { get; set; }
        public double inc_above_6000 { get; set; }
        public double ageRange_below_25 { get; set; }
        public double ageRange_25_34 { get; set; }
        public double ageRange_35_44 { get; set; }
        public double ageRange_45_54 { get; set; }
        public double ageRange_55_64 { get; set; }

        public Report calculate(Object T, Object U)
        {
            int currentYear = DateTime.Now.Year;
            //DateTime currentYear = DateTime.Now;
            int previousYear = currentYear - 1;
            int nextYear = currentYear + 1;
            int maleCount = 0;
            int femaleCount = 0;
            double inc_below_1000Avg = 0;
            double inc_1001_2000Avg = 0;
            double inc_2001_3000Avg = 0;
            double inc_3001_4000Avg = 0;
            double inc_4001_5000Avg = 0;
            double inc_5001_6000Avg = 0;
            double inc_above_6000Avg = 0;
            double ageRange_below_25Avg = 0;
            double ageRange_25_34Avg = 0;
            double ageRange_35_44Avg = 0;
            double ageRange_45_54Avg = 0;
            double ageRange_55_64Avg = 0;


            Report report = new Report();

            //Assuming calculation logic is implemented here

            using (var ctx = new RetireHappyContext())
            {
                //var maleCountQuery = ctx.User.SqlQuery("SELECT COUNT(*) FROM Users where Gender='Male'").FirstOrDefault<User>();

                var sql = "SELECT COUNT(*) FROM UserProfile where Gender='Male' AND timestamp > '" + previousYear + "' AND timestamp < '" + nextYear + "'";
                var total = ctx.Database.SqlQuery<int>(sql).Single();

                maleCount = total;

                sql = "SELECT COUNT(*) FROM UserProfile where Gender='Female' AND timestamp > '" + previousYear + "' AND timestamp < '" + nextYear + "'";
                total = ctx.Database.SqlQuery<int>(sql).Single();

                femaleCount = total;

                sql = "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE calcRetSavings < 1000";
                var average = ctx.Database.SqlQuery<double?>(sql).FirstOrDefault();

                inc_below_1000Avg = Convert.ToDouble(average);

                sql = "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE calcRetSavings BETWEEN 1001 and 2000";
                average = ctx.Database.SqlQuery<double?>(sql).FirstOrDefault();

                inc_1001_2000Avg = Convert.ToDouble(average);

                sql = "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE calcRetSavings BETWEEN 2001 and 3000";
                average = ctx.Database.SqlQuery<double?>(sql).FirstOrDefault();

                inc_2001_3000Avg = Convert.ToDouble(average);

                sql = "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE calcRetSavings BETWEEN 3001 and 4000";
                average = ctx.Database.SqlQuery<double?>(sql).FirstOrDefault();

                inc_3001_4000Avg = Convert.ToDouble(average);

                sql = "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE calcRetSavings BETWEEN 4001 and 5000";
                average = ctx.Database.SqlQuery<double?>(sql).FirstOrDefault();

                inc_4001_5000Avg = Convert.ToDouble(average);

                sql = "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE calcRetSavings BETWEEN 5001 and 6000";
                average = ctx.Database.SqlQuery<double?>(sql).FirstOrDefault();

                inc_5001_6000Avg = Convert.ToDouble(average);

                sql = "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE calcRetSavings > 6000";
                average = ctx.Database.SqlQuery<double?>(sql).FirstOrDefault();

                inc_above_6000Avg = Convert.ToDouble(average);

                sql = "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE age < 25";
                average = ctx.Database.SqlQuery<double?>(sql).FirstOrDefault();

                ageRange_below_25Avg = Convert.ToDouble(average);


                sql = "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE age BETWEEN 25 AND 34";
                average = ctx.Database.SqlQuery<double>(sql).FirstOrDefault();

                ageRange_25_34Avg = Convert.ToDouble(average);

                sql = "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE age BETWEEN 25 AND 34";
                average = ctx.Database.SqlQuery<double?>(sql).FirstOrDefault();

                ageRange_25_34Avg = Convert.ToDouble(average);

                sql = "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE age BETWEEN 35 AND 44";
                average = ctx.Database.SqlQuery<double?>(sql).FirstOrDefault();

                ageRange_35_44Avg = Convert.ToDouble(average);

                sql = "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE age BETWEEN 45 AND 54";
                average = ctx.Database.SqlQuery<double?>(sql).FirstOrDefault();

                ageRange_45_54Avg = Convert.ToDouble(average);

                sql = "SELECT AVG(calcRetSavings) FROM UserProfile LEFT JOIN SavingsInfo ON UserProfile.Id = SavingsInfo.Id WHERE age BETWEEN 55 AND 64";
                average = ctx.Database.SqlQuery<double?>(sql).FirstOrDefault();

                ageRange_55_64Avg = Convert.ToDouble(average);

            }

            report.year = currentYear;
            report.male = maleCount;
            report.female = femaleCount;
            report.inc_below_1000 = Math.Round(inc_below_1000Avg, 2);
            report.inc_1001_2000 = Math.Round(inc_1001_2000Avg, 2);
            report.inc_2001_3000 = Math.Round(inc_2001_3000Avg, 2);
            report.inc_3001_4000 = Math.Round(inc_3001_4000Avg, 2);
            report.inc_4001_5000 = Math.Round(inc_4001_5000Avg, 2);
            report.inc_5001_6000 = Math.Round(inc_5001_6000Avg, 2);
            report.inc_above_6000 = Math.Round(inc_above_6000Avg, 2);
            report.ageRange_below_25 = Math.Round(ageRange_below_25Avg, 2);
            report.ageRange_25_34 = Math.Round(ageRange_25_34Avg, 2);
            report.ageRange_35_44 = Math.Round(ageRange_35_44Avg, 2);
            report.ageRange_45_54 = Math.Round(ageRange_45_54Avg, 2);
            report.ageRange_55_64 = Math.Round(ageRange_55_64Avg, 2);

            return report;
        }
    }
}