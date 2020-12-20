using MessagingToolkit.QRCode.Codec;
using Microsoft.Win32;
using Progetto_Palestra.Classi;
using QRCoder;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Progetto_Palestra.Interfacce
{
    /// <summary>
    /// Logica di interazione per ControlloreWindow.xaml
    /// </summary>
    public partial class ControlloreWindow : Window
    {
        //PROPERTIES
        public string Username { get; set; }
        public CControllore Controllore { get; set; }
        private MySQLdatabase db { get; set; }
        public bool Logout { get; set; }

        ////COSTRUTTORE CON PARAMETRO USERNAME
        public ControlloreWindow(string Username)
        {
            InitializeComponent();

            //Imposta oggetto CControllore
            db = new MySQLdatabase();
            this.Username = Username;
            Controllore = db.GetControllore(Username);

            UpdateDashboard(); ////Aggiorna dashboard

            Logout = false;
        }

        #region METODI UPDATE VISUALIZZAZIONE

        ////AGGIORNA VISUALIZZAZIONE DASHBOARD
        public void UpdateDashboard()
        {
            ////RENDE VISIBILE SOLO UNA GRID
            GridDashboard.Visibility = Visibility.Visible;
            GridScanQR.Visibility = Visibility.Hidden;
            GridCreateQR.Visibility = Visibility.Hidden;
            GridProfile.Visibility = Visibility.Hidden;
            GridLogout.Visibility = Visibility.Hidden;

            //VISUALIZZA LISTA ACCESSI
            try
            {
                ////Inserisce dati nella dashboard
                List<CVisita> lista = db.SelectVisite();
                DataGridAccessi.ItemsSource = lista;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ////CALCOLA NUMERO VISITE
            LabelVisiteTot.Content = db.CountVisite();
            LabelVisiteWeek.Content = db.CountVisiteWeek() + " questa settimana";
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

            ////INSERISCE DATI NELLE TEXTBOX
            TB_Nome.Text = Controllore.Nome;
            TB_Cognome.Text = Controllore.Cognome;
            TB_Password.Text = "";
            TB_PasswordC.Text = "";
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

        #endregion

        #region METODI COMPONENTI

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
            { }
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
                int id = db.GetIDAtleta(username);


                //Inserimento nel database
                if (!db.CheckAccesso(id))
                {
                    MessageBox.Show("Accesso Eseguito");
                    db.EseguiAccesso(id);     ////INSERT
                }
                else
                {
                    MessageBox.Show("Uscita eseguita");
                    db.EseguiUscita(id); ////UPDATE
                }
            }
        }

        ////BOTTONE ANNULLA INSERIMENTO, CANCELLA TEXTBOX
        private void BT_Annulla_QR_Click(object sender, RoutedEventArgs e)
        {
            TB_Username_QR.Text = "";
        }

        ////AGGIORNA DASHBOARD
        private void LWDashboard_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UpdateDashboard();
        }

        ////AGGIORNA SCANNER
        private void LVScan_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UpdateScanQR();
        }

        ////AGGIORNA CREA QR
        private void LVCreate_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UpdateCreateQR();
        }

        ////AGGIORNA PROFILO
        private void LVProfilo_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UpdateProfilo();
        }

        ////AGGIORNA LOGOUT
        private void LWLogout_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UpdateLogout();
        }

        ////INSERISCE ACCESSI NELLA DATAGRID
        private void DataGridAccessi_Initialized(object sender, System.EventArgs e)
        {
            try
            {
                ////Inserisce dati nella dashboard
                List<CVisita> lista = db.SelectVisite();
                DataGridAccessi.ItemsSource = lista;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        
        ////CREA CODICE QR E LO INSERISCE NELL'IMMAGINE
        private void BT_Crea_QR_Click(object sender, RoutedEventArgs e)
        {
            QRCodeEncoder encoder = new QRCodeEncoder();
            Bitmap bitmap = encoder.Encode(TB_Codice.Text);
            encoder.QRCodeScale = 8;
            ImageQR_Code.Source = ConvertImage.ToBitmapImage(bitmap);
        }

        ////SALVA IL CODICE QR
        private void BT_Save_QR_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TB_Codice.Text != "")
                {
                    QRCodeEncoder encoder = new QRCodeEncoder();
                    Bitmap bitmap = encoder.Encode(TB_Codice.Text);
                    encoder.QRCodeScale = 8;
                    ImageQR_Code.Source = ConvertImage.ToBitmapImage(bitmap);

                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "png files (*.png)|*.png";
                    sfd.FilterIndex = 1;
                    sfd.RestoreDirectory = true;

                    if (sfd.ShowDialog() == true)
                    {
                        SaveToPng(GridCreateQR, sfd.FileName);
                    }
                }else
                {
                    MessageBox.Show("Prima di salvare creare il codice QR!");
                }

            }
            catch (System.Exception)
            {

            }


        }

        ////METODO PER IL BOTTONE DISCONNETTI
        private void BT_Logout_Click(object sender, RoutedEventArgs e)
        {
            Logout = true;
            this.Close();
        }

        ////METODO PER IL BOTTONE CHIUDI
        private void BT_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        ////AGGIORNA INFORMAZIONI CONTROLLORE
        private void BT_Salva_Modifiche_Click(object sender, RoutedEventArgs e)
        {
            ////AGGIORNA INFO CONTROLLORE
            if (TB_Password.Text != "") //Se cambio password
                Controllore.Password = ConvertMD5(TB_Password.Text);
            if (TB_Nome.Text != "") //Se cambio nome
                Controllore.Nome = TB_Nome.Text;
            if (TB_Cognome.Text != "") //Se cambio cognome
                Controllore.Cognome = TB_Cognome.Text;

            if (TB_Password.Text != TB_PasswordC.Text)
                MessageBox.Show("Le password non corrispondono");
            else
            {
                db.UpdateControllore(Controllore);
                MessageBox.Show("Il profilo è stato aggiornato!");
                UpdateProfilo();
            }

        }

        #endregion
                
        #region METODI PER SALVARE IMMAGINE PNG

        void SaveToPng(FrameworkElement visual, string fileName)
        {
            var encoder = new PngBitmapEncoder();
            SaveUsingEncoder(visual, fileName, encoder);
        }
        
        void SaveUsingEncoder(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            BitmapFrame frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);

            using (var stream = File.Create(fileName))
            {
                encoder.Save(stream);
            }
        }

        #endregion

        #region ALTRI METODI
        
        ////CONVERTE STRINGA IN MD5
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
