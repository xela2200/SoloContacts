USE [SoloContacts]
GO

ALTER TABLE [dbo].[ApplicationUser] DROP CONSTRAINT IF EXISTS [CK_ApplicationUser_Nprmalized]
GO

ALTER TABLE [dbo].[ApplicationUser] DROP CONSTRAINT IF EXISTS [CK_ApplicationUser]
GO

ALTER TABLE [dbo].[ApplicationUserRole] DROP CONSTRAINT IF EXISTS [FK_ApplicationUserRole_User]
GO

ALTER TABLE [dbo].[ApplicationUserRole] DROP CONSTRAINT IF EXISTS [FK_ApplicationUserRole_Role]
GO

DROP TABLE IF EXISTS [dbo].[ApplicationUser]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ApplicationUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[NormalizedUserName] [nvarchar](256) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[NormalizedEmail] [nvarchar](256) NOT NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
 CONSTRAINT [PK__Applicat__3214EC0713466F06] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ApplicationUser]  WITH CHECK ADD  CONSTRAINT [CK_ApplicationUser] CHECK  (([UserName]=[Email]))
GO

ALTER TABLE [dbo].[ApplicationUser] CHECK CONSTRAINT [CK_ApplicationUser]
GO

ALTER TABLE [dbo].[ApplicationUser]  WITH CHECK ADD  CONSTRAINT [CK_ApplicationUser_Nprmalized] CHECK  (([NormalizedUserName]=[NormalizedEmail]))
GO

ALTER TABLE [dbo].[ApplicationUser] CHECK CONSTRAINT [CK_ApplicationUser_Nprmalized]
GO


DROP TABLE IF EXISTS [dbo].[ApplicationRole]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ApplicationRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[NormalizedName] [nvarchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

DROP TABLE IF EXISTS [dbo].[ApplicationUserRole]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ApplicationUserRole](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ApplicationUserRole]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationUserRole_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[ApplicationRole] ([Id])
GO

ALTER TABLE [dbo].[ApplicationUserRole] CHECK CONSTRAINT [FK_ApplicationUserRole_Role]
GO

ALTER TABLE [dbo].[ApplicationUserRole]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationUserRole_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[ApplicationUser] ([Id])
GO

ALTER TABLE [dbo].[ApplicationUserRole] CHECK CONSTRAINT [FK_ApplicationUserRole_User]
GO

DROP PROCEDURE IF EXISTS [dbo].[UserUpdate]
GO

DROP PROCEDURE IF EXISTS [dbo].[UserRoleRetrieveList]
GO

DROP PROCEDURE IF EXISTS [dbo].[UserRoleExists]
GO

DROP PROCEDURE IF EXISTS [dbo].[UserRoleDeleteByRoleName]
GO

DROP PROCEDURE IF EXISTS [dbo].[UserRoleCreateWithRoleName]
GO

DROP PROCEDURE IF EXISTS [dbo].[UserRoleCreateByRoleName]
GO

DROP PROCEDURE IF EXISTS [dbo].[UserRoleCreate]
GO

DROP PROCEDURE IF EXISTS [dbo].[UserRetrieveListByRole]
GO

DROP PROCEDURE IF EXISTS [dbo].[UserRetrieveByName]
GO

DROP PROCEDURE IF EXISTS [dbo].[UserRetrievebyEmail]
GO

DROP PROCEDURE IF EXISTS [dbo].[UserRetrieve]
GO

DROP PROCEDURE IF EXISTS [dbo].[UserDelete]
GO

DROP PROCEDURE IF EXISTS [dbo].[UserCreate]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alex Seiglie
-- Description:	Creates new user
-- =============================================
CREATE PROC [dbo].[UserCreate]
	 @UserName nvarchar(256)
	,@NormalizedUserName nvarchar(256)
	,@Email nvarchar(256)
	,@NormalizedEmail nvarchar(256)
	,@EmailConfirmed bit
	,@PasswordHash nvarchar(max)
	,@PhoneNumber nvarchar(50)
	,@PhoneNumberConfirmed bit
	,@TwoFactorEnabled bit
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ErrorMessage varchar(256)

	IF EXISTS (
				SELECT 1 FROM [dbo].[ApplicationUser]
				WHERE [dbo].[ApplicationUser].[NormalizedEmail] = @NormalizedEmail
				)
	BEGIN
		SET @ErrorMessage = 'Email ' + @Email + ' is already taken.'
		RAISERROR (@ErrorMessage, -- Message text.
					16, -- Severity.
					1 -- State.
					);
			RETURN
	END


	INSERT INTO [dbo].[ApplicationUser]
			   ([UserName]
			   ,[NormalizedUserName]
			   ,[Email]
			   ,[NormalizedEmail]
			   ,[EmailConfirmed]
			   ,[PasswordHash]
			   ,[PhoneNumber]
			   ,[PhoneNumberConfirmed]
			   ,[TwoFactorEnabled])
		 VALUES
			   (
			    @UserName
			   ,@NormalizedUserName
			   ,@Email
			   ,@NormalizedEmail
			   ,@EmailConfirmed
			   ,@PasswordHash
			   ,@PhoneNumber
			   ,@PhoneNumberConfirmed
			   ,@TwoFactorEnabled
			   )

	SELECT SCOPE_IDENTITY() As InsertedID
