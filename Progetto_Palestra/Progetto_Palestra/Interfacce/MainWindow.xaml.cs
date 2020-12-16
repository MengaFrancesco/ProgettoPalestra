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
            //Se la risposta del messaggio è "Yes"
            if (MessageBox.Show("Sicuri di voler abbandonare la sessione?", "Attenzione!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        ////BOTTONE ACCEDI
        private void ButtonAccedi_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden; //Nasconde finestra principale

            LoginWindow lw = new LoginWindow();
            lw.ShowDialog();

            if(lw.Login)
            {
                MySQLdatabase db = new MySQLdatabase();
                switch (lw.Tipologia)
                {
                    case "Amministratore":
                        
                        AdministratorWindow aw = new AdministratorWindow(ToAmministratore(db.GetAdmin(lw.Username)));
                        aw.ShowDialog();
                        if (!aw.Logout) this.Close();
                        else this.Visibility = Visibility.Visible; //Visualizza finestra principale
                        break;
                    case "Atleta":
                        UserWindow uw = new UserWindow(lw.Username);
                        uw.ShowDialog();
                        this.Visibility = Visibility.Visible; //Visualizza finestra principale
                        break;
                    case "Controllore":
                        ControlloreWindow cw = new ControlloreWindow(lw.Username);
                        cw.ShowDialog();
                        if (!cw.Logout) this.Close();
                        else
                        this.Visibility = Visibility.Visible; //Visualizza finestra principale
                        break;
                    case "Meccanico":
                        MeccanicoWindow mw = new MeccanicoWindow(lw.Username);
                        mw.ShowDialog();
                        this.Visibility = Visibility.Visible; //Visualizza finestra principale
                        break;
                }
                
                


            }
            
        
        
        }

        ////BOTTONE REGISTRA
        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            RegistrationWindow rw = new RegistrationWindow();
            rw.ShowDialog();

            //L'esecuzione ritorna alla chiusura della finestra
            this.Visibility = Visibility.Visible; //Visualizza finestra principale

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
            int anno = Int32.Parse(data.ElementAt(2).Substring(0,4));
            DateTime isc = new DateTime(anno, mese, giorno);
            string link = list.ElementAt(6);


            CAmministratore admin = new CAmministratore(id, username, password, nome, cognome, isc, link);
            return admin;
        }
    }
}
