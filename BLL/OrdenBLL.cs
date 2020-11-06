using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class OrdenBLL
    {
        public int id_orden { get; set; }
        public int id_boleta { get; set; }
        public DateTime fecha_orden { get; set; }

        public OrdenBLL()
        {
        }

        public OrdenBLL(int id_orden, int id_boleta, DateTime fecha_orden)
        {
            this.id_orden = id_orden;
            this.id_boleta = id_boleta;
            this.fecha_orden = fecha_orden;
        }

        public bool creaOrden(int num_boleta)
        {
            OrdenDAL ordDAL = new OrdenDAL();
            return ordDAL.CreaOrden(num_boleta);
        }

        public DataTable GetDetalleByBoleta (int numbol)
        {
            OrdenDAL ordDAL = new OrdenDAL();
            return ordDAL.GetAllOrdenesByBoleta(numbol);
        }
    }
}
