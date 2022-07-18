using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoApp.Conexion.Cloud
{
    public class Conexion
    {
        private readonly string _server;
        private readonly string _base;
        private readonly string _userId;
        private readonly string _password;
        public string conexion { get; set; }

        public Conexion()
        {
            _server = ConfigurationManager.AppSettings["server_cloud"];
            _base = ConfigurationManager.AppSettings["base_cloud"];
            _userId = ConfigurationManager.AppSettings["userId"];
            _password = ConfigurationManager.AppSettings["password"];

            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = _server,
                UserID = _userId,
                Password = _password,
                InitialCatalog = _base,
            };
            conexion = sqlConnectionStringBuilder.ConnectionString;
        }
    }
}
