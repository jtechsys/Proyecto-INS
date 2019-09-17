using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for Constante
/// </summary>
public class Constante
{
    public enum Area
    {
        POOL_SECRETARIAL = 1,
        RECEPCION_MUESTRA = 2,
        SUBCONTRATACION = 3,
        ESTANDARES = 4,
        FISICOQUIMICA = 5,
        MICROBIOLOGIA = 6,
        CERTIFICACION = 7
    }
    public enum TipoGrabado
    {
        NUEVO = 1,
        ACTUALIZA = 2,        
        EXPORTA = 3
    }
    public enum Numeros
    {
        Cero = 0,
        Uno = 1,
        Dos = 2,
        Tres = 3,
        Cuatro = 4,
        Cinco = 5,
        Seis = 6,
        Siete = 7,
        Ocho = 8,
        Nueve = 9,
        Diez = 10,
    }; 
    public enum Estados
    {
        Enviado = 1,
        Recepcionado = 2,
        Custodia_Contramuestra = 3,
        Custodia_Custodia = 4,
        Custodia_Devuelto=5,
        Devuelto = 20,
        Pendiente = 21,
        Reportado = 22,
        Anulado = 23,
        Creado = 24
    };
    public void MsgBox(Label lblMessageOut, string MessageOut, string Tipo = "[ Success, Info, Warning, Error ]")
    {
        lblMessageOut.Text = MessageOut;
        lblMessageOut.ForeColor = System.Drawing.Color.Black;
        lblMessageOut.BackColor = System.Drawing.Color.White;

        if (Tipo == "Info")
        {
            lblMessageOut.ForeColor = System.Drawing.Color.Black;
        }
        if (Tipo == "Error")
        {
            lblMessageOut.ForeColor = System.Drawing.Color.Red;
        }
        if (Tipo == "Warning")
        {
            lblMessageOut.ForeColor = System.Drawing.Color.DarkOrange;
        }
        if (Tipo == "Success")
        {
            lblMessageOut.ForeColor = System.Drawing.Color.Green;
        }
    }
    public enum Perfil
    {
        ADMINISTRADOR = 1,
        COORDINADOR = 2,
        ANALISTA_EVALUADOR = 3,
        ANALISTA_EJECUTOR = 4,
    }

}