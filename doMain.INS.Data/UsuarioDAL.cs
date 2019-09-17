using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Reflection;
using doMain.INS.Entity;

using System.Data.SqlClient;


namespace doMain.INS.Data
{
    public class UsuarioDAL
    {
        public List<UsuarioBE> ListUsuario(UsuarioBE objUsuario)
        {
            List<UsuarioBE> lstUsuario = null;
            Dictionary<string, object> searchParameters = null;

            try
            {
                searchParameters = new Dictionary<string, object>() {
                                                                        { "@inStartRowIndex", objUsuario.StartRowIndex },
                                                                        { "@inEndRoowIndex", objUsuario.EndRowIndex },
                                                                        { "@IdRol", objUsuario.IdRol },
                                                                        { "@IdArea", objUsuario.IdArea },
                                                                        { "@ApellidoPaterno", objUsuario.ApellidoPaterno }

                                                                    };

                SqlDataReader reader = SqlHelper.GetDataReader("pListUsuario", searchParameters);

                if (reader.HasRows)
                {
                    lstUsuario = new List<UsuarioBE>();

                    while (reader.Read())
                    {
                        UsuarioBE oUsuario = new UsuarioBE();

                        oUsuario.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
                        oUsuario.Nombre = Convert.ToString(reader["Nombre"]);
                        oUsuario.ApellidoPaterno = Convert.ToString(reader["ApellidoPaterno"]);
                        oUsuario.ApellidoMaterno = Convert.ToString(reader["ApellidoMaterno"]);
                        oUsuario.DNI = Convert.ToString(reader["DNI"]);
                        oUsuario.MailTrabajo = Convert.ToString(reader["MailTrabajo"]);
                        
                        if (!Convert.IsDBNull(reader["Celular"]))
                        {
                            oUsuario.Celular = Convert.ToInt32(reader["Celular"]);
                        }

                        oUsuario.Login = Convert.ToString(reader["Login"]);
                        oUsuario.IdRol = Convert.ToInt32(reader["IdRol"]);

                        oUsuario.Rol = new RolBE();
                        oUsuario.Rol.Nombre = Convert.ToString(reader["ROL"]);

                        if (!Convert.IsDBNull(reader["FechaCaducidad"]))
                        {
                            oUsuario.FechaCaducidad = Convert.ToDateTime(reader["FechaCaducidad"]);
                        }

                        oUsuario.IdArea = Convert.ToInt32(reader["IdArea"]);

                        oUsuario.Area = Convert.ToString(reader["AREA"]);

                        oUsuario.Total = Convert.ToInt32(reader["Total"]);

                        lstUsuario.Add(oUsuario);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstUsuario;
        }

        public List<UsuarioBE> ExportUsuario(UsuarioBE objUsuario)
        {
            List<UsuarioBE> lstUsuario = null;
            Dictionary<string, object> searchParameters = null;

            try
            {
                searchParameters = new Dictionary<string, object>() {                                                                      
                                                                        { "@IdRol", objUsuario.IdRol },
                                                                        { "@IdArea", objUsuario.IdArea },
                                                                        { "@ApellidoPaterno", objUsuario.ApellidoPaterno }

                                                                    };

                SqlDataReader reader = SqlHelper.GetDataReader("pExportUsuario", searchParameters);

                if (reader.HasRows)
                {
                    lstUsuario = new List<UsuarioBE>();

                    while (reader.Read())
                    {
                        UsuarioBE oUsuario = new UsuarioBE();

                        oUsuario.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
                        oUsuario.Nombre = Convert.ToString(reader["Nombre"]);
                        oUsuario.ApellidoPaterno = Convert.ToString(reader["ApellidoPaterno"]);
                        oUsuario.ApellidoMaterno = Convert.ToString(reader["ApellidoMaterno"]);
                        oUsuario.DNI = Convert.ToString(reader["DNI"]);
                        oUsuario.MailTrabajo = Convert.ToString(reader["MailTrabajo"]);

                        if (!Convert.IsDBNull(reader["Celular"]))
                        {
                            oUsuario.Celular = Convert.ToInt32(reader["Celular"]);
                        }

                        oUsuario.Login = Convert.ToString(reader["Login"]);
                        oUsuario.IdRol = Convert.ToInt32(reader["IdRol"]);

                        oUsuario.Rol = new RolBE();
                        oUsuario.Rol.Nombre = Convert.ToString(reader["ROL"]);

                        if (!Convert.IsDBNull(reader["FechaCaducidad"]))
                        {
                            oUsuario.FechaCaducidad = Convert.ToDateTime(reader["FechaCaducidad"]);
                        }

                        oUsuario.IdArea = Convert.ToInt32(reader["IdArea"]);

                        oUsuario.Area = Convert.ToString(reader["AREA"]);
                       

                        lstUsuario.Add(oUsuario);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstUsuario;
        }
        public UsuarioBE ValidateAccess(UsuarioBE objUsuario)
        {

            UsuarioBE oUsuario = null;
            Dictionary<string, object> searchParameters = null;

            try
            {

                searchParameters = new Dictionary<string, object>() { { "@vcLogin", objUsuario.Login },
                                                                      { "@vcPassword", objUsuario.Password }
                                                                     };

                SqlDataReader reader = SqlHelper.GetDataReader("pValidateAcceso", searchParameters);

                if (reader.HasRows)
                {
                    oUsuario = new UsuarioBE();

                    reader.Read();                  
                    oUsuario.Valor = Convert.ToInt32(reader["VALOR"]);

                    if (oUsuario.Valor == 0)
                    {
                        oUsuario.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
                        oUsuario.Nombre = Convert.ToString(reader["Nombre"]);
                        oUsuario.ApellidoPaterno = Convert.ToString(reader["ApellidoPaterno"]);
                        oUsuario.ApellidoMaterno = Convert.ToString(reader["ApellidoMaterno"]);
                        oUsuario.DNI = Convert.ToString(reader["DNI"]);
                        oUsuario.MailTrabajo = Convert.ToString(reader["MailTrabajo"]);

                        if (!Convert.IsDBNull(reader["Celular"]))
                        {
                            oUsuario.Celular = Convert.ToInt32(reader["Celular"]);
                        }
                                                
                        oUsuario.Login = Convert.ToString(reader["Login"]);
                        oUsuario.Password = Convert.ToString(reader["Password"]);
                        oUsuario.Estado = Convert.ToInt32(reader["Estado"]);
                        oUsuario.IdArea = Convert.ToInt32(reader["IdArea"]);
                        oUsuario.IdRol = Convert.ToInt32(reader["IdRol"]);
                        oUsuario.IdUsuarioRegistro = Convert.ToInt32(reader["UsuarioRegistro"]);
                        oUsuario.FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]);
                    }
                    
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oUsuario;

        }

        public List<UsuarioBE> GetUsuario(UsuarioBE objUsuario)
        {
            List<UsuarioBE> lstUsuario = null;
            Dictionary<string, object> searchParameters = null;

            try
            {
                searchParameters = new Dictionary<string, object>() {
                                                                        { "@IdUsuario", objUsuario.IdUsuario }                                                                                                                                            
                                                                    
                                                                    };

                SqlDataReader reader = SqlHelper.GetDataReader("pGetUsuario", searchParameters);

                if (reader.HasRows)
                {
                    lstUsuario = new List<UsuarioBE>();

                    while (reader.Read())
                    {
                        UsuarioBE oUsuario = new UsuarioBE();

                        oUsuario.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
                        oUsuario.Nombre = Convert.ToString(reader["Nombre"]);
                        oUsuario.ApellidoPaterno = Convert.ToString(reader["ApellidoPaterno"]);
                        oUsuario.ApellidoMaterno = Convert.ToString(reader["ApellidoMaterno"]);
                        oUsuario.DNI = Convert.ToString(reader["DNI"]);
                        oUsuario.MailTrabajo = Convert.ToString(reader["MailTrabajo"]);
                        oUsuario.Celular = Convert.ToInt32(reader["Celular"]);
                        oUsuario.Login = Convert.ToString(reader["Login"]);
                        oUsuario.Password = Convert.ToString(reader["Password"]);

                        if (!Convert.IsDBNull(reader["FechaCaducidad"]))
                        {
                            oUsuario.FechaCaducidad = Convert.ToDateTime(reader["FechaCaducidad"]);
                        }

                        oUsuario.IdArea = Convert.ToInt32(reader["IdArea"]);

                        oUsuario.Area = Convert.ToString(reader["AREA"]);

                        oUsuario.IdRol = Convert.ToInt32(reader["IdRol"]);

                        lstUsuario.Add(oUsuario);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstUsuario;
        }

       public Boolean UpdateUsuario(UsuarioBE objUsuario)
        {

            Dictionary<string, object> parameters = null;

            try
            {
                parameters = new Dictionary<string, object>() { { "@IdUsuario", objUsuario.IdUsuario },
                                                                { "@Login", objUsuario.Login },
                                                                { "@Nombre", objUsuario.Nombre },
                                                                { "@ApellidoPaterno", objUsuario.ApellidoPaterno},
                                                                { "@ApellidoMaterno", objUsuario.ApellidoMaterno},
                                                                { "@IdArea", objUsuario.IdArea},
                                                                { "@DNI", objUsuario.DNI},
                                                                { "@Celular", objUsuario.Celular},
                                                                { "@MailTrabajo", objUsuario.MailTrabajo},
                                                                { "@IdRol", objUsuario.IdRol},
                                                                { "@FechaCaducidad", objUsuario.FechaCaducidad},
                                                                { "@IdUsuarioActualiza", objUsuario.IdUsuarioActualiza}
                                                              };

                SqlHelper.ExecuteNonQuery("pUpdateUsuario", parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

        public Boolean SaveUsuario(UsuarioBE objUsuario)
        {

            Dictionary<string, object> parameters = null;

            try
            {
                parameters = new Dictionary<string, object>() {                                                               
                                                                { "@Nombre", objUsuario.Nombre },
                                                                { "@ApellidoPaterno", objUsuario.ApellidoPaterno},
                                                                { "@ApellidoMaterno", objUsuario.ApellidoMaterno},
                                                                { "@IdArea", objUsuario.IdArea},
                                                                { "@DNI", objUsuario.DNI},
                                                                { "@Celular", objUsuario.Celular},
                                                                { "@MailTrabajo", objUsuario.MailTrabajo},
                                                                { "@IdRol", objUsuario.IdRol},
                                                                { "@FechaCaducidad", objUsuario.FechaCaducidad},
                                                                { "@Login", objUsuario.Login},
                                                                { "@Password", objUsuario.Password},
                                                                { "@IdUsuarioRegistro", objUsuario.IdUsuarioRegistro}
                                                              };

                SqlHelper.ExecuteNonQuery("pSaveUsuario", parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

        public Boolean DeleteUsuario(UsuarioBE objUsuario)
        {

            Dictionary<string, object> parameters = null;

            try
            {
                parameters = new Dictionary<string, object>() {
                                                               { "@IdUsuario", objUsuario.IdUsuario }                                                             
                                                              };

                SqlHelper.ExecuteNonQuery("pDeleteUsuario", parameters);

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
