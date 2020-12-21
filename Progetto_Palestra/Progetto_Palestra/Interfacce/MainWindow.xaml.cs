using Progetto_Palestra.Classi;
using Progetto_Palestra.QR_Reader;
using System;
using System.Linq;
using System.Windows;

namespace Progetto_Palestra.Interfacce
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ////COSTRUTTORE SENZA PARAMETRI
        public MainWindow()
        {
            InitializeComponent(); //Inizializza componenti 
        }

        ////BOTTONE ESCI
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            //Se il risultato è affermativo chiude la finestra principale
            if (MessageBox.Show("Sicuri di voler abbandonare la sessione?", "Attenzione!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        ////BOTTONE ACCEDI
        private void ButtonAccedi_Click(object sender, RoutedEventArgs e)
        {
            //Rende nascosta la finestra principale
            this.Visibility = Visibility.Hidden; 

            //Visualizza finestra login come dialogo
            LoginWindow lw = new LoginWindow();
            lw.ShowDialog();

            //Controllo login effettuato
            if (lw.Login)
            {
                MySQLdatabase db = new MySQLdatabase();
                switch (lw.Tipologia)
                {
                    case "Amministratore":
                        //Visualizza finestra amministratore
                        AdministratorWindow aw = new AdministratorWindow(ToAmministratore(db.GetAdmin(lw.Username)));
                        aw.ShowDialog();

                        //Controllo logout
                        if (!aw.Logout) this.Close();
                        else this.Visibility = Visibility.Visible; //Visualizza finestra principale
                        break;
                    case "Atleta":
                        //Visualizza finestra atleta
                        UserWindow uw = new UserWindow(lw.Username);
                        uw.ShowDialog();

                        //Controllo logout
                        if (!uw.Logout) this.Close();
                        else this.Visibility = Visibility.Visible; //Visualizza finestra principale
                        break;
                    case "Controllore":
                        //Visualizza finestra controllore
                        ControlloreWindow cw = new ControlloreWindow(lw.Username);
                        cw.ShowDialog();

                        //Controllo logout
                        if (!cw.Logout) this.Close();
                        else this.Visibility = Visibility.Visible; //Visualizza finestra principale
                        break;
                    case "Meccanico":
                        //Visualizza finestra meccanico
                        MeccanicoWindow mw = new MeccanicoWindow(lw.Username);
                        mw.ShowDialog();

                        //Controllo logout
                        if (!mw.Logout) this.Close();
                        else this.Visibility = Visibility.Visible; //Visualizza finestra principale
                        break;
                }
            }
            else
                this.Visibility = Visibility.Visible;
        }

        ////BOTTONE REGISTRA
        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            //Rende la finestra principale invisibile
            this.Visibility = Visibility.Hidden;

            //Visualizza la finestra di registrazione come dialogo
            RegistrationWindow rw = new RegistrationWindow();
            rw.ShowDialog();

            //Rende nuovamente visibile la finestra principale
            this.Visibility = Visibility.Visible;
        }

        ////BOTTONE CONVERTE IN AMMINISTRATORE
        private CAmministratore ToAmministratore(string s)
        {
            string[] list = s.Split(';');
            int id = Int32.Parse(list.ElementAt(0));
            string username = list.ElementAt(1);
            string password = list.ElementAt(2);
            string nome = list.ElementAt(3);
            string cognome = list.ElementAt(4);
            string[] data = list.ElementAt(5).Split('/');
            int giorno = Int32.Parse(data.ElementAt(0));
            int mese = Int32.Parse(data.ElementAt(1));
            int anno = Int32.Parse(data.ElementAt(2).Substring(0, 4));
            DateTime isc = new DateTime(anno, mese, giorno);
            string link = list.ElementAt(6);


            CAmministratore admin = new CAmministratore(id, username, password, nome, cognome, isc, link);
            return admin;
        }
    }
}
