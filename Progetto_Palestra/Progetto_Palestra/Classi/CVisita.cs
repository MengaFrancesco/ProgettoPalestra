using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Progetto_Palestra.Classi
{
    public class CVisita
    {
        ////PROPERTIES
        public int ID_Visita { get; set; }
        public int ID_Atleta { get; set; }
        public DateTime Data { get; set; }
        public DateTime Orario { get; set; }
        public bool Uscita { get; set; }

        ////COSTRUTTORE CON PARAMETRI
        public CVisita(int iD_Visita, int iD_Atleta, DateTime dataIngresso, DateTime orario, bool uscita)
        {
            ID_Visita = iD_Visita;
            ID_Atleta = iD_Atleta;
            Data= dataIngresso;
            Orario = orario;
            Uscita = uscita;
        }

        ////COSTRUTTORE CON PARAMETRO QUERYRESULT
        public CVisita(string queryRes)
        {
            string[] list = queryRes.Split(';');
            ID_Visita = Int32.Parse(list[0]);
            ID_Atleta = Int32.Parse(list[1]);
            Data = ToDateTime(list[2]);
            Orario = ToTime(list[3]);
            if (list[4] == "True")
                Uscita = true;
            else
                Uscita = false;
        }

        ////CONVERTE ORARIO IN DATETIME
        private DateTime ToTime(string QueryRis)
        {
            try
            {
                string[] data = QueryRis.Split(':');
                int h = Int32.Parse(data[0]);
                int m = Int32.Parse(data[1]);
                int s = Int32.Parse(data[2]);
                DateTime ris = new DateTime(1, 1, 1, h, m, s);
                return ris;
            }
            catch (Exception)
            {
                return new DateTime();
            }
        }

        ////CONVERTE DA STRINGA A DATETIME
        private DateTime ToDateTime(string QueryRis)
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
