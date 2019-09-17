using doMain.Utils;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Collections.Generic;

using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace doMain.Utils
{

    public class PdfUtils
    {
        public static event EventHandler OnSplitProcess;

        //public static PdfPosition GetPageSize(byte[] arrayBytes,int index)
        //{
        //    PdfReader pdfReader = new PdfReader(arrayBytes);
        //    Rectangle pageSize = pdfReader.GetPageSize(index);
        //    pdfReader.Close();
        //    return new PdfPosition(pageSize.Width,pageSize.Height);
        //}

        //public static PdfPosition GetPageSize(string path, int index)
        //{
        //    PdfReader pdfReader = new PdfReader(path);
        //    Rectangle pageSize = pdfReader.GetPageSize(index);
        //    pdfReader.Close();
        //    return new PdfPosition(pageSize.Width, pageSize.Height);
        //}

       

        //public static Image ConvertPdfToTiff(byte[] arrayBytes)
        //{

        //    PdfReader pdfReader = new PdfReader(arrayBytes);
            
            
        //} 

        public static Rectangle GetPageSize(byte[] arrayBytes, int pageNumber)
        {
            PdfReader pdfReader = new PdfReader(arrayBytes);
            Rectangle pageSize = pdfReader.GetPageSize(pageNumber);
            pdfReader.Close();
            return pageSize;
        }

        public static Rectangle GetPageSize(string path, int pageNumber)
        {
            PdfReader pdfReader = new PdfReader(path);
            Rectangle pageSize = pdfReader.GetPageSize(pageNumber);
            pdfReader.Close();
            return pageSize;
        }


        public static void ExtractPage(string pdfFilePath,string fileDest ,int pageNumber)
        {
            using (PdfReader reader = new PdfReader(pdfFilePath))
            {
                Document document = new Document();
                

                PdfCopy copy = new PdfCopy(document, new FileStream(fileDest, FileMode.Create));
                document.Open();
                              
                copy.AddPage(copy.GetImportedPage(reader, pageNumber));

                document.Close();


            }

            
            
        }

        public static List<byte[]> Split(byte[] arrayBytes)
        {
            PdfReader pdfReader = new PdfReader(arrayBytes);
            return Split(pdfReader);
        }

        public static List<byte[]> Split(string sourcePdfPath)
        {
            PdfReader pdfReader = new PdfReader(new RandomAccessFileOrArray(sourcePdfPath), new ASCIIEncoding().GetBytes(""));
            return Split(pdfReader);
        }

        public static List<byte[]> Split(PdfReader reader)
        {


            int p = 0;
                
                Document document;
                
                var data = new List<byte[]>();


                for (p = 1; p <= reader.NumberOfPages; p++)
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        document = new iTextSharp.text.Document();
                        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                        writer.SetPdfVersion(PdfWriter.PDF_VERSION_1_2);
                        writer.CompressionLevel = PdfStream.BEST_COMPRESSION;
                        writer.SetFullCompression();
                        document.SetPageSize(reader.GetPageSize(p));
                        document.NewPage();
                        document.Open();
                        document.AddDocListener(writer);
                        PdfContentByte cb = writer.DirectContent;
                        PdfImportedPage pageImport = writer.GetImportedPage(reader, p);
                        int rot = reader.GetPageRotation(p);
                        if (rot == 90 || rot == 270)
                        {
                            cb.AddTemplate(pageImport, 0, -1.0F, 1.0F, 0, 0, reader.GetPageSizeWithRotation(p).Height);
                        }
                        else
                        {
                            cb.AddTemplate(pageImport, 1.0F, 0, 0, 1.0F, 0, 0);
                        }
                        document.Close();
                        document.Dispose();
                        //File.WriteAllBytes(DestinationFolder + "/" + p + ".pdf", memoryStream.ToArray());

                        data.Add(memoryStream.ToArray());

                        if (OnSplitProcess != null)
                            OnSplitProcess(p, null);
                    }
                }
                reader.Close();
                reader.Dispose();
           
            return data;

        }

   
     

        public static List<byte[]> Join(List<byte[]> source, List<byte[]> news, int position)
        {
            source.InsertRange(position, news);
            return source;
        }

        public static List<byte[]> Remove(List<byte[]> source, int position)
        {  
            source.RemoveAt(position);
            return source;
        }


        public static bool InsertPages(string sourcePdf, string insertPdf, int pagenumber)
        {

            //variables
            //String first_source = "D:/myprogram/pdf/htmlpdf.pdf";
            //String second_source = "d:/pdfcontentadded.pdf";
            string pathout = Guid.NewGuid().ToString() + ".pdf"; //@"C:\doMain.Utils\inDigitalization\pdfout.pdf";
            //create a document object
            //var doc = new Document(PageSize.A4);
            //create PdfReader objects to read pages from the source files
            PdfReader reader = new PdfReader(insertPdf);
            PdfReader reader1 = new PdfReader(sourcePdf);
            //create PdfStamper object to write to the pages read from readers 
            PdfStamper stamper = new PdfStamper(reader1, new FileStream(pathout, FileMode.Create));
            //get one page from htmlpdf.pdf
            PdfImportedPage page = stamper.GetImportedPage(reader, 1);
            //the page gotten from htmlpdf.pdf will be inserted at the second page in the first source doc
            stamper.InsertPage(pagenumber, reader1.GetPageSize(1));
            //insert the page
            PdfContentByte pb = stamper.GetUnderContent(pagenumber);
            pb.AddTemplate(page, 0, 0);
            //close the stamper
            stamper.Close();

            reader.Close();
            reader1.Close();

            reader.Dispose();
            reader1.Dispose();

            FileUtils.Delete(sourcePdf, true);
            FileUtils.CopyBlocking(pathout, sourcePdf);
            FileUtils.Delete(pathout);

           

            return true;
        }

       


        public static byte[] Merge(List<byte[]> pdf)
        {
            using (var ms = new MemoryStream())
            {
                using (var doc = new Document())
                {

                    

                    using (var copy = new PdfSmartCopy(doc, ms))
                    {
                        doc.Open();

                        //Loop through each byte array
                        foreach (var p in pdf)
                        {

                            //Create a PdfReader bound to that byte array
                            using (var reader = new PdfReader(p))
                            {

                                //Add the entire document instead of page-by-page
                                copy.AddDocument(reader);
                            }
                        }

                        doc.Close();
                    }
                }

                //Return just before disposing
                return ms.ToArray();
            }
        }
    }
}
