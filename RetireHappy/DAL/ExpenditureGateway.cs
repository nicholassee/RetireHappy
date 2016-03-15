using RetireHappy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetireHappy.DAL
{
    public class ExpenditureGateway : CommonGateway<ExpenditureList>
    {
        public ExpenditureList checkExistingExpList(int mId)
        {
            ExpenditureList expenditureList = new ExpenditureList();
            string query = "SELECT * FROM ExpenditureList WHERE mId = " + mId + "";
            try
            {
                expenditureList = db.ExpenditureLists.SqlQuery(query).Single();
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            return expenditureList;
        }
        public void updateExpenditureList(ExpenditureList expenditureList)
        {
            string query = "Update ExpenditureList SET item1 = {0}, item2 = {1}, item3 = {2}, item4 = {3}, item5 = {4}, item6 = {5}, item7 = {6}, item8 = {7}, item9 = {8}, item10 = {9} WHERE mId = {10}";
            db.Database.ExecuteSqlCommand(query, expenditureList.item1, expenditureList.item2, expenditureList.item3, expenditureList.item4, expenditureList.item5,
                expenditureList.item6, expenditureList.item7, expenditureList.item8, expenditureList.item9, expenditureList.item10, expenditureList.mId);
            Save();
        }
    }
}