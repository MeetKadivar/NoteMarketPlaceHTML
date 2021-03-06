USE [master]
GO
/****** Object:  Database [NoteMarketPlace]    Script Date: 3/19/2021 2:56:23 PM ******/
CREATE DATABASE [NoteMarketPlace]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NoteMarketPlace', FILENAME = N'E:\sql-17\New folder\MSSQL14.MSSQLSERVER\MSSQL\DATA\NoteMarketPlace.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NoteMarketPlace_log', FILENAME = N'E:\sql-17\New folder\MSSQL14.MSSQLSERVER\MSSQL\DATA\NoteMarketPlace_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [NoteMarketPlace] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NoteMarketPlace].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NoteMarketPlace] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET ARITHABORT OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NoteMarketPlace] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NoteMarketPlace] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NoteMarketPlace] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NoteMarketPlace] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET RECOVERY FULL 
GO
ALTER DATABASE [NoteMarketPlace] SET  MULTI_USER 
GO
ALTER DATABASE [NoteMarketPlace] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NoteMarketPlace] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NoteMarketPlace] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NoteMarketPlace] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NoteMarketPlace] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'NoteMarketPlace', N'ON'
GO
ALTER DATABASE [NoteMarketPlace] SET QUERY_STORE = OFF
GO
USE [NoteMarketPlace]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 3/19/2021 2:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[CountryCode] [varchar](100) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Downloads]    Script Date: 3/19/2021 2:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Downloads](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[Seller] [int] NOT NULL,
	[Downloader] [int] NOT NULL,
	[IsSellerHasAllowedDownload] [bit] NOT NULL,
	[AttachmentPath] [varchar](max) NULL,
	[IsAttachmentDownloaded] [bit] NOT NULL,
	[AttachmentDownloadedDate] [datetime] NULL,
	[IsPaid] [bit] NOT NULL,
	[PurchasedPrice] [float] NULL,
	[NoteTitle] [varchar](100) NOT NULL,
	[NoteCategory] [varchar](100) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Downloads] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteCategories]    Script Date: 3/19/2021 2:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteCategories](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_NoteCategories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteTypes]    Script Date: 3/19/2021 2:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_NoteTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReferenceData]    Script Date: 3/19/2021 2:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReferenceData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](100) NOT NULL,
	[Datavalue] [varchar](100) NOT NULL,
	[RefCategory] [varchar](100) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_ReferenceData] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SellerNotes]    Script Date: 3/19/2021 2:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SellerNotes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SellerID] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[ActionedBy] [int] NULL,
	[AdminRemarks] [varchar](max) NULL,
	[PublishedDate] [datetime] NULL,
	[Title] [varchar](100) NOT NULL,
	[Category] [int] NOT NULL,
	[DisplayPicture] [varchar](500) NULL,
	[NoteType] [int] NULL,
	[NumberofPages] [int] NULL,
	[Description] [varchar](max) NOT NULL,
	[UniversityName] [varchar](200) NULL,
	[Country] [int] NULL,
	[Course] [varchar](100) NULL,
	[CourseCode] [varchar](100) NULL,
	[Professor] [varchar](100) NULL,
	[IsPaid] [bit] NOT NULL,
	[SellingPrice] [float] NULL,
	[NotesPreview] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_SellerNotes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SellerNotesAttachements]    Script Date: 3/19/2021 2:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SellerNotesAttachements](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[FileName] [varchar](100) NOT NULL,
	[FilePath] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_SellerNotesAttachements] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SellerNotesReportedIssues]    Script Date: 3/19/2021 2:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SellerNotesReportedIssues](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[ReportedByID] [int] NOT NULL,
	[AgainstDownloadID] [int] NOT NULL,
	[Remarks] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_SellerNotesReportedIssues] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SellerNotesReviews]    Script Date: 3/19/2021 2:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SellerNotesReviews](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[ReviewedByID] [int] NOT NULL,
	[AgainstDownloadsID] [int] NOT NULL,
	[Ratings] [float] NOT NULL,
	[Comments] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_SellerNotesReviews] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemConfigurations]    Script Date: 3/19/2021 2:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemConfigurations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Value] [varchar](max) NOT NULL,
	[CreateDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_SystemConfigurations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 3/19/2021 2:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[DOB] [date] NULL,
	[Gender] [int] NULL,
	[SecondaryEmailAddres] [varchar](100) NULL,
	[CountryCode] [varchar](5) NOT NULL,
	[PhoneNumber] [varchar](20) NOT NULL,
	[ProfilePicture] [varchar](500) NULL,
	[AddressLine1] [varchar](100) NOT NULL,
	[AddressLine2] [varchar](100) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[ZipCode] [varchar](50) NOT NULL,
	[Country] [varchar](50) NOT NULL,
	[University] [varchar](100) NULL,
	[College] [varchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 3/19/2021 2:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/19/2021 2:56:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Password] [varchar](24) NOT NULL,
	[IsEmailVerify] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ActivationCode] [uniqueidentifier] NULL,
	[ResetPasswordCode] [nvarchar](100) NULL,
 CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Countries] ON 

