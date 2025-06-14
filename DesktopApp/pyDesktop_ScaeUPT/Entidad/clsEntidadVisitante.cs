using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCAE_UPT.Entidad
{
    public class clsEntidadVisitante
    {
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }

        public clsEntidadVisitante()
        {
        }

        public clsEntidadVisitante(string dni, string nombre, string apellido, string telefono)
        {
            DNI = dni;
            Nombre = nombre;
            Apellido = apellido;
            Telefono = telefono;
        }
    }
}
