using System;
using System.Collections.Generic;
using System.Text;

namespace doMain.Utils
{
    public class NumbersToLetters
    {
        const string LetraPlural = "s";
        const string TextoMil = " mil";
        const string TextoMillones = " millones";

        private bool esMasculino; //genero del numero en letra
        private long numeroDecimales; //nº de decimales a pasar a letra
        private string textoAcomParteEntera; //texto para acompañar a la parte entera
        private string textoAcomParteDecimal; //texto para acompañar a la parte decimal
        private string textoAcomParteEnteraSin; //textos en singular
        private string textoAcomParteDecimalSin;
        private string textoAcom; //solo se usa en EnteroAletrasRec al mirar al caso Especial nº1

        private string[] listaUnidades; //continene los textos para las unidades
        private string[] listaDecenas;	//decenas
        private string[] listaCentenas;	//y centenas

        public NumbersToLetters()
            : this(true, 2, string.Empty, string.Empty)
        {
        }
        public NumbersToLetters(bool esMasculino)
            : this(esMasculino, 2, string.Empty, string.Empty)
        {
        }
        public NumbersToLetters(bool esMasculino, long numeroDecimales)
            : this(esMasculino, numeroDecimales, string.Empty, string.Empty)
        {
        }
        public NumbersToLetters(bool esMasculino, long numeroDecimales, string textoAcomParteEntera, string textoAcomParteDecimal)
        {
            this.esMasculino = esMasculino;

            NumeroDecimales = numeroDecimales;
            TextoAcomParteEntera = textoAcomParteEntera;
            TextoAcomParteDecimal = textoAcomParteDecimal;

            listaUnidades = new string[30];
            listaDecenas = new string[10];
            listaCentenas = new string[10];
            this.InicioListasTexto();
        }
        #region Propiedades de la clase
        public bool GeneroMasculino
        {
            get
            {
                return this.esMasculino;
            }
            set
            {
                this.esMasculino = value;
                this.InicioListasTexto(); //es necesario actualizar las listas segun el género
            }
        }
        public long NumeroDecimales
        {
            get
            {
                return this.numeroDecimales;
            }
            set
            {
                if (value < 0)
                    this.numeroDecimales = 0;
                else
                    this.numeroDecimales = value;
            }
        }

