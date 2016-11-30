using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Braintree;

namespace ProudSourceAccountingLibrary
{
    /// <summary>
    /// This class will provide actions that can be done on a financial account at an account level.
    /// </summary>
    public class ProudSourceFinancialAccount : PSAccountingBase
    {
        /// <summary>
        /// Protected integer resident that represents the account in question with this object.
        /// </summary>
        protected int Account_Id;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="account_id"></param>
        public ProudSourceFinancialAccount(int account_id) : base()
        {
            Account_Id = account_id;
        }

        /// <summary>
        /// This method will get a list of transactions for the account this object represents.
        /// </summary>
        /// <returns></returns>
        public DataSet get_Transactions()
        {
            DataSet set = new DataSet();
            string query = @"SELECT [Id],
                                [Account_Id],
                                [DateTime_Created],
                                [Description],
                                [Amount],
                                [src_Account_Id],
                                [Transaction_State],
                                [Outside_Transaction_Id],
                                [Outside_Financial_Platform],
                                [DateTime_Processed]
                            FROM Transactions T
                            WHERE T.[Account_Id] = @Account_Id";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.Text;
            adapter.SelectCommand.Parameters.AddWithValue("@Account_Id", Account_Id);
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
            return set;
        }

        /// <summary>
        /// This method will return a particular transaction given the a transaction Id.
        /// </summary>
        /// <param name="Transaction_Id"></param>
        /// <returns></returns>
        public DataSet get_Transaction(int Transaction_Id)
        {
            DataSet set = new DataSet();
            string query = @"SELECT [Id],
                                [Account_Id],
                                [DateTime_Created],
                                [Description],
                                [Amount],
                                [src_Account_Id],
                                [Transaction_State],
                                [Outside_Transaction_Id],
                                [Outside_Financial_Platform],
                                [DateTime_Processed]
                            FROM Transactions T
                            WHERE T.[Id] = @Transaction_Id";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.Text;
            adapter.SelectCommand.Parameters.AddWithValue("@Transaction_Id", Transaction_Id);
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
            return set;
        }

        /// <summary>
        /// This method will return a list of pending transactions for the account this object is for.
        /// </summary>
        /// <returns></returns>
        public DataSet get_Transactions_Pending()
        {
            DataSet set = new DataSet();
            string query = @"SELECT [Id],
                                [Account_Id],
                                [DateTime_Created],
                                [Description],
                                [Amount],
                                [src_Account_Id],
                                [Transaction_State],
                                [Outside_Transaction_Id],
                                [Outside_Financial_Platform],
                                [DateTime_Processed]
                            FROM Transactions T
                            WHERE T.[Account_Id] = @Account_Id
                            AND T.[Transaction_State] = 'PENDING'";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.Text;
            adapter.SelectCommand.Parameters.AddWithValue("@Account_Id", Account_Id);
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
            return set;
        }

        /// <summary>
        /// This method will return a list of transactions that have been processed.
        /// </summary>
        /// <returns></returns>
        public DataSet get_Transactions_Processed()
        {
            DataSet set = new DataSet();
            string query = @"SELECT [Id],
                                [Account_Id],
                                [DateTime_Created],
                                [Description],
                                [Amount],
                                [src_Account_Id],
                                [Transaction_State],
                                [Outside_Transaction_Id],
                                [Outside_Financial_Platform],
                                [DateTime_Processed]
                            FROM Transactions T
                            WHERE T.[Account_Id] = @Account_Id
                            AND T.[Transaction_State] = 'PROCESSED'";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.Text;
            adapter.SelectCommand.Parameters.AddWithValue("@Account_Id", Account_Id);
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
            return set;
        }

        /// <summary>
        /// This method will return a list of transactions that have failed.
        /// </summary>
        /// <returns></returns>
        public DataSet get_Transactions_Failed()
        {
            DataSet set = new DataSet();
            string query = @"SELECT [Id],
                                [Account_Id],
                                [DateTime_Created],
                                [Description],
                                [Amount],
                                [src_Account_Id],
                                [Transaction_State],
                                [Outside_Transaction_Id],
                                [Outside_Financial_Platform],
                                [DateTime_Processed]
                            FROM Transactions T
                            WHERE T.[Account_Id] = @Account_Id
                            AND T.[Transaction_State] = 'DENIED'";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.Text;
            adapter.SelectCommand.Parameters.AddWithValue("@Account_Id", Account_Id);
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
            return set;
        }

