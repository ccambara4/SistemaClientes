using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;

namespace SistemaClientes.Clases
{
    public class Conexion
    {
        private static string cadenaConexion =
            ConfigurationManager.ConnectionStrings["ClientesDB"].ConnectionString;

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}
