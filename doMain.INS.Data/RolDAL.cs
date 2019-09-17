using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using doMain.INS.Entity;
using System.Data.SqlClient;

namespace doMain.INS.Data
{
    public class RolDAL
    {
        public List<RolBE> List()
        {

            List<RolBE> lstRol = null;

            try
            {
                SqlDataReader reader = SqlHelper.GetDataReader("pListRol");

                if (reader.HasRows)
                {
                    lstRol = new List<RolBE>();

                    while (reader.Read())
                    {
                        RolBE oRol = new RolBE();

                        oRol.IdRol = Convert.ToInt32(reader["IdRol"]);
                        oRol.Nombre = Convert.ToString(reader["Nombre"]);                        
                        oRol.Activo = Convert.ToBoolean(reader["Estado"]);

                        lstRol.Add(oRol);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstRol;
        }

    }
}
