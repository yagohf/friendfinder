USE [FriendFinder];
GO

IF NOT EXISTS (
		SELECT 1
		FROM sys.tables t inner join sys.schemas s on t.schema_id = s.schema_id
		WHERE t.name = 'Usuario' AND s.name = 'dbo'
		)
BEGIN
	PRINT 'CREATE TABLE [dbo].[Usuario]';

	CREATE TABLE [dbo].[Usuario]
	(
		Id INT NOT NULL IDENTITY(1,1),
		[Login] VARCHAR(20) NOT NULL,
		Senha VARCHAR(32) NOT NULL,
		Nome VARCHAR(100) NOT NULL,
		CONSTRAINT PK_Usuario PRIMARY KEY CLUSTERED (Id),
		CONSTRAINT UQ_Usuario UNIQUE ([Login])
	);
END
GO
