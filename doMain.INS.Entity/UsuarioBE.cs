using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doMain.INS.Entity
{
    public class UsuarioBE
    {

        public int IdUsuario { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombre { get; set; }
        public string DNI { get; set; }
        public string MailTrabajo { get; set; }
        public int Celular { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Estado { get; set; }
        public int IdArea { get; set; }
        public string Area { get; set; }
        public int IdRol { get; set; }
        
        public DateTime FechaCaducidad { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdUsuarioRegistro { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public int IdUsuarioActualiza { get; set; }
        public int Valor { get; set; }
        public int Total { get; set; }

        /* Atributos de Paginacion */
        public int StartRowIndex { get; set; }
        public int EndRowIndex { get; set; }
        public string SortRow { get; set; }

        /* Referencia a otras clases */
        public RolBE Rol { get; set; }

    }
}
