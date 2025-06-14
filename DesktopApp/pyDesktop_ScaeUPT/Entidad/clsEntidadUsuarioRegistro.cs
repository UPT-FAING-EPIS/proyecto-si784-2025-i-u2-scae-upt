using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCAE_UPT.Entidad
{
    public class clsEntidadUsuarioRegistro
    {
        public int ID_registro { get; set; }
        public string ID_Empleado { get; set; }
        public DateTime? FechaHora_Ingreso { get; set; }
        public DateTime? FechaHora_Salida { get; set; }
        public string TipoEntrada { get; set; }
        public TimeSpan? TiempoTardanza { get; set; }

        public clsEntidadUsuarioRegistro()
        {
        }

        public clsEntidadUsuarioRegistro(int idRegistro, string idEmpleado, DateTime? fechaHoraIngreso, DateTime? fechaHoraSalida, string tipoEntrada, TimeSpan? tiempoTardanza)
        {
            ID_registro = idRegistro;
            ID_Empleado = idEmpleado;
            FechaHora_Ingreso = fechaHoraIngreso;
            FechaHora_Salida = fechaHoraSalida;
            TipoEntrada = tipoEntrada;
            TiempoTardanza = tiempoTardanza;
        }
    }
}
