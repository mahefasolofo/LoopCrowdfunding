-- phpMyAdmin SQL Dump
-- version 5.1.3
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1
-- Généré le : mer. 15 juin 2022 à 06:57
-- Version du serveur : 10.4.24-MariaDB
-- Version de PHP : 7.4.28

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

CREATE TABLE `categorie` (
  `id_categ` int(11) NOT NULL,
  `nom_categ` varchar(20) NOT NULL,
  `mot_cle` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `categorie`
--

INSERT INTO `categorie` (`id_categ`, `nom_categ`, `mot_cle`) VALUES
(1, 'Immobilier', 'maison'),
(2, 'Agriculture', 'plantation'),
(3, 'Nouvelle technologie', 'informatique'),
(4, 'Social', 'social'),
(5, 'Environnement', NULL),
(6, 'Education', NULL);

-- --------------------------------------------------------

--
-- Structure de la table `client`
--

CREATE TABLE `client` (
  `fk_id_user` int(11) NOT NULL,
  `datenaissance` date NOT NULL,
  `adresse` varchar(50) NOT NULL,
  `ville` varchar(50) NOT NULL,
  `pays` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `investissement_projet`
--

CREATE TABLE `investissement_projet` (
  `fk_id_investissement` int(11) NOT NULL,
  `fk_id_projet` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `investisssement`
--

CREATE TABLE `investisssement` (
  `ID_investissement` int(11) NOT NULL,
  `fk_id_user_invest` int(11) NOT NULL,
  `montant_investissement` float NOT NULL,
  `date_investissement` datetime NOT NULL DEFAULT current_timestamp(),
  `fk_ID_paiement_invest` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `moyenpaiement`
--

CREATE TABLE `moyenpaiement` (
  `ID_paiement` int(11) NOT NULL,
  `type_payement` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `notification`
--

CREATE TABLE `notification` (
  `id_notif` int(11) NOT NULL,
  `contenu` text NOT NULL,
  `dateTime` datetime NOT NULL DEFAULT current_timestamp(),
  `fk_id_user_notif` int(11) NOT NULL,
  `statu_notif` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `projet`
--

CREATE TABLE `projet` (
  `ID_projet` int(11) NOT NULL,
  `fk_id_user_projet` int(11) NOT NULL,
  `fk_id_categ_projet` int(11) NOT NULL,
  `titreprojet` text NOT NULL,
  `descriptionProjet` text NOT NULL,
  `sommeCagnotte` float DEFAULT NULL,
  `objectifCagnotte` float NOT NULL,
  `fichierBP` varchar(250) DEFAULT NULL,
  `pieceidentite` varchar(250) DEFAULT NULL,
  `statut` enum('Ouvert','Validé','Fermé','Annulé') NOT NULL DEFAULT 'Ouvert',
  `date_ouverture_cagnotte` datetime NOT NULL DEFAULT current_timestamp(),
  `date_fermeture_cagnotte` date NOT NULL,
  `date_debut_paiement` date NOT NULL,
  `image_projet` varchar(350) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `projet`
--

INSERT INTO `projet` (`ID_projet`, `fk_id_user_projet`, `fk_id_categ_projet`, `titreprojet`, `descriptionProjet`, `sommeCagnotte`, `objectifCagnotte`, `fichierBP`, `pieceidentite`, `statut`, `date_ouverture_cagnotte`, `date_fermeture_cagnotte`, `date_debut_paiement`, `image_projet`) VALUES
(33, 1, 6, 'Farara tokana', 'Ecole de musique sociale dans la ville d\'Antananarivo', NULL, 12600, 'dossiers\\BP\\business plan projet farara.txt', 'dossiers\\CIN\\CIN farara.jpg', 'Ouvert', '2022-06-15 07:51:00', '2022-09-01', '2022-09-30', 'dossiers\\IMG\\rafael-leao-k4WJjF0-3Yo-unsplash.jpg'),
(34, 1, 4, 'Tafaradia Madagascar', 'Agence de voyage engagé socialement, éco-responsable pour visiter la Grande île', NULL, 23000, 'dossiers\\BP\\business plan projet tafaradia.txt', 'dossiers\\CIN\\CIN tafaradia.jpg', 'Ouvert', '2022-06-15 07:55:05', '2022-09-02', '2022-12-26', 'dossiers\\IMG\\ramon-kagie-4EZKcKxOlRE-unsplash.jpg'),
(35, 1, 5, 'pot 2 fleur', 'Fabrication de pot de fleur à partir de plastique recyclé', NULL, 6000, 'dossiers\\BP\\POT DE FLEUR.txt', 'dossiers\\CIN\\CIN pot de fleur.jpg', 'Ouvert', '2022-06-15 07:56:31', '2022-09-05', '2022-11-03', 'dossiers\\IMG\\egor-myznik-aIJmMwkuzQ4-unsplash (1).jpg');

-- --------------------------------------------------------

--
-- Structure de la table `projet_suivi`
--

CREATE TABLE `projet_suivi` (
  `id_user_suiveur` int(11) NOT NULL,
  `id_projet_suivi` int(11) NOT NULL,
  `description_projet` text NOT NULL,
  `sommeCagnotte` float NOT NULL,
  `titreprojet` varchar(30) NOT NULL,
  `objectifCagnotte` float NOT NULL,
  `fichierBP` blob NOT NULL,
  `pieceidentite` blob NOT NULL,
  `statut` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `role`
--

CREATE TABLE `role` (
  `id_role` int(11) NOT NULL,
  `nom_role` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

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

CREATE TABLE `users` (
  `ID_user` int(10) NOT NULL,
  `nom` varchar(50) NOT NULL,
  `prenom` varchar(50) NOT NULL,
  `email` varchar(50) NOT NULL,
  `motdepasse` varchar(50) NOT NULL,
  `fk_id_role_user` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `users`
--

INSERT INTO `users` (`ID_user`, `nom`, `prenom`, `email`, `motdepasse`, `fk_id_role_user`) VALUES
(1, 'Ramiandrisoa', 'Antsa', 'taovina1205@gmail.com', 'antsa12', 2),
(3, 'Rakotonandrasana', 'Jenny', 'jennynandrasana@gmail.com', '123456nandrasana', 2),
(4, 'Rasolofonirina', 'Mahefa', 'rasolofo@gmail.com', '654321', 1),
(5, 'Rakoto', 'Jean', 'jeanrakoto@gmail.com', 'Passe', 2),
(6, 'Rasolofonantenaina', 'Luc', 'luc.nantenaina@gmail.com', 'luckyluc', 2);

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `categorie`
--
ALTER TABLE `categorie`
  ADD PRIMARY KEY (`id_categ`);

--
-- Index pour la table `client`
--
ALTER TABLE `client`
  ADD KEY `fk_id_user` (`fk_id_user`);

--
-- Index pour la table `investissement_projet`
--
ALTER TABLE `investissement_projet`
  ADD KEY `fk_id_investissement` (`fk_id_investissement`),
  ADD KEY `fk_id_projet` (`fk_id_projet`);

--
-- Index pour la table `investisssement`
--
ALTER TABLE `investisssement`
  ADD PRIMARY KEY (`ID_investissement`),
  ADD KEY `fk_id_user_invest` (`fk_id_user_invest`),
  ADD KEY `fk_ID_paiement_invest` (`fk_ID_paiement_invest`);

--
-- Index pour la table `moyenpaiement`
--
ALTER TABLE `moyenpaiement`
  ADD PRIMARY KEY (`ID_paiement`);

--
-- Index pour la table `notification`
--
ALTER TABLE `notification`
  ADD PRIMARY KEY (`id_notif`),
  ADD KEY `fk_id_user_notif` (`fk_id_user_notif`);

--
-- Index pour la table `projet`
--
ALTER TABLE `projet`
  ADD PRIMARY KEY (`ID_projet`),
  ADD KEY `fk_id_user_projet` (`fk_id_user_projet`),
  ADD KEY `fk_id_categ_projet` (`fk_id_categ_projet`);

--
-- Index pour la table `projet_suivi`
--
ALTER TABLE `projet_suivi`
  ADD KEY `id_projet_suivi` (`id_projet_suivi`),
  ADD KEY `id_user_suiveur` (`id_user_suiveur`);

--
-- Index pour la table `role`
--
ALTER TABLE `role`
  ADD PRIMARY KEY (`id_role`);

--
-- Index pour la table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`ID_user`),
  ADD KEY `fk_id_role_user` (`fk_id_role_user`);

--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table `categorie`
--
ALTER TABLE `categorie`
  MODIFY `id_categ` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT pour la table `investisssement`
--
ALTER TABLE `investisssement`
  MODIFY `ID_investissement` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `moyenpaiement`
--
ALTER TABLE `moyenpaiement`
  MODIFY `ID_paiement` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `notification`
--
ALTER TABLE `notification`
  MODIFY `id_notif` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `projet`
--
ALTER TABLE `projet`
  MODIFY `ID_projet` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=36;

--
-- AUTO_INCREMENT pour la table `role`
--
ALTER TABLE `role`
  MODIFY `id_role` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT pour la table `users`
--
ALTER TABLE `users`
  MODIFY `ID_user` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

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
  ADD CONSTRAINT `fk_id_user_invest` FOREIGN KEY (`fk_id_user_invest`) REFERENCES `users` (`ID_user`);

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
