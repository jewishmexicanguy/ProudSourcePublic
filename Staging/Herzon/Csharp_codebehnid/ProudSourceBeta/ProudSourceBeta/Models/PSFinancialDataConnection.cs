using System.Data.SqlClient;
using System.Data;
using System;

namespace ProudSourceBeta.Models
{
    public class PSFinancialDataConnection
    {
        protected SqlConnection conn;

        public PSFinancialDataConnection()
        {
            conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceAccounting"].ConnectionString);
        }
    }
}