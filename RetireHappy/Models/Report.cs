using RetireHappy.DAL;
using System;
using System.Collections.Generic;

namespace RetireHappy.Models
{
    public class Report
    {
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

        

        public void updateData()
        {

            ReportMapper reportMapper = new ReportMapper();
            year = Int32.Parse(reportMapper.retrieveInfo(-1));
            male = Int32.Parse(reportMapper.retrieveInfo(0));
            female = Int32.Parse(reportMapper.retrieveInfo(1));
            inc_below_1000 = Double.Parse(reportMapper.retrieveInfo(2));
            inc_1001_2000 = Double.Parse(reportMapper.retrieveInfo(3));
            inc_2001_3000 = Double.Parse(reportMapper.retrieveInfo(4));
            inc_3001_4000 = Double.Parse(reportMapper.retrieveInfo(5));
            inc_4001_5000 = Double.Parse(reportMapper.retrieveInfo(6));
            inc_5001_6000 = Double.Parse(reportMapper.retrieveInfo(7));
            inc_above_6000 = Double.Parse(reportMapper.retrieveInfo(8));
            ageRange_below_25 = Double.Parse(reportMapper.retrieveInfo(9));
            ageRange_25_34 = Double.Parse(reportMapper.retrieveInfo(10));
            ageRange_35_44 = Double.Parse(reportMapper.retrieveInfo(11));
            ageRange_45_54 = Double.Parse(reportMapper.retrieveInfo(12));
            ageRange_55_64 = Double.Parse(reportMapper.retrieveInfo(13));



        }
    }
    
}