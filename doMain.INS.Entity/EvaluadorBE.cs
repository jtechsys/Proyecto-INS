using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doMain.INS.Entity
{
    public class EvaluadorBE
    {
        public int IdEvaluador { get; set; }

        public string Evaluador { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string TituloProfesional { get; set; }

        public string Area { get; set; }
        public string Cargo { get; set; }

        public string Condicion { get; set; }

        public int Estado { get; set; }

    }
}