        /// <summary>
        /// This method will insert a new transaction into our transaction table and will return an integer Id that is the id of the new transaction.
        /// </summary>
        /// <param name="Transaction_Dictionary"></param>
        /// <returns></returns>
        public int insert_Transaction(Dictionary<string, string> Transaction_Dictionary)
        {
            string query = "usp_Transaction_Insert";
            int transaction_id = 0;
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Account_Id", int.Parse(Transaction_Dictionary["Account_Id"]));
            command.Parameters.AddWithValue("@Category_Type_Id", 3);
            command.Parameters.AddWithValue("@Transaction_Type_Id", 1);
            command.Parameters.AddWithValue("@Amount", decimal.Parse(Transaction_Dictionary["Amount"]));
            command.Parameters.AddWithValue("@Currency_Type_Id", 2);
            command.Parameters.AddWithValue("@Financial_Platform", Transaction_Dictionary["Financial_Platform"]);
            try
            {
                conn.Open();
                transaction_id = int.Parse(command.ExecuteScalar().ToString());
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
            return transaction_id;
        }

        /// <summary>
        /// This method will update a transaction with values from the dictionary accepted by this method.
        /// </summary>
        /// <param name="Transaction_Id"></param>
        /// <param name="Transaction_Dictionary"></param>
        /// <returns></returns>
        public bool update_Transaction(int Transaction_Id, Dictionary<string, string> Transaction_Dictionary)
        {
            bool result = false;
            string query = "usp_Update_Transaction";
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Transaction_Id", Transaction_Id);
            foreach(string key in Transaction_Dictionary.Keys)
            {
                if (string.IsNullOrEmpty(Transaction_Dictionary[key]))
                {
                    command.Parameters.AddWithValue("@" + key, DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@" + key, Transaction_Dictionary[key]);
                }
            }
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                result = true;
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }

    /// <summary>
    /// This class will expose functionality to do BrainTree level accounting actions.
    /// 
    /// The methods are dependent on inherited methods from ProudSourceFinancialAccount and BrainTree
    /// 
    /// The account id scopes the actions performed to a particular account.
    /// </summary>
    public class ProudSourceBrainTree : ProudSourceFinancialAccount
    {
        /// <summary>
        /// merchant id for brain tree.
        /// </summary>
        private const string merchant_ID = "h9vwr53gf26m9c8v";

        /// <summary>
        /// public key used for signing requests with braintree.
        /// </summary>
        private const string public_key = "kd9jp9qvdny8xkcv";

        /// <summary>
        /// private key used for signing requests with braintree
        /// </summary>
        private const string private_key = "4cc5cc1ed3ffc1f92715ae026ca72f96";

        /// <summary>
        /// Class resident that is a gateway object whcih opens connections to BrainTrees financial processing.
        /// </summary>
        protected BraintreeGateway gateway;

        /// <summary>
        /// private resident that will house a braintree request
        /// </summary>
        private TransactionRequest request;

        /// <summary>
        /// Accessor that harbours whether or not the transaction was successfully submited or not.
        /// </summary>
        public bool Transaction_Success { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="account_id"></param>
        public ProudSourceBrainTree(int account_id) : base(account_id)
        {
            gateway = new BraintreeGateway
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = merchant_ID,
                PublicKey = public_key,
                PrivateKey = private_key
            };
        }

        public string get_ClientToken()
        {
            return gateway.ClientToken.generate();
        }

        /// <summary>
        /// This method will affect a new inbound payment by introducing a new insertion to our Transactions table.
        /// 
        /// We may want to chek to make sure that negative values don't get introduced as deposits.
        /// </summary>
        /// <param name="nonce"></param>
        /// <param name="amount"></param>
        /// <param name="platform"></param>
        /// <returns></returns>
        public bool make_InboundPayment(string nonce, decimal amount, int account_id, string platform)
        {
            Dictionary<string, string> transactionDict = new Dictionary<string, string>();
            transactionDict.Add("Category_Type_Id", "3");
            transactionDict.Add("Transaction_Type_Id", "1");
            transactionDict.Add("Amount", amount.ToString());
            transactionDict.Add("Currency_Type_Id", "2");
            transactionDict.Add("Account_Id", account_id.ToString());
            transactionDict.Add("Financial_Platform", platform);
            int Transaction_Id = insert_Transaction(transactionDict);
            // create a string dictionary using the newly created transaction id on hand.
            Dictionary<string, string> proudsource_custom_field = new Dictionary<string, string>();
            proudsource_custom_field.Add("proudsource", Transaction_Id.ToString());
            // create a BrainTree transaction using our custom field 
            request = new TransactionRequest()
            {
                Amount = amount,
                PaymentMethodNonce = nonce,
                Options = new TransactionOptionsRequest()
                {
                    SubmitForSettlement = true
                },
                CustomFields = proudsource_custom_field
            };
            // create dictionary for the method that will update our record.
            Dictionary<string, string> updateDict = new Dictionary<string, string>();
            // send transaction request
            Result<Transaction> result = gateway.Transaction.Sale(request);
            if(result.IsSuccess())
            {
                updateDict.Add("Amount", result.Target.Amount.ToString());
                updateDict.Add("Transaction_State", "PENDING");
                updateDict.Add("Outside_Transaction_Id", result.Target.Id);
                updateDict.Add("Outside_Financial_Platform", "BrainTree");
                updateDict.Add("DateTime_Processed", DateTime.Now.ToString());
            }
            else
            {
                updateDict.Add("Amount", amount.ToString());
                updateDict.Add("Transaction_State", "FAILED");
                updateDict.Add("Outside_Transaction_Id", DBNull.Value.ToString());
                updateDict.Add("Outside_Financial_Platform", "BrainTree");
                updateDict.Add("DateTime_Processed", DateTime.Now.ToString());
            }
            // update our transaction record based o nthe success of our request.
            update_Transaction(Transaction_Id, updateDict);
            return result.IsSuccess();
        }

        public bool make_OutboundPayment(string nonce, decimal amount, string platform)
        {
            // TODO:
            return false;
        }

        public bool update_TransactionStatus(int Transaction_Id, Dictionary<string, string> TransactionDictionary)
        {
            bool success = false;
            string query = @"usp_Update_Transaction";
            SqlCommand commad = new SqlCommand(query, conn);
            commad.CommandType = CommandType.StoredProcedure;
            commad.Parameters.AddWithValue("@Transaction_Id", Transaction_Id);
            if(TransactionDictionary.ContainsKey("Amount"))
            {
                commad.Parameters.AddWithValue("@Amount", decimal.Parse(TransactionDictionary["Amount"]));
            }
            if(TransactionDictionary.ContainsKey("src_Account_Id"))
            {
                commad.Parameters.AddWithValue("@src_Account_Id", int.Parse(TransactionDictionary["src_Account_Id"]));
            }
            if(TransactionDictionary.ContainsKey("Transaction_State"))
            {
                commad.Parameters.AddWithValue("@Transaction_State", TransactionDictionary["Transaction_State"]);
            }
            if(TransactionDictionary.ContainsKey("Outside_Transaction_Id"))
            {
                commad.Parameters.AddWithValue("@Outside_Transaction_Id", TransactionDictionary["Outside_Transaction_Id"]);
            }
            if(TransactionDictionary.ContainsKey("Outside_Financial_Platform"))
            {
                commad.Parameters.AddWithValue("@Outside_Financial_Platform", TransactionDictionary["Outside_Financial_Platform"]);
            }
            if(TransactionDictionary.ContainsKey("DateTime_Processed"))
            {
                commad.Parameters.AddWithValue("@DateTime_Processed", DateTime.Parse(TransactionDictionary["DateTime_Processed"]));
            }
            try
            {
                conn.Open();
                commad.ExecuteNonQuery();
                success = true;
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
            return success;
        }

        /// <summary>
        /// This method will retrive the status of a given BrainTree transaction.
        /// </summary>
        /// <param name="BrainTree_Transaction_Id"></param>
        /// <returns></returns>
        public string get_BrainTree_Transaction_Status(string BrainTree_Transaction_Id)
        {
            Transaction BTtran = gateway.Transaction.Find(BrainTree_Transaction_Id);
            StatusEvent[] statuses = BTtran.StatusHistory;
            string status = BTtran.Status.ToString();
            return status;
        }

        /// <summary>
        /// Get the transactions on our transactions table that still need to be processed.
        /// </summary>
        /// <returns></returns>
        private DataSet _get_UnprocessedTransactions()
        {
            DataSet set = new DataSet();
            // TODO:
            return set;
        }
    }

    /// <summary>
    /// This class will expose methods to be used by internal ProudSource routines to manage Account's and the Transactions belonging to them.
    /// </summary>
    public class ProudSourceAccountingProcesses : PSAccountingBase
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        public ProudSourceAccountingProcesses() : base()
        {

        }

        /// <summary>
        /// Method that will retrive a set of all pending transactions from our Transactions table.
        /// </summary>
        /// <returns></returns>
        public DataSet get_pendingTransactions()
        {
            string query = @"SELECT [Id],
                                [Account_Id],
                                [Amount],
                                [Transaction_State],
                                [Outside_Transaction_Id]
                            FROM Transactions T
                            WHERE T.[Transaction_State] <> 'DENIED'
                            AND T.[Transaction_State] <> 'PROCESSED'
                            AND T.[Transaction_State] <> 'FAILED'
                            AND T.[Transaction_State] <> 'DENIED'
                            AND T.[Outside_Transaction_Id] IS NOT NULL";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.Text;
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
            return set;
        }
    }
}