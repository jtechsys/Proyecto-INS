using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using doMain.INS.Entity;
using doMain.INS.Business;
using doMain.Utils;

namespace slnPryINS.Calidad
{
    public partial class frmLogeo : System.Web.UI.Page
    {
        public string Usuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
            }
        }
        
        void SAccederIngreso()
        {
            UsuarioBLL capanegocios = new UsuarioBLL();
            UsuarioBE objeto = new UsuarioBE();
            UsuarioBE objUsuario = new UsuarioBE();

            string message = string.Empty;
            string errores = string.Empty;
            

             try
             {
                objUsuario.Login = txtUsuario.Text.Trim();
                //objUsuario.Password = txtClave.Text.Trim();
                objUsuario.Password = doMain.Utils.EncryptorUtils.Encriptar(txtClave.Text.Trim());

                objeto = capanegocios.ValidateAccess(objUsuario);               

                switch (objeto.Valor)
                {
                    case 0:

                        Session["IdUsuario"] = objeto.IdUsuario;
                        Session["IdArea"] = objeto.IdArea;
                        Session["IdRol"] = objeto.IdRol;
                        Session["Login"] = objeto.Login;

                        if (objeto.IdArea == Convert.ToInt32(Constante.Area.POOL_SECRETARIAL))
                        {
                            Response.Redirect("frmListExpediente.aspx");
                        }
                        else if (objeto.IdArea == Convert.ToInt32(Constante.Area.RECEPCION_MUESTRA))
                        {
                            Response.Redirect("frmListRM.aspx");
                        }
                        else if (objeto.IdArea == Convert.ToInt32(Constante.Area.FISICOQUIMICA))
                        {
                            Response.Redirect("frmListFisicoquimica.aspx");
                        }
                        else if (objeto.IdArea == Convert.ToInt32(Constante.Area.CERTIFICACION))
                        {
                            Response.Redirect("frmListFisicoquimica.aspx");
                        }
                        else
                        {
                            message += "\n El usuario no está pertenece a ninguna área.";
                        }

                        break;

                    case 1:

                        message += "\n Su fecha de caducidad a expirado.";
                        break;

                    case 2:

                        message += "\n La cuenta del usuario está desactivada.";
                        break;

                    case 3:

                        message += "\n La cuenta y password ingresado son errónea.";
                        break;

                };

                //Message                
                if (!message.Equals(string.Empty))
                {                    
                    lblMensaje.Text = message; 
                }
                

            }
            catch (Exception ex)
            {
                errores = ex.Message;
                lblMensaje.Text = errores;
            }
        }

        protected void btnAcceder_Click(object sender, EventArgs e)
        {
            SAccederIngreso();
        }

    }
}