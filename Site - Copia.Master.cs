using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

using System.Data.SqlClient;
using doMain.INS.Entity;
using doMain.INS.Business;
using System.Linq;
using System.Web.Script.Serialization;

namespace doMain.INS.Presentation
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        private string errores;
        private MenuItem mnuNewMenuItem;

        protected void Page_Init(object sender, EventArgs e)
        {
            // El código siguiente ayuda a proteger frente a ataques XSRF
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Utilizar el token Anti-XSRF de la cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generar un nuevo token Anti-XSRF y guardarlo en la cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Establecer token Anti-XSRF
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validar el token Anti-XSRF
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Error de validación del token Anti-XSRF.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SCargarOpcionesModulo(1);
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }


        private void SCargarOpcionesModulo(Int32 pintIdRol)
        {
            ModuloRolBLL capanegocios = new ModuloRolBLL();            
            ModuloRolBE objModuloRol = new ModuloRolBE();

            try
            {
                objModuloRol.IdRol = pintIdRol;

                List<ModuloRolBE> listModuloRol = new List<ModuloRolBE>();
                listModuloRol = capanegocios.List(objModuloRol);

                foreach (ModuloRolBE oModuloRol in listModuloRol)
                {
                    if (oModuloRol.IdModulo.Equals(oModuloRol.Modulo.IdPadre))
                    {
                        mnuPrincipal.Items.Add(new MenuItem(oModuloRol.Modulo.Modulo.ToString(), oModuloRol.IdModulo.ToString()));
                    }
                }

                foreach (ModuloRolBE oModuloRol in listModuloRol)
                {

                    if (oModuloRol.Modulo.Nivel > 1)
                    {
                        MenuItem mnu = new MenuItem(oModuloRol.Modulo.Modulo.ToString(), oModuloRol.IdModulo.ToString());
                        mnuPrincipal.FindItem(oModuloRol.Modulo.IdPadre.ToString()).ChildItems.Add(mnu);
                    }
                }

            }
            catch (Exception ex)
            {
                errores = ex.Message;                
            }
        }

        private void SCargarOpcionesModulo1(Int32 pintIdRol)
        {
            /*
                //Recorremos cada elemento del datatable para poder determinar cuales son elementos hijos
                if (oModuloRol.IdModulo.Equals(oModuloRol.Modulo.IdPadre))                      
                {
                    MenuItem mnuMenuItem = new MenuItem();
                    mnuMenuItem.Value = oModuloRol.IdModulo.ToString();
                    mnuMenuItem.Text = oModuloRol.Modulo.Modulo.ToString();

                    //Agregamos el Nuevo MenuItem al MenuItem que viene de un nivel superior.
                    mnuPrincipal.Items.Add(mnuMenuItem);

                    //Hacemos un llamado al metodo recursivo encargado de generar el árbol del menú.
                    AddMenuItem(mnuMenuItem, listModuloRol);
                }                  
             */

            /*
               foreach (DataRow dr in dt.Select("ParentID >" + 0))
              {
                  MenuItem mnu = new MenuItem(dr["MenuName"].ToString(),
                                 dr["MenuID"].ToString(),
                                 "", dr["MenuLocation"].ToString());
                  menuBar.FindItem(dr["ParentID"].ToString()).ChildItems.Add(mnu);
              }
              */

        }


        private void AddMenuItem(MenuItem mnuMenuItem,List<ModuloRolBE> lstMenuItems )
        {
            //Recorremos cada elemento del datatable para poder determinar cuales son elementos hijos
            foreach (ModuloRolBE oModuloRol in lstMenuItems)
            {
                if (oModuloRol.Modulo.IdPadre.Equals(mnuMenuItem.Value) && !oModuloRol.IdModulo.Equals(oModuloRol.Modulo.IdPadre))
                {
                    MenuItem mnuNewMenuItem = new MenuItem();                    

                    mnuNewMenuItem.Value = oModuloRol.IdModulo.ToString();
                    mnuNewMenuItem.Text = oModuloRol.Modulo.Modulo.ToString();

                    //Agregamos el Nuevo MenuItem al MenuItem que viene de un nivel superior.
                    mnuMenuItem.ChildItems.Add(mnuNewMenuItem);

                    //llamada recursiva para ver si el nuevo menú ítem aun tiene elementos hijos
                    AddMenuItem(mnuNewMenuItem, lstMenuItems);

                }

            }


        }


        private List<ModuloBE> GetMenuTree(List<ModuloRolBE> List, int? pIdPadre)
        {
            ModuloBE oModulo = null;
            oModulo = new ModuloBE();


            return List.Where(x => x.Modulo.IdPadre == pIdPadre).Select(x => new ModuloBE()
            {
                IdModulo = x.IdModulo,
                Modulo = x.Modulo.Modulo,
                IdPadre = x.Modulo.IdPadre,
                Estado = x.Modulo.Estado,
                List = GetMenuTree(List, x.IdModulo)

            }).ToList();

        }
    }

}