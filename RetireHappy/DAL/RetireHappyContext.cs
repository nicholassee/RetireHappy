using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RetireHappy.Models;
using System.Data.Entity;

namespace RetireHappy.DAL
{
    public class RetireHappyContext : DbContext
    {
        public RetireHappyContext() : base("RetireHappyDB")
        {
        }
        //public DbSet<AvgExpenditure> AvgExpenditures{get;set;}
        //public DbSet<Expenditure> Expenditures { get; set; }
        //public DbSet<Report> Reports { get; set; }
        public DbSet<SavingsInfo> SavingsInfos { get; set; }
        public DbSet<User> User { get; set; }
        //public DbSet<Admin> Admins { get; set; }
    }
}