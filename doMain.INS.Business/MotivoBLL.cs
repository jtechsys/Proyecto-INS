using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using doMain.INS.Entity;
using doMain.INS.Data;

namespace doMain.INS.Business
{
    public class MotivoBLL
    {
        public List<MotivoBE> List()
        {
            try
            {
                return new MotivoDAL().List();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
