using Progetto_Palestra.Classi;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Progetto_Palestra.Interfacce
{
    /// <summary>
    /// Logica di interazione per RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public string Nome;
        public string Cognome;
        public string Username;
        public string Password;
        public DateTime DataDiNascita;
        public string Sesso;
        public bool Visible1;
        public bool Visible2;
        public bool Registrazione;
        public string Residenza;

        /**
         * @brief Costruttore senza parametri
         * @details Inizializza la finestra di registrazione ed inizializza gli attributi
         */
        public RegistrationWindow()
        {
            InitializeComponent();

            /* Inizializzazione degli attributi */
            Nome = "";
            Cognome = "";
            Username = "";
            Password = "";
            Sesso = "";
            DataDiNascita = new DateTime();
            Sesso = "";
            Visible1 = false;
            Visible2 = false;
            Registrazione = false;
            Residenza = "";
        }

        /**
         * @brief Metodo eseguito con la pressione del bottone registra
         * @details Controlla se i parametri sono corretti e invia la registrazione
         *          al server
         */
        private void ButtonRegistra_Click(object sender, RoutedEventArgs e)
        {
            bool InputOK = true; //Inserimento parametri
            bool PwUguali = true; //Le password corrispondono
            bool PwSuff = false; //Le password sono sufficientemente complesse

            /* Controlla che tutti gli input siano stati inseriti */
            if (TextNome.Text == "")
            {
                MessageBox.Show("Inserire il nome e riprovare", "Attenzione", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                InputOK = false;
            }else if(TextCognome.Text =="")
            {
                MessageBox.Show("Inserire il cognome e riprovare", "Attenzione", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                InputOK = false;
            }else if (TextUsername.Text == "")
            {
                MessageBox.Show("Inserire l'username e riprovare", "Attenzione", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                InputOK = false;
            }else if (TextPassword.Password == "")
            {
                MessageBox.Show("Inserire la password e riprovare", "Attenzione", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                InputOK = false;
            }else if (TextConferma.Password == "")
            {
                MessageBox.Show("Inserire la conferma della password e riprovare", "Attenzione", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                InputOK = false;
            }else if (DataNascita.SelectedDate == null)
            {
                MessageBox.Show("Inserire la data di nascita e riprovare", "Attenzione", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                InputOK = false;
            }else if(RadioM.IsChecked == false && RadioF.IsChecked == false)
            {
                MessageBox.Show("Selezionare il sesso e riprovare", "Attenzione", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                InputOK = false;
            }else if (TextCitta.Text == "")
            {
                MessageBox.Show("Inserire la città e riprovare", "Attenzione", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                InputOK = false;
            }

            if(TextPassword.Password != TextConferma.Password)
            {
                MessageBox.Show("Le password non corrispondono!", "Attenzione", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                PwUguali = false;
            }
            else
            {
                //Controlla se la password è valida
                PasswordCheck pw = new PasswordCheck();
                PwSuff = pw.IsStrongPassword(TextPassword.Password);

                if (!PwSuff) //Visualizza errore se password insufficiente
                    MessageBox.Show("La password non soddisfa i requisiti di sicurezza!", "Attenzione", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            }
            
            if (InputOK && PwUguali && PwSuff)
            {
                Nome = TextNome.Text;
                Cognome = TextCognome.Text;
                Username = TextUsername.Text;
                Password = TextPassword.Password;
                DataDiNascita = DataNascita.SelectedDate.Value;
                Residenza = TextCitta.Text;
                if (RadioM.IsChecked.Value)
                    Sesso = "M";
                else
                    Sesso = "F";
                Registrazione = true; //Registrazione effettuata
                ConvertMD5(TextPassword.Password); //Converte password in MD5
                MessageBox.Show("Registrazione effettuata con successo!", "Attenzione!", MessageBoxButton.OK, MessageBoxImage.Information);

                //Creazione ed inserimento nel database
                CAtleta Atleta = new CAtleta(0,Username,Password,Nome,Cognome,Residenza,DateTime.Today,Sesso,DateTime.Today.AddDays(-1), DataDiNascita);
                MySQLdatabase db = new MySQLdatabase();
                db.InserisciAtleta(Atleta);
                
                this.Close();
            }


        }

        /**
         * @brief Converte il parametro in una stringa MD5
         * @details Utilizza il parametro per calcolare ed impostare il valore dell'
         *          attributo MD5
         */
        private void ConvertMD5(string Password)
        {
            StringBuilder hash = new StringBuilder(); //Crea hash da impostare
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(Password));
            for (int i = 0; i < bytes.Length; i++)
                hash.Append(bytes[i].ToString("x2"));

            //Converte l'hash in stringa
            this.Password = hash.ToString();
        }

        /**
         * @brief Metodo che visualizza o nasconde la password 
         */
        private void ButtonShow1_Click(object sender, RoutedEventArgs e)
        {
            if(Visible1)
            {
                //Se visibile rende nascosto
                TextPassword2.Visibility = Visibility.Hidden;
                TextPassword.Visibility = Visibility.Visible;
            }
            else
            {
                //Se non visibile visualizza
                TextPassword2.Visibility = Visibility.Visible;
                TextPassword.Visibility = Visibility.Hidden;
                TextPassword2.Text = TextPassword.Password;
            }
            Visible1 = !Visible1; //Inverte visibilità
        }

        /**
         * @brief Metodo che visualizza o nasconde la password di conferma
         */
        private void ButtonShow2_Click(object sender, RoutedEventArgs e)
        {
            if (Visible2)
            {
                //Se visibile rende nascosto
                TextConferma2.Visibility = Visibility.Hidden;
                TextConferma.Visibility = Visibility.Visible;
            }
            else
            {
                //Se non visibile visualizza
                TextConferma2.Visibility = Visibility.Visible;
                TextConferma.Visibility = Visibility.Hidden;
                TextConferma2.Text = TextConferma.Password;
            }
            Visible2 = !Visible2; //Inverte visibilità
        }

        private void ButtonAnnulla_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
