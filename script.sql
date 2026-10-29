USE [master]
GO
/****** Object:  Database [MatchAndRoll]    Script Date: 29/10/2026 11:13:04 ******/
CREATE DATABASE [MatchAndRoll]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MatchAndRoll', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MatchAndRoll.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MatchAndRoll_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MatchAndRoll_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [MatchAndRoll] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MatchAndRoll].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MatchAndRoll] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MatchAndRoll] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MatchAndRoll] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MatchAndRoll] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MatchAndRoll] SET ARITHABORT OFF 
GO
ALTER DATABASE [MatchAndRoll] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MatchAndRoll] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MatchAndRoll] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MatchAndRoll] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MatchAndRoll] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MatchAndRoll] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MatchAndRoll] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MatchAndRoll] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MatchAndRoll] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MatchAndRoll] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MatchAndRoll] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MatchAndRoll] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MatchAndRoll] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MatchAndRoll] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MatchAndRoll] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MatchAndRoll] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MatchAndRoll] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MatchAndRoll] SET RECOVERY FULL 
GO
ALTER DATABASE [MatchAndRoll] SET  MULTI_USER 
GO
ALTER DATABASE [MatchAndRoll] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MatchAndRoll] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MatchAndRoll] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MatchAndRoll] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MatchAndRoll] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MatchAndRoll', N'ON'
GO
ALTER DATABASE [MatchAndRoll] SET QUERY_STORE = OFF
GO
USE [MatchAndRoll]
GO
/****** Object:  User [alumno]    Script Date: 29/10/2026 11:13:04 ******/
CREATE USER [alumno] FOR LOGIN [alumno] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Chats]    Script Date: 29/10/2026 11:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chats](
	[idChat] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario1] [int] NOT NULL,
	[idUsuario2] [int] NOT NULL,
	[fechaCreacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idChat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Etiquetas]    Script Date: 29/10/2026 11:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Etiquetas](
	[idEtiqueta] [int] IDENTITY(1,1) NOT NULL,
	[nombreEtiqueta] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Etiquetas] PRIMARY KEY CLUSTERED 
