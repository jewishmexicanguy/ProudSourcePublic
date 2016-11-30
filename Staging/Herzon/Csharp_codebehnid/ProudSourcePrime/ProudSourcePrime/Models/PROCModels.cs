using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ProudSourcePrime.Models
{
    public class PROCDetailsModel
    {
        public ServiceReference1.PROCComposite proc;

        public PROCDetailsModel(int proc_id, int entrepreneur_id = 0, int investor_id = 0)
        {
            proc = new ServiceReference1.Service1Client().get_PROC(proc_id, entrepreneur_id, investor_id);
        }
    }
}