END
GO

GRANT EXECUTE ON [dbo].[UserCreate] TO [SoloContactsApp]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alex Seiglie
-- Description:	Delete user record based on id
-- =============================================
CREATE PROC [dbo].[UserDelete]
	@Id int
AS
BEGIN
	SET NOCOUNT ON;

	  DELETE 
		[dbo].[ApplicationUser]
	  WHERE
		[Id] = @Id
END
GO

GRANT EXECUTE ON [dbo].[UserDelete] TO [SoloContactsApp]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alex Seiglie
-- Description:	Retrieves specific user record based on id
-- =============================================
CREATE PROC [dbo].[UserRetrieve]
	@Id int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id]
		  ,[UserName]
		  ,[NormalizedUserName]
		  ,[Email]
		  ,[NormalizedEmail]
		  ,[EmailConfirmed]
		  ,[PasswordHash]
		  ,[PhoneNumber]
		  ,[PhoneNumberConfirmed]
		  ,[TwoFactorEnabled]
	  FROM 
		[dbo].[ApplicationUser]
	  WHERE
		[Id] = @Id
END
GO

GRANT EXECUTE ON [dbo].[UserRetrieve] TO [SoloContactsApp]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alex Seiglie
-- Description:	Retrieves specific user record based on email
-- =============================================
CREATE PROC [dbo].[UserRetrievebyEmail]
	@NormalizedEmail nvarchar(256)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id]
		  ,[UserName]
		  ,[NormalizedUserName]
		  ,[Email]
		  ,[NormalizedEmail]
		  ,[EmailConfirmed]
		  ,[PasswordHash]
		  ,[PhoneNumber]
		  ,[PhoneNumberConfirmed]
		  ,[TwoFactorEnabled]
	  FROM 
		[dbo].[ApplicationUser]
	  WHERE
		[NormalizedEmail] = @NormalizedEmail
END
GO

GRANT EXECUTE ON [dbo].[UserRetrievebyEmail] TO [SoloContactsApp]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alex Seiglie
-- Description:	Retrieves specific user record base on name or email
-- =============================================
CREATE PROC [dbo].[UserRetrieveByName]
	@NormalizedUserName NVARCHAR(256) 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id]
		  ,[UserName]
		  ,[NormalizedUserName]
		  ,[Email]
		  ,[NormalizedEmail]
		  ,[EmailConfirmed]
		  ,[PasswordHash]
		  ,[PhoneNumber]
		  ,[PhoneNumberConfirmed]
		  ,[TwoFactorEnabled]
	  FROM 
		[dbo].[ApplicationUser]
	  WHERE
		[NormalizedUserName] = @NormalizedUserName
END
GO

GRANT EXECUTE ON [dbo].[UserRetrieveByName] TO [SoloContactsApp]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alex Seiglie
-- Description:	Retrieves list of users by role
-- =============================================
CREATE PROC [dbo].[UserRetrieveListByRole]
	 @NormalizedName nvarchar(256)
AS
BEGIN
	SET NOCOUNT ON
	
	IF NOT EXISTS (
				SELECT 1 FROM [dbo].[ApplicationRole]
				WHERE [dbo].[ApplicationRole].[NormalizedName] = @normalizedName
				)
	BEGIN
		RAISERROR ('Role name does not exist in the database.', -- Message text.
			16, -- Severity.
			1 -- State.
			);
			RETURN
	END

	SELECT 
		 [dbo].[ApplicationUser].[Id]
		,[dbo].[ApplicationUser].[UserName]
		,[dbo].[ApplicationUser].[NormalizedUserName]
		,[dbo].[ApplicationUser].[Email]
		,[dbo].[ApplicationUser].[NormalizedEmail]
		,[dbo].[ApplicationUser].[EmailConfirmed]
		,[dbo].[ApplicationUser].[PasswordHash]
		,[dbo].[ApplicationUser].[PhoneNumber]
		,[dbo].[ApplicationUser].[PhoneNumberConfirmed]
		,[dbo].[ApplicationUser].[TwoFactorEnabled]
	FROM 
		[dbo].[ApplicationUser]
			INNER JOIN [dbo].[ApplicationUserRole]
				ON [dbo].[ApplicationUserRole].[UserId] = [dbo].[ApplicationUser].[Id] 
			INNER JOIN [dbo].[ApplicationRole]
				ON [dbo].[ApplicationRole].[Id] = [dbo].[ApplicationUserRole].[RoleId] 
	WHERE
		[dbo].[ApplicationRole].[NormalizedName] = @normalizedName			