INSERT [dbo].[Countries] ([ID], [Name], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'India', N'91', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Countries] ([ID], [Name], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'USA', N'1', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Countries] ([ID], [Name], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, N'UK', N'101', NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Countries] OFF
GO
SET IDENTITY_INSERT [dbo].[Downloads] ON 

INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (6, 1, 4, 5, 1, NULL, 1, NULL, 0, NULL, N'Computer Science', N'TestCate.1', NULL, NULL, NULL, NULL)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (7, 1, 4, 6, 1, N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021211728911.pdf', 1, NULL, 0, NULL, N'IT', N'TestCate.2', NULL, NULL, NULL, NULL)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (8, 1, 4, 3, 1, N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021211728911.pdf', 1, NULL, 0, NULL, N'Accounts', N'TestCate.3', NULL, NULL, NULL, NULL)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (9, 1, 4, 5, 1, N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021211728911.pdf', 1, NULL, 0, NULL, N'Computer', N'TestCate.1', NULL, NULL, NULL, NULL)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (10, 1, 4, 5, 1, N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021211728911.pdf', 1, NULL, 0, NULL, N'Computer Science', N'TestCate.1', NULL, NULL, NULL, NULL)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1004, 1, 5, 4, 1, N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021211728911.pdf', 1, CAST(N'2021-02-24T21:57:34.347' AS DateTime), 0, 0, N'Sci-FI', N'Test-cate.101', CAST(N'2021-02-24T21:57:34.347' AS DateTime), 4, CAST(N'2021-02-24T21:57:34.347' AS DateTime), 4)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2002, 1, 4, 3, 1, N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021211728911.pdf', 1, CAST(N'2021-02-13T00:00:00.000' AS DateTime), 0, NULL, N'History', N'TestCate1', NULL, NULL, NULL, NULL)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3003, 1, 3, 4, 1, N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021211728911.pdf', 1, CAST(N'2021-03-16T21:26:43.357' AS DateTime), 1, 240, N'Computer Science', N'TestCate.1', CAST(N'2021-03-16T21:26:43.357' AS DateTime), 4, NULL, NULL)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3004, 2, 5, 4, 1, N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021211728911.pdf', 1, CAST(N'2021-03-16T21:26:43.357' AS DateTime), 1, 140, N'AI', N'TestCate.2', CAST(N'2021-03-16T21:26:43.357' AS DateTime), 4, NULL, NULL)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3005, 3, 6, 4, 1, N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021211728911.pdf', 1, CAST(N'2021-03-16T21:26:43.357' AS DateTime), 0, 0, N'ML', N'TestCate.3', CAST(N'2021-03-16T21:26:43.357' AS DateTime), 4, NULL, NULL)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3006, 5, 7, 4, 1, N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021211728911.pdf', 1, CAST(N'2021-03-16T21:26:43.357' AS DateTime), 1, 40, N'CA', N'TestCate.2', CAST(N'2021-03-16T21:26:43.357' AS DateTime), 4, NULL, NULL)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3007, 1006, 8, 4, 1, N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021211728911.pdf', 1, CAST(N'2021-03-16T21:26:43.357' AS DateTime), 1, 0, N'Social Science', N'TestCate.2', CAST(N'2021-03-16T21:26:43.357' AS DateTime), 4, NULL, NULL)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3008, 1, 3, 4, 1, N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021211728911.pdf', 1, CAST(N'2021-03-16T21:27:03.200' AS DateTime), 1, 240, N'Computer Science', N'TestCate.1', CAST(N'2021-03-16T21:27:03.200' AS DateTime), 4, NULL, NULL)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3009, 2, 5, 4, 1, N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021211728911.pdf', 1, CAST(N'2021-03-16T21:27:03.200' AS DateTime), 1, 140, N'AI', N'TestCate.2', CAST(N'2021-03-16T21:27:03.200' AS DateTime), 4, NULL, NULL)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3010, 3, 6, 4, 1, N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021211728911.pdf', 1, CAST(N'2021-03-16T21:27:03.200' AS DateTime), 0, 0, N'ML', N'TestCate.3', CAST(N'2021-03-16T21:27:03.200' AS DateTime), 4, NULL, NULL)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3011, 5, 7, 4, 1, N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021211728911.pdf', 1, CAST(N'2021-03-16T21:27:03.200' AS DateTime), 1, 40, N'CA', N'TestCate.2', CAST(N'2021-03-16T21:27:03.200' AS DateTime), 4, NULL, NULL)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3012, 1006, 8, 4, 1, N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021211728911.pdf', 1, CAST(N'2021-03-16T21:27:03.200' AS DateTime), 1, 0, N'Social Science', N'TestCate.2', CAST(N'2021-03-16T21:27:03.200' AS DateTime), 4, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Downloads] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteCategories] ON 

