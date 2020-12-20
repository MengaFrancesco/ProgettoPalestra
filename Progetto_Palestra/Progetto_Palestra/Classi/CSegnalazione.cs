using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_Palestra.Classi
{
    public class CSegnalazione
    {
        ////PROPERTIES
        public int ID_Segnalazione { get; set; }
        public int ID_Atleta { get; set; }
        public int ID_Attrezzo { get; set; }
        public bool RiparazioneIniziata { get; set; }
        public bool RiparazioneTerminata { get; set; }



        ////COSTRUTTORE DI DEFAULT
        public CSegnalazione()
        {
            ID_Segnalazione = 0;
            ID_Atleta = 0;
            ID_Attrezzo = 0;
            RiparazioneIniziata = false;
            RiparazioneTerminata = false;
        }

        ////COSTRUTTORE CON PARAMETRI
        public CSegnalazione(int iD_Segnalazione, int iD_Atleta, int iD_Attrezzo, bool riparazioneIniziata, bool riparazioneTerminata)
        {
            ID_Segnalazione = iD_Segnalazione;
            ID_Atleta = iD_Atleta;
            ID_Attrezzo = iD_Attrezzo;
            RiparazioneIniziata = riparazioneIniziata;
            RiparazioneTerminata = riparazioneTerminata;
        }

        ////COSTRUTTORE CON PARAMETRO QUERY RESULT
        public CSegnalazione(string QueryRes)
        {
            string[] lista = QueryRes.Split(';');
            ID_Segnalazione = Int32.Parse(lista[0]);
            ID_Atleta = Int32.Parse(lista[1]);
            ID_Attrezzo = Int32.Parse(lista[2]);

            if (lista[3] == "False")
                RiparazioneIniziata = false;
            else
                RiparazioneIniziata = true;
            
            if (lista[4] == "False")
               RiparazioneTerminata = false;
            else
               RiparazioneTerminata = true;
        }

    }

}
