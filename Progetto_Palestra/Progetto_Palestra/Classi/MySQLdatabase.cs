﻿using MySql.Data.MySqlClient;
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
         * @brief Metodo per inserire record nel database 
         * @param[in] queryInsert stringa contenente la query di inserimento
         */
        public void Insert(string queryInsert)
        {
            //string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";
             
            if (this.OpenConnection() == true) //Apre la connessione e se resta aperta continua
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(queryInsert, connection);
                cmd.ExecuteNonQuery(); //Execute command
                this.CloseConnection(); //close connection
            }
        }

        /**
         * @brief Metodo per aggiornare record nel database 
         * @param[in] queryUpdate stringa contenente la query di aggiornamento
         */
        public void Update(string queryUpdate)
        {
            //string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

            if (this.OpenConnection() == true) //Apre la connessione e se resta aperta continua
            {
                MySqlCommand cmd = new MySqlCommand(); //create mysql command
                cmd.CommandText = queryUpdate; //Assign the query using CommandText
                cmd.Connection = connection; //Assign the connection using Connection
                cmd.ExecuteNonQuery(); //Execute query
                this.CloseConnection(); //close connection
            }
        }

        /**
         * @brief Metodo per eliminare record nel database 
         * @param[in] queryDelete stringa contenente la query di eliminazione
         */
        public void Delete(string queryDelete)
        {
            //string query = "DELETE FROM tableinfo WHERE name='John Smith'";

            if (this.OpenConnection() == true) //Apre la connessione e se resta aperta continua
            {
                MySqlCommand cmd = new MySqlCommand(queryDelete, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
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
        public List<string> GetAtleti()
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
        public int GetNumAthlete()
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
        public int GetNumAthleteWeek()
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

            string query = "SELECT COUNT(*) FROM atleti WHERE atleti.Scandenza_abbonamento>=NOW()"; //Crea stringa query


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

            string query = "SELECT COUNT(*) FROM atleti WHERE atleti.Scandenza_abbonamento<NOW()"; //Crea stringa query


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
    }
}


