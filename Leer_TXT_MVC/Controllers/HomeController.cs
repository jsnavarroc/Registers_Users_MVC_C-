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
            string filePath1 = "", filePath2 = "", fimeName = "";

            Archivo modelo = new Archivo();
            if (file1 != null && file2 != null)

            {
                String path = Server.MapPath("~/Temp/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

              
                fimeName = modelo.SubirArchivo(path, file1);
                if (fimeName == "CONTENIDO.txt")
                {
                    filePath1 = path + fimeName;
                }
                else {
                    filePath2 = path + fimeName;
                }
                fimeName = modelo.SubirArchivo(path, file2);
                if (fimeName == "REGISTRADOS.txt")
                {
                    filePath2 = path + fimeName;
                }
                else
                {
                    filePath1 = path + fimeName;
                }
                if (filePath1 != "" && filePath2 !="") {
                    Registed[] registedCheck = modelo.CheckRegister(filePath1, filePath2);
                    modelo.CreateTXT(registedCheck, path);
                    filePath1 = ""; filePath2 = ""; fimeName = "";
                    ViewBag.Band = 1;
                    return View();
                }
                else{
                    ViewBag.Band = 0 ;
                    return View();
                }


            }

            return View();
        }


        public ActionResult FileShow()
        {
            ViewBag.Message = "Your fucking file.";
            List<Result> results = new List<Result>();
            Archivo modelo = new Archivo();
            string fimeName = "Resultados.txt";
            string path = Server.MapPath("~/Temp/");
            string filePath = path + fimeName;
            modelo.ReadFile(filePath);


            //Leyendo archivo
            if (System.IO.File.Exists(filePath))
            {

                string txtResult = System.IO.File.ReadAllText(filePath);
                foreach (string row in txtResult.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        results.Add(new Result
                        {
                            Text = row,
                        });
                    }
                }
                return View(results);
            }
            else {
                return View(results);
            }



        }


    }
}