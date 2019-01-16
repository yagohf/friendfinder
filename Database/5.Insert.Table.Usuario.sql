USE [FriendFinder];
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Usuario] WHERE [Login] = 'cubo')
BEGIN
	INSERT INTO [dbo].[Usuario]([Login], Nome, Senha)
	VALUES ('cubo', 'Usuário de testes', '89794b621a313bb59eed0d9f0f4e8205'); --SENHA: 123mudar
END
GO