using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class BoletaBLL
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

        public BoletaBLL()
        {
        }

        public BoletaBLL(int id_boleta, int fecha_boleta, int medio_pago, int sub_total, int descuentos, int total, int mesa_id, int garzon_id, int cliente_id)
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

        public bool verf_asignacion(int num_mesa)
        {
            BoletaDAL bolDAL = new BoletaDAL();
            return bolDAL.verificaAsignacion(num_mesa);
        }
        public int get_num_boleta(int num_mesa)
        {
            BoletaDAL bolDAL = new BoletaDAL();
            return bolDAL.get_numerobol(num_mesa);

        }

        public int GetTotalBoleta(int numBoleta)
        {
            BoletaDAL bolDAL = new BoletaDAL();
            return bolDAL.GetTotal(numBoleta);
        }

        public bool actualizarBoleta(int numbol, int tot, int subtot, string mediopago, string status)
        {
            BoletaDAL bolDAL = new BoletaDAL();
            return bolDAL.actualizarBol(numbol, tot, subtot, mediopago, status);
        }
        public bool actualizarEstadoBoleta(int numbol, string status)
        {
            BoletaDAL bolDAL = new BoletaDAL();
            return bolDAL.actualizarEstadoBol(numbol, status);

        }
    }
}
