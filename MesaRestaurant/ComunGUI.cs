using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;

namespace MesaRestaurant
{
    public class ComunGUI
    {
        public bool verificaAsignacion(int numero_mesa)
        {
            BoletaBLL bolBLL = new BoletaBLL();
            return bolBLL.verf_asignacion(numero_mesa);
        }

        public int get_numbol (int numero_mesa)
        {
            BoletaBLL bolBLL = new BoletaBLL();
            return bolBLL.get_num_boleta(numero_mesa);
        }
    }
}