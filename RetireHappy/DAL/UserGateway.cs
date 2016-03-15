using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RetireHappy.Models;

namespace RetireHappy.DAL
{
    public class UserGateway : CommonGateway<UserProfile>
    {
        public UserProfile checkIfExistUserProfile(int mId)
        {
            UserProfile userprofile = new UserProfile();
            string query = "SELECT * FROM UserProfile WHERE mId = " + mId + "";
            try {
                userprofile = db.UserProfiles.SqlQuery(query).Single();
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            return userprofile;
        }
        public void updateUserProfile(UserProfile userProfile)
        {

            string query = "UPDATE UserProfile SET mId = {0} , age = {1}, gender = {2}, expRetAge = {3}, retDuration = {4}, " +
                "monIncome = {5}, avgMonExpenditure = {6}, curSavingAmt = {7}, desiredMonRetInc = {8}, " +
                "timestamp = {9}, inflationRate = {10}, ifUseAvgExp = {11} where Id = {12}";
            db.Database.ExecuteSqlCommand(query, userProfile.mId, userProfile.age, userProfile.gender, userProfile.expRetAge, userProfile.retDuration , userProfile.monIncome,
                userProfile.avgMonExpenditure, userProfile.curSavingAmt, userProfile.desiredMonRetInc, userProfile.timestamp, userProfile.inflationRate, userProfile.ifUseAvgExp,
                userProfile.Id);
            Save();
        }
    }
}