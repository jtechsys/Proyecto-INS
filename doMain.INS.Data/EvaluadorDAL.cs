using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using doMain.INS.Entity;
using System.Data.SqlClient;


namespace doMain.INS.Data
{
    public class EvaluadorDAL
    {

        public List<EvaluadorBE> List()
        {

            List<EvaluadorBE> lstEvaluador = null;

            try
            {
                SqlDataReader reader = SqlHelper.GetDataReader("pListEvaluador");

                if (reader.HasRows)
                {
                    lstEvaluador = new List<EvaluadorBE>();

                    while (reader.Read())
                    {
                        EvaluadorBE oEvaluador = new EvaluadorBE();

                        oEvaluador.IdEvaluador = Convert.ToInt32(reader["IdEvaluador"]);
                        oEvaluador.Evaluador = Convert.ToString(reader["Evaluador"]);
                        oEvaluador.Nombre = Convert.ToString(reader["Nombre"]);
                        oEvaluador.Apellido = Convert.ToString(reader["Apellido"]);
                        oEvaluador.TituloProfesional = Convert.ToString(reader["TituloProfesional"]);
                        oEvaluador.Area = Convert.ToString(reader["Area"]);
                        oEvaluador.Cargo = Convert.ToString(reader["Cargo"]);
                        oEvaluador.Condicion = Convert.ToString(reader["Condicion"]);
                        oEvaluador.Estado = Convert.ToInt32(reader["Estado"]);

                        lstEvaluador.Add(oEvaluador);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstEvaluador;
        }
    }
}
