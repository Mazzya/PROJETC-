
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using ModeleCshapG4;
using ModeleCshapG4.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.Forms.MessageBox;

namespace VueProjetCsharp
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window


    {
        public Capteurs capteurs { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public LineSeries HygroSerie { get; set; }
        public LineSeries TempSerie { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public Func<double, string> XFormatter { get; set; }


        public MainWindow()
        {
            this.capteurs = null;

            InitializeComponent();
            HygroSerie = new LineSeries(Title = "Hygrometrie");
            TempSerie = new LineSeries(Title = "Temperature");

            SeriesCollection = new SeriesCollection { HygroSerie, TempSerie };



        }


    
        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var selectionCombo = ID_Capteurs.SelectedItem as Nullable<int>;

            if (selectionCombo != null)
            {
                ID_Capteurs.SelectedIndex = -1;
            }
            if (ID_Capteurs.Text != "ID Capteurs" && ID_Capteurs.Text.Length > 0)
            {



                ID_Capteurs.ItemsSource = ServicesDonnees.getFilteredListCapteur(ID_Capteurs.Text);
                ID_Capteurs.IsDropDownOpen = true;

                var textBox = Keyboard.FocusedElement as System.Windows.Controls.TextBox;
                textBox.SelectionLength = 0;
                textBox.SelectionStart = ID_Capteurs.Text.Length;



            }
            else
            {

                ID_Capteurs.ItemsSource = null;
                ID_Capteurs.IsDropDownOpen = false;
            }

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.capteurs != null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    ServiceFileHandling.ImportFile(this.capteurs, openFileDialog);
                    this.capteurs = ServicesDonnees.getCapteurInfo(capteurs.num_capteur);

                    refreshComponent();
                    MessageBox.Show(string.Format("Nombre de relevés importées : {0}.", capteurs.Releves.Count));

                }

            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Merci de valider le choix d'un capteur");
            }

        }

        private void ValidationCapteur(object sender, RoutedEventArgs e)

        {
            this.capteurs = ServicesDonnees.getCapteurInfo(int.Parse(ID_Capteurs.Text));
            System.Windows.Forms.MessageBox.Show(string.Format("le capteur validé est {0}.", this.capteurs.id_capteur.ToString()));

           refreshComponent();

        }
        private void ExportCSV(object sender, RoutedEventArgs e)
        {
            if (this.capteurs != null)
            {
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

                if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    ServiceFileHandling.ExportCSV(this.capteurs, folderBrowser);
                    System.Windows.Forms.MessageBox.Show("Fichier Exporté");


                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Merci de valider le choix d'un capteur");
            }
        }
        private void refreshComponent()
        {
            bindingraph();
            RelevesDG.ItemsSource = capteurs.Releves;

        }
        private void bindingraph()
        {



            HygroSerie.Values  = new ChartValues<DateTimePoint>(capteurs.Releves.Select(l => new DateTimePoint(l.releve_DTTM,Decimal.ToDouble(l.hygrometrie))));
            HygroSerie.Title = "Hygrometrie";

            TempSerie.Values  = new ChartValues<DateTimePoint>(capteurs.Releves.Select(l => new DateTimePoint(l.releve_DTTM, Decimal.ToDouble(l.temperature))));
            TempSerie.Title = "Température";




            XFormatter = val => new DateTime((long)val).ToString("dd/MM/yyyy HH:mm:ss");

            YFormatter = value => value.ToString("F");

            chart.DataContext = this;
 
            chart.Update(true, true);


        }
}

    }