END
GO

GRANT EXECUTE ON [dbo].[UserRetrieveListByRole] TO [SoloContactsApp]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alex Seiglie
-- Description:	Adds user to role
-- =============================================
CREATE PROC [dbo].[UserRoleCreate]
	 @UserId int
	,@RoleId int
AS
BEGIN
	SET NOCOUNT ON
		
	IF NOT EXISTS (
				SELECT 1 FROM [dbo].[ApplicationUser]
				WHERE [dbo].[ApplicationUser].[Id] != @UserId
				)
	BEGIN
		RAISERROR ('User Id does not exist in the database.', -- Message text.
			16, -- Severity.
			1 -- State.
			);
			RETURN
	END


	INSERT INTO [dbo].[ApplicationUserRole]
			   ([UserId]
			   ,[RoleId])
		 VALUES
			   (
			    @UserId
			   ,@RoleId
			   )

	SELECT SCOPE_IDENTITY() As InsertedID
END
GO

GRANT EXECUTE ON [dbo].[UserRoleCreate] TO [SoloContactsApp]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alex Seiglie
-- Description:	Adds user to role with role name
-- =============================================
CREATE PROC [dbo].[UserRoleCreateByRoleName]
	 @UserId int
	,@NormalizedName nvarchar(256)
AS
BEGIN
	SET NOCOUNT ON

	IF NOT EXISTS (
				SELECT 1 FROM [dbo].[ApplicationRole]
				WHERE [dbo].[ApplicationRole].[NormalizedName] = @NormalizedName
				)
	BEGIN
		RAISERROR ('Role Name does not exist in the database.', -- Message text.
			16, -- Severity.
			1 -- State.
			);
			RETURN
	END
		
	IF NOT EXISTS (
				SELECT 1 FROM [dbo].[ApplicationUser]
				WHERE [dbo].[ApplicationUser].[Id] = @UserId
				)
	BEGIN
		RAISERROR ('User Id does not exist in the database.', -- Message text.
			16, -- Severity.
			1 -- State.
			);
			RETURN
	END

	DECLARE @RoleId int = -1

	SELECT 
		@RoleId = [Id] 
	FROM
		[dbo].[ApplicationRole]
	WHERE
		[NormalizedName] = @NormalizedName


	INSERT INTO [dbo].[ApplicationUserRole]
			   ([UserId]
			   ,[RoleId])
		 VALUES
			   (
			    @UserId
			   ,@RoleId
			   )

	SELECT SCOPE_IDENTITY() As InsertedID
END
GO

GRANT EXECUTE ON [dbo].[UserRoleCreateByRoleName] TO [SoloContactsApp]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alex Seiglie
-- Description:	Adds user to role with role name
-- =============================================
CREATE PROC [dbo].[UserRoleCreateWithRoleName]
	 @UserId int
	,@NormalizedName nvarchar(256)
AS
BEGIN
	SET NOCOUNT ON

	IF NOT EXISTS (
				SELECT 1 FROM [dbo].[ApplicationRole]
				WHERE [dbo].[ApplicationRole].[NormalizedName] = @NormalizedName
				)
	BEGIN
		RAISERROR ('Role Name does not exist in the database.', -- Message text.
			16, -- Severity.
			1 -- State.
			);
			RETURN
	END
		
	IF NOT EXISTS (
				SELECT 1 FROM [dbo].[ApplicationUser]
				WHERE [dbo].[ApplicationUser].[Id] = @UserId
				)
	BEGIN
		RAISERROR ('User Id does not exist in the database.', -- Message text.
			16, -- Severity.
			1 -- State.
			);
			RETURN
	END

	DECLARE @RoleId int = -1

	SELECT 
		@RoleId = [Id] 
	FROM
		[dbo].[ApplicationRole]
	WHERE
		[NormalizedName] = @NormalizedName



	DELETE FROM 
		[dbo].[ApplicationUserRole]
      WHERE 
		[UserId]		= @UserId
		AND [RoleId]	= @RoleId


	SELECT SCOPE_IDENTITY() As InsertedID
END
GO

