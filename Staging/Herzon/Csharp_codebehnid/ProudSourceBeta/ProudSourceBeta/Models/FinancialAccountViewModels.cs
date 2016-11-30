using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using ProudSourceAccountingLibrary.BrainTree;

namespace ProudSourceBeta.Models
{
    public class FinancialAccountDetails : PSFinancialDataConnection
    {
        [Display(Name = "Financial Account Number")]
        public int Account_ID { get; private set; }

        [Display(Name = "Pending transactions balance")]
        public decimal Pending_Account_Balance_USD { get; private set; }

        [Display(Name = "Pending bitcoin transaction balance")]
        public decimal Pending_Account_Balance_BTC { get; private set; }

        [Display(Name = "Account balance")]
        public decimal Processed_Account_Balance_USD { get; private set; }

        [Display(Name = "Account bitcoin balance")]
        public decimal Processed_Account_Balance_BTC { get; private set; }

        public DataRowCollection Pending_Transactions { get; private set; }

        public DataRowCollection Processed_Transactions { get; private set; }

        public string Client_BrainTree_Token { get; private set; }

        public FinancialAccountDetails(int account_id) : base()
        {
            Account_ID = account_id;
            get_AccountData();
            Client_BrainTree_Token = new BrainTreeBase().get_ClientToken();
        }

        private void get_AccountData()
        {
            string query = "sp_get_FinancialAccountDetails";

            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Account_ID", Account_ID);
            DataSet set = new DataSet();
            try
            {
                conn.Open();
                adapter.Fill(set);
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
            // Tables Returned
            //      [0] : All the transactions for this account that are PENDING.
            //      [1] : All the transactions for this account that are not PENDING.

            Pending_Account_Balance_BTC = 0.0m;
            Pending_Account_Balance_USD = 0.0m;
            Processed_Account_Balance_BTC = 0.0m;
            Processed_Account_Balance_USD = 0.0m;

            Pending_Transactions = set.Tables[0].Rows;

            if(Pending_Transactions.Count > 0)
            {
                foreach(DataRow k in Pending_Transactions)
                {
                    if (k["Currency"].ToString() == "USD")
                    {
                        Pending_Account_Balance_USD += (decimal)k["Amount"];
                    }
                    else if (k["Currecny"].ToString() == "BTC")
                    {
                        Pending_Account_Balance_BTC += (decimal)k["Amount"];
                    }
                }
            }

            Processed_Transactions = set.Tables[1].Rows;

            if(Processed_Transactions.Count > 0)
            {
                foreach(DataRow k in Processed_Transactions)
                {
                    if (k["Transaction_State"].ToString() == "PROCESSED" && k["Currency"].ToString() == "USD")
                    {
                        Processed_Account_Balance_USD += (decimal)k["Amount"];
                    }
                    else if (k["Transaction_State"].ToString() == "PROCESSED" && k["Currency"].ToString() == "BTC")
                    {
                        Processed_Account_Balance_BTC += (decimal)k["Amount"];
                    }
                }
            }
        }
    }
}