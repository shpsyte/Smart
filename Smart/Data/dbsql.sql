-- MySQL dump 10.16  Distrib 10.3.8-MariaDB, for Win64 (AMD64)
--
-- Host: localhost    Database: Smart
-- ------------------------------------------------------
-- Server version	10.3.8-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(95) COLLATE latin1_general_ci NOT NULL,
  `ProductVersion` varchar(32) COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `accountbank`
--

DROP TABLE IF EXISTS `accountbank`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `accountbank` (
  `AccountBankId` int(11) NOT NULL AUTO_INCREMENT COMMENT 'PRIMARY KEY FOR This table - records',
  `BusinessEntityID` int(11) NOT NULL COMMENT 'Foreign key to BusinessEntity.BusinessEntityID, Used for Application Filter.',
  `Name` varchar(50) COLLATE latin1_general_ci NOT NULL COMMENT 'Name Of Bank',
  `Code` varchar(10) COLLATE latin1_general_ci NOT NULL COMMENT 'Universal CODE from bank',
  `Agency` varchar(50) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Agency Name',
  `DigitAgency` varchar(1) COLLATE latin1_general_ci DEFAULT NULL,
  `Account` varchar(50) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Account Code',
  `DigitAccount` varchar(1) COLLATE latin1_general_ci DEFAULT NULL,
  `Active` bit(1) DEFAULT b'1' COMMENT '0 = This Record no longer used. 1 = This Record is actively used.',
  PRIMARY KEY (`AccountBankId`),
  KEY `FK_accountbank_businessentity` (`BusinessEntityID`),
  FULLTEXT KEY `AccountBankFT` (`Name`,`Account`),
  CONSTRAINT `FK_accountbank_businessentity` FOREIGN KEY (`BusinessEntityID`) REFERENCES `businessentity` (`BusinessEntityID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='Contains Center of Cost using in Financial Type';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `address`
--

DROP TABLE IF EXISTS `address`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `address` (
  `AddressId` int(11) NOT NULL AUTO_INCREMENT COMMENT 'PRIMARY KEY FOR This table - records',
  `BusinessEntityID` int(11) NOT NULL COMMENT 'Foreign key to BusinessEntity.BusinessEntityID, Used for Application Filter.',
  `PostalCode` varchar(50) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'postal code for the street address.',
  `StreetAddress` varchar(150) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'First street address line.',
  `StreetAddressLine2` varchar(150) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Second street address line.',
  `StreetAddressLine3` varchar(150) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Thirt street address line.',
  `Number` varchar(20) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Number of Street Address',
  `District` varchar(50) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'District, Neighborhood, Zone,',
  `StateProvinceId` int(11) DEFAULT NULL COMMENT 'Foreign key to StateProvince.StateProvinceId',
  `StateProvinceName` varchar(150) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Name Of StateProvince from API',
  `CityId` int(11) DEFAULT NULL COMMENT 'Foreign key to City.CityID.',
  `CityName` varchar(150) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Name of City Name from API',
  `CityCode` varchar(150) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Code of City Name from API',
  `SpatialLocation` varchar(150) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Latitude and longitude of this address.',
  `Deleted` bit(1) DEFAULT b'0' COMMENT '0 = This record was not marked as deleted. 1 = This record was marked to deleted.',
  PRIMARY KEY (`AddressId`),
  KEY `FK_BusinessEntity_Address` (`BusinessEntityID`),
  KEY `FK_City_Address` (`CityId`),
  FULLTEXT KEY `Address` (`PostalCode`,`StreetAddress`,`StreetAddressLine2`,`StreetAddressLine3`),
  CONSTRAINT `FK_BusinessEntity_Address` FOREIGN KEY (`BusinessEntityID`) REFERENCES `businessentity` (`BusinessEntityID`) ON DELETE CASCADE,
  CONSTRAINT `FK_City_Address` FOREIGN KEY (`CityId`) REFERENCES `city` (`CityId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='Contains all address used in system. Link to many tables.';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `businessentity`
--

DROP TABLE IF EXISTS `businessentity`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `businessentity` (
  `BusinessEntityID` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime NOT NULL,
  `Email` varchar(255) COLLATE latin1_general_ci NOT NULL,
  `ExternalCode` varchar(40) COLLATE latin1_general_ci NOT NULL,
  `Name` varchar(50) COLLATE latin1_general_ci NOT NULL,
  `Validate` datetime DEFAULT NULL,
  `Active` bit(1) DEFAULT NULL,
  KEY `BusinessEntityPK` (`BusinessEntityID`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `categoryfinancial`
--

DROP TABLE IF EXISTS `categoryfinancial`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `categoryfinancial` (
  `ChartAccountId` int(11) NOT NULL AUTO_INCREMENT COMMENT 'PRIMARY KEY FOR This table - records',
  `BusinessEntityID` int(11) NOT NULL COMMENT 'Foreign key to BusinessEntity.BusinessEntityID, Used for Application Filter.',
  `Name` varchar(50) COLLATE latin1_general_ci NOT NULL COMMENT 'Name Of Operation',
  `Type` int(11) NOT NULL,
  `Active` bit(1) NOT NULL DEFAULT b'1' COMMENT '0 = This Record no longer used. 1 = This Record is actively used.',
  PRIMARY KEY (`ChartAccountId`),
  KEY `FK_categoryfinancial_businessentity` (`BusinessEntityID`),
  CONSTRAINT `FK_categoryfinancial_businessentity` FOREIGN KEY (`BusinessEntityID`) REFERENCES `businessentity` (`BusinessEntityID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='Contains chart account using in Finantial Categories';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `categoryperson`
--

DROP TABLE IF EXISTS `categoryperson`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `categoryperson` (
  `CategoryId` int(11) NOT NULL AUTO_INCREMENT,
  `BusinessEntityId` int(11) NOT NULL,
  `Name` varchar(80) COLLATE latin1_general_ci NOT NULL,
  `CreateDate` date NOT NULL,
  PRIMARY KEY (`CategoryId`),
  KEY `BusinessEntityFK` (`BusinessEntityId`),
  CONSTRAINT `BusinessEntityFK` FOREIGN KEY (`BusinessEntityId`) REFERENCES `businessentity` (`BusinessEntityID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `categoryproduct`
--

DROP TABLE IF EXISTS `categoryproduct`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `categoryproduct` (
  `CategoryId` int(11) NOT NULL AUTO_INCREMENT COMMENT 'PRIMARY KEY FOR This table - records',
  `BusinessEntityID` int(11) NOT NULL COMMENT 'Foreign key to BusinessEntity.BusinessEntityID, Used for Application Filter.',
  `Name` varchar(120) COLLATE latin1_general_ci NOT NULL COMMENT 'Name Of Category',
  PRIMARY KEY (`CategoryId`),
  KEY `FK_categoryproduct_businessentity` (`BusinessEntityID`),
  FULLTEXT KEY `CategoryProduct` (`Name`),
  CONSTRAINT `FK_categoryproduct_businessentity` FOREIGN KEY (`BusinessEntityID`) REFERENCES `businessentity` (`BusinessEntityID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='Category Using in Product';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `city`
--

DROP TABLE IF EXISTS `city`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `city` (
  `CityId` int(11) NOT NULL AUTO_INCREMENT COMMENT 'PRIMARY KEY FOR This table - records',
  `BusinessEntityID` int(11) NOT NULL COMMENT 'Foreign key to BusinessEntity.BusinessEntityID, Used for Application Filter.',
  `Name` varchar(80) COLLATE latin1_general_ci NOT NULL COMMENT 'Name Of City',
  `MiddleName` varchar(50) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Midle name of City',
  `SpecialCodeRegion` varchar(80) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Special Code using in Governament',
  `StateProvinceId` int(11) DEFAULT NULL COMMENT 'Foreign key to StateProvince',
  PRIMARY KEY (`CityId`),
  KEY `CityStateProvince` (`StateProvinceId`),
  KEY `FK_city_businessentity` (`BusinessEntityID`),
  FULLTEXT KEY `CityIndex` (`Name`,`MiddleName`),
  CONSTRAINT `CityStateProvince` FOREIGN KEY (`StateProvinceId`) REFERENCES `stateprovince` (`StateProvinceId`) ON DELETE CASCADE,
  CONSTRAINT `FK_city_businessentity` FOREIGN KEY (`BusinessEntityID`) REFERENCES `businessentity` (`BusinessEntityID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='Cities';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `costcenter`
--

DROP TABLE IF EXISTS `costcenter`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `costcenter` (
  `CostCenterId` int(11) NOT NULL AUTO_INCREMENT COMMENT 'PRIMARY KEY FOR This table - records',
  `BusinessEntityID` int(11) NOT NULL COMMENT 'Foreign key to BusinessEntity.BusinessEntityID, Used for Application Filter.',
  `Name` varchar(50) COLLATE latin1_general_ci NOT NULL COMMENT 'Name Of CostCenter',
  `Active` bit(1) NOT NULL DEFAULT b'1' COMMENT '0 = This Record no longer used. 1 = This Record is actively used.',
  PRIMARY KEY (`CostCenterId`),
  KEY `FK_costcenter_businessentity` (`BusinessEntityID`),
  CONSTRAINT `FK_costcenter_businessentity` FOREIGN KEY (`BusinessEntityID`) REFERENCES `businessentity` (`BusinessEntityID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='Contains Center of Cost using in Financial Type';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `country`
--

DROP TABLE IF EXISTS `country`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `country` (
  `CountryId` int(11) NOT NULL AUTO_INCREMENT COMMENT 'PRIMARY KEY FOR This table - records',
  `BusinessEntityID` int(11) NOT NULL COMMENT 'Foreign key to BusinessEntity.BusinessEntityID, Used for Application Filter.',
  `Name` varchar(80) COLLATE latin1_general_ci NOT NULL COMMENT 'Name Of Country/Terretory',
  `MiddleName` varchar(30) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Midle name of Terretory',
  `CountryRegionCode` varchar(6) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'ISO standard country or region code. Foreign key to CountryRegion.CountryRegionCode.',
  `SpecialCodeRegion` varchar(10) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Special Code Country using any ',
  PRIMARY KEY (`CountryId`),
  KEY `FK_country_businessentity` (`BusinessEntityID`),
  FULLTEXT KEY `CountryFTIndex` (`Name`,`MiddleName`),
  CONSTRAINT `FK_country_businessentity` FOREIGN KEY (`BusinessEntityID`) REFERENCES `businessentity` (`BusinessEntityID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='Contains Countries';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `location`
--

DROP TABLE IF EXISTS `location`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `location` (
  `WarehouseId` int(11) NOT NULL AUTO_INCREMENT COMMENT 'PRIMARY KEY FOR This table - records',
  `BusinessEntityID` int(11) NOT NULL COMMENT 'Foreign key to BusinessEntity.BusinessEntityID, Used for Application Filter.',
  `Name` varchar(120) COLLATE latin1_general_ci NOT NULL COMMENT 'Name Of Location',
  `DefaultLocation` bit(1) NOT NULL DEFAULT b'0' COMMENT 'Indicate for defautl location for auto insert',
  PRIMARY KEY (`WarehouseId`),
  KEY `FK_location_businessentity` (`BusinessEntityID`),
  FULLTEXT KEY `Location` (`Name`),
  CONSTRAINT `FK_location_businessentity` FOREIGN KEY (`BusinessEntityID`) REFERENCES `businessentity` (`BusinessEntityID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='Classified using in product';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `paymentcondition`
--

DROP TABLE IF EXISTS `paymentcondition`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `paymentcondition` (
  `ConditionId` int(11) NOT NULL AUTO_INCREMENT COMMENT 'PRIMARY KEY FOR This table - records',
  `BusinessEntityID` int(11) NOT NULL COMMENT 'Foreign key to BusinessEntity.BusinessEntityID, Used for Application Filter.',
  `Name` varchar(50) COLLATE latin1_general_ci NOT NULL COMMENT 'Name Of payment',
  `PaymentQty` int(11) DEFAULT NULL COMMENT 'Qty Payments',
  `PaymentDays` int(11) DEFAULT NULL COMMENT 'Interval of Days in payments',
  `Tax` decimal(5,2) DEFAULT NULL COMMENT 'Tax for use this payment',
  `PaymentUse` int(11) DEFAULT NULL COMMENT 'Use in: 0 = All; 1 = Sales; 2 = Purchase;',
  `Active` bit(1) NOT NULL DEFAULT b'1' COMMENT '0 = This Record no longer used. 1 = This Record is actively used.',
  `Deleted` bit(1) NOT NULL DEFAULT b'0' COMMENT '0 = This record was not marked as deleted. 1 = This record was marked to deleted.',
  PRIMARY KEY (`ConditionId`),
  KEY `FK_BusinessEntity_PaymentCondition` (`BusinessEntityID`),
  FULLTEXT KEY `PaymentCondition` (`Name`),
  CONSTRAINT `FK_BusinessEntity_PaymentCondition` FOREIGN KEY (`BusinessEntityID`) REFERENCES `businessentity` (`BusinessEntityID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='Containts all condition for Payment';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `person`
--

DROP TABLE IF EXISTS `person`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `person` (
  `PersonId` int(11) NOT NULL AUTO_INCREMENT COMMENT 'PRIMARY KEY FOR This table - records',
  `BusinessEntityID` int(11) NOT NULL COMMENT 'Foreign key to BusinessEntity.BusinessEntityID, Used for Application Filter.',
  `PersonCode` int(11) NOT NULL COMMENT 'Sequence CODE for BusinessEntity',
  `FirstName` varchar(120) COLLATE latin1_general_ci NOT NULL COMMENT 'First name of the person.',
  `LastName` varchar(80) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Last name of the person.',
  `RegistrationCode` varchar(150) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Code for registration code country like VATNUMBER or CNPJ',
  `RegistrationState` varchar(150) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Code for registration province like IE',
  `Type` int(11) DEFAULT 0 COMMENT 'Type Person 0 = Company, 1 = Person',
  `PersonType` int(11) DEFAULT 0 COMMENT 'Primary type of person: 0 = Store Contact, 1 = Individual (retail) customer, 2 = Sales person, 3 = Employee (non-sales), 4 = Vendor contact, 5 = General contact',
  `CategoryId` int(11) DEFAULT NULL COMMENT 'dbo.PersonCategory.PersonCategoryIdCategory for this Person. ForenKey to PersonCategory',
  `Email` varchar(255) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Primary Email',
  `Phone` varchar(50) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Primary Phone for Person',
  `Image` longblob DEFAULT NULL COMMENT 'Image of the person.',
  `Comments` longtext COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Comments/Description for this person',
  `CreateDate` datetime NOT NULL DEFAULT current_timestamp() COMMENT 'Date and time the record was create.',
  `ModifiedDate` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp() COMMENT 'Date and time the record was last updated.',
  `Active` bit(1) DEFAULT b'1' COMMENT '0 = This Record no longer used. 1 = This Record is actively used.',
  `Deleted` bit(1) DEFAULT b'0' COMMENT '0 = This record was not marked as deleted. 1 = This record was marked to deleted.',
  PRIMARY KEY (`PersonId`),
  KEY `FK_BusinessEntity_Person` (`BusinessEntityID`),
  KEY `FK_BusinessEntity_Category` (`CategoryId`),
  FULLTEXT KEY `Person` (`FirstName`,`LastName`),
  CONSTRAINT `FK_BusinessEntity_Category` FOREIGN KEY (`CategoryId`) REFERENCES `categoryperson` (`CategoryId`) ON DELETE CASCADE,
  CONSTRAINT `FK_BusinessEntity_Person` FOREIGN KEY (`BusinessEntityID`) REFERENCES `businessentity` (`BusinessEntityID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='Human beings involved with AdventureWorks: employees, customer contacts, and vendor contacts.';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `personaddress`
--

DROP TABLE IF EXISTS `personaddress`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `personaddress` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'PRIMARY KEY FOR This table - records',
  `BusinessEntityID` int(11) NOT NULL COMMENT 'Foreign key to BusinessEntity.BusinessEntityID, Used for Application Filter.',
  `AddressId` int(11) NOT NULL COMMENT 'Foreign key to Address.AddressId',
  `PersonId` int(11) NOT NULL COMMENT 'Foreign key to Person.PersonId',
  PRIMARY KEY (`Id`),
  KEY `FK_Address` (`AddressId`),
  KEY `FK_Person` (`PersonId`),
  CONSTRAINT `FK_Address` FOREIGN KEY (`AddressId`) REFERENCES `address` (`AddressId`),
  CONSTRAINT `FK_Person` FOREIGN KEY (`PersonId`) REFERENCES `person` (`PersonId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='Address for Person';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `product` (
  `ProductId` int(11) NOT NULL AUTO_INCREMENT COMMENT 'PRIMARY KEY FOR This table - records',
  `BusinessEntityID` int(11) NOT NULL COMMENT 'Foreign key to BusinessEntity.BusinessEntityID, Used for Application Filter.',
  `Name` varchar(120) COLLATE latin1_general_ci NOT NULL COMMENT 'Name of the product.',
  `ProductNumber` varchar(80) COLLATE latin1_general_ci NOT NULL COMMENT 'Unique product identification number.',
  `Manufacturer` varchar(100) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Manufacture name',
  `Ean` varchar(100) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Code for EAN / R CODE',
  `HsCode` varchar(30) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'HS Code (Harmony System)',
  `HsCodeTax` varchar(30) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Tax for HS Code',
  `Location` varchar(50) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Location this Product on Stock',
  `MakeFlag` bit(1) NOT NULL DEFAULT b'1' COMMENT '0 = Product is purchased, 1 = Product is manufactured in-house.',
  `VariableFlag` bit(1) NOT NULL DEFAULT b'1' COMMENT '0 = Product is not variable. 1 = Product is variable.',
  `FinishedGoodsFlag` bit(1) NOT NULL DEFAULT b'1' COMMENT '0 = Product is not a salable item. 1 = Product is salable.',
  `SafetyStockLevel` decimal(12,4) DEFAULT NULL COMMENT 'Minimum inventory quantity.',
  `MaximumStocklevel` decimal(12,4) DEFAULT NULL COMMENT 'Maximum inventory quantity.',
  `ReorderPoint` decimal(12,4) DEFAULT NULL COMMENT 'Inventory level that triggers a purchase order or work order.',
  `StandardCost` decimal(12,4) DEFAULT NULL COMMENT 'Standard cost of the product.',
  `ListPrice` decimal(12,4) DEFAULT NULL COMMENT 'Selling price.',
  `SizeUnitMeasureCode` char(3) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Unit of measure for Size column.',
  `Weight` decimal(12,4) DEFAULT NULL COMMENT 'Product weight.',
  `WeightTotal` decimal(12,4) DEFAULT NULL COMMENT 'Total Product weight.',
  `Height` decimal(10,3) DEFAULT NULL COMMENT 'Height',
  `Width` decimal(10,3) DEFAULT NULL COMMENT 'Width',
  `Length` decimal(10,3) DEFAULT NULL COMMENT 'Length',
  `DaysToManufacture` int(11) DEFAULT NULL COMMENT 'Number of days required to manufacture the product.',
  `ProductAttribute` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin DEFAULT NULL COMMENT 'Attibute for this product',
  `ProductSourceID` int(11) DEFAULT NULL COMMENT 'Source for Product , 0 = National, 1 = Imported ,2 = Purchase Nat. Bu Imported',
  `ClassID` int(11) DEFAULT NULL COMMENT 'Forenkey to ProductClass.ProductClassId Class for Product ',
  `CategoryID` int(11) DEFAULT NULL COMMENT 'ForenKey to ProductCategory.ProductCategoryId Product is a member of this product subcategory.',
  `TaxGroupId` int(11) DEFAULT NULL COMMENT 'Forenkey to TaxGroup.TaxGroupId Tax for this Product',
  `TaxIva` decimal(5,2) DEFAULT NULL COMMENT 'IVA Tax',
  `TaxImport` decimal(5,2) DEFAULT NULL COMMENT 'Tax Import like II',
  `TaxProduction` decimal(5,2) DEFAULT NULL COMMENT 'Tax for production on this product',
  `TaxSale` decimal(5,2) DEFAULT NULL COMMENT 'Tax for sale this product',
  `SellStartDate` datetime NOT NULL DEFAULT current_timestamp() COMMENT 'Date the product was available for sale.',
  `SellEndDate` datetime DEFAULT NULL COMMENT 'Date the product was no longer available for sale.',
  `ModifiedDate` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp() COMMENT 'Date and time the record was last updated.',
  `Active` bit(1) NOT NULL DEFAULT b'1' COMMENT '0 = Not Active; 1 = Active',
  PRIMARY KEY (`ProductId`),
  KEY `FK_BusinessEntity_ProductId` (`BusinessEntityID`),
  KEY `FK_Category` (`CategoryID`),
  KEY `FK_Class` (`ClassID`),
  KEY `FK_GroupTax` (`TaxGroupId`),
  FULLTEXT KEY `Address` (`Name`,`ProductNumber`,`Manufacturer`,`Ean`),
  CONSTRAINT `FK_BusinessEntity_ProductId` FOREIGN KEY (`BusinessEntityID`) REFERENCES `businessentity` (`BusinessEntityID`) ON DELETE CASCADE,
  CONSTRAINT `FK_Category` FOREIGN KEY (`CategoryID`) REFERENCES `categoryproduct` (`CategoryId`) ON DELETE SET NULL,
  CONSTRAINT `FK_Class` FOREIGN KEY (`ClassID`) REFERENCES `productclass` (`ClassId`) ON DELETE SET NULL,
  CONSTRAINT `FK_GroupTax` FOREIGN KEY (`TaxGroupId`) REFERENCES `taxgroup` (`TaxGroupId`) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='Products sold or used in the manfacturing of sold products.';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `productclass`
--

DROP TABLE IF EXISTS `productclass`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `productclass` (
  `ClassId` int(11) NOT NULL AUTO_INCREMENT COMMENT 'PRIMARY KEY FOR This table - records',
  `BusinessEntityID` int(11) NOT NULL COMMENT 'Foreign key to BusinessEntity.BusinessEntityID, Used for Application Filter.',
  `Name` varchar(120) COLLATE latin1_general_ci NOT NULL COMMENT 'Name Of Classe of Product',
  PRIMARY KEY (`ClassId`),
  KEY `FK_productclass_businessentity` (`BusinessEntityID`),
  FULLTEXT KEY `ProductClass` (`Name`),
  CONSTRAINT `FK_productclass_businessentity` FOREIGN KEY (`BusinessEntityID`) REFERENCES `businessentity` (`BusinessEntityID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='Classified using in product';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `productinventory`
--

DROP TABLE IF EXISTS `productinventory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `productinventory` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'PRIMARY KEY FOR This table - records',
  `BusinessEntityID` int(11) NOT NULL COMMENT 'Foreign key to BusinessEntity.BusinessEntityID, Used for Application Filter.',
  `LocationId` int(11) NOT NULL COMMENT 'Foreign key to Production.Location.WarehouseID',
  `ProductId` int(11) NOT NULL COMMENT 'Foreign key to Production.Product.ProductId',
  `VarId` int(11) DEFAULT NULL,
  `Shelf` varchar(50) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'Storage compartment within an inventory location.',
  `Bin` int(11) DEFAULT NULL COMMENT 'Storage container on a shelf in an inventory location.',
  `CreateDate` date NOT NULL DEFAULT current_timestamp(),
  `Description` varchar(150) COLLATE latin1_general_ci DEFAULT NULL,
  `NumberDoc` varchar(150) COLLATE latin1_general_ci DEFAULT NULL,
  `Quantity` decimal(12,4) NOT NULL DEFAULT 0.0000 COMMENT 'Name of Expense',
  `Signal` int(11) NOT NULL COMMENT '1 = SellIn; 2 = SellOut',
  PRIMARY KEY (`Id`),
  KEY `FK_BusinessEntity_ProductInventory` (`BusinessEntityID`),
  KEY `FK_Location` (`LocationId`),
  KEY `FK_Product` (`ProductId`),
  FULLTEXT KEY `ProductInventory` (`Shelf`,`Description`,`NumberDoc`),
  CONSTRAINT `FK_BusinessEntity_ProductInventory` FOREIGN KEY (`BusinessEntityID`) REFERENCES `businessentity` (`BusinessEntityID`) ON DELETE CASCADE,
  CONSTRAINT `FK_Location` FOREIGN KEY (`LocationId`) REFERENCES `location` (`WarehouseId`) ON DELETE CASCADE,
  CONSTRAINT `FK_Product` FOREIGN KEY (`ProductId`) REFERENCES `product` (`ProductId`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='Inventory fro Products on stock.';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `role` (
  `Id` varchar(255) COLLATE latin1_general_ci NOT NULL,
  `Name` varchar(256) COLLATE latin1_general_ci DEFAULT NULL,
  `NormalizedName` varchar(256) COLLATE latin1_general_ci DEFAULT NULL,
  `ConcurrencyStamp` longtext COLLATE latin1_general_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `roleclaim`
--

DROP TABLE IF EXISTS `roleclaim`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `roleclaim` (
  `Id` int(11) NOT NULL,
  `RoleId` varchar(255) COLLATE latin1_general_ci NOT NULL,
  `ClaimType` longtext COLLATE latin1_general_ci DEFAULT NULL,
  `ClaimValue` longtext COLLATE latin1_general_ci DEFAULT NULL,
  PRIMARY KEY (`RoleId`,`Id`),
  UNIQUE KEY `AK_RoleClaim_Id` (`Id`),
  CONSTRAINT `FK_RoleClaim_Role_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `role` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `stateprovince`
--

DROP TABLE IF EXISTS `stateprovince`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `stateprovince` (
  `StateProvinceId` int(11) NOT NULL AUTO_INCREMENT COMMENT 'PRIMARY KEY FOR This table - records',
  `BusinessEntityID` int(11) NOT NULL COMMENT 'Foreign key to BusinessEntity.BusinessEntityID, Used for Application Filter.',
  `StateProvinceCode` varchar(50) COLLATE latin1_general_ci NOT NULL COMMENT 'ISO standard state or province code.',
  `IsOnlyStateProvinceFlag` bit(1) NOT NULL DEFAULT b'1' COMMENT '0 = StateProvinceCode exists. 1 = StateProvinceCode unavailable, using CountryRegionCode.',
  `Name` varchar(80) COLLATE latin1_general_ci NOT NULL COMMENT 'Name Of StateProvince',
  `CountryID` int(11) NOT NULL COMMENT 'Foreign key to CountryID',
  PRIMARY KEY (`StateProvinceId`),
  KEY `StateProvinceCountry` (`CountryID`),
  KEY `FK_stateprovince_businessentity` (`BusinessEntityID`),
  FULLTEXT KEY `StateProvinceIndex` (`Name`,`StateProvinceCode`),
  CONSTRAINT `FK_stateprovince_businessentity` FOREIGN KEY (`BusinessEntityID`) REFERENCES `businessentity` (`BusinessEntityID`) ON DELETE CASCADE,
  CONSTRAINT `StateProvinceCountry` FOREIGN KEY (`CountryID`) REFERENCES `country` (`CountryId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='States Provinces';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `taxgroup`
--

DROP TABLE IF EXISTS `taxgroup`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `taxgroup` (
  `TaxGroupId` int(11) NOT NULL AUTO_INCREMENT COMMENT 'PRIMARY KEY FOR TaxGroup records.',
  `BusinessEntityID` int(11) NOT NULL COMMENT 'Foreign key to BusinessEntity.BusinessEntityID, Used for Application Filter.',
  `Name` varchar(50) COLLATE latin1_general_ci NOT NULL COMMENT 'Name Of TaxGroup',
  PRIMARY KEY (`TaxGroupId`),
  KEY `FK_taxgroup_businessentity` (`BusinessEntityID`),
  CONSTRAINT `FK_taxgroup_businessentity` FOREIGN KEY (`BusinessEntityID`) REFERENCES `businessentity` (`BusinessEntityID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='Table used for filter gruoup of tax products ';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `taxoperation`
--

DROP TABLE IF EXISTS `taxoperation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `taxoperation` (
  `TaxOperationId` int(11) NOT NULL AUTO_INCREMENT COMMENT 'PRIMARY KEY FOR This table - records',
  `BusinessEntityID` int(11) NOT NULL COMMENT 'Foreign key to BusinessEntity.BusinessEntityID, Used for Application Filter.',
  `Name` varchar(50) COLLATE latin1_general_ci NOT NULL COMMENT 'Name Of Operation',
  `TaxFunction` int(11) NOT NULL DEFAULT 1 COMMENT 'Function of this Tax 1 = Sales; 2 = Return Request; 3 = Return; 4 = Transfer, 5 = Aux',
  `TaxWay` int(11) NOT NULL DEFAULT 2 COMMENT '1 = In, 2 = Out',
  `DefaultCode` varchar(4) COLLATE latin1_general_ci NOT NULL COMMENT 'Special Code State like CFOP',
  `StockTrigger` bit(1) NOT NULL DEFAULT b'1' COMMENT 'Trigger to move stock; If 1 = All products are move in stock; 0 = Not move stock',
  `CostTrigger` bit(1) NOT NULL DEFAULT b'1' COMMENT 'Trigger to move cost product; If 1 = buy product are make history in Cost',
  PRIMARY KEY (`TaxOperationId`),
  KEY `FK_taxoperation_businessentity` (`BusinessEntityID`),
  CONSTRAINT `FK_taxoperation_businessentity` FOREIGN KEY (`BusinessEntityID`) REFERENCES `businessentity` (`BusinessEntityID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='Operation tax, using for Invoice And Tax';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `teste`
--

DROP TABLE IF EXISTS `teste`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `teste` (
  `Coluna 1` int(11) NOT NULL,
  `tedto` varchar(50) COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`Coluna 1`),
  FULLTEXT KEY `tedto` (`tedto`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='teste';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `Id` varchar(255) COLLATE latin1_general_ci NOT NULL,
  `UserName` varchar(256) COLLATE latin1_general_ci DEFAULT NULL,
  `NormalizedUserName` varchar(256) COLLATE latin1_general_ci DEFAULT NULL,
  `Email` varchar(256) COLLATE latin1_general_ci DEFAULT NULL,
  `NormalizedEmail` varchar(256) COLLATE latin1_general_ci DEFAULT NULL,
  `EmailConfirmed` bit(1) NOT NULL,
  `PasswordHash` longtext COLLATE latin1_general_ci DEFAULT NULL,
  `SecurityStamp` longtext COLLATE latin1_general_ci DEFAULT NULL,
  `ConcurrencyStamp` longtext COLLATE latin1_general_ci DEFAULT NULL,
  `PhoneNumber` longtext COLLATE latin1_general_ci DEFAULT NULL,
  `PhoneNumberConfirmed` bit(1) NOT NULL,
  `TwoFactorEnabled` bit(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` bit(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  `BusinessEntityId` int(11) NOT NULL,
  `FirstName` longtext COLLATE latin1_general_ci DEFAULT NULL,
  `LastName` longtext COLLATE latin1_general_ci DEFAULT NULL,
  `MidleName` longtext COLLATE latin1_general_ci DEFAULT NULL,
  `AvatarImage` longblob DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `userclaim`
--

DROP TABLE IF EXISTS `userclaim`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `userclaim` (
  `Id` int(11) NOT NULL,
  `UserId` varchar(255) COLLATE latin1_general_ci NOT NULL,
  `ClaimType` longtext COLLATE latin1_general_ci DEFAULT NULL,
  `ClaimValue` longtext COLLATE latin1_general_ci DEFAULT NULL,
  PRIMARY KEY (`UserId`,`Id`),
  UNIQUE KEY `AK_UserClaim_Id` (`Id`),
  CONSTRAINT `FK_UserClaim_User_UserId` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `userlogin`
--

DROP TABLE IF EXISTS `userlogin`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `userlogin` (
  `LoginProvider` varchar(255) COLLATE latin1_general_ci NOT NULL,
  `ProviderKey` varchar(255) COLLATE latin1_general_ci NOT NULL,
  `ProviderDisplayName` longtext COLLATE latin1_general_ci DEFAULT NULL,
  `UserId` varchar(255) COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`UserId`,`ProviderKey`),
  UNIQUE KEY `AK_UserLogin_LoginProvider_ProviderKey` (`LoginProvider`,`ProviderKey`),
  CONSTRAINT `FK_UserLogin_User_UserId` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `userrole`
--

DROP TABLE IF EXISTS `userrole`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `userrole` (
  `UserId` varchar(255) COLLATE latin1_general_ci NOT NULL,
  `RoleId` varchar(255) COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_UserRole_RoleId` (`RoleId`),
  CONSTRAINT `FK_UserRole_Role_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `role` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_UserRole_User_UserId` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `usertoken`
--

DROP TABLE IF EXISTS `usertoken`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `usertoken` (
  `UserId` varchar(255) COLLATE latin1_general_ci NOT NULL,
  `LoginProvider` varchar(255) COLLATE latin1_general_ci NOT NULL,
  `Name` varchar(255) COLLATE latin1_general_ci NOT NULL,
  `Value` longtext COLLATE latin1_general_ci DEFAULT NULL,
  PRIMARY KEY (`UserId`),
  UNIQUE KEY `AK_UserToken_UserId_LoginProvider_Name` (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_UserToken_User_UserId` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Temporary table structure for view `wiewinventoryproduct`
--

DROP TABLE IF EXISTS `wiewinventoryproduct`;
/*!50001 DROP VIEW IF EXISTS `wiewinventoryproduct`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `wiewinventoryproduct` (
  `ProductId` tinyint NOT NULL,
  `BusinessEntityId` tinyint NOT NULL,
  `Name` tinyint NOT NULL,
  `ProductNumber` tinyint NOT NULL,
  `Manufacturer` tinyint NOT NULL,
  `Ean` tinyint NOT NULL,
  `HsCode` tinyint NOT NULL,
  `Location` tinyint NOT NULL,
  `ListPrice` tinyint NOT NULL,
  `ProductAttribute` tinyint NOT NULL,
  `Stock` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;

--
-- Final view structure for view `wiewinventoryproduct`
--

/*!50001 DROP TABLE IF EXISTS `wiewinventoryproduct`*/;
/*!50001 DROP VIEW IF EXISTS `wiewinventoryproduct`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY INVOKER */
/*!50001 VIEW `wiewinventoryproduct` AS select `a`.`ProductId` AS `ProductId`,`a`.`BusinessEntityID` AS `BusinessEntityId`,`a`.`Name` AS `Name`,`a`.`ProductNumber` AS `ProductNumber`,`a`.`Manufacturer` AS `Manufacturer`,`a`.`Ean` AS `Ean`,`a`.`HsCode` AS `HsCode`,`a`.`Location` AS `Location`,`a`.`ListPrice` AS `ListPrice`,`a`.`ProductAttribute` AS `ProductAttribute`,(select ifnull(sum(`b`.`Quantity`),0) from `productinventory` `b` where `b`.`BusinessEntityID` = `a`.`BusinessEntityID` and `b`.`ProductId` = `a`.`ProductId` and `b`.`Signal` = 1) - (select ifnull(sum(`b`.`Quantity`),0) from `productinventory` `b` where `b`.`BusinessEntityID` = `a`.`BusinessEntityID` and `b`.`ProductId` = `a`.`ProductId` and `b`.`Signal` = 2) AS `Stock` from `product` `a` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-08-08 20:33:16
