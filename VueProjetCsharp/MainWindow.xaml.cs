
using ModeleCshapG4;
using ModeleCshapG4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace VueProjetCsharp
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        Capteurs capteurs;
        public MainWindow()
        {
            InitializeComponent();
            this.capteurs = null;
          
        }

        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var selectionCombo =  ID_Capteurs.SelectedItem as Nullable<int>;

            if (selectionCombo!=null)
            {
                ID_Capteurs.SelectedIndex = -1;
            }
            if (ID_Capteurs.Text != "ID Capteurs" && ID_Capteurs.Text.Length > 0)
            {



                ID_Capteurs.ItemsSource =  ServicesDonnees.getFilteredListCapteur(ID_Capteurs.Text);
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
           
        private void ID_Capteurs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.capteurs != null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                     ServiceFileHandling.ImportFile(this.capteurs, openFileDialog);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Merci de valider le choix d'un capteur");
            }
        }

        private void ValidationCapteur(object sender, RoutedEventArgs e)

        {
            this.capteurs =ServicesDonnees.getCapteurInfo(int.Parse(ID_Capteurs.Text));
            System.Windows.Forms.MessageBox.Show(string.Format("le capteur validé est {0}.", this.capteurs.id_capteur.ToString()));
     

    }
    }
}