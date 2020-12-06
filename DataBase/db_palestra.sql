-- phpMyAdmin SQL Dump
-- version 5.0.3
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Creato il: Dic 06, 2020 alle 23:04
-- Versione del server: 10.4.14-MariaDB
-- Versione PHP: 7.2.34

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `db_palestra`
--

-- --------------------------------------------------------

--
-- Struttura della tabella `amministratori`
--

CREATE TABLE `amministratori` (
  `ID_Amministratore` int(11) NOT NULL,
  `Username` text NOT NULL,
  `Password` text NOT NULL,
  `Nome` text NOT NULL,
  `Cognome` text NOT NULL,
  `Iscrizione` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dump dei dati per la tabella `amministratori`
--

INSERT INTO `amministratori` (`ID_Amministratore`, `Username`, `Password`, `Nome`, `Cognome`, `Iscrizione`) VALUES
(1, 'admin1', '5f4dcc3b5aa765d61d8327deb882cf99', 'Amministratore', 'Tecnico', '2020-12-05'),
(2, 'admin_menga', '0b798742e47cca27398944b4fad98be7', 'Francesco', 'Menga', '2020-12-05');

-- --------------------------------------------------------

--
-- Struttura della tabella `atleti`
--

CREATE TABLE `atleti` (
  `ID_Atleta` int(11) NOT NULL,
  `Username` text NOT NULL,
  `Password` text NOT NULL,
  `Nome` text NOT NULL,
  `Cognome` text NOT NULL,
  `Residenza` text NOT NULL,
  `Data Iscrizione` date NOT NULL,
  `Sesso` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Struttura della tabella `attrezzatura`
--

CREATE TABLE `attrezzatura` (
  `ID_Attrezzo` int(11) NOT NULL,
  `Nome` text NOT NULL,
  `Tipologia` text NOT NULL,
  `Utilizzato` tinyint(1) NOT NULL,
  `Manutenzione Richiesta` tinyint(1) NOT NULL,
  `Manutenzione In Corso` tinyint(1) NOT NULL,
  `Link Foto` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Struttura della tabella `attrezzi_in_uso`
--

CREATE TABLE `attrezzi_in_uso` (
  `ID` int(11) NOT NULL,
  `ID_Atleta` int(32) NOT NULL,
  `ID_Attrezzo` int(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Struttura della tabella `controllore`
--

CREATE TABLE `controllore` (
  `ID_Controllore` int(11) NOT NULL,
  `Username` text NOT NULL,
  `Password` text NOT NULL,
  `Nome` text NOT NULL,
  `Cognome` text NOT NULL,
  `Data Iscrizione` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Struttura della tabella `elenco segnalazioni`
--

CREATE TABLE `elenco segnalazioni` (
  `ID_Segnalazione` int(11) NOT NULL,
  `ID_Atleta` int(11) NOT NULL,
  `ID_Attrezzo` int(11) NOT NULL,
  `Riparazione Terminata` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Struttura della tabella `meccanico`
--

CREATE TABLE `meccanico` (
  `ID_Meccanico` int(11) NOT NULL,
  `Nome` text NOT NULL,
  `Cognome` text NOT NULL,
  `Username` text NOT NULL,
  `Password` text NOT NULL,
  `Occupato` tinyint(1) NOT NULL,
  `ID_Attrezzo` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Indici per le tabelle scaricate
--

--
-- Indici per le tabelle `amministratori`
--
ALTER TABLE `amministratori`
  ADD PRIMARY KEY (`ID_Amministratore`);

--
-- Indici per le tabelle `atleti`
--
ALTER TABLE `atleti`
  ADD PRIMARY KEY (`ID_Atleta`);

--
-- Indici per le tabelle `attrezzatura`
--
ALTER TABLE `attrezzatura`
  ADD PRIMARY KEY (`ID_Attrezzo`);

--
-- Indici per le tabelle `attrezzi_in_uso`
--
ALTER TABLE `attrezzi_in_uso`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `Rel_ID_Atleta` (`ID_Atleta`),
  ADD KEY `Rel_ID_Attrezzo` (`ID_Attrezzo`);

--
-- Indici per le tabelle `controllore`
--
ALTER TABLE `controllore`
  ADD PRIMARY KEY (`ID_Controllore`);

--
-- Indici per le tabelle `elenco segnalazioni`
--
ALTER TABLE `elenco segnalazioni`
  ADD PRIMARY KEY (`ID_Segnalazione`),
  ADD KEY `Rel_Atleti` (`ID_Atleta`),
  ADD KEY `Rel_Attrezzi` (`ID_Attrezzo`);

--
-- Indici per le tabelle `meccanico`
--
ALTER TABLE `meccanico`
  ADD PRIMARY KEY (`ID_Meccanico`),
  ADD KEY `Rel_Attrezzo` (`ID_Attrezzo`);

--
-- AUTO_INCREMENT per le tabelle scaricate
--

--
-- AUTO_INCREMENT per la tabella `amministratori`
--
ALTER TABLE `amministratori`
  MODIFY `ID_Amministratore` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT per la tabella `atleti`
--
ALTER TABLE `atleti`
  MODIFY `ID_Atleta` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `attrezzatura`
--
ALTER TABLE `attrezzatura`
  MODIFY `ID_Attrezzo` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `controllore`
--
ALTER TABLE `controllore`
  MODIFY `ID_Controllore` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `elenco segnalazioni`
--
ALTER TABLE `elenco segnalazioni`
  MODIFY `ID_Segnalazione` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `meccanico`
--
ALTER TABLE `meccanico`
  MODIFY `ID_Meccanico` int(11) NOT NULL AUTO_INCREMENT;

--
-- Limiti per le tabelle scaricate
--

--
-- Limiti per la tabella `attrezzi_in_uso`
--
ALTER TABLE `attrezzi_in_uso`
  ADD CONSTRAINT `Rel_ID_Atleta` FOREIGN KEY (`ID_Atleta`) REFERENCES `atleti` (`ID_Atleta`),
  ADD CONSTRAINT `Rel_ID_Attrezzo` FOREIGN KEY (`ID_Attrezzo`) REFERENCES `atleti` (`ID_Atleta`);

--
-- Limiti per la tabella `elenco segnalazioni`
--
ALTER TABLE `elenco segnalazioni`
  ADD CONSTRAINT `Rel_Atleti` FOREIGN KEY (`ID_Atleta`) REFERENCES `atleti` (`ID_Atleta`),
  ADD CONSTRAINT `Rel_Attrezzi` FOREIGN KEY (`ID_Attrezzo`) REFERENCES `attrezzatura` (`ID_Attrezzo`);

--
-- Limiti per la tabella `meccanico`
--
ALTER TABLE `meccanico`
  ADD CONSTRAINT `Rel_Attrezzo` FOREIGN KEY (`ID_Attrezzo`) REFERENCES `attrezzatura` (`ID_Attrezzo`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
