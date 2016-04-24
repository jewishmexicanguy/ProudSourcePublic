using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProudSourceAccountingLibrary
{
    /// <summary>
    /// Class that will handle all actions that need to add, modify or delete accounts or transactions.
    /// </summary>
    public class AccountingEdits : PSAccountingBase
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        public AccountingEdits() : base()
        {

        }

        /// <summary>
        /// This method will handle entering a PROC transaction between an investor an project.
        /// 
        /// It will in reality be adding two records to our transaction table. One reciving the money, the Project, and the other sending the money, the Investor.
        /// 
        /// TODO: Make a generator class out of this and make it accept an array of arguments to generate handeling making PROCS between different currencies. 
        /// 
        ///         If we want to stay away from being tied to the dollar we must be able to potentially be able to make PROCS between any currency. 
        /// 
        ///         We can use queries using the datestamp field to keep track of the value between the currencies.
        /// 
        /// </summary>
        /// <param name="investor_financial_account_id">The financial account ID of the invesotr</param>
        /// <param name="project_financial_account_id">The financial account ID of the project</param>
        /// <returns></returns>
        protected bool Insert_PROCTransaction(int investor_financial_account_id, int project_financial_account_id, decimal amount)
        {
            try
            {
                // instantiate our string that has our stored procedure
                string query = "sp_Insert_Financial_Transaction";
                SqlCommand investor_transaction_record = new SqlCommand(query, conn);
                SqlCommand project_transaction_record = new SqlCommand(query, conn);

                // define the command for the investor record, it will record a negative value for this transaction.
                investor_transaction_record.CommandType = CommandType.StoredProcedure;
                investor_transaction_record.Parameters.AddWithValue("@Account_ID", investor_financial_account_id);
                investor_transaction_record.Parameters.AddWithValue("@Category_Type_ID", 7);
                investor_transaction_record.Parameters.AddWithValue("@Transaction_Type_ID", 5);
                investor_transaction_record.Parameters.AddWithValue("@Amount", decimal.Negate(amount));
                investor_transaction_record.Parameters.AddWithValue("@Currency_Type_ID", 2);
                investor_transaction_record.Parameters.AddWithValue("@Src_Account_ID", project_financial_account_id);

                // define the command for the project record, it will record a positive value for this transaction and as a transfer type.
                project_transaction_record.CommandType = CommandType.StoredProcedure;
                project_transaction_record.Parameters.AddWithValue("@Account_ID", project_financial_account_id);
                project_transaction_record.Parameters.AddWithValue("@Category_Type_ID", 7);
                project_transaction_record.Parameters.AddWithValue("@Transaction_Type_ID", 1);
                project_transaction_record.Parameters.AddWithValue("@Amount", amount);
                project_transaction_record.Parameters.AddWithValue("@Currency_Type_ID", 2);
                project_transaction_record.Parameters.AddWithValue("@Src_Account_ID", investor_financial_account_id);

                int investor_transaction_ID = 0;
                int project_transaction_ID = 0;

                try
                {
                    conn.Open();
                    investor_transaction_ID = (int)investor_transaction_record.ExecuteScalar();
                    project_transaction_ID = (int)project_transaction_record.ExecuteScalar();
                }
                finally
                {
                    conn.Close();
                }
                // we could do something with the transaction identities we have returned from inserting these two records.
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("AccountEdits.Insert_PROCTransaction() error message : {0} \r\n {1} \r\n {2}", e.Message, e.InnerException, e.Data));
                return false;
            }
        }

        /// <summary>
        /// TODO : Make a generator class capable of inserting transactions of any currency.
        /// 
        ///         We want to be able to be able to know what the percived value was in the past when compared in the future.
        ///         
        ///         This is a requirment because of the volatile nature of crypto currencies.
        ///         
        /// </summary>
        /// <param name="financial_account_id"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        protected int? Insert_BitcoinTransaction(int financial_account_id, decimal amount, bool isDeposit)
        {
            try
            {
                string query = "ProudSourceAccounting.dbo.sp_Insert_Financial_Transaction";
                SqlDataAdapter command = new SqlDataAdapter(query, conn);
                command.SelectCommand.CommandType = CommandType.StoredProcedure;
                command.SelectCommand.Parameters.AddWithValue("@Account_ID", financial_account_id);
                command.SelectCommand.Parameters.AddWithValue("@Category_Type_ID", 2);
                if (isDeposit)
                {
                    command.SelectCommand.Parameters.AddWithValue("@Transaction_Type_ID", 1);
                }
                else
                {
                    command.SelectCommand.Parameters.AddWithValue("@Transaction_Type_ID", 2);
                }
                command.SelectCommand.Parameters.AddWithValue("@Amount", amount);
                command.SelectCommand.Parameters.AddWithValue("@Currency_Type_ID", 1);
                command.SelectCommand.Parameters.AddWithValue("@Src_Account_ID", DBNull.Value);
                DataSet ds = new DataSet();
                try
                {
                    conn.Open();
                    command.Fill(ds);
                }
                finally
                {
                    conn.Close();
                }
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("AccountingEdits.InsertBitcoinTransaction() error message : {0} \r\n {1} \r\n {2}", e.Message, e.InnerException, e.Data));
                return null;
            }
        }

        protected int? Insert_USDTransaction(int financial_account_ID, decimal amount, bool isDeposit)
        {
            try
            {
                string query = "ProudSourceAccounting.dbo.sp_Insert_Financial_Transaction";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@Account_ID", financial_account_ID);
                if (isDeposit)
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@Category_Type_ID", 13);
                }
                else
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@Category_Type_ID", 14);
                }
                if (isDeposit)
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@Transaction_Type_ID", 1);
                }
                else
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@Transaction_Type_ID", 2);
                }
                adapter.SelectCommand.Parameters.AddWithValue("@Amount", amount);
                adapter.SelectCommand.Parameters.AddWithValue("@Currency_Type_ID", 2);
                adapter.SelectCommand.Parameters.AddWithValue("@Src_Account_ID", DBNull.Value);
                DataSet ds = new DataSet();
                try
                {
                    conn.Open();
                    adapter.Fill(ds);
                }
                finally
                {
                    conn.Close();
                }
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("AccountingEdits.InsertCurrencyTransaction() error message : {0} \r\n {1} \r\n {2}", e.Message, e.InnerException, e.Data));
                return null;
            }
        }

        /// <summary>
        /// This method will update a transaction record as processed, it accepts one parameter which is the id of the transaction to be updated.
        /// </summary>
        /// <param name="Transaction_ID"></param>
        /// <returns></returns>
        protected bool Update_TransactionProceessed(int Transaction_ID)
        {
            string query = @"UPDATE Transactions
                                SET [Transaction_State] = 'PROCESSED'
                                WHERE [Transaction_ID] = @Transaction_ID";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@Transaction_ID", Transaction_ID);
            bool success = false;
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                success = true;
            }
            catch (Exception e)
            {
                success = false;
            }
            finally
            {
                conn.Close();
            }
            return success;
        }


    }
}
