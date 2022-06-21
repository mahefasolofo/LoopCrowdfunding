-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le : mar. 21 juin 2022 à 07:45
-- Version du serveur : 5.7.36
-- Version de PHP : 7.4.26

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `crowdfunding`
--

-- --------------------------------------------------------

--
-- Structure de la table `categorie`
--

DROP TABLE IF EXISTS `categorie`;
CREATE TABLE IF NOT EXISTS `categorie` (
  `id_categ` int(11) NOT NULL AUTO_INCREMENT,
  `nom_categ` varchar(20) NOT NULL,
  `mot_cle` text,
  PRIMARY KEY (`id_categ`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `categorie`
--

INSERT INTO `categorie` (`id_categ`, `nom_categ`, `mot_cle`) VALUES
(1, 'Immobilier', 'maison'),
(2, 'Agriculture', 'plantation'),
(3, 'Nouvelle technologie', 'informatique'),
(4, 'Solidaire', 'solidaire'),
(5, 'Ecologie', NULL),
(6, 'Santé', NULL),
(10, 'Art', 'artiste, artistique, art, oeuvre'),
(11, 'Autre', NULL);

-- --------------------------------------------------------

--
-- Structure de la table `client`
--

DROP TABLE IF EXISTS `client`;
CREATE TABLE IF NOT EXISTS `client` (
  `fk_id_user` int(11) NOT NULL,
  `ID_client` int(11) NOT NULL AUTO_INCREMENT,
  `date_client` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID_client`),
  KEY `fk_id_user` (`fk_id_user`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `investissement_projet`
--

DROP TABLE IF EXISTS `investissement_projet`;
CREATE TABLE IF NOT EXISTS `investissement_projet` (
  `fk_id_investissement` int(11) NOT NULL,
  `fk_id_projet` int(11) NOT NULL,
  KEY `fk_id_investissement` (`fk_id_investissement`),
  KEY `fk_id_projet` (`fk_id_projet`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `investisssement`
--

DROP TABLE IF EXISTS `investisssement`;
CREATE TABLE IF NOT EXISTS `investisssement` (
  `ID_investissement` int(11) NOT NULL AUTO_INCREMENT,
  `fk_id_user_invest` int(11) NOT NULL,
  `montant_investissement` float NOT NULL,
  `date_investissement` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `fk_ID_paiement_invest` int(11) NOT NULL,
  `reference_projet` int(11) NOT NULL,
  PRIMARY KEY (`ID_investissement`),
  KEY `fk_id_user_invest` (`fk_id_user_invest`),
  KEY `fk_ID_paiement_invest` (`fk_ID_paiement_invest`),
  KEY `reference_projet` (`reference_projet`)
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `investisssement`
--

INSERT INTO `investisssement` (`ID_investissement`, `fk_id_user_invest`, `montant_investissement`, `date_investissement`, `fk_ID_paiement_invest`, `reference_projet`) VALUES
(26, 9, 2000, '2022-06-19 23:28:36', 1, 38),
(27, 9, 6000, '2022-06-20 11:04:48', 1, 36);

--
-- Déclencheurs `investisssement`
--
DROP TRIGGER IF EXISTS `update montant`;
DELIMITER $$
CREATE TRIGGER `update montant` AFTER INSERT ON `investisssement` FOR EACH ROW UPDATE projet 
INNER JOIN investisssement
SET projet.sommeCagnotte = New.montant_investissement+projet.sommeCagnotte 
WHERE projet.ID_projet=New.reference_projet
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Structure de la table `moyenpaiement`
--

DROP TABLE IF EXISTS `moyenpaiement`;
CREATE TABLE IF NOT EXISTS `moyenpaiement` (
  `ID_paiement` int(11) NOT NULL AUTO_INCREMENT,
  `type_payement` varchar(20) NOT NULL,
  PRIMARY KEY (`ID_paiement`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `moyenpaiement`
--

INSERT INTO `moyenpaiement` (`ID_paiement`, `type_payement`) VALUES
(1, 'Visa'),
(2, 'Payoneer'),
(3, 'AmericanExpress');

-- --------------------------------------------------------

--
-- Structure de la table `notification`
--

DROP TABLE IF EXISTS `notification`;
CREATE TABLE IF NOT EXISTS `notification` (
  `id_notif` int(11) NOT NULL AUTO_INCREMENT,
  `contenu` text NOT NULL,
  `dateTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `fk_id_user_notif` int(11) NOT NULL,
  `statu_notif` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id_notif`),
  KEY `fk_id_user_notif` (`fk_id_user_notif`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `projet`
--

DROP TABLE IF EXISTS `projet`;
CREATE TABLE IF NOT EXISTS `projet` (
  `ID_projet` int(11) NOT NULL AUTO_INCREMENT,
  `fk_id_user_projet` int(11) NOT NULL,
  `fk_id_categ_projet` int(11) NOT NULL,
  `titreprojet` text NOT NULL,
  `descriptionProjet` text NOT NULL,
  `sommeCagnotte` float DEFAULT '0',
  `objectifCagnotte` float NOT NULL,
  `fichierBP` varchar(250) DEFAULT NULL,
  `pieceidentite` varchar(250) DEFAULT NULL,
  `statut` enum('Ouvert','Validé','Fermé','Annulé') NOT NULL DEFAULT 'Ouvert',
  `date_ouverture_cagnotte` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `date_fermeture_cagnotte` date NOT NULL,
  `date_debut_paiement` date NOT NULL,
  `image_projet` varchar(350) NOT NULL,
  PRIMARY KEY (`ID_projet`),
  KEY `fk_id_user_projet` (`fk_id_user_projet`),
  KEY `fk_id_categ_projet` (`fk_id_categ_projet`)
) ENGINE=InnoDB AUTO_INCREMENT=39 DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `projet`
--

INSERT INTO `projet` (`ID_projet`, `fk_id_user_projet`, `fk_id_categ_projet`, `titreprojet`, `descriptionProjet`, `sommeCagnotte`, `objectifCagnotte`, `fichierBP`, `pieceidentite`, `statut`, `date_ouverture_cagnotte`, `date_fermeture_cagnotte`, `date_debut_paiement`, `image_projet`) VALUES
(36, 1, 10, 'Paradice Studio', 'Welcome to Paradice Studio,  a retro portrait studio in the heart of Hollywood. Remember the 80s? So do we. Okay fine, we remember the 90s & the 00s, but we promise you, we\'ve done our research. You thought glamour shots and mall portraits were a thing of the past? At Paradice Studio, the past is now. We\'re passionate about retro aesthetics, nostalgia, and preserving memories--so we\'re bringing back the retro glamour shots & mall portraits of yesteryear. Paradice Studio is for fans of nostalgia, Trapper Keepers, dress-up, time-travel, side ponies, Walkmans, disco balls, & cinematic portraits. Paradice is unique because it\'s a one-stop shop that not only offers retro portrait sessions, but supplies props & accessories, & delivers professionally edited photos ready for print. Like any small business, we\'ve started, well, small--but we want to open our doors to the public &  hit the ground doing the running man when we do (how cringe was that?!). Our funding goal accounts for upgrading our current studio equipment so that we can operate at maximum efficiency and quality.  It\'ll also allow us to transform our Hollywood studio into a space that fully represents & embodies the idea of Paradice. We think people will be just as passionate about this project as we are, and because of that, we want to offer our clients a vast variety of backdrops, props & accessories that are available from the very first session onward. If we meet our funding goal, we\'ll immediately get to work on upgrading our equipment & overhauling our Hollywood studio so that it\'s ready for the public in the fall of 2022. A lot of the studio overhaul will be done by myself, accompanied by a small team, and we\'re committed to meeting our deadlines. However, it\'s possible that the supply chain shortages & shipping delays will pose some setbacks. We will keep communication open & be transparent about any delays we might experience.', 6000, 70000, 'dossiers\\BP\\paradice studio.pdf', 'dossiers\\CIN\\paradice_studio.PNG', 'Ouvert', '2022-06-19 12:32:04', '2024-01-01', '2025-01-01', 'dossiers\\IMG\\cin_paradice_studio.PNG'),
(37, 1, 3, '8000Kicks | The waterproof HEMP backpacks', 'A minimalist backpack, with a ton of features. Designed and redesigned countless times, to fit all your daily needs while still being accessible, durable and waterproof. Here are 9 reasons why you need our hemp backpack in your everyday life. You might decide on your gear but you definitely don\'t decide on the weather.  And more likely than not, you will be carrying your laptop, tablet, books and other water-unfriendly stuff. Protecting your cargo with a waterproof backpack will be the best investment you ever made. It\'s smart enough for the office but roomy enough for everything else. This backpack will have the versatility you need for all your week\'s work essentials, including your laptop (16\'\', 15\'\' or 14\'\'), your tablet, your chargers, your gym gear and a lot more. Made for versatility. It ranges from a smooth size of 18L to a full 30L of space for all your weekend clothing. This is the backpack you need for all your weekend getaways. What is the use of a backpack if it doesn\'t fit the airplane measurements? Our backpacks are made for airline carriers and all airports, 30L of free extra luggage that you can bring wherever you go. Built for maximum comfort on your back, all day long, up and downhill, during the summer or during the winter. One backpack for all your adventures. When we started 8000Kicks, we had one mission in mind, to disrupt the textile industries and show everyone it is possible to make great products by using eco-friendly fibers like hemp instead of other incumbent polluting ones. And that\'s precisely what we have done by building a carbon-neutral backpack.', 0, 90000, 'dossiers\\BP\\Hemp backpack.pdf', 'dossiers\\CIN\\cin_bagpack.PNG', 'Ouvert', '2022-06-19 15:44:17', '2024-01-01', '2025-01-01', 'dossiers\\IMG\\bagpack.PNG'),
(38, 1, 3, 'Neat Whiskey Glass Tumbler with Chilled Coaster', 'The Neat Whiskey Glass Tumbler and Chilled Coaster is designed to help chill and keep your favorite whiskey chilled without dilution of ice or water. Simply keep the Billet Aluminum Coaster in your freezer and pull it out when its time to pour your favorite whiskey.  The Neat Whiskey Glass Tumbler and Chilled Coaster is the result of countless hours of design, manufacturing and testing (the best part). All of the details have been considered, and it is designed to be a pleasure to use. Striving for a better way to chill whiskey and keep it chilled through the whole experience, the Whiskey Tumbler and Chilled Coaster is born. Thoughtful design, multiple prototypes and thorough testing all went into this launch. Ted has a broad background with service in the Marine Corps, an EMS degree and career experience in sales, marketing, and product development. Ted graduated from Embry Riddle with a B.S. in aeronautics. While aviation is a passion of his, Ted is also an avid outdoorsman and loves anything whiskey related. He especially loves finding ways to make whiskey consumption an enjoyable experience for all. Neat was born in the basement of his house (aka ‘man cave’) when he wanted to enjoy a glass of whiskey, neat, without dilution.', 2000, 80000, 'dossiers\\BP\\Neat Whiskey Glass Tumbler with Chilled Coaster.pdf', 'dossiers\\CIN\\cin_whiskey_glass.PNG', 'Ouvert', '2022-06-19 15:51:52', '2024-05-01', '2025-01-01', 'dossiers\\IMG\\whiskey_glass.PNG');

-- --------------------------------------------------------

--
-- Structure de la table `projet_a_valider`
--

DROP TABLE IF EXISTS `projet_a_valider`;
CREATE TABLE IF NOT EXISTS `projet_a_valider` (
  `ID_projet` int(11) NOT NULL AUTO_INCREMENT,
  `fk_id_porteur` int(11) NOT NULL,
  `fk_id_categorie` int(11) NOT NULL,
  `titreprojet` text NOT NULL,
  `descriptionProjet` text NOT NULL,
  `sommeCagnotte` float DEFAULT '0',
  `objectifCagnotte` float NOT NULL,
  `fichierBP` varchar(250) DEFAULT NULL,
  `pieceidentite` varchar(250) DEFAULT NULL,
  `statut` enum('Ouvert','Validé','Fermé','Annulé') NOT NULL DEFAULT 'Ouvert',
  `date_ouverture_cagnotte` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `date_fermeture_cagnotte` date NOT NULL,
  `date_debut_paiement` date NOT NULL,
  `image_projet` varchar(350) NOT NULL,
  PRIMARY KEY (`ID_projet`),
  KEY `fk_id_user_projet` (`fk_id_porteur`),
  KEY `fk_id_categ_projet` (`fk_id_categorie`)
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `projet_a_valider`
--

INSERT INTO `projet_a_valider` (`ID_projet`, `fk_id_porteur`, `fk_id_categorie`, `titreprojet`, `descriptionProjet`, `sommeCagnotte`, `objectifCagnotte`, `fichierBP`, `pieceidentite`, `statut`, `date_ouverture_cagnotte`, `date_fermeture_cagnotte`, `date_debut_paiement`, `image_projet`) VALUES
(39, 28, 11, 'Essaie', 'Essaie', 0, 17000, 'dossiers\\CIN\\cin_whiskey_glass.PNG', 'dossiers\\CIN\\cin_whiskey_glass.PNG', 'Ouvert', '2022-06-20 19:11:00', '2022-06-30', '2022-07-01', 'dossiers\\CIN\\cin_whiskey_glass.PNG');

-- --------------------------------------------------------

--
-- Structure de la table `projet_suivi`
--

DROP TABLE IF EXISTS `projet_suivi`;
CREATE TABLE IF NOT EXISTS `projet_suivi` (
  `id_user_suiveur` int(11) NOT NULL,
  `id_projet_suivi` int(11) NOT NULL,
  `Date_suivi` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  KEY `id_projet_suivi` (`id_projet_suivi`),
  KEY `id_user_suiveur` (`id_user_suiveur`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `role`
--

DROP TABLE IF EXISTS `role`;
CREATE TABLE IF NOT EXISTS `role` (
  `id_role` int(11) NOT NULL AUTO_INCREMENT,
  `nom_role` varchar(50) NOT NULL,
  PRIMARY KEY (`id_role`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `role`
--

INSERT INTO `role` (`id_role`, `nom_role`) VALUES
(1, 'administrateur'),
(2, 'user'),
(3, 'Super administrateur');

-- --------------------------------------------------------

--
-- Structure de la table `users`
--

DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `ID_user` int(10) NOT NULL AUTO_INCREMENT,
  `nom` varchar(50) NOT NULL,
  `prenom` varchar(50) NOT NULL,
  `email` varchar(50) NOT NULL,
  `motdepasse` varchar(50) NOT NULL,
  `fk_id_role_user` int(11) NOT NULL DEFAULT '2',
  `adresse` varchar(50) NOT NULL,
  `datenaissance` date NOT NULL,
  `ville` varchar(50) NOT NULL,
  `pays` varchar(50) NOT NULL,
  PRIMARY KEY (`ID_user`),
  KEY `fk_id_role_user` (`fk_id_role_user`)
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `users`
--

INSERT INTO `users` (`ID_user`, `nom`, `prenom`, `email`, `motdepasse`, `fk_id_role_user`, `adresse`, `datenaissance`, `ville`, `pays`) VALUES
(1, 'Ramiandrisoa', 'Antsa', 'toavina1205@gmail.com', 'antsa12', 1, '', '0000-00-00', '', ''),
(3, 'Rakotonandrasana', 'Jenny', 'jennynandrasana@gmail.com', '123456nandrasana', 2, '', '0000-00-00', '', ''),
(4, 'Rasolofonirina', 'Mahefa', 'mahefa.rasolofonirina@gmail.com', 'mahefa', 1, '', '0000-00-00', '', ''),
(5, 'Rakoto', 'Jean', 'jeanrakoto@gmail.com', 'Passe', 2, '', '0000-00-00', '', ''),
(6, 'Rasolofonantenaina', 'Luc', 'luc.nantenaina@gmail.com', 'luckyluc', 2, '', '0000-00-00', '', ''),
(9, 'ANDRIANARIJAONA', 'Antsa Felaniaina', 'toavina1205@gmail.com', '1205', 2, 'Anosipatrana', '1997-05-12', 'Antananarivo', 'Madagascar'),
(10, 'Martin', 'Léo ', 'leo.martin@gmail.com', 'leomartin', 2, 'Analakely', '1993-03-16', 'Antananarivo', 'Madagascar'),
(11, 'Lopez', 'Gabriel', 'gabriel.loper', 'gabriellopez', 2, 'Paris', '1992-06-14', 'France', 'France'),
(12, 'Robert', 'Julie', 'julie.robert@gmail.com', 'julierobert', 2, 'Anosizato', '1990-02-13', 'Antananarivo', 'Madagascar'),
(13, 'Muller', 'Evan', 'evan.muller@gmail.com', 'evan', 2, 'Manhattan', '1990-07-02', 'New-York', 'USA'),
(24, 'Roux', 'Soan', 'soan.roux@gmail.com', 'soaroux', 2, 'Itaosy', '1992-08-02', 'Antananarivo', 'Madagascar'),
(25, 'Perez', 'Marie', 'marie.perez@gmail.com', 'marieperez', 2, 'Tsaramandroso', '1985-05-10', 'Mahajanga', 'Madagascar'),
(26, 'Marty', 'Lucie', 'lucie.marty@gmail.com', 'luciemarty', 2, 'Tanambao', '1987-10-02', 'Toamasina', 'Madagascar'),
(27, 'Fontaine', 'Jean', 'jean.fontaine', 'jeanfontaine', 2, 'Joffre', '1992-10-08', 'Antsiranana', 'Madagascar'),
(28, 'Adam', 'Sohan', 'sohan.adam@gmail.com', 'sohanadam', 2, 'Lazaret', '1995-06-05', 'Antsiranana', 'Madagascar'),
(29, 'Brun', 'Sacha', 'sacha.brun@gmail.com', 'sachabrun', 2, 'Ramena', '1982-11-09', 'Antsiranana', 'Madagascar'),
(30, 'Rey', 'Noam', 'noam.rey@gmail.com', 'noamrey', 2, 'Faravohitra', '1984-07-25', 'Antananarivo', 'Madagascar'),
(31, 'Antoine', 'Charly', 'charly.antoine@gmail.com', 'charlyantoine', 2, 'Antaninarenina', '1991-10-09', 'Antananarivo', 'Madagascar'),
(32, 'Lecomte', 'Basille', 'basille.lecomte', 'basillelecomte', 2, 'Analakely', '1985-12-24', 'Mahajanga', 'Madagascar'),
(33, 'Hamon', 'Nancy', 'nancy.hamon@gmail.com', 'nancyhamon', 2, 'Ankerana', '1986-09-14', 'Antananarivo', 'Madagascar');

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `client`
--
ALTER TABLE `client`
  ADD CONSTRAINT `fk_id_user` FOREIGN KEY (`fk_id_user`) REFERENCES `users` (`ID_user`);

--
-- Contraintes pour la table `investissement_projet`
--
ALTER TABLE `investissement_projet`
  ADD CONSTRAINT `fk_id_investissement` FOREIGN KEY (`fk_id_investissement`) REFERENCES `investisssement` (`ID_investissement`),
  ADD CONSTRAINT `fk_id_projet` FOREIGN KEY (`fk_id_projet`) REFERENCES `projet` (`ID_projet`);

--
-- Contraintes pour la table `investisssement`
--
ALTER TABLE `investisssement`
  ADD CONSTRAINT `fk_ID_paiement_invest` FOREIGN KEY (`fk_ID_paiement_invest`) REFERENCES `moyenpaiement` (`ID_paiement`),
  ADD CONSTRAINT `fk_id_user_invest` FOREIGN KEY (`fk_id_user_invest`) REFERENCES `users` (`ID_user`),
  ADD CONSTRAINT `reference_projet` FOREIGN KEY (`reference_projet`) REFERENCES `projet` (`ID_projet`);

--
-- Contraintes pour la table `notification`
--
ALTER TABLE `notification`
  ADD CONSTRAINT `fk_id_user_notif` FOREIGN KEY (`fk_id_user_notif`) REFERENCES `users` (`ID_user`);

--
-- Contraintes pour la table `projet`
--
ALTER TABLE `projet`
  ADD CONSTRAINT `fk_id_categ_projet` FOREIGN KEY (`fk_id_categ_projet`) REFERENCES `categorie` (`id_categ`),
  ADD CONSTRAINT `fk_id_user_projet` FOREIGN KEY (`fk_id_user_projet`) REFERENCES `users` (`ID_user`);

--
-- Contraintes pour la table `projet_a_valider`
--
ALTER TABLE `projet_a_valider`
  ADD CONSTRAINT `fk_id_categorie` FOREIGN KEY (`fk_id_categorie`) REFERENCES `categorie` (`id_categ`),
  ADD CONSTRAINT `fk_id_porteur` FOREIGN KEY (`fk_id_porteur`) REFERENCES `users` (`ID_user`);

--
-- Contraintes pour la table `projet_suivi`
--
ALTER TABLE `projet_suivi`
  ADD CONSTRAINT `id_projet_suivi` FOREIGN KEY (`id_projet_suivi`) REFERENCES `projet` (`ID_projet`),
  ADD CONSTRAINT `id_user_suiveur` FOREIGN KEY (`id_user_suiveur`) REFERENCES `users` (`ID_user`);

--
-- Contraintes pour la table `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `fk_id_role_user` FOREIGN KEY (`fk_id_role_user`) REFERENCES `role` (`id_role`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
