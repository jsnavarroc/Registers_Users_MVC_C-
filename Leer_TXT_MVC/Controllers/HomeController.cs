using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leer_TXT_MVC.Models;

namespace Leer_TXT_MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Carga()
        {
            ViewBag.Message = "Cargar archivos";

            return View();
        }

        [HttpPost]
        public ActionResult Carga(HttpPostedFileBase file1, HttpPostedFileBase file2)
        {
            Archivo modelo = new Archivo();
            if (file1 != null && file2 != null)

            {
                String path = Server.MapPath("~/Temp/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string filePath1 = modelo.SubirArchivo(path, file1);


                string filePath2 = modelo.SubirArchivo(path, file2);
     

                modelo.CheckRegister(filePath1, filePath2);


                /*
                    ViewBag.Error = modelo.Error;
                    ViewBag.Correcto = modelo.Confirmacion;     

                    Array resultados = modelo.CompararArchivo(ruta1);
                    ViewData["resultados"] = resultados;
                    Array resultados2 = modelo.CompararArchivo(ruta2);
                    ViewData["resultados2"] = resultados2;
                    */

                return View();
                
            }
            return View();
        }



    }
}