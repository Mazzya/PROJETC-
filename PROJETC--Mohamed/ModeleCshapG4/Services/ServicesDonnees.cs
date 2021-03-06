﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeleCshapG4.Services
{
    public class ServicesDonnees
    {
        public void AddNewCapteur(int numcapteur, string lieu)
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
        public List<Capteurs> readAllCapteur()
        {
            using (var context = new GestionCapteurEntities())
            {
                return context.Capteurs.ToList();
            }
        }
        public void AddReleve(DateTime dateReleve, int  idcapteur, decimal temperatur,decimal hygrometry )
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
        public void SaveCapteur(HashSet<Releves> listReleves)
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
        public Capteurs getCapteurInfo(Capteurs capteur)
        {
            using (var context = new GestionCapteurEntities())
            {
                return context.Capteurs.Include("Releves").Where(c => c.id_capteur == capteur.id_capteur).FirstOrDefault();
            }
        }


    }


}

