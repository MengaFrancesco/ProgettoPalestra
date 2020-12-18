using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_Palestra.Classi
{
    public class CAllenamento
    {
        ////PROPERTIES
        public int ID_Allenamento { get; set; }
        public DateTime Data { get; set; }
        public LocalTime Ora_Inizio { get; set; }
        public LocalTime OraFine { get; set; }
        public int Atleta { get; set; }

        public CAllenamento()
        {
            ID_Allenamento = 0;
            Data = new DateTime();
            Ora_Inizio = new LocalTime();
            OraFine = new LocalTime() ;
            Atleta = 0;
        }



        ////COSTRUTTORE CON PARAMETRI
        public CAllenamento(int iD_Allenamento, DateTime data, LocalTime ora_Inizio, LocalTime oraFine, int atleta)
        {
            ID_Allenamento = iD_Allenamento;
            Data = data;
            Ora_Inizio = ora_Inizio;
            OraFine = oraFine;
            Atleta = atleta;
        }

        ////COSTRUTTORE PARAMETRO QUERY
        public CAllenamento(string QueryRes)
        {
            string[] lista = QueryRes.Split(';');
            ID_Allenamento = Int32.Parse(lista[0]);
            Data = ToDateTime(lista[1]);
            Ora_Inizio = ToLocalTime(lista[2]);
            OraFine = ToLocalTime(lista[3]);
            Atleta = Int32.Parse(lista[4]);
        }

        ////RITORNA STRINGA QUERY INSERT
        public string ToQueryInsert()
        {
            string res = "";
            res += "INSERT INTO `elenco_allenamenti` (`ID_Allenamento`, `Data`, `Ora_inizio`, `Ora_fine`, `Atleta`) VALUES (NULL, '";
            res += Data.Year + "-" + Data.Month + "-" + Data.Day + "', '";
            res += LocalTimeParse(Ora_Inizio) + "', '";
            res += LocalTimeParse(OraFine)+ "', '";
            res += Atleta + "');";
            return res;
        }

        ////RITORNA STRINGA CON LOCAL TIME PER QUERY
        public string LocalTimeParse(LocalTime time)
        {
            string h = "";
            string m = "";
            string s = "";
            if (time.Hour < 9)
                h = " ";
            if (time.Minute < 9)
                m = " ";
            if (time.Second < 9)
                s = " ";
            h += time.Hour;
            m += time.Minute;
            s += time.Minute;

            return h + ":" + m + ":" + s;
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

        ////Converte DA STRIGNA A LOCALTIME
        private LocalTime ToLocalTime(string QueryRis)
        {
            try
            {
                string[] data = QueryRis.Split(':');
                int h = Int32.Parse(data[0]);
                int m = Int32.Parse(data[1]);
                int s = Int32.Parse(data[2]);
                LocalTime ris = new LocalTime(h, m, s);
                return ris;
            }
            catch (Exception)
            {
                return new LocalTime();
            }
        }
    }
}
