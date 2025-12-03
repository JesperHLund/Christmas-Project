using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ChristmasBackend.Infrastructure.Data
{
    public class DBConnection : DbContext
    {
        public DBConnection(DbContextOptions<DBConnection> options) : base(options)
        {
        }

        // DbSets for entities
        public DbSet<Animation> Animations { get; set; }

        // Open and return the underlying ADO.NET connection (returns null on error)
        public DbConnection OpenConnection()
        {
            var conn = Database.GetDbConnection();
            try
            {
                if (conn != null && conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }
                return conn;
            }
            catch
            {
                return null;
            }
        }

        // Close the underlying ADO.NET connection
        public void CloseConnection()
        {
            var conn = Database.GetDbConnection();
            try
            {
                if (conn != null && conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
            catch
            {
                // swallow or log as appropriate
            }
        }
    }
}
