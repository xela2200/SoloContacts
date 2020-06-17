USE [master]
GO

/**********************************************************************************************************
Change the following:
001. CREATE DATABASE --> change to desired location
002. CREATE LOGIN --> Change Password. If you already setup external database, run script from this section.
***********************************************************************************************************/


/****** Change 001. CREATE DATABASE --> change to desired location --> FILENAME = N'<folder>\SoloContacts.mdf' and  FILENAME = N'<folder>\SoloContacts_log.ldf' ******/
CREATE DATABASE [SoloContacts]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SoloContacts', FILENAME = N'D:\Databases\SoloContacts.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SoloContacts_log', FILENAME = N'D:\Databases\SoloContacts_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SoloContacts].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [SoloContacts] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [SoloContacts] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [SoloContacts] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [SoloContacts] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [SoloContacts] SET ARITHABORT OFF 
GO

ALTER DATABASE [SoloContacts] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [SoloContacts] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [SoloContacts] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [SoloContacts] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [SoloContacts] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [SoloContacts] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [SoloContacts] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [SoloContacts] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [SoloContacts] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [SoloContacts] SET  DISABLE_BROKER 
GO

ALTER DATABASE [SoloContacts] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [SoloContacts] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [SoloContacts] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [SoloContacts] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [SoloContacts] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [SoloContacts] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [SoloContacts] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [SoloContacts] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [SoloContacts] SET  MULTI_USER 
GO

ALTER DATABASE [SoloContacts] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [SoloContacts] SET DB_CHAINING OFF 
GO

ALTER DATABASE [SoloContacts] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [SoloContacts] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [SoloContacts] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [SoloContacts] SET QUERY_STORE = OFF
GO

USE [SoloContacts]
GO

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE [SoloContacts] SET  READ_WRITE 
GO


/****** Change 002. CREATE LOGIN --> Change Password --> PASSWORD=N'<password>' :: Credentials will be used in application connection string on appsettings.json ******/
USE [master]
GO

/****** Object:  Login [SoloContactsApp]    ******/
IF EXISTS(SELECT * FROM sys.sql_logins WHERE name = 'SoloContactsApp')
BEGIN
	DROP LOGIN [SoloContactsApp]	
END
GO

CREATE LOGIN [SoloContactsApp] WITH PASSWORD=N'G0n31n60', DEFAULT_DATABASE=[SoloContacts], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO


USE [SoloContacts]
GO

/****** Object:  User [SoloContactsApp] ******/
DROP USER IF EXISTS [SoloContactsApp]
GO

CREATE USER [SoloContactsApp] FOR LOGIN [SoloContactsApp] WITH DEFAULT_SCHEMA=[dbo]
GO


