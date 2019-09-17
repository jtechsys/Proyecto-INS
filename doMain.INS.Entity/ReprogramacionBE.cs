using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doMain.INS.Entity
{
    public class ReprogramacionBE
    {
        public int IdReprogramacion { get; set; }
        public int IdExpediente { get; set; }
        public string Oficio { get; set; }
        public DateTime FechaOficio { get; set; }
        public DateTime FechaCorreo { get; set; }
        public int IdEnsayo { get; set; }
        public int IdAnalista { get; set; }
        public int Plazo { get; set; }
        public int IdMotivo { get; set; }
        public string OficioRpta { get; set; }
        public DateTime FechaOficioRpta { get; set; }
        public int IdUsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }        
        public int DiaPlazo { get; set; }        

        /* Referencia a otras clases */
        public EvaluadorBE Evaluador { get; set; }
        public EnsayoBE Ensayo { get; set; }
        public MotivoBE Motivo { get; set; }
    }
}
