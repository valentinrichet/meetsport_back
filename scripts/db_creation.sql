CREATE DATABASE IF NOT EXISTS `projet`
USE `projet`;

DROP TABLE IF EXISTS `EVENT_ATTENDEE`;
DROP TABLE IF EXISTS `EVENT_COMMENT`;
DROP TABLE IF EXISTS `EVENT_CHAT`;
DROP TABLE IF EXISTS `EVENT`;
DROP TABLE IF EXISTS `PLACE`;
DROP TABLE IF EXISTS `ATHLETIC_USER`;
DROP TABLE IF EXISTS `MOBILE_USER`;
DROP TABLE IF EXISTS `USER`;
DROP TABLE IF EXISTS `ROLE_CLAIM`;
DROP TABLE IF EXISTS `ROLE`;
DROP TABLE IF EXISTS `CLAIM`;

CREATE TABLE IF NOT EXISTS `ROLE` (
`id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
`name` tinytext NOT NULL,
PRIMARY KEY (`id`)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE IF NOT EXISTS `CLAIM` (
`id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
`name` tinytext NOT NULL,
PRIMARY KEY (`id`)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE IF NOT EXISTS `ROLE_CLAIM` (
`role` bigint(20) unsigned NOT NULL,
`claim` bigint(20) unsigned NOT NULL,
PRIMARY KEY (`role`,`claim`),
KEY `ROLE_CLAIM_key_role` (`role`),
CONSTRAINT `ROLE_CLAIM_fk_role` FOREIGN KEY (`role`) REFERENCES `ROLE` (`id`) ON DELETE CASCADE,
KEY `ROLE_CLAIM_key_claim` (`claim`),
CONSTRAINT `ROLE_CLAIM_fk_claim` FOREIGN KEY (`claim`) REFERENCES `CLAIM` (`id`) ON DELETE CASCADE
)ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE IF NOT EXISTS `USER` (
  `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `mail` varchar(320) NOT NULL,
  `hashed_password` text NOT NULL,
  `first_name` varchar(50) NOT NULL,
  `last_name` varchar(50) NOT NULL,
  `birthday` date NOT NULL,
  `role` bigint(20) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `USER_key_mail` (`mail`) USING HASH,
  KEY `USER_key_role` (`role`),
  CONSTRAINT `USER_fk_role` FOREIGN KEY (`role`) REFERENCES `ROLE` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE IF NOT EXISTS `ATHLETIC_USER` (
  `id` bigint(20) unsigned NOT NULL,
  `latitude` decimal(19, 16),
  `longitude` decimal(19, 16),
  `height` decimal(5, 2),
  `weight` decimal(3, 1),
  PRIMARY KEY (`id`),
  CONSTRAINT `ATHLETIC_USER_fk_id` FOREIGN KEY (`id`) REFERENCES `USER` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE IF NOT EXISTS `MOBILE_USER` (
  `id` bigint(20) unsigned NOT NULL,
  `firebase_token` tinytext,
  PRIMARY KEY (`id`),
  CONSTRAINT `MOBILE_USER_fk_id` FOREIGN KEY (`id`) REFERENCES `USER` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE IF NOT EXISTS `PLACE` (
`id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
`title` tinytext NOT NULL,
`adress` tinytext NOT NULL,
`city` tinytext NOT NULL,
`latitude` decimal(19, 16) NOT NULL,
`longitude` decimal(19, 16) NOT NULL,
PRIMARY KEY (`id`)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE IF NOT EXISTS `EVENT` (
`id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
`title` tinytext NOT NULL,
`description` tinytext NOT NULL,
`creator` bigint(20) unsigned NOT NULL,
`place` bigint(20) unsigned NOT NULL,
`date` date NOT NULL,
`likes` int unsigned NOT NULL,
`dislikes` int unsigned NOT NULL,
PRIMARY KEY (`id`),
KEY `EVENT_key_creator` (`creator`),
CONSTRAINT `EVENT_fk_creator` FOREIGN KEY (`creator`) REFERENCES `USER` (`id`) ON DELETE CASCADE,
KEY `EVENT_key_place` (`place`),
CONSTRAINT `EVENT_fk_place` FOREIGN KEY (`place`) REFERENCES `PLACE` (`id`) ON DELETE CASCADE
)ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE IF NOT EXISTS `EVENT_ATTENDEE` (
`event` bigint(20) unsigned NOT NULL,
`user` bigint(20) unsigned NOT NULL,
PRIMARY KEY (`event`,`user`),
KEY `EVENT_ATTENDEE_key_event` (`event`),
CONSTRAINT `EVENT_ATTENDEE_fk_event` FOREIGN KEY (`event`) REFERENCES `EVENT` (`id`) ON DELETE CASCADE,
KEY `EVENT_ATTENDEE_key_user` (`user`),
CONSTRAINT `EVENT_ATTENDEE_fk_user` FOREIGN KEY (`user`) REFERENCES `USER` (`id`) ON DELETE CASCADE
)ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE IF NOT EXISTS `EVENT_COMMENT` (
  `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `event` bigint(20) unsigned NOT NULL,
  `user` bigint(20) unsigned NOT NULL,
  `date` date NOT NULL,
  `comment` text NOT NULL,
  `likes` int unsigned NOT NULL,
  PRIMARY KEY (`id`),
  KEY `EVENT_COMMENT_key_event` (`event`),
  CONSTRAINT `EVENT_COMMENT_fk_event` FOREIGN KEY (`event`) REFERENCES `EVENT` (`id`) ON DELETE CASCADE,
  KEY `EVENT_COMMENT_key_user` (`user`),
  CONSTRAINT `EVENT_COMMENT_fk_user` FOREIGN KEY (`user`) REFERENCES `USER` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE IF NOT EXISTS `EVENT_CHAT` (
  `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `event` bigint(20) unsigned NOT NULL,
  `user` bigint(20) unsigned NOT NULL,
  `date` date NOT NULL,
  `message` text NOT NULL,
  PRIMARY KEY (`id`),
  KEY `EVENT_CHAT_key_event` (`event`),
  CONSTRAINT `EVENT_CHAT_fk_event` FOREIGN KEY (`event`) REFERENCES `EVENT` (`id`) ON DELETE CASCADE,
  KEY `EVENT_CHAT_key_user` (`user`),
  CONSTRAINT `EVENT_CHAT_fk_user` FOREIGN KEY (`user`) REFERENCES `USER` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;