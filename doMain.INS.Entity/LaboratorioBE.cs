using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doMain.INS.Entity
{
    public class LaboratorioBE
    {
        public int IdLaboratorio { get; set; }
        public int IdExpediente { get; set; }
        public int IdEnsayo { get; set; }
        public int IdAnalista { get; set; }
        public string OrdenServicio { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime FechaEntregaMax { get; set; }
        public string Confirmacion { get; set; }
        public string Pesquisa { get; set; }
        public string EnsayoHPLC { get; set; }
        public string Condicion { get; set; }
        public string Observaciones { get; set; }

        /* Referencia a otras clases */
        public EvaluadorBE Evaluador { get; set; }
        public EnsayoBE Ensayo { get; set; }

    }
}
