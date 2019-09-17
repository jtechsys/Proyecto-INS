using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doMain.INS.Entity
{
    public class ModuloBE
    {

        public int IdModulo { get; set; }
        public string CodigoModulo { get; set; }
        public string Modulo { get; set; }
        public int IdPadre { get; set; }
        public int Nivel { get; set; }
        public int Estado { get; set; }
        public string Url { get; set; }
        public string Tooltip { get; set; }
        public List<ModuloBE> List { get; set;}

    }
}
