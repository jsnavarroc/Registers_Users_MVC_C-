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
                string fimeName = Path.GetFileName(file.FileName);
                filePath = path + fimeName;
                string extension = Path.GetExtension(file.FileName);
                file.SaveAs(filePath);
                return fimeName;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Registed[] CheckRegister(string filePath1, string filePath2)
        {
            string result_cheked = "";
            //-----------------------------------------------------------------//
            //Se crea un vector con las letras establecidas en el contenido.txt
            //-----------------------------------------------------------------//
            string txtData = File.ReadAllText(filePath1);
            string[] letterArryaCode = txtData.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var codeLetter = new Letters[letterArryaCode.Length].Select(h => new Letters("", 0)).ToArray();
            int num = 0;
            foreach (var letter in letterArryaCode)
            {
                codeLetter[num].letter = letter;
                num++;
            }

            //-------------------------------------------------------------------------------//
            //Se crea un vector que controla si fue o no invitado como una lista de chekeo
            //------------------------------------------------------------------------------//
            string txtData2 = File.ReadAllText(filePath2);
            string[] registeds = txtData2.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var registedCheck = new Registed[registeds.Length].Select(h => new Registed("", 0)).ToArray();
            num = 0;
            foreach (var name in registeds)
            {
                registedCheck[num].name = name;
                num++;
            }


            // Se comienza a checkear por invitado
            // Se compara el vector de las letrasa con el vector de la persona 

            for (int i = 0; i < registedCheck.Length; i++)
            {

                var wordLength = (registedCheck[i].name.Length);

                var letterCheck = new Letters[wordLength].Select(h => new Letters("", 0)).ToArray();
                //conbierte palabra en un objeto vector de letras    
                for (int j = 0; j < wordLength; j++)
                {
                    letterCheck[j].letter = registedCheck[i].name[j].ToString();
                }

                /*
                    *Evalua que el customer este registrado 
                    *(Evalua que las letras de  una sola palabra 
                    *si tenga interseccion con el cojunto de letras)
                */
                for (int j = 0; j < letterCheck.Length; j++)
                {
                    for (int c = 0; c < codeLetter.Length; c++)
                    {
                        if (letterCheck[j].letter == codeLetter[c].letter)
                        {
                            if (letterCheck[j].check == 0 && codeLetter[c].check == 0)
                            {
                                letterCheck[j].check = 1;
                                codeLetter[c].check = 1;
                                break;
                            }
                        }
                    }
                    if (letterCheck[j].check == 0)
                    {
                        break;
                    }
                }
                int count = 0;
                foreach (var check in letterCheck)
                {
                    count += check.check;
                }
                if (count == wordLength)
                {
                    registedCheck[i].check = 1;
                }

            }

            return registedCheck;
        }

        // Modelo que crea el archivo de salida.
        public String CreateTXT(Registed[] registedCheck, string path)
        {
            string filePath = string.Empty;
            filePath = path + "Resultados.txt";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            StreamWriter outputFile = File.CreateText(filePath);


            string check = "";
            foreach (var registed in registedCheck)
            {
                if (registed.check == 1)
                {
                    check = registed.name + " --> Si esta registrado";
                }
                else
                {
                    check = registed.name + " --> No esta registrado";
                }

                outputFile.WriteLine(check);
            }
            outputFile.Dispose();

            return filePath;

        }

        public void ReadFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string txtData = File.ReadAllText(filePath);
                string[] letterArryaCode = txtData.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            }         
        }


    }   
}