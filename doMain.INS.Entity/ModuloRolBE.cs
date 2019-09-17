using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doMain.INS.Entity
{
    public class ModuloRolBE
    {
        public int IdRol { get; set; }
        public int IdModulo { get; set; }
        public int Nuevo { get; set; }
        public int Actualizar { get; set; }
        public int Eliminar { get; set; }
        public int Exportar { get; set; }
        public int Imprimir { get; set; }
        public ModuloBE Modulo { get; set; }        
        public RolBE Rol { get; set; }


    }
}
