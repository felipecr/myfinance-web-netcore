using System.Data;
using System.Data.SqlClient;

namespace myfinance_web_netcore.Infra {
    public class DAL {
        private SqlConnection Connection;
        private string ConnectionString;
        public static IConfiguration? Configuration;

        private static DAL? Instance;

        public static DAL GetInstance
        {
            get{
                if (Instance == null) {
                    Instance = new();
                }

                return Instance;
            }
        }

        public DAL()
        {
            ConnectionString = Configuration.GetValue<string>("ConnectionString");
            Connection = new();
            Connection.ConnectionString = ConnectionString;
        }

        public void Connect()
        { 
            if (Connection.State != ConnectionState.Open) 
            {
                Connection.Open();  
            }
        }

        public void Disconnect()
        {
            Connection.Close();
        }

        public DataTable Select(string sql)
        {
            DataTable dataTable = new();
            SqlDataAdapter adapter = new(sql, Connection);
            adapter.Fill(dataTable);

            return dataTable;
        }

        public void ExecuteCommand(string sql)
        {
            SqlCommand command = new(sql, Connection);
            command.ExecuteNonQuery();
        }
    }
}