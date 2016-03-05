using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetireHappy.Models
{
    public class Report
    {
        public int cRID { get; set; }
        public int year { get; set; }
        public DateTime latestModDate { get; set; }
        public float male { get; set; }
        public float female { get; set; }
        public float inc_below_1000 { get; set; }
        public float inc_1001_2000 { get; set; }
        public float inc_2001_3000 { get; set; }
        public float inc_3001_4000 { get; set; }
        public float inc_4001_5000 { get; set; }
        public float inc_5001_6000 { get; set; }
        public float inc_above_6000 { get; set; }
        public float ageRange_below_25 { get; set; }
        public float ageRange_25_34 { get; set; }
        public float ageRange_35_44 { get; set; }
        public float ageRange_45_54 { get; set; }
        public float ageRange_55_64 { get; set; }
        public Report calculate(Object T, Object U)
        {
            Report report = new Report();

            //Assuming calculation logic is implemented here


            report.year = 2016;
            report.male = 1350;
            report.female = 1230;
            report.inc_below_1000 = 870;
            report.inc_1001_2000 = 1300;
            report.inc_2001_3000 = 2400;
            report.inc_3001_4000 = 3056;
            report.inc_4001_5000 = 4678;
            report.inc_5001_6000 = 5836;
            report.inc_above_6000 = 6903;
            report.ageRange_below_25 = 1600;
            report.ageRange_25_34 = 1833;
            report.ageRange_35_44 = 1735;
            report.ageRange_45_54 = 1246;
            report.ageRange_55_64 = 1405;

            return report;
        }
    }
}