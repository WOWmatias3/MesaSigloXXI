using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UsuarioDAL
    {
        public int id_usuario { get; set; }
        public string nombre_usuario { get; set; }
        public string clave { get; set; }
        public string rol { get; set; }

        public UsuarioDAL()
        {
        }

        public UsuarioDAL(int id_usuario, string nombre_usuario, string clave, string rol)
        {
            this.id_usuario = id_usuario;
            this.nombre_usuario = nombre_usuario;
            this.clave = clave;
            this.rol = rol;
        }

        private string encriptador(string palabra)
        {
            SHA256 sha = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha.ComputeHash(encoding.GetBytes(palabra));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }



        public bool verificaUsuario(string username,string password)
        {
            using (OracleConnection con = new Conexion().conexion())
            {
                OracleCommand cm = new OracleCommand("get_userpass", con);
                cm.BindByName = true;
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter output = cm.Parameters.Add("my_cursor", OracleDbType.RefCursor);
                cm.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
                output.Direction = System.Data.ParameterDirection.ReturnValue;
                con.Open();
                cm.ExecuteNonQuery();

                OracleDataReader reader = ((OracleRefCursor)output.Value).GetDataReader();
                
                string encriptada = encriptador(password);
                while (reader.Read())
                {

                    clave = reader.GetString(2);
                    rol = reader.GetString(3);
                }
                con.Close();
                if (clave == encriptada && rol == "Garzon")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
   
        }
    }
}
