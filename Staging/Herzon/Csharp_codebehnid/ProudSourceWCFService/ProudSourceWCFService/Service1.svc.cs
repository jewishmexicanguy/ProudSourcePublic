using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ProudSourceWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public FinancialAccountComposite get_FinancialAccountData(string UserId, int account_id)
        {
            DataSet set = new DataSet();
            FinancialAccountComposite financialaccount = new FinancialAccountComposite();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "usp_Select_FinancialAccount_Details";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@User_Id", UserId);
                adapter.SelectCommand.Parameters.AddWithValue("@Account_Id", account_id);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
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
            }
            // [0] : account details
            // [1] : account balance
            // [2] : processed transaction
            // [3] : pending transactions
            // [4] : failed transactions
            financialaccount.FinancialAccount = get_DictionaryObject(set.Tables[0].Rows[0], set.Tables[0].Columns);
            if(set.Tables[1].Rows[0]["Balance"] != DBNull.Value)
            {
                financialaccount.FinancialAccount.Add("Balance", set.Tables[1].Rows[0]["Balance"].ToString());
            }
            else
            {
                financialaccount.FinancialAccount.Add("Balance", "0.00");
            }
            financialaccount.FinancialTransactions_Processed = get_indexed_MetaDictionary(set.Tables[2].Rows, set.Tables[2].Columns);
            financialaccount.FinancialTransactions_Pending = get_indexed_MetaDictionary(set.Tables[3].Rows, set.Tables[3].Columns);
            financialaccount.FinancialTransactions_Failed = get_indexed_MetaDictionary(set.Tables[4].Rows, set.Tables[4].Columns);
            financialaccount.Client_Braintree_Token = new ProudSourceAccountingLibrary.ProudSourceBrainTree(account_id).get_ClientToken();
            return financialaccount;
        }

        public bool create_new_BT_Transactions(string UserId, int account_Id, string nonce, decimal amount)
        {
            bool result = false;
            // TODO: Implement this, call method from our accouting library connected with BrainTree.
            return result;
        }

        public List<Dictionary<string, string>> ajax_SearchResults(string keyArg)
        {
            DataSet set = new DataSet();
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "usp_Select_KeyArg_Search";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (string.IsNullOrEmpty(keyArg))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@keyArg", "");
                }
                else
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@keyArg", keyArg);
                }
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
                // [0] : Project_Results
                // [1] : Entrepreneur_Results
                // [2] : Investor_Results
                foreach(Dictionary<string,string> i in get_indexed_MetaDictionary(set.Tables[0].Rows, set.Tables[0].Columns).Values)
                {
                    i.Add("ProfileType_Id", "3");
                    results.Add(i);
                }
                //foreach(Dictionary<string,string> i in get_indexed_MetaDictionary(set.Tables[1].Rows, set.Tables[1].Columns).Values)
                //{
                //    i.Add("ProfileType_Id", "1");
                //    results.Add(i);
                //}
                //foreach(Dictionary<string,string> i in get_indexed_MetaDictionary(set.Tables[2].Rows, set.Tables[2].Columns).Values)
                //{
                //    i.Add("ProfileType_Id", "2");
                //    results.Add(i);
                //}
                return results;
            }
        }

        #region Document operations
        public bool upload_Document(DocumentFileComposite document)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                if ((bool)document.DocumentDict["bool,IsProject"])
                {
                    string query = "usp_Project_Document_Insert";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@Project_Id", document.DocumentDict["int,profile_Id"]);
                    command.Parameters.AddWithValue("@File_Name", document.DocumentDict["string,fileName"]);
                    command.Parameters.AddWithValue("@Mime_Type ", document.DocumentDict["string,mimeType"]);
                    command.Parameters.AddWithValue("@FileBytes", document.DocumentDict["byte[],docData"]);
                    command.Parameters.AddWithValue("@Description", document.DocumentDict["string,description"]);
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        conn.Open();
                        command.ExecuteNonQuery();
                        result = true;
                    }
                    catch (Exception e)
                    {
                        result = false;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    string query = "usp_Profile_Document_Insert";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@Profile_Id", document.DocumentDict["int,profile_Id"]);
                    command.Parameters.AddWithValue("@File_Name", document.DocumentDict["string,fileName"]);
                    command.Parameters.AddWithValue("@Mime_Type ", document.DocumentDict["string,mimeType"]);
                    command.Parameters.AddWithValue("@FileBytes", document.DocumentDict["byte[],docData"]);
                    command.Parameters.AddWithValue("@Description", document.DocumentDict["string,description"]);
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        conn.Open();
                        command.ExecuteNonQuery();
                        result = true;
                    }
                    catch (Exception e)
                    {
                        result = false;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return result;
        }

        public bool update_Document(DocumentFileComposite document)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "usp_Document_Update";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Document_Id", document.DocumentDict["int,document_Id"]);
                command.Parameters.AddWithValue("@Binary_File", document.DocumentDict["byte[],docData"]);
                command.Parameters.AddWithValue("@File_Name", document.DocumentDict["string,fileName"]);
                command.Parameters.AddWithValue("@Mime_Type", document.DocumentDict["string,mimeType"]);
                command.Parameters.AddWithValue("@Description", document.DocumentDict["string,description"]);
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception e)
                {
                    result = false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        public Dictionary<string, string> get_Document(int document_Id)
        {
            DataSet set = new DataSet();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "SELECT [Id], [Binary_File], [File_Name], [Mime_Type], [Description] FROM Documents WHERE [Id] = @Document_Id";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@Document_Id", document_Id);
                adapter.SelectCommand.CommandType = CommandType.Text;
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
            }
            //return get_DictionaryFile(set.Tables[0].Rows[0], set.Tables[0].Columns);
            return get_DictionaryObject(set.Tables[0].Rows[0], set.Tables[0].Columns);
        }

        public bool delete_Document(int document_id)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "usp_Delete_Document";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Document_Id", document_id);
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
            }
            return result;
        }
        #endregion

        #region Images operations
        public bool upload_Image(ImageFileComposite image)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                if ((bool)image.ImageDict["bool,IsProject"])
                {
                    // this is a projct profile that we are associating this image with.
                    string query = "usp_Project_Image_Insert";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@Project_Id", image.ImageDict["int,profile_Id"]);
                    command.Parameters.AddWithValue("@Binary_Image", image.ImageDict["byte[],imageData"]);
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        conn.Open();
                        command.ExecuteNonQuery();
                        result = true;
                    }
                    catch (Exception e)
                    {
                        result = false;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    // this is a normal profile that we are associating this image with.
                    string query = "usp_Profile_Image_Insert";
                    SqlCommand comamnd = new SqlCommand(query, conn);
                    comamnd.Parameters.AddWithValue("@Profile_Id", image.ImageDict["int,profile_Id"]);
                    comamnd.Parameters.AddWithValue("@Binary_Image", image.ImageDict["byte[],imageData"]);
                    comamnd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        conn.Open();
                        comamnd.ExecuteNonQuery();
                        result = true;
                    }
                    catch (Exception e)
                    {
                        result = false;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return result;
        }

        public bool update_Image(ImageFileComposite image)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "usp_Image_Update";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Image_Id", image.ImageDict["int,image_Id"]);
                command.Parameters.AddWithValue("@Binary_Image", image.ImageDict["byte[],imageData"]);
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception e)
                {
                    result = false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        public bool delete_Image(int image_id)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "usp_Delete_Image";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Image_Id", image_id);
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
            }
            return result;
            }

        public string get_Image(int image_id)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string results = string.Empty;
                string query = "SELECT [Binary_Image] FROM Images WHERE [Id] = @Image_Id";
                DataSet set = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.CommandType = CommandType.Text;
                if (image_id == 0)
                {
                    return string.Empty;
                }
                else
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@Image_Id", image_id);
                }
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
                results = Convert.ToBase64String((byte[])set.Tables[0].Rows[0]["Binary_Image"]);
                return results;
            }
        }
        #endregion

        #region User operations
        public UserRecordComposite get_UserById(string Id)
        {
            string query = @"SELECT 
                                [Id],
                                [Email],
                                [EmailConfirmed],
                                [PasswordHash],
                                [SecurityStamp],
                                [PhoneNumber],
                                [PhoneNumberConfirmed],
                                [TwoFactorEnabled],
                                [LockoutEndDateUtc],
                                [LockoutEnabled],
                                [AccessFailedCount],
                                [UserName],
                                [Name]
                            FROM Users
                            WHERE [Id] = @Id";

            DataSet set = new DataSet();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@Id", Id);
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
            }
            UserRecordComposite user = new UserRecordComposite();
            if (set.Tables[0].Rows.Count > 0 && set.Tables[0].Rows.Count < 2)
            {
                DataRow row = set.Tables[0].Rows[0];
                user.Id = row["Id"].ToString();
                if (row["AccessFailedCount"] == DBNull.Value)
                {
                    user.AccessFailedCount = 0;
                }
                else
                {
                    user.AccessFailedCount = (int)row["AccessFailedCount"];
                }
                user.EmailConfirmed = (bool)row["EmailConfirmed"];
                if (row["Email"] == DBNull.Value)
                {
                    user.Email = null;
                }
                else
                {
                    user.Email = row["Email"].ToString();
                }
                user.LockoutEnabled = (bool)row["LockoutEnabled"];
                if (row["LockoutEndDateUtc"] == DBNull.Value)
                {
                    user.LockoutEndDateUtc = DateTime.MinValue;
                }
                else
                {
                    user.LockoutEndDateUtc = DateTime.Parse(row["LockoutEndDateUtc"].ToString());
                }
                if (row["Name"] == DBNull.Value)
                {
                    user.Name = null;
                }
                else
                {
                    user.Name = row["Name"].ToString();
                }
                user.PasswordHash = row["PasswordHash"].ToString();
                if (row["PhoneNumber"] == DBNull.Value)
                {
                    user.PhoneNumber = null;
                }
                else
                {
                    user.PhoneNumber = row["PhoneNumber"].ToString();
                }
                user.PhoneNumberConfirmed = (bool)row["PhoneNumberConfirmed"];
                if (row["SecurityStamp"] == DBNull.Value)
                {
                    user.SecurityStamp = null;
                }
                else
                {
                    user.SecurityStamp = row["SecurityStamp"].ToString();
                }
                user.TwoFactorEnabled = (bool)row["TwoFactorEnabled"];
                if (row["UserName"] == DBNull.Value)
                {
                    user.UserName = null;
                }
                else
                {
                    user.UserName = row["UserName"].ToString();
                }
            }
            return user;
        }

        public UserRecordComposite get_UserByUserName(string UserName)
        {
            string query = @"SELECT 
                                [Id],
                                [Email],
                                [EmailConfirmed],
                                [PasswordHash],
                                [SecurityStamp],
                                [PhoneNumber],
                                [PhoneNumberConfirmed],
                                [TwoFactorEnabled],
                                [LockoutEndDateUtc],
                                [LockoutEnabled],
                                [AccessFailedCount],
                                [UserName],
                                [Name]
                            FROM Users
                            WHERE [UserName] = @UserName";

            DataSet set = new DataSet();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@UserName", UserName);
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
            }
            UserRecordComposite user = new UserRecordComposite();
            if (set.Tables[0].Rows.Count > 0 && set.Tables[0].Rows.Count < 2)
            {
                DataRow row = set.Tables[0].Rows[0];
                user.Id = row["Id"].ToString();
                if (row["AccessFailedCount"] == DBNull.Value)
                {
                    user.AccessFailedCount = 0;
                }
                else
                {
                    user.AccessFailedCount = (int)row["AccessFailedCount"];
                }
                user.EmailConfirmed = (bool)row["EmailConfirmed"];
                if (row["Email"] == DBNull.Value)
                {
                    user.Email = null;
                }
                else
                {
                    user.Email = row["Email"].ToString();
                }
                user.LockoutEnabled = (bool)row["LockoutEnabled"];
                if (row["LockoutEndDateUtc"] == DBNull.Value)
                {
                    user.LockoutEndDateUtc = DateTime.MinValue;
                }
                else
                {
                    user.LockoutEndDateUtc = DateTime.Parse(row["LockoutEndDateUtc"].ToString());
                }
                if (row["Name"] == DBNull.Value)
                {
                    user.Name = null;
                }
                else
                {
                    user.Name = row["Name"].ToString();
                }
                user.PasswordHash = row["PasswordHash"].ToString();
                if (row["PhoneNumber"] == DBNull.Value)
                {
                    user.PhoneNumber = null;
                }
                else
                {
                    user.PhoneNumber = row["PhoneNumber"].ToString();
                }
                user.PhoneNumberConfirmed = (bool)row["PhoneNumberConfirmed"];
                if (row["SecurityStamp"] == DBNull.Value)
                {
                    user.SecurityStamp = null;
                }
                else
                {
                    user.SecurityStamp = row["SecurityStamp"].ToString();
                }
                user.TwoFactorEnabled = (bool)row["TwoFactorEnabled"];
                if (row["UserName"] == DBNull.Value)
                {
                    user.UserName = null;
                }
                else
                {
                    user.UserName = row["UserName"].ToString();
                }
            }
            return user;
        }

        public string get_PasswordHash(string UserId)
        {
            DataSet set = new DataSet();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = @"SELECT [PasswordHash],
                            FROM Users
                            WHERE [Id] = @Id";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@Id", UserId);
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
            }
            if (set.Tables[0].Rows.Count > 0 && set.Tables[0].Rows.Count > 2)
            {
                return set.Tables[0].Rows[0]["PasswordHash"].ToString();
            }
            else
            {
                return null;
            }
        }

        public bool set_PasswordHash(string userId, string passwordHash, string userName, string name)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "usp_Users_Insert";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", userId);
                command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Name", name);
                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception e)
                {
                    result = false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        public UserIndexComposite get_UserIndexData(string userId)
        {
            DataSet set = new DataSet();
            UserIndexComposite user = new UserIndexComposite();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "usp_Select_UserIndex";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
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
            }
            if (set.Tables.Count != 12)
            {
                // somthing went wrong.
            }
            else
            {
                // SELECT the user profile data
                // SELECT the Entrepreneur profile data
                // SELECT the Entrepreneur Documents
                // SELECT the Entrepreneur Links
                // SELECT the Entrepreneur Embelishments
                // SELECT the balance of each project account for this entrepreneur(in USD)
                // SELECT the Investor profile data
                // SELECT the Investor Documents
                // SELECT the Investor Links
                // SELECT the Investor Embelishments
                // SELECT the Investor's Financial account details in USD
                // SELECT the investors account Id

                // User profile data
                user.UserProfile = get_DictionaryObject(set.Tables[0].Rows[0], set.Tables[0].Columns);
                // Entrepreneur profile data
                user.EntrepreneurProfile = get_DictionaryObject(set.Tables[1].Rows[0], set.Tables[1].Columns);
                // Entrepreneur documents
                user.EntrepreneurDocuments = get_indexed_MetaDictionary(set.Tables[2].Rows, set.Tables[2].Columns);
                // Entrepreneur links
                user.EntrepreneurLinks = get_indexed_MetaDictionary(set.Tables[3].Rows, set.Tables[3].Columns);
                // Entrepreneur embelishments
                user.EntrepreneurEmbelishments = get_indexed_MetaDictionary(set.Tables[4].Rows, set.Tables[4].Columns);
                // Entrepreneur Accounts
                user.EntrepreneurAccounts = get_indexed_MetaDictionary(set.Tables[5].Rows, set.Tables[5].Columns);
                // Investor profie data
                user.InvestorProfile = get_DictionaryObject(set.Tables[6].Rows[0], set.Tables[6].Columns);
                // Investor documents
                user.InvestorDocuments = get_indexed_MetaDictionary(set.Tables[7].Rows, set.Tables[7].Columns);
                // Investor links
                user.InvestorLinks = get_indexed_MetaDictionary(set.Tables[8].Rows, set.Tables[8].Columns);
                // Investor embelishments
                user.InvestorEmbelishments = get_indexed_MetaDictionary(set.Tables[9].Rows, set.Tables[9].Columns);
                // Investor accounts
                if (set.Tables[10].Rows.Count > 0)
                {
                    user.InvestorAccount = get_DictionaryObject(set.Tables[10].Rows[0], set.Tables[10].Columns);
                }
                else
                {
                    user.InvestorAccount = new Dictionary<string, string>();
                }
                user.Investor_Financial_Account_Id = int.Parse(set.Tables[11].Rows[0]["Id"].ToString());
            }
            return user;
        }

        public bool update_ProfileData(string UserId, Dictionary<string, string> userdata)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "update_UserIndexProfile";
                object entrepreneur_public, investor_public;
                if (userdata.ContainsKey("userIndexData.EntrepreneurProfile[Profile_Public]"))
                {
                    if (userdata["userIndexData.EntrepreneurProfile[Profile_Public]"] == "on")
                    {
                        entrepreneur_public = true;
                    }
                    else
                    {
                        entrepreneur_public = false;
                    }
                }
                else
                {
                    entrepreneur_public = DBNull.Value;
                }
                if (userdata.ContainsKey("userIndexData.InvestorProfile[Profile_Public]"))
                {
                    if (userdata["userIndexData.InvestorProfile[Profile_Public]"] == "on")
                    {
                        investor_public = true;
                    }
                    else
                    {
                        investor_public = false;
                    }
                }
                else
                {
                    investor_public = DBNull.Value;
                }
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@User_Id", userdata["userIndexData.UserProfile[Id]"]);
                command.Parameters.AddWithValue("@Entrepreneur_Id", int.Parse(userdata["userIndexData.EntrepreneurProfile[Entrepreneur_Id]"]));
                command.Parameters.AddWithValue("@Investor_Id", int.Parse(userdata["userIndexData.InvestorProfile[Investor_Id]"]));
                command.Parameters.AddWithValue("@Name", userdata["userIndexData.UserProfile[Name]"]);
                command.Parameters.AddWithValue("@PhoneNumber", userdata["userIndexData.UserProfile[PhoneNumber]"]);
                command.Parameters.AddWithValue("@Entrepreneur_Public", entrepreneur_public);
                command.Parameters.AddWithValue("@Investor_Public", investor_public);
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
            }
            return result;
        }
        #endregion

        #region Entrepreneur operations
        public EntrepreneurDetailsComposite get_EntrepreneurDetails_Data(int Entrepreneur_Id)
        {
            DataSet set = new DataSet();
            EntrepreneurDetailsComposite entrepreneur = new EntrepreneurDetailsComposite();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "usp_Select_EntrepreneurDetails";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@Entrepreneur_Id", Entrepreneur_Id);
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
                //SELECT the Entrepreneur profile data
                //SELECT the Entrepreneur Documents
                //SELECT the Entrepreneur Links
                //SELECT the Entrepreneur Embelishments
                //SELECT the data of each project account for this entrepreneur that is public
                //SELECT the PROCs this Entrepreneurs has

                entrepreneur.EntrepreneurDetails = get_DictionaryObject(set.Tables[0].Rows[0], set.Tables[0].Columns);
                entrepreneur.EntrepreneurDocuments = get_indexed_MetaDictionary(set.Tables[1].Rows, set.Tables[1].Columns);
                entrepreneur.EntrepreneurLinks = get_indexed_MetaDictionary(set.Tables[2].Rows, set.Tables[2].Columns);
                entrepreneur.EntrepreneurEmbelishments = get_indexed_MetaDictionary(set.Tables[3].Rows, set.Tables[3].Columns);
                entrepreneur.EntrepreneurProjects = get_indexed_MetaDictionary(set.Tables[4].Rows, set.Tables[4].Columns);
                entrepreneur.EntrepreneurPROCS = get_indexed_MetaDictionary(set.Tables[5].Rows, set.Tables[5].Columns);
                return entrepreneur;
            }
        }

        public EntrepreneurIndexComposite get_EntrepreneurIndexData(string UserId, int entrepreneur_Id)
        {
            DataSet set = new DataSet();
            EntrepreneurIndexComposite entrepreneur = new EntrepreneurIndexComposite();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "usp_Select_EntrepreneurIndex";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@User_Id", UserId);
                adapter.SelectCommand.Parameters.AddWithValue("@Entrepreneur_Id", entrepreneur_Id);
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
                //SELECT the Entrepreneur profile data
                //SELECT the Entrepreneur Documents
                //SELECT the Entrepreneur Links
                //SELECT the Entrepreneur Embelishments
                //SELECT the data of each project account for this entrepreneur

                entrepreneur.EntrepreneurProfile = get_DictionaryObject(set.Tables[0].Rows[0], set.Tables[0].Columns);
                entrepreneur.EntrepreneurDocuments = get_indexed_MetaDictionary(set.Tables[1].Rows, set.Tables[1].Columns);
                entrepreneur.EntrepreneurLinks = get_indexed_MetaDictionary(set.Tables[2].Rows, set.Tables[2].Columns);
                entrepreneur.EntrepreneurEmbelishments = get_indexed_MetaDictionary(set.Tables[3].Rows, set.Tables[3].Columns);
                entrepreneur.EntrepreneurProjects = get_indexed_MetaDictionary(set.Tables[4].Rows, set.Tables[4].Columns);
                return entrepreneur;
            }
        }
        #endregion

        #region Investor operations
        public InvestorDetailsComposite get_InvestorDetails_Data(int Investor_Id)
        {
            DataSet set = new DataSet();
            InvestorDetailsComposite investor = new InvestorDetailsComposite();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "usp_Select_InvestorsDetails";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@Investor_Id", Investor_Id);
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
                //SELECT the Investor profile data
                //SELECT the Investor Documents
                //SELECT the Investor Links
                //SELECT the Investor Embelishments
                //SELECT the investor's PROCs

                investor.InvestorDetails = get_DictionaryObject(set.Tables[0].Rows[0], set.Tables[0].Columns);
                investor.InvestorDocuments = get_indexed_MetaDictionary(set.Tables[1].Rows, set.Tables[1].Columns);
                investor.InvestorLinks = get_indexed_MetaDictionary(set.Tables[2].Rows, set.Tables[2].Columns);
                investor.InvestorEmbelishments = get_indexed_MetaDictionary(set.Tables[3].Rows, set.Tables[3].Columns);
                investor.InvestorPROCs = get_indexed_MetaDictionary(set.Tables[4].Rows, set.Tables[4].Columns);
                return investor;
            }
        }

        public InvestorIndexComposite get_InvestorIndexData(string UserId, int investor_Id)
        {
            DataSet set = new DataSet();
            InvestorIndexComposite investor = new InvestorIndexComposite();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "usp_Select_InvestorIndex";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@User_Id", UserId);
                adapter.SelectCommand.Parameters.AddWithValue("@Investor_Id", investor_Id);
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
                //SELECT the Investor profile data
                //SELECT the Investor Documents
                //SELECT the Investor Links
                //SELECT the Investor Embelishments
                //SELECT the investor's PROCs
                //SELECT the Investor's Financial account details
                //SELECT the Investor's account Id

                investor.InvestorProfile = get_DictionaryObject(set.Tables[0].Rows[0], set.Tables[0].Columns);
                investor.InvestorDocuments = get_indexed_MetaDictionary(set.Tables[1].Rows, set.Tables[1].Columns);
                investor.InvestorLinks = get_indexed_MetaDictionary(set.Tables[2].Rows, set.Tables[2].Columns);
                investor.InvestorEmbelishments = get_indexed_MetaDictionary(set.Tables[3].Rows, set.Tables[3].Columns);
                investor.InvestorePROCs = get_indexed_MetaDictionary(set.Tables[4].Rows, set.Tables[4].Columns);
                if (set.Tables[5].Rows.Count > 0)
                {
                    investor.InvestorAccount = get_DictionaryObject(set.Tables[5].Rows[0], set.Tables[5].Columns);
                }
                investor.InvestorAccount_Id = (int)set.Tables[6].Rows[0]["Account_Id"];
                return investor;
            }
        }
        #endregion

        #region project operations
        public ProjectDetailsComposite get_ProjectDetails_Data(int Project_Id)
        {
            DataSet set = new DataSet();
            ProjectDetailsComposite project = new ProjectDetailsComposite();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "usp_Select_ProjectDetails";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@Project_Id", Project_Id);
                try
                {
                    conn.Open();
                    adapter.Fill(set);
                }
                catch (Exception e)
                {
                    return project;
                }
                finally
                {
                    conn.Close();
                }
                //ProjectProfile
                //ProjectDocuments
                //ProjectImages
                //ProjectPROCs
                //ProjectLinks
                //ProjectEmbelishments

                project.ProjectDetails = get_DictionaryObject(set.Tables[0].Rows[0], set.Tables[0].Columns);
                project.ProjectDocuments = get_indexed_MetaDictionary(set.Tables[1].Rows, set.Tables[1].Columns);
                project.ProjectImages = get_indexed_MetaDictionary(set.Tables[2].Rows, set.Tables[2].Columns);
                project.ProjectPROCs = get_indexed_MetaDictionary(set.Tables[3].Rows, set.Tables[3].Columns);
                project.ProjectLinks = get_indexed_MetaDictionary(set.Tables[4].Rows, set.Tables[4].Columns);
                project.ProjectEmbelishments = get_indexed_MetaDictionary(set.Tables[5].Rows, set.Tables[5].Columns);
                return project;
            }
        }

        public int create_Project(string UserId, int Entrepreneur_Id, Dictionary<string, string> projectdata)
        {
            int project_id = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "usp_Project_Insert";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@User_Id", UserId);
                command.Parameters.AddWithValue("@Entrepreneur_Id", Entrepreneur_Id);
                command.Parameters.AddWithValue("@Name", projectdata["Project_Name"]);
                command.Parameters.AddWithValue("@Summary", projectdata["Project_Description"]);
                command.Parameters.AddWithValue("@Investment_Goal", decimal.Parse(projectdata["Investment_Amount"]));
                try
                {
                    conn.Open();
                    project_id = int.Parse(command.ExecuteScalar().ToString());
                }
                catch (Exception e)
                {

                }
                finally
                {
                    conn.Close();
                }
                return project_id;
            }
        }

        public ProjectIndexComposite get_ProjectIndexData(string UserId, int entrepreneur_id, int project_id)
        {
            DataSet set = new DataSet();
            ProjectIndexComposite project = new ProjectIndexComposite();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "usp_Select_ProjectIndex";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@User_Id", UserId);
                adapter.SelectCommand.Parameters.AddWithValue("@Entrepreneur_Id", entrepreneur_id);
                adapter.SelectCommand.Parameters.AddWithValue("@Project_Id", project_id);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
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
                // [0] : _ProjectProfile
                // [1] : _ProjectAccount
                // [2] : project account Id
                // [3] : _ProjectDocuments
                // [4] : _ProjectImages
                // [5] : _ProjectPROCs
                // [6] : _ProjectLinks
                // [7] : _ProjectEmbelishments

                project.ProjectProfile = get_DictionaryObject(set.Tables[0].Rows[0], set.Tables[0].Columns);
                if (set.Tables[1].Rows.Count > 0)
                {
                    project.ProjectAccount = get_DictionaryObject(set.Tables[1].Rows[0], set.Tables[1].Columns);
                }
                project.ProjectAccount_Int = (int)set.Tables[2].Rows[0]["Account_Id"];
                project.ProjectDocuments = get_indexed_MetaDictionary(set.Tables[3].Rows, set.Tables[3].Columns);
                project.ProjectImages = get_indexed_MetaDictionary(set.Tables[4].Rows, set.Tables[4].Columns);
                project.ProjectPROCs = get_indexed_MetaDictionary(set.Tables[5].Rows, set.Tables[5].Columns);
                project.ProjectLinks = get_indexed_MetaDictionary(set.Tables[6].Rows, set.Tables[6].Columns);
                project.ProjectEmbelishments = get_indexed_MetaDictionary(set.Tables[7].Rows, set.Tables[7].Columns);
                return project;
            }
        }

        public bool update_ProjectProfileData(string UserId, int Entrepreneur_Id, int Project_Id, Dictionary<string, string> projectdata)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "usp_update_Project";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@User_Id", UserId);
                command.Parameters.AddWithValue("@Entrepreneur_Id", Entrepreneur_Id);
                command.Parameters.AddWithValue("@Project_Id", Project_Id);
                if (projectdata.ContainsKey("Project_Name"))
                {
                    command.Parameters.AddWithValue("@Name", projectdata["Project_Name"]);
                }
                else
                {
                    command.Parameters.AddWithValue("@Name", DBNull.Value);
                }
                if (projectdata.ContainsKey("Project_Summary"))
                {
                    command.Parameters.AddWithValue("@Summary", projectdata["Project_Summary"]);
                }
                else
                {
                    command.Parameters.AddWithValue("@Summary", DBNull.Value);
                }
                if (projectdata.ContainsKey("Project_Public"))
                {
                    if (projectdata["Project_Public"] == "on")
                    {
                        command.Parameters.AddWithValue("@Profile_Public", true);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Profile_Public", false);
                    }
                }
                else
                {
                    command.Parameters.AddWithValue("@Profile_Public", DBNull.Value);
                }
                if (projectdata.ContainsKey("Project_Investment_Goal"))
                {
                    command.Parameters.AddWithValue("@Investment_Goal", projectdata["Project_Investment_Goal"]);
                }
                else
                {
                    command.Parameters.AddWithValue("@Investment_Goal", DBNull.Value);
                }
                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception e)
                {
                    result = false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }
        #endregion

        #region PROC operations
        public PROCComposite get_PROC(int proc_id, int entrepreneur_id = 0, int investor_id = 0)
        {
            DataSet set = new DataSet();
            PROCComposite PROC = new PROCComposite();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "usp_Select_PROCDetails";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@PROC_Id", proc_id);
                if (entrepreneur_id == 0)
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@Entrepreneur_Id", DBNull.Value);
                }
                else
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@Entrepreneur_Id", entrepreneur_id);
                }
                if (investor_id == 0)
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@Investor_Id", DBNull.Value);
                }
                else
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@Investor_Id", investor_id);
                }
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
                // [1] : PROC Details
                // [2] : Who is owner [[investor],[entrepreneur]]

                PROC.PROC = get_DictionaryObject(set.Tables[0].Rows[0], set.Tables[0].Columns);
                if (set.Tables[1].Rows[0][0].ToString() == "True")
                {
                    PROC.InvestorOwner = true;
                }
                else
                {
                    PROC.InvestorOwner = false;
                }
                if (set.Tables[1].Rows[0][1].ToString() == "True")
                {
                    PROC.EntrepreneurOwner = true;
                }
                else
                {
                    PROC.EntrepreneurOwner = false;
                }
                return PROC;
            }
        }

        public bool update_PROC(int proc_id, Dictionary<string, string> updateDictionary, int entrepreneur_id = 0, int investor_id = 0)
        {
            DataSet set = new DataSet();
            bool result = false;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "usp_Update_PROC";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PROC_Id", proc_id);
                if (entrepreneur_id != 0)
                {
                    command.Parameters.AddWithValue("@Entrepreneur_Id", entrepreneur_id);
                }
                else
                {
                    command.Parameters.AddWithValue("@Entrepreneur_Id", DBNull.Value);
                }
                if (investor_id != 0)
                {
                    command.Parameters.AddWithValue("@Investor_Id", investor_id);
                }
                else
                {
                    command.Parameters.AddWithValue("@Investor_Id", DBNull.Value);
                }
                if (updateDictionary.ContainsKey("Investment_Amount"))
                {
                    command.Parameters.AddWithValue("@Investment_Amount", decimal.Parse(updateDictionary["Investment_Amount"]));
                }
                else
                {
                    command.Parameters.AddWithValue("@Investment_Amount", DBNull.Value);
                }
                if (updateDictionary.ContainsKey("Revenue_Percentage"))
                {
                    if (!string.IsNullOrEmpty(updateDictionary["Revenue_Percentage"]))
                    {
                        command.Parameters.AddWithValue("@Revenue_Percentage", decimal.Parse(updateDictionary["Revenue_Percentage"]));
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Revenue_Percentage", DBNull.Value);
                    }
                }
                else
                {
                    command.Parameters.AddWithValue("@Revenue_Percentage", DBNull.Value);
                }
                if (updateDictionary.ContainsKey("date_begin"))
                {
                    command.Parameters.AddWithValue("@DateTime_Begin", DateTime.Parse(updateDictionary["date_begin"]));
                }
                else
                {
                    command.Parameters.AddWithValue("@DateTime_Begin", DBNull.Value);
                }
                if (updateDictionary.ContainsKey("date_end"))
                {
                    command.Parameters.AddWithValue("@DateTime_End", DateTime.Parse(updateDictionary["date_end"]));
                }
                else
                {
                    command.Parameters.AddWithValue("@DateTime_End", DBNull.Value);
                }
                if (updateDictionary.ContainsKey("User_Accepts_PROC"))
                {
                    if (updateDictionary["User_Accepts_PROC"] == "on")
                    {
                        command.Parameters.AddWithValue("@User_Accepts_PROC", true);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@User_Accepts_PROC", false);
                    }
                }
                else
                {
                    command.Parameters.AddWithValue("@User_Accepts_PROC", DBNull.Value);
                }
                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception e)
                {
                    result = false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        public int create_PROC(string UserId, int Investor_Id, int Project_Id, Dictionary<string, string> PROCdata)
        {
            int proc_id = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "usp_PROC_Insert";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@User_Id", UserId);
                command.Parameters.AddWithValue("@Investor_Id", Investor_Id);
                command.Parameters.AddWithValue("@Project_Id", Project_Id);
                if (PROCdata.ContainsKey("Investment_Amount"))
                {
                    command.Parameters.AddWithValue("@Investment_Amount", PROCdata["Investment_Amount"]);
                }
                else
                {
                    throw new Exception("You must enter an invesment amoount of capital");
                }
                if (PROCdata.ContainsKey("Revenue_Percentage"))
                {
                    command.Parameters.AddWithValue("@Revenue_Percentage", PROCdata["Revenue_Percentage"]);
                }
                else
                {
                    throw new Exception("You must enter a return on capital percentage");
                }
                if (PROCdata.ContainsKey("date_end"))
                {
                    command.Parameters.AddWithValue("@DateTime_End", PROCdata["date_end"]);
                }
                else
                {
                    throw new Exception("You must enter an end-date");
                }
                if (PROCdata.ContainsKey("date_begin"))
                {
                    command.Parameters.AddWithValue("@DateTime_Begin", PROCdata["date_begin"]);
                }
                else
                {
                    throw new Exception("You must enter a start-date");
                }
                try
                {
                    conn.Open();
                    proc_id = int.Parse(command.ExecuteScalar().ToString());
                }
                catch (Exception e)
                {

                }
                finally
                {
                    conn.Close();
                }

            }
            return proc_id;
        }

        public bool alter_PROC_MutualyAccepted(int proc_id, bool investor_mutualy_accepts_PROC, int entrepreneur_id = 0, int investor_id = 0)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "usp_Update_PROC";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PROC_Id", proc_id);
                if (investor_mutualy_accepts_PROC)
                {
                    command.Parameters.AddWithValue("@Investor_Activates_Mutual_Acceptance", investor_mutualy_accepts_PROC);
                }
                if (entrepreneur_id != 0)
                {
                    command.Parameters.AddWithValue("@Entrepreneur_Id", entrepreneur_id);
                }
                else
                {
                    command.Parameters.AddWithValue("@Entrepreneur_Id", DBNull.Value);
                }
                if (investor_id != 0)
                {
                    command.Parameters.AddWithValue("@Investor_Id", investor_id);
                }
                else
                {
                    command.Parameters.AddWithValue("@Investor_Id", DBNull.Value);
                }
                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception e)
                {
                    result = false;
                }
                finally
                {
                    conn.Close();
                }
                return result;
            }
        }

        public bool recant_PROC_MutualAcceptance(int proc_id, bool mutual_acceptance, int entrepreneur_id, int investor_id)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "usp_Recant_PROC_acceptance";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PROC_Id", proc_id);
                command.Parameters.AddWithValue("@Entrepreneur_Id", entrepreneur_id);
                command.Parameters.AddWithValue("@Investor_Id", investor_id);
                command.Parameters.AddWithValue("@MutualAcceptance", mutual_acceptance);
                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception e)
                {
                    result = false;
                }
                finally
                {
                    conn.Close();
                }
                return result;
            }
        }
        #endregion

        #region Link operations
        public bool upload_Link(Dictionary<string, string> linkDictionary)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "usp_Insert_Link";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                if(linkDictionary.ContainsKey("Profile_Id"))
                {
                    command.Parameters.AddWithValue("@Profile_Id", linkDictionary["Profile_Id"]);
                }
                if(linkDictionary.ContainsKey("Is_Project"))
                {
                    command.Parameters.AddWithValue("@Is_Project", linkDictionary["Is_Project"]);
                }
                if(linkDictionary.ContainsKey("Link"))
                {
                    command.Parameters.AddWithValue("@Link", linkDictionary["Link"]);
                }
                if(linkDictionary.ContainsKey("Link_Type"))
                {
                    command.Parameters.AddWithValue("@Link_Type", linkDictionary["Link_Type"]);
                }
                if(linkDictionary.ContainsKey("FriendlyName"))
                {
                    command.Parameters.AddWithValue("@FriendlyName", linkDictionary["FriendlyName"]);
                }
                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception e)
                {
                    result = false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        // The client can just delte the link
        //public bool update_Link(Dictionary<string, string> link)
        //{
        //    // TODO
        //    return false;
        //}

        public bool delete_Link(int link_id, int profile_id, int profile_type_id)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDataBase"].ConnectionString))
            {
                string query = "usp_Delete_Link";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Link_Id", link_id);
                command.Parameters.AddWithValue("@Profile_Id", profile_id);
                command.Parameters.AddWithValue("@Profile_Type_Id", profile_type_id);
                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception e)
                {
                    result = false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }
        #endregion

        //#region Embelishment operations
        //public bool upload_Embelishment(Dictionary<string, string> embelishment)
        //{
        //    // TODO
        //    return false;
        //}

        //public bool update_Embelishment(Dictionary<string, string> embelishment)
        //{
        //    // TODO
        //    return false;
        //}

        //public bool delete_Embelishment(int embelishment_Id, int profile_id, int profile_type)
        //{
        //    // TODO
        //    return false;
        //}
        //#endregion

        #region helper methods.
        /// <summary>
        /// Helper method that will turn a data row into a dictionary object.
        /// 
        /// This method is prepared to handle the event when the column name is Binary_Image and 
        /// 
        /// transforms that binary image to the base64 string neccesary to be rendered in the views page for the web application.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="colls"></param>
        /// <returns></returns>
        private Dictionary<string, string> get_DictionaryObject(DataRow row, DataColumnCollection colls)
        {
            Dictionary<string, string> entity = new Dictionary<string, string>();
            for(int i = 0; i< row.ItemArray.Count(); i++)
            {
                if(row[colls[i]] == DBNull.Value)
                {
                    entity.Add(colls[i].ColumnName, null);
                }
                else
                {
                    // handle instance when column name is Binary_Image as this field is a byte[] that needs to be converted into a base64 version of that byte[].
                    if (colls[i].ColumnName == "Binary_Image")
                    {
                        entity.Add("Base64Image", string.Format("data:image/gif;base64,{0}", Convert.ToBase64String((byte[])row[colls[i]])));
                    }
                    else if (colls[i].ColumnName == "Entrepreneur_Binary_Image")
                    {
                        entity.Add("Entrepreneur_Base64Image", string.Format("data:image/gif;base64,{0}", Convert.ToBase64String((byte[])row[colls[i]])));
                    }
                    else if (colls[i].ColumnName == "Investor_Binary_Image")
                    {
                        entity.Add("Investor_Base64Image", string.Format("data:image/gif;base64,{0}", Convert.ToBase64String((byte[])row[colls[i]])));
                    }
                    else if(colls[i].ColumnName == "Binary_File")
                    {
                        entity.Add("Base64_Encoded_File", Convert.ToBase64String((byte[])row[colls[i]]));
                    }
                    else
                    {
                        entity.Add(colls[i].ColumnName, row[colls[i]].ToString());
                    }
                }
            }
            return entity;
        }
        /// <summary>
        /// Helper method that will turn a data row into a dictionary object.
        /// 
        /// This method will retrive entries as objects to be able to pass document data to the client.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="colls"></param>
        /// <returns></returns>
        private Dictionary<string, object> get_DictionaryFile(DataRow row, DataColumnCollection colls)
        {
            Dictionary<string, object> entity = new Dictionary<string, object>();
            for (int i = 0; i < row.ItemArray.Count(); i++)
            {
                if (row[colls[i]] == DBNull.Value)
                {
                    entity.Add(colls[i].ColumnName, null);
                }
                else
                {
                    entity.Add(colls[i].ColumnName, row[colls[i]].ToString());
                }
            }
            return entity;
        }
        /// <summary>
        /// Helper method that will turn a data row collections into an indexed dictionary of dictionary that have the keys and values of the data rows.
        ///
        /// This method is prepared to handle the event when the column name is Binary_Image and 
        /// 
        /// transforms that binary image to the base64 string neccesary to be rendered in the views page for the web application.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="colls"></param>
        /// <returns></returns>
        private Dictionary<int, Dictionary<string, string>> get_indexed_MetaDictionary(DataRowCollection rows, DataColumnCollection colls)
        {
            Dictionary<int, Dictionary<string, string>> metaDictionary = new Dictionary<int, Dictionary<string, string>>();
            if(rows.Count > 0)
            {
                for(int i = 0; i < rows.Count; i++)
                {
                    Dictionary<string, string> row = new Dictionary<string, string>();
                    for(int k = 0; k < rows[i].ItemArray.Count(); k++)
                    {
                        if(rows[i][colls[k]] == DBNull.Value)
                        {
                            row.Add(colls[k].ColumnName, null);
                        }
                        else
                        {
                            // handle instance when column name is Binary_Image as this field is a byte[] that needs to be converted into a base64 version of that byte[].
                            if (colls[k].ColumnName == "Binary_Image")
                            {
                                row.Add("Base64Image", string.Format("data:image/gif;base64,{0}", Convert.ToBase64String((byte[])rows[i][colls[k]])));
                            }
                            else
                            {
                                row.Add(colls[k].ColumnName, rows[i][colls[k]].ToString());
                            }
                        }
                    }
                    metaDictionary.Add(i, row);
                }
            }
            return metaDictionary;
        }
        #endregion
    }
}