GRANT EXECUTE ON [dbo].[UserRoleCreateWithRoleName] TO [SoloContactsApp]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alex Seiglie
-- Description:	Adds user to role with role name
-- =============================================
CREATE PROC [dbo].[UserRoleDeleteByRoleName]
	 @UserId int
	,@NormalizedName nvarchar(256)
AS
BEGIN
	SET NOCOUNT ON

	IF NOT EXISTS (
				SELECT 1 FROM [dbo].[ApplicationRole]
				WHERE [dbo].[ApplicationRole].[NormalizedName] = @NormalizedName
				)
	BEGIN
		RAISERROR ('Role Name does not exist in the database.', -- Message text.
			16, -- Severity.
			1 -- State.
			);
			RETURN
	END
		
	IF NOT EXISTS (
				SELECT 1 FROM [dbo].[ApplicationUser]
				WHERE [dbo].[ApplicationUser].[Id] = @UserId
				)
	BEGIN
		RAISERROR ('User Id does not exist in the database.', -- Message text.
			16, -- Severity.
			1 -- State.
			);
			RETURN
	END

	DECLARE @RoleId int = -1

	SELECT 
		@RoleId = [Id] 
	FROM
		[dbo].[ApplicationRole]
	WHERE
		[NormalizedName] = @NormalizedName



	DELETE FROM 
		[dbo].[ApplicationUserRole]
      WHERE 
		[UserId]		= @UserId
		AND [RoleId]	= @RoleId


	SELECT SCOPE_IDENTITY() As InsertedID
END
GO

GRANT EXECUTE ON [dbo].[UserRoleDeleteByRoleName] TO [SoloContactsApp]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alex Seiglie
-- Description:	Test user exists in role
-- =============================================
CREATE PROC [dbo].[UserRoleExists]
	 @UserId int
	,@NormalizedName nvarchar(256)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @RoleId int = -1

	SELECT 
		@RoleId = [Id] 
	FROM
		[dbo].[ApplicationRole]
	WHERE
		[NormalizedName] = @NormalizedName

	SELECT 
		COUNT(1)
	FROM
		[dbo].[ApplicationUserRole]
      WHERE 
		[UserId]		= @UserId
		AND [RoleId]	= @RoleId
END
GO

GRANT EXECUTE ON [dbo].[UserRoleExists] TO [SoloContactsApp]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alex Seiglie
-- Description:	Retrieve roles the user belongs to
-- =============================================
CREATE PROC [dbo].[UserRoleRetrieveList]
	 @UserId int
AS
BEGIN
	SET NOCOUNT ON
	
	IF NOT EXISTS (
				SELECT 1 FROM [dbo].[ApplicationUser]
				WHERE [dbo].[ApplicationUser].[Id] = @UserId
				)
	BEGIN
		RAISERROR ('User Id does not exist in the database.', -- Message text.
			16, -- Severity.
			1 -- State.
			);
			RETURN
	END

	SELECT 
		[RoleName] = [dbo].[ApplicationRole].[Name] 
	FROM 
		[dbo].[ApplicationRole]
			INNER JOIN [dbo].[ApplicationUserRole]
				ON [dbo].[ApplicationUserRole].[RoleId] = [dbo].[ApplicationRole].[Id]
	WHERE 
		[dbo].[ApplicationUserRole].[UserId] = @UserId

END
GO

GRANT EXECUTE ON [dbo].[UserRoleRetrieveList] TO [SoloContactsApp]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alex Seiglie
-- Description:	Update user based on Id
-- =============================================
CREATE PROC [dbo].[UserUpdate]
	 @Id int
	,@UserName nvarchar(256)
	,@NormalizedUserName nvarchar(256)
	,@Email nvarchar(256)
	,@NormalizedEmail nvarchar(256)
	,@EmailConfirmed bit
	,@PasswordHash nvarchar(max)
	,@PhoneNumber nvarchar(50)
	,@PhoneNumberConfirmed bit
	,@TwoFactorEnabled bit
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[ApplicationUser]
	   SET 
		   [UserName] = @UserName
		  ,[NormalizedUserName]		= @NormalizedUserName
		  ,[Email]					= @Email
		  ,[NormalizedEmail]		= @NormalizedEmail
		  ,[EmailConfirmed]			= @EmailConfirmed
		  ,[PasswordHash]			= @PasswordHash
		  ,[PhoneNumber]			= @PhoneNumber
		  ,[PhoneNumberConfirmed]	= @PhoneNumberConfirmed
		  ,[TwoFactorEnabled]		= @TwoFactorEnabled
	 WHERE 
		[Id] = @Id
END
GO

GRANT EXECUTE ON [dbo].[UserUpdate] TO [SoloContactsApp]
GO

