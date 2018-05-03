-- phpMyAdmin SQL Dump
-- version 4.1.4
-- http://www.phpmyadmin.net
--
-- Client :  127.0.0.1
-- Généré le :  Mar 17 Avril 2018 à 01:51
-- Version du serveur :  5.6.15-log
-- Version de PHP :  5.5.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de données :  `munarela`
--
CREATE DATABASE IF NOT EXISTS `munarela` DEFAULT CHARACTER SET utf8 COLLATE utf8_unicode_ci;
USE `munarela`;

-- --------------------------------------------------------

--
-- Structure de la table `adresse`
--

CREATE TABLE IF NOT EXISTS `adresse` (
  `idAdresse` int(11) NOT NULL AUTO_INCREMENT,
  `ville` varchar(100) NOT NULL,
  `codePostale` varchar(45) NOT NULL,
  `pays` varchar(100) NOT NULL,
  PRIMARY KEY (`idAdresse`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=48 ;

--
-- Contenu de la table `adresse`
--

INSERT INTO `adresse` (`idAdresse`, `ville`, `codePostale`, `pays`) VALUES
(44, '', '', ''),
(45, '', '', ''),
(46, '', '', ''),
(47, '', '', '');

-- --------------------------------------------------------

--
-- Structure de la table `banner`
--

CREATE TABLE IF NOT EXISTS `banner` (
  `banner_id` int(11) NOT NULL,
  `banner_title` varchar(200) NOT NULL,
  `banner_image` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `banner`
--

INSERT INTO `banner` (`banner_id`, `banner_title`, `banner_image`) VALUES
(1, 'mon reve', 'banner-1.jpg'),
(2, 'profite du paradis', 'banner-2.jpg'),
(3, 'explore d''autre horizons', 'banner-3.jpg'),
(4, 'Oublie', 'banner-4.jpg'),
(5, 'Caractere choix', 'banner-5.jpg'),
(6, 'Le choix de l''evasion', 'banner-6.jpg'),
(7, 'Un amour de voyage', 'banner-7.jpg'),
(8, 'J''adore mon escapase', 'banner-8.jpg');

-- --------------------------------------------------------

--
-- Structure de la table `circuit`
--

CREATE TABLE IF NOT EXISTS `circuit` (
  `idCircuit` int(11) NOT NULL AUTO_INCREMENT,
  `titre` varchar(45) NOT NULL,
  `dateDeDepart` timestamp(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  `dateDeRetour` timestamp(6) NOT NULL DEFAULT '0000-00-00 00:00:00.000000',
  `nbPersonnesMax` int(2) NOT NULL,
  `nbPersonnesMin` int(2) NOT NULL,
  `description` longtext NOT NULL,
  `prix` float NOT NULL,
  `imageCircuit` varchar(256) NOT NULL,
  `guide` varchar(256) DEFAULT NULL,
  `idPromotion` int(11) DEFAULT NULL,
  `idThematique` int(11) NOT NULL,
  `published` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`idCircuit`),
  KEY `idPromotion` (`idPromotion`),
  KEY `idThematique` (`idThematique`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=8 ;

--
-- Contenu de la table `circuit`
--

INSERT INTO `circuit` (`idCircuit`, `titre`, `dateDeDepart`, `dateDeRetour`, `nbPersonnesMax`, `nbPersonnesMin`, `description`, `prix`, `imageCircuit`, `guide`, `idPromotion`, `idThematique`, `published`) VALUES
(4, 'Circuit A', '2018-03-01 05:00:00.000000', '2018-03-02 05:00:00.000000', 2, 4, 'un premier circuit', 3111, 'pic', NULL, NULL, 1, 0),
(5, 'Circuit Australien', '2018-03-03 05:00:00.000000', '2018-03-15 04:00:00.000000', 1, 2, 'kangorouuu', 3999, 'aucune', NULL, NULL, 1, 0),
(6, 'un circuit', '2018-04-15 04:00:00.000000', '2018-04-22 04:00:00.000000', 20, 10, '<p>resume du circuit</p>\r\n', 2000, 'ad90a19282ed43d0732163ccfdb441a544018560.jpg', NULL, NULL, 1, 0),
(7, 'circuit 1', '2018-04-18 04:00:00.000000', '2018-04-30 04:00:00.000000', 20, 10, '<p>Kicheche Bush Camp se trouve dans la &ldquo;Olare Motorogi Conservancy&rdquo;, qui jouxte la r&eacute;serve du Masai Mara. C&rsquo;est un camp de toile de petite taille, intime et luxueux, situ&eacute; au beau milieu d&rsquo;une savane d&rsquo;acacias &agrave; &eacute;pines, sans aucune cl&ocirc;ture. La faune y est pr&eacute;sente partout...<br />\r\nLe camp se compose de six tentes pourvues de moustiquaires, avec eau courante froide au lavabo, toilettes avec chasse d&rsquo;eau et douche &ldquo;safari&rdquo;. Son impact sur l&rsquo;environnement est volontairement limit&eacute;. L&rsquo;&eacute;lectricit&eacute; est solaire et la lumi&egrave;re est disponible dans la tente &agrave; toute heure du jour et de la nuit.<br />\r\nBien que Kicheche soit un camp de toile, le confort n&rsquo;est pas compromis. La nourriture et le service sont de premi&egrave;re classe, les guides et le personnel en charge sont chaleureux et exp&eacute;riment&eacute;s, et constamment d&eacute;sireux de nous faire plaisir. Ce safari sera inoubliable...<br />\r\nLe nombre de camps et de lits &agrave; l&rsquo;int&eacute;rieur de la Conservancy est strictement limit&eacute;, ce qui nous procure une exp&eacute;rience incomparable en terme d&rsquo;observation, sans la foule habituellement pr&eacute;sente dans le reste de la r&eacute;serve.</p>\r\n', 2500, '84e69e6b254baca29b3407b2a8814ec2bb7b6d4f.jpg', NULL, NULL, 1, 0);

-- --------------------------------------------------------

--
-- Structure de la table `etape`
--

CREATE TABLE IF NOT EXISTS `etape` (
  `idEtape` int(11) NOT NULL AUTO_INCREMENT,
  `nomEtape` varchar(256) NOT NULL,
  `descriptionEtape` longtext NOT NULL,
  `idCircuit` int(11) NOT NULL,
  PRIMARY KEY (`idEtape`),
  KEY `idCircuit` (`idCircuit`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Contenu de la table `etape`
--

INSERT INTO `etape` (`idEtape`, `nomEtape`, `descriptionEtape`, `idCircuit`) VALUES
(1, 'premiere etapeun ', '<p>petit descriptif</p>\r\n', 6),
(2, 'Kicheche...', '<p>Bien que Kicheche soit un camp de toile, le confort n&rsquo;est pas compromis. La nourriture et le service sont de premi&egrave;re classe, les guides et le personnel en charge sont chaleureux et exp&eacute;riment&eacute;s, et constamment d&eacute;sireux de nous faire plaisir. Ce safari sera inoubliable...<br />\r\nLe nombre de camps et de lits &agrave; l&rsquo;int&eacute;rieur de la Conservancy est strictement limit&eacute;, ce qui nous procure une exp&eacute;rience incomparable en terme d&rsquo;observation, sans la foule habituellement pr&eacute;sente dans le reste de la r&eacute;serve.</p>\r\n', 7);

-- --------------------------------------------------------

--
-- Structure de la table `jour`
--

CREATE TABLE IF NOT EXISTS `jour` (
  `nomhotel` varchar(256) DEFAULT NULL,
  `urlHotel` varchar(100) DEFAULT NULL,
  `nomRestaurant` varchar(100) DEFAULT NULL,
  `urlRestaurant` varchar(100) DEFAULT NULL,
  `dejeuner` tinyint(1) NOT NULL DEFAULT '0',
  `diner` tinyint(1) NOT NULL DEFAULT '0',
  `souper` tinyint(1) NOT NULL DEFAULT '0',
  `descriptionJour` longtext,
  `idEtape` int(11) NOT NULL,
  KEY `idEtape` (`idEtape`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Contenu de la table `jour`
--

INSERT INTO `jour` (`nomhotel`, `urlHotel`, `nomRestaurant`, `urlRestaurant`, `dejeuner`, `diner`, `souper`, `descriptionJour`, `idEtape`) VALUES
('alo', 'balo', 'malo', 'talo', 0, 1, 0, '<p>un descriptif</p>\r\n', 1),
('aaa', 'aaa', 'cc', 'ccc', 0, 1, 1, '<p>Vol de Paris &agrave; Nairobi (direct ou via Amsterdam). R&eacute;cup&eacute;ration des bagages. Etablissement des visas. Nous sommes accueillis par Peter et Kevin, les guides de Kicheche, qui nous conduisent dans un h&ocirc;tel 4 &eacute;toiles avec leurs camionnettes.</p>\r\n', 2);

-- --------------------------------------------------------

--
-- Structure de la table `paiement`
--

CREATE TABLE IF NOT EXISTS `paiement` (
  `idPaiement` int(11) NOT NULL AUTO_INCREMENT,
  `datePaiment` timestamp(6) NULL DEFAULT NULL,
  `montanttPaiment` float DEFAULT NULL,
  `idReservation` int(11) NOT NULL,
  PRIMARY KEY (`idPaiement`),
  KEY `idReservation` (`idReservation`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Structure de la table `panier`
--

CREATE TABLE IF NOT EXISTS `panier` (
  `idPanier` int(11) NOT NULL AUTO_INCREMENT,
  `idCircuit` int(11) NOT NULL,
  `email` varchar(60) NOT NULL,
  PRIMARY KEY (`idPanier`),
  KEY `idCircuit` (`idCircuit`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Contenu de la table `panier`
--

INSERT INTO `panier` (`idPanier`, `idCircuit`, `email`) VALUES
(1, 5, 'asdf@gmail.com'),
(2, 7, 'jas@hotmail.com');

-- --------------------------------------------------------

--
-- Structure de la table `participants`
--

CREATE TABLE IF NOT EXISTS `participants` (
  `idparticipants` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(100) NOT NULL,
  `prenom` varchar(100) NOT NULL,
  `courriel` varchar(256) NOT NULL,
  `sexe` varchar(45) NOT NULL,
  `tel` varchar(100) NOT NULL,
  `idAdresse` int(11) NOT NULL,
  `idPasspor` int(11) NOT NULL,
  PRIMARY KEY (`idparticipants`),
  KEY `idAdresse` (`idAdresse`),
  KEY `idPasspor` (`idPasspor`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=43 ;

--
-- Contenu de la table `participants`
--

INSERT INTO `participants` (`idparticipants`, `nom`, `prenom`, `courriel`, `sexe`, `tel`, `idAdresse`, `idPasspor`) VALUES
(39, 'lala', 'nana', 'jas@hotmail.com', '', '', 44, 40),
(40, '1111', '', '', '', '', 45, 41),
(41, 'lala', 'nana', 'jas@hotmail.com', '', '', 46, 42),
(42, 'ddddddd', '', '', '', '', 47, 43);

-- --------------------------------------------------------

--
-- Structure de la table `passeport`
--

CREATE TABLE IF NOT EXISTS `passeport` (
  `idPasspor` int(11) NOT NULL AUTO_INCREMENT,
  `numeroPass` int(50) NOT NULL,
  `dateDelivPass` date NOT NULL,
  `dateExpiration` date NOT NULL,
  `nationalites` varchar(100) NOT NULL,
  `lieuDeliv` varchar(256) NOT NULL,
  PRIMARY KEY (`idPasspor`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=44 ;

--
-- Contenu de la table `passeport`
--

INSERT INTO `passeport` (`idPasspor`, `numeroPass`, `dateDelivPass`, `dateExpiration`, `nationalites`, `lieuDeliv`) VALUES
(40, 0, '0000-00-00', '0000-00-00', '', ''),
(41, 0, '0000-00-00', '0000-00-00', '', ''),
(42, 0, '0000-00-00', '0000-00-00', '', ''),
(43, 0, '0000-00-00', '0000-00-00', '', '');

-- --------------------------------------------------------

--
-- Structure de la table `promotion`
--

CREATE TABLE IF NOT EXISTS `promotion` (
  `idPromotion` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(256) CHARACTER SET utf32 COLLATE utf32_unicode_ci NOT NULL,
  `taux` float NOT NULL,
  `datedebut` date NOT NULL,
  `datefin` date NOT NULL,
  PRIMARY KEY (`idPromotion`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Structure de la table `reservation`
--

CREATE TABLE IF NOT EXISTS `reservation` (
  `idReservation` int(11) NOT NULL AUTO_INCREMENT,
  `montanTotal` float NOT NULL,
  `nbrPlaceDispo` int(11) NOT NULL,
  `nbrAdult` int(11) NOT NULL,
  `nbrEnfMTrois` int(11) NOT NULL,
  `NbrEnfBTroisONZ` int(11) NOT NULL,
  `datePaiment` timestamp(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  `idUtilisateur` int(11) NOT NULL,
  `idCircuit` int(11) NOT NULL,
  `solde` float NOT NULL,
  PRIMARY KEY (`idReservation`),
  UNIQUE KEY `idReservation` (`idReservation`,`montanTotal`,`idUtilisateur`,`idCircuit`),
  KEY `idCircuit` (`idCircuit`),
  KEY `idUtilisateur` (`idUtilisateur`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=17 ;

--
-- Contenu de la table `reservation`
--

INSERT INTO `reservation` (`idReservation`, `montanTotal`, `nbrPlaceDispo`, `nbrAdult`, `nbrEnfMTrois`, `NbrEnfBTroisONZ`, `datePaiment`, `idUtilisateur`, `idCircuit`, `solde`) VALUES
(15, 6222, 2, 1, 0, 0, '2018-04-13 05:34:11.000000', 5, 4, 6222),
(16, 7998, 2, 1, 0, 0, '2018-04-17 04:47:33.000000', 5, 5, 7998);

-- --------------------------------------------------------

--
-- Structure de la table `reservationparticipant`
--

CREATE TABLE IF NOT EXISTS `reservationparticipant` (
  `idParticipants` int(11) NOT NULL,
  `idReservation` int(11) NOT NULL,
  PRIMARY KEY (`idParticipants`,`idReservation`),
  KEY `idReservation` (`idReservation`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Contenu de la table `reservationparticipant`
--

INSERT INTO `reservationparticipant` (`idParticipants`, `idReservation`) VALUES
(39, 15),
(40, 15),
(41, 16),
(42, 16);

-- --------------------------------------------------------

--
-- Structure de la table `thematique`
--

CREATE TABLE IF NOT EXISTS `thematique` (
  `idThematique` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(45) NOT NULL,
  `description` longtext NOT NULL,
  `image` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`idThematique`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=7 ;

--
-- Contenu de la table `thematique`
--

INSERT INTO `thematique` (`idThematique`, `nom`, `description`, `image`) VALUES
(1, 'Oceanie', 'Tout ce qui est en oceanie', 'a0f1e055bde100cc799d036ede688d6b9ef8ee5f.jpg'),
(2, 'Pacifique', 'zfzz', '03e249731897c59c0c75e0cb0e9cc1fa3ef57403.jpg'),
(3, 'Europe', 'qszf', '3a85dba78f53862dbf6321135088987a61968b8b.jpg'),
(4, 'Afrique', 'qszf', '03434e9348bed1b93193ef2c29e9ed697a2d78cc.jpg'),
(5, 'Antilles', 'qszf', 'e5dfef216dc2aa856d78116320b8a5b959a5b2e1.jpg'),
(6, 'Capitales', 'qszf', '8fec20818bcb9334ebfee5e45dc3657f3185aaae.jpg');

-- --------------------------------------------------------

--
-- Structure de la table `utilisateur`
--

CREATE TABLE IF NOT EXISTS `utilisateur` (
  `idUtilisateur` int(11) NOT NULL AUTO_INCREMENT,
  `first_name` varchar(50) NOT NULL,
  `last_name` varchar(50) NOT NULL,
  `display_name` varchar(50) NOT NULL,
  `email` varchar(150) NOT NULL,
  `password` varchar(50) NOT NULL,
  `actif` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`idUtilisateur`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=14 ;

--
-- Contenu de la table `utilisateur`
--

INSERT INTO `utilisateur` (`idUtilisateur`, `first_name`, `last_name`, `display_name`, `email`, `password`, `actif`) VALUES
(1, 'Jay', 'Pabs', 'jaypabs', 'asdf@gmail.com', '202cb962ac59075b964b07152d234b70', 1),
(2, 'nadjib', 'larkem', 'nadj851', 'nadjiblar@hotmail.fr', '21232f297a57a5a743894a0e4a801fc3', 1),
(3, 'NONO', 'LOLO', 'nadj851', 'naar8@hotmail.com', '202cb962ac59075b964b07152d234b70', 1),
(4, 'admin', 'admin', 'admin', 'admin@admin.com', '21232f297a57a5a743894a0e4a801fc3', 1),
(5, 'nana', 'lala', 'jas', 'jas@hotmail.com', '202cb962ac59075b964b07152d234b70', 1),
(6, 'nadjib', 'larkem', 'nadjib', 'nadjib@hotmail.com', '202cb962ac59075b964b07152d234b70', 1),
(7, 'lara', 'mara', 'lara', 'lara@hotmail.com', '202cb962ac59075b964b07152d234b70', 1),
(8, 'haha', 'lala', 'lala', 'lala@hotmail.com', '202cb962ac59075b964b07152d234b70', 1),
(9, 'ahcen', 'boutaghane', 'haswen', 'eee@d.com', '96e79218965eb72c92a549dd5a330112', 1),
(10, 'nasd', 'dwdw', 'fwfwf', 'n@hotmail.fr33', '698d51a19d8a121ce581499d7b701668', 1),
(11, 'ahcen', 'boutaghane', 'ere', 'nadjiblar@h1otmaill.fr', '698d51a19d8a121ce581499d7b701668', 1),
(12, 'ahcen', 'boutaghane', 'haswen', 'nad111jiblar@hotmaill.fr', '698d51a19d8a121ce581499d7b701668', 1),
(13, '1', '1', '1', '1', 'c4ca4238a0b923820dcc509a6f75849b', 1);

--
-- Contraintes pour les tables exportées
--

--
-- Contraintes pour la table `circuit`
--
ALTER TABLE `circuit`
  ADD CONSTRAINT `circuit_ibfk_1` FOREIGN KEY (`idPromotion`) REFERENCES `promotion` (`idPromotion`) ON DELETE CASCADE,
  ADD CONSTRAINT `circuit_ibfk_2` FOREIGN KEY (`idThematique`) REFERENCES `thematique` (`idThematique`) ON DELETE CASCADE;

--
-- Contraintes pour la table `etape`
--
ALTER TABLE `etape`
  ADD CONSTRAINT `etape_ibfk_1` FOREIGN KEY (`idCircuit`) REFERENCES `circuit` (`idCircuit`) ON DELETE CASCADE;

--
-- Contraintes pour la table `jour`
--
ALTER TABLE `jour`
  ADD CONSTRAINT `jour_ibfk_1` FOREIGN KEY (`idEtape`) REFERENCES `etape` (`idEtape`) ON DELETE CASCADE;

--
-- Contraintes pour la table `paiement`
--
ALTER TABLE `paiement`
  ADD CONSTRAINT `paiement_ibfk_1` FOREIGN KEY (`idReservation`) REFERENCES `reservation` (`idReservation`);

--
-- Contraintes pour la table `panier`
--
ALTER TABLE `panier`
  ADD CONSTRAINT `panier_ibfk_1` FOREIGN KEY (`idCircuit`) REFERENCES `circuit` (`idCircuit`);

--
-- Contraintes pour la table `participants`
--
ALTER TABLE `participants`
  ADD CONSTRAINT `participants_ibfk_1` FOREIGN KEY (`idAdresse`) REFERENCES `adresse` (`idAdresse`) ON DELETE CASCADE,
  ADD CONSTRAINT `participants_ibfk_2` FOREIGN KEY (`idPasspor`) REFERENCES `passeport` (`idPasspor`) ON DELETE CASCADE;

--
-- Contraintes pour la table `reservation`
--
ALTER TABLE `reservation`
  ADD CONSTRAINT `reservation_ibfk_1` FOREIGN KEY (`idCircuit`) REFERENCES `circuit` (`idCircuit`),
  ADD CONSTRAINT `reservation_ibfk_2` FOREIGN KEY (`idUtilisateur`) REFERENCES `utilisateur` (`idUtilisateur`) ON UPDATE NO ACTION;

--
-- Contraintes pour la table `reservationparticipant`
--
ALTER TABLE `reservationparticipant`
  ADD CONSTRAINT `reservationparticipant_ibfk_1` FOREIGN KEY (`idParticipants`) REFERENCES `participants` (`idparticipants`),
  ADD CONSTRAINT `reservationparticipant_ibfk_2` FOREIGN KEY (`idReservation`) REFERENCES `reservation` (`idReservation`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
