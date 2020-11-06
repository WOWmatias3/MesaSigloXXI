using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace DAL
{
    public class BoletaDAL
    {
        public int id_boleta { get; set; }
        public int fecha_boleta { get; set; }
        public int Medio_pago { get; set; }
        public int Sub_total { get; set; }
        public int Descuentos { get; set; }
        public int Total { get; set; }
        public int mesa_id { get; set; }
        public int garzon_id { get; set; }
        public int cliente_id { get; set; }

        public BoletaDAL()
        {
        }

        public BoletaDAL(int id_boleta, int fecha_boleta, int medio_pago, int sub_total, int descuentos, int total, int mesa_id, int garzon_id, int cliente_id)
        {
            this.id_boleta = id_boleta;
            this.fecha_boleta = fecha_boleta;
            Medio_pago = medio_pago;
            Sub_total = sub_total;
            Descuentos = descuentos;
            Total = total;
            this.mesa_id = mesa_id;
            this.garzon_id = garzon_id;
            this.cliente_id = cliente_id;
        }

        public bool verificaAsignacion(int num_mesa)
        {
            try
            {
                using (OracleConnection con = new Conexion().conexion())
                {
                    OracleCommand cm = new OracleCommand("Get_asignacion", con);
                    cm.BindByName = true;
                    cm.CommandType = System.Data.CommandType.StoredProcedure;

                    cm.Parameters.Add("num_mesa", OracleDbType.Int32).Value = num_mesa;

                    OracleParameter output = cm.Parameters.Add("my_cursor", OracleDbType.RefCursor);
                    output.Direction = System.Data.ParameterDirection.ReturnValue;
                    con.Open();
                    cm.ExecuteNonQuery();

                    OracleDataReader reader = ((OracleRefCursor)output.Value).GetDataReader();
                    con.Close();
                    using (DataTable dt = new DataTable())
                    {
                        OracleDataAdapter adapter = new OracleDataAdapter(cm);
                        adapter.Fill(dt);
                        
                        foreach (DataRow dr in dt.Rows)
                        {
                            string count = dr["COUNT(MESA_ID_MESA)"].ToString();
                            if (count == "1")
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
                return false;
            }
            catch ( Exception ex)
            {
                return false;
            }

        }
        public int get_numerobol(int num_mesa)
        {
            try
            {
                using (OracleConnection con = new Conexion().conexion())
                {
                    OracleCommand cm = new OracleCommand("Get_numbol", con);
                    cm.BindByName = true;
                    cm.CommandType = System.Data.CommandType.StoredProcedure;

                    cm.Parameters.Add("num_mesa", OracleDbType.Int32).Value = num_mesa;

                    OracleParameter output = cm.Parameters.Add("my_cursor", OracleDbType.Int32);
                    output.Direction = System.Data.ParameterDirection.ReturnValue;
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    return Int32.Parse( output.Value.ToString());
                }

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int GetTotal(int numBoleta)
        {
            try
            {
                using (OracleConnection con = new Conexion().conexion())
                {
                    OracleCommand cm = new OracleCommand("GetAllTotalByBoleta", con);
                    cm.BindByName = true;
                    cm.CommandType = System.Data.CommandType.StoredProcedure;

                    cm.Parameters.Add("num_boleta", OracleDbType.Int32).Value = numBoleta;

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
                return -1;
            }
        }



        public bool actualizarBol(int numbol,int tot,int subtot,string mediopago,string status)
        {
            try
            {
                using (OracleConnection con = new Conexion().conexion())
                {
                    OracleCommand cm = new OracleCommand("UpdateBoleta", con);
                    cm.BindByName = true;
                    cm.CommandType = System.Data.CommandType.StoredProcedure;

                    cm.Parameters.Add("numbol", OracleDbType.Int32).Value = numbol;
                    cm.Parameters.Add("tot", OracleDbType.Int32).Value = tot;
                    cm.Parameters.Add("subtot", OracleDbType.Int32).Value = subtot;
                    cm.Parameters.Add("mediopago", OracleDbType.Varchar2).Value = mediopago;
                    cm.Parameters.Add("status", OracleDbType.Varchar2).Value = status;

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

        public bool actualizarEstadoBol(int numbol, string status)
        {
            try
            {
                using (OracleConnection con = new Conexion().conexion())
                {
                    OracleCommand cm = new OracleCommand("UpdateEstadoBoleta", con);
                    cm.BindByName = true;
                    cm.CommandType = System.Data.CommandType.StoredProcedure;

                    cm.Parameters.Add("numbol", OracleDbType.Int32).Value = numbol;
                    cm.Parameters.Add("status", OracleDbType.Varchar2).Value = status;

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
