using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using doMain.INS.Entity;
using System.Data.SqlClient;

namespace doMain.INS.Data
{
    public class EnsayoDAL
    {
        public List<EnsayoBE> List()
        {

            List<EnsayoBE> lstEnsayo = null;

            try
            {
                SqlDataReader reader = SqlHelper.GetDataReader("pListEnsayo");

                if (reader.HasRows)
                {
                    lstEnsayo = new List<EnsayoBE>();

                    while (reader.Read())
                    {
                        EnsayoBE oEnsayo = new EnsayoBE();

                        oEnsayo.IdEnsayo = Convert.ToInt32(reader["IdEnsayo"]);
                        oEnsayo.CodigoEnsayo = Convert.ToString(reader["CodigoEnsayo"]);
                        oEnsayo.Nombre = Convert.ToString(reader["Nombre"]);
                        oEnsayo.Laboratorio = Convert.ToString(reader["Laboratorio"]);
                        oEnsayo.Red = Convert.ToString(reader["Red"]);

                        if (!Convert.IsDBNull(reader["Plazo"]))
                        {
                            oEnsayo.Plazo = Convert.ToInt32(reader["Plazo"]);
                        }

                        if (!Convert.IsDBNull(reader["Precio"]))
                        {
                            oEnsayo.Precio = Convert.ToDecimal(reader["Precio"]);
                        }                           

                        lstEnsayo.Add(oEnsayo);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstEnsayo;
        }

    }
}
