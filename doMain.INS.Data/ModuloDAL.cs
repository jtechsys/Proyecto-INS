using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using doMain.INS.Entity;
using System.Data.SqlClient;

namespace doMain.INS.Data
{
    public class ModuloDAL
    {
        
        public List<ModuloBE> List()
        {
           
            List<ModuloBE> lstModulo = null;
            
            try
            {
                SqlDataReader reader = SqlHelper.GetDataReader("pListModulo");

                if (reader.HasRows)
                {                   
                    lstModulo = new List<ModuloBE>();

                    while (reader.Read())
                    {
                        ModuloBE oModulo = new ModuloBE();

                        oModulo.IdModulo = Convert.ToInt32(reader["MODU_inIdModulo"]);
                        oModulo.CodigoModulo = Convert.ToString(reader["MODU_vcCodModulo"]);
                        oModulo.Modulo = Convert.ToString(reader["MODU_vcNombre"]);
                        oModulo.IdPadre = Convert.ToInt32(reader["MODU_inPadre"]);
                        oModulo.Nivel = Convert.ToInt32(reader["MODU_inNivel"]);
                        oModulo.Estado = Convert.ToInt32(reader["MODU_btEstado"]);
                        lstModulo.Add(oModulo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstModulo;            
        }
      

    }
}
