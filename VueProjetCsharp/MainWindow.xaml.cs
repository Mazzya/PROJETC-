
using ModeleCshapG4;
using ModeleCshapG4.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace VueProjetCsharp
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
          
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            textbox.Text = ID_Capteurs.Text;
            List<Capteurs> listCapteur = new ServicesDonnees().readAllCapteur();

            System.Console.WriteLine(" {0}  {1}", ID_Capteurs.Text, ID_Capteurs.Text.Length);

            Capteurs cap =  listCapteur.First();
           
            System.Console.WriteLine(cap.num_capteur.ToString().Substring(0, 1));
            if (ID_Capteurs.Text.Length > 3 && ID_Capteurs.Text!="ID capteurs")
            {
                /* ID_Capteurs.ItemsSource = listCapteur.Where(c => c.num_capteur.ToString().Substring(0, ID_Capteurs.Text.Length) == ID_Capteurs.Text);*/

            }
        }
           
        private void ID_Capteurs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
          


                List<Capteurs> listcap = new ServicesDonnees().readAllCapteur();
                Capteurs capteur = listcap.First();
                




                new FileHandling().ImportFile(capteur, openFileDialog);
            }
        }

       
    }
}