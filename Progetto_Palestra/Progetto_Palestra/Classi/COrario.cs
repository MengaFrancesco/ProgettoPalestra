using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_Palestra.Classi
{
    public class COrario
    {
        //Properties
        public int ID { get; set; }
        public string Giorno { get; set; }
        public LocalTime DalleM { get; set; }
        public LocalTime AlleM { get; set; }
        public LocalTime DalleP { get; set; }
        public LocalTime AlleP { get; set; }

        /**
         * @brief Costruttore senza parametri
         */
        public COrario()
        {
            ID = 0;
            Giorno = "";
            DalleM = new LocalTime();
            AlleM = new LocalTime();
            DalleP = new LocalTime();
            AlleP = new LocalTime();
        }

        /**
         * @brief Costruttore con parametri
         */
        public COrario(int ID, string Giorno, LocalTime DalleM, LocalTime AlleM, LocalTime DalleP, LocalTime AlleP)
        {
            this.ID = ID;
            this.Giorno = Giorno;
            this.DalleM = DalleM;
            this.AlleM = AlleM;
            this.DalleP = DalleP;
            this.AlleP = AlleP;
        }
    }
}
