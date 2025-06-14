using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCAE_UPT.Entidad
{
    public class clsEntidadRegistro
    {
        public int ID_registro { get; set; }
        public string DNI { get; set; }
        public DateTime? FechaHora_Ingreso { get; set; }
        public DateTime? FechaHora_Salida { get; set; }
        public string IDUsuario_Ingreso { get; set; }
        public string IDUsuario_Salida { get; set; }
        public string TipoEntrada { get; set; }

        public clsEntidadRegistro()
        {
        }

        public clsEntidadRegistro(string tipoEntrada, int idRegistro, string dni, DateTime? fechaHoraIngreso, DateTime? fechaHoraSalida, string idUsuarioIngreso, string idUsuarioSalida)
        {
            TipoEntrada = tipoEntrada;
            ID_registro = idRegistro;
            DNI = dni;
            FechaHora_Ingreso = fechaHoraIngreso;
            FechaHora_Salida = fechaHoraSalida;
            IDUsuario_Ingreso = idUsuarioIngreso;
            IDUsuario_Salida = idUsuarioSalida;
        }
    }
}
