using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Progetto_Palestra
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /**
         * @brief Costruttore senza parametri 
         * @details Inizializza la classe \c MainWindow
         */
        public MainWindow()
        {
            InitializeComponent(); //Inizializza componenti 
        }

        /**
         * @brief   Metodo eseguito quando si preme il bottone "Esci"
         * @details Chiude la finestra principale
         */
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            //Se la risposta del messaggio è "Yes"
            if(MessageBox.Show("Sicuri di voler abbandonare la sessione?","Attenzione!",MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                this.Close();
            }
            
        }

        /**
         * @brief   Metodo eseguito quando si preme il bottone "Accedi"
         * @details Apre la finestra di login per eseguire l'accesso
         */
        private void ButtonAccedi_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden; //Nasconde finestra principale

            LoginWindow lw = new LoginWindow();
            lw.ShowDialog();

            if(lw.Login)
            {
                MessageBox.Show(lw.Tipologia);
                switch(lw.Tipologia)
                {
                    case "Amministratore":
                        AdministratorWindow aw = new AdministratorWindow(lw.Username);
                        aw.ShowDialog();
                        break;
                    case "Atleta":
                        UserWindow uw = new UserWindow(lw.Username);
                        uw.ShowDialog();
                        break;
                    case "Controllore":
                        ControlloreWindow cw = new ControlloreWindow(lw.Username);
                        cw.ShowDialog();
                        break;
                    case "Meccanico":
                        MeccanicoWindow mw = new MeccanicoWindow(lw.Username);
                        mw.ShowDialog();
                        break;
                        break;
                }
                
                


            }
            //L'esecuzione ritorna alla chiusura della finestra
            this.Visibility = Visibility.Visible; //Visualizza finestra principale
        
        
        }

        /**
         * @brief Metodo eseguito quando si preme il bottone "Registrati"
         * @details Apre la finestra di registrazione per effettuare la registrazione
         */
        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            RegistrationWindow rw = new RegistrationWindow();
            rw.ShowDialog();

            //L'esecuzione ritorna alla chiusura della finestra
            this.Visibility = Visibility.Visible; //Visualizza finestra principale

        }
    }
}
