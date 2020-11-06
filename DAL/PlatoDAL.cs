using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PlatoDAL
    {
        public int id_plato { get; set; }
        public string nombre_plato { get; set; }
        public int precio { get; set; }
        public string categoria { get; set; }
        public string Habilitado { get; set; }
        public byte[] imagen { get; set; }

        public PlatoDAL()
        {
        }

        public PlatoDAL(int id_plato, string nombre_plato, int precio, string categoria, string habilitado, byte[] imagen)
        {
            this.id_plato = id_plato;
            this.nombre_plato = nombre_plato;
            this.precio = precio;
            this.categoria = categoria;
            Habilitado = habilitado;
            this.imagen = imagen;
        }

        public bool ingresaRelacion(int id_plato,int cantidad)
        {
            try
            {
                OracleConnection con = new Conexion().conexion();
                
                OracleCommand cm = new OracleCommand("add_ordenPlato", con);
                cm.BindByName = true;
                cm.CommandType = System.Data.CommandType.StoredProcedure;

                cm.Parameters.Add("id_pla", OracleDbType.Int32).Value = id_plato;
                cm.Parameters.Add("cantidad", OracleDbType.Int32).Value = cantidad;

                con.Open();
                cm.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public int verificaStock(int idplato)
        {
            try
            {
                using (OracleConnection con = new Conexion().conexion())
                {
                    OracleCommand cm = new OracleCommand("VerificaStockPlato", con);
                    cm.BindByName = true;
                    cm.CommandType = System.Data.CommandType.StoredProcedure;

                    cm.Parameters.Add("idplato", OracleDbType.Int32).Value = idplato;

                    OracleParameter output = cm.Parameters.Add("my_cursor", OracleDbType.Int32);
                    output.Direction = System.Data.ParameterDirection.ReturnValue;
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    return Int32.Parse(output.Value.ToString());
                }

            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public bool reduceStock(int id_plato, int cantidad)
        {
            try
            {
                using (OracleConnection con = new Conexion().conexion())
                {
                    OracleCommand cm = new OracleCommand("reduce_stock", con);
                    cm.BindByName = true;
                    cm.CommandType = System.Data.CommandType.StoredProcedure;

                    cm.Parameters.Add("id_pla", OracleDbType.Int32).Value = id_plato;
                    cm.Parameters.Add("cantidad", OracleDbType.Int32).Value = cantidad;

                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

       

    }
}
