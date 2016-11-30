using System;
using Microsoft.AspNet.Identity;

namespace ProudSourcePrime.Models
{
    public class ApplicationUser : IUser
    {
        public DateTime Date_Creation { get; set; }

        public DateTime Date_BirthDay { get; set; }

        private string _UserName { get; set; }

        public ApplicationUser()
        {
            Date_Creation = DateTime.Now;
        }

        public string Id
        {
            get
            {
                return new Guid().ToString();
            }
        }

        public string UserName
        {
            get
            {
                return _UserName;
            }

            set
            {
                _UserName = value;
            }
        }
    }
}