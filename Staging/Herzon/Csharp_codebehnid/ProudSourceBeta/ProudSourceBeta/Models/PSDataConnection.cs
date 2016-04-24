using System.Data.SqlClient;
using System.Data;
using System;

namespace ProudSourceBeta.Models
{
    public abstract class PSDataConnection
    {
        /// <summary>
        /// Connection object to be inherited by later types.
        /// </summary>
        protected SqlConnection conn;
        /// <summary>
        /// User identity
        /// </summary>
        public int User_Id { get; private set; }
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="UserIdentiy_id"></param>
        public PSDataConnection(string UserIdentiy_id)
        {
               conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceStaging"].ConnectionString);
            User_Id = get_userID_from_userIdentity(UserIdentiy_id);
        }
        /// <summary>
        /// Class constructor used when all that needs to be inherited is the sql connection and not the user's data.
        /// For use when the User is already known.
        /// </summary>
        public PSDataConnection()
        {
            conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceStaging"].ConnectionString);
        }
        /// <summary>
        /// This private methos is what fills out the accessors of this class with user data.
        /// </summary>
        /// <param name="userIdentity"></param>
        /// <returns></returns>
        private int get_userID_from_userIdentity(string userIdentity)
        {
            string query = "auth_User_ID_from_Identity_ID";
            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@userIdentity", userIdentity);
            int user_id = 0;
            try
            {
                conn.Open();
                user_id = Convert.ToInt32(command.ExecuteScalar().ToString());
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return user_id;
        }
    }
}