using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;



namespace DAL
{
    public class Conexion
    {
        public OracleConnection conexion() {

            String mycon = "Data Source=localhost:1521/orcl;User ID=restaurant;Password=restaurant;";
            OracleConnection con = new OracleConnection(mycon);
            return con;
        }
    }
}
