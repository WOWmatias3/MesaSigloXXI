using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrdenDAL
    {
        public int id_orden { get; set; }
        public int id_boleta { get; set; }
        public DateTime fecha_orden { get; set; }

        public OrdenDAL()
        {
        }

        public OrdenDAL(int id_orden, int id_boleta, DateTime fecha_orden)
        {
            this.id_orden = id_orden;
            this.id_boleta = id_boleta;
            this.fecha_orden = fecha_orden;
        }

        public bool CreaOrden(int num_boleta)
        {
            try
            {
                using (OracleConnection con = new Conexion().conexion())
                {
                    OracleCommand cm = new OracleCommand("add_orden", con);
                    cm.BindByName = true;
                    cm.CommandType = System.Data.CommandType.StoredProcedure;

                    cm.Parameters.Add("id_boleta", OracleDbType.Int32).Value = num_boleta;
                    
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

        public bool test(int num_boleta)
        {
            using (OracleConnection con = new Conexion().conexion())
            {
                OracleCommand cm = new OracleCommand("get_seq", con);
                cm.BindByName = true;
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter output = cm.Parameters.Add("my_cursor", OracleDbType.Int32);
                output.Direction = System.Data.ParameterDirection.ReturnValue;
                con.Open();
                cm.ExecuteNonQuery();

                con.Close();
                Console.Write(""+output.Value);
                return true;

            }
        }

        public DataTable GetAllOrdenesByBoleta(int numBoleta)
        {
            using (OracleConnection con = new Conexion().conexion())
            {
                OracleCommand cm = new OracleCommand("GetAllDetalleByBoleta", con);
                cm.BindByName = true;
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter output = cm.Parameters.Add("my_cursor", OracleDbType.RefCursor);
                cm.Parameters.Add("num_boleta", OracleDbType.Int32).Value = numBoleta;
                output.Direction = System.Data.ParameterDirection.ReturnValue;
                con.Open();
                cm.ExecuteNonQuery();

                OracleDataReader reader = ((OracleRefCursor)output.Value).GetDataReader();
                con.Close();

                DataTable dt = new DataTable();

                OracleDataAdapter adapter = new OracleDataAdapter(cm);
                adapter.Fill(dt);
                return dt;
            }
        }

    }
}
