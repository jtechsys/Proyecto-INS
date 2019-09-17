using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using doMain.INS.Entity;
using System.Data.SqlClient;

namespace doMain.INS.Data
{
    public class ModuloRolDAL
    {

        public List<ModuloRolBE> List(ModuloRolBE objModuloRol)
        {
            List<ModuloRolBE> lstModuloRol = null;
            Dictionary<string, object> searchParameters = null;

            try
            {
                searchParameters = new Dictionary<string, object>() { { "@IdRol", objModuloRol.IdRol }                                                                      
                                                                     };

                SqlDataReader reader = SqlHelper.GetDataReader("pListModuloRol", searchParameters);
                

                if (reader.HasRows)
                {
                    lstModuloRol = new List<ModuloRolBE>();

                    while (reader.Read())
                    {
                        ModuloRolBE oModuloRol = new ModuloRolBE();

                        oModuloRol.IdModulo = Convert.ToInt32(reader["IdModulo"]);
                        oModuloRol.IdRol = Convert.ToInt32(reader["IdRol"]);
                        oModuloRol.Nuevo = Convert.ToInt32(reader["Nuevo"]);
                        oModuloRol.Actualizar = Convert.ToInt32(reader["Actualizar"]);
                        oModuloRol.Eliminar = Convert.ToInt32(reader["Eliminar"]);
                        oModuloRol.Exportar = Convert.ToInt32(reader["Exportar"]);
                        oModuloRol.Imprimir = Convert.ToInt32(reader["Imprimir"]);
                        oModuloRol.Modulo = new ModuloBE();
                        oModuloRol.Modulo.Modulo = Convert.ToString(reader["Nombre"]);
                        oModuloRol.Modulo.Nivel = Convert.ToInt32(reader["Nivel"]);
                        oModuloRol.Modulo.IdPadre = Convert.ToInt32(reader["Padre"]);
                        oModuloRol.Modulo.Url = Convert.ToString(reader["URL"]);
                        oModuloRol.Modulo.Tooltip = Convert.ToString(reader["ToolTip"]);

                        lstModuloRol.Add(oModuloRol);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstModuloRol;
        }

    }
}
