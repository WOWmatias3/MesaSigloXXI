using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class BebestibleBLL
    {

        public int id_bebestible { get; set; }
        public string nom_bebestible { get; set; }
        public string marca { get; set; }
        public int precio { get; set; }
        public byte[] imagen { get; set; }
        public string habilitado { get; set; }
        public int stock { get; set; }

        public BebestibleBLL()
        {
        }

        public BebestibleBLL(int id_bebestible, string nom_bebestible, string marca, int precio, byte[] imagen, string habilitado, int stock)
        {
            this.id_bebestible = id_bebestible;
            this.nom_bebestible = nom_bebestible;
            this.marca = marca;
            this.precio = precio;
            this.imagen = imagen;
            this.habilitado = habilitado;
            this.stock = stock;
        }

        public bool agregarRelacion(int id_beb, int cantidad )
        {
            BebestibleDAL bebDAL = new BebestibleDAL();
            return bebDAL.ingresaRelacion(id_beb, cantidad);
        }


        public bool reduceStock(int id_plato, int cantidad)
        {
            BebestibleDAL bebDAL = new BebestibleDAL();
            return bebDAL.reduceStock(id_plato, cantidad);
        }
        public int verificaStock(int id_plato)
        {
            BebestibleDAL bebDAL = new BebestibleDAL();
            return bebDAL.verificaStock(id_plato);
        }

    }
}
