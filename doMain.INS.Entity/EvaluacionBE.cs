using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doMain.INS.Entity
{
    public class EvaluacionBE
    {
        public int IdEvaluacion { get; set; }

        public int IdExpediente { get; set; }
        public int IdEnsayo { get; set; }
        public int IdAnalista { get; set; }
        public int IdSituacion { get; set; }
        public int IdProcede { get; set; }
        public string Procede { get; set; }
        public DateTime FechaEvaluacion { get; set; }
        public string ObservacionSituacion { get; set; }

    }
}


