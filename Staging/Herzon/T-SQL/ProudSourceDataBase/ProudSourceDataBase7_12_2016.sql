USE [master]
GO
/****** Object:  Database [ProudSourceDataBase]    Script Date: 7/12/2016 2:17:06 PM ******/
CREATE DATABASE [ProudSourceDataBase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProudSourceDataBase', FILENAME = N'D:\RDSDBDATA\DATA\ProudSourceDataBase.mdf' , SIZE = 14976KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 LOG ON 
( NAME = N'ProudSourceDataBase_log', FILENAME = N'D:\RDSDBDATA\DATA\ProudSourceDataBase_log.ldf' , SIZE = 7616KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ProudSourceDataBase] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProudSourceDataBase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProudSourceDataBase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProudSourceDataBase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProudSourceDataBase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProudSourceDataBase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProudSourceDataBase] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProudSourceDataBase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProudSourceDataBase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProudSourceDataBase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProudSourceDataBase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProudSourceDataBase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProudSourceDataBase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProudSourceDataBase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProudSourceDataBase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProudSourceDataBase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProudSourceDataBase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ProudSourceDataBase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProudSourceDataBase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProudSourceDataBase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProudSourceDataBase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProudSourceDataBase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProudSourceDataBase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProudSourceDataBase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProudSourceDataBase] SET RECOVERY FULL 
GO
ALTER DATABASE [ProudSourceDataBase] SET  MULTI_USER 
GO
ALTER DATABASE [ProudSourceDataBase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProudSourceDataBase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProudSourceDataBase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProudSourceDataBase] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ProudSourceDataBase] SET DELAYED_DURABILITY = DISABLED 
GO
USE [ProudSourceDataBase]
GO
/****** Object:  User [PSSqlDeveloper]    Script Date: 7/12/2016 2:17:08 PM ******/
CREATE USER [PSSqlDeveloper] FOR LOGIN [PSSqlDeveloper] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [PSSqlDeveloper]
GO
/****** Object:  Schema [Utility]    Script Date: 7/12/2016 2:17:08 PM ******/
CREATE SCHEMA [Utility]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 7/12/2016 2:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateTime_Created] [datetime2](7) NOT NULL,
	[Deleted] [bit] NOT NULL,
	[DateTime_Deleted] [datetime2](7) NULL,
	[Frozen] [bit] NOT NULL,
	[DateTime_Frozen] [datetime2](7) NULL,
	[DateTime_Unfrozen] [datetime2](7) NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Accounts_Investors_XREF]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts_Investors_XREF](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Account_Id] [int] NOT NULL,
	[Investor_Id] [int] NOT NULL,
 CONSTRAINT [PK_Accounts_Investors_XREF] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Accounts_Projects_XREF]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts_Projects_XREF](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Account_Id] [int] NOT NULL,
	[Project_Id] [int] NOT NULL,
 CONSTRAINT [PK_Accounts_Projects_XREF] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Category_Types]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category_Types](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](1000) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Currency_Types]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Currency_Types](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Currency] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Currency_Types] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Documents]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Documents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Binary_File] [varbinary](max) NOT NULL,
	[DateTime_Created] [datetime2](7) NOT NULL,
	[File_Name] [varchar](256) NOT NULL,
	[Mime_Type] [varchar](256) NOT NULL,
	[Description] [varchar](1000) NULL,
 CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Embelishments]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Embelishments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Embelishment] [nvarchar](1000) NOT NULL,
	[Embelishment_Type] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](250) NULL,
 CONSTRAINT [PK_Embelishments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Images]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Images](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Binary_Image] [varbinary](max) NOT NULL,
	[DateTime_Created] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PROCs]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PROCs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Investor_Id] [int] NOT NULL,
	[Project_Id] [int] NOT NULL,
	[DateTime_Created] [datetime2](7) NOT NULL,
	[Revenue_Percentage] [decimal](8, 6) NOT NULL,
	[DateTime_Enforcement_Begin] [datetime2](7) NOT NULL,
	[DateTime_Enforcement_End] [datetime2](7) NOT NULL,
	[Investment_Amount] [money] NOT NULL,
	[Active] [bit] NOT NULL,
	[Expired] [bit] NULL,
	[Count_Project_Revised] [int] NOT NULL,
	[Count_Investor_Revised] [int] NOT NULL,
	[Accepted_Project] [bit] NOT NULL,
	[DateTime_Accepted_Project] [datetime2](7) NULL,
	[Accepted_Investor] [bit] NOT NULL,
	[DateTime_Accepted_Investor] [datetime2](7) NULL,
	[Accepted_Mutualy] [bit] NOT NULL,
	[DateTime_MutualyAccepted] [datetime2](7) NULL,
	[Payment_Interval] [int] NULL,
	[Deleted] [bit] NULL,
	[DateTime_Deleted] [datetime2](7) NULL,
	[Revenue_Payout] [money] NULL,
	[DateTime_Last_Payout] [datetime2](7) NULL,
 CONSTRAINT [PK_PROCs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Profile_Types]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Profile_Types](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](256) NOT NULL,
 CONSTRAINT [PK_Profile_Types] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Profiles]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profiles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[User_Id] [nvarchar](128) NOT NULL,
	[ProfileType_Id] [int] NOT NULL,
	[DateTime_Created] [datetime2](7) NOT NULL,
	[DateTime_LastLogon] [datetime2](7) NOT NULL,
	[Profile_Public] [bit] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[DateTime_Deleted] [datetime2](7) NULL,
	[Verified] [bit] NOT NULL,
	[DateTime_Verified] [datetime2](7) NULL,
 CONSTRAINT [PK_Profiles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Profiles_Documents_XREF]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profiles_Documents_XREF](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Profile_Id] [int] NOT NULL,
	[Document_Id] [int] NOT NULL,
 CONSTRAINT [PK_Profiles_Documents_XREF] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Profiles_Embelishments_XREF]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profiles_Embelishments_XREF](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Profile_Id] [int] NOT NULL,
	[Embelishment_Id] [int] NOT NULL,
 CONSTRAINT [PK_Profiles_Embelishments_XREF] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Profiles_Images_XREF]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profiles_Images_XREF](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Profile_Id] [int] NOT NULL,
	[Image_Id] [int] NOT NULL,
 CONSTRAINT [PK_Profiles_Images_XREF] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Profiles_WebLinks_XREF]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profiles_WebLinks_XREF](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Profile_Id] [int] NOT NULL,
	[WebLink_Id] [int] NOT NULL,
 CONSTRAINT [PK_Profiles_WebLinks_XREF] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Projects]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Entrepreneur_Id] [int] NOT NULL,
	[DateTime_Created] [datetime2](7) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Summary] [varchar](5000) NULL,
	[Profile_Public] [bit] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[DateTime_Deleted] [datetime2](7) NULL,
	[Investment_Goal] [money] NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Projects_Documents_XREF]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects_Documents_XREF](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Project_Id] [int] NOT NULL,
	[Document_Id] [int] NOT NULL,
 CONSTRAINT [PK_Projects_Documents_XREF] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Projects_Embelishments_XREF]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects_Embelishments_XREF](
	[Id] [int] NOT NULL,
	[Project_Id] [int] NOT NULL,
	[Embelishment_Id] [int] NOT NULL,
 CONSTRAINT [PK_Projects_Embelishments_XREF] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Projects_Images_XREF]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects_Images_XREF](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Project_Id] [int] NOT NULL,
	[Images_Id] [int] NOT NULL,
 CONSTRAINT [PK_Projects_Images_XREF] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Projects_WebLinks_XREF]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects_WebLinks_XREF](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Project_Id] [int] NOT NULL,
	[WebLink_Id] [int] NOT NULL,
 CONSTRAINT [PK_Projects_WebLinks_XREF] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Transaction_Types]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Transaction_Types](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](1000) NOT NULL,
 CONSTRAINT [PK_Transaction_Types] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Transactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Account_Id] [int] NOT NULL,
	[Category_Type_Id] [int] NOT NULL,
	[Transaction_Type_Id] [int] NOT NULL,
	[Currency_Type_Id] [int] NOT NULL,
	[DateTime_Created] [datetime2](7) NOT NULL,
	[Description] [varchar](8000) NOT NULL,
	[Amount] [money] NOT NULL,
	[src_Account_Id] [int] NULL,
	[Transaction_State] [varchar](250) NULL,
	[Outside_Transaction_Id] [varchar](250) NULL,
	[Outside_Financial_Platform] [varchar](250) NULL,
	[DateTime_Processed] [datetime2](7) NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WebLinks]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WebLinks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Link_Type] [varchar](256) NULL,
	[Link] [nvarchar](1000) NOT NULL,
	[DateTime_Created] [datetime2](7) NOT NULL,
	[FriendlyName] [nvarchar](256) NULL,
 CONSTRAINT [PK_WebLinks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[WebLinks] ADD  CONSTRAINT [DF_WebLinks_DateTimeCreated]  DEFAULT (getdate()) FOR [DateTime_Created]
GO
ALTER TABLE [dbo].[Accounts_Investors_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Investors_XREF_Accounts] FOREIGN KEY([Account_Id])
REFERENCES [dbo].[Accounts] ([Id])
GO
ALTER TABLE [dbo].[Accounts_Investors_XREF] CHECK CONSTRAINT [FK_Accounts_Investors_XREF_Accounts]
GO
ALTER TABLE [dbo].[Accounts_Investors_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Investors_XREF_Profiles] FOREIGN KEY([Investor_Id])
REFERENCES [dbo].[Profiles] ([Id])
GO
ALTER TABLE [dbo].[Accounts_Investors_XREF] CHECK CONSTRAINT [FK_Accounts_Investors_XREF_Profiles]
GO
ALTER TABLE [dbo].[Accounts_Projects_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Projects_XREF_Accounts] FOREIGN KEY([Account_Id])
REFERENCES [dbo].[Accounts] ([Id])
GO
ALTER TABLE [dbo].[Accounts_Projects_XREF] CHECK CONSTRAINT [FK_Accounts_Projects_XREF_Accounts]
GO
ALTER TABLE [dbo].[Accounts_Projects_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Projects_XREF_Projects] FOREIGN KEY([Project_Id])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[Accounts_Projects_XREF] CHECK CONSTRAINT [FK_Accounts_Projects_XREF_Projects]
GO
ALTER TABLE [dbo].[PROCs]  WITH CHECK ADD  CONSTRAINT [FK_PROCs_Profiles] FOREIGN KEY([Investor_Id])
REFERENCES [dbo].[Profiles] ([Id])
GO
ALTER TABLE [dbo].[PROCs] CHECK CONSTRAINT [FK_PROCs_Profiles]
GO
ALTER TABLE [dbo].[PROCs]  WITH CHECK ADD  CONSTRAINT [FK_PROCs_Projects] FOREIGN KEY([Project_Id])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[PROCs] CHECK CONSTRAINT [FK_PROCs_Projects]
GO
ALTER TABLE [dbo].[Profiles]  WITH CHECK ADD  CONSTRAINT [FK_Profiles_Profile_Types] FOREIGN KEY([ProfileType_Id])
REFERENCES [dbo].[Profile_Types] ([Id])
GO
ALTER TABLE [dbo].[Profiles] CHECK CONSTRAINT [FK_Profiles_Profile_Types]
GO
ALTER TABLE [dbo].[Profiles]  WITH CHECK ADD  CONSTRAINT [FK_Profiles_Users] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Profiles] CHECK CONSTRAINT [FK_Profiles_Users]
GO
ALTER TABLE [dbo].[Profiles_Documents_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Profiles_Documents_XREF_Documents] FOREIGN KEY([Document_Id])
REFERENCES [dbo].[Documents] ([Id])
GO
ALTER TABLE [dbo].[Profiles_Documents_XREF] CHECK CONSTRAINT [FK_Profiles_Documents_XREF_Documents]
GO
ALTER TABLE [dbo].[Profiles_Documents_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Profiles_Documents_XREF_Profiles] FOREIGN KEY([Profile_Id])
REFERENCES [dbo].[Profiles] ([Id])
GO
ALTER TABLE [dbo].[Profiles_Documents_XREF] CHECK CONSTRAINT [FK_Profiles_Documents_XREF_Profiles]
GO
ALTER TABLE [dbo].[Profiles_Embelishments_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Profiles_Embelishments_XREF_Embelishments] FOREIGN KEY([Embelishment_Id])
REFERENCES [dbo].[Embelishments] ([Id])
GO
ALTER TABLE [dbo].[Profiles_Embelishments_XREF] CHECK CONSTRAINT [FK_Profiles_Embelishments_XREF_Embelishments]
GO
ALTER TABLE [dbo].[Profiles_Embelishments_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Profiles_Embelishments_XREF_Profiles] FOREIGN KEY([Profile_Id])
REFERENCES [dbo].[Profiles] ([Id])
GO
ALTER TABLE [dbo].[Profiles_Embelishments_XREF] CHECK CONSTRAINT [FK_Profiles_Embelishments_XREF_Profiles]
GO
ALTER TABLE [dbo].[Profiles_Images_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Profiles_Images_XREF_Images] FOREIGN KEY([Image_Id])
REFERENCES [dbo].[Images] ([Id])
GO
ALTER TABLE [dbo].[Profiles_Images_XREF] CHECK CONSTRAINT [FK_Profiles_Images_XREF_Images]
GO
ALTER TABLE [dbo].[Profiles_Images_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Profiles_Images_XREF_Profiles] FOREIGN KEY([Profile_Id])
REFERENCES [dbo].[Profiles] ([Id])
GO
ALTER TABLE [dbo].[Profiles_Images_XREF] CHECK CONSTRAINT [FK_Profiles_Images_XREF_Profiles]
GO
ALTER TABLE [dbo].[Profiles_WebLinks_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Profiles_WebLinks_XREF_Profiles] FOREIGN KEY([Profile_Id])
REFERENCES [dbo].[Profiles] ([Id])
GO
ALTER TABLE [dbo].[Profiles_WebLinks_XREF] CHECK CONSTRAINT [FK_Profiles_WebLinks_XREF_Profiles]
GO
ALTER TABLE [dbo].[Profiles_WebLinks_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Profiles_WebLinks_XREF_WebLinks] FOREIGN KEY([WebLink_Id])
REFERENCES [dbo].[WebLinks] ([Id])
GO
ALTER TABLE [dbo].[Profiles_WebLinks_XREF] CHECK CONSTRAINT [FK_Profiles_WebLinks_XREF_WebLinks]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_Profiles] FOREIGN KEY([Entrepreneur_Id])
REFERENCES [dbo].[Profiles] ([Id])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_Profiles]
GO
ALTER TABLE [dbo].[Projects_Documents_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Projects_Documents_XREF_Documents] FOREIGN KEY([Document_Id])
REFERENCES [dbo].[Documents] ([Id])
GO
ALTER TABLE [dbo].[Projects_Documents_XREF] CHECK CONSTRAINT [FK_Projects_Documents_XREF_Documents]
GO
ALTER TABLE [dbo].[Projects_Documents_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Projects_Documents_XREF_Projects] FOREIGN KEY([Project_Id])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[Projects_Documents_XREF] CHECK CONSTRAINT [FK_Projects_Documents_XREF_Projects]
GO
ALTER TABLE [dbo].[Projects_Embelishments_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Projects_Embelishments_XREF_Embelishments] FOREIGN KEY([Embelishment_Id])
REFERENCES [dbo].[Embelishments] ([Id])
GO
ALTER TABLE [dbo].[Projects_Embelishments_XREF] CHECK CONSTRAINT [FK_Projects_Embelishments_XREF_Embelishments]
GO
ALTER TABLE [dbo].[Projects_Embelishments_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Projects_Embelishments_XREF_Projects] FOREIGN KEY([Project_Id])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[Projects_Embelishments_XREF] CHECK CONSTRAINT [FK_Projects_Embelishments_XREF_Projects]
GO
ALTER TABLE [dbo].[Projects_Images_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Projects_Images_XREF_Images] FOREIGN KEY([Images_Id])
REFERENCES [dbo].[Images] ([Id])
GO
ALTER TABLE [dbo].[Projects_Images_XREF] CHECK CONSTRAINT [FK_Projects_Images_XREF_Images]
GO
ALTER TABLE [dbo].[Projects_Images_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Projects_Images_XREF_Projects] FOREIGN KEY([Project_Id])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[Projects_Images_XREF] CHECK CONSTRAINT [FK_Projects_Images_XREF_Projects]
GO
ALTER TABLE [dbo].[Projects_WebLinks_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Projects_WebLinks_XREF_Projects] FOREIGN KEY([Project_Id])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[Projects_WebLinks_XREF] CHECK CONSTRAINT [FK_Projects_WebLinks_XREF_Projects]
GO
ALTER TABLE [dbo].[Projects_WebLinks_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Projects_WebLinks_XREF_WebLinks] FOREIGN KEY([WebLink_Id])
REFERENCES [dbo].[WebLinks] ([Id])
GO
ALTER TABLE [dbo].[Projects_WebLinks_XREF] CHECK CONSTRAINT [FK_Projects_WebLinks_XREF_WebLinks]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Accounts] FOREIGN KEY([Account_Id])
REFERENCES [dbo].[Accounts] ([Id])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Accounts]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Category_Types] FOREIGN KEY([Category_Type_Id])
REFERENCES [dbo].[Category_Types] ([Id])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Category_Types]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Currency_Types] FOREIGN KEY([Currency_Type_Id])
REFERENCES [dbo].[Currency_Types] ([Id])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Currency_Types]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Transaction_Types] FOREIGN KEY([Transaction_Type_Id])
REFERENCES [dbo].[Transaction_Types] ([Id])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Transaction_Types]
GO
/****** Object:  StoredProcedure [dbo].[update_UserIndexProfile]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 6/14/2016
-- Description:	This procedure will update from the user's index page
-- =============================================
CREATE PROCEDURE [dbo].[update_UserIndexProfile] 
	-- Add the parameters for the stored procedure here
	@User_Id VARCHAR(128), 
	@Entrepreneur_Id INT,
	@Investor_Id INT,
	@Name NVARCHAR(150),
	@PhoneNumber NVARCHAR(50),
	@Entrepreneur_Public BIT,
	@Investor_Public BIT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here

			DECLARE @user_entrepreneur_related BIT;
			DECLARE @user_investor_related BIT;

			IF EXISTS ( SELECT * FROM Profiles WHERE [User_Id] = @User_Id AND [Id] = @Entrepreneur_Id AND [ProfileType_Id] = 1)
			BEGIN
				SET @user_entrepreneur_related = 'TRUE'
			END
			ELSE
			BEGIN
				SET @user_entrepreneur_related = 'FALSE'
			END

			IF EXISTS ( SELECT * FROM Profiles WHERE [User_Id] = @User_Id AND [Id] = @Investor_Id AND [ProfileType_Id] = 2)
			BEGIN
				SET @user_investor_related = 'TRUE'
			END
			ELSE
			BEGIN
				SET @user_investor_related = 'FALSE'
			END

			IF (@user_investor_related = 'TRUE' AND @user_entrepreneur_related = 'TRUE')
			BEGIN
				UPDATE Users 
				SET [Name] = COALESCE(@Name, [Name]),
					[PhoneNumber] = COALESCE(@PhoneNumber, [PhoneNumber])
				WHERE [Id] = @User_Id

				UPDATE Profiles
				SET [Profile_Public] = COALESCE(@Entrepreneur_Public, [Profile_Public])
				WHERE [Id] = @Entrepreneur_Id

				UPDATE Profiles
				SET [Profile_Public] = COALESCE(@Investor_Public, [Profile_Public])
				WHERE [Id] = @Investor_Id
			END
			ELSE
			BEGIN

				RAISERROR('Non-Relation error, when attempting to update profile data', 16, 1);

			END

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('update_UserIndexProfile', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Account_Insert]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 5/23/2016
-- Description:	Stored procedure that inserts a new financial account
-- =============================================
CREATE PROCEDURE [dbo].[usp_Account_Insert] 
	-- Add the parameters for the stored procedure here
	@Profile_Id INT,
	@Profile_Type_Id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY

		BEGIN TRANSACTION

			INSERT INTO Accounts
			(
				[DateTime_Created],
				[Deleted],
				[Frozen]
			)
			VALUES
			(
				GETDATE(),
				0,
				0
			)

			DECLARE @new_Account_Id INT = SCOPE_IDENTITY()

			IF(@Profile_Type_Id = 2)
			BEGIN
				INSERT INTO Accounts_Investors_XREF
				(
					[Account_Id],
					[Investor_Id]
				)
				VALUES
				(
					@new_Account_Id,
					@Profile_Id
				)
			END

			IF(@Profile_Type_Id = 3)
			BEGIN
				INSERT INTO Accounts_Projects_XREF
				(
					[Account_Id],
					[Project_Id]
				)
				VALUES
				(
					@new_Account_Id,
					@Profile_Id
				)
			END

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Account_Insert, ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Document_Delete]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 5/25/2016
-- Description:	This procedure wil delete a document given the document's id and it will remvoe it's record on the appropriate XREF table
-- =============================================
CREATE PROCEDURE [dbo].[usp_Document_Delete] 
	-- Add the parameters for the stored procedure here
	@Document_Id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			DECLARE @Document_XREF_Id INT;

			IF EXISTS
			(	-- If expression to be evaluated
				SELECT * 
				FROM Profiles_Documents_XREF
				WHERE [Document_Id] = @Document_Id
			)
			BEGIN

				SET @Document_XREF_Id = 
				(	-- If expression to be evaluated
					SELECT [Id]
					FROM Profiles_Documents_XREF
					WHERE [Document_Id] = @Document_Id
				);

				DELETE FROM Profiles_Documents_XREF
				WHERE [Id] = @Document_XREF_Id;

			END

			IF EXISTS
			(	-- If expression to be evaluated
				SELECT *
				FROM Projects_Documents_XREF
				WHERE [Document_Id] = @Document_Id
			)
			BEGIN

				SET @Document_XREF_Id = 
				(	-- If expression to be evaluated
					SELECT [Id]
					FROM Projects_Documents_XREF
					WHERE [Document_Id] = @Document_Id
				);

				DELETE FROM Projects_Documents_XREF
				WHERE [Id] = @Document_XREF_Id;

			END

			DELETE FROM Documents
			WHERE [Id] = @Document_Id;

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Document_Delete', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Document_Update]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 6/2/2016
-- Description:	This stored procedure will update a document given it's Id
-- =============================================
CREATE PROCEDURE [dbo].[usp_Document_Update] 
	-- Add the parameters for the stored procedure here
	@Document_Id INT, 
	@Binary_File VARBINARY(MAX),
	@File_Name VARCHAR(256),
	@Mime_Type VARCHAR(256),
	@Description VARCHAR(1000) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			UPDATE Documents SET
				[Binary_File] = COALESCE(@Binary_File, [Binary_File]),
				[DateTime_Created] = GETDATE(),
				[File_Name] = COALESCE(@File_Name, [File_Name]),
				[Mime_Type] = COALESCE(@Mime_Type, [Mime_Type]),
				[Description] = COALESCE(@Description, [Description])
			WHERE [Id] = @Document_Id

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Document_Update', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Embelishment_Delete]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 5/24/2016
-- Description:	This procedure will delete an emblishment given it's Id and will delete it from the Appropriate XREF table.
-- =============================================
CREATE PROCEDURE [dbo].[usp_Embelishment_Delete] 
	-- Add the parameters for the stored procedure here
	@Embelishment_Id INT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			DECLARE @Embelishment_XREF_Id INT;

			IF EXISTS
			( -- if expresion to be evaluated
				SELECT * 
				FROM Projects_Embelishments_XREF 
				WHERE [Embelishment_Id] = @Embelishment_Id
			)
			BEGIN
				
				SET @Embelishment_XREF_Id = 
				( -- if expresion to be evaluated
					SELECT [Id] 
					FROM Projects_Embelishments_XREF 
					WHERE [Embelishment_Id] = @Embelishment_Id
				)

				DELETE FROM Projects_Embelishments_XREF
				WHERE [Id] = @Embelishment_XREF_Id

			END

			IF EXISTS
			( -- if expresion to be evaluated
				SELECT * 
				FROM Profiles_Embelishments_XREF 
				WHERE [Embelishment_Id] = @Embelishment_Id
			)
			BEGIN
				
				SET @Embelishment_XREF_Id = 
				( -- if expresion to be evaluated
					SELECT [Id] 
					FROM Profiles_Embelishments_XREF 
					WHERE [Embelishment_Id] = @Embelishment_Id
				)

				DELETE FROM Profiles_Embelishments_XREF
				WHERE [Id] = @Embelishment_XREF_Id
			END

			DELETE FROM Embelishments
			WHERE [Id] = @Embelishment_Id

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Embelishment_Delete', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Embelishment_Update]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 5/24/2016
-- Description:	This procedure will update an embelishment given the embelishment's Id
-- =============================================
CREATE PROCEDURE [dbo].[usp_Embelishment_Update] 
	-- Add the parameters for the stored procedure here
	@Embelishment_Id INT, 
	@Embelishment NVARCHAR(1000) = NULL,
	@Embelishment_Type NVARCHAR(150) = NULL,
	@Description NVARCHAR(250) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			UPDATE Embelishments SET 
				[Embelishment] = COALESCE(@Embelishment, [Embelishment]),
				[Embelishment_Type] = COALESCE(@Embelishment_Type, [Embelishment_Type]),
				[Description] = COALESCE(@Description, [Description])
			WHERE [Id] = @Embelishment_Id

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Embelishment_Updat', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Entrepreneur_Insert]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 5/23/2016
-- Description:	Stored procedure that inserts a new Entrepreneur profile for a given user
-- =============================================
CREATE PROCEDURE [dbo].[usp_Entrepreneur_Insert] 
	-- Add the parameters for the stored procedure here
	@User_Id NVARCHAR(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRANSACTION

			-- Insert statements for procedure here
			INSERT INTO Profiles
			(
				[User_Id],
				[ProfileType_Id],
				[DateTime_Created],
				[DateTime_LastLogon],
				[Profile_Public],
				[Deleted],
				[Verified]
			)
			VALUES
			(
				@User_Id,
				1,
				GETDATE(),
				GETDATE(),
				0,
				0,
				0
			)

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Entrepreneur_Insert, ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[usp_Image_Delete]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 5/24/2016
-- Description:	Procedure that will delete an Image from the Images table and the record from the appropriate XREF table.
-- =============================================
CREATE PROCEDURE [dbo].[usp_Image_Delete] 
	-- Add the parameters for the stored procedure here
	@Image_Id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			DECLARE @Images_XREF_Id INT;
			
			IF EXISTS
			( -- if expresion to be evaluated
				SELECT * 
				FROM Profiles_Images_XREF 
				WHERE [Image_Id] = @Image_Id
			)
			BEGIN
			
				SET @Images_XREF_Id = 
				( -- if expresion to be evaluated
					SELECT [Id] 
					FROM Profiles_Images_XREF 
					WHERE [Image_Id] = @Image_Id
				)

				DELETE FROM Profiles_Images_XREF 
				WHERE [Id] = @Images_XREF_Id

			END 

			IF EXISTS
			( -- if expresion to be evaluated
				SELECT * 
				FROM Projects_Images_XREF 
				WHERE [Images_Id] = @Image_Id
			)
			BEGIN

				SET @Images_XREF_Id = 
				( -- if expresion to be evaluated
					SELECT [Id] 
					FROM Projects_Images_XREF 
					WHERE [Images_Id] = @Image_Id
				)

				DELETE FROM Projects_Images_XREF
				WHERE [Id] = @Images_XREF_Id

			END

			DELETE FROM Images
			WHERE [Id] = @Image_Id

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Image_Delete', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Image_Update]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hezon Flores
-- Create date: 5/24/2016
-- Description:	This stored procedure will update an image given an image Id
-- =============================================
CREATE PROCEDURE [dbo].[usp_Image_Update]
	-- Add the parameters for the stored procedure here
	@Image_Id INT,
	@Binary_Image VARBINARY(MAX) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			UPDATE Images SET
				[Binary_Image] = COALESCE(@Binary_Image, [Binary_Image]),
				[DateTime_Created] = GETDATE()
			WHERE [Id] = @Image_Id

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Image_Update', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Investor_Insert]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 5/23/2016
-- Description:	This stored procedure will create an Investor profile for a given user profile.
-- =============================================
CREATE PROCEDURE [dbo].[usp_Investor_Insert] 
	-- Add the parameters for the stored procedure here
	@User_Id NVARCHAR(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    BEGIN TRY

		BEGIN TRANSACTION

			-- Insert statements for procedure here
			INSERT INTO Profiles
			(
				[User_Id],
				[ProfileType_Id],
				[DateTime_Created],
				[DateTime_LastLogon],
				[Profile_Public],
				[Deleted],
				[Verified]
			)
			VALUES
			(
				@User_Id,
				2,
				GETDATE(),
				GETDATE(),
				0,
				0,
				0
			)

			DECLARE @new_Profile_Id INT = SCOPE_IDENTITY()

			EXECUTE usp_Account_Insert @new_Profile_Id, 2

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Investor_Insert, ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_PROC_Insert]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 6/15/2016
-- Description:	This procedure will create a new PROC given  the correct parameters.
-- =============================================
CREATE PROCEDURE [dbo].[usp_PROC_Insert] 
	-- Add the parameters for the stored procedure here
	@User_Id NVARCHAR(128), 
	@Investor_Id INT,
	@Project_Id INT,
	@Investment_Amount MONEY,
	@Revenue_Percentage DECIMAL(8,6),
	@DateTime_Begin DATETIME2(7),
	@DateTime_End DATETIME2(7)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			
			-- make sure the investor id and user id are related
			IF EXISTS
			(
				SELECT *
				FROM Profiles
				WHERE [User_Id] = @User_Id
				AND [ProfileType_Id] = 2
				AND [Id] = @Investor_Id
			)
			BEGIN

				INSERT INTO PROCs
				(
					[Investor_Id],
					[Project_Id],
					[DateTime_Created],
					[Revenue_Percentage],
					[DateTime_Enforcement_Begin],
					[DateTime_Enforcement_End],
					[Investment_Amount],
					[Active],
					[Count_Project_Revised],
					[Count_Investor_Revised],
					[Accepted_Project],
					[Accepted_Investor],
					[Accepted_Mutualy],
					[Payment_Interval]
				)
				VALUES
				(
					@Investor_Id,
					@Project_Id,
					GETDATE(),
					@Revenue_Percentage,
					@DateTime_Begin,
					@DateTime_End,
					@Investment_Amount,
					0,
					0,
					0,
					0,
					1,
					0,
					1
				)
				
				SELECT SCOPE_IDENTITY()
			END
			ELSE
			BEGIN

				RAISERROR('Non-Relation error, when attempting to create a PROC, user and investor are not related.', 16, 1);

			END

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_PROC_Insert', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Profile_Document_Insert]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 5/24/2016
-- Description:	Stored procedure will insert a document into the Documents table and add a record too the Profiles_Documents_XREF table for the given Profile the Document is for
-- =============================================
CREATE PROCEDURE [dbo].[usp_Profile_Document_Insert] 
	-- Add the parameters for the stored procedure here
	@Profile_Id INT,
	@File_Name vARCHAR(256),
	@Mime_Type VARCHAR(256),
	@FileBytes VARBINARY(MAX),
	@Description VARCHAR(1000) = NULL 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY

		BEGIN TRAN

			INSERT INTO Documents
			(
				[Binary_File],
				[DateTime_Created],
				[File_Name],
				[Mime_Type],
				[Description]
			)
			VALUES
			(
				@FileBytes,
				GETDATE(),
				@File_Name,
				@Mime_Type,
				@Description
			);

			DECLARE @new_document_Id INT = SCOPE_IDENTITY();

			INSERT INTO Profiles_Documents_XREF
			(
				[Profile_Id],
				[Document_Id]
			)
			VALUES
			(
				@Profile_Id,
				@new_document_Id
			);

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Document_Profile_Insert, ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[usp_Profile_Embelishment_Insert]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 5/24/2016
-- Description:	Stored procedure will insert an Embelishment into our Embelishments table and add a record on our Profile_Embelishments_XREF table for a given profile.
-- =============================================
CREATE PROCEDURE [dbo].[usp_Profile_Embelishment_Insert] 
	-- Add the parameters for the stored procedure here
	@Profile_Id INT, 
	@Embelishment NVARCHAR(1000),
	@Embelishment_Type NVARCHAR(150) = NULL,
	@Description NVARCHAR(250) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			INSERT INTO Embelishments
			(
				[Embelishment],
				[Embelishment_Type],
				[Description]
			)
			VALUES
			(
				@Embelishment,
				@Embelishment_Type,
				@Description
			);

			DECLARE @new_embelishment_Id INT = SCOPE_IDENTITY();

			INSERT INTO Profiles_Embelishments_XREF
			(
				[Profile_Id],
				[Embelishment_Id]
			)
			VALUES
			(
				@Profile_Id,
				@new_embelishment_Id
			);

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Profile_Embelishment_Insert', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Profile_Image_Insert]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 5/24/2016
-- Description:	This procedure will insert an image into our Images table and a record into our Images_Profiles_XREF table for a given profile
-- =============================================
CREATE PROCEDURE [dbo].[usp_Profile_Image_Insert] 
	-- Add the parameters for the stored procedure here
	@Profile_Id INT,
	@Binary_Image VARBINARY(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			INSERT INTO Images
			(
				[Binary_Image],
				[DateTime_Created]
			)
			VALUES
			(
				@Binary_Image,
				GETDATE()
			);

			DECLARE @new_image_Id INT = SCOPE_IDENTITY();

			INSERT INTO Profiles_Images_XREF
			(
				[Profile_Id],
				[Image_Id]
			)
			VALUES
			(
				@Profile_Id,
				@new_image_Id
			);

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Profile_Image_Insert', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Profile_WebLink_Insert]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 5/24/2016
-- Description:	Stored Procedure that will insert a web link into our WebLinks table and add a record too our Profiles_WebLinks_XREF table for a given Profile.
-- =============================================
CREATE PROCEDURE [dbo].[usp_Profile_WebLink_Insert] 
	-- Add the parameters for the stored procedure here
	@Profile_Id INT, 
	@Link NVARCHAR(1000),
	@Link_Type VARCHAR(256) = NULL,
	@FriendlyName NVARCHAR(256) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			INSERT INTO WebLinks
			(
				[Link_Type],
				[Link],
				[FriendlyName]
			)
			VALUES
			(
				@Link_Type,
				@Link,
				@FriendlyName
			);

			DECLARE @new_weblink_Id INT = SCOPE_IDENTITY();

			INSERT INTO Profiles_WebLinks_XREF
			(
				[Profile_Id],
				[WebLink_Id]
			)
			VALUES
			(
				@Profile_Id,
				@new_weblink_Id
			);

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Profile_WebLink_Insert', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Project_Document_Insert]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 5/24/2016
-- Description:	This procedure inserts new Docuemnts into our Documents table and adds a record into our Documents_Projects_XREF table for a given Project Profile
-- =============================================
CREATE PROCEDURE [dbo].[usp_Project_Document_Insert] 
	-- Add the parameters for the stored procedure here
	@Project_Id INT,
	@File_Name vARCHAR(256),
	@Mime_Type VARCHAR(256),
	@FileBytes VARBINARY(MAX),
	@Description VARCHAR(1000) = NULL 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			INSERT INTO Documents
			(
				[Binary_File],
				[DateTime_Created],
				[File_Name],
				[Mime_Type],
				[Description]
			)
			VALUES
			(
				@FileBytes,
				GETDATE(),
				@File_Name,
				@Mime_Type,
				@Description
			);

			DECLARE @new_document_Id INT = SCOPE_IDENTITY();

			INSERT INTO Projects_Documents_XREF
			(
				[Project_Id],
				[Document_Id]
			)
			VALUES
			(
				@Project_Id,
				@new_document_Id
			);

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Document_Project_Insert' , ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Project_Embelishment_Insert]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 5/24/2016
-- Description:	Stored procedure will insert an Embelishment into our Embelishments table and add a record on our Projects_Embelishments_XREF table for a given Project.
-- =============================================
CREATE PROCEDURE [dbo].[usp_Project_Embelishment_Insert] 
	-- Add the parameters for the stored procedure here
	@Project_Id INT, 
	@Embelishment NVARCHAR(1000),
	@Embelishment_Type NVARCHAR(150) = NULL,
	@Description NVARCHAR(250) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			INSERT INTO Embelishments
			(
				[Embelishment],
				[Embelishment_Type],
				[Description]
			)
			VALUES
			(
				@Embelishment,
				@Embelishment_Type,
				@Description
			);

			DECLARE @new_embelishment_Id INT = SCOPE_IDENTITY();

			INSERT INTO Projects_Embelishments_XREF
			(
				[Project_Id],
				[Embelishment_Id]
			)
			VALUES
			(
				@Project_Id,
				@new_embelishment_Id
			);

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Project_Embelishment_Insert', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Project_Image_Insert]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 5/24/2016
-- Description:	This stored procedure will insert an image record into our Images table and then a record into our Projects_Images_XREF tabel for a given Project profile.
-- =============================================
CREATE PROCEDURE [dbo].[usp_Project_Image_Insert] 
	-- Add the parameters for the stored procedure here
	@Project_Id INT, 
	@Binary_Image VARBINARY(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			INSERT INTO Images
			(
				[Binary_Image],
				[DateTime_Created]
			)
			VALUES
			(
				@Binary_Image,
				GETDATE()
			);

			DECLARE @new_image_Id INT = SCOPE_IDENTITY();

			INSERT INTO Projects_Images_XREF
			(
				[Project_Id],
				[Images_Id]
			)
			VALUES
			(
				@Project_Id,
				@new_image_Id
			);

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Project_Image_Insert', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Project_Insert]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 6/7/2016
-- Description:	This stored procedure will create a new project record on our projects table it will return one value which is the identity of the recrod just created..
-- =============================================
CREATE PROCEDURE [dbo].[usp_Project_Insert] 
	-- Add the parameters for the stored procedure here
	@User_Id VARCHAR(128), 
	@Entrepreneur_Id INT,
	@Name VARCHAR(150),
	@Summary VARCHAR(5000) = NULL,
	@Investment_Goal MONEY = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			IF EXISTS 
			(
				SELECT * 
				FROM Profiles
				WHERE [ProfileType_Id] = 1
				AND [Id] = @Entrepreneur_Id 
			)
			BEGIN

				INSERT INTO Projects
				(
					[Entrepreneur_Id],
					[DateTime_Created],
					[Name],
					[Summary],
					[Investment_Goal],
					[Profile_Public],
					[Deleted]
				)
				VALUES
				(
					@Entrepreneur_Id,
					GETDATE(),
					@Name,
					@Summary,
					@Investment_Goal,
					0,
					0
				)

				DECLARE @Project_Id INT = SCOPE_IDENTITY()

				EXECUTE usp_Account_Insert @Project_Id, 3

				SELECT @Project_Id

			END

			ELSE
			BEGIN

				RAISERROR('Non-Relation error, when attempting to create Project for Entrepreneur', 16, 1);

			END

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Project_Insert', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Project_WebLink_Insert]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 5/24/2016
-- Description:	Stored Procedure that will insert a web link into our WebLinks table and add a record too our Projects_WebLinks_XREF table for a given Project
-- =============================================
CREATE PROCEDURE [dbo].[usp_Project_WebLink_Insert] 
	-- Add the parameters for the stored procedure here
	@Project_Id INT,
	@Link NVARCHAR(1000),
	@Link_Type VARCHAR(256) = NULL,
	@FriendlyName NVARCHAR(256) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			INSERT INTO WebLinks
			(
				[Link_Type],
				[Link],
				[FriendlyName]
			)
			VALUES
			(
				@Link_Type,
				@Link,
				@FriendlyName
			);

			DECLARE @new_weblink_Id INT = SCOPE_IDENTITY();

			INSERT INTO Projects_WebLinks_XREF
			(
				[Project_Id],
				[WebLink_Id]
			)
			VALUES
			(
				@Project_Id,
				@new_weblink_Id
			);

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Project_WebLink_Insert', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Select_EntrepreneurDetails]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon
-- Create date: 6/19/2016
-- Description:	This stored procedure will retrive the data neccessary to display the Entrepreneur profile too an obsrver.
-- =============================================
CREATE PROCEDURE [dbo].[usp_Select_EntrepreneurDetails] 
	-- Add the parameters for the stored procedure here
	@Entrepreneur_Id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			
			IF(1 = (SELECT [Profile_Public] FROM Profiles WHERE [Id] = @Entrepreneur_Id AND [ProfileType_Id] = 1))
			BEGIN

				-- SELECT the Entrepreneur profile data
				SELECT P.[Id] AS 'Entrepreneur_Id',
					P.[Verified],
					I.[Binary_Image],
					U.[Name]
				FROM Profiles P
				JOIN Users U ON P.[User_Id] = U.[Id]
				LEFT OUTER JOIN Profiles_Images_XREF PI_XREF ON P.[Id] = PI_XREF.[Profile_Id]
				LEFT OUTER JOIN Images I ON PI_XREF.[Image_Id] = I.[Id]
				WHERE P.[Id] = @Entrepreneur_Id

				-- SELECT the Entrepreneur Documents
				SELECT D.[Id] AS 'Document_Id',
					D.[DateTime_Created],
					D.[File_Name],
					D.[Description] 
				FROM Profiles P
				JOIN Profiles_Documents_XREF PD_XREF ON P.[Id] = PD_XREF.[Profile_Id]
				JOIN Documents D ON D.[Id] = PD_XREF.[Document_Id]
				WHERE P.[Id] = @Entrepreneur_Id

				-- SELECT the Entrepreneur Links
				SELECT W.[Link_Type],
					W.[Link],
					W.[FriendlyName]
				FROM Profiles P
				JOIN Profiles_WebLinks_XREF PW_XREF ON P.[Id] = PW_XREF.[Profile_Id]
				JOIN WebLinks W ON W.[Id] = PW_XREF.[WebLink_Id]
				WHERE P.[Id] = @Entrepreneur_Id

				-- SELECT the Entrepreneur Embelishments
				SELECT E.[Embelishment],
					E.[Embelishment_Type],
					E.[Description]
				FROM Profiles P
				JOIN Profiles_Embelishments_XREF PE_XREF ON P.[Id] = PE_XREF.[Profile_Id]
				JOIN Embelishments E ON E.[Id] = PE_XREF.[Embelishment_Id]
				WHERE P.[Id] = @Entrepreneur_Id

				-- SELECT the data of each project account for this entrepreneur that is public
				SELECT P.[Id] AS 'Project_Id',
					P.[Name],
					P.[Investment_Goal],
					P.[Summary]
				FROM Projects P
				WHERE P.[Entrepreneur_Id] = @Entrepreneur_Id
				AND P.[Profile_Public] = 1

				-- SELECT the PROCs this Entrepreneurs has 
				SELECT P.[Id] AS 'PROC_Id',
					P.[Revenue_Percentage],
					P.[DateTime_Enforcement_Begin],
					P.[DateTime_Enforcement_End],
					P.[Investment_Amount],
					P.[Active],
					E.[Id] AS 'Entrepreneur_Id',
					Pro.[Id] AS 'Project_Id',
					Pro.[Name] AS 'Project_Name'
				FROM PROCs P
				JOIN Projects Pro ON P.[Project_Id] = Pro.[Id]
				JOIN Profiles E ON Pro.[Entrepreneur_Id] = E.[Id]
				WHERE E.[Id] = @Entrepreneur_Id

			END
			ELSE
			BEGIN

				RAISERROR('Profile is not public', 16, 1);

			END

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Select_EntrepreneurDetails', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Select_EntrepreneurIndex]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 6/6/2016
-- Description:	This procedure will collect the data neccessary to display to the client when they are accessing thier Entrepreneur profile.
-- =============================================
CREATE PROCEDURE [dbo].[usp_Select_EntrepreneurIndex] 
	-- Add the parameters for the stored procedure here
	@User_Id NVARCHAR(128), 
	@Entrepreneur_Id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			IF EXISTS
			(
				SELECT *
				FROM Profiles
				WHERE [User_Id] = @User_Id
				AND [ProfileType_Id] = 1
				AND [Id] = @Entrepreneur_Id
			)
			BEGIN

				-- SELECT the Entrepreneur profile data
				SELECT P.[Id] AS 'Entrepreneur_Id', 
					P.[DateTime_LastLogon], 
					P.[Profile_Public], 
					P.[Verified],
					I.[Id] AS 'Image_Id',
					I.[Binary_Image]
				FROM Profiles P
				LEFT OUTER JOIN Profiles_Images_XREF PI_XREF ON P.[Id] = PI_XREF.[Profile_Id]
				LEFT OUTER JOIN Images I ON PI_XREF.[Image_Id] = I.[Id]
				WHERE P.[Id] = @Entrepreneur_Id

				-- SELECT the Entrepreneur Documents
				SELECT D.[Id] AS 'Document_Id',
					D.[DateTime_Created],
					D.[File_Name],
					D.[Description] 
				FROM Profiles P
				JOIN Profiles_Documents_XREF PD_XREF ON P.[Id] = PD_XREF.[Profile_Id]
				JOIN Documents D ON D.[Id] = PD_XREF.[Document_Id]
				WHERE P.[Id] = @Entrepreneur_Id

				-- SELECT the Entrepreneur Links
				SELECT W.[Id] AS 'Link_Id',
					W.[Link_Type],
					W.[Link],
					W.[FriendlyName]
				FROM Profiles P
				JOIN Profiles_WebLinks_XREF PW_XREF ON P.[Id] = PW_XREF.[Profile_Id]
				JOIN WebLinks W ON W.[Id] = PW_XREF.[WebLink_Id]
				WHERE P.[Id] = @Entrepreneur_Id

				-- SELECT the Entrepreneur Embelishments
				SELECT E.[Id] AS 'Embelishment_Id',
					E.[Embelishment],
					E.[Embelishment_Type],
					E.[Description]
				FROM Profiles P
				JOIN Profiles_Embelishments_XREF PE_XREF ON P.[Id] = PE_XREF.[Profile_Id]
				JOIN Embelishments E ON E.[Id] = PE_XREF.[Embelishment_Id]
				WHERE P.[Id] = @Entrepreneur_Id

				-- SELECT the data of each project account for this entrepreneur
				SELECT P.[Id] AS 'Project_Id',
					P.[Name],
					(
						SELECT SUM([Amount])
						FROM Transactions
						WHERE [Account_Id] = A.[Id]
						AND [Transaction_State] = 'PROCESSED'
						AND [Currency_Type_Id] = 2
					) AS 'Balance',
					P.[Investment_Goal],
					P.[Summary],
					P.[Profile_Public],
					(
						SELECT COUNT(*)
						FROM PROCs
						WHERE [Project_Id] = P.[Id]
					) AS 'PROC Count'

				FROM Projects P
				LEFT OUTER JOIN Accounts_Projects_XREF AP_XREF ON P.[Id] = AP_XREF.[Project_Id]
				LEFT OUTER JOIN Accounts A ON AP_XREF.[Account_Id] = A.[Id]
				WHERE P.[Entrepreneur_Id] = @Entrepreneur_Id
				AND P.[Deleted] <> 'TRUE'

			END

			ELSE
			BEGIN

				RAISERROR('Non-Relation error, when attempting to retrive Entrepreneur data', 16, 1);

			END

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Select_EntrepreneurIndex', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Select_FinancialAccount_Details]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 6/8/2016
-- Description:	This procedure will retrive the information for a financial account
-- =============================================
CREATE PROCEDURE [dbo].[usp_Select_FinancialAccount_Details] 
	-- Add the parameters for the stored procedure here
	@User_Id NVARCHAR(128), 
	@Account_Id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			
			-- is the user and account related?
			DECLARE @investor_account_relation BIT = 0;
			DECLARE @project_account_relation BIT = 0;

			IF EXISTS
			(
				SELECT *
				FROM Accounts_Investors_XREF AI_XREF
				JOIN Profiles P ON AI_XREF.[Investor_Id] = P.[Id]
				JOIN Users U ON P.[User_Id] = U.[Id]
				WHERE AI_XREF.[Account_Id] = @Account_Id
				AND U.[Id] = @User_Id
				AND P.[ProfileType_Id] = 2
			)
			SET @investor_account_relation = 1
			ELSE
			SET @investor_account_relation = 0

			IF EXISTS
			(
				SELECT *
				FROM Accounts_Projects_XREF AP_XREF
				JOIN Projects P ON AP_XREF.[Project_Id] = P.[Id]
				JOIN Profiles E on P.[Entrepreneur_Id] = E.[Id]
				JOIN Users U ON E.[User_Id] = U.[Id]
				WHERE U.[Id] = @User_Id
				AND AP_XREF.[Account_Id] = @Account_Id
				AND E.[ProfileType_Id] = 1
			)
			SET @project_account_relation = 1
			ELSE
			SET @project_account_relation = 0

			IF (@project_account_relation = 1 OR @investor_account_relation = 1)
			BEGIN

				-- get account details
				SELECT A.[Id] AS 'Account_Id', A.[DateTime_Created], A.[Frozen]
				FROM Accounts A
				WHERE A.[Id] = @Account_Id

				-- get account balance
				SELECT SUM(T.[Amount]) AS 'Balance'
				FROM Accounts A
				JOIN Transactions T ON A.[Id] = T.[Account_Id]
				WHERE A.[Id] = @Account_Id
				AND T.[Currency_Type_Id] = 2
				AND T.[Transaction_State] = 'PROCESSED'

				-- get processed transaction
				SELECT T.[Id], 
					T.[DateTime_Created],
					T.[Amount],
					T.[Description],
					T.[Transaction_State],
					T.[DateTime_Processed]
				FROM Transactions T
				WHERE T.[Account_Id] = @Account_Id
				AND T.[Transaction_State] = 'PROCESSED'

				-- get pending transactions
				SELECT T.[Id], 
					T.[DateTime_Created],
					T.[Amount],
					T.[Description],
					T.[Transaction_State],
					T.[DateTime_Processed]
				FROM Transactions T
				WHERE T.[Account_Id] = @Account_Id
				AND T.[Transaction_State] <> 'DECLINED'
				AND T.[Transaction_State] <> 'PROCESSED'

				-- get failed transactions
				SELECT T.[Id], 
					T.[DateTime_Created],
					T.[Amount],
					T.[Description],
					T.[Transaction_State],
					T.[DateTime_Processed]
				FROM Transactions T
				WHERE T.[Account_Id] = @Account_Id
				AND T.[Transaction_State] = 'DECLINED'

			END

			ELSE
			BEGIN

				RAISERROR('Non-Relation error, when attempting to retrive financial account data', 16, 1);

			END

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Select_FinancialAccount_Details', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Select_InvestorIndex]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 6/6/2016
-- Description:	This stored procedure will retrive the information needed to show the client user their investor information concerning thier PROCs
-- =============================================
CREATE PROCEDURE [dbo].[usp_Select_InvestorIndex] 
	-- Add the parameters for the stored procedure here
	@User_Id NVARCHAR(128), 
	@Investor_Id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			IF EXISTS
			(
				SELECT *
				FROM Profiles
				WHERE [User_Id] = @User_Id
				AND [ProfileType_Id] = 2
				AND [Id] = @Investor_Id
			)
			BEGIN

				-- SELECT the Investor profile data
				SELECT P.[Id] AS 'Investor_Id', 
					P.[DateTime_LastLogon], 
					P.[Profile_Public], 
					P.[Verified],
					I.[Id] AS 'Image_Id',
					I.[Binary_Image]
				FROM Profiles P
				LEFT OUTER JOIN Profiles_Images_XREF PI_XREF ON P.[Id] = PI_XREF.[Profile_Id]
				LEFT OUTER JOIN Images I ON PI_XREF.[Image_Id] = I.[Id]
				WHERE P.[Id] = @Investor_Id 

				-- SELECT the Investor Documents
				SELECT D.[Id] AS 'Document_Id',
					D.[DateTime_Created],
					D.[File_Name],
					D.[Description] 
				FROM Profiles P
				JOIN Profiles_Documents_XREF PD_XREF ON P.[Id] = PD_XREF.[Profile_Id]
				JOIN Documents D ON D.[Id] = PD_XREF.[Document_Id]
				WHERE P.[Id] = @Investor_Id

				-- SELECT the Investor Links
				SELECT W.[Id] AS 'Link_Id',
					W.[Link_Type],
					W.[Link],
					W.[FriendlyName]
				FROM Profiles P
				JOIN Profiles_WebLinks_XREF PW_XREF ON P.[Id] = PW_XREF.[Profile_Id]
				JOIN WebLinks W ON W.[Id] = PW_XREF.[WebLink_Id]
				WHERE P.[Id] = @Investor_Id

				-- SELECT the Investor Embelishments
				SELECT E.[Id] AS 'Embelishment_Id',
					E.[Embelishment],
					E.[Embelishment_Type],
					E.[Description]
				FROM Profiles P
				JOIN Profiles_Embelishments_XREF PE_XREF ON P.[Id] = PE_XREF.[Profile_Id]
				JOIN Embelishments E ON E.[Id] = PE_XREF.[Embelishment_Id]
				WHERE P.[Id] = @Investor_Id

				-- SELECT the investor's PROCs
				SELECT pro.[Id] AS 'PROC_Id',
					pro.[Revenue_Percentage],
					pro.[DateTime_Enforcement_Begin],
					pro.[DateTime_Enforcement_End],
					pro.[Investment_Amount],
					pro.[Active],
					proj.[Id] AS 'Project_Id',
					proj.[Name],
					proj.[Entrepreneur_Id] AS 'Entrepreneur_Id'
				FROM Profiles P
				JOIN PROCs pro ON pro.[Investor_Id] = P.[Id]
				JOIN Projects proj ON pro.[Project_Id] = proj.[Id]
				WHERE P.[Id] = @Investor_Id
				
				-- SELECT the Investor's Financial account details
				SELECT A.[Id] AS 'Account_Id', 
					A.[Frozen], 
					SUM(T.[Amount]) AS 'Balance'
				FROM Accounts_Investors_XREF AI_XREF
				LEFT OUTER JOIN Accounts A ON AI_XREF.[Account_Id] = A.[Id]
				LEFT OUTER JOIN Transactions T ON A.[Id] = T.[Account_Id]
				WHERE AI_XREF.[Investor_Id] = @Investor_Id
				AND T.[Transaction_State] = 'PROCESSED'
				AND T.[Currency_Type_Id] = 2
				GROUP BY A.[Id], A.[Frozen]

				-- SELECT the Investor's account Id
				SELECT A.[Id] AS 'Account_Id'
				FROM Accounts_Investors_XREF AI_XREF
				JOIN Accounts A ON AI_XREF.[Account_Id] = A.[Id]
				WHERE AI_XREF.[Investor_Id] = @Investor_Id

			END

			ELSE
			BEGIN

				RAISERROR('Non-Relation error, when attempting to retrive Investor data', 16, 1);

			END

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Select_InvestorIndex', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Select_InvestorsDetails]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 6/19/2016
-- Description:	This stored procedure will retrive the data neccessar yo to display a public Investor profile data to be consumed.
-- =============================================
CREATE PROCEDURE [dbo].[usp_Select_InvestorsDetails] 
	-- Add the parameters for the stored procedure here
	@Investor_Id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			IF(1 = (SELECT [Profile_Public] FROM Profiles WHERE [Id] = @Investor_Id AND [ProfileType_Id] = 2))
			BEGIN

				-- SELECT the Investor profile data
				SELECT P.[DateTime_LastLogon], 
					P.[Profile_Public], 
					P.[Verified],
					I.[Binary_Image],
					U.[Name]
				FROM Profiles P
				JOIN Users U ON P.[User_Id] = U.[Id]
				LEFT OUTER JOIN Profiles_Images_XREF PI_XREF ON P.[Id] = PI_XREF.[Profile_Id]
				LEFT OUTER JOIN Images I ON PI_XREF.[Image_Id] = I.[Id]
				WHERE P.[Id] = @Investor_Id 

				-- SELECT the Investor Documents
				SELECT D.[Id] AS 'Document_Id',
					D.[DateTime_Created],
					D.[File_Name],
					D.[Description] 
				FROM Profiles P
				JOIN Profiles_Documents_XREF PD_XREF ON P.[Id] = PD_XREF.[Profile_Id]
				JOIN Documents D ON D.[Id] = PD_XREF.[Document_Id]
				WHERE P.[Id] = @Investor_Id

				-- SELECT the Investor Links
				SELECT W.[Link_Type],
					W.[Link],
					W.[FriendlyName]
				FROM Profiles P
				JOIN Profiles_WebLinks_XREF PW_XREF ON P.[Id] = PW_XREF.[Profile_Id]
				JOIN WebLinks W ON W.[Id] = PW_XREF.[WebLink_Id]
				WHERE P.[Id] = @Investor_Id

				-- SELECT the Investor Embelishments
				SELECT E.[Embelishment],
					E.[Embelishment_Type],
					E.[Description]
				FROM Profiles P
				JOIN Profiles_Embelishments_XREF PE_XREF ON P.[Id] = PE_XREF.[Profile_Id]
				JOIN Embelishments E ON E.[Id] = PE_XREF.[Embelishment_Id]
				WHERE P.[Id] = @Investor_Id

				-- SELECT the investor's PROCs
				SELECT pro.[Id] AS 'PROC_Id',
					pro.[Revenue_Percentage],
					pro.[DateTime_Enforcement_Begin],
					pro.[DateTime_Enforcement_End],
					pro.[Investment_Amount],
					pro.[Active],
					proj.[Id] AS 'Project_Id',
					proj.[Name],
					proj.[Entrepreneur_Id] AS 'Entrepreneur_Id'
				FROM Profiles P
				JOIN PROCs pro ON pro.[Investor_Id] = P.[Id]
				JOIN Projects proj ON pro.[Project_Id] = proj.[Id]
				WHERE P.[Id] = @Investor_Id

			END
			ELSE
			BEGIN

				RAISERROR('Investor Profile is not public', 16, 1);

			END

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Select_InvestorsDetails', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Select_KeyArg_Search]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 7/8/2016
-- Description:	This procedre will retrive projects whose name are simialr to the provided key argument as well as entrepreneurs and investors.
-- =============================================
CREATE PROCEDURE [dbo].[usp_Select_KeyArg_Search] 
	-- Add the parameters for the stored procedure here
	@keyArg VARCHAR(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			SET @KeyArg = '%' + @KeyArg + '%'

			-- select project profiles
			SELECT P.[Id],
				P.[Name],
				P.[Summary],
				P.[Investment_Goal],
				(
					SELECT TOP 1 I.[Binary_Image]
					FROM Projects_Images_XREF PI_XREF
					JOIN Images I ON PI_XREF.[Images_Id] = I.[Id]
					WHERE PI_XREF.[Project_Id] = P.[Id]
				) AS 'Binary_Image'
			FROM Projects P
			WHERE P.[Profile_Public] = 1
			AND P.[Name] LIKE @KeyArg

			-- select entrepreneur profiles
			SELECT P.[Id],
				I.[Binary_Image],
				U.[Name]
			FROM Profiles P
			JOIN Users U ON P.[User_Id] = U.[Id]
			LEFT OUTER JOIN Profiles_Images_XREF PI_XREF ON P.[Id] = PI_XREF.[Profile_Id]
			LEFT OUTER JOIN Images I ON PI_XREF.[Image_Id] = I.[Id] 
			WHERE P.[ProfileType_Id] = 1
			AND U.[Name] LIKE @KeyArg
			AND P.[Profile_Public] = 1

			-- select investor profiles
			SELECT P.[Id],
				I.[Binary_Image],
				U.[Name]
			FROM Profiles P
			JOIN Users U ON P.[User_Id] = U.[Id]
			LEFT OUTER JOIN Profiles_Images_XREF PI_XREF ON P.[Id] = PI_XREF.[Profile_Id]
			LEFT OUTER JOIN Images I ON PI_XREF.[Image_Id] = I.[Id] 
			WHERE P.[ProfileType_Id] = 2
			AND U.[Name] LIKE @KeyArg
			AND P.[Profile_Public] = 1

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Select_KeyArg_Search', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Select_PROCDetails]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 6/27/2016
-- Description:	This procedure will retrive data neccsssry to display PROC details
-- =============================================
CREATE PROCEDURE [dbo].[usp_Select_PROCDetails]
	-- Add the parameters for the stored procedure here
	@PROC_Id INT,
	@Investor_Id INT = NULL,
	@Entrepreneur_Id INT = NULL 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			DECLARE @isInvestorOwner BIT = 0
			DECLARE @isEntrepreneurOwner BIT = 0
			
			IF(@Entrepreneur_Id IS NOT NULL)
			BEGIN
				IF EXISTS 
				(
					SELECT * 
					FROM PROCs P
					JOIN Projects Pro ON P.[Project_Id] = Pro.[Id]
					JOIN Profiles Prof ON Pro.[Entrepreneur_Id] = Prof.[Id]
					WHERE P.[Id] = @PROC_Id
					AND Prof.[Id] = @Entrepreneur_Id
					AND Prof.[ProfileType_Id] = 1
				)
				BEGIN
					
					SET @isEntrepreneurOwner = 1

				END
			END

			IF(@Investor_Id IS NOT NULL)
			BEGIN
				IF EXISTS 
				(
					SELECT *
					FROM PROCs P
					JOIN Profiles Prof ON P.[Investor_Id] = Prof.[Id]
					WHERE P.[Id] = @PROC_Id
					AND Prof.[ProfileType_Id] = 2
				)
				BEGIN

					SET @isInvestorOwner = 1

				END
			END

			DECLARE @entrepreneur_name NVARCHAR(256)
			DECLARE @investor_name NVARCHAR(150)

			SET @entrepreneur_name = (
				SELECT U.[Name]
				FROM PROCs P
				JOIN Projects Proj ON P.[Project_Id] = Proj.[Id]
				JOIN Profiles Prof ON Proj.[Entrepreneur_Id] = Prof.[Id]
				JOIN Users U ON Prof.[User_Id] = U.[Id]
				WHERE P.[Id] = @PROC_Id
				AND Prof.[ProfileType_Id] = 1
			)

			SET @investor_name = (
				SELECT U.[Name]
				FROM PROCs P
				JOIN Profiles Prof ON P.[Investor_Id] = Prof.[Id]
				JOIN Users U ON Prof.[User_Id] = U.[Id]
				WHERE P.[Id] = @PROC_Id
				AND Prof.[ProfileType_Id] = 2
			)

			DECLARE @entrepreneur_image VARBINARY(MAX) = (
				SELECT I.[Binary_Image]
				FROM PROCs P
				JOIN Projects Proj ON P.[Project_Id] = Proj.[Id] 
				JOIN Profiles Prof ON Proj.[Entrepreneur_Id] = Prof.[Id]
				JOIN Profiles_Images_XREF PI_XREF ON Prof.[Id] = PI_XREF.[Profile_Id]
				JOIN Images I ON PI_XREF.[Image_Id] = I.[Id]
				WHERE P.[Id] = @PROC_Id
				AND Prof.[ProfileType_Id] = 1
			)

			DECLARE @investor_image VARBINARY(MAX) = (
				SELECT I.[Binary_Image]
				FROM PROCs P
				JOIN Profiles Prof ON P.[Investor_Id] = Prof.[Id]
				JOIN Profiles_Images_XREF PI_XREF ON Prof.[Id] = PI_XREF.[Profile_Id]
				JOIN Images I ON PI_XREF.[Image_Id] = I.[Id]
				WHERE P.[Id] = @PROC_Id
				AND Prof.[ProfileType_Id] = 2
			)

			DECLARE @proc_entrepreneur_id INT = (
				SELECT E.[Id]
				FROM PROCs P
				JOIN Projects Proj ON P.[Project_Id] = Proj.[Id] 
				JOIN Profiles E ON Proj.[Entrepreneur_Id] = E.[Id]
				WHERE P.[Id] = @PROC_Id
				AND E.[ProfileType_Id] = 1
			)

			-- PROC Details
			SELECT P.[Id],
				P.[Investor_Id],
				P.[Project_Id],
				@proc_entrepreneur_Id AS 'Entrepreneur_Id',
				@entrepreneur_name AS 'Entrepreneur_Name',
				@investor_name AS 'Investor_Name',
				@entrepreneur_image AS 'Entrepreneur_Binary_Image',
				@investor_image AS 'Investor_Binary_Image',
				P.[Revenue_Percentage],
				P.[DateTime_Enforcement_Begin],
				P.[DateTime_Enforcement_End],
				P.[Investment_Amount],
				P.[Active],
				P.[Accepted_Investor],
				P.[Accepted_Project],
				P.[Accepted_Mutualy]
			FROM PROCs P
			WHERE P.[Id] = @PROC_Id

			SELECT @isInvestorOwner AS 'Is_Investor_Owner',
				@isEntrepreneurOwner AS 'Is_Entrepreneur_Owner'

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Select_PROCDetails', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Select_ProjectDetails]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 6/20/2016
-- Description:	This procedure will retrive the data neccesary to display to the client this project's public info
-- =============================================
CREATE PROCEDURE [dbo].[usp_Select_ProjectDetails] 
	-- Add the parameters for the stored procedure here
	@Project_id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			
			IF(1 = (SELECT [Profile_Public] FROM Projects WHERE [Id] = @Project_Id))
			BEGIN

				--_ProjectProfile
				SELECT P.[Id] AS 'Project_Id',
					P.[Name],
					P.[Summary],
					P.[Investment_Goal],
					(
						SELECT SUM(Transactions.[Amount])
						FROM Transactions
						WHERE Transactions.[Account_Id] = A.[Id]
						AND Transactions.[Transaction_State] = 'PROCESSED'
						AND Transactions.[Currency_Type_Id] = 2
					) AS 'Funding',
					P.[Entrepreneur_Id] AS 'Entrepreneur_Id',
					U.[Name] AS 'Entrepreneur_Name'
				FROM Projects P
				JOIN Profiles ON Profiles.[Id] = P.[Entrepreneur_Id]
				JOIN Users U ON Profiles.[User_Id] = U.[Id]
				JOIN Accounts_Projects_XREF AP_XREF ON P.[Id] = AP_XREF.[Project_Id]
				JOIN Accounts A ON AP_XREF.[Account_Id] = A.[Id]
				LEFT OUTER JOIN Transactions T ON A.[Id] = T.[Account_Id]
				WHERE P.[Id] = @Project_Id
				GROUP BY P.[Id], P.[Name], P.[Summary], P.[Investment_Goal], P.[Entrepreneur_Id], U.[Name], A.[Id]

				--_ProjectDocuments
				SELECT D.[Id] AS 'Document_Id',
					D.[DateTime_Created],
					D.[File_Name],
					D.[Description]
				FROM Projects P
				JOIN Projects_Documents_XREF PD_XREF ON P.[Id] = PD_XREF.[Project_Id]
				JOIN Documents D ON D.[Id] = PD_XREF.[Document_Id]
				WHERE P.[Id] = @Project_Id

				--_ProjectImages
				SELECT I.[Id] AS 'Image_Id',
					I.[Binary_Image]
				FROM Images I
				JOIN Projects_Images_XREF PI_XREF ON I.[Id] = PI_XREF.[Images_Id]
				WHERE PI_XREF.[Project_Id] = @Project_Id

				--_ProjectPROCs
				SELECT P.[Id] AS 'PROC_Id',
					Pr.[Id] AS 'Investor_Id',
					U.[Name] AS 'Investor_Name',
					P.[Revenue_Percentage],
					P.[DateTime_Enforcement_Begin],
					P.[DateTime_Enforcement_End],
					P.[Investment_Amount],
					P.[Active]
				FROM PROCs P
				JOIN Profiles Pr ON P.[Investor_Id] = PR.[Id]
				JOIN Users U ON Pr.[User_Id] = U.[Id]
				WHERE P.[Project_Id] = @Project_Id
				
				--_ProjectLinks
				SELECT W.[Link_Type],
					W.[Link],
					W.[FriendlyName]
				FROM Projects P
				JOIN Projects_WebLinks_XREF PW_XREF ON P.[Id] = PW_XREF.[Project_Id]
				JOIN WebLinks W ON W.[Id] = PW_XREF.[WebLink_Id]
				WHERE P.[Id] = @Project_Id
				
				--_ProjectEmbelishments
				SELECT E.[Embelishment],
					E.[Embelishment_Type],
					E.[Description]
				FROM Projects P
				JOIN Projects_Embelishments_XREF PE_XREF ON P.[Id] = PE_XREF.[Project_Id]
				JOIN Embelishments E ON E.[Id] = PE_XREF.[Embelishment_Id]
				WHERE P.[Id] = @Project_Id

			END
			ELSE
			BEGIN

				RAISERROR('Project Profile is not public', 16, 1);

			END

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Select_ProjectDetails', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Select_ProjectIndex]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 6/7/2016
-- Description:	This stored procedure will collect the information neccesary to display onto the clients project profile
-- =============================================
CREATE PROCEDURE [dbo].[usp_Select_ProjectIndex] 
	-- Add the parameters for the stored procedure here
	@User_Id NVARCHAR(128), 
	@Entrepreneur_Id INT,
	@Project_Id INT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			IF EXISTS
			(
				SELECT *
				FROM Profiles P
				JOIN Projects PP ON P.[Id] = PP.[Entrepreneur_Id]
				WHERE P.[ProfileType_Id] = 1
				AND P.[Id] = @Entrepreneur_Id
				AND PP.[Id] = @Project_Id
			)
			BEGIN

				--_ProjectProfile
				SELECT P.[Id] AS 'Project_Id',
					P.[Name],
					P.[Summary],
					P.[Profile_Public],
					P.[Investment_Goal]
				FROM Projects P
				WHERE P.[Id] = @Project_Id
				
				--_ProjectAccount
				SELECT A.[Id] AS 'Account_Id', 
					A.[Frozen],
					SUM(T.[Amount]) AS 'Balance'
				FROM Accounts_Projects_XREF AP_XREF
				JOIN Accounts A ON AP_XREF.[Account_Id] = A.[Id]
				LEFT OUTER JOIN Transactions T ON A.[Id] = T.[Account_Id]
				WHERE AP_XREF.[Project_Id] = @Project_Id
				AND T.[Transaction_State] = 'PROCESSED'
				AND T.[Currency_Type_Id] = 2
				GROUP BY A.[Id], A.[Frozen]

				-- get project account Id
				SELECT A.[Id] AS 'Account_Id'
				FROM Accounts_Projects_XREF AP_XREF
				JOIN Accounts A ON AP_XREF.[Account_Id] = A.[Id]
				WHERE AP_XREF.[Project_Id] = @Project_Id

				--_ProjectDocuments
				SELECT D.[Id] AS 'Document_Id',
					D.[DateTime_Created],
					D.[File_Name],
					D.[Description]
				FROM Projects P
				JOIN Projects_Documents_XREF PD_XREF ON P.[Id] = PD_XREF.[Project_Id]
				JOIN Documents D ON D.[Id] = PD_XREF.[Document_Id]
				WHERE P.[Id] = @Project_Id

				--_ProjectImages
				SELECT I.[Id] AS 'Image_Id',
					I.[Binary_Image]
				FROM Images I
				JOIN Projects_Images_XREF PI_XREF ON I.[Id] = PI_XREF.[Images_Id]
				WHERE PI_XREF.[Project_Id] = @Project_Id

				--_ProjectPROCs
				SELECT P.[Id] AS 'PROC_Id',
					Pr.[Id] AS 'Investor_Id',
					U.[Name] AS 'Investor_Name',
					P.[Revenue_Percentage],
					P.[DateTime_Enforcement_Begin],
					P.[DateTime_Enforcement_End],
					P.[Investment_Amount],
					P.[Active]
				FROM PROCs P
				JOIN Profiles Pr ON P.[Investor_Id] = PR.[Id]
				JOIN Users U ON Pr.[User_Id] = U.[Id]
				WHERE P.[Project_Id] = @Project_Id
				
				--_ProjectLinks
				SELECT W.[Id] AS 'Link_Id',
					W.[Link_Type],
					W.[Link],
					W.[FriendlyName]
				FROM Projects P
				JOIN Projects_WebLinks_XREF PW_XREF ON P.[Id] = PW_XREF.[Project_Id]
				JOIN WebLinks W ON W.[Id] = PW_XREF.[WebLink_Id]
				WHERE P.[Id] = @Project_Id
				
				--_ProjectEmbelishments
				SELECT E.[Id] AS 'Embelishment_Id',
					E.[Embelishment],
					E.[Embelishment_Type],
					E.[Description]
				FROM Projects P
				JOIN Projects_Embelishments_XREF PE_XREF ON P.[Id] = PE_XREF.[Project_Id]
				JOIN Embelishments E ON E.[Id] = PE_XREF.[Embelishment_Id]
				WHERE P.[Id] = @Project_Id

			END

			ELSE
			BEGIN

				RAISERROR('Non-Relation error, when attempting to retrive Project data', 16, 1);

			END

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Select_ProjectIndex', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Select_UserIndex]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 5/25/2016
-- Description:	This procedure will collect the data that we want to show the user when they first log in and see the user dashboard.
-- =============================================
CREATE PROCEDURE [dbo].[usp_Select_UserIndex] 
	-- Add the parameters for the stored procedure here
	@UserId VARCHAR(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here

			-- SELECT the user profile data
			SELECT U.[Id], U.[UserName], U.[Name], U.[PhoneNumber]
			FROM Users U
			WHERE U.[Id] = @UserId
			
			-- Get the Entrepreneur ID associated with this user
			DECLARE @Entrepreneur_Id INT =
			(	-- Expresion to be evaluated, should only return one result
				SELECT [Id]
				FROM Profiles
				WHERE [ProfileType_Id] = 1
				AND [User_Id] = @UserId
			);

			-- SELECT the Entrepreneur profile data
			SELECT P.[Id] AS 'Entrepreneur_Id', 
				P.[DateTime_LastLogon], 
				P.[Profile_Public], 
				P.[Verified],
				I.[Id] AS 'Image_Id',
				I.[Binary_Image]
			FROM Profiles P
			LEFT OUTER JOIN Profiles_Images_XREF PI_XREF ON P.[Id] = PI_XREF.[Profile_Id]
			LEFT OUTER JOIN Images I ON PI_XREF.[Image_Id] = I.[Id]
			WHERE P.[Id] = @Entrepreneur_Id

			-- SELECT the Entrepreneur Documents
			SELECT D.[Id] AS 'Document_Id',
				D.[DateTime_Created],
				D.[File_Name],
				D.[Description] 
			FROM Profiles P
			JOIN Profiles_Documents_XREF PD_XREF ON P.[Id] = PD_XREF.[Profile_Id]
			JOIN Documents D ON D.[Id] = PD_XREF.[Document_Id]
			WHERE P.[Id] = @Entrepreneur_Id

			-- SELECT the Entrepreneur Links
			SELECT W.[Id] AS 'Link_Id',
				W.[Link_Type],
				W.[Link],
				W.[FriendlyName]
			FROM Profiles P
			JOIN Profiles_WebLinks_XREF PW_XREF ON P.[Id] = PW_XREF.[Profile_Id]
			JOIN WebLinks W ON W.[Id] = PW_XREF.[WebLink_Id]
			WHERE P.[Id] = @Entrepreneur_Id

			-- SELECT the Entrepreneur Embelishments
			SELECT E.[Id] AS 'Embelishment_Id',
				E.[Embelishment],
				E.[Embelishment_Type],
				E.[Description]
			FROM Profiles P
			JOIN Profiles_Embelishments_XREF PE_XREF ON P.[Id] = PE_XREF.[Profile_Id]
			JOIN Embelishments E ON E.[Id] = PE_XREF.[Embelishment_Id]
			WHERE P.[Id] = @Entrepreneur_Id

			-- SELECT the balance of each project account for this entrepreneur (in USD)
			SELECT P.[Id] AS 'Project_Id',
				P.[Name],
				(
					SELECT SUM([Amount])
					FROM Transactions
					WHERE [Account_Id] = A.[Id]
					AND [Transaction_State] = 'PROCESSED'
					AND [Currency_Type_Id] = 2
				) AS 'Balance',
				P.[Investment_Goal]
			FROM Projects P
			LEFT OUTER JOIN Accounts_Projects_XREF AP_XREF ON P.[Id] = AP_XREF.[Project_Id]
			LEFT OUTER JOIN Accounts A ON AP_XREF.[Account_Id] = A.[Id]
			WHERE P.[Entrepreneur_Id] = @Entrepreneur_Id
			AND P.[Deleted] <> 'TRUE'

			DECLARE @Investor_Id INT =
			(	-- Expresion to be evaluated, should only return one result
				SELECT [Id]
				FROM Profiles
				WHERE [ProfileType_Id] = 2
				AND [User_Id] = @UserId
			);

			-- SELECT the Investor profile data
			SELECT P.[Id] AS 'Investor_Id', 
				P.[DateTime_LastLogon], 
				P.[Profile_Public], 
				P.[Verified],
				I.[Id] AS 'Image_Id',
				I.[Binary_Image]
			FROM Profiles P
			LEFT OUTER JOIN Profiles_Images_XREF PI_XREF ON P.[Id] = PI_XREF.[Profile_Id]
			LEFT OUTER JOIN Images I ON PI_XREF.[Image_Id] = I.[Id]
			WHERE P.[Id] = @Investor_Id 

			-- SELECT the Investor Documents
			SELECT D.[Id] AS 'Document_Id',
				D.[DateTime_Created],
				D.[File_Name],
				D.[Description] 
			FROM Profiles P
			JOIN Profiles_Documents_XREF PD_XREF ON P.[Id] = PD_XREF.[Profile_Id]
			JOIN Documents D ON D.[Id] = PD_XREF.[Document_Id]
			WHERE P.[Id] = @Investor_Id

			-- SELECT the Investor Links
			SELECT W.[Id] AS 'Link_Id',
				W.[Link_Type],
				W.[Link],
				W.[FriendlyName]
			FROM Profiles P
			JOIN Profiles_WebLinks_XREF PW_XREF ON P.[Id] = PW_XREF.[Profile_Id]
			JOIN WebLinks W ON W.[Id] = PW_XREF.[WebLink_Id]
			WHERE P.[Id] = @Investor_Id

			-- SELECT the Investor Embelishments
			SELECT E.[Id] AS 'Embelishment_Id',
				E.[Embelishment],
				E.[Embelishment_Type],
				E.[Description]
			FROM Profiles P
			JOIN Profiles_Embelishments_XREF PE_XREF ON P.[Id] = PE_XREF.[Profile_Id]
			JOIN Embelishments E ON E.[Id] = PE_XREF.[Embelishment_Id]
			WHERE P.[Id] = @Investor_Id

			-- SELECT the Investor's Financial account details in USD
			SELECT A.[Frozen], SUM(T.[Amount]) AS 'Balance'
			FROM Accounts_Investors_XREF AI_XREF
			LEFT OUTER JOIN Accounts A ON AI_XREF.[Account_Id] = A.[Id]
			LEFT OUTER JOIN Transactions T ON A.[Id] = T.[Account_Id]
			WHERE AI_XREF.[Investor_Id] = @Investor_Id
			AND T.[Transaction_State] = 'PROCESSED'
			AND T.[Currency_Type_Id] = 2
			GROUP BY A.[Frozen]

			---- SELECT the Investor's Financial account details in BTC
			--SELECT A.[Frozen], SUM(T.[Amount])
			--FROM Account_Investors_XREF AI_XREF
			--JOIN Accounts A ON AI_XREF.[Account_Id] = A.[Id]
			--JOIN Transactions T ON A.[Id] = T.[Account_Id]
			--WHERE AI_XREF.[Investor_Id] = @Investor_Id
			--AND T.[Transaction_State] = 'PROCESSED'
			--AND T.[Currency_Type] = 1
			--GROUP BY A.[Frozen]

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Select_UserIndex', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Transaction_Insert]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 5/23/2016
-- Description:	Storerd procedure that inserts a new transaction to go in our transactions table
-- =============================================
CREATE PROCEDURE [dbo].[usp_Transaction_Insert] 
	-- Add the parameters for the stored procedure here
	@Account_Id INT, 
	@Category_Type_Id INT,
	@Transaction_Type_Id INT,
	@Amount MONEY,
	@Currency_Type_Id INT,
	@Src_Account_Id INT = NULL,
	@ThirdParty_Transaction_Id VARCHAR(256) = NULL,
	@Financial_Platform VARCHAR(256)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY

		BEGIN TRAN

		IF(@Account_ID IS NULL OR @Account_ID = 0)
			BEGIN

				RAISERROR ('usp_Transaction_Insert, @Account_ID is unacceptable', -1, -1);

			END

			IF(@Category_Type_ID IS NULL OR @Category_Type_ID = 0)
			BEGIN

				RAISERROR ('usp_Transaction_Insert, @Category_Type_ID is unacceptable', -1, -1);

			END

			IF(@Transaction_Type_ID IS NULL OR @Transaction_Type_ID = 0)
			BEGIN

				RAISERROR ('usp_Transaction_Insert, @Transaction_Type_ID  is unacceptable', -1, -1);

			END

			IF(@Currency_Type_ID IS NULL OR @Currency_Type_ID = 0)
			BEGIN

				RAISERROR ('usp_Transaction_Insert, @Currency_Type_ID is unacceptable', -1, -1);

			END

			IF(@Amount IS NULL OR @Account_ID = 0)
			BEGIN

				RAISERROR ('usp_Transaction_Insert, @Amount is unacceptable', -1, -1);

			END

			-- We need to get the description based on the category of this transaction
			DECLARE @category_description AS VARCHAR(250) = (SELECT [Description] FROM Category_Types WHERE [Id] = @Category_Type_ID)

			-- We need to get the description based on the transaction type
			DECLARE @transaction_description AS VARCHAR(250) = (SELECT [Description] FROM Transaction_Types WHERE [Id] = @Transaction_Type_ID)

			-- Control flow based on whether this transaction originated from an internal account.
			IF(@Src_Account_ID IS NULL OR @Src_Account_ID = 0)
			BEGIN

				INSERT INTO Transactions
				(
					[Account_Id],
					[Category_Type_Id],
					[Transaction_Type_Id],
					[DateTime_Created],
					[Description],
					[Amount],
					[Transaction_State],
					[Currency_Type_Id],
					[Outside_Transaction_Id],
					[Outside_Financial_Platform]
				)
				VALUES 
				(
					@Account_Id,
					@Category_Type_Id,
					@Transaction_Type_Id,
					GETDATE(),
					CONCAT(@category_description, ' : ', @transaction_description, ' : ', (SELECT GETDATE() AS VARCHAR)),
					@Amount,
					'PENDING',
					@Currency_Type_Id,
					@ThirdParty_Transaction_Id,
					@Financial_Platform
				)

				SELECT SCOPE_IDENTITY();

			END
			-- If it did record it in this record so we can keep track of all internal movements
			ELSE
			BEGIN

				INSERT INTO Transactions 
				(
					[Account_Id],
					[Category_Type_Id],
					[Transaction_Type_Id],
					[DateTime_Created],
					[Description],
					[Amount],
					[src_Account_Id],
					[Transaction_State],
					[Currency_Type_Id]
				)
				VALUES 
				(
					@Account_ID,
					@Category_Type_ID,
					@Transaction_Type_ID ,
					GETDATE(),
					CONCAT(@category_description, ' : ', @transaction_description, ' : ', (SELECT GETDATE() AS VARCHAR)),
					@Amount,
					@Src_Account_ID,
					'PENDING',
					@Currency_Type_ID
				)

				SELECT SCOPE_IDENTITY();

			END

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

		RETURN

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Transaction_Insert, ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

		RETURN

	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[usp_Update_PROC]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 6/28/2016
-- Description:	This procedure will affect updates and changes to PROC records on our PROCs table
-- =============================================
CREATE PROCEDURE [dbo].[usp_Update_PROC] 
	-- Add the parameters for the stored procedure here
	@PROC_Id INT, 
	@Investor_Id INT,
	@Entrepreneur_Id INT,
	@Investment_Amount MONEY = NULL,
	@Revenue_Percentage DECIMAL(8,6) = NULL,
	@DateTime_Begin DATETIME2(7) = NULL,
	@DateTime_End DATETIME2(7) = NULL,
	@User_Accepts_PROC BIT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			
			-- Establish whether or not this is the entrepreneur or investor updating the PROC.
			DECLARE @entrepreneur_owner BIT
			DECLARE @investor_owner BIT

			-- find if the user is the entrepreneur owner
			IF( 
				(
					SELECT Proj.[Entrepreneur_Id] 
					FROM PROCs P 
					JOIN Projects Proj ON P.[Project_Id] = Proj.[Id]
					WHERE P.[Id] = @PROC_Id 
				) = @Entrepreneur_Id 
			)
			BEGIN

				SET @entrepreneur_owner = 1

			END
			ELSE
			BEGIN

				SET @entrepreneur_owner = 0

			END

			-- find if the user is the investor owner
			IF(
				(
					SELECT P.[Investor_Id]
					FROM PROCS P
					WHERE P.[Id] = @PROC_Id
				) = @Investor_Id
			)
			BEGIN

				SET @investor_owner = 1

			END
			ELSE
			BEGIN

				SET @investor_owner = 0

			END

			-- update statement if this is the entrepreneur/project owner
			IF(@entrepreneur_owner = 1)
			BEGIN
				
				UPDATE PROCs
				SET [Revenue_Percentage] = COALESCE(@Revenue_Percentage, [Revenue_Percentage]),
					[DateTime_Enforcement_Begin] = COALESCE(@DateTime_Begin, [DateTime_Enforcement_Begin]),
					[DateTime_Enforcement_End] = COALESCE(@DateTime_End, [DateTime_Enforcement_End]),
					[Count_Project_Revised] = [Count_Project_Revised] + 1

				IF(@User_Accepts_PROC <> NULL)
				BEGIN

					UPDATE PROCs
					SET [Accepted_Project] = @User_Accepts_PROC,
						[DateTime_Accepted_Project] = GETDATE()

				END

			END

			-- update statement if this is the investor owner
			IF(@investor_owner = 1)
			BEGIN

				UPDATE PROCs
				SET [Revenue_Percentage] = COALESCE(@Revenue_Percentage, [Revenue_Percentage]),
					[Investment_Amount] = COALESCE(@Investment_Amount, [Investment_Amount]),
					[DateTime_Enforcement_Begin] = COALESCE(@DateTime_Begin, [DateTime_Enforcement_Begin]),
					[DateTime_Enforcement_End] = COALESCE(@DateTime_End, [DateTime_Enforcement_End]),
					[Count_Investor_Revised] = [Count_Investor_Revised] + 1

				IF(@User_Accepts_PROC <> NULL)
				BEGIN

					UPDATE PROCs
					SET [Accepted_Investor] = @User_Accepts_PROC,
						[DateTime_Accepted_Investor] = GETDATE()

				END

			END

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Update_PROC', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_update_Project]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 6/16/2016
-- Description:	This stored procedure will update the profile details of a given project
-- =============================================
CREATE PROCEDURE [dbo].[usp_update_Project] 
	-- Add the parameters for the stored procedure here
	@User_id NVARCHAR(128), 
	@Entrepreneur_Id INT,
	@Project_Id INT,
	@Name NVARCHAR(150),
	@Summary VARCHAR(5000),
	@Profile_Public BIT,
	@Investment_Goal MONEY
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			IF EXISTS
			(
				SELECT *
				FROM Users U
				JOIN Profiles P ON U.[Id] = P.[User_Id]
				JOIN Projects Pro ON P.[Id] = Pro.[Entrepreneur_Id]
				WHERE U.[Id] = @User_Id
				AND P.[ProfileType_Id] = 1
				AND P.[Id] = @Entrepreneur_Id
				AND Pro.[Id] = @Project_Id
			)
			BEGIN

				UPDATE Projects
				SET [Name] = COALESCE(@Name, [Name]),
					[Summary] = COALESCE(@Summary, [Summary]),
					[Profile_Public] = COALESCE(@Profile_Public, [Profile_Public]),
					[Investment_Goal] = COALESCE(@Investment_Goal, [Investment_Goal])
				WHERE [Id] = @Project_Id

			END
			ELSE
			BEGIN

				RAISERROR('Non-Relation error, when attempting to update profile data', 16, 1);

			END

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_update_Project', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Update_Transaction]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 7/4/2016
-- Description:	This stored procedure will update a given transaction given it's ID
-- =============================================
CREATE PROCEDURE [dbo].[usp_Update_Transaction] 
	-- Add the parameters for the stored procedure here
	@Transaction_Id INT, 
	@Amount MONEY = NULL,
	@src_Account_Id INT = NULL,
	@Transaction_State VARCHAR(250) = NULL,
	@Outside_Transaction_Id VARCHAR(250) = NULL,
	@Outside_Financial_Platform VARCHAR(250) = NULL,
	@DateTime_Processed DateTime2(7) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY

		BEGIN TRAN

			-- Insert statements for procedure here
			
			UPDATE Transactions
			SET [Amount] = COALESCE(@Amount, [Amount]),
				[src_Account_Id] = COALESCE(@src_Account_Id, [src_Account_Id]),
				[Transaction_State] = COALESCE(@Transaction_State, [Transaction_State]),
				[Outside_Transaction_Id] = COALESCE(@Outside_Transaction_Id, [Outside_Transaction_Id]),
				[Outside_Financial_Platform] = COALESCE(@Outside_Financial_Platform, [Outside_Financial_Platform]),
				[DateTime_Processed] = COALESCE(@DateTime_Processed, [DateTime_Processed])
			WHERE [Id] = @Transaction_Id

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_Update_Transaction', ', ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[usp_Users_Insert]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 5/20/2016
-- Description:	This will register a new record on ourn Users table which will represent a new user.
-- =============================================
CREATE PROCEDURE [dbo].[usp_Users_Insert] 
	-- Add the parameters for the stored procedure here
	@UserName NVARCHAR(256), 
	@PasswordHash NVARCHAR(256),
	@Id NVARCHAR(MAX),
	@Name NVARCHAR(256)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY

		BEGIN TRANSACTION

			INSERT INTO Users
            (
				[Id],
                [PasswordHash],
                [EmailConfirmed],
                [PhoneNumberconfirmed],
                [TwoFactorEnabled],
                [LockoutEnabled],
                [AccessFailedCount],
                [UserName],
                [Name]
            )
            VALUES
            (
				@Id,
                @PasswordHash,
				0,
                0,
				0,
                0,
                0,
                @UserName,
                @Name
            )

			EXECUTE usp_Entrepreneur_Insert @User_Id = @Id;

			EXECUTE usp_Investor_Insert @User_Id = @Id;

		COMMIT

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID;

	END TRY
	BEGIN CATCH
		
		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_User_Insert, ', ERROR_MESSAGE());

		EXECUTE Utility.Log_ProcedureCall @Object_Id = @@PROCID, @Additional_Info = @err_msg;

		RAISERROR (@err_msg, 16, 1);

	END CATCH
		
END

GO
/****** Object:  StoredProcedure [Utility].[Log_ProcedureCall]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 5/24/2016
-- Description:	This stored procedure will log data the stored procedure is calling this
-- =============================================
CREATE PROCEDURE [Utility].[Log_ProcedureCall] 
	-- Add the parameters for the stored procedure here
	@Object_Id INT,
	@DataBase_Id INT = NULL,
	@Additional_Info VARCHAR(MAX) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @ProcedureName NVARCHAR(400);
	SELECT @DataBase_Id = COALESCE(
		@DataBase_Id, DB_ID()),
		@ProcedureName = COALESCE(
			QUOTENAME(DB_NAME(@DataBase_Id)) + '.' 
			+ QUOTENAME(OBJECT_SCHEMA_NAME(@Object_Id, @DataBase_Id)) + '.' 
			+ QUOTENAME(OBJECT_NAME(@Object_Id, @DataBase_Id)), 
			ERROR_PROCEDURE()
		)
	INSERT Utility.dbo.StoredProcedure_Logs
	(
		[DataBase_Id],
		[Object_Id],
		[ProcedureName],
		[ErrorLine],
		[ErrorMessage],
		[UserName],
		[HostName],
		[IP_Address],
		[SPID],
		[Additional_Info]
	)
	VALUES
	(
		@DataBase_Id,
		@Object_Id,
		@ProcedureName,
		ERROR_LINE(),
		ERROR_MESSAGE(),
		CURRENT_USER,
		HOST_NAME(),
		(SELECT CONVERT(CHAR(15), CONNECTIONPROPERTY('client_net_address'))),
		@@SPID,
		@Additional_Info
	)
END

GO
/****** Object:  DdlTrigger [rds_deny_backups_trigger]    Script Date: 7/12/2016 2:17:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [rds_deny_backups_trigger] ON DATABASE WITH EXECUTE AS 'dbo' FOR
 ADD_ROLE_MEMBER, GRANT_DATABASE AS BEGIN
   SET NOCOUNT ON;
   SET ANSI_PADDING ON;
 
   DECLARE @data xml;
   DECLARE @user sysname;
   DECLARE @role sysname;
   DECLARE @type sysname;
   DECLARE @sql NVARCHAR(MAX);
   DECLARE @permissions TABLE(name sysname PRIMARY KEY);
   
   SELECT @data = EVENTDATA();
   SELECT @type = @data.value('(/EVENT_INSTANCE/EventType)[1]', 'sysname');
    
   IF @type = 'ADD_ROLE_MEMBER' BEGIN
      SELECT @user = @data.value('(/EVENT_INSTANCE/ObjectName)[1]', 'sysname'),
       @role = @data.value('(/EVENT_INSTANCE/RoleName)[1]', 'sysname');

      IF @role IN ('db_owner', 'db_backupoperator') BEGIN
         SELECT @sql = 'DENY BACKUP DATABASE, BACKUP LOG TO ' + QUOTENAME(@user);
         EXEC(@sql);
      END
   END ELSE IF @type = 'GRANT_DATABASE' BEGIN
      INSERT INTO @permissions(name)
      SELECT Permission.value('(text())[1]', 'sysname') FROM
       @data.nodes('/EVENT_INSTANCE/Permissions/Permission')
      AS DatabasePermissions(Permission);
      
      IF EXISTS (SELECT * FROM @permissions WHERE name IN ('BACKUP DATABASE',
       'BACKUP LOG'))
         RAISERROR('Cannot grant backup database or backup log', 15, 1) WITH LOG;       
   END
END


GO
ENABLE TRIGGER [rds_deny_backups_trigger] ON DATABASE
GO
USE [master]
GO
ALTER DATABASE [ProudSourceDataBase] SET  READ_WRITE 
GO
