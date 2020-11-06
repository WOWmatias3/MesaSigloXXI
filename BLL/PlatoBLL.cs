using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class PlatoBLL
    {
        public int id_plato { get; set; }
        public string nombre_plato { get; set; }
        public int precio { get; set; }
        public string categoria { get; set; }
        public string Habilitado { get; set; }
        public byte[] imagen { get; set; }

        public PlatoBLL()
        {
        }

        public PlatoBLL(int id_plato, string nombre_plato, int precio, string categoria, string habilitado, byte[] imagen)
        {
            this.id_plato = id_plato;
            this.nombre_plato = nombre_plato;
            this.precio = precio;
            this.categoria = categoria;
            Habilitado = habilitado;
            this.imagen = imagen;
        }

        public bool reduceStock(int id_plato, int cantidad)
        {
            PlatoDAL plDAL = new PlatoDAL();
            return plDAL.reduceStock(id_plato, cantidad);
        }

        public bool ingresaRelacion(int id_plato, int cantidad)
        {
            PlatoDAL plDAL = new PlatoDAL();
            return plDAL.ingresaRelacion(id_plato,cantidad);
        }
        public int verificaStock(int id_plato)
        {
            PlatoDAL plDAL = new PlatoDAL();
            return plDAL.verificaStock(id_plato);
        }

    }
}
