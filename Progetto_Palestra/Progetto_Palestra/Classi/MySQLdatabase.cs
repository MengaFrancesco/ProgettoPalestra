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

        ////Oggetto che permette la connessione
        private MySqlConnection connection;

        ////COSTRUTTORE SENZA PARAMETRI
        public MySQLdatabase()
        {
            Initialize();
        }

        #region METODI INIZIALIZZAZIONE DATABASE

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

        #endregion

        #region QUERY ATLETI

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

        ////SELECT DELLA SCADENZA DI UN ATLETA
        public string SelectScadenza(string username)
        {
            string query = "SELECT Scadenza_abbonamento FROM Atleti WHERE Username='"+username+"'"; //Crea stringa query


            if (this.OpenConnection() == true) //Prova ad aprire la connessione
            {
                MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando

                dataReader.Read(); //Read the data and store them in the list

                string ris = dataReader[0] + "";

                dataReader.Close();     //close Data Reader
                this.CloseConnection(); //close Connection
                return ris;             //Ritorna la lista con l'elenco degli amministratori
            }
            else
            {
                string ris = "";
                return ris; //Ritorna la lista vuota
            }
        }

        ////SELECT TUTTE LE COLONNE DI TUTTI GLI ATLETI
        public List<CAtleta> SelectAtleti()
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

        ////SELECT ID ATLETA DALL'USERNAME
        public int GetIDAtleta(string Username)
        {
            try
            {
                if (this.OpenConnection() == true) //Prova ad aprire la connessione
                {
                    int ris = 0;
                    string query = "SELECT ID_Atleta FROM atleti WHERE Username=\'" + Username + "\';";
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
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
        
        ////UPDATE SCADENZA DI UN ATLETA E DATA PARAMETRO
        public void UpdateScadenza(string username, DateTime scadenza)
        {
            string query = "UPDATE `atleti` SET `Scadenza_abbonamento` = '"+scadenza.Year+"-"+ scadenza.Month + "-" +scadenza.Day + "' WHERE `atleti`.`Username` ='" +username+"';";

            try
            {
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
        public int CountAtleti()
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
        public int CountAtletiWeek()
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
        public int CountAbbonati()
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
        public int CountScaduti()
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
        public void UpdateAtleta(CAtleta atleta)
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

        ////CHECK ACCESSO ESEGUITO DA USERNAME
        public bool CheckAccesso(int ID)
        {
            bool ris = false;

            try
            {

                string query = "SELECT * FROM elenco_visite WHERE ID_Atleta=" + ID + " AND Uscita=0 ORDER BY Data DESC";


                if (this.OpenConnection() == true) //Apre la connessione e se resta aperta continua
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                    MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando

                    if (dataReader.Read()) //Fino a quando c'è da leggere
                    {
                        ris = true;
                    }
                    else
                        ris = false;
                    

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
                return ris;
            }
        }

        ////QUERY INSERT ACCESSO IN ELENCO_VISITE
        public void EseguiAccesso(int ID)
        {
            try
            {
                if (this.OpenConnection() == true) //Apre la connessione e se resta aperta continua
                {
                    string query = "INSERT INTO `elenco_visite` (`ID_Visita`, `ID_Atleta`, `Data`, `Ingresso`, `Uscita`) VALUES(NULL, '" + ID + "', NOW(), NOW(), '0');";
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery(); //Execute command
                    this.CloseConnection(); //close connection
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        ////QUERY UPDATE ACCESSO IN ELENCO_VISITE
        public void EseguiUscita(int ID)
        {
            try
            {
                if (this.OpenConnection() == true) //Apre la connessione e se resta aperta continua
                {
                    string query = "UPDATE elenco_visite SET Uscita = '1' WHERE elenco_visite.Uscita=0 AND ID_Atleta="+ID+";";
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery(); //Execute command
                    this.CloseConnection(); //close connection
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        ////COUNT ISCRIZIONI CON DATA CORRISPONDENTE A PARAMETRO
        public int CountIscrizioni(DateTime data)
        {
            int ris = 0;

            string query = "SELECT COUNT(*) FROM atleti WHERE Data_Iscrizione=\'" + data.Year+"-"+data.Month+"-"+data.Day + "\'"; //Crea stringa query


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

        #endregion

        #region QUERY AMMINISTRATORI

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

        ////COUNT NUMERO TOTALE AMMIINISTRATORI
        public int CountAmministratori()
        {
            int ris = 0;

            string query = "SELECT COUNT(*) FROM amministratori"; //Crea stringa query


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

        #endregion

        #region QUERY ORARIO

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

        #endregion

        #region QUERY CONTROLLORI

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

        ////COUNT NUMERO TOTALE CONTROLLORI
        public int CountControllori()
        {
            int ris = 0;

            string query = "SELECT COUNT(*) FROM controllori"; //Crea stringa query


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

        #endregion

        #region QUERY ELENCO_VISITE

        ////SELECT VISITE
        public List<CVisita> SelectVisite()
        {
            List<CVisita> ris = new List<CVisita>();

            try
            {
                if (this.OpenConnection() == true) //Prova ad aprire la connessione
                {
                    string query = "SELECT * FROM elenco_visite";
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
                        ris.Add(new CVisita(row)); //Aggiunge colonna alla lista
                    }

                    dataReader.Close();     //close Data Reader
                    this.CloseConnection(); //close Connection
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
                return ris;
            }

            return ris;
        }

        ////COUNT VISITE
        public int CountVisite()
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

        public int CountVisiteWeek()
        {
            int ris = 0;

            string query = "SELECT COUNT(*) FROM elenco_visite WHERE YEARWEEK(Data)=YEARWEEK(NOW())"; //Crea stringa query


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

        public int CountVisite(int ID)
        {
            int ris = 0;

            string query = "SELECT COUNT(*) FROM elenco_visite WHERE ID_Atleta=\'"+ID+"\'"; //Crea stringa query


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

        public int CountVisiteWeek(int ID)
        {
            int ris = 0;

            string query = "SELECT COUNT(*) FROM elenco_visite WHERE ID_Atleta=\'" + ID + "\' AND YEARWEEK(Data)=YEARWEEK(NOW())"; //Crea stringa query


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

        #endregion  

        #region QUERY ATTREZZI

        ////SELECT ATTREZZI
        public List<CAttrezzo> SelectAttrezziDisp()
        {
            List<CAttrezzo> ris = new List<CAttrezzo>();

            try
            {
                if (this.OpenConnection() == true) //Prova ad aprire la connessione
                {
                    string query = "SELECT * FROM attrezzatura WHERE Utilizzato=0 AND Manutenzione_Richiesta=0";
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
                        ris.Add(new CAttrezzo(row)); //Aggiunge colonna alla lista
                    }

                    dataReader.Close();     //close Data Reader
                    this.CloseConnection(); //close Connection
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ris;
            }

            return ris;
        }
          
        ////UPDATE ATTREZZO A SELEZIONATO
        public void UpdateAttrezzoSeleziona(int ID_Attrezzo)
        {
            try
            {
                string query = "UPDATE `attrezzatura` SET `Utilizzato` = '1' WHERE `attrezzatura`.`ID_Attrezzo` = " + ID_Attrezzo + ";";


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
                MessageBox.Show(ex.Message);
            }
            

        }

        ////UPDATE ATTREZZO A RILASCIATO
        public void UpdateAttrezzoRilascia(int ID_Attrezzo)
        {
            try
            {
                string query = "UPDATE `attrezzatura` SET `Utilizzato` = '0' WHERE `attrezzatura`.`ID_Attrezzo` = " + ID_Attrezzo + ";";


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
                MessageBox.Show(ex.Message);
            }


        }

        ////UPDATE ATTREZZO A SEGNALATO
        public void UpdateAttrezzoSegnalato(int ID_Attrezzo)
        {
            try
            {
                string query = "UPDATE `attrezzatura` SET `Manutenzione_Richiesta` = '1' WHERE `attrezzatura`.`ID_Attrezzo` = " + ID_Attrezzo + ";";


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
                MessageBox.Show(ex.Message);
            }


        }

        ////UPDATE ATTREZZO A RIPARATO
        public void UpdateAttrezzoRiparato(int ID_Attrezzo)
        {
            try
            {
                string query = "UPDATE `attrezzatura` SET `Manutenzione_Richiesta` = '0' WHERE `attrezzatura`.`ID_Attrezzo` = " + ID_Attrezzo + ";";


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
                MessageBox.Show(ex.Message);
            }


        }

        #endregion

        #region QUERY ELENCO ALLENAMENTI

        ////SELECT ALLENAMENTI ATLETA ID PARAMETRO
        public List<CAllenamento> SelectAllenamentiAtleta(int ID)
        {
            List<CAllenamento> ris = new List<CAllenamento>();

            try
            {
                if (this.OpenConnection() == true) //Prova ad aprire la connessione
                {
                    string query = "SELECT * FROM elenco_allenamenti WHERE Atleta="+ID;
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
                        ris.Add(new CAllenamento(row)); //Aggiunge colonna alla lista
                    }

                    dataReader.Close();     //close Data Reader
                    this.CloseConnection(); //close Connection
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ris;
            }

            return ris;
        }

        ////INSERT ALLENAMENTO
        public void InsertAllenamento(CAllenamento allenamento)
        {
            if (this.OpenConnection() == true) //Apre la connessione e se resta aperta continua
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(allenamento.ToQueryInsert(), connection);
                cmd.ExecuteNonQuery(); //Execute command
                this.CloseConnection(); //close connection
            }
        }

        ////COUNT ALLENAMENTI TOTALI
        public int CountAllenamenti()
        {
            int ris = 0;

            string query = "SELECT COUNT(*) FROM elenco_allenamenti"; //Crea stringa query


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

        ////COUNT ALLENAMENTI TOTALI SETTIMANA
        public int CountAllenamentiW()
        {
            int ris = 0;

            string query = "SELECT COUNT(*) FROM elenco_allenamenti WHERE YEARWEEK(NOW())=YEARWEEK(elenco_allenamenti.Data)"; //Crea stringa query


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

        #endregion

        #region QUERY SEGNALAZIONI

        ////SELECT SEGNALAZIONI NON INIZIATE
        public List<CSegnalazione> SelectSegnalazioniDisp()
        {
            List<CSegnalazione> ris = new List<CSegnalazione>();

            try
            {
                if (this.OpenConnection() == true) //Prova ad aprire la connessione
                {
                    string query = "SELECT * FROM elenco_segnalazioni WHERE Riparazione_In_Corso=0 AND Riparazione_Terminata=0";
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
                        ris.Add(new CSegnalazione(row)); //Aggiunge colonna alla lista
                    }

                    dataReader.Close();     //close Data Reader
                    this.CloseConnection(); //close Connection
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ris;
            }

            return ris;
        }

        ////INSERT SEGNALAZIONE DI ATTREZZO DA ATLETA PARAMETRO
        public void InsertSegnalazione(int ID_Attrezzo, int ID_Atleta)
        {
            string query = "INSERT INTO `elenco_segnalazioni` (`ID_Segnalazione`, `ID_Atleta`, `ID_Attrezzo`, `Riparazione_In_Corso`, `Riparazione_Terminata`) VALUES (NULL, '";
            query += ID_Atleta+ "', '"+ ID_Attrezzo +"', '0', '0')";

            if (this.OpenConnection() == true) //Apre la connessione e se resta aperta continua
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery(); //Execute command
                this.CloseConnection(); //close connection
            }
        }

        ////UPDATE SEGNALAZIONE IN RIPARAZIONE
        public void UpdateSegnalazioneIniziata(int ID_Segnalazione)
        {
            try
            {
                string query = "UPDATE `elenco_segnalazioni` SET `Riparazione_In_Corso` = '1' WHERE `elenco_segnalazioni`.`ID_Segnalazione` = " + ID_Segnalazione + ";";


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
                MessageBox.Show(ex.Message);
            }


        }

        ////UPDATE SEGNALAZIONE FINE RIPARAZIONE
        public void UpdateSegnalazioneTerminata(int ID_Segnalazione)
        {
            try
            {
                string query = "UPDATE `elenco_segnalazioni` SET `Riparazione_Terminata` = '1' WHERE `elenco_segnalazioni`.`ID_Segnalazione` = " + ID_Segnalazione + ";";


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
                MessageBox.Show(ex.Message);
            }


        }

        ////COUNT SEGNALAZIONI DI UN UTENTE
        public int CountSegnalazioni(int ID)
        {
            int count = 0;

            try
            {
                if (this.OpenConnection() == true) //Prova ad aprire la connessione
                {
                    string query = "SELECT COUNT(*) FROM elenco_segnalazioni WHERE ID_Atleta=\'"+ID+"\'";
                    MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                    MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando

                    dataReader.Read(); //Fino a quando c'è da leggere
                    count = Int32.Parse(dataReader[0] + ""); //Legge colonna

                    dataReader.Close();     //close Data Reader
                    this.CloseConnection(); //close Connection
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return count;
            }

            return count;
        }
        #endregion

        #region QUERY MECCANICO

        ////CONTROLLA SE USERNAME E PASSWORD DEL MECCANICO SONO CORRETTI
        public bool CheckMeccanico(string username, string password)
        {
            bool ris = false;

            try
            {
                string query = "SELECT COUNT(*) FROM meccanici WHERE Username=\'" + username + "\' AND Password=\'" + password + "\'";
                
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

        ////COUNT NUMERO TOTALE MECCANICI
        public int CountMeccanici()
        {
            int ris = 0;

            string query = "SELECT COUNT(*) FROM meccanici"; //Crea stringa query


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

        ////SELECT DI TUTTI I MECCANICI
        public List<CMeccanico> SelectMeccanici()
        {
            string query = "SELECT * FROM meccanici"; //Crea stringa query
            List<CMeccanico> lista = new List<CMeccanico>(); //Creazione lista da restituire

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
                    lista.Add(new CMeccanico(queryRes)); //Aggiunge alla lista il record
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

        ////CHECK ESISTENZA MECCANICO
        public bool CheckMeccanico(string username)
        {
            string query = "SELECT COUNT(*) FROM meccanici WHERE Username=\'" + username + "\'";
            if (this.OpenConnection() == true) //Prova ad aprire la connessione
            {
                MySqlCommand cmd = new MySqlCommand(query, connection); //Crea comando da eseguire
                MySqlDataReader dataReader = cmd.ExecuteReader();       //Esegue il comando

                dataReader.Read(); //Read the data and store them in the list

                string risS = dataReader["COUNT(*)"] + "";
                int ris = Int32.Parse(risS);
                

                dataReader.Close();     //close Data Reader
                this.CloseConnection(); //close Connection
                if (ris == 0)
                    return true;
                else
                    return false;             //Ritorna la lista con l'elenco degli amministratori
            }
            else
            {
                return false; //Ritorna la lista vuota
            }
        }
        
        ////INSERT MECCANICO
        public void InsertMeccanico(CMeccanico meccanico)
        {
            if (this.OpenConnection() == true) //Apre la connessione e se resta aperta continua
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(meccanico.ToInsert(), connection);
                cmd.ExecuteNonQuery(); //Execute command
                this.CloseConnection(); //close connection
            }
        }

        ////UPDATE MECCANICO
        public void UpdateMeccanico(CMeccanico meccanico)
        {
            if (this.OpenConnection() == true) //Apre la connessione e se resta aperta continua
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(meccanico.ToUpdate(), connection);
                cmd.ExecuteNonQuery(); //Execute command
                this.CloseConnection(); //close connection
            }
        }

        ////ELIMINA MECCANICO
        public void DeleteMeccanico(int ID)
        {
            string query = "DELETE FROM meccanici WHERE ID_Meccanico=\'"+ID+"\';";
            if (this.OpenConnection() == true) //Apre la connessione e se resta aperta continua
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery(); //Execute command
                this.CloseConnection(); //close connection
            }
        }

        #endregion

    }
}


