using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ProudSourcePrime.Models
{
    public class FinancialAccountModel
    {
        public ServiceReference1.FinancialAccountComposite account;

        public FinancialAccountModel(string user_id, int account_id)
        {
            account = new ServiceReference1.Service1Client().get_FinancialAccountData(user_id, account_id);
        }
    }
}