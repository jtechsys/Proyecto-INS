using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using doMain.INS.Entity;
using System.Data.SqlClient;

namespace doMain.INS.Data
{
    public class EvaluacionDAL
    {

        public List<EvaluacionBE> GetEvaluacionbyIdExpediente(EvaluacionBE objEvaluacion)
        {
            List<EvaluacionBE> lstEvaluacion = null;
            Dictionary<string, object> searchParameters = null;

            try
            {
                searchParameters = new Dictionary<string, object>() {
                                                                        { "@IdExpediente", objEvaluacion.IdExpediente }
                                                                    };

                SqlDataReader reader = SqlHelper.GetDataReader("pGetEvaluacion", searchParameters);

                if (reader.HasRows)
                {
                    lstEvaluacion = new List<EvaluacionBE>();

                    while (reader.Read())
                    {
                        EvaluacionBE oEvaluacion = new EvaluacionBE();

                        oEvaluacion.IdEvaluacion = Convert.ToInt32(reader["IdEvaluacion"]);
                        oEvaluacion.IdExpediente = Convert.ToInt32(reader["IdExpediente"]);
                        oEvaluacion.IdEnsayo = Convert.ToInt32(reader["IdEnsayo"]);
                        oEvaluacion.IdAnalista = Convert.ToInt32(reader["IdAnalista"]);
                        oEvaluacion.IdSituacion = Convert.ToInt32(reader["IdSituacion"]);
                        oEvaluacion.IdProcede = Convert.ToInt32(reader["Procede"]);

                        if (!Convert.IsDBNull(reader["FechaEvaluacion"]))
                        {
                            oEvaluacion.FechaEvaluacion = Convert.ToDateTime(reader["FechaEvaluacion"]);
                        }

                        oEvaluacion.ObservacionSituacion = Convert.ToString(reader["Observacion_Situacion"]);

                        lstEvaluacion.Add(oEvaluacion);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstEvaluacion;

        }
        public Boolean SaveEvaluacion(EvaluacionBE objEvaluacion)
        {

            Dictionary<string, object> parameters = null;

            try
            {
                parameters = new Dictionary<string, object>() { { "@IdExpediente", objEvaluacion.IdExpediente },                                                                
                                                                { "@IdEnsayo", objEvaluacion.IdEnsayo},
                                                                { "@IdAnalista", objEvaluacion.IdAnalista},
                                                                { "@IdSituacion", objEvaluacion.IdSituacion},
                                                                { "@Procede", objEvaluacion.IdProcede},
                                                                { "@ObservacionSituacion", objEvaluacion.ObservacionSituacion}
                                                              };

                SqlHelper.ExecuteNonQuery("pSaveEvaluacion", parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }
    }
}
