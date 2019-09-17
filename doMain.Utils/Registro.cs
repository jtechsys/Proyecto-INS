using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace doMain.Utils
{
    public class Registro
    {
        public static void GrabarRegistro(byte Tipo, string SubClave, string  Clave, string Valor)
        {
            RegistryKey regKey;
            switch (Tipo)
            {
              	case 0:
                    {
                        regKey = Registry.LocalMachine.OpenSubKey(SubClave, true);
                        if ( regKey == null)
                        {
                            regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", true);
                            regKey.CreateSubKey(SubClave);
                            regKey.Close();
                        }
                        string sClave = "SOFTWARE\\" + SubClave;
                        regKey = Registry.LocalMachine.OpenSubKey(sClave, true);
                        regKey.SetValue(Clave, Valor);
                        regKey.Close();
                        break;
                    }
                case 1:
                    {
                        regKey = Registry.CurrentUser.OpenSubKey(SubClave, true);
                        if (regKey == null)
                        {
                            regKey = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);
                            regKey.CreateSubKey(SubClave);
                            regKey.Close();
                        }
                        string sClave = "SOFTWARE\\" + SubClave;
                        regKey = Registry.CurrentUser.OpenSubKey(sClave, true);
                        regKey.SetValue(Clave, Valor);
                        regKey.Close();
                        break;
                    }
            }
        }

        public static string LeerRegistro(int Tipo, string SubClave, string Clave, string Valor)
        {
            RegistryKey regKey;
            string ValorLocal="";

            switch (Tipo)
            {
                case 0:
                    {
                        string sClave = "SOFTWARE\\" + SubClave;
                        regKey = Registry.LocalMachine.OpenSubKey(sClave, true);
                        if (regKey == null)
                            ValorLocal = Valor.ToString();
                        else
                        {
                            ValorLocal = regKey.GetValue(Clave, Valor).ToString();
                            regKey.Close();
                        }
                        break;
                    }
                case 1:
                    {
                        string sClave = "SOFTWARE\\" + SubClave;
                        regKey = Registry.CurrentUser.OpenSubKey(sClave, true);
                        if (regKey == null)
                            ValorLocal = Valor.ToString();
                        else
                        {
                            ValorLocal = regKey.GetValue(Clave, Valor).ToString();
                            regKey.Close();
                        }
                        break;
                    }
            }
            return ValorLocal;
        }
    }
}
