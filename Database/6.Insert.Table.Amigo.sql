USE [FriendFinder];
GO

DECLARE @IdUsuarioTestes INT = (SELECT Id FROM [dbo].[Usuario] WHERE [Login] = 'cubo');

IF(@IdUsuarioTestes > 0)
BEGIN
	--TODO - RECHEAR COM MASSA DE DADOS.
END
GO