INSERT [dbo].[NoteCategories] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'TestCate. 1', N'Test1', CAST(N'2021-02-19T11:12:35.377' AS DateTime), NULL, CAST(N'2021-02-19T11:12:35.377' AS DateTime), NULL, 1)
INSERT [dbo].[NoteCategories] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'TestCate. 2', N'Test2', CAST(N'2021-02-19T11:12:35.377' AS DateTime), NULL, CAST(N'2021-02-19T11:12:35.377' AS DateTime), NULL, 1)
INSERT [dbo].[NoteCategories] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'TestCate. 3', N'Test3', CAST(N'2021-02-19T11:12:35.377' AS DateTime), NULL, CAST(N'2021-02-19T11:12:35.377' AS DateTime), NULL, 1)
INSERT [dbo].[NoteCategories] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, N'TestCate. 4', N'Test4', CAST(N'2021-02-19T11:12:35.377' AS DateTime), NULL, CAST(N'2021-02-19T11:12:35.377' AS DateTime), NULL, 1)
INSERT [dbo].[NoteCategories] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, N'TestCate. 5', N'Test5', CAST(N'2021-02-19T11:12:35.377' AS DateTime), NULL, CAST(N'2021-02-19T11:12:35.377' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[NoteCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteTypes] ON 

INSERT [dbo].[NoteTypes] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'Test Type 1', N'test', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[NoteTypes] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'Test Type 2', N'test', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[NoteTypes] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, N'Test Type3', N'test', NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[NoteTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[ReferenceData] ON 

INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'Male', N'M', N'Gender', CAST(N'2021-02-19T11:09:04.740' AS DateTime), NULL, CAST(N'2021-02-19T11:09:04.740' AS DateTime), NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'Female', N'Fe', N'Gender', CAST(N'2021-02-19T11:09:04.740' AS DateTime), NULL, CAST(N'2021-02-19T11:09:04.740' AS DateTime), NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'Unkown', N'U', N'Gender', CAST(N'2021-02-19T11:09:04.740' AS DateTime), NULL, CAST(N'2021-02-19T11:09:04.740' AS DateTime), NULL, 0)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, N'Paid', N'P', N'Selling Mode', CAST(N'2021-02-19T11:09:04.740' AS DateTime), NULL, CAST(N'2021-02-19T11:09:04.740' AS DateTime), NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, N'Free', N'F', N'Selling Mode', CAST(N'2021-02-19T11:09:04.740' AS DateTime), NULL, CAST(N'2021-02-19T11:09:04.740' AS DateTime), NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, N'Draft', N'Draft', N'Note Status', CAST(N'2021-02-19T11:09:04.740' AS DateTime), NULL, CAST(N'2021-02-19T11:09:04.740' AS DateTime), NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (7, N'Submitted For Review', N'Submitted For Review', N'Note Status', CAST(N'2021-02-19T11:09:04.740' AS DateTime), NULL, CAST(N'2021-02-19T11:09:04.740' AS DateTime), NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (8, N'In Review', N'In Review', N'Note Status', CAST(N'2021-02-19T11:09:04.740' AS DateTime), NULL, CAST(N'2021-02-19T11:09:04.740' AS DateTime), NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (9, N'Published', N'Approved', N'Note Status', CAST(N'2021-02-19T11:09:04.740' AS DateTime), NULL, CAST(N'2021-02-19T11:09:04.740' AS DateTime), NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (10, N'Rejected', N'Rejected', N'Note Status', CAST(N'2021-02-19T11:09:04.740' AS DateTime), NULL, CAST(N'2021-02-19T11:09:04.740' AS DateTime), NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (11, N'Removed', N'Removed', N'Note Status', CAST(N'2021-02-19T11:09:04.740' AS DateTime), NULL, CAST(N'2021-02-19T11:09:04.740' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[ReferenceData] OFF
GO
SET IDENTITY_INSERT [dbo].[SellerNotes] ON 

INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, 4, 6, NULL, NULL, CAST(N'2021-02-01T00:00:00.000' AS DateTime), N'Computer Science', 1, N'book_2.jpg', 1, 220, N'test note1', NULL, 1, N'CE', NULL, NULL, 1, 220, NULL, CAST(N'2021-02-19T11:21:01.817' AS DateTime), NULL, CAST(N'2021-02-19T11:21:01.817' AS DateTime), NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, 4, 7, NULL, NULL, CAST(N'2021-01-15T00:00:00.000' AS DateTime), N'Account', 1, N'book_3.jpg', 3, 220, N'test note2', NULL, 2, N'CA', NULL, NULL, 1, 550, NULL, CAST(N'2021-02-19T11:21:01.817' AS DateTime), NULL, CAST(N'2021-02-19T11:21:01.817' AS DateTime), NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, 4, 8, NULL, NULL, CAST(N'2021-01-30T00:00:00.000' AS DateTime), N'IT', 1, N'book_4.jpg', 4, 220, N'test note3', NULL, 1, N'Computer', NULL, NULL, 1, 204, NULL, CAST(N'2021-02-19T11:21:01.817' AS DateTime), NULL, CAST(N'2021-02-19T11:21:01.817' AS DateTime), NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, 4, 9, NULL, NULL, CAST(N'2021-02-13T00:00:00.000' AS DateTime), N'Computer Science', 1, N'book_6.jpg', 1, 220, N'test note5', NULL, 1, N'CE', NULL, NULL, 0, NULL, NULL, CAST(N'2021-02-19T11:21:01.817' AS DateTime), NULL, CAST(N'2021-02-19T11:21:01.817' AS DateTime), NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1006, 4, 9, NULL, NULL, CAST(N'2021-01-31T00:00:00.000' AS DateTime), N'CA', 1, N'book_2.jpg', 3, 125, N'test note1', NULL, 2, NULL, NULL, NULL, 1, 100, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2002, 4, 9, NULL, NULL, CAST(N'2021-01-12T00:00:00.000' AS DateTime), N'Social Studies', 1, N'book_2.jpg', 3, 450, N'test note4', NULL, 1, N'Arts', NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2004, 4, 9, NULL, NULL, CAST(N'2021-02-03T00:00:00.000' AS DateTime), N'AI', 1, N'book_3.jpg', 4, 840, N'test note5', NULL, 1, N'CE', NULL, NULL, 0, 450, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2005, 4, 9, NULL, NULL, CAST(N'2021-01-04T00:00:00.000' AS DateTime), N'Open Source', 1, N'book_4.jpg', 1, 740, N'test note4', NULL, 2, N'CE', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6006, 4, 7, NULL, NULL, CAST(N'2021-03-12T22:17:28.643' AS DateTime), N'pdf test101', 1, N'book_6211728628.jpg', 1, 780, N'pdf test101', N'MNm', 1, N'CS', N'cs101', N'mn', 1, 250, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6007, 4, 7, NULL, NULL, CAST(N'2021-03-13T11:31:28.543' AS DateTime), N'test 13', 1, N'book_1213128203.jpg', 1, 450, N'test 13', N'nxc', 1, N'CS', N'CS101', N'skadbm', 1, 250, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6008, 4, 7, NULL, NULL, CAST(N'2021-03-13T11:41:58.073' AS DateTime), N'test 103', 1, N'book_1214158055.jpg', 1, 70, N'test 103', N'nxc', 1, N'CS', N'CS101', N'CSC', 1, 250, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6009, 4, 7, NULL, NULL, CAST(N'2021-03-13T12:03:31.297' AS DateTime), N'final test', 1, N'book_1210330905.jpg', 1, 741, N'test', N'nxc', 1, N'CS', N'CS101', N'n', 1, 250, N'TSC Training Plan for Batch 2020-2021210330915.pdf', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6010, 4, 10, NULL, N'test', CAST(N'2021-03-18T11:25:08.750' AS DateTime), N'CS', 2, N'book_6211728628.jpg', 3, 140, N'reject test1', N'CVM', 1, N'CE', N'cs210', N'N.N.M', 1, 120, N'TSC Training Plan for Batch 2020-2021210330915.pdf', CAST(N'2021-03-18T11:25:08.750' AS DateTime), 4, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6011, 4, 10, NULL, N'test', CAST(N'2021-03-18T11:25:08.750' AS DateTime), N'CE', 2, N'book_6211728628.jpg', 3, 140, N'reject test2', N'CVM', 1, N'CE', N'cs210', N'N.N.M', 1, 100, N'TSC Training Plan for Batch 2020-2021210330915.pdf', CAST(N'2021-03-18T11:25:08.750' AS DateTime), 4, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6012, 4, 10, NULL, N'test', CAST(N'2021-03-18T11:25:08.750' AS DateTime), N'CA', 2, N'book_6211728628.jpg', 3, 140, N'reject test3', N'CVM', 1, N'CE', N'cs210', N'N.N.M', 0, NULL, N'TSC Training Plan for Batch 2020-2021210330915.pdf', CAST(N'2021-03-18T11:25:08.750' AS DateTime), 4, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6013, 4, 10, NULL, N'test', CAST(N'2021-03-18T11:25:08.750' AS DateTime), N'Computer', 2, N'book_6211728628.jpg', 3, 140, N'reject test4', N'CVM', 1, N'CE', N'cs210', N'N.N.M', 0, NULL, N'TSC Training Plan for Batch 2020-2021210330915.pdf', CAST(N'2021-03-18T11:25:08.750' AS DateTime), 4, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6014, 4, 10, NULL, N'test', CAST(N'2021-03-18T11:25:08.750' AS DateTime), N'Commrce', 2, N'book_6211728628.jpg', 3, 140, N'reject test5', N'CVM', 1, N'CE', N'cs210', N'N.N.M', 1, 20, N'TSC Training Plan for Batch 2020-2021210330915.pdf', CAST(N'2021-03-18T11:25:08.750' AS DateTime), 4, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6015, 4, 10, NULL, N'test', CAST(N'2021-03-18T11:25:08.750' AS DateTime), N'ABCD', 2, N'book_6211728628.jpg', 3, 140, N'reject test6', N'CVM', 1, N'CE', N'cs210', N'N.N.M', 1, 220, N'TSC Training Plan for Batch 2020-2021210330915.pdf', CAST(N'2021-03-18T11:25:08.750' AS DateTime), 4, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[SellerNotes] OFF
GO
SET IDENTITY_INSERT [dbo].[SellerNotesAttachements] ON 

INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, 6006, N'TSC Training Plan for Batch 2020-2021211728911.pdf', N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021211728911.pdf', CAST(N'2021-03-12T22:17:28.927' AS DateTime), 4, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, 6008, N'TSC Training Plan for Batch 2020-2021214158411.pdf', N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021214158411.pdf', CAST(N'2021-03-13T11:41:58.433' AS DateTime), 4, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (7, 6009, N'TSC Training Plan for Batch 2020-2021210331604.pdf', N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021210331604.pdf', CAST(N'2021-03-13T12:03:31.623' AS DateTime), 4, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (8, 6010, N'TSC Training Plan for Batch 2020-2021211728911.pdf', N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021211728911.pdf', CAST(N'2021-03-18T11:30:30.850' AS DateTime), 4, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (9, 6011, N'TSC Training Plan for Batch 2020-2021211728911.pdf', N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021211728911.pdf', CAST(N'2021-03-18T11:30:30.850' AS DateTime), 4, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (10, 6012, N'TSC Training Plan for Batch 2020-2021211728911.pdf', N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021211728911.pdf', CAST(N'2021-03-18T11:30:30.850' AS DateTime), 4, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (11, 6013, N'TSC Training Plan for Batch 2020-2021211728911.pdf', N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021211728911.pdf', CAST(N'2021-03-18T11:30:30.850' AS DateTime), 4, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (12, 6014, N'TSC Training Plan for Batch 2020-2021211728911.pdf', N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021211728911.pdf', CAST(N'2021-03-18T11:30:30.850' AS DateTime), 4, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (13, 6015, N'TSC Training Plan for Batch 2020-2021211728911.pdf', N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021211728911.pdf', CAST(N'2021-03-18T11:30:30.850' AS DateTime), 4, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (14, 6007, N'TSC Training Plan for Batch 2020-2021211728911.pdf', N'E:\MEET\visual_studio_app\final\NoteMarketPlace\NotePdf\TSC Training Plan for Batch 2020-2021211728911.pdf', CAST(N'2021-03-18T11:30:30.850' AS DateTime), 4, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[SellerNotesAttachements] OFF
GO
SET IDENTITY_INSERT [dbo].[UserProfile] ON 

INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddres], [CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, 4, CAST(N'2021-03-12' AS Date), 1, NULL, N'91', N'91 9075426345', N'E:\MEET\visual_studio_app\final\NoteMarketPlace\Images\Member\person_2213327287.jpg', N'155 mountain leo road', N'test', N'North river', N'Nova socatia', N'B6L 6M3', N'India', N'test', N'test', CAST(N'2021-03-15T23:33:33.913' AS DateTime), 4, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[UserProfile] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRoles] ON 

INSERT [dbo].[UserRoles] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'Member', N'Normal users', CAST(N'2021-02-17T13:20:09.553' AS DateTime), 1, CAST(N'2021-02-17T13:20:09.553' AS DateTime), 1, 1)
INSERT [dbo].[UserRoles] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'Admin', N'Admin', CAST(N'2021-02-17T13:20:09.553' AS DateTime), 1, CAST(N'2021-02-17T13:20:09.553' AS DateTime), 1, 1)
INSERT [dbo].[UserRoles] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'SuperAdmin', N'Super Admin ', CAST(N'2021-02-17T13:20:09.553' AS DateTime), 1, CAST(N'2021-02-17T13:20:09.553' AS DateTime), 1, 1)
SET IDENTITY_INSERT [dbo].[UserRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerify], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ActivationCode], [ResetPasswordCode]) VALUES (3, 1, N'm', N'm', N'm@m.com', N'123456', 1, NULL, NULL, NULL, NULL, 1, NULL, NULL)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerify], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ActivationCode], [ResetPasswordCode]) VALUES (4, 1, N'test profile', N'test profile last', N'test1@test.com', N'1234', 0, NULL, NULL, NULL, NULL, 1, NULL, NULL)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerify], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ActivationCode], [ResetPasswordCode]) VALUES (5, 1, N'ftest2', N'ltest2', N'test2@test.com', N'1234', 0, CAST(N'2021-02-19T11:00:44.040' AS DateTime), 3, CAST(N'2021-02-19T11:00:44.040' AS DateTime), 3, 1, NULL, NULL)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerify], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ActivationCode], [ResetPasswordCode]) VALUES (6, 1, N'ftest3', N'ltest3', N'test3@test.com', N'1234', 0, CAST(N'2021-02-19T11:00:44.040' AS DateTime), 3, CAST(N'2021-02-19T11:00:44.040' AS DateTime), 3, 1, NULL, NULL)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerify], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ActivationCode], [ResetPasswordCode]) VALUES (7, 1, N'test101', N'ltest101', N'test101@test.com', N'1234', 0, CAST(N'2021-02-21T16:22:13.800' AS DateTime), 3, CAST(N'2021-02-21T16:22:13.800' AS DateTime), 3, 1, NULL, NULL)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerify], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ActivationCode], [ResetPasswordCode]) VALUES (8, 1, N'TestActi1', N'LTestActi1', N'poorgsmer88@gmail.com', N'123456', 0, CAST(N'2021-02-21T16:38:22.563' AS DateTime), 3, CAST(N'2021-02-21T16:38:22.563' AS DateTime), 3, 1, NULL, NULL)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerify], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ActivationCode], [ResetPasswordCode]) VALUES (9, 1, N'test102', N'test102', N'poorgsmer888@gmail.com', N'123456', 0, CAST(N'2021-02-21T16:44:39.680' AS DateTime), 3, CAST(N'2021-02-21T16:44:39.680' AS DateTime), 3, 1, NULL, NULL)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerify], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ActivationCode], [ResetPasswordCode]) VALUES (10, 1, N'test102', N'test102', N'poorgamer888@gmail.com', N'1963823881', 0, CAST(N'2021-02-21T16:45:21.023' AS DateTime), 3, CAST(N'2021-02-21T16:45:21.023' AS DateTime), 3, 1, NULL, N'1963823881')
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerify], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ActivationCode], [ResetPasswordCode]) VALUES (13, 1, N'asdj', N'kasdms', N'sokihoh897@mayhco.com', N'1234', 0, CAST(N'2021-02-21T19:35:51.937' AS DateTime), 3, CAST(N'2021-02-21T19:35:51.937' AS DateTime), 3, 1, NULL, NULL)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerify], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ActivationCode], [ResetPasswordCode]) VALUES (1002, 1, N'test10004', N'Ltest10004', N'vugnahiydi@nedoz.com', N'1234', 0, NULL, NULL, NULL, NULL, 1, N'de8bad16-fc6a-486e-8472-497cd7e082cb', NULL)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerify], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ActivationCode], [ResetPasswordCode]) VALUES (1003, 1, N'test10005', N'Ltest10005', N'parkicuydo@nedoz.com', N'1234', 0, NULL, NULL, NULL, NULL, 1, N'2a688be8-d11c-44a7-b553-4974d1839311', NULL)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerify], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ActivationCode], [ResetPasswordCode]) VALUES (1004, 1, N'test10006', N'Ltest10006', N'572kldajw1@inscriptio.in', N'1234', 1, NULL, NULL, NULL, NULL, 1, N'2c5661f5-6f8f-4556-afff-3b6f6e6dd4b1', NULL)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerify], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ActivationCode], [ResetPasswordCode]) VALUES (1005, 1, N'test15', N'ltest15', N'jakneferze@nedoz.com', N'1234', 0, NULL, NULL, NULL, NULL, 1, N'4536d731-fe16-4b8b-955b-bc89d001691c', NULL)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerify], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ActivationCode], [ResetPasswordCode]) VALUES (1006, 1, N'test17', N'ltest17', N'kedobop461@mayhco.com', N'1234', 1, NULL, NULL, NULL, NULL, 1, N'2e8ee127-68a1-4050-9091-5905c29a0279', NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Downloads]  WITH CHECK ADD  CONSTRAINT [FK_Downloads_SellerNotes] FOREIGN KEY([NoteID])
REFERENCES [dbo].[SellerNotes] ([ID])
GO
ALTER TABLE [dbo].[Downloads] CHECK CONSTRAINT [FK_Downloads_SellerNotes]
GO
ALTER TABLE [dbo].[Downloads]  WITH CHECK ADD  CONSTRAINT [FK_Downloads_Users] FOREIGN KEY([Seller])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Downloads] CHECK CONSTRAINT [FK_Downloads_Users]
GO
ALTER TABLE [dbo].[Downloads]  WITH CHECK ADD  CONSTRAINT [FK_Downloads_Users1] FOREIGN KEY([Downloader])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Downloads] CHECK CONSTRAINT [FK_Downloads_Users1]
GO
ALTER TABLE [dbo].[SellerNotes]  WITH CHECK ADD  CONSTRAINT [FK_SellerNotes_Countries] FOREIGN KEY([Country])
REFERENCES [dbo].[Countries] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SellerNotes] CHECK CONSTRAINT [FK_SellerNotes_Countries]
GO
ALTER TABLE [dbo].[SellerNotes]  WITH CHECK ADD  CONSTRAINT [FK_SellerNotes_NoteCategories] FOREIGN KEY([Category])
REFERENCES [dbo].[NoteCategories] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SellerNotes] CHECK CONSTRAINT [FK_SellerNotes_NoteCategories]
GO
ALTER TABLE [dbo].[SellerNotes]  WITH CHECK ADD  CONSTRAINT [FK_SellerNotes_NoteTypes] FOREIGN KEY([NoteType])
REFERENCES [dbo].[NoteTypes] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SellerNotes] CHECK CONSTRAINT [FK_SellerNotes_NoteTypes]
GO
ALTER TABLE [dbo].[SellerNotes]  WITH CHECK ADD  CONSTRAINT [FK_SellerNotes_ReferenceData] FOREIGN KEY([Status])
REFERENCES [dbo].[ReferenceData] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SellerNotes] CHECK CONSTRAINT [FK_SellerNotes_ReferenceData]
GO
ALTER TABLE [dbo].[SellerNotes]  WITH CHECK ADD  CONSTRAINT [FK_SellerNotes_Users] FOREIGN KEY([SellerID])
REFERENCES [dbo].[Users] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SellerNotes] CHECK CONSTRAINT [FK_SellerNotes_Users]
GO
ALTER TABLE [dbo].[SellerNotes]  WITH CHECK ADD  CONSTRAINT [FK_SellerNotes_Users1] FOREIGN KEY([ActionedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SellerNotes] CHECK CONSTRAINT [FK_SellerNotes_Users1]
GO
ALTER TABLE [dbo].[SellerNotesAttachements]  WITH CHECK ADD  CONSTRAINT [FK_SellerNotesAttachements_SellerNotes] FOREIGN KEY([NoteID])
REFERENCES [dbo].[SellerNotes] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SellerNotesAttachements] CHECK CONSTRAINT [FK_SellerNotesAttachements_SellerNotes]
GO
ALTER TABLE [dbo].[SellerNotesReportedIssues]  WITH CHECK ADD  CONSTRAINT [FK_SellerNotesReportedIssues_Downloads] FOREIGN KEY([AgainstDownloadID])
REFERENCES [dbo].[Downloads] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SellerNotesReportedIssues] CHECK CONSTRAINT [FK_SellerNotesReportedIssues_Downloads]
GO
ALTER TABLE [dbo].[SellerNotesReportedIssues]  WITH CHECK ADD  CONSTRAINT [FK_SellerNotesReportedIssues_SellerNotes] FOREIGN KEY([NoteID])
REFERENCES [dbo].[SellerNotes] ([ID])
GO
ALTER TABLE [dbo].[SellerNotesReportedIssues] CHECK CONSTRAINT [FK_SellerNotesReportedIssues_SellerNotes]
GO
ALTER TABLE [dbo].[SellerNotesReportedIssues]  WITH CHECK ADD  CONSTRAINT [FK_SellerNotesReportedIssues_Users] FOREIGN KEY([ReportedByID])
REFERENCES [dbo].[Users] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SellerNotesReportedIssues] CHECK CONSTRAINT [FK_SellerNotesReportedIssues_Users]
GO
ALTER TABLE [dbo].[SellerNotesReviews]  WITH CHECK ADD  CONSTRAINT [FK_SellerNotesReviews_Downloads] FOREIGN KEY([AgainstDownloadsID])
REFERENCES [dbo].[Downloads] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SellerNotesReviews] CHECK CONSTRAINT [FK_SellerNotesReviews_Downloads]
GO
ALTER TABLE [dbo].[SellerNotesReviews]  WITH CHECK ADD  CONSTRAINT [FK_SellerNotesReviews_SellerNotes] FOREIGN KEY([NoteID])
REFERENCES [dbo].[SellerNotes] ([ID])
GO
ALTER TABLE [dbo].[SellerNotesReviews] CHECK CONSTRAINT [FK_SellerNotesReviews_SellerNotes]
GO
ALTER TABLE [dbo].[SellerNotesReviews]  WITH CHECK ADD  CONSTRAINT [FK_SellerNotesReviews_Users] FOREIGN KEY([ReviewedByID])
REFERENCES [dbo].[Users] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SellerNotesReviews] CHECK CONSTRAINT [FK_SellerNotesReviews_Users]
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_ReferenceData] FOREIGN KEY([Gender])
REFERENCES [dbo].[ReferenceData] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_ReferenceData]
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_USERS_UserRoles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[UserRoles] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_USERS_UserRoles]
GO
USE [master]
GO
ALTER DATABASE [NoteMarketPlace] SET  READ_WRITE 
GO
