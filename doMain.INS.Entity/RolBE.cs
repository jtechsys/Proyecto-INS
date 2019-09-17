using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doMain.INS.Entity
{
    public class RolBE
    {

        public int IdRol {get; set;}
        public string Nombre  {get; set;}       
        public Boolean Activo { get; set;}

        public RolBE() { }
        public RolBE(int id) { this.IdRol = id; }
       
    }
}
