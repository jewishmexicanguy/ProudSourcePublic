using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ProudSourcePrime.Models
{
    public class InvestorIndexModel
    {
        public ServiceReference1.InvestorIndexComposite investorIndexData;
        /// <summary>
        /// Constructor that gets the index data for this investor profile.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="InvestorId"></param>
        public InvestorIndexModel(string UserId, int InvestorId)
        {
            investorIndexData = new ServiceReference1.Service1Client().get_InvestorIndexData(UserId, InvestorId);
        }
    }

    public class InvestorDetailsModel
    {
        public ServiceReference1.InvestorDetailsComposite investor;

        public InvestorDetailsModel(int investor_id)
        {
            investor = new ServiceReference1.Service1Client().get_InvestorDetails_Data(investor_id);
        }
    }
}