using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using doMain.INS.Entity;
using System.Data.SqlClient;


namespace doMain.INS.Data
{
    public class LaboratorioDAL
    {

        public List<LaboratorioBE> List(LaboratorioBE objLaboratorio)
        {

            List<LaboratorioBE> lstlaboratorio = null;
            Dictionary<string, object> searchParameters = null;

            try
            {
                searchParameters = new Dictionary<string, object>() {
                                                                        { "@IdExpediente", objLaboratorio.IdExpediente }
                                                                    };

                SqlDataReader reader = SqlHelper.GetDataReader("pListLaboratorio", searchParameters);

                if (reader.HasRows)
                {
                    lstlaboratorio = new List<LaboratorioBE>();

                    while (reader.Read())
                    {
                        LaboratorioBE oLaboratorio = new LaboratorioBE();

                        oLaboratorio.IdLaboratorio = Convert.ToInt32(reader["IdLaboratorio"]);
                        oLaboratorio.IdEnsayo = Convert.ToInt32(reader["IdEnsayo"]);

                        oLaboratorio.OrdenServicio = Convert.ToString(reader["OrdenServicio"]);
                        oLaboratorio.Ensayo = new EnsayoBE();
                        oLaboratorio.Ensayo.Nombre = Convert.ToString(reader["Ensayo"]);
                        
                        oLaboratorio.IdAnalista = Convert.ToInt32(reader["IdAnalista"]);

                        oLaboratorio.Evaluador = new EvaluadorBE();
                        oLaboratorio.Evaluador.Nombre = Convert.ToString(reader["Analista"]);

                        if (!Convert.IsDBNull(reader["FechaVencimiento"]))
                        {
                            oLaboratorio.FechaVencimiento = Convert.ToDateTime(reader["FechaVencimiento"]);
                        }

                        if (!Convert.IsDBNull(reader["FechaEntregaMax"]))
                        {
                            oLaboratorio.FechaEntregaMax = Convert.ToDateTime(reader["FechaEntregaMax"]);
                        }

                        oLaboratorio.Confirmacion = Convert.ToString(reader["Confirmacion"]);
                        oLaboratorio.Pesquisa = Convert.ToString(reader["Pesquisa"]);
                        oLaboratorio.EnsayoHPLC = Convert.ToString(reader["EnsayoHPLC"]);
                        oLaboratorio.Condicion = Convert.ToString(reader["Condicion"]);
                        oLaboratorio.Observaciones = Convert.ToString(reader["Observaciones"]);

                        lstlaboratorio.Add(oLaboratorio);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstlaboratorio;
        }
        public List<LaboratorioBE> GetLaboratorio(LaboratorioBE objLaboratorio)
        {
            List<LaboratorioBE> lstlaboratorio = null;
            Dictionary<string, object> searchParameters = null;

            try
            {
                searchParameters = new Dictionary<string, object>() {
                                                                        { "@IdLaboratorio", objLaboratorio.IdLaboratorio }
                                                                    };

                SqlDataReader reader = SqlHelper.GetDataReader("pGetLaboratorio", searchParameters);

                if (reader.HasRows)
                {
                    lstlaboratorio = new List<LaboratorioBE>();

                    while (reader.Read())
                    {
                        LaboratorioBE oLaboratorio = new LaboratorioBE();

                        oLaboratorio.IdExpediente = Convert.ToInt32(reader["IdExpediente"]);
                        oLaboratorio.IdLaboratorio = Convert.ToInt32(reader["IdLaboratorio"]);
                        oLaboratorio.IdEnsayo = Convert.ToInt32(reader["IdEnsayo"]);
                        oLaboratorio.IdAnalista = Convert.ToInt32(reader["IdAnalista"]);

                        if (!Convert.IsDBNull(reader["FechaVencimiento"]))
                        {
                            oLaboratorio.FechaVencimiento = Convert.ToDateTime(reader["FechaVencimiento"]);
                        }

                        if (!Convert.IsDBNull(reader["FechaEntregaMax"]))
                        {
                            oLaboratorio.FechaEntregaMax = Convert.ToDateTime(reader["FechaEntregaMax"]);
                        }

                        oLaboratorio.Confirmacion = Convert.ToString(reader["Confirmacion"]);
                        oLaboratorio.Pesquisa = Convert.ToString(reader["Pesquisa"]);
                        oLaboratorio.EnsayoHPLC = Convert.ToString(reader["EnsayoHPLC"]);
                        oLaboratorio.Condicion = Convert.ToString(reader["Condicion"]);
                        oLaboratorio.Observaciones = Convert.ToString(reader["Observaciones"]);

                        lstlaboratorio.Add(oLaboratorio);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstlaboratorio;

        }
        public Boolean SaveLaboratorio(LaboratorioBE objLaboratorio)
        {

            Dictionary<string, object> parameters = null;

            try
            {
                parameters = new Dictionary<string, object>() { { "@IdExpediente", objLaboratorio.IdExpediente },
                                                                { "@IdEnsayo", objLaboratorio.IdEnsayo },
                                                                { "@IdAnalista", objLaboratorio.IdAnalista},
                                                                { "@FechaVencimiento", objLaboratorio.FechaVencimiento},
                                                                { "@FechaEntregaMax", objLaboratorio.FechaEntregaMax},
                                                                { "@Confirmacion", objLaboratorio.Confirmacion},
                                                                { "@Pesquisa", objLaboratorio.Pesquisa},
                                                                { "@EnsayoHPLC", objLaboratorio.EnsayoHPLC},
                                                                { "@Condicion", objLaboratorio.Condicion},
                                                                { "@Observaciones", objLaboratorio.Observaciones}
                                                              };

                SqlHelper.ExecuteNonQuery("pSaveLaboratorio", parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }
        public Boolean UpdateLaboratorio(LaboratorioBE objLaboratorio)
        {

            Dictionary<string, object> parameters = null;

            try
            {
                parameters = new Dictionary<string, object>() { { "@IdLaboratorio", objLaboratorio.IdLaboratorio },
                                                                { "@OrdenServicio", objLaboratorio.OrdenServicio },
                                                                { "@IdEnsayo", objLaboratorio.IdEnsayo },
                                                                { "@IdAnalista", objLaboratorio.IdAnalista},
                                                                { "@FechaVencimiento", objLaboratorio.FechaVencimiento},
                                                                { "@FechaEntregaMax", objLaboratorio.FechaEntregaMax},
                                                                { "@Confirmacion", objLaboratorio.Confirmacion},
                                                                { "@Pesquisa", objLaboratorio.Pesquisa},
                                                                { "@EnsayoHPLC", objLaboratorio.EnsayoHPLC},
                                                                { "@Condicion", objLaboratorio.Condicion},
                                                                { "@Observaciones", objLaboratorio.Observaciones}
                                                              };

                SqlHelper.ExecuteNonQuery("pUpdateLaboratorio", parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }
        public Boolean DeleteLaboratorio(LaboratorioBE objLaboratorio)
        {

            Dictionary<string, object> parameters = null;

            try
            {
                parameters = new Dictionary<string, object>() { { "@IdLaboratorio", objLaboratorio.IdLaboratorio }};

                SqlHelper.ExecuteNonQuery("pDeleteLaboratorio", parameters);

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
