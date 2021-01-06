using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SucorInvest.Connect;


namespace RFSUtility
{
    public class FoConnection
    {
        public static string Authentication()
        {
            try
            {
                var token = SucorInvest.Connect.BackOffice.SignIn("BackOffice", "E95$n%A9@Zi*omz@E&V+!M(2");
                if (token == null) throw new HttpException("Cannot Connect");

                return token.AuthToken;
            }
            catch (Exception e)
            {
                throw new HttpException(e.Message);
            }

        }
    }
}