        public string TextoAcomParteEntera
        {
            get
            {
                return this.textoAcomParteEntera;
            }
            set
            {
                if (value == string.Empty)
                    this.textoAcomParteEntera = this.textoAcomParteEnteraSin = value;
                else
                {
                    this.textoAcomParteEntera = " " + value;
                    this.textoAcomParteEnteraSin = this.textoAcomParteEntera.Substring(0, this.textoAcomParteEntera.Length - 1);
                }
            }
        }
        public string TextoAcomParteDecimal
        {
            get
            {
                return this.textoAcomParteDecimal;
            }
            set
            {
                if (value == string.Empty)
                    this.textoAcomParteDecimal = this.textoAcomParteDecimalSin = value;
                else
                {
                    this.textoAcomParteDecimal = " " + value;
                    this.textoAcomParteDecimalSin = this.textoAcomParteDecimal.Substring(0, this.textoAcomParteDecimal.Length - 1);
                }
            }
        }
        #endregion
        /// <summary>
        /// Inicializa las matrices de texto con los valores adecuados
        /// Ha de llamarse cada vez que cambia la propiedad GeneroMasculino
        /// </summary>
        private void InicioListasTexto()
        {
            string letraGenero;
            if (esMasculino)
                letraGenero = "o";
            else
                letraGenero = "a";
            // listaUnidades[0], listaDecenas[0 to 2] y listaCentenas[0] no se inicializan explicitamente
            listaUnidades[1] = "un" + letraGenero;// 'género
            listaUnidades[2] = "dos";
            listaUnidades[3] = "tres";
            listaUnidades[4] = "cuatro";
            listaUnidades[5] = "cinco";
            listaUnidades[6] = "seis";
            listaUnidades[7] = "siete";
            listaUnidades[8] = "ocho";
            listaUnidades[9] = "nueve";
            listaUnidades[10] = "diez";
            listaUnidades[11] = "once";
            listaUnidades[12] = "doce";
            listaUnidades[13] = "trece";
            listaUnidades[14] = "catorce";
            listaUnidades[15] = "quince";
            listaUnidades[16] = "dieciseis";
            listaUnidades[17] = "diecisiete";
            listaUnidades[18] = "dieciocho";
            listaUnidades[19] = "diecinueve";
            listaUnidades[20] = "veinte";
            listaUnidades[21] = "veintiun" + letraGenero; //género
            listaUnidades[22] = "veintidos";
            listaUnidades[23] = "veintitres";
            listaUnidades[24] = "veinticuatro";
            listaUnidades[25] = "veinticinco";
            listaUnidades[26] = "veintiseis";
            listaUnidades[27] = "veintisiete";
            listaUnidades[28] = "veintiocho";
            listaUnidades[29] = "veintinueve";

            listaDecenas[3] = "treinta";
            listaDecenas[4] = "cuarenta";
            listaDecenas[5] = "cincuenta";
            listaDecenas[6] = "sesenta";
            listaDecenas[7] = "setenta";
            listaDecenas[8] = "ochenta";
            listaDecenas[9] = "noventa";

            listaCentenas[1] = "ciento";
            listaCentenas[2] = "doscient" + letraGenero + LetraPlural;
            listaCentenas[3] = "trescient" + letraGenero + LetraPlural;
            listaCentenas[4] = "cuatrocient" + letraGenero + LetraPlural;
            listaCentenas[5] = "quinient" + letraGenero + LetraPlural;
            listaCentenas[6] = "seiscient" + letraGenero + LetraPlural;
            listaCentenas[7] = "setecient" + letraGenero + LetraPlural;
            listaCentenas[8] = "ochocient" + letraGenero + LetraPlural;
            listaCentenas[9] = "novecient" + letraGenero + LetraPlural;
        }
        /// <summary>
        /// Convierte un número decimal a su equivalente en texto.
        /// Este es el punto de inicio de la conversión a texto. Sobrecargado para decimal y double.
        /// En caso de error genera una OverflowExcpetion
        /// </summary>
        public string ToLetters(decimal num)
        {
            string textoSigno = string.Empty, textoParteEntera, textoParteDecimal = string.Empty;
            decimal parteDecimal;
            long parteEntera;
            long gruposDeTres;
            if (num < 0)
            {
                textoSigno = "menos ";
                num = num * -1;
            }
            //calculo la parte entera y la decimal, ha de hacerse con Decimal, con Double hay problemas de precisión ej: 31,31
            parteEntera = (long)num;
            parteDecimal = num - parteEntera; //o parteDecimal=num-Math.Floor(num);

            gruposDeTres = (long)Math.Ceiling(NumeroCifrasEntero(parteEntera) / 3.0);
            textoAcom = this.textoAcomParteEntera;
            textoParteEntera = EnteroAletrasRec(parteEntera, gruposDeTres);
            if ((parteDecimal != 0) && (this.numeroDecimales != 0)) //si hay decimales y se quieren decimales...
            {
                long decimalEntero = DecimalesAentero(parteDecimal, numeroDecimales);
                gruposDeTres = (long)Math.Ceiling(NumeroCifrasEntero(decimalEntero) / 3.0);
                textoAcom = this.textoAcomParteDecimal;
                //            textoParteDecimal = " CON " + EnteroAletrasRec(decimalEntero, gruposDeTres) + (decimalEntero == 1 ? this.textoAcomParteDecimalSin : this.textoAcomParteDecimal);
                string centavos = decimalEntero.ToString();
                if (decimalEntero.ToString().Length == 1)
                {
                    centavos = "0" + centavos;
                }
                textoParteDecimal = " CON " + centavos + "/100 ";
            }
            else
            {
                textoParteDecimal = " CON 00/100 ";
            }
            return textoSigno + textoParteEntera + (parteEntera == 1 ? textoAcomParteEnteraSin : textoAcomParteEntera) + textoParteDecimal;
        }
        /// <summary>
        /// Convierte un número double a su equivalente en texto.
        /// Este es el punto de inicio de la conversión a texto.
        /// En caso de error genera una OverflowExcpetion
        /// </summary>
        public string ToLetters(double num)
        {
            return this.ToLetters((decimal)num);
        }
        /// <summary>
        /// Convierte un número entero con un máximo de 18 dígitos en su equivalente en texto.
        /// En caso de error genera una OverflowExcpetion
        /// </summary>
        private string EnteroAletrasRec(long num, long gruposDeTres)
        {
            long parte1 = 0, parte2 = 0;
            string textoParte1 = string.Empty, textoParte2 = string.Empty;
            string enteroAletras;

            switch (gruposDeTres)
            {
                case 1:
                    enteroAletras = CentenasAletras(num);
                    if ((esMasculino) && (textoAcom != string.Empty))
                        //caso especial nº1
                        if (num == 1)
                            enteroAletras = enteroAletras.Substring(0, enteroAletras.Length - 1);
                        else
                            QuitarUltimaLetra(ref enteroAletras, num);
                    break;
                case 2 //miles, primero saco de num la parte2 con las cifras de los miles y parte1 el resto
                :

                    parte2 = num / 1000;
                    parte1 = num - (parte2 * 1000);

                    if (parte2 == 1)
                        textoParte2 = "mil"; //caso especial de mil, no se usa la constante TextoMil por los espacios
                    else
                        if (parte2 != 0)
                        {
                            textoParte2 = CentenasAletras(parte2);
                            if (esMasculino)
                                //Para los miles sólo quito la última letra si el genero es masculino ej 21000 femenino-->veintiuna mil
                                QuitarUltimaLetra(ref textoParte2, parte2); //	si es masculino-->veintiun mil
                            textoParte2 = textoParte2 + TextoMil;
                        }
                    if (parte1 != 0)
                        textoParte1 = (textoParte2 == string.Empty ? "" : " ") + EnteroAletrasRec(parte1, 1);
                    enteroAletras = textoParte2 + textoParte1;
                    break;
                case 3 //millones. El resto de casos se podrian a grupar en uno solo, ya que solo cambian las cosntantes
                :

                    parte2 = num / 1000000; //para calcular la parte1 q son las cifrasde los millones y la parte2 el resto de cifras
                    parte1 = num - (parte2 * 1000000);

                    if (parte2 == 1)
                        textoParte2 = "un millón"; //caso especial de millón, no se usa la constante TextoMillones por los espacios
                    else
                        if (parte2 != 0)
                        {
                            textoParte2 = CentenasAletras(parte2);
                            QuitarUltimaLetra(ref textoParte2, parte2);
                            textoParte2 = textoParte2 + TextoMillones;
                        }
                    if (parte1 != 0)
                        textoParte1 = (textoParte2 == string.Empty ? "" : " ") + EnteroAletrasRec(parte1, 2);
                    enteroAletras = textoParte2 + textoParte1;
                    break;
                case 4 //Miles de Millones
                :

                    parte2 = num / 1000000000;
                    parte1 = num - (parte2 * 1000000000);

                    if (parte2 == 1)
                        textoParte2 = "mil";
                    else
                        if (parte2 != 0)
                        {
                            textoParte2 = CentenasAletras(parte2);
                            if (esMasculino)
                                QuitarUltimaLetra(ref textoParte2, parte2);
                            textoParte2 = textoParte2 + TextoMil;
                        }
                    if (parte1 != 0)
                        textoParte1 = (textoParte2 == string.Empty ? "" : " ") + EnteroAletrasRec(parte1, 3);
                    enteroAletras = textoParte2 + textoParte1;
                    break;
                case 5 //billones
                :

                    parte2 = num / 1000000000000;
                    parte1 = num - (parte2 * 1000000000000);

                    if (parte2 == 1)
                        textoParte2 = "un billón"; //caso especial de millón, no se usa la constante TextoMillones por los espacios
                    else
                        if (parte2 != 0)
                        {
                            textoParte2 = CentenasAletras(parte2);
                            QuitarUltimaLetra(ref textoParte2, parte2);
                            textoParte2 = textoParte2 + " billones";
                        }
                    if (parte1 != 0)
                        textoParte1 = (textoParte2 == string.Empty ? "" : " ") + EnteroAletrasRec(parte1, 4);
                    enteroAletras = textoParte2 + textoParte1;
                    break;
                case 6 //miles de billones, luego vendria trillones, miles de trillones, cuatrillones, etc...
                :

                    parte2 = num / 1000000000000000;
                    parte1 = num - (parte2 * 1000000000000000);

                    if (parte2 == 1)
                        textoParte2 = "mil"; //caso especial de mil
                    else
                        if (parte2 != 0)
                        {
                            textoParte2 = CentenasAletras(parte2);
                            if (esMasculino)
                                QuitarUltimaLetra(ref textoParte2, parte2);
                            textoParte2 = textoParte2 + TextoMil;
                        }
                    if (parte1 != 0)
                        textoParte1 = (textoParte2 == string.Empty ? "" : " ") + EnteroAletrasRec(parte1, 5);
                    enteroAletras = textoParte2 + textoParte1;
                    break;
                default:
                    throw new OverflowException();
                //enteroAletras= "Error Nº Demasiado Grande";
                //break; //evito el warning de código inalcanzable
            }
            return enteroAletras.ToUpper().ToString();
        }
        /// <summary>
        /// Convierte un número entero en el rango 0..999 en su equivalente en texto.
        /// No tiene en cuenta los textos de acompañamiento Ej 21-viuntiuno 
        /// En caso de error genera una OverflowExcpetion
        /// </summary>
        private string CentenasAletras(long num)
        {
            long centenas = 0, decenas = 0, unidades = 0;
            string centenasAletras = string.Empty;

            if ((num > 999) || (num < 0))
                throw new OverflowException();
            if (num == 100)
                //caso especial de 100
                return "cien";
            if (num == 0)
                return "cero";

            centenas = num / 100;
            decenas = (num / 10) - (10 * centenas);
            unidades = num - (10 * decenas) - (100 * centenas);

            if (decenas > 2)
            {
                centenasAletras = this.listaDecenas[decenas];
                if (unidades > 0)
                    centenasAletras = centenasAletras + " y " + this.listaUnidades[unidades];
            }
            else
                centenasAletras = this.listaUnidades[decenas * 10 + unidades];
            if (centenas > 0)
            {
                if (centenasAletras == string.Empty)
                    centenasAletras = this.listaCentenas[centenas];
                else
                    centenasAletras = this.listaCentenas[centenas] + " " + centenasAletras;
            }
            return centenasAletras;
        }
        /// <summary>
        /// cuenta el nº de cifras que tiene un número entero
        /// </summary>
        private long NumeroCifrasEntero(long num)
        {
            long cont = 0;
            while (num != 0)
            {
                cont++;
                num = num / 10;
            }
            if (cont == 0)
                cont = 1;
            return cont;
        }
        /// <summary>
        /// Pasa los decimales que se piden a la parte entera devolviendola. Sobrecargado para Double y Decimal.
        /// </summary>
        private long DecimalesAentero(double parteDecimal, long numeroDecimales)
        {
            double aux = 10;
            aux = Math.Pow(aux, numeroDecimales);
            double numeroDecimal = parteDecimal * aux;
            return (long)numeroDecimal;
        }
        /// <summary>
        /// Pasa los decimales que se piden a la parte entera devolviendola. Sobrecargado para Double y Decimal.
        /// </summary>
        private long DecimalesAentero(decimal parteDecimal, long numeroDecimales)
        {
            decimal aux = 1.0m;
            for (long x = 0; x < numeroDecimales; x++)
                aux = aux * 10.0m;
            //aux=Math.Pow(aux,numeroDecimales);
            decimal numeroDecimal = parteDecimal * aux;
            return (long)numeroDecimal;
        }
        /// <summary>
        /// Esta rutina quita la ultima letra a partir de los miles
        /// (y los miles sólo si el género es masculino, se controla en el método EnterosAletrasRec)
        /// y si las decenas de millar son >11 y las unidades de millar=1
        /// 21000--> veintiun mil O veintiuna mil (depende del genero del texto)
        /// 71000000--> setenta y un millones (siempre "un" nunca "una" o "uno")
        /// </summary>
        private void QuitarUltimaLetra(ref string texto, long num)
        {
            //decenas >1 y unidades=1
            long centenas = 0, decenas = 0, unidades = 0;

            centenas = num / 100;
            decenas = (num / 10) - (10 * centenas);
            unidades = num - (10 * decenas) - (100 * centenas); //num-((num/10)*10)
            if ((decenas > 1) && (unidades == 1))
            {
                texto = texto.Substring(0, texto.Length - 1); //quito la última letra
                if (decenas == 2 && unidades == 1)
                    //deveria poner el acento a veintiún
                    texto = texto.Replace("u", "ú");
            }
        }
    }
}
