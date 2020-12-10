using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Logica di interazione per LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Tipologia { get; set; }
        public bool Login { get; set; }

        /**
         * @brief Costruttore senza parametri
         * @details Inizializza le properties e la combobox
         */
        public LoginWindow()
        {
            InitializeComponent();

            //Inserimento stringhe nella combobox
            List<string> tipi = new List<string>();
            tipi.Add("Amministratore");
            tipi.Add("Atleta");
            tipi.Add("Controllore");
            tipi.Add("Meccanico");
            ComboTipo.ItemsSource = tipi;
            ComboTipo.SelectedIndex = 1;

            //Inizializza properties
            Username = "";
            Password = "";
            Tipologia = "";
            Login = false;
        }

        /**
         * @brief Metodo eseguito con la pressione del tasto esci
         */
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Se la risposta del messaggio è "Yes"
            if (MessageBox.Show("Sicuri di voler abbandonare la finestra di login?", "Attenzione!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void ButtonAccedi_Click(object sender, RoutedEventArgs e)
        {
            //Recupero dei valori inseriti/selezionati
            Username = TextUsername.Text;
            Password = TextPassword.Password;
            Tipologia = (string)ComboTipo.SelectedItem;

            //Controllo input
            if (Username == "")
                MessageBox.Show("Nessun username inserito!", "Attenzione!", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if(Password == "")
                MessageBox.Show("Nessuna password inserita!", "Attenzione!", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                //Se login corretto chiude
                Password = ConvertMD5(Password);
                MySQLdatabase db = new MySQLdatabase();

                switch (Tipologia) //Apre la finestra corrispondente alla tipologia
                {
                    case "Amministratore":
                        List<string> amministratori = db.GetAdministrators();
                        for (int i = 0; i < amministratori.Count; i++) //Per ogni atleta
                        {
                            string[] array = amministratori.ElementAt(i).Split(';');
                            if (array[0] == Username && array[1] == Password)
                            {
                                MessageBox.Show("Login effettuato con successo!", "Attenzione!", MessageBoxButton.OK, MessageBoxImage.Information);
                                Login = true;
                                this.Close();
                                break;
                            }
                        }

                        if (!Login) //Se il login non è stato effettuato
                            MessageBox.Show("Username e password non corrispondono!", "Attenzione!", MessageBoxButton.OK, MessageBoxImage.Warning);

                        break;
                    case "Atleta":
                        List<string> atleti = db.GetAtleti();
                        for (int i = 0; i < atleti.Count; i++) //Per ogni atleta
                        {
                            string[] array = atleti.ElementAt(i).Split(';');
                            if (array[0] == Username && array[1] == Password)
                            {
                                MessageBox.Show("Login effettuato con successo!", "Attenzione!", MessageBoxButton.OK, MessageBoxImage.Information);
                                Login = true;
                                this.Close();
                                break;
                            }
                        }

                        if (!Login) //Se il login non è stato effettuato
                            MessageBox.Show("Username e password non corrispondono!", "Attenzione!", MessageBoxButton.OK, MessageBoxImage.Warning);

                        break;
                    case "Controllore": break;
                    case "Meccanico": break;
                }
                
            }
            
        }

        /**
         * @brief Converte il parametro in una stringa MD5
         * @details Utilizza il parametro per calcolare ed impostare il valore dell'
         *          attributo MD5
         */
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
    }
}
