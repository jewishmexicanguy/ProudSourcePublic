using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ProudSourceBeta.HelperClasses
{
    public class ProudSourceData
    {
        private SqlConnection conn;

        public ProudSourceData()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProudSourceStaging"].ConnectionString);
        }

        public int Register_newUser(string AspNetUser_Id, string Email, string Name)
        {
            string query = "User_Create";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Email", Email);
            adapter.SelectCommand.Parameters.AddWithValue("@AspNetUser_ID", AspNetUser_Id);
            adapter.SelectCommand.Parameters.AddWithValue("@Name", Name);
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
            return (int)set.Tables[0].Rows[0][0];
        }

        public int Create_newInvestor(int User_ID, string Name)
        {
            string query = "Investor_Create";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@User_ID", User_ID);
            adapter.SelectCommand.Parameters.AddWithValue("@Investor_Name", Name);
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
            return (int)set.Tables[0].Rows[0][0];
        }

        public bool Update_investorData(int Investor_ID, string name, bool profile_public, byte[] profile_picture_bytes)
        {
            string query = "Investor_Update";
            bool success = false;

            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Investor_ID", Investor_ID);

            if (name.Length <= 0)
            {
                command.Parameters.Add("@Name", SqlDbType.VarChar);
                command.Parameters["@Name"].Value = DBNull.Value;
            }
            else
            {
                command.Parameters.AddWithValue("@Name", name);
            }

            if (profile_public.Equals(null))
            {
                command.Parameters.Add("@profile_public", SqlDbType.Bit);
                command.Parameters["@profile_public"].Value = DBNull.Value;
            }
            else
            {
                command.Parameters.AddWithValue("@profile_public", profile_public);
            }

            try {
                if (profile_picture_bytes.Length <= 0)
                {
                    command.Parameters.Add("@profile_picture", SqlDbType.VarBinary);
                    command.Parameters["@profile_picture"].Value = DBNull.Value;
                }
                else
                {
                    command.Parameters.AddWithValue("@profile_picture", profile_picture_bytes);
                }
            }
            catch (NullReferenceException)
            {
                command.Parameters.Add("@profile_picture", SqlDbType.VarBinary);
                command.Parameters["@profile_picture"].Value = DBNull.Value;
            }

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