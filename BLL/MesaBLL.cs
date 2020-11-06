using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class MesaBLL
    {
        public int id_mesa { get; set; }
        public string nombre_sala { get; set; }
        public int capacidad { get; set; }
        public string status { get; set; }

        public MesaBLL()
        {
        }

        public MesaBLL(int id_mesa, string nombre_sala, int capacidad, string status)
        {
            this.id_mesa = id_mesa;
            this.nombre_sala = nombre_sala;
            this.capacidad = capacidad;
            this.status = status;
        }

        public bool SetStatusMesa(int idmesa,string estado)
        {
            MesaDAL mesDAL = new MesaDAL();
            return mesDAL.SetEstadoMesa(idmesa, estado);
        }
    }
}
