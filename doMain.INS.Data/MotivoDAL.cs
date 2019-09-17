using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using doMain.INS.Entity;
using System.Data.SqlClient;

namespace doMain.INS.Data
{
    public class MotivoDAL
    {
        public List<MotivoBE> List()
        {

            List<MotivoBE> lstMotivo = null;

            try
            {
                SqlDataReader reader = SqlHelper.GetDataReader("pListMotivo");

                if (reader.HasRows)
                {
                    lstMotivo = new List<MotivoBE>();

                    while (reader.Read())
                    {
                        MotivoBE oMotivo = new MotivoBE();

                        oMotivo.IdMotivo = Convert.ToInt32(reader["IdMotivo"]);
                        oMotivo.Nombre = Convert.ToString(reader["Nombre"]);
                        oMotivo.Estado = Convert.ToInt32(reader["Estado"]);

                        lstMotivo.Add(oMotivo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstMotivo;
        }

    }
}
