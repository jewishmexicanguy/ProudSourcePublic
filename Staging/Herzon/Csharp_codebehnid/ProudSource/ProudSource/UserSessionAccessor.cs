using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProudSource
{
    public class UserSessionAccessor : IDisposable
    {
        public string UserName;
        public int UserID;
        public bool UserAuthenticated;

        public UserSessionAccessor(object[] token)
        {
            try
            {
                UserName = (string)(token)[1];
                UserID = (int)token[2];
                UserAuthenticated = true;
            }
            catch (Exception e)
            {
                UserAuthenticated = false;
            }
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}