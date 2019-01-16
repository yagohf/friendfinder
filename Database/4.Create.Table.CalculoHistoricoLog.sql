USE [FriendFinder];
GO

IF NOT EXISTS (
		SELECT 1
		FROM sys.tables t inner join sys.schemas s on t.schema_id = s.schema_id
		WHERE t.name = 'CalculoHistoricoLog' AND s.name = 'dbo'
		)
BEGIN
	PRINT 'CREATE TABLE [dbo].[CalculoHistoricoLog]';

	CREATE TABLE [dbo].[CalculoHistoricoLog]
	(
		Id INT NOT NULL IDENTITY(1,1),
		IdUsuario INT NOT NULL,
		DataOcorrencia DATETIME NOT NULL,
		Resultado VARCHAR(8000) NOT NULL,
		CONSTRAINT PK_CalculoHistoricoLog PRIMARY KEY CLUSTERED (Id),
		CONSTRAINT FK_CalculoHistoricoLog_Usuario FOREIGN KEY(IdUsuario) REFERENCES [dbo].[Usuario](Id)
	);
END
GO
