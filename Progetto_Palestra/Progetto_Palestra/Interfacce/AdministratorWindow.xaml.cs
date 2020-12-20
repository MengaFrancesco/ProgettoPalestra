using Progetto_Palestra.Classi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
        public bool Logout;
        private MySQLdatabase db;

        ////COSTRUTTORE CON PARAMETRI
        public AdministratorWindow(CAmministratore Admin)
        {

            InitializeComponent();

            this.Admin = Admin;
            Logout = false;
            db = new MySQLdatabase();

            LabelBenvenuto.Content += " " + Admin.Nome;

            UpdateDashboard(); //Aggiorna la dashboard
        }

        #region UPDATE COMPONENTI


        ////AGGIORNA VISUALIZZAZIONE DASHBOARD
        public void UpdateDashboard()
        {
            //VISUALIZZA SOLO DASHBOARD
            GridDashboard.Visibility = Visibility.Visible;
            GridAtleti.Visibility = Visibility.Hidden;
            GridOrario.Visibility = Visibility.Hidden;
            GridControllore.Visibility = Visibility.Hidden;
            GridMeccanici.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Hidden;

            //AGGIORNA DATI
            int num = db.CountAtleti();
            int num2 = db.CountAtletiWeek();
            LabelNumAtleti.Content = num.ToString();
            LabelNumAtletiW.Content = "+" + num2.ToString() + " questa settimana";
            LabelNumAbbonati.Content = db.CountAbbonati();
            LabelNumScaduti.Content = db.CountScaduti() + " scaduti/non rinnovati";
            LabelNumVisite.Content = db.CountVisite();
            LabelNumVisiteW.Content = db.CountVisiteWeek() + " questa settimana";
            LabelNumAllenamenti.Content = db.CountAllenamenti();
            LabelNumAllenamentiW.Content = db.CountAllenamentiW() + " questa settimana";

            //Aggiorna i grafici a torta
            UpdateTipoUtenti();
            UpdateStoricoIscrizioni();
        }

        ////AGGIORNA VISUALIZZAZIONE ATLETI
        public void UpdateAtleti()
        {
            //Visualizza solo sezione atleti
            GridDashboard.Visibility = Visibility.Hidden;
            GridAtleti.Visibility = Visibility.Visible;
            GridOrario.Visibility = Visibility.Hidden;
            GridControllore.Visibility = Visibility.Hidden;
            GridMeccanici.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Hidden;

            //Carica dati atleti
            DataGridAtleti.ItemsSource = db.SelectAtleti();
        }

        ////AGGIORNA VISUALIZZAZIONE ORARIO
        public void UpdateOrario()
        {
            //Visualizza solo sezione orario
            GridDashboard.Visibility = Visibility.Hidden;
            GridAtleti.Visibility = Visibility.Hidden;
            GridOrario.Visibility = Visibility.Visible;
            GridControllore.Visibility = Visibility.Hidden;
            GridMeccanici.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Hidden;

            //Aggiorna datagrid
            List<COrario> orario = new List<COrario>();
            orario = db.GetOrario();
            DataGridOrario.ItemsSource = orario;
            DataGridOrario.SelectedIndex = 0;
        }

        ////AGGIORNA VISUALIZZAZIONE CONTROLLORI
        public void UpdateControllori()
        {
            //Visualizza solo sezione controllori
            GridDashboard.Visibility = Visibility.Hidden;
            GridAtleti.Visibility = Visibility.Hidden;
            GridOrario.Visibility = Visibility.Hidden;
            GridControllore.Visibility = Visibility.Visible;
            GridMeccanici.Visibility = Visibility.Hidden;

            //Aggiorna datagrid
            try
            {
                List<string> lista = db.GetControllori();
                List<CControllore> cc = new List<CControllore>();

                for (int i = 0; i < lista.Count; i++)
                    cc.Add(new CControllore(lista[i]));

                DataGridControllori.ItemsSource = cc;
            }
            catch (Exception) { };
        }

        ////AGGIORNA VISUALIZZAZIONE ATTREZZI
        public void UpdateAttrezzi()
        {
            //Visualizza solo sezione attrezzi
            GridDashboard.Visibility = Visibility.Hidden;
            GridAtleti.Visibility = Visibility.Hidden;
            GridOrario.Visibility = Visibility.Hidden;
            GridControllore.Visibility = Visibility.Hidden;
            GridMeccanici.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Hidden;
        }

        ////AGGIORNA VISUALIZZAZIONE MECCANICI
        public void UpdateMeccanici()
        {
            //Visualizza solo sezione meccanici
            GridDashboard.Visibility = Visibility.Hidden;
            GridAtleti.Visibility = Visibility.Hidden;
            GridOrario.Visibility = Visibility.Hidden;
            GridControllore.Visibility = Visibility.Hidden;
            GridMeccanici.Visibility = Visibility.Visible;
            GridLogout.Visibility = Visibility.Hidden;

            //Inserimento dati nella datagrid meccanici
            DG_Meccanici.ItemsSource = db.SelectMeccanici();
        }

        ////AGGIORNA VISUALIZZAZIONE AMMINISTRATORI
        public void UpdateAmministratori()
        {
            //Logout
            GridDashboard.Visibility = Visibility.Hidden;
            GridAtleti.Visibility = Visibility.Hidden;
            GridOrario.Visibility = Visibility.Hidden;
            GridControllore.Visibility = Visibility.Hidden;
            GridMeccanici.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Hidden;
        }

        ////AGGIORNA VISUALIZZAZIONE LOGOUT
        public void UpdateLogout()
        {
            //Logout
            GridDashboard.Visibility = Visibility.Hidden;
            GridAtleti.Visibility = Visibility.Hidden;
            GridOrario.Visibility = Visibility.Hidden;
            GridControllore.Visibility = Visibility.Hidden;
            GridMeccanici.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Visible;
        }

        #endregion

        #region METODI COMPONENTI

        ////LIST VIEW DASHBOARD
        private void LWDashboard_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateDashboard();
        }

        ////LIST VIEW ATLETI
        private void LWAtleti_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateAtleti();
        }

        ////LIST VIEW ORARIO
        private void LWOrario_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateOrario();
        }

        ////LIST VIEW CONTROLLORI
        private void LWControllori_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateControllori();
        }

        ////LIST VIEW ATTREZZI
        private void LWAttrezzi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateAttrezzi();
        }

        ////LIST VIEW MECCANICI
        private void LWMeccanici_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateMeccanici();
        }

        ////LIST VIEW AMMINISTRATORI
        private void LWAmministratori_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateAmministratori();
        }

        ////LIST VIEW LOGOUT
        private void LWLogout_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateLogout();
        }

        ////CAMBIO SELEZIONE DATAGRID ATLETI
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

        ////CONTROLLO INSERIMENTO INTERI NELLA TEXTBOX
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            //Controlla inserimento numerico
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        ////ANNULLA SELEZIONE DATAGRID ATLETI
        private void BT_Annulla_Click(object sender, RoutedEventArgs e)
        {
            DataGridAtleti.UnselectAll();
            ClearDatiAtleti();
        }

        ////ELIMINA ATLETA SELEZIONATO
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

        ////AGGIORNA ATLETA SELEZIONATO
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

        ////BOTTONE LOGOUT
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

        ////BOTTONE ANNULLA CHIUSURA
        private void BT_Ret_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            UpdateDashboard();
        }

        ////INIZIALIZZAZIONE COMBOBOX GIORNI
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

        ////INIZIALIZZAZIONE DATAGRID ORARIO
        private void DataGridOrario_Initialized(object sender, EventArgs e)
        {
            List<COrario> orario = new List<COrario>();
            db = new MySQLdatabase();
            orario = db.GetOrario();
            DataGridOrario.ItemsSource = orario;
            DataGridOrario.SelectedIndex = 0;
        }

        ////CAMBIO SELEZIONE DATAGRID ORARIO
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

        ////CLICK CHECKBOX ORARIO MATTINA
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

        ////CLICK CHECKBOX ORARIO POMERIGGIO
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

        ////BOTTONE AGGIORNA ORARIO
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
                db.AggiornaOrario(CB_Giorni.SelectedItem.ToString(), orario1M, orario2M, orario1P, orario2P);
                UpdateOrario();
            }
        }

        ////CAMBIO SELEZIONATO COMBOBOX GIORNI
        private void CB_Giorni_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                /* Mantiene datagrid e combobox sincronizzati */
                DataGridOrario.SelectedIndex = CB_Giorni.SelectedIndex;
            }
            catch (Exception) { }
        }

        ////INIZIALIZZAZIONE DATAGRID CONTROLLORI
        private void DataGridControllori_Initialized(object sender, EventArgs e)
        {
            try
            {
                List<string> lista = db.GetControllori();
                List<CControllore> cc = new List<CControllore>();

                for (int i = 0; i < lista.Count; i++)
                {
                    cc.Add(new CControllore(lista[i]));
                }

                DataGridControllori.ItemsSource = cc;
            }
            catch (Exception) { }
        }

        ////CAMBIO SELEZIONATO DATAGRID CONTROLLORI
        private void DataGridControllori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = DataGridControllori.SelectedIndex;
            try
            {
                if (index != -1)
                {
                    //Carica nelle caselle di testo
                    CControllore cc = (CControllore)DataGridControllori.SelectedItem;
                    TB_Username_Controllore.Text = cc.Username;
                    TB_Password_Controllore.Text = cc.Password;
                    TB_Nome_Controllore.Text = cc.Nome;
                    TB_Cognome_Controllore.Text = cc.Cognome;
                    DP_Iscrizione_Controllore.SelectedDate = cc.Iscrizione;
                }
                else
                {
                    TB_Username_Controllore.Text = "";
                    TB_Password_Controllore.Text = "";
                    TB_Nome_Controllore.Text = "";
                    TB_Cognome_Controllore.Text = "";
                    DP_Iscrizione_Controllore.SelectedDate = null;
                }
            }
            catch (Exception)
            {

            }

        }

        ////AGGIUNTA CONTROLLORE ALLA LISTA
        private void BT_CAggiungi_Click(object sender, RoutedEventArgs e)
        {
            string username = TB_Username_Controllore.Text;
            string password = TB_Password_Controllore.Text;
            string nome = TB_Nome_Controllore.Text;
            string cognome = TB_Cognome_Controllore.Text;
            DateTime iscrizione = DP_Iscrizione_Controllore.SelectedDate.Value;


            /* Controlla che le caselle non siano vuote */
            if (username == "" || password == "" || nome == ""
            || cognome == "" || iscrizione == null)
                MessageBox.Show("Inserire i dati richiesti e riprovare");
            /* Controlla username esistente */
            else if (db.CountControllori(TB_Username_Controllore.Text) >= 1)
                MessageBox.Show("Username esistente!");
            else
            {
                db.AggiungiControllore(new CControllore(0, username, password, nome, cognome, iscrizione));
                UpdateControllori();
            }

        }

        ////ELIMINA CONTROLLORE SELEZIONATO
        private void BT_CElimina_Click(object sender, RoutedEventArgs e)
        {
            //Controlla se elemento selezionato
            int index = DataGridControllori.SelectedIndex;
            if (index != -1)
            {
                int id = ((CControllore)DataGridControllori.SelectedItem).ID;
                db.DeleteControllore(id);
                UpdateControllori();
            }
            else
            {
                MessageBox.Show("Nessun elemento selezionato!");
            }
        }

        ////AGGIORNA CONTROLLORE SELEZIONATO
        private void BT_CAggiorna_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = TB_Username_Controllore.Text;
                string password = TB_Password_Controllore.Text;
                string nome = TB_Nome_Controllore.Text;
                string cognome = TB_Cognome_Controllore.Text;
                DateTime iscrizione = DP_Iscrizione_Controllore.SelectedDate.Value;


                /* Controlla che le caselle non siano vuote */
                if (DataGridControllori.SelectedIndex == -1)
                    MessageBox.Show("Nessun elemento selezionato!");
                else if (username == "" || password == "" || nome == ""
                    || cognome == "" || iscrizione == null)
                    MessageBox.Show("Inserire i dati richiesti e riprovare");
                /* Controlla username esistente */
                else if (db.CountControllori(TB_Username_Controllore.Text) >= 1 && ((CControllore)DataGridControllori.SelectedItem).Username != username)
                    MessageBox.Show("Username esistente!");
                else
                {
                    MessageBox.Show("Aggiornato");
                    int id = ((CControllore)DataGridControllori.SelectedItem).ID;
                    db.UpdateControllore(new CControllore(id, username, password, nome, cognome, iscrizione));
                    UpdateControllori();
                }
            }
            catch (Exception)
            {

            }

        }

        ////ANNULLA SELEZIONE CONTROLLORE E RITORNA ALLA DASHBOARD
        private void BT_CAnnulla_Click(object sender, RoutedEventArgs e)
        {
            UpdateDashboard();
        }

        ////AGGIUNGI ATLETA ALLA LISTA
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

        ////ELEMENTO SEELZIONATO DELLA DATAGRID MECCANICI CAMBIATO
        private void DG_Meccanici_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DG_Meccanici.SelectedIndex != -1) //Se elemento selezionato
            {
                //Inserimento dati nelle caselle di testo
                CMeccanico selezionato = (CMeccanico)DG_Meccanici.SelectedItem;
                TB_NomeMeccanico.Text = selezionato.Nome;
                TB_CognomeMeccanico.Text = selezionato.Cognome;
                TB_UsernameMeccanico.Text = selezionato.Username;
                TB_PasswordMeccanico.Text = selezionato.Password;
            }
            else
            {
                TB_NomeMeccanico.Text = "";
                TB_CognomeMeccanico.Text = "";
                TB_UsernameMeccanico.Text = "";
                TB_PasswordMeccanico.Text = "";
            }
        }

        ////BOTTONE AGGIUNGI MECCANICO
        private void BT_AggiungiMeccanico_Click(object sender, RoutedEventArgs e)
        {
            //Controllo campi dati
            if (!ControllaInsMeccanico())
                MessageBox.Show("Inserire i dati nelle caselle di testo e riprovare");
            else if (!db.CheckMeccanico(TB_UsernameMeccanico.Text))
                MessageBox.Show("Username del meccanico esistente!");
            else
            {
                //Crea oggetto e lo inserisce nel database
                CMeccanico meccanico = new CMeccanico(0, TB_NomeMeccanico.Text, TB_CognomeMeccanico.Text, TB_UsernameMeccanico.Text, TB_PasswordMeccanico.Text);
                db.InsertMeccanico(meccanico);

                //Visualizza esito e aggiorna pagina
                MessageBox.Show("Meccanico aggiunto");
                UpdateMeccanici();
            }
        }

        ////BOTTONE AGGIORNA MECCANICO
        private void BT_AggiornaMeccanico_Click(object sender, RoutedEventArgs e)
        {
            //Controlla elemento selezionato
            if (DG_Meccanici.SelectedIndex == -1)
                MessageBox.Show("Nessun elemento selezionato");
            else if (!ControllaInsMeccanico())
                MessageBox.Show("Inserire i dati nelle caselle di testo e riprovare");
            else
            {
                //Crea oggetto CMeccanico e lo aggiorna
                int id = ((CMeccanico)DG_Meccanici.SelectedItem).ID_Meccanico;
                CMeccanico meccanico = new CMeccanico(id, TB_NomeMeccanico.Text, TB_CognomeMeccanico.Text, TB_UsernameMeccanico.Text, TB_PasswordMeccanico.Text);
                db.UpdateMeccanico(meccanico);

                //Visualizza informazione e aggiorna visualizzazione
                MessageBox.Show("Informazioni meccanico aggiornate");
                UpdateMeccanici();
            }

        }

        ////BOTTONE ELIMINA DATI MECCANICO
        private void BT_EliminaMeccanico_Click(object sender, RoutedEventArgs e)
        {
            //Controllo selezione
            if (DG_Meccanici.SelectedIndex == -1)
                MessageBox.Show("Nessun elemento selezionato");
            else
            {
                //Rimuove meccanico dal database
                int id = ((CMeccanico)DG_Meccanici.SelectedItem).ID_Meccanico;
                db.DeleteMeccanico(id);

                //Visualizza informazione e aggiorna visualizzazione
                MessageBox.Show("Il meccanico selezionato è stato eliminato");
                UpdateMeccanici();
            }

        }

        ////BOTTONE ANNULA DATI MECCANICO
        private void BT_AnnullaMeccanico_Click(object sender, RoutedEventArgs e)
        {
            //Svuota caselle di testo e deseleziona elemento
            TB_Nome.Text = "";
            TB_Cognome.Text = "";
            TB_Username.Text = "";
            TB_Password.Text = "";
            DG_Meccanici.SelectedIndex = -1;
        }

        #endregion

        #region ALTRI METODI

        ////SVUOTA CONTENUTO TEXTBOX ATLETI
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
              
        ////CONTROLLO INSERIMENTO INFORMAZIONI ATLETA
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

        private bool ControllaInsMeccanico()
        {
            if (TB_NomeMeccanico.Text != "" && TB_CognomeMeccanico.Text != "" && TB_UsernameMeccanico.Text != "" && TB_PasswordMeccanico.Text != "")
                return true;
            else
                return false;
        }

        #endregion

        #region METODI GRAFICI

        ////AGGIORNA PIE CHART (GRAFICI A TORTA)
        private void UpdateTipoUtenti()
        {
            //Calcola numero di utenti
            int amministratori = db.CountAmministratori();
            int atleti = db.CountAtleti();
            int controllori = db.CountControllori();
            int meccanici = db.CountMeccanici();
            int totale = amministratori + atleti + controllori + meccanici;

            int percAdmin = (100 * amministratori) / totale;
            int percAtleti = (100 * atleti) / totale;
            int percControllori = (100 * controllori) / totale;
            int percMeccanici = (100 * meccanici) / totale;

            #region Pie Chart Iscritti
            CanvasIscritti.Children.Clear();
            float pieWidth = 232, pieHeight = 232, centerX = pieWidth / 2, centerY = pieHeight / 2, radius = pieWidth / 2;
            CanvasIscritti.Width = pieWidth;
            CanvasIscritti.Height = pieHeight;

            List<Category> Categories = new List<Category>()
            {
                new Category
                {
                    Title = "Amministratori",
                    Percentage = percAdmin,
                    ColorBrush = Brushes.Red,
                },

                new Category
                {
                    Title = "Atleti",
                    Percentage = percAtleti,
                    ColorBrush = Brushes.Green,
                },

                new Category
                {
                    Title = "Controllori",
                    Percentage = percControllori,
                    ColorBrush = Brushes.Blue,
                },

                new Category
                {
                    Title = "Meccanici",
                    Percentage = percMeccanici,
                    ColorBrush = Brushes.Yellow,
                }
            };


            // draw pie
            float angle = 0, prevAngle = 0;
            foreach (var category in Categories)
            {
                double line1X = (radius * Math.Cos(angle * Math.PI / 180)) + centerX;
                double line1Y = (radius * Math.Sin(angle * Math.PI / 180)) + centerY;

                angle = category.Percentage * (float)360 / 100 + prevAngle;
                Debug.WriteLine(angle);

                double arcX = (radius * Math.Cos(angle * Math.PI / 180)) + centerX;
                double arcY = (radius * Math.Sin(angle * Math.PI / 180)) + centerY;

                var line1Segment = new LineSegment(new System.Windows.Point(line1X, line1Y), false);
                double arcWidth = radius, arcHeight = radius;
                bool isLargeArc = category.Percentage > 50;
                var arcSegment = new ArcSegment()
                {
                    Size = new System.Windows.Size(arcWidth, arcHeight),
                    Point = new System.Windows.Point(arcX, arcY),
                    SweepDirection = SweepDirection.Clockwise,
                    IsLargeArc = isLargeArc,
                };
                var line2Segment = new LineSegment(new System.Windows.Point(centerX, centerY), false);

                var pathFigure = new PathFigure(
                    new System.Windows.Point(centerX, centerY),
                    new List<PathSegment>()
                    {
                        line1Segment,
                        arcSegment,
                        line2Segment,
                    },
                    true);

                var pathFigures = new List<PathFigure>() { pathFigure, };
                var pathGeometry = new PathGeometry(pathFigures);
                var path = new Path()
                {
                    Fill = category.ColorBrush,
                    Data = pathGeometry,
                };
                CanvasIscritti.Children.Add(path);

                prevAngle = angle;


                // draw outlines
                var outline1 = new Line()
                {
                    X1 = centerX,
                    Y1 = centerY,
                    X2 = line1Segment.Point.X,
                    Y2 = line1Segment.Point.Y,
                    Stroke = Brushes.White,
                    StrokeThickness = 5,
                };
                var outline2 = new Line()
                {
                    X1 = centerX,
                    Y1 = centerY,
                    X2 = arcSegment.Point.X,
                    Y2 = arcSegment.Point.Y,
                    Stroke = Brushes.White,
                    StrokeThickness = 5,
                };

                CanvasIscritti.Children.Add(outline1);
                CanvasIscritti.Children.Add(outline2);
            }


            #endregion


        }

        ////AGGIORNA GRAFICO ISCRIZIONI
        private void UpdateStoricoIscrizioni()
        {
            List<int> conteggio = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                conteggio.Add(db.CountIscrizioni(DateTime.Today.AddDays(i - 9)));
            }

            CanvasStorico.Children.Clear();

            const double margin = 10;
            double xmin = margin;
            double xmax = CanvasStorico.Width - margin;
            double ymin = margin;
            double ymax = CanvasStorico.Height - margin;
            const double step = 25;

            // Make the X axis.
            GeometryGroup xaxis_geom = new GeometryGroup();
            xaxis_geom.Children.Add(new LineGeometry(
                new Point(0, ymax), new Point(CanvasStorico.Width, ymax)));
            for (double x = xmin + step;
                x <= CanvasStorico.Width - step; x += step)
            {
                xaxis_geom.Children.Add(new LineGeometry(
                    new Point(x, ymax - margin / 2),
                    new Point(x, ymax + margin / 2)));
            }

            Path xaxis_path = new Path();
            xaxis_path.StrokeThickness = 1;
            xaxis_path.Stroke = Brushes.Black;
            xaxis_path.Data = xaxis_geom;

            CanvasStorico.Children.Add(xaxis_path);

            // Make the Y ayis.
            GeometryGroup yaxis_geom = new GeometryGroup();
            yaxis_geom.Children.Add(new LineGeometry(
                new Point(xmin, 0), new Point(xmin, CanvasStorico.Height)));
            for (double y = step; y <= CanvasStorico.Height - step; y += step)
            {
                yaxis_geom.Children.Add(new LineGeometry(
                    new Point(xmin - margin / 2, y),
                    new Point(xmin + margin / 2, y)));
            }

            Path yaxis_path = new Path();
            yaxis_path.StrokeThickness = 1;
            yaxis_path.Stroke = Brushes.Black;
            yaxis_path.Data = yaxis_geom;

            CanvasStorico.Children.Add(yaxis_path);

            // Make some data sets.
            Brush[] brushes = { Brushes.Red, Brushes.Green, Brushes.Blue };
            Random rand = new Random();
            for (int data_set = 0; data_set < 1; data_set++)
            {
                int last_y = 10;
                int i = 0;

                PointCollection points = new PointCollection();
                for (double x = xmin; x <= xmax; x += step)
                {
                    if (i < 10)
                    {
                        last_y = ((int)ymax) - (conteggio[i]*25) ;
                        if (last_y < ymin) last_y = (int)ymin;
                        if (last_y > ymax) last_y = (int)ymax;
                        points.Add(new Point(x, last_y));
                        i++;
                    }
                    else
                        break;
                }

                Polyline polyline = new Polyline();
                polyline.StrokeThickness = 1;
                polyline.Stroke = brushes[data_set];
                polyline.Points = points;

                CanvasStorico.Children.Add(polyline);
            }

        }

        #endregion

        public class Category
        {
            public float Percentage { get; set; }
            public string Title { get; set; }
            public Brush ColorBrush { get; set; }
        }

        
    }
}
