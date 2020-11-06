using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MesaDAL
    {
        public int id_mesa { get; set; }
        public string nombre_sala { get; set; }
        public int capacidad { get; set; }
        public string status { get; set; }

        public MesaDAL()
        {
        }

        public MesaDAL(int id_mesa, string nombre_sala, int capacidad, string status)
        {
            this.id_mesa = id_mesa;
            this.nombre_sala = nombre_sala;
            this.capacidad = capacidad;
            this.status = status;
        }

        public bool SetEstadoMesa(int idmesa , string estado)
        {
            try
            {
                using (OracleConnection con = new Conexion().conexion())
                {
                    OracleCommand cm = new OracleCommand("SetEstadoMesa", con);
                    cm.BindByName = true;
                    cm.CommandType = System.Data.CommandType.StoredProcedure;

                    cm.Parameters.Add("idmesa", OracleDbType.Int32).Value = idmesa;
                    cm.Parameters.Add("estado", OracleDbType.Varchar2).Value = estado;

                    con.Open();
                    cm.ExecuteNonQuery();

                    con.Close();
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
    }
}
