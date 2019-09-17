using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doMain.INS.Entity
{
    public class EnsayoBE
    {
        public int IdEnsayo { get; set; }

        public string CodigoEnsayo { get; set; }

        public string Nombre { get; set; }

        public string Laboratorio { get; set; }

        public string Red { get; set; }

        public int Plazo { get; set; }

        public decimal Precio { get; set; }

        public int Estado { get; set; }

    }
}
