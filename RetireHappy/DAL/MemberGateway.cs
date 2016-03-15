using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RetireHappy.Models;

namespace RetireHappy.DAL
{
    public class MemberGateway : CommonGateway<Member>
    {
        public Member SearchByUsername(string userName)
        {
            Member member = new Member();
            string query = "SELECT * FROM Member WHERE userName = '" + userName + "'";
            try {
                member = db.Members.SqlQuery(query).Single();
            }catch(Exception e)
            {
                Console.Write(e);
            }
            return member;
        }

        public Member verifyCredential(string userName, string password)
        {
            Member member = new Member();
            string query = "SELECT * FROM Member WHERE userName = '" + userName + "' AND password = '" + password + "'";
            try
            {
                member = db.Members.SqlQuery(query).Single();
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            return member;
        }

        
    }
}