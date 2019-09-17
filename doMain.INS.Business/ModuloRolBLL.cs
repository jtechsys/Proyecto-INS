using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using doMain.INS.Entity;
using doMain.INS.Data;


namespace doMain.INS.Business
{
    public class ModuloRolBLL
    {
        public List<ModuloRolBE> List(ModuloRolBE objModuloRol)
        {
            try
            {
                return new ModuloRolDAL().List(objModuloRol);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
