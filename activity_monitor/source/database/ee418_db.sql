SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL';

CREATE SCHEMA IF NOT EXISTS `Activity_Monitor` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci ;
USE `Activity_Monitor`;

-- -----------------------------------------------------
-- Table `Activity_Monitor`.`User`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Activity_Monitor`.`User` ;

CREATE  TABLE IF NOT EXISTS `Activity_Monitor`.`User` (
  `Username` VARCHAR(20) NOT NULL ,
  `Password` VARBINARY(32) NULL ,
  `FirstName` VARBINARY(48) NULL ,
  `MiddleName` VARBINARY(48) NULL ,
  `LastName` VARBINARY(48) NULL ,
  `idNumber` INT UNSIGNED NOT NULL AUTO_INCREMENT ,
  `Address` VARBINARY(256) NULL ,
  `PhoneNumber` VARBINARY(16) NULL ,
  `UserClass` VARCHAR(45) NULL ,
  PRIMARY KEY (`Username`) ,
  INDEX `id_index` (`idNumber` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Activity_Monitor`.`Patient`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Activity_Monitor`.`Patient` ;

CREATE  TABLE IF NOT EXISTS `Activity_Monitor`.`Patient` (
  `idNumber` INT UNSIGNED NOT NULL ,
  `DOB` DATETIME NULL ,
  `Height` TINYINT UNSIGNED NULL ,
  `Weight` TINYINT UNSIGNED NULL ,
  `Gender` VARCHAR(7) NULL ,
  `SSN` VARBINARY(16) NULL ,
  `MedicalConditions` BLOB NULL ,
  `ClinicianId` INT UNSIGNED NOT NULL ,
  PRIMARY KEY (`idNumber`) ,
  INDEX `fk_idPatient_User` (`idNumber` ASC) ,
  INDEX `fk_ClinicianId_User` (`ClinicianId` ASC) ,
  CONSTRAINT `fk_idPatient_User`
    FOREIGN KEY (`idNumber` )
    REFERENCES `Activity_Monitor`.`User` (`idNumber` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_ClinicianId_User`
    FOREIGN KEY (`ClinicianId` )
    REFERENCES `Activity_Monitor`.`User` (`idNumber` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Activity_Monitor`.`DailyTask`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Activity_Monitor`.`DailyTask` ;

CREATE  TABLE IF NOT EXISTS `Activity_Monitor`.`DailyTask` (
  `idDailyTask` INT UNSIGNED NOT NULL AUTO_INCREMENT ,
  `idPatient` INT UNSIGNED NOT NULL ,
  `DailyTaskClass` VARCHAR(45) NOT NULL ,
  `Date` TIMESTAMP NOT NULL ,
  `Active` TINYINT(1) NULL ,
  PRIMARY KEY (`idDailyTask`) ,
  INDEX `fk_DailyTask_Patient` (`idPatient` ASC) ,
  CONSTRAINT `fk_DailyTask_Patient`
    FOREIGN KEY (`idPatient` )
    REFERENCES `Activity_Monitor`.`Patient` (`idNumber` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Activity_Monitor`.`Statistics`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Activity_Monitor`.`Statistics` ;

CREATE  TABLE IF NOT EXISTS `Activity_Monitor`.`Statistics` (
  `idStatistics` INT UNSIGNED NOT NULL AUTO_INCREMENT ,
  `StatsClass` VARCHAR(45) NOT NULL ,
  `idDailyTask` INT UNSIGNED NOT NULL ,
  `StartDateTime` TIMESTAMP NULL ,
  `EndDateTime` TIMESTAMP NULL ,
  PRIMARY KEY (`idStatistics`) ,
  INDEX `fk_Statistics_DailyTask` (`idDailyTask` ASC) ,
  CONSTRAINT `fk_Statistics_DailyTask`
    FOREIGN KEY (`idDailyTask` )
    REFERENCES `Activity_Monitor`.`DailyTask` (`idDailyTask` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Activity_Monitor`.`WalkingTask`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Activity_Monitor`.`WalkingTask` ;

CREATE  TABLE IF NOT EXISTS `Activity_Monitor`.`WalkingTask` (
  `idDailyTask` INT UNSIGNED NOT NULL ,
  `Steps` INT NOT NULL ,
  PRIMARY KEY (`idDailyTask`) ,
  INDEX `fk_WalkingTask_DailyTask` (`idDailyTask` ASC) ,
  CONSTRAINT `fk_WalkingTask_DailyTask`
    FOREIGN KEY (`idDailyTask` )
    REFERENCES `Activity_Monitor`.`DailyTask` (`idDailyTask` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Activity_Monitor`.`WalkingStats`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Activity_Monitor`.`WalkingStats` ;

CREATE  TABLE IF NOT EXISTS `Activity_Monitor`.`WalkingStats` (
  `idStatistics` INT UNSIGNED NOT NULL ,
  `Steps` INT NOT NULL ,
  PRIMARY KEY (`idStatistics`) ,
  INDEX `fk_WalkingStats_Statistics` (`idStatistics` ASC) ,
  CONSTRAINT `fk_WalkingStats_Statistics`
    FOREIGN KEY (`idStatistics` )
    REFERENCES `Activity_Monitor`.`Statistics` (`idStatistics` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;



SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
