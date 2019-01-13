USE [FriendFinder];
GO

IF NOT EXISTS (
		SELECT 1
		FROM sys.tables t inner join sys.schemas s on t.schema_id = s.schema_id
		WHERE t.name = 'Amigo' AND s.name = 'dbo'
		)
BEGIN
	PRINT 'CREATE TABLE [dbo].[Amigo]';

	CREATE TABLE [dbo].[Amigo]
	(
		Id INT NOT NULL IDENTITY(1,1),
		IdUsuario INT NOT NULL,
		Nome VARCHAR(100) NOT NULL,
		Latitude DECIMAL(8,6) NOT NULL,
		Longitude DECIMAL(9,6) NOT NULL,
		CONSTRAINT PK_Amigo PRIMARY KEY CLUSTERED (Id),
		CONSTRAINT UQ_Amigo UNIQUE (IdUsuario, Latitude, Longitude)
	);
END
GO
