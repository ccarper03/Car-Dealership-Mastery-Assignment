using System.Configuration;

namespace GuildCars.Data
{
    public class Settings
    {
        private static string ConnectionString { get; set; }
        private static string RepositoryType { get; set; }

        public static string GetConnectionString()
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }
            return ConnectionString;
        }

        public static string GetRepositoryType()
        {
            if (string.IsNullOrEmpty(RepositoryType))
            {
                RepositoryType = ConfigurationManager.AppSettings["Mode"].ToString();
            }

            return RepositoryType;
        }
    }
}
