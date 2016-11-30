using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace ProudSourcePrime.Models
{
    public class CustomUserManager : UserManager<ApplicationUser>
    {
        public CustomUserManager() : base(new UserStore<ApplicationUser>())
        {
            PasswordHasher = new OldSystemPasswordHasher();
        }

        public override Task<ApplicationUser> FindAsync(string userName, string password)
        {
            Task<ApplicationUser> taskInvoke = Task<ApplicationUser>.Factory.StartNew(() =>
            {
                PasswordVerificationResult result = PasswordHasher.VerifyHashedPassword(userName, password);
                if(result == PasswordVerificationResult.SuccessRehashNeeded)
                {
                    ///
                    /// We can do a call to our data base here for profile data
                    ///
                    ApplicationUser applicationuser = new ApplicationUser();
                    applicationuser.UserName = "sans";
                    return applicationuser;
                }
                return null;
            });
            return taskInvoke;
        }

        public class OldSystemPasswordHasher : PasswordHasher
        {
            public override string HashPassword(string password)
            {
                return base.HashPassword(password);
            }

            public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
            {
                ///
                /// TODO: We need to implement calling a hash function here
                /// 
                if(true) /// replace this "true" with something that stores whether or not the provided password after being hashed is equivalent to has stored in our User record.
                {
                    return PasswordVerificationResult.SuccessRehashNeeded;
                }
                else
                {
                    return PasswordVerificationResult.Failed;
                }
            }
        }
    }
}