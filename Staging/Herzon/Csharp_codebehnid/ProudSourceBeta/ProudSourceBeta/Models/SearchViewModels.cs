using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ProudSourceBeta.Models
{
    public class SearchIndexViewModel : PSDataConnection
    {
        /// <summary>
        /// Accessor that contains rows returned from projects that are gathered by trending search.
        /// </summary>
        public List<DataRow> Project_Trending_Results { get; set; }
        /// <summary>
        /// A list of rows returned from a query that gets images for project along with the project key and image id
        /// </summary>
        public List<DataRow> Project_Images_Result { get; set; }
        /// <summary>
        /// Constructor for this class that fills the public accessors of this object with data on trending projects
        /// </summary>
        public SearchIndexViewModel() : base()
        {
            Project_Trending_Results = new List<DataRow>();
            Project_Images_Result = new List<DataRow>();
            _get_Trends();
        }
        /// <summary>
        /// Private method that intiates getting the results of project trends for this object.
        /// </summary>
        private void _get_Trends()
        {
            string query = "sp_get_Trending_Projects";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet set = new DataSet();
            try
            {
                conn.Open();
                adapter.Fill(set);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            if(set.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow i in set.Tables[0].Rows)
                {
                    Project_Trending_Results.Add(i);
                    DataTable table = _get_Project_Images((int)i["Project_ID"]);
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow k in table.Rows)
                        {
                            Project_Images_Result.Add(k);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Method that gets an individual project accounts data to display as results.
        /// </summary>
        /// <param name="project_id"></param>
        /// <returns></returns>
        private DataTable _get_Project_Images(int project_id)
        {
            string query = "sp_get_Project_Images";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Project_ID", project_id);
            DataSet set = new DataSet();
            try
            {
                conn.Open();
                adapter.Fill(set);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            if (set.Tables[0].Rows.Count < 1)
                return new DataTable();
            else
            {
                return set.Tables[0];
            }
        }
    }
    public class SearchKeyArgViewModel : PSDataConnection
    {
        /// <summary>
        /// DataRow accessor that exposes that houses the project reults that will be returned by our query.
        /// </summary>
        public List<DataRow> Project_Results { get; set; }
        /// <summary>
        /// DataRow accessor that exposes that houses the Images that Projects may have.
        /// </summary>
        public List<DataRow> Project_Images_Results { get; set; }
        /// <summary>
        /// DataRow accessor that exposes that houses results returned for Entrepreneur accounts.
        /// </summary>
        public List<DataRow> Entrepreneur_Results { get; set; }
        /// <summary>
        /// Currently Entrepreneurs are meant to have only one picture however if the need arises in the future this line can be uncommented and functionality to retrive multiple images added.
        /// </summary>
        // public List<DataRow> Entrepreneur_Image_Results { get; set; }
        /// <summary>
        /// DataRow accessor that exposes that houses results returned for Investor accounts.
        /// </summary>
        public List<DataRow> Investor_Results { get; set; }
        /// <summary>
        /// Currently Investors are meant to have only one picture however if the need arises in the future this line can be uncommented and functionality to retrive multiple images added.
        /// </summary>
        // public List<DataRow> Investor_Image_Results { get; set; }
        /// <summary>
        /// Class contrusctor that accepts one argument and will populate this objects accessors with results based off of the input string.
        /// </summary>
        /// <param name="KeyArg"></param>
        public SearchKeyArgViewModel(string KeyArg) : base()
        {
            Project_Results = new List<DataRow>();
            Project_Images_Results = new List<DataRow>();
            Entrepreneur_Results = new List<DataRow>();
            //Entrepreneur_Image_Results = new List<DataRow>();
            Investor_Results = new List<DataRow>();
            //Investor_Image_Results = new List<DataRow>();
            _QuerySearchKeyArg(KeyArg);
        }
        /// <summary>
        /// Private method that runs through and fills our accessors with results if any are found.
        /// </summary>
        /// <param name="KeyArg"></param>
        private void _QuerySearchKeyArg(string KeyArg)
        {
            // TODO: we could do some kind of string key argument cheking here.
            get_Project_by_KeyArg(KeyArg);
            get_Entrepreneur_by_KeyArg(KeyArg);
            get_Investor_by_KeyArg(KeyArg);
        }
        /// <summary>
        /// Private method that gets our projects that will meet our search criteria.
        /// </summary>
        /// <param name="KeyArg"></param>
        private void get_Project_by_KeyArg(string KeyArg)
        {
            // Prepare our key argument by adding % to the end and to the begining.
            KeyArg = "%" + KeyArg + "%";
            string query = @"SELECT P.[Project_ID], P.[Name], P.[Description], P.[Investment_Goal], E.[Entrepreneur_ID], E.[Name] AS 'Entrepreneur_Name'
                             FROM Projects P
                             JOIN Entrepreneurs E ON P.[Entrepreneur_ID] = E.[Entrepreneur_ID]
                             WHERE P.[Name] LIKE @KeyArg
                             AND P.[Profile_Public] = 'TRUE'";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.Text;
            adapter.SelectCommand.Parameters.AddWithValue("@KeyArg", KeyArg);
            DataSet set = new DataSet();
            try
            {
                conn.Open();
                adapter.Fill(set);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            if (set.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow i in set.Tables[0].Rows)
                {
                    Project_Results.Add(i);
                    DataTable table = _get_Project_Images(int.Parse(i["Project_ID"].ToString()));
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow k in table.Rows)
                        {
                            Project_Images_Results.Add(k);
                        }
                    }
                }
            }
            return;
        }
        /// <summary>
        /// Method that gets an individual project accounts data to display as results.
        /// </summary>
        /// <param name="project_id"></param>
        /// <returns></returns>
        private DataTable _get_Project_Images(int project_id)
        {
            string query = "sp_get_Project_Images";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Project_ID", project_id);
            DataSet set = new DataSet();
            try
            {
                conn.Open();
                adapter.Fill(set);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            if (set.Tables[0].Rows.Count < 1)
                return new DataTable();
            else
            {
                return set.Tables[0];
            }
        }
        /// <summary>
        /// Private methods that fills our accessor with resluts for Entrepreneurs that match our search criteria.
        /// </summary>
        /// <param name="KeyArg"></param>
        private void get_Entrepreneur_by_KeyArg(string KeyArg)
        {
            // Prepare our key argument by adding % to the end and to the begining.
            KeyArg = "%" + KeyArg + "%";
            string query = @"SELECT E.Entrepreneur_ID, E.Name, I.[Binary_Image]
                            FROM Entrepreneurs E
                            JOIN Images_Entrepreneurs_XREF IE_XREF ON E.[Entrepreneur_ID] = IE_XREF.[Entrepreneur_ID]
                            JOIN Images I ON IE_XREF.[Image_ID] = I.[Image_ID]
                            WHERE E.[Name] LIKE @KeyArg
                            AND E.[Profile_Public] = 'TRUE'";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.Text;
            adapter.SelectCommand.Parameters.AddWithValue("@KeyArg", KeyArg);
            DataSet set = new DataSet();
            try
            {
                conn.Open();
                adapter.Fill(set);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            if (set.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow i in set.Tables[0].Rows)
                {
                    Entrepreneur_Results.Add(i);
                }
            }
            return;
        }
        /// <summary>
        /// Private method that gets our investor results that match our search criteria.
        /// </summary>
        /// <param name="KeyArg"></param>
        private void get_Investor_by_KeyArg(string KeyArg)
        {
            // Prepare our key argument by adding % to the end and to the begining.
            KeyArg = "%" + KeyArg + "%";
            string query = @"SELECT I.[Investor_ID], I.[Name], IM.[Binary_Image]
                             FROM Investors I
                             JOIN Images_Investors_XREF II_XREF ON I.[Investor_ID] = II_XREF.[Investor_ID]
                             JOIN Images IM ON II_XREF.[Image_ID] = IM.[Image_ID]
                             WHERE I.[Name] LIKE @KeyArg
                             AND I.[Profile_Public] = 'TRUE'";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.CommandType = CommandType.Text;
            adapter.SelectCommand.Parameters.AddWithValue("@KeyArg", KeyArg);
            DataSet set = new DataSet();
            try
            {
                conn.Open();
                adapter.Fill(set);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            if (set.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow i in set.Tables[0].Rows)
                {
                    Investor_Results.Add(i);
                }
            }
            return;
        }
    }
}