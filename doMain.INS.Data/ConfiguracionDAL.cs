using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using doMain.INS.Entity;


namespace doMain.INS.Data
{
    public class ConfiguracionDAL
    {

        public Dictionary<string, string> GetListValue(int idParametro)
        {
            try
            {

                Dictionary<string, object> param = new Dictionary<string, object>() { { "@IdParametro", idParametro }};                
                return SqlHelper.GetDictionary("pListConfiguracion", param);

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

                Dictionary<string, object> param = new Dictionary<string, object>() {
                                                                                        { "@IdPadre", idPadre },
                                                                                        { "@IdParametro", idParametro }
                                                                                    };
                return SqlHelper.GetDictionary("pGet_ValorConfiguracion", param);

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

                Dictionary<string, object> param = new Dictionary<string, object>() {
                                                                                        { "@IdPadre", idPadre }                                                                                     
                                                                                    };
                return SqlHelper.GetDictionary("pList_Area", param);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
