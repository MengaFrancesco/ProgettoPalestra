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
using System.Windows.Shapes;

namespace Progetto_Palestra
{
    /// <summary>
    /// Logica di interazione per AdministratorWindow.xaml
    /// </summary>
    public partial class AdministratorWindow : Window
    {
        //Properties
        public string Username { get; set; }

        /**
         * @brief Costruttore con parametri
         * @details Inizializza AdministratorWindow e le properties utilizzando il parametro
         */
        public AdministratorWindow(string Username)
        {
            InitializeComponent();

            this.Username = Username;
        }
    }
}
