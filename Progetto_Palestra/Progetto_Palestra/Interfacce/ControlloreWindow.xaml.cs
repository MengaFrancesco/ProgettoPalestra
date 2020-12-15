using Progetto_Palestra.Classi;
using System.Windows;

namespace Progetto_Palestra.Interfacce
{
    /// <summary>
    /// Logica di interazione per ControlloreWindow.xaml
    /// </summary>
    public partial class ControlloreWindow : Window
    {
        //Properties
        public string Username { get; set; }
        public CControllore Controllore { get; set; }
        private MySQLdatabase db { get; set; }

        ////Costruttore con parametri
        public ControlloreWindow(string Username)
        {
            InitializeComponent();
            
            //Imposta oggetto CControllore
            db = new MySQLdatabase();
            this.Username = Username;
            Controllore = db.GetControllore(Username);

            UpdateDashboard(); ////Aggiorna dashboard
        }

        ////AGGIORNA VISUALIZZAZIONE DASHBOARD
        public void UpdateDashboard()
        {
            ////RENDE VISIBILE SOLO UNA GRID
            GridDashboard.Visibility = Visibility.Visible;
            GridScanQR.Visibility = Visibility.Visible;
            GridCreateQR.Visibility = Visibility.Visible;
            GridProfile.Visibility = Visibility.Visible;
            GridLogout.Visibility = Visibility.Visible;
        }

        ////AGGIORNA VISUALIZZAZIONE SCAN QR
        public void UpdateScanQR()
        {
            ////RENDE VISIBILE SOLO UNA GRID
            GridDashboard.Visibility = Visibility.Hidden;
            GridScanQR.Visibility = Visibility.Visible;
            GridCreateQR.Visibility = Visibility.Hidden;
            GridProfile.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Hidden;
        }

        ////AGGIORNA VISUALIZZAZIONE CREATE QR
        public void UpdateCreateQR()
        {
            ////RENDE VISIBILE SOLO UNA GRID
            GridDashboard.Visibility = Visibility.Hidden;
            GridScanQR.Visibility = Visibility.Hidden;
            GridCreateQR.Visibility = Visibility.Visible;
            GridProfile.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Hidden;
        }

        ////AGGIORNA VISUALIZZAZIONE PROFILO
        public void UpdateProfilo()
        {
            ////RENDE VISIBILE SOLO UNA GRID
            GridDashboard.Visibility = Visibility.Hidden;
            GridScanQR.Visibility = Visibility.Hidden;
            GridCreateQR.Visibility = Visibility.Hidden;
            GridProfile.Visibility = Visibility.Visible;
            GridLogout.Visibility = Visibility.Hidden;
        }

        ////AGGIORNA VISUALIZZAZIONE LOGOUT
        public void UpdateLogout()
        {
            ////RENDE VISIBILE SOLO UNA GRID
            GridDashboard.Visibility = Visibility.Hidden;
            GridScanQR.Visibility = Visibility.Hidden;
            GridCreateQR.Visibility = Visibility.Hidden;
            GridProfile.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Visible;
        }

        ////BOTTONE CHE APRE LA FINESTRA SCANNER
        private void BT_Scansiona_Click(object sender, RoutedEventArgs e)
        {
            string username = "";
            try
            {
                QR_Reader.QR_Reader qr = new QR_Reader.QR_Reader();
                qr.ShowDialog();
                username = qr.QR_code;
            }
            catch (System.Exception)
            {  }
            TB_Username_QR.Text = username; ////IMPOSTA USERNAME
        }

        ////BOTTONE CHE INSERISCE L'ACCESSO NEL DATABASE
        private void BT_Accedi_Click(object sender, RoutedEventArgs e)
        {
            string username = TB_Username_QR.Text;

            ////Controllo textbox vuota
            if (username == "")
                MessageBox.Show("Nessun QR code scansionato!");
            ////Controllo esistenza username
            else if (!db.CheckUsernameAtleta(username))
                MessageBox.Show("Username inesistente!");
            else
            {
                ////Inserimento nel database
                //TODO
            }
        }

        ////BOTTONE ANNULLA INSERIMENTO, CANCELLA TEXTBOX
        private void BT_Annulla_QR_Click(object sender, RoutedEventArgs e)
        {
            TB_Username_QR.Text = "";
        }
    }
}
