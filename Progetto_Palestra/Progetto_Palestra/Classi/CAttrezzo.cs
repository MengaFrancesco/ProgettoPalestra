using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_Palestra.Classi
{
    public class CAttrezzo
    {
        public int ID_Attrezzo { get; set; }
        public string Nome { get; set; }
        public string Tipologia { get; set; }
        public string Modello { get; set; }
        public bool Utilizzato { get; set; }
        public bool ManutenzioneRichiesta { get; set; }
        public string Link { get; set; }

        public CAttrezzo(int iD_Attrezzo, string nome, string tipologia, string modello, bool utilizzato, bool manutenzioneRichiesta , string link)
        {
            ID_Attrezzo = iD_Attrezzo;
            Nome = nome;
            Tipologia = tipologia;
            Modello = modello;
            Utilizzato = utilizzato;
            ManutenzioneRichiesta = manutenzioneRichiesta;
            Link = link;
        }

        public CAttrezzo(string QueryRes)
        {
            string[] list = QueryRes.Split(';');
            ID_Attrezzo = Int32.Parse(list[0]);
            Nome = list[1];
            Tipologia = list[2];
            Modello = list[3];
            if (list[4] == "True")
                Utilizzato = true;
            else
                Utilizzato = false;
            if (list[5] == "True")
                ManutenzioneRichiesta = true;
            else
                ManutenzioneRichiesta = false;
            Link = list[6];
        }
    }
}
