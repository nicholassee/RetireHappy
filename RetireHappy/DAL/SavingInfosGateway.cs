using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RetireHappy.Models;

namespace RetireHappy.DAL
{
    public class SavingInfosGateway : CommonGateway<SavingsInfo>
    {
        public void updateSavingInfos(SavingsInfo savingsInfo)
        {
            string query = "UPDATE SavingsInfo SET calcRetSavings = {0}, riskLevel = {1}, expPercent = {2}, diffPercent = {3} WHERE Id = {4}";
            db.Database.ExecuteSqlCommand(query, savingsInfo.calcRetSavings, savingsInfo.riskLevel, savingsInfo.expPercent, savingsInfo.diffPercent, savingsInfo.Id);
            Save();
        }
    }
}