(
	[idEtiqueta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Matches]    Script Date: 29/10/2026 11:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Matches](
	[idMatch] [int] IDENTITY(1,1) NOT NULL,
	[idUsuarioSolicitante] [int] NOT NULL,
	[idUsuarioReceptor] [int] NOT NULL,
	[fechaMatch] [datetime] NOT NULL,
	[estadoMatch] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idMatch] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mensajes]    Script Date: 29/10/2026 11:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mensajes](
	[idMensaje] [int] IDENTITY(1,1) NOT NULL,
	[idChat] [int] NOT NULL,
	[idUsuarioEmisor] [int] NOT NULL,
	[contenido] [varchar](1000) NOT NULL,
	[fechaEnvio] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idMensaje] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProyectosMusicales]    Script Date: 29/10/2026 11:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProyectosMusicales](
	[idProyecto] [int] IDENTITY(1,1) NOT NULL,
	[idUsuarioCreador] [int] NOT NULL,
	[nombreProyecto] [varchar](100) NOT NULL,
	[descripcion] [varchar](500) NULL,
	[enlaceDemostracion] [varchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[idProyecto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservas]    Script Date: 29/10/2026 11:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservas](
	[idReserva] [int] IDENTITY(1,1) NOT NULL,
	[idSala] [int] NOT NULL,
	[idUsuario] [int] NOT NULL,
	[fechaHoraInicio] [datetime] NOT NULL,
	[fechaHoraFin] [datetime] NOT NULL,
	[montoTotal] [int] NOT NULL,
	[estadoReserva] [varchar](30) NOT NULL,
	[resenaComentario] [varchar](500) NULL,
	[puntajeResena] [int] NULL,
 CONSTRAINT [PK__Reservas__94D104C8D001A56E] PRIMARY KEY CLUSTERED 
(
	[idReserva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalasEnsayo]    Script Date: 29/10/2026 11:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalasEnsayo](
	[idSala] [int] IDENTITY(1,1) NOT NULL,
	[idUsuarioDueno] [int] NOT NULL,
	[idUbicacion] [int] NOT NULL,
	[nombreSala] [varchar](100) NOT NULL,
	[direccion] [varchar](200) NOT NULL,
	[precioPorHora] [decimal](10, 2) NOT NULL,
	[descripcion] [varchar](500) NULL,
	[equipamiento] [varchar](500) NULL,
	[horarioApertura] [time](7) NULL,
	[horarioCierre] [time](7) NULL,
	[duracionTurnoHoras] [int] NULL,
	[resenaPromedio] [decimal](3, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[idSala] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ubicaciones]    Script Date: 29/10/2026 11:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ubicaciones](
	[idUbicacion] [int] IDENTITY(1,1) NOT NULL,
	[provincia] [varchar](100) NULL,
	[ciudad] [varchar](100) NULL,
	[barrio] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[idUbicacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsuarioEtiquetas]    Script Date: 29/10/2026 11:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsuarioEtiquetas](
	[idUsuario] [int] NOT NULL,
	[idEtiqueta] [int] NOT NULL,
 CONSTRAINT [PK_UsuarioEtiquetas] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC,
	[idEtiqueta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 29/10/2026 11:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[apellido] [varchar](50) NULL,
	[email] [varchar](100) NOT NULL,
	[contraseña] [varchar](255) NOT NULL,
	[tipoUsuario] [varchar](50) NULL,
	[biografia] [varchar](500) NULL,
	[instrumentoPrincipal] [varchar](100) NULL,
	[nivelExperiencia] [varchar](30) NULL,
	[idUbicacion] [int] NULL,
	[genero] [varchar](50) NOT NULL,
	[apodo] [varchar](50) NOT NULL,
	[edad] [int] NOT NULL,
	[videoPrincipal] [varchar](500) NULL,
 CONSTRAINT [PK__Usuarios__5B65BF9779E2A8E8] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Etiquetas] ON 
GO
INSERT [dbo].[Etiquetas] ([idEtiqueta], [nombreEtiqueta]) VALUES (1, N'Tímido')
GO
INSERT [dbo].[Etiquetas] ([idEtiqueta], [nombreEtiqueta]) VALUES (2, N'Gracioso')
GO
INSERT [dbo].[Etiquetas] ([idEtiqueta], [nombreEtiqueta]) VALUES (3, N'Callado')
GO
INSERT [dbo].[Etiquetas] ([idEtiqueta], [nombreEtiqueta]) VALUES (4, N'Creativo')
GO
INSERT [dbo].[Etiquetas] ([idEtiqueta], [nombreEtiqueta]) VALUES (5, N'Extrovertido')
GO
INSERT [dbo].[Etiquetas] ([idEtiqueta], [nombreEtiqueta]) VALUES (6, N'Responsable')
GO
INSERT [dbo].[Etiquetas] ([idEtiqueta], [nombreEtiqueta]) VALUES (7, N'Tranquilo')
GO
INSERT [dbo].[Etiquetas] ([idEtiqueta], [nombreEtiqueta]) VALUES (8, N'Sociable')
GO
INSERT [dbo].[Etiquetas] ([idEtiqueta], [nombreEtiqueta]) VALUES (9, N'Curioso')
GO
INSERT [dbo].[Etiquetas] ([idEtiqueta], [nombreEtiqueta]) VALUES (10, N'Organizado')
GO
SET IDENTITY_INSERT [dbo].[Etiquetas] OFF
GO
SET IDENTITY_INSERT [dbo].[Matches] ON 
GO
INSERT [dbo].[Matches] ([idMatch], [idUsuarioSolicitante], [idUsuarioReceptor], [fechaMatch], [estadoMatch]) VALUES (1, 51, 1, CAST(N'2026-10-29T10:38:29.933' AS DateTime), N'Match')
GO
INSERT [dbo].[Matches] ([idMatch], [idUsuarioSolicitante], [idUsuarioReceptor], [fechaMatch], [estadoMatch]) VALUES (2, 51, 2, CAST(N'2026-10-29T10:38:31.690' AS DateTime), N'Pendiente')
GO
INSERT [dbo].[Matches] ([idMatch], [idUsuarioSolicitante], [idUsuarioReceptor], [fechaMatch], [estadoMatch]) VALUES (3, 51, 3, CAST(N'2026-10-29T11:03:16.443' AS DateTime), N'Pendiente')
GO
INSERT [dbo].[Matches] ([idMatch], [idUsuarioSolicitante], [idUsuarioReceptor], [fechaMatch], [estadoMatch]) VALUES (4, 51, 4, CAST(N'2026-10-29T11:03:16.627' AS DateTime), N'Pendiente')
GO
INSERT [dbo].[Matches] ([idMatch], [idUsuarioSolicitante], [idUsuarioReceptor], [fechaMatch], [estadoMatch]) VALUES (5, 51, 5, CAST(N'2026-10-29T11:03:16.797' AS DateTime), N'Pendiente')
GO
INSERT [dbo].[Matches] ([idMatch], [idUsuarioSolicitante], [idUsuarioReceptor], [fechaMatch], [estadoMatch]) VALUES (6, 51, 6, CAST(N'2026-10-29T11:03:16.980' AS DateTime), N'Pendiente')
GO
INSERT [dbo].[Matches] ([idMatch], [idUsuarioSolicitante], [idUsuarioReceptor], [fechaMatch], [estadoMatch]) VALUES (7, 51, 7, CAST(N'2026-10-29T11:03:17.157' AS DateTime), N'Pendiente')
GO
INSERT [dbo].[Matches] ([idMatch], [idUsuarioSolicitante], [idUsuarioReceptor], [fechaMatch], [estadoMatch]) VALUES (8, 51, 8, CAST(N'2026-10-29T11:03:17.323' AS DateTime), N'Pendiente')
GO
INSERT [dbo].[Matches] ([idMatch], [idUsuarioSolicitante], [idUsuarioReceptor], [fechaMatch], [estadoMatch]) VALUES (9, 51, 9, CAST(N'2026-10-29T11:03:17.510' AS DateTime), N'Pendiente')
GO
INSERT [dbo].[Matches] ([idMatch], [idUsuarioSolicitante], [idUsuarioReceptor], [fechaMatch], [estadoMatch]) VALUES (10, 51, 21, CAST(N'2026-10-29T11:03:54.683' AS DateTime), N'Pendiente')
GO
SET IDENTITY_INSERT [dbo].[Matches] OFF
GO
SET IDENTITY_INSERT [dbo].[Ubicaciones] ON 
GO
INSERT [dbo].[Ubicaciones] ([idUbicacion], [provincia], [ciudad], [barrio]) VALUES (1, N'Buenos Aires', N'CABA', N'Villa Crespo')
GO
INSERT [dbo].[Ubicaciones] ([idUbicacion], [provincia], [ciudad], [barrio]) VALUES (2, N'Buenos Aires', N'CABA', N'Palermo')
GO
INSERT [dbo].[Ubicaciones] ([idUbicacion], [provincia], [ciudad], [barrio]) VALUES (3, N'Buenos Aires', N'CABA', N'Caballito')
GO
INSERT [dbo].[Ubicaciones] ([idUbicacion], [provincia], [ciudad], [barrio]) VALUES (4, N'Buenos Aires', N'CABA', N'Almagro')
GO
SET IDENTITY_INSERT [dbo].[Ubicaciones] OFF
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (1, 1)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (2, 9)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (3, 8)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (4, 5)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (5, 8)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (6, 6)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (7, 7)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (8, 9)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (9, 4)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (10, 10)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (11, 10)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (12, 2)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (13, 5)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (14, 9)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (15, 8)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (16, 2)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (17, 3)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (18, 1)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (19, 2)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (20, 4)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (21, 7)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (22, 10)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (23, 1)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (24, 9)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (25, 8)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (26, 10)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (27, 4)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (28, 5)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (29, 9)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (30, 7)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (31, 2)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (32, 5)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (33, 2)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (34, 7)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (35, 7)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (36, 9)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (37, 5)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (38, 10)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (39, 5)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (40, 6)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (41, 8)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (42, 6)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (43, 5)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (44, 9)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (45, 9)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (46, 9)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (47, 9)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (48, 10)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (49, 2)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (50, 7)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (51, 3)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (51, 5)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (51, 7)
GO
INSERT [dbo].[UsuarioEtiquetas] ([idUsuario], [idEtiqueta]) VALUES (51, 10)
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (1, N'Lucas', N'Gómez', N'lucas.gomez1@gmail.com', N'pass1234', N'Músico', N'Bajista buscando banda de rock alternativo.', N'Bajo', N'Intermedio', 1, N'Masculino', N'Luquitas', 24, N'https://youtu.be/bwxXcZkfC8Q?si=yYTfU4WGo_rx9tAW')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (2, N'Sofia', N'Martínez', N'sofia.m@hotmail.com', N'sofia2024', N'Músico', N'Vocalista con experiencia en jazz y blues.', N'Voz', N'Avanzado', 2, N'Femenino', N'Sofi', 28, N'https://youtu.be/Mzb05Gh0RVo?si=WVWy9wvwSoI5pFeY')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (3, N'Mateo', N'Fernández', N'mateo.f@yahoo.com', N'mateopass', N'Músico', N'Baterista con sala propia y doble pedal.', N'Batería', N'Avanzado', 3, N'Masculino', N'Matu', 22, N'https://youtu.be/K125X_RFyaE?si=5RRrZDxld2XtaR_a')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (4, N'Camila', N'López', N'cami.lopez@gmail.com', N'cami1234', N'Músico', N'Guitarrista rítmica aprendiendo producción.', N'Guitarra', N'Principiante', 4, N'Femenino', N'Cami', 20, N'https://youtu.be/X7IhwlFIrCY?si=tpZFWdB4gKnZrrd5')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (5, N'Joaquín', N'Díaz', N'joaco.diaz@outlook.com', N'joaco999', N'Músico', N'Tecladista influenciado por el synthwave.', N'Teclado', N'Intermedio', 1, N'Masculino', N'Joaco', 26, N'https://youtu.be/j-5TLouMrM4?si=9OvGivGEfVNaUqsY')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (6, N'Valentina', N'Pérez', N'valen.perez@gmail.com', N'valen2026', N'Músico', N'Violinista clásica explorando el folclore.', N'Violín', N'Avanzado', 2, N'Femenino', N'Valen', 25, N'https://youtu.be/hUtEOEyEk6U?si=vFRwivOmLdmAlS_f')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (7, N'Nicolás', N'Romero', N'nico.romero@hotmail.com', N'nico1234', N'Músico', N'Guitarrista líder, fan de los solos de los 80s.', N'Guitarra', N'Avanzado', 3, N'Masculino', N'Nico', 31, N'https://youtu.be/iAwl_an5XUA?si=3FlZpt7s1wEIuCZh')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (8, N'Mariana', N'Sosa', N'marian.sosa@gmail.com', N'marianpass', N'Músico', N'Cantautora acústica.', N'Voz', N'Intermedio', 4, N'Femenino', N'Mari', 23, N'https://youtu.be/rZcbB5ZaDaI?si=OxgcjM2odfSgxz_l')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (9, N'Agustín', N'Torres', N'agus.torres@gmail.com', N'agus3210', N'Músico', N'Percusionista enfocado en ritmos latinos.', N'Percusión', N'Intermedio', 1, N'Masculino', N'Agus', 29, N'https://youtu.be/3sUENMc-Vc0?si=9zlS8h67mip1sFVf')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (10, N'Lucía', N'Benítez', N'lu.benitez@yahoo.com', N'lulu2024', N'Músico', N'Bajista funk y groove.', N'Bajo', N'Avanzado', 2, N'Femenino', N'Lula', 27, N'https://youtu.be/_0vu-SjbtmU?si=J3CNxRT7FWQ2XXBz')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (11, N'Tomas', N'Ramírez', N'tomi.ramirez@outlook.com', N'tomi5555', N'Músico', N'Baterista buscando proyecto de metal.', N'Batería', N'Avanzado', 3, N'Masculino', N'Tomi', 21, N'https://youtu.be/eY2uwZUm7Oc?si=PGXJMDhyYm1czutF')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (12, N'Martina', N'Flores', N'marti.flores@gmail.com', N'marti123', N'Músico', N'Flautista e interesada en ensamble de viento.', N'Flauta', N'Principiante', 4, N'Femenino', N'Martu', 19, N'https://youtu.be/M0mTOWITHB8?si=C-SruqBj9fDGafpM')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (13, N'Jeffrey', N'Epstein', N'jeff@gmail.com', N'jeff123', N'Músico', N'Productor y saxofonista.', N'Saxofón', N'Avanzado', 1, N'Masculino', N'jeff', 34, N'https://youtu.be/C_AqnLoFMig?si=WWLUT_jcBymBierP')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (14, N'Elena', N'Medina', N'elena.m@gmail.com', N'elena987', N'Músico', N'Pianista clásica abriéndose al pop.', N'Teclado', N'Intermedio', 2, N'Femenino', N'Ele', 26, N'https://youtu.be/9khZ-CksRDY?si=DQmodhYNZvKUT81q')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (15, N'Facundo', N'Castro', N'facu.castro@yahoo.com', N'facu1234', N'Músico', N'Guitarrista rítmico para indie rock.', N'Guitarra', N'Intermedio', 3, N'Masculino', N'Facu', 22, N'https://youtu.be/sfhz6IUiRgs?si=evaW4rDjneCVx_Ze')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (16, N'Clara', N'Mendoza', N'clara.m@gmail.com', N'clarita1', N'Músico', N'Vocalista mezzosoprano.', N'Voz', N'Avanzado', 4, N'Femenino', N'Clari', 28, N'https://youtu.be/9Pg3BoyFQ-k?si=17YK_dS5RAOinTae')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (17, N'Santiago', N'Alvarez', N'santi.alvarez@outlook.com', N'santi2026', N'Músico', N'Bajista de sesión con disponibilidad.', N'Bajo', N'Avanzado', 1, N'Masculino', N'Santi', 30, N'https://youtu.be/QEiVlE4wfOg?si=9i0n9Q8FjYRyLLCU')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (18, N'Victoria', N'Rojas', N'vicky.rojas@hotmail.com', N'vicky123', N'Músico', N'Baterista de indie y pop.', N'Batería', N'Intermedio', 2, N'Femenino', N'Vicky', 24, N'https://youtu.be/2ihxzCYiodA?si=4qfucZGn9rIp_2AC')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (19, N'Gabriel', N'Molina', N'gabi.molina@gmail.com', N'gabi4567', N'Músico', N'Guitarrista acústico y compositor.', N'Guitarra', N'Intermedio', 3, N'Masculino', N'Gabi', 27, N'https://youtu.be/Tu4obryAP34?si=KZczyDhV1HdWBCnh')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (20, N'Paula', N'Ortiz', N'pau.ortiz@yahoo.com', N'paupass', N'Músico', N'Tecladista y corista.', N'Teclado', N'Principiante', 4, N'Femenino', N'Pau', 21, N'https://youtu.be/NNLOZS4ouw0?si=W_joMNdhkqEgj4Ni')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (21, N'Kanye', N'West', N'ye@gmail.com', N'ye123', N'Músico', N'Mejor artista/productor de la historia', N'Voz', N'Avanzado', 1, N'Masculino', N'ye', 33, N'https://youtu.be/_ABk7TmjnVk?si=rnodovn1HSzz-u9Y')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (22, N'Daniela', N'Nuñez', N'dani.nunez@outlook.com', N'dani2024', N'Músico', N'Cellista explorando música experimental.', N'Cello', N'Avanzado', 2, N'Femenino', N'Dani', 29, N'https://youtu.be/MyPt0VAWyKM?si=6HCzQEUjsjB5_wWR')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (23, N'Ignacio', N'Luna', N'nacho.luna@hotmail.com', N'nacho999', N'Músico', N'Baterista de punk rock.', N'Batería', N'Intermedio', 3, N'Masculino', N'Nacho', 20, N'https://youtu.be/ir49RgrGI7w?si=WoVLzI5ndTmuKak_')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (24, N'Abril', N'Juárez', N'abril.j@gmail.com', N'abril123', N'Músico', N'Bajista de pop rock.', N'Bajo', N'Principiante', 4, N'Femenino', N'Abby', 18, N'https://youtu.be/TfIsl6tZI68?si=ZfRNx9XUSyWNs4oP')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (25, N'Manuel', N'Cabrera', N'manu.cabrera@yahoo.com', N'manuPass', N'Músico', N'Guitarrista flamenco y criollo.', N'Guitarra', N'Avanzado', 1, N'Masculino', N'Manu', 35, N'https://youtu.be/F4rGQ7gKYx0?si=apOZ9fwGWm64msPC')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (26, N'Rocío', N'Rios', N'ro.rios@gmail.com', N'ro123456', N'Músico', N'Cantante R&B y soul.', N'Voz', N'Avanzado', 2, N'Femenino', N'Rochi', 25, N'https://youtu.be/f2c_OGRzLv8?si=1Bu8J801vzs5BBmr')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (27, N'Felipe', N'Godoy', N'felipe.g@outlook.com', N'feli2026', N'Músico', N'Productor y DJ que busca vocalistas.', N'Teclado', N'Intermedio', 3, N'Masculino', N'Feli', 23, N'https://youtu.be/lF6bm8J9vlk?si=wXD_ANrc-WRFOlo4')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (28, N'Micaela', N'Morales', N'mica.m@hotmail.com', N'micapass', N'Músico', N'Sintetizadores y diseño sonoro.', N'Teclado', N'Intermedio', 4, N'Femenino', N'Mica', 26, N'https://youtu.be/MIh2EueRmk0?si=f91UHwXV9gUz5ypI')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (29, N'Julian', N'Peralta', N'juli.peralta@gmail.com', N'juli1234', N'Músico', N'Trompetista de ska y regaee.', N'Trompeta', N'Avanzado', 1, N'Masculino', N'Juli', 32, N'https://youtu.be/38vsGG1m8RI?si=guvMT59BA4qm0D7Y')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (30, N'Lola', N'Carrizo', N'lola.c@yahoo.com', N'lolita99', N'Músico', N'Guitarrista rítmica alternativo.', N'Guitarra', N'Principiante', 2, N'Femenino', N'Lola', 19, N'https://youtu.be/FzRIt0VOAIo?si=6G0umozlyUvJgkLT')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (31, N'Esteban', N'Vega', N'esteban.v@gmail.com', N'estebanPass', N'Músico', N'Baterista de jazz fusion.', N'Batería', N'Avanzado', 3, N'Masculino', N'Es', 38, N'https://youtu.be/VH3QTjcfUrc?si=m1-wt9dVypbcD0Mr')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (32, N'Julieta', N'Quiroga', N'julieta.q@outlook.com', N'julieta12', N'Músico', N'Vocalista soprano para rock sinfónico.', N'Voz', N'Avanzado', 4, N'Femenino', N'Juli', 27, N'https://youtu.be/hxvBjtsXV64?si=L0JtFDzG4lixvjn2')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (33, N'Bruno', N'Navarro', N'bruno.n@hotmail.com', N'bruno123', N'Músico', N'Bajista de hard rock.', N'Bajo', N'Intermedio', 1, N'Masculino', N'Bruno', 28, N'https://youtu.be/76yAo3aHPmM?si=ELc-6IJJomg_Skji')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (34, N'Florencia', N'Correa', N'flor.c@gmail.com', N'flor2024', N'Músico', N'Ukulele y voz acústica.', N'Voz', N'Principiante', 2, N'Femenino', N'Flor', 21, N'https://youtu.be/OoepEabGb9E?si=HxrJyL-xK8IudTw-')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (35, N'Rodrigo', N'Ledesma', N'rodri.l@yahoo.com', N'rodripass', N'Músico', N'Guitarrista de blues tradicional.', N'Guitarra', N'Avanzado', 3, N'Masculino', N'Rodri', 40, N'https://youtu.be/us5xN06GhKU?si=9x4uNQUbv7hLukk_')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (36, N'Sol', N'Mercado', N'sol.m@gmail.com', N'solcito1', N'Músico', N'Tecladista e influencias de funk.', N'Teclado', N'Intermedio', 4, N'Femenino', N'Sol', 23, N'https://youtu.be/YpCeD61WAsU?si=0LZ1yJyIbJEMP2FN')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (37, N'Federico', N'Aguirre', N'fede.a@outlook.com', N'fede1234', N'Músico', N'Baterista versátil para sesionar.', N'Batería', N'Avanzado', 1, N'Masculino', N'Fede', 29, N'https://youtu.be/1YACQ8Lc1Tw?si=qBTjrZM7K3Z4kRSE')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (38, N'Milagros', N'Duarte', N'mili.d@hotmail.com', N'miliPass', N'Músico', N'Vocalista y compositora.', N'Voz', N'Intermedio', 2, N'Femenino', N'Mili', 22, N'https://youtu.be/QwPjG3IcRBM?list=PLzav_cAW0Ug9VZ8_FjENtSiP401tZVjwA')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (39, N'Leandro', N'Soria', N'lea.soria@gmail.com', N'lea12345', N'Músico', N'Bajista de disco y funk.', N'Bajo', N'Intermedio', 3, N'Masculino', N'Lea', 30, N'https://youtu.be/ZUzy5EmXixg?si=dD0Tg0n2GLStRfUX')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (40, N'Candela', N'Ojeda', N'cande.o@yahoo.com', N'cande2026', N'Músico', N'Guitarrista solista.', N'Guitarra', N'Intermedio', 4, N'Femenino', N'Cande', 24, N'https://youtu.be/vXHOorXZ8qY?si=KXqxdPsKkHhhmL-H')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (41, N'Marcos', N'Valenzuela', N'marcos.v@gmail.com', N'marcos12', N'Músico', N'Productor y beatmaker.', N'Teclado', N'Avanzado', 1, N'Masculino', N'Marquitos', 26, N'https://youtu.be/VG4HvhzKhwE?si=i_zLAoNi7m8bXgNp')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (42, N'Delfina', N'Bustos', N'delfi.b@outlook.com', N'delfi123', N'Músico', N'Vocalista pop y urbana.', N'Voz', N'Intermedio', 2, N'Femenino', N'Delfi', 20, N'https://youtu.be/jPulLuBRfWQ?list=PLzav_cAW0Ug9VZ8_FjENtSiP401tZVjwA')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (43, N'Ezequiel', N'Nunez', N'eze.n@hotmail.com', N'ezepass1', N'Músico', N'Guitarrista de post-punk.', N'Guitarra', N'Intermedio', 3, N'Masculino', N'Eze', 25, N'https://youtu.be/W6qHrdqWT_A?si=7dVYFJiI1CI4daW-')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (44, N'Guadalupe', N'Franco', N'guada.f@gmail.com', N'guada2024', N'Músico', N'Baterista de pop rock.', N'Batería', N'Principiante', 4, N'Femenino', N'Guada', 19, N'https://youtu.be/PC-ZdPrFzdk?si=efqEKeC1AZCguB7T')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (45, N'Kevin', N'Sosa', N'kevin.s@yahoo.com', N'kevin1234', N'Músico', N'Bajista de reggae y dub.', N'Bajo', N'Avanzado', 1, N'Masculino', N'Kev', 27, N'https://youtu.be/RTthLmEYLbg?si=_EJv6lqxwqkOQbbs')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (46, N'Antonella', N'Roldan', N'anto.r@gmail.com', N'antopass', N'Músico', N'Tecladista con piano clásico.', N'Teclado', N'Avanzado', 2, N'Femenino', N'Anto', 28, N'https://youtu.be/WFsdhTDezg4?si=xUFTzEqM4RlIQ6t8')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (47, N'Luciano', N'Paz', N'lucho.paz@outlook.com', N'lucho123', N'Músico', N'Vocalista y segunda guitarra.', N'Guitarra', N'Intermedio', 3, N'Masculino', N'Lucho', 23, N'https://youtu.be/xpRgR-tZEpk?si=_y-NyZEpDcKH4NMC')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (48, N'Catalina', N'Molina', N'cata.m@hotmail.com', N'catita12', N'Músico', N'Sintetizadores e instrumentos virtuales.', N'Teclado', N'Principiante', 4, N'Femenino', N'Cata', 21, N'https://youtu.be/3Li1LnqfxQU?si=4TwvS2GI3rCiM3_Y')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (49, N'Ivan', N'Ferreyra', N'ivan.f@gmail.com', N'ivanpass', N'Músico', N'Baterista sesionista.', N'Batería', N'Avanzado', 1, N'Masculino', N'Ivan', 9, N'https://youtu.be/EqPRRkFQnk0?si=zsKsC3IXt2RqOl0g')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (50, N'Brisa', N'Gallardo', N'bri.gallardo@yahoo.com', N'brisa2026', N'Músico', N'Cantante de rock nacional.', N'Voz', N'Intermedio', 2, N'Femenino', N'Bri', 22, N'https://youtu.be/La6ePTfimk4?si=oOzAo3u7PgBaVfSF')
GO
INSERT [dbo].[Usuarios] ([idUsuario], [nombre], [apellido], [email], [contraseña], [tipoUsuario], [biografia], [instrumentoPrincipal], [nivelExperiencia], [idUbicacion], [genero], [apodo], [edad], [videoPrincipal]) VALUES (51, N'MÁXIMO', N'AGUILERA', N'r@gmail.com', N'123', N'Músico', N'hola hola', N'Guitarra', N'Profesional', 3, N'Masculino', N'r', 19, N'https://youtu.be/hBeur29GlfI?si=khPveKkiep-Z3REw')
GO
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Usuarios__A9D1053471765528]    Script Date: 29/10/2026 11:13:04 ******/
ALTER TABLE [dbo].[Usuarios] ADD  CONSTRAINT [UQ__Usuarios__A9D1053471765528] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Chats] ADD  DEFAULT (getdate()) FOR [fechaCreacion]
GO
ALTER TABLE [dbo].[Mensajes] ADD  DEFAULT (getdate()) FOR [fechaEnvio]
GO
ALTER TABLE [dbo].[Chats]  WITH CHECK ADD FOREIGN KEY([idUsuario1])
REFERENCES [dbo].[Usuarios] ([idUsuario])
GO
ALTER TABLE [dbo].[Chats]  WITH CHECK ADD FOREIGN KEY([idUsuario2])
REFERENCES [dbo].[Usuarios] ([idUsuario])
GO
ALTER TABLE [dbo].[Matches]  WITH CHECK ADD  CONSTRAINT [FK_Match_UsuarioReceptor] FOREIGN KEY([idUsuarioReceptor])
REFERENCES [dbo].[Usuarios] ([idUsuario])
GO
ALTER TABLE [dbo].[Matches] CHECK CONSTRAINT [FK_Match_UsuarioReceptor]
GO
ALTER TABLE [dbo].[Matches]  WITH CHECK ADD  CONSTRAINT [FK_Match_UsuarioSolicitante] FOREIGN KEY([idUsuarioSolicitante])
REFERENCES [dbo].[Usuarios] ([idUsuario])
GO
ALTER TABLE [dbo].[Matches] CHECK CONSTRAINT [FK_Match_UsuarioSolicitante]
GO
ALTER TABLE [dbo].[Mensajes]  WITH CHECK ADD FOREIGN KEY([idChat])
REFERENCES [dbo].[Chats] ([idChat])
GO
ALTER TABLE [dbo].[Mensajes]  WITH CHECK ADD FOREIGN KEY([idUsuarioEmisor])
REFERENCES [dbo].[Usuarios] ([idUsuario])
GO
ALTER TABLE [dbo].[ProyectosMusicales]  WITH CHECK ADD  CONSTRAINT [FK_Proyecto_Usuario] FOREIGN KEY([idUsuarioCreador])
REFERENCES [dbo].[Usuarios] ([idUsuario])
GO
ALTER TABLE [dbo].[ProyectosMusicales] CHECK CONSTRAINT [FK_Proyecto_Usuario]
GO
ALTER TABLE [dbo].[Reservas]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Sala] FOREIGN KEY([idSala])
REFERENCES [dbo].[SalasEnsayo] ([idSala])
GO
ALTER TABLE [dbo].[Reservas] CHECK CONSTRAINT [FK_Reserva_Sala]
GO
ALTER TABLE [dbo].[Reservas]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Usuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[Usuarios] ([idUsuario])
GO
ALTER TABLE [dbo].[Reservas] CHECK CONSTRAINT [FK_Reserva_Usuario]
GO
ALTER TABLE [dbo].[SalasEnsayo]  WITH CHECK ADD  CONSTRAINT [FK_Sala_Ubicacion] FOREIGN KEY([idUbicacion])
REFERENCES [dbo].[Ubicaciones] ([idUbicacion])
GO
ALTER TABLE [dbo].[SalasEnsayo] CHECK CONSTRAINT [FK_Sala_Ubicacion]
GO
ALTER TABLE [dbo].[SalasEnsayo]  WITH CHECK ADD  CONSTRAINT [FK_Sala_Usuario] FOREIGN KEY([idUsuarioDueno])
REFERENCES [dbo].[Usuarios] ([idUsuario])
GO
ALTER TABLE [dbo].[SalasEnsayo] CHECK CONSTRAINT [FK_Sala_Usuario]
GO
ALTER TABLE [dbo].[UsuarioEtiquetas]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioEtiquetas_Etiquetas] FOREIGN KEY([idEtiqueta])
REFERENCES [dbo].[Etiquetas] ([idEtiqueta])
GO
ALTER TABLE [dbo].[UsuarioEtiquetas] CHECK CONSTRAINT [FK_UsuarioEtiquetas_Etiquetas]
GO
ALTER TABLE [dbo].[UsuarioEtiquetas]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioEtiquetas_Usuarios] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[Usuarios] ([idUsuario])
GO
ALTER TABLE [dbo].[UsuarioEtiquetas] CHECK CONSTRAINT [FK_UsuarioEtiquetas_Usuarios]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Ubicacion] FOREIGN KEY([idUbicacion])
REFERENCES [dbo].[Ubicaciones] ([idUbicacion])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuario_Ubicacion]
GO
ALTER TABLE [dbo].[Chats]  WITH CHECK ADD CHECK  (([idUsuario1]<>[idUsuario2]))
GO
/****** Object:  StoredProcedure [dbo].[sp_CrearProyectoMusical]    Script Date: 29/10/2026 11:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_CrearProyectoMusical]
    @idUsuarioCreador INT,
    @nombreProyecto VARCHAR(100),
    @descripcion VARCHAR(500),
    @enlaceDemostracion VARCHAR(300)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.ProyectosMusicales (idUsuarioCreador, nombreProyecto, descripcion, enlaceDemostracion)
    VALUES (@idUsuarioCreador, @nombreProyecto, @descripcion, @enlaceDemostracion);

    SELECT 'OK' AS Estado, 'Proyecto creado' AS Mensaje;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CrearReserva]    Script Date: 29/10/2026 11:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_CrearReserva]
    @idSala INT,
    @idUsuario INT,
    @fechaHoraInicio DATETIME,
    @fechaHoraFin DATETIME,
    @montoTotal INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT * FROM dbo.SalasEnsayo WHERE idSala = @idSala)
    BEGIN
        IF NOT EXISTS (
            SELECT * FROM dbo.Reservas
            WHERE idSala = @idSala 
              AND estadoReserva <> 'Cancelada'
              AND (@fechaHoraInicio < fechaHoraFin AND @fechaHoraFin > fechaHoraInicio)
        )
        BEGIN
            INSERT INTO dbo.Reservas (
                idSala, idUsuario, fechaHoraInicio, fechaHoraFin, montoTotal, estadoReserva
            )
            VALUES (
                @idSala, @idUsuario, @fechaHoraInicio, @fechaHoraFin, @montoTotal, 'Pendiente'
            );

            SELECT 'OK' AS Estado, 'Reserva creada' AS Mensaje;
        END
        ELSE
        BEGIN
            SELECT 'ERROR' AS Estado, 'Horario ocupado' AS Mensaje;
        END
    END
    ELSE
    BEGIN
        SELECT 'ERROR' AS Estado, 'La sala no existe' AS Mensaje;
    END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerPerfilUsuario]    Script Date: 29/10/2026 11:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ObtenerPerfilUsuario]
    @idUsuario INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        u.idUsuario,
        u.nombre,
        u.apellido,
        u.apodo,
        u.email,
        u.tipoUsuario,
        u.biografia,
        u.instrumentoPrincipal,
        u.nivelExperiencia,
        u.genero,
        u.edad,
        u.videoPrincipal,
        ub.provincia,
        ub.ciudad,
        ub.barrio
    FROM dbo.Usuarios u
    LEFT JOIN dbo.Ubicaciones ub ON u.idUbicacion = ub.idUbicacion
    WHERE u.idUsuario = @idUsuario;

    SELECT 
        e.idEtiqueta,
        e.nombreEtiqueta
    FROM dbo.UsuarioEtiquetas ue
    INNER JOIN dbo.Etiquetas e ON ue.idEtiqueta = e.idEtiqueta
    WHERE ue.idUsuario = @idUsuario;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarInteraccionMatch]    Script Date: 29/10/2026 11:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_RegistrarInteraccionMatch]
    @idUsuarioSolicitante INT,
    @idUsuarioReceptor INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @idMatchExistente INT;
    DECLARE @estadoExistente VARCHAR(30);

    SELECT TOP 1 
        @idMatchExistente = idMatch,
        @estadoExistente = estadoMatch
    FROM dbo.Matches
    WHERE (idUsuarioSolicitante = @idUsuarioSolicitante AND idUsuarioReceptor = @idUsuarioReceptor)
       OR (idUsuarioSolicitante = @idUsuarioReceptor AND idUsuarioReceptor = @idUsuarioSolicitante);

    IF @idMatchExistente IS NULL
    BEGIN
        INSERT INTO dbo.Matches (idUsuarioSolicitante, idUsuarioReceptor, fechaMatch, estadoMatch)
        VALUES (@idUsuarioSolicitante, @idUsuarioReceptor, GETDATE(), 'Pendiente');

        SELECT 'SolicitudEnviada' AS Resultado, 'Like registrado' AS Mensaje;
    END
    ELSE IF @estadoExistente = 'Pendiente'
    BEGIN
        UPDATE dbo.Matches
        SET estadoMatch = 'Match',
            fechaMatch = GETDATE()
        WHERE idMatch = @idMatchExistente;

        SELECT 'MatchExitoso' AS Resultado, 'Match generado' AS Mensaje;
    END
    ELSE
    BEGIN
        SELECT 'YaExisteMatch' AS Resultado, 'Ya tienen match' AS Mensaje;
    END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarUsuario]    Script Date: 29/10/2026 11:13:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_RegistrarUsuario]
    @nombre VARCHAR(50),
    @apellido VARCHAR(50),
    @email VARCHAR(100),
    @contraseña VARCHAR(255),
    @tipoUsuario VARCHAR(50),
    @biografia VARCHAR(500),
    @instrumentoPrincipal VARCHAR(100),
    @nivelExperiencia VARCHAR(30),
    @idUbicacion INT,
    @genero VARCHAR(50),
    @apodo VARCHAR(50),
    @edad INT,
    @videoPrincipal VARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT * FROM dbo.Usuarios WHERE email = @email)
    BEGIN
        INSERT INTO dbo.Usuarios (
            nombre, apellido, email, contraseña, tipoUsuario, biografia,
            instrumentoPrincipal, nivelExperiencia, idUbicacion, genero,
            apodo, edad, videoPrincipal
        )
        VALUES (
            @nombre, @apellido, @email, @contraseña, @tipoUsuario, @biografia,
            @instrumentoPrincipal, @nivelExperiencia, @idUbicacion, @genero,
            @apodo, @edad, @videoPrincipal
        );

        SELECT 'OK' AS Estado, 'Usuario registrado con éxito' AS Mensaje;
    END
    ELSE
    BEGIN
        SELECT 'ERROR' AS Estado, 'El correo electrónico ya existe' AS Mensaje;
    END
END
GO
USE [master]
GO
ALTER DATABASE [MatchAndRoll] SET  READ_WRITE 
GO
