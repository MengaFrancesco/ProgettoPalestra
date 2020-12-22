-- phpMyAdmin SQL Dump
-- version 4.9.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Creato il: Dic 22, 2020 alle 12:17
-- Versione del server: 10.4.6-MariaDB
-- Versione PHP: 7.3.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
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
  `Iscrizione` date NOT NULL,
  `Link_foto` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dump dei dati per la tabella `amministratori`
--

INSERT INTO `amministratori` (`ID_Amministratore`, `Username`, `Password`, `Nome`, `Cognome`, `Iscrizione`, `Link_foto`) VALUES
(1, 'admin1', '5f4dcc3b5aa765d61d8327deb882cf99', 'Amministratore', 'Tecnico', '2020-12-05', 'https://www.laleggepertutti.it/wp-content/uploads/2019/02/Risalire-profilo-falso-360x240.jpg'),
(3, 'Scimmia', 'caa55851ce62c1953362569e7d48bfa1', 'Stefano', 'Borgonovo', '2020-12-02', 'https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.mbnews.it%2F2018%2F01%2Fda-sindaco-a-candidato-al-senato-della-repubblica-riccardo-borgonovo%2F&psig=AOvVaw1dEufpqJ51pujs6R8TcP0I&ust=1608721736630000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCNiX9s-54e0CFQAAAAAdAAAAABAE');

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
  `Data_Iscrizione` date NOT NULL,
  `Data_nascita` date NOT NULL,
  `Sesso` text NOT NULL,
  `Scadenza_abbonamento` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dump dei dati per la tabella `atleti`
--

INSERT INTO `atleti` (`ID_Atleta`, `Username`, `Password`, `Nome`, `Cognome`, `Residenza`, `Data_Iscrizione`, `Data_nascita`, `Sesso`, `Scadenza_abbonamento`) VALUES
(1, 'Waye1980', 'db73f93218febe86219325415703c8cd', 'Rufino', 'Rossi', 'Tavarnelle Val Di Pesa', '2020-12-20', '1980-09-21', 'M', '2021-03-18'),
(2, 'Zably1994', '229017f52cef53b783832b75891d7023', 'Giovanna', 'Endrizzi', 'Laveno Mombello', '2020-12-20', '1994-11-01', 'F', '2020-12-18'),
(3, 'Untake', 'ea3b6e450ca0c990bc6cd150d663b667', 'Ernesto', 'Pinti', 'Catania', '2020-12-18', '1995-10-24', 'M', '2020-12-09'),
(4, 'Barries', 'db73f93218febe86219325415703c8cd', 'Silvestro', 'Manna', 'Castel Focognano', '2020-12-14', '1980-02-07', 'M', '2020-12-10'),
(5, 'Decithe1985', '37e4fdd78a0277d15d0e01d9978e7dd6', 'Gloria', 'Siciliano', 'Faenza', '2020-12-14', '1994-03-24', 'F', '2020-12-18');

-- --------------------------------------------------------

--
-- Struttura della tabella `attrezzatura`
--

