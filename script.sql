USE [master]
GO
/****** Object:  Database [webapps]    Script Date: 8/8/2024 4:51:19 PM ******/
CREATE DATABASE [webapps]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'webapps', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\webapps.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'webapps_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\webapps_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [webapps] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [webapps].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [webapps] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [webapps] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [webapps] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [webapps] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [webapps] SET ARITHABORT OFF 
GO
ALTER DATABASE [webapps] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [webapps] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [webapps] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [webapps] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [webapps] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [webapps] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [webapps] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [webapps] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [webapps] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [webapps] SET  ENABLE_BROKER 
GO
ALTER DATABASE [webapps] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [webapps] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [webapps] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [webapps] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [webapps] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [webapps] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [webapps] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [webapps] SET RECOVERY FULL 
GO
ALTER DATABASE [webapps] SET  MULTI_USER 
GO
ALTER DATABASE [webapps] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [webapps] SET DB_CHAINING OFF 
GO
ALTER DATABASE [webapps] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [webapps] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [webapps] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [webapps] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'webapps', N'ON'
GO
ALTER DATABASE [webapps] SET QUERY_STORE = ON
GO
ALTER DATABASE [webapps] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [webapps]
GO
/****** Object:  Table [dbo].[Chocolate]    Script Date: 8/8/2024 4:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chocolate](
	[ImageFile] [varbinary](max) NULL,
	[ImageName] [nvarchar](max) NULL,
	[ImagePath] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [decimal](18, 2) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Chocolated]    Script Date: 8/8/2024 4:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chocolated](
	[ImageName] [nvarchar](255) NOT NULL,
	[ImagePath] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[ImageName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Chocolates]    Script Date: 8/8/2024 4:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chocolates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ImageFile] [varbinary](max) NULL,
	[ImageName] [nvarchar](max) NULL,
	[ImagePath] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DryFruit]    Script Date: 8/8/2024 4:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DryFruit](
	[ImageName] [nvarchar](255) NOT NULL,
	[ImagePath] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[ImageName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DryFruits]    Script Date: 8/8/2024 4:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DryFruits](
	[ImageFile] [varbinary](max) NULL,
	[ImageName] [nvarchar](max) NULL,
	[ImagePath] [nvarchar](max) NULL,
	[Description] [nvarchar](500) NULL,
	[Price] [decimal](18, 0) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inventory]    Script Date: 8/8/2024 4:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](255) NOT NULL,
	[ProductType] [nvarchar](50) NOT NULL,
	[QuantitySold] [int] NOT NULL,
	[QuantityAvailable] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 8/8/2024 4:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[ImageUrl] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Spice]    Script Date: 8/8/2024 4:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Spice](
	[ImageName] [nvarchar](255) NOT NULL,
	[ImagePath] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[ImageName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Spices]    Script Date: 8/8/2024 4:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Spices](
	[ImageFile] [varbinary](max) NULL,
	[ImageName] [nvarchar](max) NULL,
	[ImagePath] [nvarchar](max) NULL,
	[Description] [nvarchar](500) NULL,
	[Price] [decimal](18, 0) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Userd]    Script Date: 8/8/2024 4:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Userd](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](64) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Gender] [nvarchar](10) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[Pincode] [nvarchar](6) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRegistrationDB]    Script Date: 8/8/2024 4:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRegistrationDB](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Gender] [nvarchar](10) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[Pincode] [nvarchar](20) NOT NULL,
	[OTP] [nvarchar](6) NOT NULL,
	[Role] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRegistrationDBS]    Script Date: 8/8/2024 4:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRegistrationDBS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Gender] [nvarchar](10) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[Pincode] [nvarchar](20) NOT NULL,
	[OTP] [nvarchar](255) NULL,
	[Role] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 8/8/2024 4:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](64) NULL,
	[DateOfBirth] [date] NULL,
	[Gender] [nvarchar](10) NULL,
	[Email] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[Pincode] [nvarchar](6) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usert]    Script Date: 8/8/2024 4:51:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usert](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](256) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[Password] [nvarchar](256) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Gender] [nvarchar](10) NOT NULL,
	[Address] [nvarchar](256) NOT NULL,
	[Pincode] [nvarchar](10) NOT NULL,
	[OTP] [nvarchar](6) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [webapps] SET  READ_WRITE 
GO
