using System;
using Npgsql;

namespace ProudSource
{
    public class PSUIQuery : IDisposable
    {
        protected NpgsqlConnection conn;

        public PSUIQuery()
        {
            conn = new NpgsqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ProudSourceDB"].ConnectionString);
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}