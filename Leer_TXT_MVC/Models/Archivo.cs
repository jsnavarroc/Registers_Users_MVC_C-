using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Leer_TXT_MVC.Models
{
    public class Archivo
    {
        public string Confirmacion { get; set; }
        public Exception Error { get; set; }


        //Metodo que suve el archivo y guarda en la carpeta Temp 
        public string SubirArchivo(String path, HttpPostedFileBase file)
        {
            try
            {
                string filePath = string.Empty;
                filePath = path + Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                file.SaveAs(filePath);
                return filePath;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}