using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProudSourceAccountingLibrary
{ 
    public abstract class PSAccountingBase
    {
        protected System.Data.SqlClient.SqlConnection conn;

        public PSAccountingBase()
        {
            conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceStaging"].ConnectionString);
        }
    }
}
