using Progetto_Palestra.Classi;
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
        private CAtleta atleta { get; set; }
        private MySQLdatabase db;

        /**
         * @brief Costruttore con parametri
         * @details Inizializza UserWindow e le properties utilizzando il parametro
         */
        public UserWindow(string Username)
        {
            InitializeComponent();

            this.Username = Username;
            db = new MySQLdatabase();

            /* Inizializza atleta */
            atleta = db.GetAtleta(Username);
            
            /* Inizializza la dashboard */
            UpdateDashboard();
        }

        public void UpdateDashboard()
        {
            /* Rende visibile solo la dashboard */
            GridDashboard.Visibility = Visibility.Visible;

            /* Inserisce dati nelle label */

            int numAll = db.GetNumAllenamenti(atleta.ID);
            int numAllW = db.GetNumAllenamentiW(atleta.ID);
            LabelNumAllenamenti.Content = numAll;
            LabelNumAllenamentiW.Content = numAllW + " questa settimana";
        }
    }
}
