﻿using NodaTime;
using Progetto_Palestra.Classi;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Progetto_Palestra.Interfacce
{
    /// <summary>
    /// Logica di interazione per UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        ////PROPERTIES
        public string Username { get; set; }
        private CAtleta atleta { get; set; }
        private CAllenamento allenamento { get; set; }
        private MySQLdatabase db;
        public bool Logout { get; set; }

        ////COSTRUTTORE CON PARAMETRI
        public UserWindow(string Username)
        {
            InitializeComponent();

            this.Username = Username;
            db = new MySQLdatabase();

            /* Inizializza atleta */
            atleta = db.GetAtleta(Username);

            /* Inizializza la dashboard */
            UpdateDashboard();
        }


        #region METODI AGGIORNAMENTO VISUALIZZAZIONE

        ////AGGIORNA VISUALIZZAZIONE DASHBOARD
        public void UpdateDashboard()
        {
            /* Rende visibile solo la dashboard */
            GridDashboard.Visibility = Visibility.Visible;
            GridProfilo.Visibility = Visibility.Hidden;
            GridAbbonamento.Visibility = Visibility.Hidden;
            GridAllenamento.Visibility = Visibility.Hidden;
            GridAllenamPrec.Visibility = Visibility.Hidden;
            GridSegnala.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Hidden;



            /* Inserisce dati nelle label */

            int numAll = db.GetNumAllenamenti(atleta.ID);
            int numAllW = db.GetNumAllenamentiW(atleta.ID);
            LabelNumAllenamenti.Content = numAll;
            LabelNumAllenamentiW.Content = numAllW + " questa settimana";
            int segn = db.CountSegnalazioni(atleta.ID);
            LabelSegnalazioni.Content = segn;
            LabelNumVisite.Content = db.CountVisite(atleta.ID);
            LabelNumVisiteW.Content = db.CountVisiteWeek(atleta.ID) + " questa settimana";

            /* Inserisce dati nel calendario */
            List<CAllenamento> allenamenti = db.SelectAllenamentiAtleta(atleta.ID);

            foreach (var allenamento in allenamenti)
            {
                CalendarAllenamenti.SelectedDates.Add(allenamento.Data); //Inserisce data nell'array
            }
        }

        ////AGGIORNA VISUALIZZAZIONE PROFILO
        public void UpdateProfilo()
        {
            //Visualizza solo sezione profilo
            GridDashboard.Visibility = Visibility.Hidden;
            GridProfilo.Visibility = Visibility.Visible;
            GridAbbonamento.Visibility = Visibility.Hidden;
            GridAllenamento.Visibility = Visibility.Hidden;
            GridAllenamPrec.Visibility = Visibility.Hidden ;
            GridSegnala.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Hidden;


            //Inserisce dati nelle textbox
            TB_Password.Text = "";
            TB_Nome.Text = atleta.Nome;
            TB_Cognome.Text = atleta.Cognome;
            TB_Residenza.Text = atleta.Residenza;
            DP_Nascita.SelectedDate = atleta.DataNascita;
            if (atleta.Sesso == "M")
                RadioM.IsChecked = true;
            else
                RadioF.IsChecked = true;
        }

        ////AGGIORNA VISUALIZZAZIONE ABBONAMENTO
        public void UpdateAbbonamento()
        {
            GridDashboard.Visibility = Visibility.Hidden;
            GridProfilo.Visibility = Visibility.Hidden;
            GridAbbonamento.Visibility = Visibility.Visible;
            GridAllenamento.Visibility = Visibility.Hidden;
            GridAllenamPrec.Visibility = Visibility.Hidden;
            GridSegnala.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Hidden;

            //INSERISCE DATI NELLA LABEL
            TB_Scadenza.Text = db.SelectScadenza(Username);
        }

        ////AGGIORNA VISUALIZZAZIONE ALLENAMENTO
        public void UpdateAlleamento()
        {
            if (!db.CheckAccesso(atleta.ID)) //Se accesso non eseguito
            {
                MessageBox.Show("L'accesso in palestra non è stato eseguito!");
                UpdateDashboard();
                LV_Menu.SelectedIndex = 0;
            }
            else //Altrimenti visualizza grid allenamento
            {
                GridDashboard.Visibility = Visibility.Hidden;
                GridProfilo.Visibility = Visibility.Hidden;
                GridAbbonamento.Visibility = Visibility.Hidden;
                GridAllenamento.Visibility = Visibility.Visible;
                GridAllenamPrec.Visibility = Visibility.Hidden;
                GridSegnala.Visibility = Visibility.Hidden;
                GridLogout.Visibility = Visibility.Hidden;

                //Inserisce dati nella datagrid
                DG_Attrezzi.ItemsSource = db.SelectAttrezziDisp();

                //Aggiornaselezione bottoni
                GridMenu.IsEnabled = true;
                BT_Inizia.IsEnabled = true;
                BT_Termina.IsEnabled = false;
                BT_Seleziona.IsEnabled = false;
                BT_Rilascia.IsEnabled = false;
            }
        }

        ////AGGIORNA VISUALIZZAZIONE ALLENAMENTO PRECEDENTE
        public void UpdateAllenamPrec()
        {
            GridDashboard.Visibility = Visibility.Hidden;
            GridProfilo.Visibility = Visibility.Hidden;
            GridAbbonamento.Visibility = Visibility.Hidden;
            GridAllenamento.Visibility = Visibility.Hidden;
            GridAllenamPrec.Visibility = Visibility.Visible;
            GridSegnala.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Hidden;

            //INSERISCE DATI NELLA DATAGRID
            DG_Allenamenti.ItemsSource = db.SelectAllenamentiAtleta(atleta.ID);
            DG_Allenamenti.Columns[0].Visibility = Visibility.Collapsed;
            DG_Allenamenti.Columns[4].Visibility = Visibility.Collapsed;
        }

        ////AGGIORNA VISUALIZZAZIONE SEGNALA
        public void UpdateSegnala()
        {
            //Visualizza solo grid segnala
            GridDashboard.Visibility = Visibility.Hidden;
            GridProfilo.Visibility = Visibility.Hidden;
            GridAbbonamento.Visibility = Visibility.Hidden;
            GridAllenamento.Visibility = Visibility.Hidden;
            GridAllenamPrec.Visibility = Visibility.Hidden;
            GridSegnala.Visibility = Visibility.Visible;
            GridLogout.Visibility = Visibility.Hidden;

            //Inserisce dati nella datagrid
            DG_Segnala.ItemsSource = db.SelectAttrezziDisp();
            if (DG_Segnala.Items.Count > 0)
                DG_Segnala.SelectedIndex = 0;
        }

        ////AGGIORNA VISUALIZZAZIONE LOGOUT
        public void UpdateLogout()
        {
            GridDashboard.Visibility = Visibility.Hidden;
            GridProfilo.Visibility = Visibility.Hidden;
            GridAbbonamento.Visibility = Visibility.Hidden;
            GridAllenamento.Visibility = Visibility.Hidden;
            GridAllenamPrec.Visibility = Visibility.Hidden;
            GridSegnala.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Visible;
        }

        #endregion

        #region METODI COMPONENTI

        ////SELEZIONE DASHBOARD
        private void LWDashboard_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UpdateDashboard();
        }

        ////SELEZIONE PROFILO
        private void LVInfo_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UpdateProfilo();
        }

        ////SELEZIONE ABBONAMENTO
        private void LVAbbonamento_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UpdateAbbonamento();
        }

        ////SELEZIONE ALLENAMENTO
        private void LVAllenamento_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UpdateAlleamento();
        }

        ////SELEZIONE ALLENAMENTO PRECEDENTE
        private void LVAllenamentoPrec_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UpdateAllenamPrec();
        }

        ////SELEZIONE SEGNALA
        private void LVSegnala_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UpdateSegnala();
        }

        ////SELEZIONE LOGOUT
        private void LVLogout_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UpdateLogout();
        }

        ////BOTTONE RINNOVA ABBONAMENTO
        private void BT_Rinnova_Click(object sender, RoutedEventArgs e)
        {
            string data = TB_Scadenza.Text;
            int g = Int32.Parse(data.Substring(0, 2));
            int m = Int32.Parse(data.Substring(3, 2));
            int a = Int32.Parse(data.Substring(6, 4));

            DateTime scadenza = new DateTime(a, m, g); //Calcola data scadenza
            DateTime oggi = DateTime.Today; //Calcola oggi

            //Se abbonamento scaduto
            if (scadenza < oggi)
            {
                //Imposta nuova scadenza
                scadenza = oggi;
            }


            //Apre finestra di pagamento
            PagamentoWindow pw = new PagamentoWindow();
            pw.ShowDialog();
            if (pw.Result == true)
            {

                //Aggiorna scadenza
                switch (pw.Durata)
                {
                    case "1mese": scadenza = scadenza.AddMonths(1); break;
                    case "3mese": scadenza = scadenza.AddMonths(3); break;
                    case "12mese": scadenza = scadenza.AddYears(1); break;
                }
                db.UpdateScadenza(Username, scadenza);
                UpdateAbbonamento();
            }

        }

        ////SELEZIONE DATAGRID ATTREZZI CAMBIATA
        private void DG_Attrezzi_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (DG_Attrezzi.SelectedIndex != -1)
            {

                try
                {
                    ImageLink.Source = new BitmapImage(new Uri(((CAttrezzo)DG_Attrezzi.SelectedItem).Link));
                }
                catch (Exception)
                {

                }
            }
        }

        ////BOTTONE INIZIA ALLENAMENTO
        private void BT_Inizia_Click(object sender, RoutedEventArgs e)
        {
            ////Disabilita / abilita componenti corrispondenti
            GridMenu.IsEnabled = false;
            BT_Inizia.IsEnabled = false;
            BT_Termina.IsEnabled = true;
            BT_Seleziona.IsEnabled = true;
            BT_Rilascia.IsEnabled = false;
            MessageBox.Show("Allenamento iniziato");

            ////Inizializza oggetto CAllenamento
            allenamento = new CAllenamento();
            allenamento.Data = DateTime.Today;
            allenamento.Atleta = atleta.ID;
            DateTime ora = DateTime.Now;
            allenamento.Ora_Inizio = new LocalTime(ora.Hour, ora.Minute, ora.Second);
        }

        ////BOTTONE TERMINA ALLENAMENTO
        private void BT_Termina_Click(object sender, RoutedEventArgs e)
        {
            //Controlla attrezzo rilasciato
            if (TB_Attrezzo.Text != "")
                MessageBox.Show("Rilasciare attrezzo prima di terminare l'allenamento");
            else
            {
                //Termina allenamento
                DateTime ora = DateTime.Now;
                allenamento.OraFine = new LocalTime(ora.Hour, ora.Minute, ora.Second);

                //Aggiunge allenamento al db
                db.InsertAllenamento(allenamento);

                BT_Inizia.IsEnabled = true;
                GridMenu.IsEnabled = true;
                BT_Termina.IsEnabled = false;
                BT_Seleziona.IsEnabled = false;
                BT_Rilascia.IsEnabled = false;
                MessageBox.Show("Allenamento terminato");
            }
        }

        ////BOTTONE SELEZIONA ATTREZZO
        private void BT_Seleziona_Click(object sender, RoutedEventArgs e)
        {
            if (DG_Attrezzi.SelectedIndex != -1)
            {
                int selezionato = ((CAttrezzo)DG_Attrezzi.SelectedItem).ID_Attrezzo;

                //Update attrezzo
                db.UpdateAttrezzoSeleziona(selezionato);

                //Inserisce nella textbox
                TB_Attrezzo.Text = ((CAttrezzo)DG_Attrezzi.SelectedItem).Nome;

                //Disabilita/abilita corrispondenti
                DG_Attrezzi.IsEnabled = false;
                BT_Seleziona.IsEnabled = false;
                BT_Rilascia.IsEnabled = true;
            }
            else
                MessageBox.Show("Nessun attrezzo selezionato!");
        }

        ////BOTTONE RILASCIA ATTREZZO
        private void BT_Rilascia_Click(object sender, RoutedEventArgs e)
        {
            DG_Attrezzi.IsEnabled = true;
            int selezionato = ((CAttrezzo)DG_Attrezzi.SelectedItem).ID_Attrezzo;

            //Update attrezzo
            db.UpdateAttrezzoRilascia(selezionato);

            //Inserisce nella textbox
            TB_Attrezzo.Text = "";
            BT_Seleziona.IsEnabled = true;
            BT_Rilascia.IsEnabled = false;



            //Update datagrid
            DG_Attrezzi.ItemsSource = db.SelectAttrezziDisp();
        }

        ////BOTTONE SEGNALA ATTREZZO    
        private void BT_Segnala_Click(object sender, RoutedEventArgs e)
        {
            //Controlla elemento selezionato
            if (DG_Segnala.SelectedIndex == -1)
                MessageBox.Show("Nessun attrezzo selezionato!");
            else
            {
                MessageBox.Show("Segnalazione aggiunta");
                int selezionato = ((CAttrezzo)DG_Segnala.SelectedItem).ID_Attrezzo;
                db.UpdateAttrezzoSegnalato(selezionato);//Modifica valore attrezzo per non essere utilizzato
                db.InsertSegnalazione(selezionato, atleta.ID);  //Aggiunta segnalazione
                //Ritorna alla dashboard
                UpdateDashboard();
                LV_Menu.SelectedIndex = 0;
            }
        }

        ////BOTTONE ANNULLA SEGNALAZIONE
        private void BT_Segnala_Annulla_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Segnalazione annullata");
            UpdateDashboard();
            LV_Menu.SelectedIndex = 0;
        }

        ////BOTTONE LOGOUT RITORNO ALLA HOME
        private void BT_Logout_Click(object sender, RoutedEventArgs e)
        {
            Logout = true;
            this.Close();
        }

        ////BOTTONE CHIUDI PROGRAMMA
        private void BT_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        ////PRESSIONE MOUSE SUL CALENDARIO
        private void CalendarAllenamenti_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            /* Inserisce dati nel calendario */
            MySQLdatabase db = new MySQLdatabase();
            List<CAllenamento> allenamenti = db.SelectAllenamentiAtleta(atleta.ID);
            CalendarAllenamenti.SelectedDates.Clear();
            foreach (var allenamento in allenamenti)
            {
                CalendarAllenamenti.SelectedDates.Add(allenamento.Data); //Inserisce data nell'array
            }
        }

        ////PRESSIONE BOTTONE ANNULLA
        private void BT_Annulla_Click(object sender, RoutedEventArgs e)
        {
            //Ritorna alla dashboard
            UpdateDashboard();
        }

        ////PRESSIONE BOTTONE CONFERMA
        private void BT_Conferma_Click(object sender, RoutedEventArgs e)
        {
            //Controlla se i campi dati sono vuoti
            if (TB_Nome.Text == "" || TB_Cognome.Text == ""
                || TB_Residenza.Text == "" || DP_Nascita.SelectedDate == null)
                MessageBox.Show("Alcuni campi sono vuoti. Inserire i parametri e riprovare");
            else
            {
                if (TB_Password.Text != "")
                {
                    atleta.Password = ConvertMD5(TB_Password.Text);
                }

                atleta.Nome = TB_Nome.Text;
                atleta.Cognome = TB_Cognome.Text;
                atleta.Residenza = TB_Residenza.Text;
                atleta.DataNascita = DP_Nascita.SelectedDate.Value;
                if (RadioM.IsChecked == true)
                    atleta.Sesso = "M";
                else
                    atleta.Sesso = "F";

                db.UpdateAtleta(atleta);
                UpdateDashboard();
                MessageBox.Show("Informazioni atleta aggiornate.");
            }
        }

        #endregion

        #region ALTRI METODI

        ////CONVERTE STRINGA INPUT IN MD5
        private string ConvertMD5(string Password)
        {
            StringBuilder hash = new StringBuilder(); //Crea hash da impostare
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(Password));
            for (int i = 0; i < bytes.Length; i++)
                hash.Append(bytes[i].ToString("x2"));

            //Converte l'hash in stringa
            return hash.ToString();
        }

        #endregion
    }
}
