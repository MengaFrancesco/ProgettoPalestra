using MySql.Data.MySqlClient;
using NodaTime;
using Progetto_Palestra.Classi;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Progetto_Palestra
{
    class MySQLdatabase
    {
        ////OGGETTO MYSQLCONNECTION CHE PERMETTE LA CONNESSIONE
        private MySqlConnection connection;

        ////COSTRUTTORE SENZA PARAMETRI
        public MySQLdatabase()
        {
            Initialize();
        }

        ////INIZIALIZZAZIONE DELLA CONNESSIONE CON IL SERVER
        private void Initialize()
        {
            string connectionString = "Server=localhost;Database=db_palestra;Uid=root;Pwd=;"; //Inizializza la stringa di connessione
            connection = new MySqlConnection(connectionString); //Invia la stringa al server
        }

        ////TENTATIVO DI APERTURA DELLA CONNESSIONE CON IL SERVER
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
                MessageBox.Show(ex.ToString() + ", error:" + ex.Number.ToString());
                return false;
            }
        }

        ////TENTATIVO DI CHIUSURA DELLA CONNESSIONE CON IL SERVER
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


        ////QUERY ATLETI

        ////SELECT USERNAME E PASSWORD DI TUTTI GLI ATLETI
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

        ////SELECT TUTTE LE COLONNE DI TUTTI GLI ATLETI
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

        ////SELECT TUTTE LE COLONNE DELL'ATLETA DALL'USERNAME
        public CAtleta GetAtleta(string Username)
        {
            if (this.OpenConnection() == true) //Prova ad aprire la connessione
            {

                string query = "SELECT * FROM atleti WHERE Username=\"" + Username + "\";";
                MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando

                dataReader.Read(); //Read the data

                /* Legge le colonne, le inserisce in una stringa */
                string queryRes = "";
                int numC = dataReader.FieldCount;
                for (int i = 0; i < numC; i++)
                {
                    queryRes += dataReader[i] + ";";
                }
                CAtleta atleta = new CAtleta(queryRes); //Crea oggetto atleta

                dataReader.Close();     //close Data Reader
                this.CloseConnection(); //close Connection
                return atleta;            //Ritorna la lista con l'elenco degli amministratori
            }
            else
                return null;
        }

        ////SELECT COUNT DI TUTTI GLI ALLENAMENTI DALL'ID PARAMETRO
        public int GetNumAllenamenti(int ID)
        {
            if (this.OpenConnection() == true) //Prova ad aprire la connessione
            {
                int ris = 0;
                string query = "SELECT COUNT(*) FROM elenco_allenamenti WHERE Atleta=" + ID + ";";
                MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando

                dataReader.Read(); //Read the data

                ris = Int32.Parse(dataReader[0] + "");

                dataReader.Close();     //close Data Reader
                this.CloseConnection(); //close Connection
                return ris;            //Ritorna la lista con l'elenco degli amministratori
            }
            else
                return 0;
        }

        ////SELECT COUNT DEGLI ALLENAMENTI SETTIMANALI TOTALI
        public int GetNumAllenamentiW(int ID)
        {
            if (this.OpenConnection() == true) //Prova ad aprire la connessione
            {
                int ris = 0;
                string query = "SELECT COUNT(*) FROM elenco_allenamenti WHERE Atleta=" + ID + " AND YEARWEEK(NOW())=YEARWEEK(elenco_allenamenti.Data);";
                MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando

                dataReader.Read(); //Read the data

                ris = Int32.Parse(dataReader[0] + "");

                dataReader.Close();     //close Data Reader
                this.CloseConnection(); //close Connection
                return ris;            //Ritorna la lista con l'elenco degli amministratori
            }
            else
                return 0;
        }

        ////COUNT TUTTI GLI ATLETI
        public int GetNumAtleti()
        {
            int ris = 0;

            string query = "SELECT COUNT(*) FROM atleti"; //Crea stringa query


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

        ////COUNT ATLETI REGISTRATI QUESTA SETTIMANA
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

        ////COUNT DI TUTTI GLI ABBONATI
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

        ////COUNT DI TUTTI GLI ABBONAMENTI SCADUTI
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

        ////COUNT DI TUTTE LE VISITE
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

        ////COUNT DI TUTTE LE VISITE SETTIMANALI
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

        ////DELETE DELL'ATLETA DALL'ID PARAMETRO
        public void RimuoviAtleta(int ID)
        {
            try
            {
                string query = "DELETE FROM Atleti WHERE Atleti.ID_Atleta=" + ID + ";"; //Crea stringa query


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

        ////UPDATE DELL'ATLETA PARAMETRO
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

        ////INSERT DELL'ATLETA PARAMETRO
        public void InserisciAtleta(CAtleta Atleta)
        {
            if (this.OpenConnection() == true) //Apre la connessione e se resta aperta continua
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(Atleta.ToInsertQuery(), connection);
                cmd.ExecuteNonQuery(); //Execute command
                this.CloseConnection(); //close connection
            }
        }

        ////CHECK ESISTENZA USERNAME ATLETA
        public bool CheckUsernameAtleta(string Username)
        {
            bool ris = false;

            try
            {

                string query = "SELECT COUNT(*) FROM atleti WHERE Username=\'" + Username + "\'";


                if (this.OpenConnection() == true) //Apre la connessione e se resta aperta continua
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                    MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando

                    dataReader.Read(); //Fino a quando c'è da leggere

                    string s = (dataReader[0] + "");
                    if (s != "0")
                        ris = true;      //Legge colonna

                    dataReader.Close();     //close Data Reader
                    this.CloseConnection(); //close Connection

                    return ris;
                }
                else
                    return ris;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return false;
            }
        }
        
        
        ////QUERY AMMINISTRATORI

        ////SELECT DI USERNAME E PASSWORD DI TUTTI GLI AMMINISTRATORI
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

        ////SELECT DI TUTTE LE COLONNE DELL'AMMINISTRATORE PARAMETRO
        public string GetAdmin(string Username)
        {
            string query = "SELECT * FROM amministratori WHERE Username=\"" + Username + "\""; //Crea stringa query
            string ris = ""; //Creazione lista da restituire

            if (this.OpenConnection() == true) //Prova ad aprire la connessione
            {
                MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando

                dataReader.Read(); //Read the data and store them in the list

                ris = (dataReader["ID_Amministratore"] + ";" + dataReader["Username"] + ";" + dataReader["Password"] + ";" + dataReader["Nome"] + ";" + dataReader["Cognome"] + ";" + dataReader["Iscrizione"] + ";" + dataReader["Link_foto"] + "");  //Aggiunge alla lista il record


                dataReader.Close();     //close Data Reader
                this.CloseConnection(); //close Connection
                return ris;             //Ritorna la lista con l'elenco degli amministratori
            }
            else
            {
                return ris; //Ritorna la lista vuota
            }
        }


        ////QUERY ORARIO

        ////SELECT DI TUTTE LE COLONNE DI ORARI
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

                    while (dataReader.Read())
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

        ////AGGIORNA ORARIO DATO COME PARAMETRO AL GIORNO PARAMETRO
        public void AggiornaOrario(string giorno, DateTime orario1M, DateTime orario2M, DateTime orario1P, DateTime orario2P)
        {
            try
            {
                string query = "UPDATE orari "; //Crea stringa query       
                query += "SET DalleM=\'" + OrarioToQuery(orario1M) + "\'";
                query += ", AlleM=\'" + OrarioToQuery(orario2M) + "\'";
                query += ", DalleP=\'" + OrarioToQuery(orario1P) + "\'";
                query += ", AlleP=\'" + OrarioToQuery(orario2P) + "\'";
                query += " WHERE Giorno=\"" + giorno + "\";";

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
                MessageBox.Show("Impossibile aggiornare l'orario, errore:" + ex.Message);
            }


        }

        ////CONVERTE ORARIO IN STRIGA QUERY
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


        ////QUERY CONTROLLORI

        ////SELECT OGNI COLONNA DEI CONTROLLORI
        public CControllore GetControllore(string Username)
        {
            CControllore ris; //Inizializza nuova lista

            try
            {
                if (this.OpenConnection() == true) //Prova ad aprire la connessione
                {
                    string query = "SELECT * FROM controllori WHERE Username='" + Username + "';";
                    MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                    MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando

                    dataReader.Read();

                    string lista = "";
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        lista += dataReader[i] + ";";
                    }
                    ris = new CControllore(lista);
                    dataReader.Close();     //close Data Reader
                    this.CloseConnection(); //close Connection
                    return ris;
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }

        ////SELECT OGNI COLONNA DEI CONTROLLORI
        public List<string> GetControllori()
        {
            List<string> ris = new List<string>(); //Inizializza nuova lista

            try
            {
                if (this.OpenConnection() == true) //Prova ad aprire la connessione
                {
                    string query = "SELECT * FROM controllori";
                    MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                    MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando

                    while (dataReader.Read()) //Fino a quando c'è da leggere
                    {
                        string row = "";
                        int columns = dataReader.FieldCount; //Calcola numero colonne
                        for (int i = 0; i < columns; i++)
                        {
                            row += dataReader[i] + ";"; //Legge colonna
                        }
                        ris.Add(row); //Aggiunge colonna alla lista
                    }

                    dataReader.Close();     //close Data Reader
                    this.CloseConnection(); //close Connection
                }
            }
            catch (Exception)
            {
                return ris;
            }

            return ris;
        }

        ////INSERT CONTROLLORE PARAMETRO
        public void AggiungiControllore(CControllore cc)
        {
            if (this.OpenConnection() == true) //Apre la connessione e se resta aperta continua
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(cc.ToInsertQuery(), connection);
                cmd.ExecuteNonQuery(); //Execute command
                this.CloseConnection(); //close connection
            }
        }

        ////COUNT DEI CONTROLLORI CON L'USERNAME CORRISPONDENTE A PARAMETRO
        public int CountControllori(string username)
        {
            int ris = 0;
            try
            {
                if (this.OpenConnection() == true) //Prova ad aprire la connessione
                {
                    string query = "SELECT COUNT(*) FROM controllori WHERE Username=\"" + username + "\"";
                    MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                    MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando

                    dataReader.Read(); //Fino a quando c'è da leggere

                    ris = Int32.Parse(dataReader[0] + ""); //Legge colonna

                    dataReader.Close();     //close Data Reader
                    this.CloseConnection(); //close Connection
                }
            }
            catch (Exception)
            {
                return ris;
            }

            return ris;
        }

        ////UPDATE CONTROLLORE PARAMETRO
        public void UpdateControllore(CControllore cc)
        {
            if (this.OpenConnection() == true) //Apre la connessione e se resta aperta continua
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(cc.ToUpdateQuery(), connection);
                cmd.ExecuteNonQuery(); //Execute command
                this.CloseConnection(); //close connection
            }
        }

        ////DELETE DEL CONTROLLORE CON ID PARAMETRO
        public void DeleteControllore(int ID)
        {
            try
            {
                string query = "DELETE FROM Controllori WHERE Controllori.ID_Controllore=" + ID + ";"; //Crea stringa query


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
                MessageBox.Show("Impossibile rimuovere controllore, errore:" + ex.Message);
            }
        }

        ////CHECK CONTROLLORE SE ESISTE
        public bool CheckControllore(string Username, string Password)
        {
            bool ris = false;

            try
            {
                string query = "SELECT COUNT(*) FROM controllori WHERE Username=\'" + Username + "\' AND Password=\'" + Password + "\'";
                MessageBox.Show(query);

                if (this.OpenConnection() == true) //Apre la connessione e se resta aperta continua
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                    MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando

                    dataReader.Read(); //Fino a quando c'è da leggere

                    string s = (dataReader[0]+"");
                    if (s!="0")
                        ris = true;      //Legge colonna

                    dataReader.Close();     //close Data Reader
                    this.CloseConnection(); //close Connection


                    return ris;
                }
                else
                    return ris;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return false;
            }
        }

        
    }
}


