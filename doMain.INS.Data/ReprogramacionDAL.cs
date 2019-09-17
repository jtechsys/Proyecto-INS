using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using doMain.INS.Entity;
using System.Data.SqlClient;

namespace doMain.INS.Data
{
    public class ReprogramacionDAL
    {

        public Boolean SaveReprogramacion(ReprogramacionBE objReprogramacion)
        {

            Dictionary<string, object> parameters = null;

            try
            {
                parameters = new Dictionary<string, object>() { { "@IdExpediente", objReprogramacion.IdExpediente },
                                                                { "@Oficio", objReprogramacion.Oficio },
                                                                { "@FechaOficio", objReprogramacion.FechaOficio},
                                                                { "@FechaCorreo", objReprogramacion.FechaCorreo},
                                                                { "@IdEnsayo", objReprogramacion.IdEnsayo},
                                                                { "@IdAnalista", objReprogramacion.IdAnalista},
                                                                { "@Plazo", objReprogramacion.Plazo},
                                                                { "@IdMotivo", objReprogramacion.IdMotivo},
                                                                { "@OficioRpta", objReprogramacion.OficioRpta},
                                                                { "@FechaOficioRpta", objReprogramacion.FechaOficioRpta},
                                                                { "@IdUsuarioRegistro", objReprogramacion.IdUsuarioRegistro}
                                                              };

                SqlHelper.ExecuteNonQuery("pSaveReprogramacion", parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

        public Boolean UpdateReprogramacion(ReprogramacionBE objReprogramacion)
        {

            Dictionary<string, object> parameters = null;

            try
            {
                parameters = new Dictionary<string, object>() { { "@IdReprogramacion", objReprogramacion.IdReprogramacion },
                                                                { "@Oficio", objReprogramacion.Oficio },
                                                                { "@FechaOficio", objReprogramacion.FechaOficio},
                                                                { "@FechaCorreo", objReprogramacion.FechaCorreo},
                                                                { "@IdEnsayo", objReprogramacion.IdEnsayo},
                                                                { "@IdAnalista", objReprogramacion.IdAnalista},
                                                                { "@Plazo", objReprogramacion.Plazo},
                                                                { "@IdMotivo", objReprogramacion.IdMotivo},
                                                                { "@OficioRpta", objReprogramacion.OficioRpta},
                                                                { "@FechaOficioRpta", objReprogramacion.FechaOficioRpta},
                                                                { "@IdUsuarioRegistro", objReprogramacion.IdUsuarioRegistro}
                                                              };

                SqlHelper.ExecuteNonQuery("pUpdateReprogramacion", parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

        public Boolean DeleteReprogramacion(ReprogramacionBE objReprogramacion)
        {

            Dictionary<string, object> parameters = null;

            try
            {
                parameters = new Dictionary<string, object>() { { "@IdReprogramacion", objReprogramacion.IdReprogramacion } };

                SqlHelper.ExecuteNonQuery("pDeleteReprogramacion", parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

        public List<ReprogramacionBE> GetReprogramacion(ReprogramacionBE objReprogramacion)
        {
            List<ReprogramacionBE> lstReprogramacion = null;
            Dictionary<string, object> searchParameters = null;

            try
            {
                searchParameters = new Dictionary<string, object>() {
                                                                        { "@IdReprogramacion", objReprogramacion.IdReprogramacion }
                                                                    };

                SqlDataReader reader = SqlHelper.GetDataReader("pGetReprogramacion", searchParameters);

                if (reader.HasRows)
                {
                    lstReprogramacion = new List<ReprogramacionBE>();

                    while (reader.Read())
                    {
                        ReprogramacionBE oReprogramacion = new ReprogramacionBE();

                        oReprogramacion.IdReprogramacion = Convert.ToInt32(reader["IdReprogramacion"]);
                        oReprogramacion.IdExpediente = Convert.ToInt32(reader["IdExpediente"]);
                        oReprogramacion.Oficio = Convert.ToString(reader["Oficio"]);
                        oReprogramacion.FechaOficio = Convert.ToDateTime(reader["FechaOficio"]);
                        oReprogramacion.FechaCorreo = Convert.ToDateTime(reader["FechaCorreo"]);
                        oReprogramacion.IdEnsayo = Convert.ToInt32(reader["IdEnsayo"]);
                        oReprogramacion.IdAnalista = Convert.ToInt32(reader["IdAnalista"]);
                        oReprogramacion.Plazo = Convert.ToInt32(reader["Plazo"]);
                        oReprogramacion.IdMotivo = Convert.ToInt32(reader["IdMotivo"]);
                        oReprogramacion.OficioRpta = Convert.ToString(reader["OficioRpta"]);
                        oReprogramacion.FechaOficioRpta = Convert.ToDateTime(reader["FechaOficioRpta"]);
                        oReprogramacion.IdUsuarioRegistro = Convert.ToInt32(reader["IdUsuarioRegistro"]);

                        lstReprogramacion.Add(oReprogramacion);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstReprogramacion;

        }

        public List<ReprogramacionBE> GetReprogramacionbyExpediente(ReprogramacionBE objReprogramacion)
        {
            List<ReprogramacionBE> lstReprogramacion = null;
            Dictionary<string, object> searchParameters = null;

            try
            {
                searchParameters = new Dictionary<string, object>() {
                                                                        { "@IdExpediente", objReprogramacion.IdExpediente }
                                                                    };

                SqlDataReader reader = SqlHelper.GetDataReader("pGetReprogramacionbyExpediente", searchParameters);

                if (reader.HasRows)
                {
                    lstReprogramacion = new List<ReprogramacionBE>();

                    while (reader.Read())
                    {
                        ReprogramacionBE oReprogramacion = new ReprogramacionBE();

                        oReprogramacion.IdReprogramacion = Convert.ToInt32(reader["IdReprogramacion"]);
                        oReprogramacion.IdExpediente = Convert.ToInt32(reader["IdExpediente"]);
                        oReprogramacion.Oficio = Convert.ToString(reader["Oficio"]);
                        oReprogramacion.FechaOficio = Convert.ToDateTime(reader["FechaOficio"]);
                        oReprogramacion.FechaCorreo = Convert.ToDateTime(reader["FechaCorreo"]);

                        oReprogramacion.IdEnsayo = Convert.ToInt32(reader["IdEnsayo"]);

                        oReprogramacion.Ensayo = new EnsayoBE();
                        oReprogramacion.Ensayo.Nombre = Convert.ToString(reader["ENSAYO"]);                        

                        oReprogramacion.IdAnalista = Convert.ToInt32(reader["IdAnalista"]);

                        oReprogramacion.Evaluador = new EvaluadorBE();
                        oReprogramacion.Evaluador.Nombre = Convert.ToString(reader["ANALISTA"]);

                        oReprogramacion.Plazo = Convert.ToInt32(reader["Plazo"]);
                        oReprogramacion.IdMotivo = Convert.ToInt32(reader["IdMotivo"]);
                        oReprogramacion.OficioRpta = Convert.ToString(reader["OficioRpta"]);
                        oReprogramacion.FechaOficioRpta = Convert.ToDateTime(reader["FechaOficioRpta"]);
                        oReprogramacion.IdUsuarioRegistro = Convert.ToInt32(reader["IdUsuarioRegistro"]);
                        oReprogramacion.DiaPlazo = Convert.ToInt32(reader["DiaPlazo"]);

                        oReprogramacion.Motivo = new MotivoBE();
                        oReprogramacion.Motivo.Nombre = Convert.ToString(reader["MOTIVO"]);                       

                        lstReprogramacion.Add(oReprogramacion);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstReprogramacion;

        }

    }
}
