using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BebestibleDAL
    {
        public int id_bebestible { get; set; }
        public string nom_bebestible { get; set; }
        public string marca { get; set; }
        public int precio { get; set; }
        public byte[] imagen { get; set; }
        public string habilitado { get; set; }
        public int stock { get; set; }

        public BebestibleDAL()
        {
        }

        public BebestibleDAL(int id_bebestible, string nom_bebestible, string marca, int precio, byte[] imagen, string habilitado, int stock)
        {
            this.id_bebestible = id_bebestible;
            this.nom_bebestible = nom_bebestible;
            this.marca = marca;
            this.precio = precio;
            this.imagen = imagen;
            this.habilitado = habilitado;
            this.stock = stock;
        }

        public bool ingresaRelacion(int id_beb, int cantidad)
        {
            try
            {
                OracleConnection con = new Conexion().conexion();

                OracleCommand cm = new OracleCommand("add_ordenBebestible", con);
                cm.BindByName = true;
                cm.CommandType = System.Data.CommandType.StoredProcedure;

                cm.Parameters.Add("id_beb", OracleDbType.Int32).Value = id_beb;
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



        public int verificaStock(int idbeb)
        {
            try
            {
                using (OracleConnection con = new Conexion().conexion())
                {
                    OracleCommand cm = new OracleCommand("VerificaStockBebestible", con);
                    cm.BindByName = true;
                    cm.CommandType = System.Data.CommandType.StoredProcedure;

                    cm.Parameters.Add("idbeb", OracleDbType.Int32).Value = idbeb;

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

        public bool reduceStock(int idbeb, int cantidad)
        {
            try
            {
                using (OracleConnection con = new Conexion().conexion())
                {
                    OracleCommand cm = new OracleCommand("reduce_stock_bebestible", con);
                    cm.BindByName = true;
                    cm.CommandType = System.Data.CommandType.StoredProcedure;

                    cm.Parameters.Add("idbeb", OracleDbType.Int32).Value = idbeb;
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
