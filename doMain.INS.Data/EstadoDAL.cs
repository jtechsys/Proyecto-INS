using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using doMain.INS.Entity;
using System.Data.SqlClient;

namespace doMain.INS.Data
{
    public class EstadoDAL
    {
        public List<EstadoBE> List()
        {

            List<EstadoBE> lstEstado = null;

            try
            {
                SqlDataReader reader = SqlHelper.GetDataReader("pListEstado");

                if (reader.HasRows)
                {
                    lstEstado = new List<EstadoBE>();

                    while (reader.Read())
                    {
                        EstadoBE oEstado = new EstadoBE();

                        oEstado.IdEstado = Convert.ToInt32(reader["IdEstado"]);
                        oEstado.Nombre = Convert.ToString(reader["Nombre"]);
                        oEstado.flagEstado = Convert.ToInt32(reader["Estado"]);

                        lstEstado.Add(oEstado);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstEstado;
        }
    }
}
