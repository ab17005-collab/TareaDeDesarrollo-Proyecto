CREATE DATABASE `veterinariapatitasypelos` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
CREATE TABLE `citas` (
  `idCita` int unsigned NOT NULL AUTO_INCREMENT,
  `fechaCita` datetime NOT NULL,
  `diagnostico` varchar(255) DEFAULT NULL,
  `tratamiento` text,
  `idMascota` int unsigned NOT NULL,
  `idCliente` int unsigned NOT NULL,
  PRIMARY KEY (`idCita`),
  KEY `idx_citas_mascota` (`idMascota`),
  KEY `idx_citas_cliente` (`idCliente`),
  CONSTRAINT `fk_citas_clientes` FOREIGN KEY (`idCliente`) REFERENCES `clientes` (`idCliente`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_citas_mascotas` FOREIGN KEY (`idMascota`) REFERENCES `mascotas` (`idMascota`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
CREATE TABLE `clientes` (
  `idCliente` int unsigned NOT NULL AUTO_INCREMENT,
  `nombre` varchar(60) NOT NULL,
  `apellido` varchar(60) NOT NULL,
  `direccion` varchar(150) DEFAULT NULL,
  `telefono` varchar(15) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `fechaRegistro` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `motivoCita`  VARCHAR(100) NOT NULL,
  `sintomas`    TEXT         NULL,
  PRIMARY KEY (`idCliente`),
  UNIQUE KEY `uq_clientes_email` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
CREATE TABLE `mascotas` (
  `idMascota` int unsigned NOT NULL AUTO_INCREMENT,
  `nombre` varchar(60) NOT NULL,
  `especie` varchar(30) NOT NULL,
  `raza` varchar(60) DEFAULT NULL,
  `sexo` varchar(10) DEFAULT NULL,
  `fechaNacimiento` date DEFAULT NULL,
  `idCliente` int unsigned NOT NULL,
  PRIMARY KEY (`idMascota`),
  KEY `idx_mascotas_cliente` (`idCliente`),
  CONSTRAINT `fk_mascotas_clientes` FOREIGN KEY (`idCliente`) REFERENCES `clientes` (`idCliente`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
