using System.Windows;

namespace Progetto_Palestra.Interfacce
{
    /// <summary>
    /// Logica di interazione per UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        //Properties
        public string Username { get; set; }

        /**
         * @brief Costruttore con parametri
         * @details Inizializza UserWindow e le properties utilizzando il parametro
         */
        public UserWindow(string Username)
        {
            InitializeComponent();

            this.Username = Username;
            MessageBox.Show(Username);
        }
    }
}
