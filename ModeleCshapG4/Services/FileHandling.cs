using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModeleCshapG4.Services
{
    public static class ServiceFileHandling
    {

        
        public static void ImportFile(Capteurs capteur, OpenFileDialog openFileDialog)
        {
            //Capteurs capteurs = capteur;

            try
            {

                string[] lines = ToolsDataTest.isExtensionEqualsToTxt(openFileDialog);
                foreach (string line in lines)

                {

                    string[] lineContent = line.Split(' ');

                    Releves releves = new Releves
                    {
                        releve_DTTM = ToolsDataTest.isValidDateFormat(lineContent[1], lineContent[2]),
                        temperature = decimal.Parse(lineContent[3].Replace(".", ",")),
                        hygrometrie = decimal.Parse(lineContent[4].Replace(".", ",").Replace("%", "")),
                        insertion_DTTM = DateTime.Now,
                        id_capteur = capteur.id_capteur

                    
                    };
                                   
                  

                    capteur.Releves.Add(releves);
                }
                ServicesDonnees.SaveCapteur(capteur);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Le fichier que vous tentez d'ouvrir n'est pas au bon format", ex.Message);
            }


        }
        public static void ExportCSV(Capteurs capteur, FolderBrowserDialog folderBrowser  )
        {
            var sb = new StringBuilder();

            var finalPath = Path.Combine(folderBrowser.SelectedPath, capteur.num_capteur+ "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss")  + ".csv");
            var header = "";
            var info = typeof(Releves).GetProperties();
            List<string> unwantedProperties = new List<string>() { "Capteurs", "id_capteur", "id_releve" };
            if (!File.Exists(finalPath))
            {
                var file = File.Create(finalPath);
                file.Close();
           



                
                foreach (var prop in typeof(Releves).GetProperties())
                {
                    if (!unwantedProperties.Contains(prop.Name))
                    {
                        header += prop.Name + "; ";
                    }
                }

                header = header.Substring(0, header.Length - 2);
                sb.AppendLine(header);
                TextWriter sw = new StreamWriter(finalPath, true);
                sw.Write(sb.ToString());
                sw.Close();
            }
            foreach (var obj in capteur.Releves)
            {
                sb = new StringBuilder();
                var line = "";
                foreach (var prop in info)
                {
                    if (!unwantedProperties.Contains(prop.Name))
                    {
                        line += prop.GetValue(obj, null) + "; ";
                    }
                }
                line = line.Substring(0, line.Length - 2);
                sb.AppendLine(line);
                TextWriter sw = new StreamWriter(finalPath, true);
                sw.Write(sb.ToString());
                sw.Close();
            }
        }
    }

}
    
