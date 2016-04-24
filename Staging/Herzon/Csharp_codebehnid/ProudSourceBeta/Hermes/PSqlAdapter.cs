using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes
{
    class PSqlAdapter
    {

        private SqlConnection connection;

        /// <summary>
        /// This boolean will be constantly maintained to make sure that we
        /// always have a connection to the server.  If it is false, most
        /// functions should throw an exception.
        /// </summary>
        private bool isConnected { get; set; }

        /// <summary>
        /// The constructor for the class.  It sets the isConnected bool to
        /// false to initialize the class.  This will be set to true when we
        /// connect.
        /// </summary>
        public PSqlAdapter() {
            isConnected = false;
        }

        /// <summary>
        /// This should be called when the adapter is created.  It connects to
        /// the PSQL server, then checks to see if it is successful.
        /// 
        /// NOTE:  This may be a superfluous function -- We may be able to
        /// implement a handler to take care of state changes
        /// </summary>
        /// <param name="address">The address for the database</param>
        /// <returns>Whether the connection was successful</returns>
        public bool connect( String address )
        {
            // TODO: Implement event handler for state change with SqlConnection
            // TODO: Check to see if connection was successful
            connection = new SqlConnection(address);
            isConnected = true;
            return isConnected;
        }

        public DataSet Query(string query, CommandType commandType )
        {

            DataSet set = new DataSet();
            if (!isConnected) throw new MissingDatabaseConnection();
            // TODO: Stub

            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.SelectCommand.CommandType = commandType;

            try
            {
                connection.Open();
                adapter.Fill(set);
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught exception of type: " + e.GetType());
                Console.WriteLine(e.ToString());
                // TODO:  Figure out what exceptions this could catch.
            }
            finally
            {
                // Always close the connections that you open to the data base. 
                // If you don't you can cause the sql server to crash if too
                // many connections are opened and not closed.
                connection.Close();
            }

            return set;
        }

        /// <summary>
        /// Overload for default command type.
        /// </summary>
        /// <param name="query">The query to be executed on the database.</param>
        /// <returns></returns>
        public DataSet Query( string query )
        {
            return Query(query, CommandType.Text);
        }

        public bool Insert( string commandString, CommandType commandType, Dictionary<string, string> parameters )
        {
            if (!isConnected) throw new MissingDatabaseConnection();


            SqlCommand command = new SqlCommand(commandString, connection);
            command.CommandType = commandType;

            foreach( string key in parameters.Keys ) {
                string value = "";
                if (!parameters.TryGetValue(key, out value)) continue;
                command.Parameters.AddWithValue(key, value);
            }

            bool result = true;

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Caught exception of type: " + e.GetType());
                Console.WriteLine(e.ToString());
                result = false; // TODO:  does this happen before or after the finally
                // TODO:  Figure out what exceptions this could catch.
            }
            finally
            {
                connection.Close();
            }

            return result;  // TODO:  There may be a more correct way of this
        }

        public bool Insert( string commandString, Dictionary<string, string> parameters )
        {
            return Insert(commandString, CommandType.Text, parameters);
        }
    }
}
