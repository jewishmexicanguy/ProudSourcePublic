using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ProudSourcePrime.Models
{
    public class UserIndexModel
    {
        public ServiceReference1.UserIndexComposite userIndexData;
        /// <summary>
        /// Constructor that accepts a user id string and gets the user account in question.
        /// </summary>
        /// <param name="userId"></param>
        public UserIndexModel(string userId)
        {
            userIndexData = new ServiceReference1.Service1Client().get_UserIndexData(userId);
        }
    }
}