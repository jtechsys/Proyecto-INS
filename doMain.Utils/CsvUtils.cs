using System;
using System.Collections.Generic;
using System.Text;


using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data.Odbc;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Globalization;


namespace doMain.Utils
{
    public class CsvUtils
    {


        public void CreateCSVfile(DataTable dtable, string strFilePath)
        {

            StreamWriter sw = new StreamWriter(strFilePath, false, System.Text.Encoding.UTF8);
            int icolcount = dtable.Columns.Count;
            
            foreach (DataRow drow in dtable.Rows)
            {
                for (int i = 0; i < icolcount; i++)
                {
                    //if (!Convert.IsDBNull(drow[i]))
                    //{
                    sw.Write(drow[i].ToString());
                    //}
                    //else
                    //{

                    //}
                    //if (i < icolcount - 1)
                    //{
                    //    //sw.Write("\t");
                    //    sw.Write("");
                    //}
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
            //StringBuilder sb = new StringBuilder();
            //foreach (DataRow drow in dtable.Rows)
            //{
            //    string rowtext = "";
            //    for (int i = 0; i < icolcount; i++)
            //    {
            //        string val = drow[i].ToString();
            //        rowtext += val;
            //        //if (!Convert.IsDBNull(drow[i]))
            //        //{
            //        //    sw.Write(drow[i].ToString());
            //        //}
            //        //else
            //        //{

            //        //}
            //        //if (i < icolcount - 1)
            //        //{
            //        //    //sw.Write("\t");
            //        //    sw.Write("");
            //        //}
            //    }
            //    sb.AppendLine(rowtext);
            //}

            //FileUtils.EscribirTexto(strFilePath, sb.ToString());

        }

        private string fileNevCSV;	//name (with extension) of file to import - field
        //private string fileCSV;		//full file name
        private string dirCSV = null;	//directory of file to import
        public string //name (with extension) of file to import - property
        FileNevCSV
        {
            get
            {
                return fileNevCSV;
            }
            set
            {
                fileNevCSV = value;
            }
        }



        private void Format(string separator)
        {
            
            
                strFormat = "Delimited(" + separator + ")";
               

           
        }

        // Encoding selection
        private void Encoding()
        {
          
                strEncoding = "ANSI";
               
        }

        private void writeSchema(bool cabecera)
        {

            FileStream fsOutput = new FileStream(this.dirCSV + "\\schema.ini", FileMode.Create, FileAccess.Write);
            StreamWriter srOutput = new StreamWriter(fsOutput);
            string s1, s2, s3, s4, s5;

            s1 = "[" + this.FileNevCSV + "]";
            s2 = "ColNameHeader=" + cabecera.ToString();
            s3 = "Format=" + this.strFormat;
            s4 = "MaxScanRows=25";
            s5 = "CharacterSet=" + this.strEncoding;

            srOutput.WriteLine(s1.ToString() + "\r\n" + s2.ToString() + "\r\n" + s3.ToString() + "\r\n" + s4.ToString() + "\r\n" + s5.ToString());
            srOutput.Close();
            fsOutput.Close();
        }



        private string strFormat;	//CSV separator character
        private string strEncoding; //Encoding of CSV file

        // Delimiter character selection





        public static DataTable LoadCSV(string filename, string delimiter, string[] columnNames)
        {

            //  Create the new table 
            DataTable data = new DataTable();
            data.Locale = System.Globalization.CultureInfo.CurrentCulture;

            //  Check file 
            if (!File.Exists(filename))
                throw new FileNotFoundException("File not found", filename);

            //  Process the file line by line 
            string line;
            using (TextReader tr = new StreamReader(filename, System.Text.Encoding.Default))
            {
                //  If column names were not passed, we'll read them from the file 
                if (columnNames == null)
                {
                    //  Get the first line 
                    line = tr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        throw new IOException("Could not read column names from file.");
                    columnNames = line.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
                }

                //  Add the columns to the data table 
                foreach (string colName in columnNames)
                    data.Columns.Add(colName);

                //  Read the file 
                string[] columns;
                while ((line = tr.ReadLine()) != null)
                {
                    columns = line.Split(new string[] { delimiter }, StringSplitOptions.None);
                    //  Ensure we have the same number of columns 
                    if (columns.Length != columnNames.Length)
                    {
                        string message = "Data row has {0} columns and {1} are defined by column names.";
                        throw new DataException(string.Format(message, columns.Length, columnNames.Length));
                    }
                    data.Rows.Add(columns);
                }
            }
            return data;



        }


        public static DataTable LoadCSV(string path, bool IsFirstRowHeader)
        {

            

            string header = "No";
            string sql = string.Empty;
            DataTable dataTable = null;
            string pathOnly = string.Empty;
            string fileName = string.Empty;
                
           

                pathOnly = Path.GetDirectoryName(path);
                fileName = Path.GetFileName(path);

                sql = @"SELECT * FROM [" + fileName + "]";

                if (IsFirstRowHeader)
                {
                    header = "Yes";
                }

                using (OleDbConnection connection = new OleDbConnection(    
                @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly +
                ";Extended Properties=\"Text;HDR=" + header + "\""))
                {
                    using (OleDbCommand command = new OleDbCommand(sql, connection))
                    {
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                        {
                            dataTable = new DataTable();
                            dataTable.Locale = CultureInfo.CurrentCulture;
                            adapter.Fill(dataTable);

                        }
                    }
                }
           

            return dataTable;

        }

    }
}
