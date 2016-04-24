using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Web.Mvc;
using System;

namespace ProudSourceBeta.Models
{
    public class FileDownloadHelper : PSDataConnection
    {
        public FileDownloadHelper() : base()
        {

        }

        public DataSet get_FileData(int File_ID)
        {
            string query = @"SELECT D.[Document_ID], D.[Binary_File], D.[File_Name], D.[Mime_Type]
                             FROM Documents D
                             WHERE D.[Document_ID] = @Document_ID";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@Document_ID", File_ID);
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

    public class FileDownloadResult : ContentResult
    {
        public string fileName;

        public byte[] fileData;

        private DataSet File_Set;

        public string content_type;

        public FileDownloadResult(int file_id)
        { 
            File_Set = new FileDownloadHelper().get_FileData(file_id);
            get_file_data();
        }

        private void get_file_data()
        {
            fileName = File_Set.Tables[0].Rows[0]["File_Name"].ToString();
            content_type = File_Set.Tables[0].Rows[0]["Mime_Type"].ToString();
            fileData = (byte[])File_Set.Tables[0].Rows[0]["Binary_File"];
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if(string.IsNullOrEmpty(fileName))
            {
                throw new Exception("A file name is required");
            }

            if(fileData == null)
            {
                throw new Exception("There is no data!");
            }

            string contentDisposition = string.Format("attachment; filename={0}", fileName);
            context.HttpContext.Response.AddHeader("Content-Disposition", contentDisposition);
            ContentType = content_type;
            context.HttpContext.Response.BinaryWrite(fileData);
        }
    }
}