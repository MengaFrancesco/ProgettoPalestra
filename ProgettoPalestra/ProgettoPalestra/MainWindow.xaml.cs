using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProgettoPalestra
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            InizializzaCombo();
        }

        public void InizializzaCombo()
        {
            ComboTipo.Items.Add("Atleta");
            ComboTipo.Items.Add("Amministratore");
            ComboTipo.Items.Add("Controllore");
            ComboTipo.Items.Add("Manutenzione");
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            /* Controllo input */
            if(TextUsername.Text == "")
            {
                MessageBox.Show("Username non valido.", "Attenzione!", MessageBoxButton.OK, MessageBoxImage.Error);
            }else if (TextPassword.Password == "")
            {
                MessageBox.Show("Password non valida.", "Attenzione!", MessageBoxButton.OK, MessageBoxImage.Error);
            }else if(ComboTipo.SelectedIndex == -1)
            {
                MessageBox.Show("Tipologia utente non selezionata.", "Attenzione!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            /* Controllo rouolo (richiesta server DB) */




            /* Apri finestra */
            MessageBox.Show("Login effettuato.");
        }
    }
}
