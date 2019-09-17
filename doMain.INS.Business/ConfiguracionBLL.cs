using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using doMain.INS.Entity;
using doMain.INS.Data;

namespace doMain.INS.Business
{
    public class ConfiguracionBLL
    {

        public Dictionary<string, string> GetValue(int idParametro)
        {
            try
            {
                return new ConfiguracionDAL().GetListValue(idParametro);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dictionary<string, string> GetListValue(int idPadre, int idParametro)
        {
            try
            {
                return new ConfiguracionDAL().GetListValue(idPadre, idParametro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


         public Dictionary<string, string> GetListArea(int idPadre)
        {
            try
            {
                return new ConfiguracionDAL().GetListArea(idPadre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