CREATE TABLE `attrezzatura` (
  `ID_Attrezzo` int(11) NOT NULL,
  `Nome` text NOT NULL,
  `Tipologia` text NOT NULL,
  `Modello` text NOT NULL,
  `Utilizzato` tinyint(1) NOT NULL,
  `Manutenzione_Richiesta` tinyint(1) NOT NULL,
  `Link Foto` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dump dei dati per la tabella `attrezzatura`
--

INSERT INTO `attrezzatura` (`ID_Attrezzo`, `Nome`, `Tipologia`, `Modello`, `Utilizzato`, `Manutenzione_Richiesta`, `Link Foto`) VALUES
(1, 'Tapis Roulant', 'Corsa', 'F75 TAPIS ROULANT', 0, 0, 'https://www.sports-tech.it/products/sportstech/IT/f75/gallery/default/100.jpg'),
(2, 'Tapis Roulant', 'Corsa', 'F75 TAPIS ROULANT', 0, 0, 'https://www.sports-tech.it/products/sportstech/IT/f75/gallery/default/100.jpg'),
(3, 'Tapis Roulant', 'Corsa', 'F75 TAPIS ROULANT', 0, 0, 'https://www.sports-tech.it/products/sportstech/IT/f75/gallery/default/100.jpg'),
(4, 'Tapis Roulant', 'Corsa', 'F75 TAPIS ROULANT', 0, 0, 'https://www.sports-tech.it/products/sportstech/IT/f75/gallery/default/100.jpg'),
(5, 'Tapis Roulant', 'Corsa', 'F75 TAPIS ROULANT', 0, 0, 'https://www.sports-tech.it/products/sportstech/IT/f75/gallery/default/100.jpg'),
(6, 'Cyclette', 'Bici', 'SX500 BICI DA INTERNO', 0, 0, 'https://www.sports-tech.it/products/sportstech/IT/sx500/gallery/default/100.jpg'),
(7, 'Cyclette', 'Bici', 'SX500 BICI DA INTERNO', 0, 0, 'https://www.sports-tech.it/products/sportstech/IT/sx500/gallery/default/100.jpg'),
(8, 'Cyclette', 'Bici', 'SX500 BICI DA INTERNO', 0, 1, 'https://www.sports-tech.it/products/sportstech/IT/sx500/gallery/default/100.jpg'),
(9, 'Cyclette', 'Bici', 'SX500 BICI DA INTERNO', 0, 1, 'https://www.sports-tech.it/products/sportstech/IT/sx500/gallery/default/100.jpg'),
(10, 'Cyclette', 'Bici', 'SX500 BICI DA INTERNO', 0, 0, 'https://www.sports-tech.it/products/sportstech/IT/sx500/gallery/default/100.jpg'),
(11, 'Panca Piana', 'Petto e tricipiti', 'Panca con Bilanciere Pieghevole Benzoni Gym ', 0, 0, 'https://media.giordanoshop.com/catalog/product/cache/1/thumbnail/9df78eab33525d08d6e5fb8d27136e95/A/9/A91-008-1/www.giordanoshop.com-benzoni-185433-11.jpg'),
(12, 'Panca Piana', 'Petto e tricipiti', 'Panca con Bilanciere Pieghevole Benzoni Gym ', 0, 0, 'https://media.giordanoshop.com/catalog/product/cache/1/thumbnail/9df78eab33525d08d6e5fb8d27136e95/A/9/A91-008-1/www.giordanoshop.com-benzoni-185433-11.jpg'),
(13, 'Panca Piana', 'Petto e tricipiti', 'Panca con Bilanciere Pieghevole Benzoni Gym ', 0, 0, 'https://media.giordanoshop.com/catalog/product/cache/1/thumbnail/9df78eab33525d08d6e5fb8d27136e95/A/9/A91-008-1/www.giordanoshop.com-benzoni-185433-11.jpg'),
(14, 'Panca Piana', 'Petto e tricipiti', 'Panca con Bilanciere Pieghevole Benzoni Gym ', 0, 0, 'https://media.giordanoshop.com/catalog/product/cache/1/thumbnail/9df78eab33525d08d6e5fb8d27136e95/A/9/A91-008-1/www.giordanoshop.com-benzoni-185433-11.jpg'),
(15, 'Panca Piana', 'Petto e tricipiti', 'Panca con Bilanciere Pieghevole Benzoni Gym ', 0, 0, 'https://media.giordanoshop.com/catalog/product/cache/1/thumbnail/9df78eab33525d08d6e5fb8d27136e95/A/9/A91-008-1/www.giordanoshop.com-benzoni-185433-11.jpg'),
(16, 'Panca Piana', 'Petto e tricipiti', 'Panca con Bilanciere Pieghevole Benzoni Gym ', 0, 0, 'https://media.giordanoshop.com/catalog/product/cache/1/thumbnail/9df78eab33525d08d6e5fb8d27136e95/A/9/A91-008-1/www.giordanoshop.com-benzoni-185433-11.jpg'),
(17, 'Bilanciere 30 kg', 'Bicipiti, schiena, tricipiti, spalle', 'vidaXL Set Bilanciere con 2 Manubri 30,5 kg', 0, 0, 'https://vdxl.im/8718475844204_a_en_r458.jpg'),
(18, 'Bilanciere 30 kg', 'Bicipiti, schiena, tricipiti, spalle', 'vidaXL Set Bilanciere con 2 Manubri 30,5 kg', 0, 0, 'https://vdxl.im/8718475844204_a_en_r458.jpg'),
(19, 'Bilanciere 30 kg', 'Bicipiti, schiena, tricipiti, spalle', 'vidaXL Set Bilanciere con 2 Manubri 30,5 kg', 0, 0, 'https://vdxl.im/8718475844204_a_en_r458.jpg'),
(20, 'Bilanciere 30 kg', 'Bicipiti, schiena, tricipiti, spalle', 'vidaXL Set Bilanciere con 2 Manubri 30,5 kg', 0, 0, 'https://vdxl.im/8718475844204_a_en_r458.jpg'),
(21, 'Bilanciere 30 kg', 'Bicipiti, schiena, tricipiti, spalle', 'vidaXL Set Bilanciere con 2 Manubri 30,5 kg', 0, 0, 'https://vdxl.im/8718475844204_a_en_r458.jpg');

-- --------------------------------------------------------

--
-- Struttura della tabella `controllori`
--

CREATE TABLE `controllori` (
  `ID_Controllore` int(11) NOT NULL,
  `Username` text NOT NULL,
  `Password` text NOT NULL,
  `Nome` text NOT NULL,
  `Cognome` text NOT NULL,
  `Data_Iscrizione` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dump dei dati per la tabella `controllori`
--

INSERT INTO `controllori` (`ID_Controllore`, `Username`, `Password`, `Nome`, `Cognome`, `Data_Iscrizione`) VALUES
(1, 'controllore', '5f4dcc3b5aa765d61d8327deb882cf99', 'Mario', 'Rossi', '2020-12-15'),
(2, 'maik80', '180eb41a8704236cf0fbb79e464526ff', 'Matteo', 'Serroni', '2020-12-15');

-- --------------------------------------------------------

--
-- Struttura della tabella `elenco_allenamenti`
--

CREATE TABLE `elenco_allenamenti` (
  `ID_Allenamento` int(11) NOT NULL,
  `Data` date NOT NULL,
  `Ora_inizio` time NOT NULL,
  `Ora_fine` time NOT NULL,
  `Atleta` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dump dei dati per la tabella `elenco_allenamenti`
--

INSERT INTO `elenco_allenamenti` (`ID_Allenamento`, `Data`, `Ora_inizio`, `Ora_fine`, `Atleta`) VALUES
(1, '2020-12-14', '09:10:00', '10:00:00', 1),
(2, '2020-12-14', '10:10:00', '11:00:00', 2),
(3, '2020-12-16', '09:10:00', '10:00:00', 5),
(4, '2020-12-18', '19:33:00', '19:33:33', 1),
(5, '2020-12-18', '19:40:00', '19:40:40', 1),
(6, '2020-12-18', '19:40:40', '19:41:41', 1);

-- --------------------------------------------------------

--
-- Struttura della tabella `elenco_segnalazioni`
--

CREATE TABLE `elenco_segnalazioni` (
  `ID_Segnalazione` int(11) NOT NULL,
  `ID_Atleta` int(11) NOT NULL,
  `ID_Attrezzo` int(11) NOT NULL,
  `Riparazione_In_Corso` tinyint(1) NOT NULL,
  `Riparazione_Terminata` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dump dei dati per la tabella `elenco_segnalazioni`
--

INSERT INTO `elenco_segnalazioni` (`ID_Segnalazione`, `ID_Atleta`, `ID_Attrezzo`, `Riparazione_In_Corso`, `Riparazione_Terminata`) VALUES
(4, 3, 20, 1, 1),
(5, 1, 4, 1, 1),
(6, 1, 9, 1, 1),
(7, 1, 21, 1, 1),
(8, 1, 1, 1, 1),
(9, 1, 4, 1, 1),
(10, 1, 8, 0, 0),
(11, 1, 9, 0, 0);

-- --------------------------------------------------------

--
-- Struttura della tabella `elenco_visite`
--

CREATE TABLE `elenco_visite` (
  `ID_Visita` int(11) NOT NULL,
  `ID_Atleta` int(11) NOT NULL,
  `Data` date NOT NULL,
  `Ingresso` time NOT NULL,
  `Uscita` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dump dei dati per la tabella `elenco_visite`
--

INSERT INTO `elenco_visite` (`ID_Visita`, `ID_Atleta`, `Data`, `Ingresso`, `Uscita`) VALUES
(3, 1, '2020-12-14', '08:00:00', 1),
(4, 1, '2020-12-15', '08:00:00', 1),
(5, 1, '2020-12-16', '08:00:00', 1),
(6, 4, '2020-12-16', '18:35:00', 1),
(7, 4, '2020-12-16', '18:42:54', 1),
(8, 4, '2020-12-16', '18:43:09', 1),
(9, 4, '2020-12-16', '18:47:07', 1),
(10, 1, '2020-12-20', '18:49:08', 1);

-- --------------------------------------------------------

--
-- Struttura della tabella `meccanici`
--

CREATE TABLE `meccanici` (
  `ID_Meccanico` int(11) NOT NULL,
  `Nome` text NOT NULL,
  `Cognome` text NOT NULL,
  `Username` text NOT NULL,
  `Password` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dump dei dati per la tabella `meccanici`
--

INSERT INTO `meccanici` (`ID_Meccanico`, `Nome`, `Cognome`, `Username`, `Password`) VALUES
(1, 'nome_meccanico', 'cognome_meccanico', 'meccanico', '5f4dcc3b5aa765d61d8327deb882cf99'),
(8, 'Mario', 'Delnero', 'Paperino', 'dc647eb65e6711e155375218212b3964');

-- --------------------------------------------------------

--
-- Struttura della tabella `orari`
--

CREATE TABLE `orari` (
  `ID` int(11) NOT NULL,
  `Giorno` text NOT NULL,
  `DalleM` time(6) NOT NULL,
  `AlleM` time(6) NOT NULL,
  `DalleP` time(6) NOT NULL,
  `AlleP` time(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dump dei dati per la tabella `orari`
--

INSERT INTO `orari` (`ID`, `Giorno`, `DalleM`, `AlleM`, `DalleP`, `AlleP`) VALUES
(1, 'Lunedì', '09:00:00.000000', '12:00:00.000000', '15:00:00.000000', '20:00:00.000000'),
(2, 'Martedì', '08:00:00.000000', '12:00:00.000000', '16:00:00.000000', '20:00:00.000000'),
(3, 'Martedì', '08:00:00.000000', '12:00:00.000000', '16:00:00.000000', '20:00:00.000000'),
(4, 'Mercoledì', '08:00:00.000000', '12:00:00.000000', '16:00:00.000000', '20:00:00.000000'),
(5, 'Giovedì', '08:00:00.000000', '12:00:00.000000', '16:00:00.000000', '20:00:00.000000'),
(6, 'Venerdì', '08:00:00.000000', '12:00:00.000000', '16:00:00.000000', '20:00:00.000000'),
(7, 'Sabato', '08:00:00.000000', '12:00:00.000000', '16:00:00.000000', '20:00:00.000000'),
(8, 'Domenica', '08:00:00.000000', '12:00:00.000000', '16:00:00.000000', '20:00:00.000000');

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
-- Indici per le tabelle `controllori`
--
ALTER TABLE `controllori`
  ADD PRIMARY KEY (`ID_Controllore`);

--
-- Indici per le tabelle `elenco_allenamenti`
--
ALTER TABLE `elenco_allenamenti`
  ADD PRIMARY KEY (`ID_Allenamento`),
  ADD KEY `Rel_Atleta_allenamento` (`Atleta`);

--
-- Indici per le tabelle `elenco_segnalazioni`
--
ALTER TABLE `elenco_segnalazioni`
  ADD PRIMARY KEY (`ID_Segnalazione`),
  ADD KEY `Rel_Atleti` (`ID_Atleta`),
  ADD KEY `Rel_Attrezzi` (`ID_Attrezzo`);

--
-- Indici per le tabelle `elenco_visite`
--
ALTER TABLE `elenco_visite`
  ADD PRIMARY KEY (`ID_Visita`),
  ADD KEY `Rel_id_atleta_visita` (`ID_Atleta`);

--
-- Indici per le tabelle `meccanici`
--
ALTER TABLE `meccanici`
  ADD PRIMARY KEY (`ID_Meccanico`);

--
-- Indici per le tabelle `orari`
--
ALTER TABLE `orari`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT per le tabelle scaricate
--

--
-- AUTO_INCREMENT per la tabella `amministratori`
--
ALTER TABLE `amministratori`
  MODIFY `ID_Amministratore` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT per la tabella `atleti`
--
ALTER TABLE `atleti`
  MODIFY `ID_Atleta` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT per la tabella `attrezzatura`
--
ALTER TABLE `attrezzatura`
  MODIFY `ID_Attrezzo` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT per la tabella `controllori`
--
ALTER TABLE `controllori`
  MODIFY `ID_Controllore` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT per la tabella `elenco_allenamenti`
--
ALTER TABLE `elenco_allenamenti`
  MODIFY `ID_Allenamento` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT per la tabella `elenco_segnalazioni`
--
ALTER TABLE `elenco_segnalazioni`
  MODIFY `ID_Segnalazione` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT per la tabella `elenco_visite`
--
ALTER TABLE `elenco_visite`
  MODIFY `ID_Visita` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT per la tabella `meccanici`
--
ALTER TABLE `meccanici`
  MODIFY `ID_Meccanico` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT per la tabella `orari`
--
ALTER TABLE `orari`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- Limiti per le tabelle scaricate
--

--
-- Limiti per la tabella `elenco_allenamenti`
--
ALTER TABLE `elenco_allenamenti`
  ADD CONSTRAINT `Rel_Atleta_allenamento` FOREIGN KEY (`Atleta`) REFERENCES `atleti` (`ID_Atleta`);

--
-- Limiti per la tabella `elenco_segnalazioni`
--
ALTER TABLE `elenco_segnalazioni`
  ADD CONSTRAINT `Rel_Atleti` FOREIGN KEY (`ID_Atleta`) REFERENCES `atleti` (`ID_Atleta`),
  ADD CONSTRAINT `Rel_Attrezzi` FOREIGN KEY (`ID_Attrezzo`) REFERENCES `attrezzatura` (`ID_Attrezzo`);

--
-- Limiti per la tabella `elenco_visite`
--
ALTER TABLE `elenco_visite`
  ADD CONSTRAINT `Rel_id_atleta_visita` FOREIGN KEY (`ID_Atleta`) REFERENCES `atleti` (`ID_Atleta`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
