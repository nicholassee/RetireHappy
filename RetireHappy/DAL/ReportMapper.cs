using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RetireHappy.Models;


namespace RetireHappy.DAL
{
    public class ReportMapper : ICommonGateway<User>, ICommonGateway<SavingsInfo>
    {
        public Object ComputeAverage()
        {
            // for prototype purposes only, user profile and savings info are not yet access for prototype report implementation
            Report report = new Report();

            // Assuming all this data are retrieved after calling userProfile.ToList and savingsInfo.ToList
            User user = new User();
            SavingsInfo savingsInfo = new SavingsInfo();
            // Assuming all calculation are done after after calling calculation() in Report object.

            //report.year = 2016;
            //report.male = 1350;
            //report.female = 1230;
            //report.inc_below_1000 = 870;
            //report.inc_1001_2000 = 1300;
            //report.inc_2001_3000 = 2400;
            //report.inc_3001_4000 = 3056;
            //report.inc_4001_5000 = 4678;
            //report.inc_5001_6000 = 5836;
            //report.inc_above_6000 = 6903;
            //report.ageRange_below_25 = 1600;
            //report.ageRange_25_34 = 1833;
            //report.ageRange_35_44 = 1735;
            //report.ageRange_45_54 = 1246;
            //report.ageRange_55_64 = 1405;
            report = report.calculate(user,savingsInfo);

            return report;
        }

        public User Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public void Insert(SavingsInfo obj)
        {
            throw new NotImplementedException();
        }

        public void Insert(User obj)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> SelectAll()
        {
            throw new NotImplementedException();
        }

        public User SelectById(int? id)
        {
            throw new NotImplementedException();
        }

        public void Update(SavingsInfo obj)
        {
            throw new NotImplementedException();
        }

        public void Update(User obj)
        {
            throw new NotImplementedException();
        }

        SavingsInfo ICommonGateway<SavingsInfo>.Delete(int? id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<SavingsInfo> ICommonGateway<SavingsInfo>.SelectAll()
        {
            throw new NotImplementedException();
        }

        SavingsInfo ICommonGateway<SavingsInfo>.SelectById(int? id)
        {
            throw new NotImplementedException();
        }
    }
}