using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using ProudSourceAccountingLibrary.BrainTree;

namespace ProudSourceBeta.Models
{
    public class FinancialAccountDetails : PSDataConnection
    {
        [Display(Name = "Financial Account Number")]
        public int Account_ID { get; private set; }

        [Display(Name = "Pending transactions balance")]
        public decimal Account_Balance { get; private set; }

        public DataRowCollection Transactions { get; private set; }

        public string Client_BrainTree_Token { get; private set; }

        public FinancialAccountDetails(int account_id) : base()
        {
            Account_ID = account_id;
            get_AccountData();
            Client_BrainTree_Token = new BrainTreeBase().get_ClientToken();
        }

        private void get_AccountData()
        {
            string query = @"SELECT T.[Transaction_ID], T.[Amount], T.[Date_of_Transaction], T.[Description], CT.[Currency]
	                         FROM ProudSourceAccounting.dbo.Accounts A 
                             JOIN ProudSourceAccounting.dbo.Transactions T ON A.[Account_ID] = T.[Account_ID]
                             JOIN ProudSourceAccounting.dbo.Currency_Types CT ON T.[Currency_Type_ID] = CT.[Currency_Type_ID]
                             WHERE A.[Account_ID] = @Account_ID";

            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.Text;
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
            //      [0] : All of the transactions for this account.

            Transactions = set.Tables[0].Rows;

            if(Transactions.Count > 0)
            {
                decimal i = 0.0m;
                foreach(DataRow k in Transactions)
                {
                    if (k["Currency"].ToString() == "USD")
                    {
                        i += (decimal)k["Amount"];
                    }
                }
                Account_Balance = i;
            }
            else
            {
                Account_Balance = 0.0m;
            }
        }

    }
}