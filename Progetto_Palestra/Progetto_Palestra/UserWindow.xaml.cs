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
        }
    }
}
