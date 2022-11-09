-- MySQL dump 10.13  Distrib 8.0.23, for Win64 (x86_64)
--
-- Host: localhost    Database: petshop
-- ------------------------------------------------------
-- Server version	5.7.31

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `alojamento`
--

DROP TABLE IF EXISTS `alojamento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `alojamento` (
  `id_alojamento` int(11) NOT NULL AUTO_INCREMENT,
  `alojamento_descricao` varchar(60) NOT NULL,
  `alojamento_status` int(11) NOT NULL COMMENT 'column - 0 = ocupado, 1 = livre, 2 = esperando o dono.',
  PRIMARY KEY (`id_alojamento`)
) ENGINE=InnoDB AUTO_INCREMENT=3520 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `alojamento`
--

LOCK TABLES `alojamento` WRITE;
/*!40000 ALTER TABLE `alojamento` DISABLE KEYS */;
INSERT INTO `alojamento` VALUES (3510,'alojamento 2',0),(3511,'alojamento 3',1),(3512,'alojamento 4',1),(3513,'alojamento 1',0),(3514,'alojamento 5',1),(3515,'alojamento 6',1),(3516,'alojamento 7',1),(3517,'alojamento 8',1),(3518,'alojamento 9',1),(3519,'alojamento 10',1);
/*!40000 ALTER TABLE `alojamento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `animal`
--

DROP TABLE IF EXISTS `animal`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `animal` (
  `id_animal` int(11) NOT NULL AUTO_INCREMENT,
  `animal_nome` varchar(45) NOT NULL,
  `animal_especie` varchar(45) DEFAULT NULL,
  `animal_raca` varchar(45) DEFAULT NULL,
  `animal_fkcliente` int(11) NOT NULL,
  PRIMARY KEY (`id_animal`),
  KEY `animal_fkcliente_index` (`animal_fkcliente`),
  CONSTRAINT `animal_fkcliente` FOREIGN KEY (`animal_fkcliente`) REFERENCES `cliente` (`id_cliente`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `animal`
--

LOCK TABLES `animal` WRITE;
/*!40000 ALTER TABLE `animal` DISABLE KEYS */;
INSERT INTO `animal` VALUES (1,'Rex','cachorro','pitbull',1),(2,'Mel','cachorro','Poodle',2),(3,'Tom','gato','siamês',3);
/*!40000 ALTER TABLE `animal` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cliente`
--

DROP TABLE IF EXISTS `cliente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cliente` (
  `id_cliente` int(11) NOT NULL AUTO_INCREMENT,
  `cliente_nome` varchar(60) NOT NULL,
  `cliente_rua` varchar(60) NOT NULL,
  `cliente_bairro` varchar(60) NOT NULL,
  `cliente_numero` varchar(10) NOT NULL,
  `cliente_telefone` varchar(15) NOT NULL,
  PRIMARY KEY (`id_cliente`),
  KEY `idx_cliente_id_cliente` (`id_cliente`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cliente`
--

LOCK TABLES `cliente` WRITE;
/*!40000 ALTER TABLE `cliente` DISABLE KEYS */;
INSERT INTO `cliente` VALUES (1,'João','Rua teste','Bairro teste','120','87988694840'),(2,'Maria','Rua teste 2','Bairro teste 2','150','87988123432'),(3,'cliente fulano de tal','Rua 25 de março','Centro','45','8798812454');
/*!40000 ALTER TABLE `cliente` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prontuario`
--

DROP TABLE IF EXISTS `prontuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `prontuario` (
  `id_prontuario` int(11) NOT NULL AUTO_INCREMENT,
  `prontuario_motivo` varchar(45) NOT NULL,
  `prontuario_fkalojamento` int(11) NOT NULL,
  `prontuario_fkanimal` int(11) NOT NULL,
  `prontuario_estado` int(11) NOT NULL COMMENT 'column - 0 = em tratamento, 1 = se recuperando, 2 = recuperado.',
  PRIMARY KEY (`id_prontuario`),
  KEY `idx_alojamento` (`prontuario_fkalojamento`),
  KEY `idx_animal` (`prontuario_fkanimal`),
  CONSTRAINT `fk_alojamento` FOREIGN KEY (`prontuario_fkalojamento`) REFERENCES `alojamento` (`id_alojamento`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_animal` FOREIGN KEY (`prontuario_fkanimal`) REFERENCES `animal` (`id_animal`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prontuario`
--

LOCK TABLES `prontuario` WRITE;
/*!40000 ALTER TABLE `prontuario` DISABLE KEYS */;
INSERT INTO `prontuario` VALUES (2,'doença do carrapato',3513,1,0),(3,'Panleucopenia felina',3510,3,1),(4,'Parvovirose Canina',3511,2,2);
/*!40000 ALTER TABLE `prontuario` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-04-04 23:00:18
