using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_Palestra.Classi
{
    public class CMeccanico
    {
        ////Properties
        public int ID_Meccanico { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        ////COSTRUTTORE CON PARAMETRI
        public CMeccanico(int iD_Meccanico, string nome, string cognome, string username, string password)
        {
            ID_Meccanico = iD_Meccanico;
            Nome = nome;
            Cognome = cognome;
            Username = username;
            Password = password;
        }

        ////COSTRUTTORE CON PARAMETRO QUERYRES
        public CMeccanico(string QueryRes)
        {
            string[] lista = QueryRes.Split(';');

            ID_Meccanico = Int32.Parse(lista[0]);
            Nome = lista[1];
            Cognome = lista[2];
            Username = lista[3];
            Password = lista[4];
        }

        ////CALCOLA STRINGA DI UPDATE
        public string ToUpdate()
        {
            string ris = "UPDATE `meccanici` SET `Nome` = \'" + Nome + "\', `Cognome` = \'" + Cognome + "\', `Username` = \'" + Username + "\', `Password` = \'" + Password + "\'  WHERE `meccanici`.`ID_Meccanico` = " + ID_Meccanico + ";";
            return ris;
        }

        ////CALCOLA STRINGA DI INSERT
        public string ToInsert()
        {
            string ris = "INSERT INTO `meccanici` (`ID_Meccanico`, `Nome`, `Cognome`, `Username`, `Password`) VALUES(NULL, \'" + Nome+ "\', \'" + Cognome + "\', \'"+Username+ "\', \'"+Password+"\');";
            return ris;
        }

    }
}
