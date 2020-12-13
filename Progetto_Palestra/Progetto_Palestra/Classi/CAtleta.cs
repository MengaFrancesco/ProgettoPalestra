using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_Palestra.Classi
{
    class CAtleta
    {
        /* PROPERTIES DI ATLETA */
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Residenza { get; set; }
        public DateTime DataIscrizione { get; set; }
        public string Sesso { get; set; }
        public DateTime DataScadenza { get; set; }
        public DateTime DataNascita { get; set; }

        /**
         * @brief Costruttore con parametri
         * @details Inizializza tutte le properties utilizzando i parametri
         */
        public CAtleta(string Username, string Password, string Nome, string Cognome, string Residenza, DateTime DataIscrizione, string Sesso, DateTime DataScadenza, DateTime DataNascita) 
        {
            this.Username = Username;
            this.Password = Password;
            this.Nome = Nome;
            this.Cognome = Cognome;
            this.Residenza = Residenza;
            this.DataIscrizione = DataIscrizione;
            this.Sesso = Sesso;
            this.DataScadenza = DataScadenza;
            this.DataNascita = DataNascita;
        }

        /**
         * @brief Metodo che converte tutte le properties nella QUERY di inserimento
         */
        public string InsertQuery()
        {
            string s = "INSERT INTO `atleti` (`ID_Atleta`, `Username`, `Password`, `Nome`, `Cognome`, `Residenza`, `Data_Iscrizione`, `Data_nascita`, `Sesso`, `Scandenza_abbonamento`)"
                + " VALUES(NULL, '"+Username+"', '"+Password+"', '"+Nome+ "', '" + Cognome + "', '" + Residenza + "', '"+DataIscrizione.Year+ "-" + DataIscrizione.Month + "-" + DataIscrizione.Day + "', '" + DataNascita.Year + "-" + DataNascita.Month + "-" + DataNascita.Day + "', '" + Sesso + "', '" + DataScadenza.Year + "-" + DataScadenza.Month + "-" + DataScadenza.Day + "')";

            return s;
        }


    }
}
