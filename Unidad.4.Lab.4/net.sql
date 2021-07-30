# SQL Manager 2005 Lite for MySQL 3.7.0.1
# ---------------------------------------
# Host     : localhost
# Port     : 3306
# Database : net


SET FOREIGN_KEY_CHECKS=0;

DROP DATABASE IF EXISTS `net`;

CREATE DATABASE `net`
    CHARACTER SET 'utf8'
    COLLATE 'utf8_general_ci';

USE `net`;

#
# Structure for the `contactos` table : 
#

DROP TABLE IF EXISTS `contactos`;

CREATE TABLE `contactos` (
  `id` int(11) NOT NULL,
  `nombre` varchar(20) default NULL,
  `apellido` varchar(20) default NULL,
  `email` varchar(50) default NULL,
  `telefono` varchar(10) default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

#
# Data for the `contactos` table  (LIMIT 0,500)
#

INSERT INTO `contactos` (`id`, `nombre`, `apellido`, `email`, `telefono`) VALUES 
  (1,'Juana','De Blanco','jdblanco@gmail.com','411-1111'),
  (2,'Jose','Gonzales','johny_smithy@gmail.com','422-2222'),
  (3,'Rodrigo','Rodriguez','rodrirodri@gmail.com','433-3333');

COMMIT;

