using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using doMain.INS.Entity;
using doMain.INS.Data;

namespace doMain.INS.Business
{
    public class EvaluadorBLL
    {
        public List<EvaluadorBE> List()
        {
            try
            {
                return new EvaluadorDAL().List();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
