using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using doMain.INS.Entity;
using doMain.INS.Data;

namespace doMain.INS.Business
{
    public class EstadoBLL
    {
        public List<EstadoBE> List()
        {
            try
            {
                return new EstadoDAL().List();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
