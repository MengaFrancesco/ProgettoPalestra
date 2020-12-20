using Progetto_Palestra.Classi;
using System;
using System.Windows;

namespace Progetto_Palestra.Interfacce
{
    ////FINESTRA MECCANICO
    ////VISUALIZZA LE RIPARAZIONI CHE POSSONO ESSERE COMPIUTE DAL MECCANICO
    public partial class MeccanicoWindow : Window
    {
        //Properties pubbliche
        public string Username { get; set; }
        public bool Logout { get; set; }
        public int ID_Attrezzo { get; set; }
        public int ID_Segnalazione { get; set; }

        ////COSTRUTTORE CON PARAMETRO USERNAME
        public MeccanicoWindow(string Username)
        {
            InitializeComponent();
                 
            //Inserisce segnalazioni possibili nella datagrid
            MySQLdatabase db = new MySQLdatabase();
            DG_Segnalazioni.ItemsSource = db.SelectSegnalazioniDisp();

            //Inizializzazioni
            this.Username = Username;
            ButtonTermina.IsEnabled = false;
            LabelBenvenuto.Content += " " + Username;
        }
        
        ////BOTTONE DISCONNETTI
        private void ButtonDisconnetti_Click(object sender, RoutedEventArgs e)
        {
            //Imposta alogout a true per visualizzare MainWindow
            Logout = true;
            this.Close();
        }

        ////INIZIA LA RIPARAZIONE DELL'ATTREZZO
        private void ButtonRipara_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Controllo elemento datagrid selezionato
                if (DG_Segnalazioni.SelectedIndex != -1)
                {
                    //Disattiva / attiva elementi corrispondenti
                    ButtonRipara.IsEnabled = false;
                    ButtonTermina.IsEnabled = true;
                    ButtonDisconnetti.IsEnabled = false;
                    DG_Segnalazioni.IsEnabled = false;

                    //Salva il valore dell'ID attrezzo e segnalazione dell'elemento selezionato
                    ID_Attrezzo = ((CSegnalazione)DG_Segnalazioni.SelectedItem).ID_Attrezzo;
                    ID_Segnalazione = ((CSegnalazione)DG_Segnalazioni.SelectedItem).ID_Segnalazione;

                    //Aggiorna valore riparazione iniziata della segnalazione selezionata
                    MySQLdatabase db = new MySQLdatabase();
                    db.UpdateSegnalazioneIniziata(ID_Segnalazione);

                    //Visualizza esito in output
                    MessageBox.Show("Riparazione iniziata");
                }
                else
                {
                    //Visualizza esito in output
                    MessageBox.Show("Nessuna segnalazione selezionata");

                    //Aggiorna visualizzazione della datagrid
                    MySQLdatabase db = new MySQLdatabase();
                    DG_Segnalazioni.ItemsSource = db.SelectSegnalazioniDisp();
                }
            }
            catch (Exception ex)
            {
                //Visualizza messaggio di errore in output
                MessageBox.Show(ex.Message);
            }
        }

        ////TERMINA LA RIPARAZIONE DELL'ATTREZZO
        private void ButtonTermina_Click(object sender, RoutedEventArgs e)
        {
            //Attiva disattiva componenti corrispondenti
            ButtonRipara.IsEnabled = true;
            ButtonTermina.IsEnabled = false;
            ButtonDisconnetti.IsEnabled = true;
            DG_Segnalazioni.IsEnabled = true;

            //Cambia valore segnalazione
            MySQLdatabase db = new MySQLdatabase();

            //Cambia valore segnalazione terminata e attrezzo manutenzione
            db.UpdateSegnalazioneTerminata(ID_Segnalazione);
            db.UpdateAttrezzoRiparato(ID_Attrezzo);

            //Aggiorna visualizzazione datagrid
            DG_Segnalazioni.ItemsSource = db.SelectSegnalazioniDisp();

            //Visualizza esito in output
            MessageBox.Show("Riparazione terminata");
        }
    }
}
