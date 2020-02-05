using System;
using System.Collections.Generic;
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
            System.Console.WriteLine(capteur.id_capteur);
            System.Console.WriteLine(capteur.id_capteur);
            try
            {
                HashSet<Releves> listReleves = new HashSet<Releves>();
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
                        Capteurs = capteur,
                    //id_capteur = capteurs.id_capteur

                    
                    };
                                   
                  

                    listReleves.Add(releves);
                }
                ServicesDonnees.SaveCapteur(listReleves);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Le fichier que vous tentez d'ouvrir n'est pas au bon format", ex.Message);
            }


        }
    }
}
