using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_Palestra.Classi
{
    public class CAmministratore
    {
        /* PROPERTIES */
        public int ID_Amministratore { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime Iscrizione { get; set; }
        public string Link_foto { get; set; }


        /**
         * @brief Costruttore con parametri
         * @details Inizializza tutte le properties utilizzando i parametri
         */
        public CAmministratore(int ID_Amministratore, string Username, string Password, string Nome, string Cognome, DateTime Iscrizione, string Link_foto)
        {
            this.ID_Amministratore = ID_Amministratore;
            this.Username = Username;
            this.Password = Password;
            this.Nome = Nome;
            this.Cognome = Cognome;
            this.Iscrizione = Iscrizione;
            this.Link_foto = Link_foto;
        }

        public override string ToString()
        {
            string s = "";
            s = ID_Amministratore + "," + Username + "," + Password + "," + Nome + "," + Cognome + "," + Iscrizione.ToString() + "," + Link_foto;
            return s;
        }

        public CAmministratore GetThis()
        {
            return this;
        }
    }
}
