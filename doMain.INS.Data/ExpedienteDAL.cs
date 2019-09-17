using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using doMain.INS.Entity;
using System.Data.SqlClient;

namespace doMain.INS.Data
{
    public class ExpedienteDAL
    {
        public List<ExpedienteBE> ListExpediente(ExpedienteBE objExpediente)
        {
            List<ExpedienteBE> lstExpediente = null;
            Dictionary<string, object> searchParameters = null;

            try
            {
                searchParameters = new Dictionary<string, object>() {
                                                                        { "@inStartRowIndex", objExpediente.StartRowIndex },
                                                                        { "@inEndRoowIndex", objExpediente.EndRowIndex },
                                                                        { "@CodigoExpediente", objExpediente.CodigoExpediente },
                                                                        { "@CodigoCotizacion", objExpediente.CodigoCotizacion },
                                                                        { "@IdTipoCliente", objExpediente.IdTipoCliente },
                                                                        { "@Cliente", objExpediente.Cliente },
                                                                        { "@Producto", objExpediente.Producto },
                                                                        { "@IdEstado", objExpediente.IdEstado },
                                                                        { "@IdEnsayo", objExpediente.IdEnsayo },
                                                                        { "@IdAnalista", objExpediente.IdAnalista },
                                                                        { "@FecCotizacion", objExpediente.FechaCotizacion },
                                                                        { "@Procede", objExpediente.IdProcedencia },
                                                                        { "@FecIngreso", objExpediente.FechaIngreso }
                                                                    };

                SqlDataReader reader = SqlHelper.GetDataReader("pListExpediente", searchParameters);                

                if (reader.HasRows)
                {
                    lstExpediente = new List<ExpedienteBE>();

                    while (reader.Read())
                    {
                        ExpedienteBE oExpediente = new ExpedienteBE();

                        oExpediente.IdExpediente = Convert.ToInt32(reader["IdExpediente"]);
                        oExpediente.CodigoExpediente = Convert.ToString(reader["Expediente"]);
                        oExpediente.CodigoCotizacion = Convert.ToString(reader["CodigoCotizacion"]);

                        if (!Convert.IsDBNull(reader["FechaCotizacion"]))
                        {
                            oExpediente.FechaCotizacion = Convert.ToDateTime(reader["FechaCotizacion"]);
                        }
                        
                        oExpediente.Cliente = Convert.ToString(reader["Cliente"]);
                        oExpediente.TipoCliente = Convert.ToString(reader["Tipo_Cliente"]);
                        oExpediente.Situacion = Convert.ToString(reader["Situacion"]);
                        oExpediente.Producto = Convert.ToString(reader["Producto"]);
                        oExpediente.Lote = Convert.ToString(reader["Lote"]);
                        oExpediente.Correo = Convert.ToString(reader["Correo"]);
                        oExpediente.DocumentoFisico = Convert.ToString(reader["DocumentoFisico"]);
                        oExpediente.DocumentoAnexo = Convert.ToString(reader["DocumentoAnexo"]);
                        oExpediente.Estado = Convert.ToString(reader["Estado"]);
                        oExpediente.MotivoAnulacion = Convert.ToString(reader["Motivo_Anulacion"]);
                        oExpediente.NroEvaluacion = Convert.ToInt32(reader["NroEvaluacion"]);

                        if (!Convert.IsDBNull(reader["FechaVencimiento"]))
                        {
                            oExpediente.FechaVencimiento = Convert.ToDateTime(reader["FechaVencimiento"]);
                        }

                        oExpediente.Evaluacion = new EvaluacionBE();
                        oExpediente.Evaluacion.Procede = Convert.ToString(reader["Procede"]);

                        oExpediente.Evaluador = new EvaluadorBE();
                        oExpediente.Evaluador.Nombre = Convert.ToString(reader["Analista"]);

                        oExpediente.Ensayo = new EnsayoBE();
                        oExpediente.Ensayo.Nombre = Convert.ToString(reader["Ensayo"]);

                        oExpediente.Usuario = new UsuarioBE();
                        oExpediente.Usuario.Login = Convert.ToString(reader["Usuario"]);
                        oExpediente.NroExpediente = Convert.ToInt32(reader["Total"]);
                        oExpediente.Alerta = Convert.ToInt32(reader["Alerta"]);

                        /*Recepcion Muestras*/
                        if (!Convert.IsDBNull(reader["FechaIngreso"]))
                        {
                            oExpediente.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
                        }

                        oExpediente.DocumentoCustodia = Convert.ToString(reader["DocumentoCustodia"]);

                        if (!Convert.IsDBNull(reader["FechaIngresoLab"]))
                        {
                            oExpediente.FechaIngresoLab = Convert.ToDateTime(reader["FechaIngresoLab"]);
                        }

                        if (!Convert.IsDBNull(reader["FechaEntregaCliente"]))
                        {
                            oExpediente.FechaEntregaCliente = Convert.ToDateTime(reader["FechaEntregaCliente"]);
                        }

                        if (!Convert.IsDBNull(reader["FechaRecepcionIE"]))
                        {
                            oExpediente.FechaRecepcionIE = Convert.ToDateTime(reader["FechaRecepcionIE"]);
                        }

                        oExpediente.InformeEnsayo = Convert.ToString(reader["InformeEnsayo"]);

                        oExpediente.NroReprogramacion = Convert.ToInt32(reader["NroReprogramacion"]);

                        lstExpediente.Add(oExpediente);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstExpediente;
        }
        public List<ExpedienteBE> ListExpedienteRM(ExpedienteBE objExpediente)
        {
            List<ExpedienteBE> lstExpediente = null;
            Dictionary<string, object> searchParameters = null;

            try
            {
                searchParameters = new Dictionary<string, object>() {
                                                                        { "@inStartRowIndex", objExpediente.StartRowIndex },
                                                                        { "@inEndRoowIndex", objExpediente.EndRowIndex },
                                                                        { "@CodigoExpediente", objExpediente.CodigoExpediente },
                                                                        { "@CodigoCotizacion", objExpediente.CodigoCotizacion },
                                                                        { "@IdTipoCliente", objExpediente.IdTipoCliente },
                                                                        { "@Cliente", objExpediente.Cliente },
                                                                        { "@Producto", objExpediente.Producto },
                                                                        { "@IdEstado", objExpediente.IdEstado },
                                                                        { "@IdEnsayo", objExpediente.IdEnsayo },
                                                                        { "@IdAnalista", objExpediente.IdAnalista },
                                                                        { "@FecCotizacion", objExpediente.FechaCotizacion },
                                                                        { "@Procede", objExpediente.IdProcedencia },
                                                                        { "@FecIngreso", objExpediente.FechaIngreso }
                                                                    };

                SqlDataReader reader = SqlHelper.GetDataReader("pListExpedienteRM", searchParameters);

                if (reader.HasRows)
                {
                    lstExpediente = new List<ExpedienteBE>();

                    while (reader.Read())
                    {
                        ExpedienteBE oExpediente = new ExpedienteBE();

                        oExpediente.IdExpediente = Convert.ToInt32(reader["IdExpediente"]);
                        oExpediente.CodigoExpediente = Convert.ToString(reader["Expediente"]);
                        oExpediente.CodigoCotizacion = Convert.ToString(reader["CodigoCotizacion"]);

                        if (!Convert.IsDBNull(reader["FechaCotizacion"]))
                        {
                            oExpediente.FechaCotizacion = Convert.ToDateTime(reader["FechaCotizacion"]);
                        }

                        oExpediente.Cliente = Convert.ToString(reader["Cliente"]);
                        oExpediente.TipoCliente = Convert.ToString(reader["Tipo_Cliente"]);
                        oExpediente.Situacion = Convert.ToString(reader["Situacion"]);
                        oExpediente.Producto = Convert.ToString(reader["Producto"]);
                        oExpediente.Lote = Convert.ToString(reader["Lote"]);
                        oExpediente.Correo = Convert.ToString(reader["Correo"]);
                        oExpediente.DocumentoFisico = Convert.ToString(reader["DocumentoFisico"]);
                        oExpediente.DocumentoAnexo = Convert.ToString(reader["DocumentoAnexo"]);
                        oExpediente.Estado = Convert.ToString(reader["Estado"]);
                        oExpediente.MotivoAnulacion = Convert.ToString(reader["Motivo_Anulacion"]);
                        oExpediente.NroEvaluacion = Convert.ToInt32(reader["NroEvaluacion"]);

                        if (!Convert.IsDBNull(reader["FechaVencimiento"]))
                        {
                            oExpediente.FechaVencimiento = Convert.ToDateTime(reader["FechaVencimiento"]);
                        }

                        oExpediente.Evaluacion = new EvaluacionBE();
                        oExpediente.Evaluacion.Procede = Convert.ToString(reader["Procede"]);

                        oExpediente.Evaluador = new EvaluadorBE();
                        oExpediente.Evaluador.Nombre = Convert.ToString(reader["Analista"]);

                        oExpediente.Ensayo = new EnsayoBE();
                        oExpediente.Ensayo.Nombre = Convert.ToString(reader["Ensayo"]);

                        oExpediente.Usuario = new UsuarioBE();
                        oExpediente.Usuario.Login = Convert.ToString(reader["Usuario"]);
                        oExpediente.NroExpediente = Convert.ToInt32(reader["Total"]);
                        oExpediente.Alerta = Convert.ToInt32(reader["Alerta"]);

                        /*Recepcion Muestras*/
                        if (!Convert.IsDBNull(reader["FechaIngreso"]))
                        {
                            oExpediente.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
                        }

                        oExpediente.DocumentoCustodia = Convert.ToString(reader["DocumentoCustodia"]);

                        if (!Convert.IsDBNull(reader["FechaIngresoLab"]))
                        {
                            oExpediente.FechaIngresoLab = Convert.ToDateTime(reader["FechaIngresoLab"]);
                        }

                        if (!Convert.IsDBNull(reader["FechaEntregaCliente"]))
                        {
                            oExpediente.FechaEntregaCliente = Convert.ToDateTime(reader["FechaEntregaCliente"]);
                        }

                        if (!Convert.IsDBNull(reader["FechaRecepcionIE"]))
                        {
                            oExpediente.FechaRecepcionIE = Convert.ToDateTime(reader["FechaRecepcionIE"]);
                        }

                        oExpediente.InformeEnsayo = Convert.ToString(reader["InformeEnsayo"]);

                        oExpediente.NroReprogramacion = Convert.ToInt32(reader["NroReprogramacion"]);

                        lstExpediente.Add(oExpediente);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstExpediente;
        }
        public List<ExpedienteBE> ListExpedienteFisicoQuimico(ExpedienteBE objExpediente)
        {
            List<ExpedienteBE> lstExpediente = null;
            Dictionary<string, object> searchParameters = null;

            try
            {
                searchParameters = new Dictionary<string, object>() {
                                                                        { "@inStartRowIndex", objExpediente.StartRowIndex },
                                                                        { "@inEndRoowIndex", objExpediente.EndRowIndex },
                                                                        { "@CodigoExpediente", objExpediente.CodigoExpediente },
                                                                        { "@CodigoCotizacion", objExpediente.CodigoCotizacion },
                                                                        { "@IdTipoCliente", objExpediente.IdTipoCliente },
                                                                        { "@Cliente", objExpediente.Cliente },
                                                                        { "@Producto", objExpediente.Producto },
                                                                        { "@IdEstado", objExpediente.IdEstado },
                                                                        { "@IdEnsayo", objExpediente.IdEnsayo },
                                                                        { "@IdAnalista", objExpediente.IdAnalista },
                                                                        { "@FecCotizacion", objExpediente.FechaCotizacion }
                                                                      
                                                                    };

                SqlDataReader reader = SqlHelper.GetDataReader("pListExpedienteFisicoQuimico", searchParameters);

                if (reader.HasRows)
                {
                    lstExpediente = new List<ExpedienteBE>();

                    while (reader.Read())
                    {
                        ExpedienteBE oExpediente = new ExpedienteBE();

                        oExpediente.IdExpediente = Convert.ToInt32(reader["IdExpediente"]);
                        oExpediente.CodigoExpediente = Convert.ToString(reader["Expediente"]);
                        oExpediente.CodigoCotizacion = Convert.ToString(reader["CodigoCotizacion"]);

                        if (!Convert.IsDBNull(reader["FechaCotizacion"]))
                        {
                            oExpediente.FechaCotizacion = Convert.ToDateTime(reader["FechaCotizacion"]);
                        }

                        oExpediente.Cliente = Convert.ToString(reader["Cliente"]);
                        oExpediente.TipoCliente = Convert.ToString(reader["Tipo_Cliente"]);
                        oExpediente.Situacion = Convert.ToString(reader["Situacion"]);
                        oExpediente.Producto = Convert.ToString(reader["Producto"]);
                        oExpediente.Lote = Convert.ToString(reader["Lote"]);                     
                        oExpediente.Estado = Convert.ToString(reader["Estado"]);
                        oExpediente.Evaluador = new EvaluadorBE();
                        oExpediente.Evaluador.Nombre = Convert.ToString(reader["Analista"]);

                        oExpediente.Ensayo = new EnsayoBE();
                        oExpediente.Ensayo.Nombre = Convert.ToString(reader["Ensayo"]);

                        oExpediente.Usuario = new UsuarioBE();
                        oExpediente.Usuario.Login = Convert.ToString(reader["Usuario"]);
                        oExpediente.NroExpediente = Convert.ToInt32(reader["Total"]);
                        oExpediente.Alerta = Convert.ToInt32(reader["Alerta"]);                                             
                        oExpediente.NroReprogramacion = Convert.ToInt32(reader["NroReprogramacion"]);
                        oExpediente.NroEvaluacion = Convert.ToInt32(reader["NroEvaluacion"]);

                        /*Fisico Quimica*/
                        oExpediente.DCI = Convert.ToString(reader["DCI"]);
                        oExpediente.Clasificacion = Convert.ToString(reader["Clasificacion"]);
                        
                        if (!Convert.IsDBNull(reader["Red"]))
                        {
                            oExpediente.Red = Convert.ToInt32(reader["Red"]);
                        }

                        oExpediente.Norma = Convert.ToString(reader["Norma"]);

                        if (!Convert.IsDBNull(reader["FechaIngreso"]))
                        {
                            oExpediente.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
                        }

                        if (!Convert.IsDBNull(reader["FechaIngresoSIGEL"]))
                        {
                            oExpediente.FechaIngresoSIGEL = Convert.ToDateTime(reader["FechaIngresoSIGEL"]);
                        }

                        lstExpediente.Add(oExpediente);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstExpediente;
        }
        public List<ExpedienteBE> ExportExpediente(ExpedienteBE objExpediente)
        {
            List<ExpedienteBE> lstExpediente = null;
            Dictionary<string, object> searchParameters = null;

            try
            {
                searchParameters = new Dictionary<string, object>() {                                                                       
                                                                        { "@CodigoExpediente", objExpediente.CodigoExpediente },
                                                                        { "@CodigoCotizacion", objExpediente.CodigoCotizacion },
                                                                        { "@IdTipoCliente", objExpediente.IdTipoCliente },
                                                                        { "@Cliente", objExpediente.Cliente },
                                                                        { "@Producto", objExpediente.Producto },
                                                                        { "@IdEstado", objExpediente.IdEstado },
                                                                        { "@IdEnsayo", objExpediente.IdEnsayo },
                                                                        { "@IdAnalista", objExpediente.IdAnalista },
                                                                        { "@FecCotizacion", objExpediente.FechaCotizacion },
                                                                        { "@Procede", objExpediente.IdProcedencia }
                                                                    };

                SqlDataReader reader = SqlHelper.GetDataReader("pExportExpediente", searchParameters);

                if (reader.HasRows)
                {
                    lstExpediente = new List<ExpedienteBE>();

                    while (reader.Read())
                    {
                        ExpedienteBE oExpediente = new ExpedienteBE();

                        oExpediente.IdExpediente = Convert.ToInt32(reader["IdExpediente"]);
                        oExpediente.CodigoExpediente = Convert.ToString(reader["Expediente"]);
                        oExpediente.CodigoCotizacion = Convert.ToString(reader["CodigoCotizacion"]);

                        if (!Convert.IsDBNull(reader["FechaCotizacion"]))
                        {
                            oExpediente.FechaCotizacion = Convert.ToDateTime(reader["FechaCotizacion"]);
                        }

                        oExpediente.Cliente = Convert.ToString(reader["Cliente"]);
                        oExpediente.TipoCliente = Convert.ToString(reader["Tipo_Cliente"]);
                        oExpediente.Situacion = Convert.ToString(reader["Situacion"]);
                        oExpediente.Producto = Convert.ToString(reader["Producto"]);
                        oExpediente.Lote = Convert.ToString(reader["Lote"]);
                        oExpediente.Correo = Convert.ToString(reader["Correo"]);
                        oExpediente.DocumentoFisico = Convert.ToString(reader["DocumentoFisico"]);
                        oExpediente.DocumentoAnexo = Convert.ToString(reader["DocumentoAnexo"]);
                        oExpediente.Estado = Convert.ToString(reader["Estado"]);
                        oExpediente.MotivoAnulacion = Convert.ToString(reader["Motivo_Anulacion"]);
                        oExpediente.NroEvaluacion = Convert.ToInt32(reader["NroEvaluacion"]);

                        if (!Convert.IsDBNull(reader["FechaVencimiento"]))
                        {
                            oExpediente.FechaVencimiento = Convert.ToDateTime(reader["FechaVencimiento"]);
                        }

                        oExpediente.Evaluacion = new EvaluacionBE();
                        oExpediente.Evaluacion.Procede = Convert.ToString(reader["Procede"]);

                        oExpediente.Evaluador = new EvaluadorBE();
                        oExpediente.Evaluador.Nombre = Convert.ToString(reader["Analista"]);

                        oExpediente.Ensayo = new EnsayoBE();
                        oExpediente.Ensayo.Nombre = Convert.ToString(reader["Ensayo"]);

                        oExpediente.Usuario = new UsuarioBE();
                        oExpediente.Usuario.Login = Convert.ToString(reader["Usuario"]);

                        if (!Convert.IsDBNull(reader["FechaIngreso"]))
                        {
                            oExpediente.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
                        }

                        if (!Convert.IsDBNull(reader["FechaIngresoLab"]))
                        {
                            oExpediente.FechaIngresoLab = Convert.ToDateTime(reader["FechaIngresoLab"]);
                        }

                        if (!Convert.IsDBNull(reader["FechaRecepcion"]))
                        {
                            oExpediente.FechaRecepcion = Convert.ToDateTime(reader["FechaRecepcion"]);
                        }

                        oExpediente.DocumentoCustodia = Convert.ToString(reader["DocumentoCustodia"]);

                        oExpediente.InformeEnsayo = Convert.ToString(reader["InformeEnsayo"]);

                        lstExpediente.Add(oExpediente);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstExpediente;
        }
        public List<ExpedienteBE> GetExpediente(ExpedienteBE objExpediente)
        {
            List<ExpedienteBE> lstExpediente = null;
            Dictionary<string, object> searchParameters = null;

            try
            {
                searchParameters = new Dictionary<string, object>() {
                                                                        { "@IdExpediente", objExpediente.IdExpediente }                                                                        
                                                                    };

                SqlDataReader reader = SqlHelper.GetDataReader("pGetExpediente", searchParameters);

                if (reader.HasRows)
                {
                    lstExpediente = new List<ExpedienteBE>();

                    while (reader.Read())
                    {
                        ExpedienteBE oExpediente = new ExpedienteBE();

                        oExpediente.IdExpediente = Convert.ToInt32(reader["IdExpediente"]);
                        oExpediente.CodigoExpediente = Convert.ToString(reader["Expediente"]);
                        oExpediente.CodigoCotizacion = Convert.ToString(reader["CodigoCotizacion"]);

                        if (!Convert.IsDBNull(reader["FechaCotizacion"]))
                        {
                            oExpediente.FechaCotizacion = Convert.ToDateTime(reader["FechaCotizacion"]);
                        }
                            
                        oExpediente.Cliente = Convert.ToString(reader["Cliente"]);
                        oExpediente.IdTipoCliente = Convert.ToInt32(reader["IdTipoCliente"]);                        
                        oExpediente.Producto = Convert.ToString(reader["Producto"]);
                        oExpediente.Lote = Convert.ToString(reader["Lote"]);
                        oExpediente.Correo = Convert.ToString(reader["Correo"]);
                        oExpediente.DocumentoFisico = Convert.ToString(reader["DocumentoFisico"]);
                        oExpediente.DocumentoAnexo = Convert.ToString(reader["DocumentoAnexo"]);
                        oExpediente.Estado = Convert.ToString(reader["flgEstado"]);
                        oExpediente.MotivoAnulacion = Convert.ToString(reader["MotivoAnulacion"]);

                        if (!Convert.IsDBNull(reader["FechaVencimiento"]))
                        {
                            oExpediente.FechaVencimiento = Convert.ToDateTime(reader["FechaVencimiento"]);
                        }                        

                        lstExpediente.Add(oExpediente);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstExpediente;

        }
        public Boolean UpdateAnulacionCotizacion(ExpedienteBE objExpediente)
        {

            Dictionary<string, object> parameters = null;

            try
            {
                parameters = new Dictionary<string, object>() { { "@IdExpediente", objExpediente.IdExpediente },
                                                                { "@MotivoAnulacion", objExpediente.MotivoAnulacion }};

                SqlHelper.ExecuteNonQuery("pUpdateCotizacionAnulacion", parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }         

        }
        public Boolean SaveCotizacion(ExpedienteBE objExpediente)
        {

            Dictionary<string, object> parameters = null;

            try
            {
                parameters = new Dictionary<string, object>() { { "@CodigoExpediente", objExpediente.CodigoExpediente },
                                                                { "@CodigoCotizacion", objExpediente.CodigoCotizacion },
                                                                { "@IdTipoCliente", objExpediente.IdTipoCliente},
                                                                { "@FechaCotizacion", objExpediente.FechaCotizacion},
                                                                { "@Correo", objExpediente.Correo},
                                                                { "@DocumentoFisico", objExpediente.DocumentoFisico},
                                                                { "@DocumentoAnexo", objExpediente.DocumentoAnexo},
                                                                { "@IdUsuarioRegistro", objExpediente.IdUsuario},
                                                                { "@Oficio", objExpediente.Oficio},
                                                                { "@FechaOficio", objExpediente.FechaOficio},
                                                                { "@ActaPesquisa", objExpediente.ActaPesquisa},
                                                                { "@FechaPesquisa", objExpediente.FechaPesquisa}
                                                              };

                SqlHelper.ExecuteNonQuery("pSaveCotizacion", parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }
        public Boolean UpdateCotizacion(ExpedienteBE objExpediente)
        {

            Dictionary<string, object> parameters = null;

            try
            {
                parameters = new Dictionary<string, object>() { { "@IdExpediente", objExpediente.IdExpediente },
                                                                { "@CodigoExpediente", objExpediente.CodigoExpediente },
                                                                { "@CodigoCotizacion", objExpediente.CodigoCotizacion },
                                                                { "@IdTipoCliente", objExpediente.IdTipoCliente},
                                                                { "@FechaCotizacion", objExpediente.FechaCotizacion},
                                                                { "@Correo", objExpediente.Correo},
                                                                { "@DocumentoFisico", objExpediente.DocumentoFisico},
                                                                { "@DocumentoAnexo", objExpediente.DocumentoAnexo},
                                                                { "@IdUsuarioActualiza", objExpediente.IdUsuarioActualiza}
                                                              };

                SqlHelper.ExecuteNonQuery("pUpdateCotizacion", parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }
        public Boolean UpdateCotizacionRM(ExpedienteBE objExpediente)
        {

            Dictionary<string, object> parameters = null;

            try
            {
                parameters = new Dictionary<string, object>() { { "@IdExpediente", objExpediente.IdExpediente },
                                                                { "@CodigoExpediente", objExpediente.CodigoExpediente },                                                                
                                                                { "@IdTipoCliente", objExpediente.IdTipoCliente},
                                                                { "@Oficio", objExpediente.Oficio},
                                                                { "@FechaOficio", objExpediente.FechaOficio},
                                                                { "@FechaRecepcion", objExpediente.FechaRecepcion},                                                                
                                                                { "@IdUsuarioActualiza", objExpediente.IdUsuarioActualiza}
                                                              };

                SqlHelper.ExecuteNonQuery("pUpdateCotizacionRM", parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }
        public Boolean UpdateEstadoExpediente(ExpedienteBE objExpediente)
        {
            Dictionary<string, object> parameters = null;

            try
            {
                parameters = new Dictionary<string, object>() { { "@IdExpediente", objExpediente.IdExpediente },
                                                                { "@IdEstado", objExpediente.IdEstado },
                                                                { "@IdUsuarioActualiza", objExpediente.IdUsuarioActualiza}
                                                              };

                SqlHelper.ExecuteNonQuery("pUpdateEstadoExpediente", parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }
        public Boolean UpdateExpedienteCustodia(ExpedienteBE objExpediente)
        {
            Dictionary<string, object> parameters = null;

            try
            {
                parameters = new Dictionary<string, object>() { { "@IdExpediente", objExpediente.IdExpediente },
                                                                { "@FechaIngreso", objExpediente.FechaIngreso },
                                                                { "@DocumentoCustodia", objExpediente.DocumentoCustodia },
                                                                { "@IdTipoProducto", objExpediente.IdTipoProducto },
                                                                { "@IdClaseProducto", objExpediente.IdClaseProducto },
                                                                { "@CantidadCustodia", objExpediente.CantidadCustodia },
                                                                { "@CondicionAmbiental", objExpediente.CondicionAmbiental },
                                                                { "@ContraMuestra", objExpediente.ContraMuestra},
                                                                { "@CamaraFria", objExpediente.CamaraFria},
                                                                { "@PreCamara", objExpediente.PreCamara},
                                                                { "@IdUsuarioActualiza", objExpediente.IdUsuarioActualiza}
                                                              };

                SqlHelper.ExecuteNonQuery("pUpdateExpedienteCustodia", parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }
        public Boolean UpdateExpedienteContraMuestra(ExpedienteBE objExpediente)
        {
            Dictionary<string, object> parameters = null;

            try
            {
                parameters = new Dictionary<string, object>() { { "@IdExpediente", objExpediente.IdExpediente },
                                                                { "@FechaIngresoLab", objExpediente.FechaIngresoLab },
                                                                { "@FechaEntregaCliente", objExpediente.FechaEntregaCliente },
                                                                { "@ContramuestraCER", objExpediente.ContramuestraCER },
                                                                { "@ContramuestraFQ", objExpediente.ContramuestraFQ },
                                                                { "@ContramuestraMicrob", objExpediente.ContramuestraMicrob },
                                                                { "@MuestraOficio", objExpediente.MuestraOficio },
                                                                { "@MuestraCantidad", objExpediente.MuestraCantidad},
                                                                { "@IdAreaRetiro1", objExpediente.IdAreaRetiro1},
                                                                { "@CantidadRetiro1", objExpediente.CantidadRetiro1},
                                                                { "@IdAreaRetiro2", objExpediente.IdAreaRetiro2},
                                                                { "@CantidadRetiro2", objExpediente.CantidadRetiro2},
                                                                { "@Saldo", objExpediente.Saldo},
                                                                { "@MuestraUbicacion", objExpediente.MuestraUbicacion},
                                                                { "@IdUsuarioActualiza", objExpediente.IdUsuarioActualiza}
                                                              };

                SqlHelper.ExecuteNonQuery("pUpdateExpedienteContraMuestra", parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }
        public Boolean UpdateExpedienteFacturacion(ExpedienteBE objExpediente)
        {
            Dictionary<string, object> parameters = null;

            try
            {
                parameters = new Dictionary<string, object>() { { "@IdExpediente", objExpediente.IdExpediente },
                                                                { "@FechaRecepcionIE", objExpediente.FechaRecepcionIE },
                                                                { "@InformeEnsayo", objExpediente.InformeEnsayo },
                                                                { "@Conclusion", objExpediente.Conclusion },
                                                                { "@Proforma", objExpediente.Proforma },
                                                                { "@Factura", objExpediente.Factura },
                                                                { "@FechaEntregaPool", objExpediente.FechaEntregaPool },
                                                                { "@Muestras", objExpediente.Muestras},                                                             
                                                                { "@IdUsuarioActualiza", objExpediente.IdUsuarioActualiza}
                                                              };

                SqlHelper.ExecuteNonQuery("pUpdateExpedienteFacturacion", parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }

        }
        public List<ExpedienteBE> GetExpedienteRecepcionMuestraId(ExpedienteBE objExpediente)
        {
            List<ExpedienteBE> lstExpediente = null;
            Dictionary<string, object> searchParameters = null;

            try
            {
                searchParameters = new Dictionary<string, object>() {
                                                                        { "@IdExpediente", objExpediente.IdExpediente }
                                                                    };

                SqlDataReader reader = SqlHelper.GetDataReader("pGetExpedienteRecepcionMuestraId", searchParameters);

                if (reader.HasRows)
                {
                    lstExpediente = new List<ExpedienteBE>();

                    while (reader.Read())
                    {
                        ExpedienteBE oExpediente = new ExpedienteBE();

                        oExpediente.IdExpediente = Convert.ToInt32(reader["IdExpediente"]);
                        oExpediente.CodigoExpediente = Convert.ToString(reader["CodigoExpediente"]);

                        oExpediente.IdTipoCliente = Convert.ToInt32(reader["IdTipoCliente"]);

                        oExpediente.Oficio = Convert.ToString(reader["Oficio"]);

                        if (!Convert.IsDBNull(reader["FechaIngreso"]))
                        {
                            oExpediente.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
                        }

                        if (!Convert.IsDBNull(reader["FechaOficio"]))
                        {
                            oExpediente.FechaOficio = Convert.ToDateTime(reader["FechaOficio"]);
                        }

                        if (!Convert.IsDBNull(reader["FechaRecepcion"]))
                        {
                            oExpediente.FechaRecepcion = Convert.ToDateTime(reader["FechaRecepcion"]);
                        }

                        oExpediente.DocumentoCustodia = Convert.ToString(reader["DocumentoCustodia"]);
                        
                        if (!Convert.IsDBNull(reader["IdClaseProducto"]))
                        {
                            oExpediente.IdClaseProducto = Convert.ToInt32(reader["IdClaseProducto"]);
                        }

                        oExpediente.ClaseProducto = Convert.ToString(reader["ClaseProducto"]);

                        if (!Convert.IsDBNull(reader["IdTipoProducto"]))
                        {
                            oExpediente.IdTipoProducto = Convert.ToInt32(reader["IdTipoProducto"]);
                        }

                        oExpediente.TipoProducto = Convert.ToString(reader["TipoProducto"]);

                        if (!Convert.IsDBNull(reader["CantidadCustodia"]))
                        {
                            oExpediente.CantidadCustodia = Convert.ToInt32(reader["CantidadCustodia"]);
                        }                        

                        oExpediente.CondicionAmbiental = Convert.ToString(reader["CondicionAmbiental"]);
                        oExpediente.ContraMuestra = Convert.ToString(reader["Contramuestra"]);
                        oExpediente.CamaraFria = Convert.ToString(reader["CamaraFria"]);
                        oExpediente.PreCamara = Convert.ToString(reader["PreCamara"]);

                        if (!Convert.IsDBNull(reader["FechaIngresoLab"]))
                        {
                            oExpediente.FechaIngresoLab = Convert.ToDateTime(reader["FechaIngresoLab"]);
                        }

                        if (!Convert.IsDBNull(reader["FechaEntregaCliente"]))
                        {
                            oExpediente.FechaEntregaCliente = Convert.ToDateTime(reader["FechaEntregaCliente"]);
                        }

                        oExpediente.ContramuestraCER = Convert.ToString(reader["ContramuestraCER"]);
                        oExpediente.ContramuestraFQ = Convert.ToString(reader["ContramuestraFQ"]);
                        oExpediente.ContramuestraMicrob = Convert.ToString(reader["ContramuestraMicrob"]);
                        oExpediente.MuestraOficio = Convert.ToString(reader["MuestraOficio"]);

                        if (!Convert.IsDBNull(reader["MuestraCantidad"]))
                        {
                            oExpediente.MuestraCantidad = Convert.ToInt32(reader["MuestraCantidad"]);
                        }                        

                        if (!Convert.IsDBNull(reader["IdAreaRetiro1"]))
                        {
                            oExpediente.IdAreaRetiro1 = Convert.ToInt32(reader["IdAreaRetiro1"]);
                        }

                        oExpediente.AreaRetiro1 = Convert.ToString(reader["AREA1"]);

                        if (!Convert.IsDBNull(reader["CantidadRetiro1"]))
                        {
                            oExpediente.CantidadRetiro1 = Convert.ToInt32(reader["CantidadRetiro1"]);
                        }

                        if (!Convert.IsDBNull(reader["IdAreaRetiro2"]))
                        {
                            oExpediente.IdAreaRetiro2 = Convert.ToInt32(reader["IdAreaRetiro2"]);
                        }

                        oExpediente.AreaRetiro2 = Convert.ToString(reader["AREA2"]);

                        if (!Convert.IsDBNull(reader["CantidadRetiro2"]))
                        {
                            oExpediente.CantidadRetiro2 = Convert.ToInt32(reader["CantidadRetiro2"]);
                        }

                        if (!Convert.IsDBNull(reader["CantidadRetiro2"]))
                        {
                            oExpediente.Saldo = Convert.ToDecimal(reader["Saldo"]);
                        }                        

                        oExpediente.MuestraUbicacion = Convert.ToString(reader["MuestraUbicacion"]);

                        if (!Convert.IsDBNull(reader["FechaRecepcionIE"]))
                        {
                            oExpediente.FechaRecepcionIE = Convert.ToDateTime(reader["FechaRecepcionIE"]);
                        }

                        oExpediente.InformeEnsayo = Convert.ToString(reader["InformeEnsayo"]);

                        if (!Convert.IsDBNull(reader["Conclusion"]))
                        {
                            oExpediente.Conclusion = Convert.ToInt32(reader["Conclusion"]);
                        }

                        oExpediente.NombreConclusion = Convert.ToString(reader["NombreConclusion"]);

                        oExpediente.Proforma = Convert.ToString(reader["Proforma"]);
                        oExpediente.Factura = Convert.ToString(reader["Factura"]);

                        if (!Convert.IsDBNull(reader["FechaEntregaPool"]))
                        {
                            oExpediente.FechaEntregaPool = Convert.ToDateTime(reader["FechaEntregaPool"]);
                        }

                        if (!Convert.IsDBNull(reader["Muestras"]))
                        {
                            oExpediente.Muestras = Convert.ToInt32(reader["Muestras"]);
                        }

                        oExpediente.NombreMuestras = Convert.ToString(reader["NombreMuestra"]);

                        lstExpediente.Add(oExpediente);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstExpediente;

        }
    }
}
