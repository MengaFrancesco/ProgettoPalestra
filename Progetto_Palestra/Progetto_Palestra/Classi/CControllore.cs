using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Progetto_Palestra.Classi
{
    public class CControllore
    {
        ////PROPERTIES
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime Iscrizione { get; set; }

        ////COSTRUTTORE CON PARAMETRI
        public CControllore(int ID, string Username, string Password, string Nome, string Cognome, DateTime Iscrizione)
        {
            this.ID = ID;
            this.Username = Username;
            this.Password = Password;
            this.Nome = Nome;
            this.Cognome = Cognome;
            this.Iscrizione = Iscrizione;
        }

        ////COSTRUTTORE CON PARAMETRO QUERY
        public CControllore(string Query)
        {
            try
            {
                string[] lista = Query.Split(';');
                ID = Int32.Parse(lista[0]);
                Username = lista[1];
                Password = lista[2];
                Nome = lista[3];
                Cognome = lista[4];
                Iscrizione = ToDateTime(lista[5]);
            }
            catch(Exception)
            {
            }
        }

        ////RITORNA DATETIME DA STRINGA QUERY PARAMETRO
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

        ////RITORNA STRINGA QUERY INSERT
        public string ToInsertQuery()
        {
            string s = "INSERT INTO controllori (ID_Controllore,Username,Password,Nome,Cognome,Data_Iscrizione) ";
            s += "VALUES (\'NULL\', \'" + Username + "\', \'" + Password + "\', \'" + Nome + "\', \'" + Cognome + "\', \'" + Iscrizione.Year + "-" + Iscrizione.Month + "-" + Iscrizione.Day + "\');";

            return s;
        }

        ////RITORNA STRINGA QUERY UPDATE
        public string ToUpdateQuery()
        {
            string s = "UPDATE controllori ";
            s += "SET Username='" + Username + "\', Password=\'" + Password + "\', Nome=\'" + Nome + "\', Cognome=\'" + Cognome + "\', Data_Iscrizione=\'" + Iscrizione.Year + "-" + Iscrizione.Month + "-" + Iscrizione.Day + "\' ";
            s += "WHERE controllori.ID_Controllore=" + ID + ";";
            return s;
        }
    }
}
