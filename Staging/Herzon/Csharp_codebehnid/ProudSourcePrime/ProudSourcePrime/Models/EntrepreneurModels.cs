using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ProudSourcePrime.Models
{
    public class EntrepreneurIndexModel
    {
        public ServiceReference1.EntrepreneurIndexComposite entrepreneurIndexData;
        /// <summary>
        /// Public constructor that takes in the user id and the entrepreneur id to load the appropriate data to sohw the client front end. 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="EntrepreneurId"></param>
        public EntrepreneurIndexModel(string UserId, int EntrepreneurId)
        {
            entrepreneurIndexData = new ServiceReference1.Service1Client().get_EntrepreneurIndexData(UserId, EntrepreneurId);
        }
    }

    public class EntrepreneurDetailsModel
    {
        public ServiceReference1.EntrepreneurDetailsComposite entrepreneur;

        public EntrepreneurDetailsModel(int entrepreneur_id)
        {
            entrepreneur = new ServiceReference1.Service1Client().get_EntrepreneurDetails_Data(entrepreneur_id);
        }
    }
}