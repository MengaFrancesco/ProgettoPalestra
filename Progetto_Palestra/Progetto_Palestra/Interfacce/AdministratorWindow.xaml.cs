using Progetto_Palestra.Classi;
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
using System.Windows.Shapes;

namespace Progetto_Palestra.Interfacce
{
    /// <summary>
    /// Logica di interazione per AdministratorWindow.xaml
    /// </summary>
    public partial class AdministratorWindow : Window
    {
        //Properties
        public CAmministratore Admin;

        /**
         * @brief Costruttore con parametri
         * @details Inizializza AdministratorWindow e le properties utilizzando il parametro
         */
        public AdministratorWindow(CAmministratore Admin)
        {
            
            InitializeComponent();

            this.Admin = Admin;

            LabelBenvenuto.Content += " " + Admin.Nome;

            UpdateDashboard(); //Aggiorna numero atleti

            //Rende tutte le grid invisibili tranne la home
            GridHome.Visibility = Visibility.Visible;
            GridUtente.Visibility = Visibility.Hidden;
            GridOrario.Visibility = Visibility.Hidden;
            GridControllore.Visibility = Visibility.Hidden;
            GridAttrezzi.Visibility = Visibility.Hidden;
            GridMeccanici.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Hidden;
        }

        private void BTHome_Click(object sender, RoutedEventArgs e)
        {
            //Rende tutte le grid invisibili tranne la home
            GridHome.Visibility = Visibility.Visible;
            GridUtente.Visibility = Visibility.Hidden;
            GridOrario.Visibility = Visibility.Hidden;
            GridControllore.Visibility = Visibility.Hidden;
            GridAttrezzi.Visibility = Visibility.Hidden;
            GridMeccanici.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Hidden;

            UpdateDashboard(); //Aggiorna la dashboard ed inserisce i dati
        }

        private void BTUtente_Click(object sender, RoutedEventArgs e)
        {
            //Rende tutte le grid invisibili tranne la modifica utente
            GridHome.Visibility = Visibility.Hidden;
            GridUtente.Visibility = Visibility.Visible;
            GridOrario.Visibility = Visibility.Hidden;
            GridControllore.Visibility = Visibility.Hidden;
            GridAttrezzi.Visibility = Visibility.Hidden;
            GridMeccanici.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Hidden;
        }

        private void BTOrario_Click(object sender, RoutedEventArgs e)
        {
            //Rende tutte le grid invisibili tranne la modifica orario
            GridHome.Visibility = Visibility.Hidden;
            GridUtente.Visibility = Visibility.Hidden;
            GridOrario.Visibility = Visibility.Visible;
            GridControllore.Visibility = Visibility.Hidden;
            GridAttrezzi.Visibility = Visibility.Hidden;
            GridMeccanici.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Hidden;
        }

        private void BTControllori_Click(object sender, RoutedEventArgs e)
        {
            //Rende tutte le grid invisibili tranne la modifica controllori
            GridHome.Visibility = Visibility.Hidden;
            GridUtente.Visibility = Visibility.Hidden;
            GridOrario.Visibility = Visibility.Hidden;
            GridControllore.Visibility = Visibility.Visible;
            GridAttrezzi.Visibility = Visibility.Hidden;
            GridMeccanici.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Hidden;
        }

        private void BTAttrezzi_Click(object sender, RoutedEventArgs e)
        {
            //Rende tutte le grid invisibili tranne la modifica attrezzi
            GridHome.Visibility = Visibility.Hidden;
            GridUtente.Visibility = Visibility.Hidden;
            GridOrario.Visibility = Visibility.Hidden;
            GridControllore.Visibility = Visibility.Hidden;
            GridAttrezzi.Visibility = Visibility.Visible;
            GridMeccanici.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Hidden;
        }

        private void BTMeccanici_Click(object sender, RoutedEventArgs e)
        {
            //Rende tutte le grid invisibili tranne la modifica meccanici
            GridHome.Visibility = Visibility.Hidden;
            GridUtente.Visibility = Visibility.Hidden;
            GridOrario.Visibility = Visibility.Hidden;
            GridControllore.Visibility = Visibility.Hidden;
            GridAttrezzi.Visibility = Visibility.Hidden;
            GridMeccanici.Visibility = Visibility.Visible;
            GridLogout.Visibility = Visibility.Hidden;
        }

        private void BTLogout_Click(object sender, RoutedEventArgs e)
        {
            //Rende tutte le grid invisibili tranne il logout
            GridHome.Visibility = Visibility.Hidden;
            GridUtente.Visibility = Visibility.Hidden;
            GridOrario.Visibility = Visibility.Hidden;
            GridControllore.Visibility = Visibility.Hidden;
            GridAttrezzi.Visibility = Visibility.Hidden;
            GridMeccanici.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Visible;
        }

        public void UpdateDashboard()
        {
            MySQLdatabase db = new MySQLdatabase();
            int num = db.GetNumAthlete();
            int num2 = db.GetNumAthleteWeek();
            LabelNumAtleti.Content= num.ToString();
            LabelNumAtletiW.Content ="+"+ num2.ToString()+" questa settimana";
            LabelNumAbbonati.Content = db.GetNumAbbonati();
            LabelNumScaduti.Content = db.GetNumScaduti() + " scaduti/non rinnovati";
            LabelNumVisite.Content = db.GetNumVisite();
            LabelNumVisiteW.Content = db.GetNumVisiteWeek()+ " questa settimana";
        }
    }
}
