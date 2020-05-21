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