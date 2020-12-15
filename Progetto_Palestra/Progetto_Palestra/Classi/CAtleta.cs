using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Progetto_Palestra.Classi
{
    public class CAtleta
    {
        ////PROPERTIES
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Residenza { get; set; }
        public DateTime DataIscrizione { get; set; }
        public string Sesso { get; set; }
        public DateTime DataScadenza { get; set; }
        public DateTime DataNascita { get; set; }

        ////COSTRUTTORE CON PARAMETRO QUERY
        public CAtleta(string QueryResult)
        {
            string[] list= QueryResult.Split(';');
            if(list.Length==11)
            {
                ID = Int32.Parse(list[0]);
                Username = list[1];
                Password = list[2];
                Nome = list[3];
                Cognome = list[4];
                Residenza = list[5];
                Sesso = list[8];

                //Converte in datetime
                DataIscrizione = ToDateTime(list[6]);
                DataNascita = ToDateTime(list[7]);
                DataScadenza = ToDateTime(list[9]);
            }
            else
            {
                MessageBox.Show("error");
            }
        }

        ////COSTRUTTORE CON PARAMETRI
        public CAtleta(int ID ,string Username, string Password, string Nome, string Cognome, string Residenza, DateTime DataIscrizione, string Sesso, DateTime DataScadenza, DateTime DataNascita) 
        {
            this.ID = ID;
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

        ////RITORNA UNA STRINGA IN FORMATO QUERY DELL'OGGETTO
        public string ToInsertQuery()
        {
            string s = "INSERT INTO `atleti` (`ID_Atleta`, `Username`, `Password`, `Nome`, `Cognome`, `Residenza`, `Data_Iscrizione`, `Data_nascita`, `Sesso`, `Scadenza_abbonamento`)"
                + " VALUES(NULL, '"+Username+"', '"+Password+"', '"+Nome+ "', '" + Cognome + "', '" + Residenza + "', '"+DataIscrizione.Year+ "-" + DataIscrizione.Month + "-" + DataIscrizione.Day + "', '" + DataNascita.Year + "-" + DataNascita.Month + "-" + DataNascita.Day + "', '" + Sesso + "', '" + DataScadenza.Year + "-" + DataScadenza.Month + "-" + DataScadenza.Day + "')";

            return s;
        }

        ////CONVERTE PARAMETRO STRINGA QUERY IN OGGETTO DATETIME
        public DateTime ToDateTime(string QueryRis)
        {
            try
            {
                string[] data = QueryRis.Split('/');
                int giorno = Int32.Parse(data[0]);
                int mese = Int32.Parse(data[1]);
                int anno = Int32.Parse(data[2].Substring(0, 4));
                DateTime ris = new DateTime(anno, mese, giorno);
                return ris;
            }
            catch (Exception)
            {
                return new DateTime();
            }
        }
    }
}
