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
        private int IdArea;
        private string errores;
        private MenuItem mnuNewMenuItem;            

        protected void Page_Load(object sender, EventArgs e)
        {           
            //Mostrar usuario
            lblLogeo.Text = "Usuario: " + Convert.ToString(Session["Login"]);

            if (Convert.ToInt32(Session["IdArea"]) == Convert.ToInt32(Constante.Numeros.Uno))
            {
                lblArea.Text = "Area: Pool Secretarial";
            }
            else if (Convert.ToInt32(Session["IdArea"]) == Convert.ToInt32(Constante.Numeros.Dos))
            {
                lblArea.Text = "Area: Recepción de Muestras";
            }
            else if (Convert.ToInt32(Session["IdArea"]) == Convert.ToInt32(Constante.Numeros.Cinco))
            {
                lblArea.Text = "Area: Fisicoquímica";
            }
            else if (Convert.ToInt32(Session["IdArea"]) == Convert.ToInt32(Constante.Numeros.Siete))
            {
                lblArea.Text = "Area: Certificación";
            }

            SCargarOpcionesModulo(Convert.ToInt32(Session["IdRol"]));
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

                if (listModuloRol != null)
                {

                    foreach (ModuloRolBE oModuloRol in listModuloRol)
                    {
                        if (oModuloRol.Modulo.IdPadre.Equals(oModuloRol.Modulo.Nivel))
                        {
                            mnuPrincipal.Items.Add(new MenuItem(oModuloRol.Modulo.Modulo.ToString(), oModuloRol.IdModulo.ToString()));
                        }
                    }

                    foreach (ModuloRolBE oModuloRol in listModuloRol)
                    {

                        if (oModuloRol.Modulo.Nivel > 1)
                        {
                            MenuItem mnu = new MenuItem(oModuloRol.Modulo.Modulo.ToString(), oModuloRol.IdModulo.ToString());
                            mnu.NavigateUrl = oModuloRol.Modulo.Url.ToString();
                            mnu.ToolTip = oModuloRol.Modulo.Tooltip.ToString();
                            mnuPrincipal.FindItem(oModuloRol.Modulo.IdPadre.ToString()).ChildItems.Add(mnu);
                            
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                errores = ex.Message;                
            }
        }      


        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            Response.AppendHeader("Cache-Control", "no-store");
            Response.Cookies.Clear();
            Response.Redirect("~/index.aspx");
        }
    }

}