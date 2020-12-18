using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logica di interazione per PagamentoWindow.xaml
    /// </summary>
    public partial class PagamentoWindow : Window
    {
        public bool Result { get; set; }
        public string Durata { get; set; }

        public PagamentoWindow()
        {
            InitializeComponent();
        }

        ////METODO VERIFICA CARTA DI CREDITO
        public bool IsCreditCardInfoValid(string cardNo, string expiryDate, string cvv)
        {
            if (cardNo.Length == 16 && expiryDate.Length == 5 && cvv.Length == 3)
                return true;
            else
                return false;
               
        
        }


        //BOTTONE RINNOVA ABBONAMENTO
        private void BT_Rinnova_Click(object sender, RoutedEventArgs e)
        {
            //SE IMPORTO SELEZIONATO
            string selected = "";
            string carta = TB_Carta.Text;
            string scadenza = TB_ScadenzaM.Text+"/"+TB_ScadenzaA.Text;
            string cvv = TB_CVV.Text;

            ////CONTROLLO RADIOBUTTON SELEZIONATO
            if (RB_1mese.IsChecked == true)
            {
                selected = "1mese";
            }
            else if (RB_3mesi.IsChecked == true)
            {
                selected = "3mese";
            }
            else if (RB_12mesi.IsChecked == true)
            {
                selected = "12mese";
            }

            //CONTROLLO SE RADIOBUTTON SELEZIONATO
            if (selected == "")
            {
                MessageBox.Show("Durata rinnovo non selezionata");
            }
            else if (!IsCreditCardInfoValid(carta, scadenza, cvv))
            {
                MessageBox.Show("Dati carta non validi");
            }
            else
            {
                MessageBox.Show("Rinnovo effettuato");
                Result = true;
                Durata = selected;
                this.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Result = false;
            this.Close();
        }
    }
}
