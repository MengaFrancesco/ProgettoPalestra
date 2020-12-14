using MySql.Data.MySqlClient;
using NodaTime;
using Progetto_Palestra.Classi;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Progetto_Palestra
{
    class MySQLdatabase
    {
        private MySqlConnection connection; 

        //Constructor
        public MySQLdatabase()
        {
            Initialize();
        }

        
        /**
         *  @brief Costruttore senza parametri
         *  @details Inizializza il valore degli attributi \c server, \c database, \c uid
         *           e \c password; inoltre inizializza la connessione con il database.
         */
        private void Initialize()
        {
            string connectionString="Server=localhost;Database=db_palestra;Uid=root;Pwd=;"; //Inizializza la stringa di connessione
            connection = new MySqlConnection(connectionString); //Invia la stringa al server
        }

        /**
         * @brief Apre la connessione con il database
         */
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //Visualizza messaggio di errore se la connessione con il server è fallita
                MessageBox.Show(ex.ToString()+", error:" + ex.Number.ToString());
                return false;
            }
        }

        /**
         * @brief Chiude la connessione con il database 
         */
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /**
         * @brief Metodo che riceve dal database la lista degli amministratori
         * @details Utilizza la connessione con il database per ritorare una stringa con username e 
         *          password di tutti gli amministratori
         * @return  lista oggetto \c List contenente la lista di username e password di tutti gli
         *          amministratori
         */
        public List<string> GetAdministrators()
        {
            string query = "SELECT Username,Password FROM amministratori"; //Crea stringa query
            List<string> lista = new List<string>(); //Creazione lista da restituire

            if (this.OpenConnection() == true) //Prova ad aprire la connessione
            {
                MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando
                 
                while (dataReader.Read()) //Read the data and store them in the list
                {
                    lista.Add(dataReader["Username"] + ";" + dataReader["Password"]); //Aggiunge alla lista il record
                }

                dataReader.Close();     //close Data Reader
                this.CloseConnection(); //close Connection
                return lista;            //Ritorna la lista con l'elenco degli amministratori
            }
            else
            {
                return lista; //Ritorna la lista vuota
            }
        }

        /**
         * @brief Metodo che ritorna una lista con username e password
         * @details Esegue la query per ottenere l'elenco di username e password
         */
        public List<string> GetUsPwAtleti()
        {
            string query = "SELECT Username,Password FROM atleti"; //Crea stringa query
            List<string> lista = new List<string>(); //Creazione lista da restituire

            if (this.OpenConnection() == true) //Prova ad aprire la connessione
            {
                MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando

                while (dataReader.Read()) //Read the data and store them in the list
                {
                    lista.Add(dataReader["Username"] + ";" + dataReader["Password"]); //Aggiunge alla lista il record
                }

                dataReader.Close();     //close Data Reader
                this.CloseConnection(); //close Connection
                return lista;            //Ritorna la lista con l'elenco degli amministratori
            }
            else
            {
                return lista; //Ritorna la lista vuota
            }
        }

        /**
         * @brief Aggiunge atleta dato come parametro alla lista
         */
        public void InserisciAtleta(CAtleta Atleta)
        {
            if (this.OpenConnection() == true) //Apre la connessione e se resta aperta continua
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(Atleta.InsertQuery(), connection);
                cmd.ExecuteNonQuery(); //Execute command
                this.CloseConnection(); //close connection
            }
        }

        /**
         * @brief Ritorna stringa con le informazioni dell'amministratore parametro
         */
        public string GetAdmin(string Username)
        {
            string query = "SELECT * FROM amministratori WHERE Username=\""+Username+"\""; //Crea stringa query
            string ris = ""; //Creazione lista da restituire

            if (this.OpenConnection() == true) //Prova ad aprire la connessione
            {
                MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando

                dataReader.Read(); //Read the data and store them in the list
                
                    ris = (dataReader["ID_Amministratore"]+";"+ dataReader["Username"] +";"+ dataReader["Password"] + ";" + dataReader["Nome"] + ";" + dataReader["Cognome"] + ";" + dataReader["Iscrizione"] + ";" + dataReader["Link_foto"]+"");  //Aggiunge alla lista il record
                

                dataReader.Close();     //close Data Reader
                this.CloseConnection(); //close Connection
                return ris;             //Ritorna la lista con l'elenco degli amministratori
            }
            else
            {
                return ris; //Ritorna la lista vuota
            }
        }

        /**
         * @brief Ritorna il numero totale di atleti
         */
        public int GetNumAtleti()
        {
            int ris = 0;

            string query = "SELECT COUNT(*) FROM atleti"; //Crea stringa query
            

            if (this.OpenConnection() == true) //Prova ad aprire la connessione
            {
                MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando

                dataReader.Read(); //Read the data and store them in the list

                string risS = dataReader["COUNT(*)"]+"" ;
                ris = Int32.Parse(risS);

                dataReader.Close();     //close Data Reader
                this.CloseConnection(); //close Connection
                return ris;             //Ritorna la lista con l'elenco degli amministratori
            }
            else
            {
                return ris; //Ritorna la lista vuota
            }
        }

        /**
         * @Ritorna il numero di atleti registrati questa settimana
         */
        public int GetNumAtletiWeek()
        {
            int ris = 0;

            string query = "SELECT COUNT(*) FROM atleti WHERE YEARWEEK(Data_Iscrizione) = YEARWEEK(NOW())"; //Crea stringa query


            if (this.OpenConnection() == true) //Prova ad aprire la connessione
            {
                MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando

                dataReader.Read(); //Read the data and store them in the list

                string risS = dataReader["COUNT(*)"] + "";
                ris = Int32.Parse(risS);

                dataReader.Close();     //close Data Reader
                this.CloseConnection(); //close Connection
                return ris;             //Ritorna la lista con l'elenco degli amministratori
            }
            else
            {
                return ris; //Ritorna la lista vuota
            }
        }

        /**
         * @brief Ritorna il numero di abbonati attivi
         */
        public int GetNumAbbonati()
        {
            int ris = 0;

            string query = "SELECT COUNT(*) FROM atleti WHERE atleti.Scadenza_abbonamento>=NOW()"; //Crea stringa query


            if (this.OpenConnection() == true) //Prova ad aprire la connessione
            {
                MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando

                dataReader.Read(); //Read the data and store them in the list

                string risS = dataReader["COUNT(*)"] + "";
                ris = Int32.Parse(risS);

                dataReader.Close();     //close Data Reader
                this.CloseConnection(); //close Connection
                return ris;             //Ritorna la lista con l'elenco degli amministratori
            }
            else
            {
                return ris; //Ritorna la lista vuota
            }
        }

        /**
         * @brief Ritorna il numero di abbonamenti scaduti
         */
        public int GetNumScaduti()
        {
            int ris = 0;

            string query = "SELECT COUNT(*) FROM atleti WHERE atleti.Scadenza_abbonamento<NOW()"; //Crea stringa query


            if (this.OpenConnection() == true) //Prova ad aprire la connessione
            {
                MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando

                dataReader.Read(); //Read the data and store them in the list

                string risS = dataReader["COUNT(*)"] + "";
                ris = Int32.Parse(risS);

                dataReader.Close();     //close Data Reader
                this.CloseConnection(); //close Connection
                return ris;             //Ritorna la lista con l'elenco degli amministratori
            }
            else
            {
                return ris; //Ritorna la lista vuota
            }
        }

        /**
         * @brief Ritorna il numero di visite totali
         */
        public int GetNumVisite()
        {
            int ris = 0;

            string query = "SELECT COUNT(*) FROM elenco_visite"; //Crea stringa query


            if (this.OpenConnection() == true) //Prova ad aprire la connessione
            {
                MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando

                dataReader.Read(); //Read the data and store them in the list

                string risS = dataReader["COUNT(*)"] + "";
                ris = Int32.Parse(risS);

                dataReader.Close();     //close Data Reader
                this.CloseConnection(); //close Connection
                return ris;             //Ritorna la lista con l'elenco degli amministratori
            }
            else
            {
                return ris; //Ritorna la lista vuota
            }
        }

        public void RimuoviAtleta(int ID)
        {
            try
            {
                string query = "DELETE FROM Atleti WHERE Atleti.ID_Atleta="+ID+";"; //Crea stringa query


                if (this.OpenConnection() == true) //Prova ad aprire la connessione
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                    MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando
                    dataReader.Close();     //close Data Reader
                    this.CloseConnection(); //close Connection
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossibile rimuovere atleta, errore:" + ex.Message);
            }
        }

        /**
         * @brief Ritorna il numero di visite di questa settimana
         */
        public int GetNumVisiteWeek()
        {
            int ris = 0;

            string query = "SELECT COUNT(*) FROM elenco_visite WHERE YEARWEEK(elenco_visite.Data) = YEARWEEK(NOW())"; //Crea stringa query


            if (this.OpenConnection() == true) //Prova ad aprire la connessione
            {
                MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando

                dataReader.Read(); //Read the data and store them in the list

                string risS = dataReader["COUNT(*)"] + "";
                ris = Int32.Parse(risS);

                dataReader.Close();     //close Data Reader
                this.CloseConnection(); //close Connection
                return ris;             //Ritorna la lista con l'elenco degli amministratori
            }
            else
            {
                return ris; //Ritorna la lista vuota
            }
        }

        public List<CAtleta> GetInfoAtleti()
        {
            string query = "SELECT * FROM atleti"; //Crea stringa query
            List<CAtleta> lista = new List<CAtleta>(); //Creazione lista da restituire

            if (this.OpenConnection() == true) //Prova ad aprire la connessione
            {
                MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando

                while (dataReader.Read()) //Read the data and store them in the list
                {
                    string queryRes = "";
                    int numC = dataReader.FieldCount;
                    for (int i = 0; i < numC; i++)
                    {
                        queryRes += dataReader[i] + ";";
                    }
                    lista.Add(new CAtleta(queryRes)); //Aggiunge alla lista il record
                }

                dataReader.Close();     //close Data Reader
                this.CloseConnection(); //close Connection
                return lista;            //Ritorna la lista con l'elenco degli amministratori
            }
            else
            {
                return lista; //Ritorna la lista vuota
            }
        }

        /**
         * @brief Metodo che aggiorna un atleta dato come parametro
         */
        public void AggiornaAtleta(CAtleta atleta)
        {
            try
            {
                string query = "UPDATE Atleti "; //Crea stringa query       
                query += "SET Username=\'" + atleta.Username + "\'";
                query += ", Password=\'" + atleta.Password + "\'";
                query += ", Nome=\'" + atleta.Nome + "\'";
                query += ", Cognome=\'" + atleta.Cognome + "\'";
                query += ", Residenza=\'" + atleta.Residenza + "\'";
                query += ", Data_Iscrizione=\'" + atleta.DataIscrizione.Year + "-" + atleta.DataIscrizione.Month + "-" + atleta.DataIscrizione.Day + "\'";
                query += ", Data_Nascita=\'" + atleta.DataNascita.Year + "-" + atleta.DataNascita.Month + "-" + atleta.DataNascita.Day + "\'";
                query += ", Scadenza_Abbonamento=\'" + atleta.DataScadenza.Year + "-" + atleta.DataScadenza.Month + "-" + atleta.DataScadenza.Day + "\'";
                query += ", Sesso=\'" + atleta.Sesso + "\'";
                query += " WHERE ID_Atleta=" + atleta.ID + ";";

                if (this.OpenConnection() == true) //Prova ad aprire la connessione
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                    MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando
                    dataReader.Close();     //close Data Reader
                    this.CloseConnection(); //close Connection
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Impossibile aggiornare il record");
            }
        }

        public List<COrario> GetOrario()
        {
            try
            {
                List<COrario> orario = new List<COrario>();

                string query = "SELECT * FROM orari";

                if (this.OpenConnection() == true) //Prova ad aprire la connessione
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                    MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando
                    
                    while(dataReader.Read())
                    {
                        COrario row = new COrario();
                        row.ID = Int32.Parse(dataReader["ID"] + "");
                        row.Giorno = dataReader["Giorno"] + "";
                        

                        //Converte stringhe in localtime
                        string complex = dataReader["DalleM"] + "";
                        int ora = Int32.Parse(complex.Substring(0, 2));
                        int minuti = Int32.Parse(complex.Substring(3, 2));
                        row.DalleM = new LocalTime(ora, minuti);

                        complex = dataReader["AlleM"] + "";
                        ora = Int32.Parse(complex.Substring(0, 2));
                        minuti = Int32.Parse(complex.Substring(3, 2));
                        row.AlleM = new LocalTime(ora, minuti);

                        complex = dataReader["DalleP"] + "";
                        ora = Int32.Parse(complex.Substring(0, 2));
                        minuti = Int32.Parse(complex.Substring(3, 2));
                        row.DalleP = new LocalTime(ora, minuti);

                        complex = dataReader["AlleP"] + "";
                        ora = Int32.Parse(complex.Substring(0, 2));
                        minuti = Int32.Parse(complex.Substring(3, 2));
                        row.AlleP = new LocalTime(ora, minuti);

                        orario.Add(row);
                    }
                    
                    dataReader.Close();     //close Data Reader
                    this.CloseConnection(); //close Connection
                }


                return orario;
            }
            catch (Exception)
            {
                MessageBox.Show("Impossibile caricare orario.");
                return new List<COrario>();
            }
        }

        public void AggiornaOrario(string giorno,DateTime orario1M, DateTime orario2M, DateTime orario1P, DateTime orario2P)
        {
            try
            {
                

                string query = "UPDATE orari "; //Crea stringa query       
                query += "SET DalleM=\'" + OrarioToQuery(orario1M) + "\'";
                query += ", AlleM=\'" + OrarioToQuery(orario2M) + "\'";
                query += ", DalleP=\'" + OrarioToQuery(orario1P) + "\'";
                query += ", AlleP=\'" + OrarioToQuery(orario2P) + "\'";
                query += " WHERE Giorno=\"" + giorno+ "\";";

                if (this.OpenConnection() == true) //Prova ad aprire la connessione
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                    MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando
                    dataReader.Close();     //close Data Reader
                    this.CloseConnection(); //close Connection
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossibile aggiornare l'orario, errore:"+ex.Message);
            }
            

        }

        private string OrarioToQuery(DateTime orario)
        {
            string s = "";
            if (orario.Hour < 10)
                s += "0";
            s += orario.Hour + ":";
            if (orario.Minute < 10)
                s += "0";
            s += orario.Minute + ":00.000000";
            return s;
        }
    }
}


