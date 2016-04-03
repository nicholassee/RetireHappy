using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetireHappy.Models
{
    interface IReport
    {
        int cRID { get; set; }
        int year { get; set; }
        DateTime latestModDate { get; set; }

        int male { get; set; }
        int female { get; set; }
        double inc_below_1000 { get; set; }
        double inc_1001_2000 { get; set; }
        double inc_2001_3000 { get; set; }
        double inc_3001_4000 { get; set; }
        double inc_4001_5000 { get; set; }
        double inc_5001_6000 { get; set; }
        double inc_above_6000 { get; set; }
        double ageRange_below_25 { get; set; }
        double ageRange_25_34 { get; set; }
        double ageRange_35_44 { get; set; }
        double ageRange_45_54 { get; set; }
        double ageRange_55_64 { get; set; }

        void updateData();
    }
}
