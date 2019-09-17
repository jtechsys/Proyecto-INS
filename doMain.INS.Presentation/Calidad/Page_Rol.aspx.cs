using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using doMain.INS.Entity;
using doMain.INS.Business;

namespace doMain.INS.Presentation.Calidad
{
    public partial class Page_Rol : System.Web.UI.Page
    {
        private string errores;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        private void ListRol()
        {
            try
            {



            }
            catch (Exception ex)
            {
                errores = ex.Message;
            }
        }

    }
}