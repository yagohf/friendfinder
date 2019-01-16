USE [FriendFinder];
GO

DECLARE @IdUsuarioTestes INT = (SELECT Id FROM [dbo].[Usuario] WHERE [Login] = 'cubo');

IF(@IdUsuarioTestes > 0)
BEGIN
	INSERT [dbo].[Amigo] ( [IdUsuario], [Nome], [Latitude], [Longitude]) VALUES (@IdUsuarioTestes, N'Mauri', CAST(-12.524853 AS Decimal(8, 6)), CAST(-51.516763 AS Decimal(9, 6)))
	INSERT [dbo].[Amigo] ( [IdUsuario], [Nome], [Latitude], [Longitude]) VALUES (@IdUsuarioTestes, N'Joaquim', CAST(-13.253095 AS Decimal(8, 6)), CAST(-43.518717 AS Decimal(9, 6)))
	INSERT [dbo].[Amigo] ( [IdUsuario], [Nome], [Latitude], [Longitude]) VALUES (@IdUsuarioTestes, N'Manoel', CAST(-18.575753 AS Decimal(8, 6)), CAST(-48.423015 AS Decimal(9, 6)))
	INSERT [dbo].[Amigo] ( [IdUsuario], [Nome], [Latitude], [Longitude]) VALUES (@IdUsuarioTestes, N'Antonio', CAST(-16.733323 AS Decimal(8, 6)), CAST(-57.414228 AS Decimal(9, 6)))
	INSERT [dbo].[Amigo] ( [IdUsuario], [Nome], [Latitude], [Longitude]) VALUES (@IdUsuarioTestes, N'Leandro', CAST(-23.092159 AS Decimal(8, 6)), CAST(-47.728682 AS Decimal(9, 6)))
	INSERT [dbo].[Amigo] ( [IdUsuario], [Nome], [Latitude], [Longitude]) VALUES ( @IdUsuarioTestes, N'Jair', CAST(-9.882061 AS Decimal(8, 6)), CAST(-61.023864 AS Decimal(9, 6)))
	INSERT [dbo].[Amigo] ( [IdUsuario], [Nome], [Latitude], [Longitude]) VALUES ( @IdUsuarioTestes, N'Laura', CAST(-7.083234 AS Decimal(8, 6)), CAST(-43.559998 AS Decimal(9, 6)))
	INSERT [dbo].[Amigo] ( [IdUsuario], [Nome], [Latitude], [Longitude]) VALUES ( @IdUsuarioTestes, N'Maria', CAST(-3.916651 AS Decimal(8, 6)), CAST(-48.508242 AS Decimal(9, 6)))
	INSERT [dbo].[Amigo] ( [IdUsuario], [Nome], [Latitude], [Longitude]) VALUES ( @IdUsuarioTestes, N'Andreia', CAST(-19.799581 AS Decimal(8, 6)), CAST(-52.182071 AS Decimal(9, 6)))
	INSERT [dbo].[Amigo] ( [IdUsuario], [Nome], [Latitude], [Longitude]) VALUES ( @IdUsuarioTestes, N'Paula', CAST(-17.531226 AS Decimal(8, 6)), CAST(-42.507974 AS Decimal(9, 6)))
	INSERT [dbo].[Amigo] ( [IdUsuario], [Nome], [Latitude], [Longitude]) VALUES ( @IdUsuarioTestes, N'Flavio', CAST(-20.768746 AS Decimal(8, 6)), CAST(-43.589031 AS Decimal(9, 6)))
	INSERT [dbo].[Amigo] ( [IdUsuario], [Nome], [Latitude], [Longitude]) VALUES ( @IdUsuarioTestes, N'Carina', CAST(-27.338628 AS Decimal(8, 6)), CAST(-51.231441 AS Decimal(9, 6)))
	INSERT [dbo].[Amigo] ( [IdUsuario], [Nome], [Latitude], [Longitude]) VALUES ( @IdUsuarioTestes, N'Lucas', CAST(-30.150863 AS Decimal(8, 6)), CAST(-53.059568 AS Decimal(9, 6)))
	INSERT [dbo].[Amigo] ( [IdUsuario], [Nome], [Latitude], [Longitude]) VALUES ( @IdUsuarioTestes, N'Jaci', CAST(-3.823193 AS Decimal(8, 6)), CAST(-64.116210 AS Decimal(9, 6)))
	INSERT [dbo].[Amigo] ( [IdUsuario], [Nome], [Latitude], [Longitude]) VALUES ( @IdUsuarioTestes, N'Mariana', CAST(-5.703231 AS Decimal(8, 6)), CAST(-54.724771 AS Decimal(9, 6)))
	INSERT [dbo].[Amigo] ( [IdUsuario], [Nome], [Latitude], [Longitude]) VALUES ( @IdUsuarioTestes, N'Juca', CAST(-9.735569 AS Decimal(8, 6)), CAST(-38.478160 AS Decimal(9, 6)))
	INSERT [dbo].[Amigo] ( [IdUsuario], [Nome], [Latitude], [Longitude]) VALUES ( @IdUsuarioTestes, N'Marcia', CAST(-10.540183 AS Decimal(8, 6)), CAST(-46.678357 AS Decimal(9, 6)))
	INSERT [dbo].[Amigo] ( [IdUsuario], [Nome], [Latitude], [Longitude]) VALUES ( @IdUsuarioTestes, N'Claudia', CAST(-13.723159 AS Decimal(8, 6)), CAST(-39.721843 AS Decimal(9, 6)))
END
GO