using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleCshapG4.Services
{
    public static class ServicesDonnees
    {
        public static void AddNewCapteur(int numcapteur, string lieu)
        {
            using (var context = new GestionCapteurEntities())
            {
                var capteur = new Capteurs
                {
                    num_capteur = numcapteur,
                    lieu = lieu
                };

                context.Capteurs.Add(capteur);
                context.SaveChanges();
            }
        }
        public static List<Capteurs> readAllCapteur()
        {
            using (var context = new GestionCapteurEntities())
            {
                return context.Capteurs.ToList();
            }
        }
        public static void AddReleve(DateTime dateReleve, int  idcapteur, decimal temperatur,decimal hygrometry )
        {
            using (var context = new GestionCapteurEntities())
            {
                var releve = new Releves
                {

                    releve_DTTM = dateReleve,
                    temperature = temperatur,
                    hygrometrie = hygrometry,
                    id_capteur = idcapteur,
                    insertion_DTTM = DateTime.Now
                };

                context.Releves.Add(releve);
                context.SaveChanges();
            }

        }
        public static void SaveCapteur(HashSet<Releves> listReleves)
        {
            
            using (var context = new GestionCapteurEntities())
            {
                
                foreach (Releves releve in listReleves)
                {
                    ;
                    context.Releves.Add(releve);
                    
                }
             
                context.SaveChanges();

            }
        }
        public static Capteurs getCapteurInfo(int numCapteur)
        {
            using (var context = new GestionCapteurEntities())
            {
                return context.Capteurs.Include("Releves").Where(c => c.num_capteur == numCapteur).FirstOrDefault();
            }
        }

        public static  IEnumerable<int> getFilteredListCapteur(string filter )
        {

           return (from c in readAllCapteur() where c.num_capteur.ToString().Substring(0, Math.Min(filter.Length, c.num_capteur.ToString().Length)) == filter select c.num_capteur);

        }

    }


}

