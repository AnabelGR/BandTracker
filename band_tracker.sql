USE [master]
GO
/****** Object:  Database [band_tracker]    Script Date: 6/16/2017 4:01:18 PM ******/
CREATE DATABASE [band_tracker]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'band_tracker', FILENAME = N'C:\Users\epicodus\band_tracker.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'band_tracker_log', FILENAME = N'C:\Users\epicodus\band_tracker_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [band_tracker] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [band_tracker].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [band_tracker] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [band_tracker] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [band_tracker] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [band_tracker] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [band_tracker] SET ARITHABORT OFF 
GO
ALTER DATABASE [band_tracker] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [band_tracker] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [band_tracker] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [band_tracker] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [band_tracker] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [band_tracker] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [band_tracker] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [band_tracker] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [band_tracker] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [band_tracker] SET  ENABLE_BROKER 
GO
ALTER DATABASE [band_tracker] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [band_tracker] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [band_tracker] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [band_tracker] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [band_tracker] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [band_tracker] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [band_tracker] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [band_tracker] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [band_tracker] SET  MULTI_USER 
GO
ALTER DATABASE [band_tracker] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [band_tracker] SET DB_CHAINING OFF 
GO
ALTER DATABASE [band_tracker] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [band_tracker] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [band_tracker] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [band_tracker] SET QUERY_STORE = OFF
GO
USE [band_tracker]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [band_tracker]
GO
/****** Object:  Table [dbo].[bands]    Script Date: 6/16/2017 4:01:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bands](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[bands_concerts]    Script Date: 6/16/2017 4:01:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bands_concerts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[bands_id] [int] NULL,
	[concerts_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[concerts]    Script Date: 6/16/2017 4:01:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[concerts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[showDate] [datetime] NULL,
	[venues_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[venues]    Script Date: 6/16/2017 4:01:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[venues_bands]    Script Date: 6/16/2017 4:01:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venues_bands](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[bands_id] [int] NULL,
	[venues_id] [int] NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[bands] ON 

INSERT [dbo].[bands] ([id], [name]) VALUES (1, N'Jack White')
INSERT [dbo].[bands] ([id], [name]) VALUES (2, N'Faith No More')
INSERT [dbo].[bands] ([id], [name]) VALUES (3, N'Beyonce')
SET IDENTITY_INSERT [dbo].[bands] OFF
SET IDENTITY_INSERT [dbo].[concerts] ON 

INSERT [dbo].[concerts] ([id], [showDate], [venues_id]) VALUES (1, CAST(N'2000-11-12T00:00:00.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[concerts] OFF
SET IDENTITY_INSERT [dbo].[venues] ON 

INSERT [dbo].[venues] ([id], [name]) VALUES (1, N'Roseland Theater')
INSERT [dbo].[venues] ([id], [name]) VALUES (2, N'Revolution Hall')
INSERT [dbo].[venues] ([id], [name]) VALUES (3, N'Dantes')
INSERT [dbo].[venues] ([id], [name]) VALUES (4, N'Paris Theater')
INSERT [dbo].[venues] ([id], [name]) VALUES (5, N'Century Link Field')
SET IDENTITY_INSERT [dbo].[venues] OFF
SET IDENTITY_INSERT [dbo].[venues_bands] ON 

INSERT [dbo].[venues_bands] ([id], [bands_id], [venues_id]) VALUES (1, 1, 1)
INSERT [dbo].[venues_bands] ([id], [bands_id], [venues_id]) VALUES (2, 1, 2)
INSERT [dbo].[venues_bands] ([id], [bands_id], [venues_id]) VALUES (3, 2, 2)
INSERT [dbo].[venues_bands] ([id], [bands_id], [venues_id]) VALUES (4, 2, 2)
INSERT [dbo].[venues_bands] ([id], [bands_id], [venues_id]) VALUES (5, 3, 5)
SET IDENTITY_INSERT [dbo].[venues_bands] OFF
USE [master]
GO
ALTER DATABASE [band_tracker] SET  READ_WRITE 
GO
