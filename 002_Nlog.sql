USE [SoloContacts]
GO

/****** Object: Table [dbo].[NLog] ******/
DROP TABLE IF EXISTS [dbo].[NLog]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[NLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MachineName] [nvarchar](200) NOT NULL,
	[ApplicationName] [nvarchar](200) NOT NULL,
	[UserName] [nvarchar](200) NOT NULL,
	[Logged] [datetime] NOT NULL,
	[Level] [varchar](5) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[Logger] [nvarchar](300) NOT NULL,
	[Properties] [nvarchar](max) NOT NULL,
	[Callsite] [nvarchar](300) NOT NULL,
	[Exception] [nvarchar](max) NOT NULL,
	[RequestId] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_dbo.Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


/****** Object:  StoredProcedure [dbo].[NLogEntryCreate] ******/
DROP PROCEDURE IF EXISTS [dbo].[NLogEntryCreate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[NLogEntryCreate]
	@MachineName nvarchar(200),
	@ApplicationName nvarchar(200),
	@UserName nvarchar(200),
	@Logged datetime,
	@Level varchar(5),
	@Message nvarchar(max),
	@Logger nvarchar(300),
	@Properties nvarchar(max),
	@Callsite nvarchar(300),
	@Exception nvarchar(max),
	@RequestId nvarchar(100)
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[NLog] (
		[MachineName],
		[ApplicationName],
		[UserName],
		[Logged],
		[Level],
		[Message],
		[Logger],
		[Properties],
		[Callsite],
		[Exception],
		[RequestId]
	)
	VALUES (
		@MachineName,
		@ApplicationName,
		@UserName,
		@Logged,
		@Level,
		@Message,
		@Logger,
		@Properties,
		@Callsite,
		@Exception,
		@RequestId
	)

	SELECT SCOPE_IDENTITY() As InsertedID
END
GO

GRANT EXECUTE ON [dbo].[NLogEntryCreate] TO [SoloContactsApp]
GO

