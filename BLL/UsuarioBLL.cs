using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class UsuarioBLL
    {

        public int id_usuario { get; set; }
        public string nombre_usuario { get; set; }
        public string clave { get; set; }
        public string rol { get; set; }

        public UsuarioBLL()
        {
        }

        public UsuarioBLL(int id_usuario, string nombre_usuario, string clave, string rol)
        {
            this.id_usuario = id_usuario;
            this.nombre_usuario = nombre_usuario;
            this.clave = clave;
            this.rol = rol;
        }

        public bool verificaUser(string username,string pass)
        {
            UsuarioDAL usrDAL = new UsuarioDAL();
            return usrDAL.verificaUsuario(username,pass);
        }

    }
}
