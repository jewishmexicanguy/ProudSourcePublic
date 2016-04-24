using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace ZPaypalAPI
{
    public class ZDatabaseManager
    {

        public static bool sendTransactionQuery(int accountID, double subTotal, int currencyType, int categoryType, int transactionType)
        {

            bool returnValue = true;

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProudSourceStaging"].ConnectionString);

            string query = "sp_Insert_Financial_Transaction";
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Account_ID", accountID);
            command.Parameters.AddWithValue("@Category_Type_ID", categoryType);
            command.Parameters.AddWithValue("@Currency_Type_ID", currencyType);
            command.Parameters.AddWithValue("@Transaction_Type_ID", transactionType);
            command.Parameters.AddWithValue("@Amount", subTotal);
            command.Parameters.AddWithValue("@Src_Account_ID", DBNull.Value);
            //command.Parameters.AddWithValue("@Current_Balance", 10);

            int rowid = 0;
            try
            {
                conn.Open();
                rowid = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                returnValue = false;
                // do something
            }
            finally
            {
                conn.Close();
            }

            return returnValue;
        }

        public static bool editTransactionState(int transactionID, string state)
        {

            bool returnValue = true;

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProudSourceStaging"].ConnectionString);

            string query = @"
                UPDATE Transactions
                SET Transaction_State = @State
                WHERE Transaction_ID = @TransactionID; ";

            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.AddWithValue("@TransactionID", transactionID);
            command.Parameters.AddWithValue("@State", state);

            int rowid = 0;
            try
            {
                conn.Open();
                rowid = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                returnValue = false;
                // do something
            }
            finally
            {
                conn.Close();
            }

            return returnValue;
        }

    }
}
