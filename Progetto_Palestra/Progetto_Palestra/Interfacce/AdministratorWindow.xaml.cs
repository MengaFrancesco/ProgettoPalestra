using Progetto_Palestra.Classi;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Progetto_Palestra.Interfacce
{
    /// <summary>
    /// Logica di interazione per AdministratorWindow.xaml
    /// </summary>
    public partial class AdministratorWindow : Window
    {
        //Properties
        public CAmministratore Admin;
        public bool Logout;
        private MySQLdatabase db;

        /**
         * @brief Costruttore con parametri
         * @details Inizializza AdministratorWindow e le properties utilizzando il parametro
         */
        public AdministratorWindow(CAmministratore Admin)
        {

            InitializeComponent();

            this.Admin = Admin;
            Logout = false;
            db = new MySQLdatabase();

            LabelBenvenuto.Content += " " + Admin.Nome;

            UpdateDashboard(); //Aggiorna la dashboard


        }

        /**
         * @brief Metodo che visualizza e aggiorna le info della dashboard
         */
        public void UpdateDashboard()
        {
            //VISUALIZZA SOLO DASHBOARD
            GridDashboard.Visibility = Visibility.Visible;
            GridAtleti.Visibility = Visibility.Hidden;
            GridOrario.Visibility = Visibility.Hidden;
            GridControllore.Visibility = Visibility.Hidden;
            GridAttrezzi.Visibility = Visibility.Hidden;
            GridMeccanici.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Hidden;
            GridAdmin.Visibility = Visibility.Hidden;

            //AGGIORNA DATI
            int num = db.GetNumAtleti();
            int num2 = db.GetNumAtletiWeek();
            LabelNumAtleti.Content = num.ToString();
            LabelNumAtletiW.Content = "+" + num2.ToString() + " questa settimana";
            LabelNumAbbonati.Content = db.GetNumAbbonati();
            LabelNumScaduti.Content = db.GetNumScaduti() + " scaduti/non rinnovati";
            LabelNumVisite.Content = db.GetNumVisite();
            LabelNumVisiteW.Content = db.GetNumVisiteWeek() + " questa settimana";

            //Inserisce dati nella datagrid

        }

        /**
         * @brief Metodo che visualizza e aggiorna le info della  sezione atleti
         */
        public void UpdateAtleti()
        {
            //Visualizza solo sezione atleti
            GridDashboard.Visibility = Visibility.Hidden;
            GridAtleti.Visibility = Visibility.Visible;
            GridOrario.Visibility = Visibility.Hidden;
            GridControllore.Visibility = Visibility.Hidden;
            GridAttrezzi.Visibility = Visibility.Hidden;
            GridMeccanici.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Hidden;
            GridAdmin.Visibility = Visibility.Hidden;

            //Carica dati atleti
            DataGridAtleti.ItemsSource = db.GetInfoAtleti();
        }

        public void UpdateOrario()
        {
            //Visualizza solo sezione orario
            GridDashboard.Visibility = Visibility.Hidden;
            GridAtleti.Visibility = Visibility.Hidden;
            GridOrario.Visibility = Visibility.Visible;
            GridControllore.Visibility = Visibility.Hidden;
            GridAttrezzi.Visibility = Visibility.Hidden;
            GridMeccanici.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Hidden;
            GridAdmin.Visibility = Visibility.Hidden;

            //Aggiorna datagrid
            List<COrario> orario = new List<COrario>();
            orario = db.GetOrario();
            DataGridOrario.ItemsSource = orario;
            DataGridOrario.SelectedIndex = 0;
        }

        public void UpdateControllori()
        {
            //Visualizza solo sezione controllori
            GridDashboard.Visibility = Visibility.Hidden;
            GridAtleti.Visibility = Visibility.Hidden;
            GridOrario.Visibility = Visibility.Hidden;
            GridControllore.Visibility = Visibility.Visible;
            GridAttrezzi.Visibility = Visibility.Hidden;
            GridMeccanici.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Hidden;
            GridAdmin.Visibility = Visibility.Hidden;
        }

        public void UpdateAttrezzi()
        {
            //Visualizza solo sezione attrezzi
            GridDashboard.Visibility = Visibility.Hidden;
            GridAtleti.Visibility = Visibility.Hidden;
            GridOrario.Visibility = Visibility.Hidden;
            GridControllore.Visibility = Visibility.Hidden;
            GridAttrezzi.Visibility = Visibility.Visible;
            GridMeccanici.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Hidden;
            GridAdmin.Visibility = Visibility.Hidden;
        }

        public void UpdateMeccanici()
        {
            //Visualizza solo sezione meccanici
            GridDashboard.Visibility = Visibility.Hidden;
            GridAtleti.Visibility = Visibility.Hidden;
            GridOrario.Visibility = Visibility.Hidden;
            GridControllore.Visibility = Visibility.Hidden;
            GridAttrezzi.Visibility = Visibility.Hidden;
            GridMeccanici.Visibility = Visibility.Visible;
            GridLogout.Visibility = Visibility.Hidden;
            GridAdmin.Visibility = Visibility.Hidden;
        }

        public void UpdateAmministratori()
        {
            //Logout
            GridDashboard.Visibility = Visibility.Hidden;
            GridAtleti.Visibility = Visibility.Hidden;
            GridOrario.Visibility = Visibility.Hidden;
            GridControllore.Visibility = Visibility.Hidden;
            GridAttrezzi.Visibility = Visibility.Hidden;
            GridMeccanici.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Hidden;
            GridAdmin.Visibility = Visibility.Visible;
        }

        public void UpdateLogout()
        {
            //Logout
            GridDashboard.Visibility = Visibility.Hidden;
            GridAtleti.Visibility = Visibility.Hidden;
            GridOrario.Visibility = Visibility.Hidden;
            GridControllore.Visibility = Visibility.Hidden;
            GridAttrezzi.Visibility = Visibility.Hidden;
            GridMeccanici.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Visible;
            GridAdmin.Visibility = Visibility.Hidden;
        }



        private void LWDashboard_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateDashboard();
        }

        private void LWAtleti_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateAtleti();
        }

        private void LWOrario_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateOrario();
        }

        private void LWControllori_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateControllori();
        }

        private void LWAttrezzi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateAttrezzi();
        }

        private void LWMeccanici_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateMeccanici();
        }

        private void LWAmministratori_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateAmministratori();
        }

        private void LWLogout_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateLogout();
        }


        private void DataGridAtleti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridAtleti.SelectedIndex != -1)
            {
                //Inserisce i dati nelle caselle di testo
                CAtleta atleta = (CAtleta)DataGridAtleti.SelectedItem;
                TB_ID.Text = atleta.ID.ToString();
                TB_Username.Text = atleta.Username;
                TB_Password.Text = atleta.Password;
                TB_Nome.Text = atleta.Nome;
                TB_Cognome.Text = atleta.Cognome;
                TB_Residenza.Text = atleta.Residenza;
                DP_Iscrizione.SelectedDate = atleta.DataIscrizione;
                DP_Nascita.SelectedDate = atleta.DataNascita;
                if (atleta.Sesso == "M")
                    M.IsChecked = true;
                else
                    F.IsChecked = true;
                DP_Scadenza.SelectedDate = atleta.DataScadenza;
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            //Controlla inserimento numerico
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BT_Annulla_Click(object sender, RoutedEventArgs e)
        {
            DataGridAtleti.UnselectAll();
            ClearDatiAtleti();
        }

        private void ClearDatiAtleti()
        {
            TB_ID.Text = "";
            TB_Username.Text = "";
            TB_Password.Text = "";
            TB_Nome.Text = "";
            TB_Cognome.Text = "";
            DP_Iscrizione.SelectedDate = null;
            DP_Nascita.SelectedDate = null;
            TB_Residenza.Text = "";
            M.IsChecked = false;
            F.IsChecked = false;
            DP_Scadenza.SelectedDate = null;
        }

        private void BT_Aggiungi_Click(object sender, RoutedEventArgs e)
        {
            if (ControllaInsAtleta())
            {
                int ID = 0;
                string Username = TB_Username.Text;
                string Password = TB_Password.Text;
                string Nome = TB_Nome.Text;
                string Cognome = TB_Cognome.Text;
                string Residenza = TB_Residenza.Text;
                string Sesso = "";
                if (M.IsChecked == true) Sesso = "M"; else Sesso = "F";

                //Seleziona datetime
                DateTime DataIscrizione = (DateTime)DP_Iscrizione.SelectedDate;
                DateTime DataNascita = (DateTime)DP_Nascita.SelectedDate;
                DateTime DataScadenza = (DateTime)DP_Scadenza.SelectedDate;

                CAtleta nuovo = new CAtleta(ID, Username, Password, Nome, Cognome, Residenza, DataIscrizione, Sesso, DataScadenza, DataNascita);

                db.InserisciAtleta(nuovo);
                UpdateAtleti();
                System.Windows.MessageBox.Show("Atleta inserito!");
            }
        }

        /**
         * @brief Metodo che controlla se sono state inserite informazioni dell'atleta
         */
        private bool ControllaInsAtleta()
        {
            bool ris = true;

            if (TB_Username.Text == "")
            {
                ris = false;
                System.Windows.MessageBox.Show("Nessun username inserito!", "Attenzione!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return ris;
            }
            else if (TB_Password.Text.Length != 32)
            {
                ris = false;
                System.Windows.MessageBox.Show("Nessuna password inserita!", "Attenzione!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return ris;
            }
            else if (TB_Nome.Text == "")
            {
                ris = false;
                System.Windows.MessageBox.Show("Nessun nome inserito!", "Attenzione!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return ris;
            }
            else if (TB_Nome.Text == "")
            {
                ris = false;
                System.Windows.MessageBox.Show("Nessuna residenza inserita!", "Attenzione!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return ris;
            }
            else if (TB_Cognome.Text == "")
            {
                ris = false;
                System.Windows.MessageBox.Show("Nessun cognome inserito!", "Attenzione!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return ris;
            }
            else if (TB_Residenza.Text == "")
            {
                ris = false;
                System.Windows.MessageBox.Show("Nessuna residenza inserita!", "Attenzione!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return ris;
            }
            else if (DP_Iscrizione.SelectedDate == null)
            {
                ris = false;
                System.Windows.MessageBox.Show("Nessuna data iscrizione selezionata!", "Attenzione!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return ris;
            }
            else if (DP_Nascita.SelectedDate == null)
            {
                ris = false;
                System.Windows.MessageBox.Show("Nessuna data di nascita selezionata!", "Attenzione!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return ris;
            }
            else if (M.IsChecked == false && F.IsChecked == false)
            {
                ris = false;
                System.Windows.MessageBox.Show("Sesso non selezionato!", "Attenzione!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return ris;
            }
            else if (DP_Scadenza.SelectedDate == null)
            {
                ris = false;
                System.Windows.MessageBox.Show("Data scadenza non selezionata!", "Attenzione!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return ris;
            }//Controlla se username duplicato

            return ris;
        }

        /**
         * @brief Metodo eseguito alla pressione del bottone elimina record
         */
        private void BT_Elimina_Click(object sender, RoutedEventArgs e)
        {
            //Se un elemento selezionato
            if (DataGridAtleti.SelectedIndex != -1)
            {
                db.RimuoviAtleta(((CAtleta)DataGridAtleti.SelectedItem).ID);
            }
            else
            {
                System.Windows.MessageBox.Show("Nessun atleta selezionato!");
            }
            UpdateAtleti(); //Aggiorna finestra
        }

        /**
         * @brief Metodo eseguito alla pressione del bottone aggiorna record
         */
        private void BT_Aggiorna_Click(object sender, RoutedEventArgs e)
        {
            if (ControllaInsAtleta()) //Se i dati sono inseriti ed è selezionato un elemento
            {
                if (DataGridAtleti.SelectedIndex != -1)
                {
                    int ID = ((CAtleta)DataGridAtleti.SelectedItem).ID;
                    string Username = TB_Username.Text;
                    string Password = TB_Password.Text;
                    string Nome = TB_Nome.Text;
                    string Cognome = TB_Cognome.Text;
                    string Residenza = TB_Residenza.Text;
                    string Sesso = "";
                    if (M.IsChecked == true) Sesso = "M"; else Sesso = "F";

                    //Seleziona datetime
                    DateTime DataIscrizione = (DateTime)DP_Iscrizione.SelectedDate;
                    DateTime DataNascita = (DateTime)DP_Nascita.SelectedDate;
                    DateTime DataScadenza = (DateTime)DP_Scadenza.SelectedDate;

                    CAtleta atleta = new CAtleta(ID, Username, Password, Nome, Cognome, Residenza, DataIscrizione, Sesso, DataScadenza, DataNascita);

                    db.AggiornaAtleta(atleta);
                    UpdateAtleti();
                }
            }
        }

        private void BT_Logout_Click(object sender, RoutedEventArgs e)
        {
            Logout = true;
            this.Close();
        }

        private void BT_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BT_Ret_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            UpdateDashboard();
        }

        private void CB_Giorni_Initialized(object sender, EventArgs e)
        {
            List<string> settimana = new List<string>();
            settimana.Add("Lunedì");
            settimana.Add("Martedì");
            settimana.Add("Mercoledì");
            settimana.Add("Giovedì");
            settimana.Add("Venerdì");
            settimana.Add("Sabato");
            settimana.Add("Domenica");
            CB_Giorni.ItemsSource = settimana;
            CB_Giorni.SelectedIndex = 0;
        }

        /**
         * @brief Metodo eseguito all'inizializzazione di DataGridOrario
         */
        private void DataGridOrario_Initialized(object sender, EventArgs e)
        {
            List<COrario> orario = new List<COrario>();
            orario = db.GetOrario();
            DataGridOrario.ItemsSource = orario;
            DataGridOrario.SelectedIndex = 0;
        }

        private void DataGridOrario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridOrario.SelectedIndex != -1)
            {
                CB_Giorni.SelectedIndex = DataGridOrario.SelectedIndex;

                //Imposta orario nelle caselle di testo
                COrario selezionato = (COrario)DataGridOrario.SelectedItem;
                TP_AperturaM.SelectedTime = new DateTime(2000, 1, 1, selezionato.DalleM.Hour, selezionato.DalleM.Minute, 1);
                TP_ChiusuraM.SelectedTime = new DateTime(2000, 1, 1, selezionato.AlleM.Hour, selezionato.AlleM.Minute, 1);
                TP_AperturaP.SelectedTime = new DateTime(2000, 1, 1, selezionato.DalleP.Hour, selezionato.DalleP.Minute, 1);
                TP_ChiusuraP.SelectedTime = new DateTime(2000, 1, 1, selezionato.AlleP.Hour, selezionato.AlleP.Minute, 1);
            }
            else
            {
                CB_Giorni.SelectedIndex = DataGridOrario.SelectedIndex;

                TP_AperturaM.SelectedTime = null;
                TP_ChiusuraM.SelectedTime = null;
                TP_AperturaP.SelectedTime = null;
                TP_ChiusuraP.SelectedTime = null;
            }
        }

        private void CB_Mattina_Click(object sender, RoutedEventArgs e)
        {
            if (CB_Mattina.IsChecked == true)
            {
                TP_AperturaM.SelectedTime = new DateTime(2000, 1, 1, 0, 0, 1);
                TP_ChiusuraM.SelectedTime = new DateTime(2000, 1, 1, 0, 0, 1);
            }
            else
            {
                if (DataGridOrario.SelectedIndex != -1)
                {
                    CB_Giorni.SelectedIndex = DataGridOrario.SelectedIndex;

                    //Imposta orario nelle caselle di testo
                    COrario selezionato = (COrario)DataGridOrario.SelectedItem;
                    TP_AperturaM.SelectedTime = new DateTime(2000, 1, 1, selezionato.DalleM.Hour, selezionato.DalleM.Minute, 1);
                    TP_ChiusuraM.SelectedTime = new DateTime(2000, 1, 1, selezionato.AlleM.Hour, selezionato.AlleM.Minute, 1);
                }
                else
                {
                    CB_Giorni.SelectedIndex = DataGridOrario.SelectedIndex;

                    TP_AperturaM.SelectedTime = new DateTime();
                    TP_ChiusuraM.SelectedTime = new DateTime();
                }
            }
        }

        private void CB_Pomeriggio_Click(object sender, RoutedEventArgs e)
        {
            if (CB_Pomeriggio.IsChecked == true)
            {
                TP_AperturaP.SelectedTime = new DateTime(2000, 1, 1, 0, 0, 1);
                TP_ChiusuraP.SelectedTime = new DateTime(2000, 1, 1, 0, 0, 1);
            }
            else
            {
                if (DataGridOrario.SelectedIndex != -1)
                {
                    CB_Giorni.SelectedIndex = DataGridOrario.SelectedIndex;

                    //Imposta orario nelle caselle di testo
                    COrario selezionato = (COrario)DataGridOrario.SelectedItem;
                    TP_AperturaP.SelectedTime = new DateTime(2000, 1, 1, selezionato.DalleP.Hour, selezionato.DalleP.Minute, 1);
                    TP_ChiusuraP.SelectedTime = new DateTime(2000, 1, 1, selezionato.AlleP.Hour, selezionato.AlleP.Minute, 1);
                }
                else
                {
                    CB_Giorni.SelectedIndex = DataGridOrario.SelectedIndex;

                    TP_AperturaP.SelectedTime = new DateTime();
                    TP_ChiusuraP.SelectedTime = new DateTime();
                }
            }
        }

        private void BT_AggiornaOrario_Click(object sender, RoutedEventArgs e)
        {
            //Orario apertura e chiusura mattina, orario apertura e chiusura pomeriggio
            DateTime orario1M = TP_AperturaM.SelectedTime.Value; 
            DateTime orario2M = TP_ChiusuraM.SelectedTime.Value;
            DateTime orario1P = TP_AperturaP.SelectedTime.Value;
            DateTime orario2P = TP_ChiusuraP.SelectedTime.Value;

            //Controlla che le caselle non siano vuote
            if (orario1M == null || orario2M == null || orario1P == null || orario2P == null)
            {
                MessageBox.Show("Uno o più orari non sono stati inseriti!", "Attenzione!");
            }
            else if (orario1M > orario2M)
            {
                MessageBox.Show("L'orario di apertura della mattina non è corretto!", "Attenzione!");
            }
            else if (orario1P > orario2P)
            {
                MessageBox.Show("L'orario di apertura del pomeriggio non è corretto!", "Attenzione!");
            }
            else
            {
                /* Aggiorna orario del database e ricarica la pagina */
                db.AggiornaOrario(CB_Giorni.SelectedItem.ToString() ,orario1M, orario2M, orario1P, orario2P);
                UpdateOrario();
            }
        }

        /**
         * @brief Metodo eseguito quando cambia la selezione della combobox giorni
         */
        private void CB_Giorni_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                /* Mantiene datagrid e combobox sincronizzati */
                DataGridOrario.SelectedIndex = CB_Giorni.SelectedIndex;
            }
            catch (Exception)
            {

            }
        }
    
    
    
    
    
    }
}
