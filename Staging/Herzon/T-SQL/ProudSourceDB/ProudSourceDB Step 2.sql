USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 3/18/2016 3:46:46 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Users](
	[User_ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](255) NULL,
	[Email_Verified] [bit] NOT NULL,
	[Created_Date] [datetime] NOT NULL,
	[Last_Logon] [datetime] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[Deleted_Date] [datetime] NULL,
 CONSTRAINT [PK_User_ID] PRIMARY KEY CLUSTERED 
(
	[User_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Entrepreneurs]    Script Date: 3/18/2016 3:50:08 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Entrepreneurs](
	[Entrepreneur_ID] [int] IDENTITY(1,1) NOT NULL,
	[User_ID] [int] NOT NULL,
	[Created_Date] [datetime] NOT NULL,
	[Name] [varchar](255) NULL,
	[Last_Logon] [datetime] NOT NULL,
	[Profile_Public] [bit] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[Date_Deleted] [datetime] NULL,
	[Verified] [bit] NOT NULL,
	[Date_Verified] [datetime] NULL,
 CONSTRAINT [PK_Entrepreneurs] PRIMARY KEY CLUSTERED 
(
	[Entrepreneur_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Entrepreneurs]  WITH CHECK ADD  CONSTRAINT [FK_Entrepreneurs_Users] FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([User_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Entrepreneurs] CHECK CONSTRAINT [FK_Entrepreneurs_Users]
GO

USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Investors]    Script Date: 3/18/2016 3:49:36 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Investors](
	[Investor_ID] [int] IDENTITY(1,1) NOT NULL,
	[User_ID] [int] NOT NULL,
	[Created_Date] [datetime] NOT NULL,
	[Name] [varchar](255) NULL,
	[Last_Logon] [datetime] NOT NULL,
	[Profile_Public] [bit] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[Date_Deleted] [datetime] NULL,
	[Verified] [bit] NOT NULL,
	[Date_Verified] [datetime] NULL,
 CONSTRAINT [PK_Investors] PRIMARY KEY CLUSTERED 
(
	[Investor_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Investors]  WITH CHECK ADD  CONSTRAINT [FK_Investors_Users] FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([User_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Investors] CHECK CONSTRAINT [FK_Investors_Users]
GO

USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Projects]    Script Date: 3/18/2016 3:54:44 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Projects](
	[Project_ID] [int] IDENTITY(1,1) NOT NULL,
	[Entrepreneur_ID] [int] NOT NULL,
	[Created_Date] [datetime] NOT NULL,
	[Name] [varchar](255) NULL,
	[Description] [varchar](max) NULL,
	[Investment_Goal] [money] NOT NULL,
	[Profile_Public] [bit] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[Date_Deleted] [datetime] NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[Project_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_Entrepreneurs] FOREIGN KEY([Entrepreneur_ID])
REFERENCES [dbo].[Entrepreneurs] ([Entrepreneur_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_Entrepreneurs]
GO

USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Procs]    Script Date: 3/18/2016 3:59:19 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Procs](
	[Proc_ID] [int] IDENTITY(1,1) NOT NULL,
	[Investor_ID] [int] NOT NULL,
	[Project_ID] [int] NOT NULL,
	[Created_Date] [datetime] NOT NULL,
	[Performance_BeginDate] [datetime] NOT NULL,
	[Investment_Ammount] [money] NOT NULL,
	[Revenue_Percentage] [decimal](18, 0) NOT NULL,
	[Active] [bit] NOT NULL,
	[Date_Activated] [datetime] NULL,
	[Expired] [bit] NULL,
	[Project_Accepted] [bit] NOT NULL,
	[Investor_Accepted] [bit] NOT NULL,
	[Mutually_Accepted] [bit] NOT NULL,
	[Date_Mutually_Accepted] [datetime] NULL,
	[Payment_Interval] [int] NULL,
	[Performance_EndDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Procs] PRIMARY KEY CLUSTERED 
(
	[Proc_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Procs]  WITH CHECK ADD  CONSTRAINT [FK_Procs_Investors] FOREIGN KEY([Investor_ID])
REFERENCES [dbo].[Investors] ([Investor_ID])
GO

ALTER TABLE [dbo].[Procs] CHECK CONSTRAINT [FK_Procs_Investors]
GO

ALTER TABLE [dbo].[Procs]  WITH CHECK ADD  CONSTRAINT [FK_Procs_Projects] FOREIGN KEY([Project_ID])
REFERENCES [dbo].[Projects] ([Project_ID])
GO

ALTER TABLE [dbo].[Procs] CHECK CONSTRAINT [FK_Procs_Projects]
GO

USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Images]    Script Date: 3/18/2016 3:59:53 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Images](
	[Image_ID] [int] IDENTITY(1,1) NOT NULL,
	[Binary_Image] [varbinary](max) NOT NULL,
	[Created_DateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[Image_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Images_Users_XREF]    Script Date: 3/18/2016 4:01:49 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Images_Users_XREF](
	[XREF_ID] [int] IDENTITY(1,1) NOT NULL,
	[User_ID] [int] NOT NULL,
	[Image_ID] [int] NOT NULL,
 CONSTRAINT [PK_Images_Users_XREF] PRIMARY KEY CLUSTERED 
(
	[XREF_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Images_Users_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Images_Users_XREF_Images] FOREIGN KEY([Image_ID])
REFERENCES [dbo].[Images] ([Image_ID])
GO

ALTER TABLE [dbo].[Images_Users_XREF] CHECK CONSTRAINT [FK_Images_Users_XREF_Images]
GO

ALTER TABLE [dbo].[Images_Users_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Images_Users_XREF_Users] FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([User_ID])
GO

ALTER TABLE [dbo].[Images_Users_XREF] CHECK CONSTRAINT [FK_Images_Users_XREF_Users]
GO

USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Images_Entrepreneurs_XREF]    Script Date: 3/18/2016 4:02:14 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Images_Entrepreneurs_XREF](
	[XREF_ID] [int] IDENTITY(1,1) NOT NULL,
	[Entrepreneur_ID] [int] NOT NULL,
	[Image_ID] [int] NOT NULL,
 CONSTRAINT [PK_Images_Entrepreneurs_XREF] PRIMARY KEY CLUSTERED 
(
	[XREF_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Images_Entrepreneurs_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Images_Entrepreneurs_XREF_Entrepreneurs] FOREIGN KEY([Entrepreneur_ID])
REFERENCES [dbo].[Entrepreneurs] ([Entrepreneur_ID])
GO

ALTER TABLE [dbo].[Images_Entrepreneurs_XREF] CHECK CONSTRAINT [FK_Images_Entrepreneurs_XREF_Entrepreneurs]
GO

ALTER TABLE [dbo].[Images_Entrepreneurs_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Images_Entrepreneurs_XREF_Images] FOREIGN KEY([Image_ID])
REFERENCES [dbo].[Images] ([Image_ID])
GO

ALTER TABLE [dbo].[Images_Entrepreneurs_XREF] CHECK CONSTRAINT [FK_Images_Entrepreneurs_XREF_Images]
GO

USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Images_Investors_XREF]    Script Date: 3/18/2016 4:02:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Images_Investors_XREF](
	[XREF_ID] [int] IDENTITY(1,1) NOT NULL,
	[Investor_ID] [int] NOT NULL,
	[Image_ID] [int] NOT NULL,
 CONSTRAINT [PK_Images_Investors_XREF] PRIMARY KEY CLUSTERED 
(
	[XREF_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Images_Investors_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Images_Investors_XREF_Images] FOREIGN KEY([Image_ID])
REFERENCES [dbo].[Images] ([Image_ID])
GO

ALTER TABLE [dbo].[Images_Investors_XREF] CHECK CONSTRAINT [FK_Images_Investors_XREF_Images]
GO

ALTER TABLE [dbo].[Images_Investors_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Images_Investors_XREF_Investors] FOREIGN KEY([Investor_ID])
REFERENCES [dbo].[Investors] ([Investor_ID])
GO

ALTER TABLE [dbo].[Images_Investors_XREF] CHECK CONSTRAINT [FK_Images_Investors_XREF_Investors]
GO

USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Images_Projects_XREF]    Script Date: 3/18/2016 4:02:53 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Images_Projects_XREF](
	[XREF_ID] [int] IDENTITY(1,1) NOT NULL,
	[Project_ID] [int] NOT NULL,
	[Image_ID] [int] NOT NULL,
 CONSTRAINT [PK_Images_Projects_XREF] PRIMARY KEY CLUSTERED 
(
	[XREF_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Images_Projects_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Images_Projects_XREF_Images] FOREIGN KEY([Image_ID])
REFERENCES [dbo].[Images] ([Image_ID])
GO

ALTER TABLE [dbo].[Images_Projects_XREF] CHECK CONSTRAINT [FK_Images_Projects_XREF_Images]
GO

ALTER TABLE [dbo].[Images_Projects_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Images_Projects_XREF_Projects] FOREIGN KEY([Project_ID])
REFERENCES [dbo].[Projects] ([Project_ID])
GO

ALTER TABLE [dbo].[Images_Projects_XREF] CHECK CONSTRAINT [FK_Images_Projects_XREF_Projects]
GO

USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Documents]    Script Date: 3/18/2016 4:03:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Documents](
	[Document_ID] [int] IDENTITY(1,1) NOT NULL,
	[Binary_File] [varbinary](max) NOT NULL,
	[Create_DateTime] [datetime] NOT NULL,
	[File_Name] [varchar](255) NOT NULL,
	[Mime_Type] [varchar](255) NOT NULL,
 CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED 
(
	[Document_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Documents_Entrepreneurs_XREF]    Script Date: 3/18/2016 4:03:46 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Documents_Entrepreneurs_XREF](
	[XREF_ID] [int] IDENTITY(1,1) NOT NULL,
	[Entrepreneur_ID] [int] NOT NULL,
	[Document_ID] [int] NOT NULL,
 CONSTRAINT [PK_Documents_Entrepreneurs_XREF] PRIMARY KEY CLUSTERED 
(
	[XREF_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Documents_Entrepreneurs_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Documents_Entrepreneurs_XREF_Documents] FOREIGN KEY([Document_ID])
REFERENCES [dbo].[Documents] ([Document_ID])
GO

ALTER TABLE [dbo].[Documents_Entrepreneurs_XREF] CHECK CONSTRAINT [FK_Documents_Entrepreneurs_XREF_Documents]
GO

ALTER TABLE [dbo].[Documents_Entrepreneurs_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Documents_Entrepreneurs_XREF_Entrepreneurs] FOREIGN KEY([Entrepreneur_ID])
REFERENCES [dbo].[Entrepreneurs] ([Entrepreneur_ID])
GO

ALTER TABLE [dbo].[Documents_Entrepreneurs_XREF] CHECK CONSTRAINT [FK_Documents_Entrepreneurs_XREF_Entrepreneurs]
GO

USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Documents_Investors_XREF]    Script Date: 3/18/2016 4:04:04 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Documents_Investors_XREF](
	[XREF_ID] [int] IDENTITY(1,1) NOT NULL,
	[Investor_ID] [int] NOT NULL,
	[Document_ID] [int] NOT NULL,
 CONSTRAINT [PK_Documents_Investors_XREF] PRIMARY KEY CLUSTERED 
(
	[XREF_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Documents_Investors_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Documents_Investors_XREF_Documents] FOREIGN KEY([Document_ID])
REFERENCES [dbo].[Documents] ([Document_ID])
GO

ALTER TABLE [dbo].[Documents_Investors_XREF] CHECK CONSTRAINT [FK_Documents_Investors_XREF_Documents]
GO

ALTER TABLE [dbo].[Documents_Investors_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Documents_Investors_XREF_Investors] FOREIGN KEY([Investor_ID])
REFERENCES [dbo].[Investors] ([Investor_ID])
GO

ALTER TABLE [dbo].[Documents_Investors_XREF] CHECK CONSTRAINT [FK_Documents_Investors_XREF_Investors]
GO

USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Documents_Projects_XREF]    Script Date: 3/18/2016 4:04:26 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Documents_Projects_XREF](
	[XREF_ID] [int] IDENTITY(1,1) NOT NULL,
	[Project_ID] [int] NOT NULL,
	[Document_ID] [int] NOT NULL,
 CONSTRAINT [PK_Documents_Projects_XREF] PRIMARY KEY CLUSTERED 
(
	[XREF_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Documents_Projects_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Documents_Projects_XREF_Documents] FOREIGN KEY([Document_ID])
REFERENCES [dbo].[Documents] ([Document_ID])
GO

ALTER TABLE [dbo].[Documents_Projects_XREF] CHECK CONSTRAINT [FK_Documents_Projects_XREF_Documents]
GO

ALTER TABLE [dbo].[Documents_Projects_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Documents_Projects_XREF_Projects] FOREIGN KEY([Project_ID])
REFERENCES [dbo].[Projects] ([Project_ID])
GO

ALTER TABLE [dbo].[Documents_Projects_XREF] CHECK CONSTRAINT [FK_Documents_Projects_XREF_Projects]
GO

USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Email_Messages]    Script Date: 3/18/2016 4:05:35 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Email_Messages](
	[Email_Message_ID] [int] IDENTITY(1,1) NOT NULL,
	[Gmail_UID] [int] NULL,
	[Rfc_Message_ID] [varchar](998) NULL,
	[Origin_Address] [varchar](1024) NULL,
	[Destination] [varchar](max) NULL,
	[Inbound] [bit] NULL,
	[Outbound] [bit] NULL,
	[Response] [bit] NULL,
	[Subject] [varchar](max) NULL,
	[Body] [varchar](max) NULL,
	[Sent] [bit] NULL,
	[Sent_DateTime] [datetime] NULL,
	[Processed] [bit] NULL,
	[Processed_DateTime] [datetime] NULL,
	[Reference_ID] [int] NULL,
	[Reference_Type] [int] NULL,
	[Application_Created] [bit] NULL,
	[Appliction_ID] [int] NULL,
	[Error] [bit] NULL,
	[Error_Message] [varchar](max) NULL,
	[Received] [datetime] NULL,
 CONSTRAINT [PK_Email_Messages] PRIMARY KEY CLUSTERED 
(
	[Email_Message_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Email_Messages_Users_XREF]    Script Date: 3/18/2016 4:05:54 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Email_Messages_Users_XREF](
	[XREF_ID] [int] IDENTITY(1,1) NOT NULL,
	[Email_Message_ID] [int] NOT NULL,
	[User_ID] [int] NOT NULL,
 CONSTRAINT [PK_Email_Messages_Users_XREF] PRIMARY KEY CLUSTERED 
(
	[XREF_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Email_Messages_Users_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Email_Messages_Users_XREF_Email_Messages] FOREIGN KEY([Email_Message_ID])
REFERENCES [dbo].[Email_Messages] ([Email_Message_ID])
GO

ALTER TABLE [dbo].[Email_Messages_Users_XREF] CHECK CONSTRAINT [FK_Email_Messages_Users_XREF_Email_Messages]
GO

ALTER TABLE [dbo].[Email_Messages_Users_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Email_Messages_Users_XREF_Users] FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([User_ID])
GO

ALTER TABLE [dbo].[Email_Messages_Users_XREF] CHECK CONSTRAINT [FK_Email_Messages_Users_XREF_Users]
GO

USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Email_Messages_Entrepreneurs_XREF]    Script Date: 3/18/2016 4:06:21 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Email_Messages_Entrepreneurs_XREF](
	[XREF_ID] [int] IDENTITY(1,1) NOT NULL,
	[Email_Message_ID] [int] NOT NULL,
	[Entrepreneur_ID] [int] NOT NULL,
 CONSTRAINT [PK_Email_Messages_Entrepreneurs_XREF] PRIMARY KEY CLUSTERED 
(
	[XREF_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Email_Messages_Entrepreneurs_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Email_Messages_Entrepreneurs_XREF_Email_Messages] FOREIGN KEY([Email_Message_ID])
REFERENCES [dbo].[Email_Messages] ([Email_Message_ID])
GO

ALTER TABLE [dbo].[Email_Messages_Entrepreneurs_XREF] CHECK CONSTRAINT [FK_Email_Messages_Entrepreneurs_XREF_Email_Messages]
GO

ALTER TABLE [dbo].[Email_Messages_Entrepreneurs_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Email_Messages_Entrepreneurs_XREF_Entrepreneurs] FOREIGN KEY([Entrepreneur_ID])
REFERENCES [dbo].[Entrepreneurs] ([Entrepreneur_ID])
GO

ALTER TABLE [dbo].[Email_Messages_Entrepreneurs_XREF] CHECK CONSTRAINT [FK_Email_Messages_Entrepreneurs_XREF_Entrepreneurs]
GO

USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Email_Messages_Investors_XREF]    Script Date: 3/18/2016 4:06:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Email_Messages_Investors_XREF](
	[XREF_ID] [int] IDENTITY(1,1) NOT NULL,
	[Email_Message_ID] [int] NOT NULL,
	[Investor_ID] [int] NOT NULL,
 CONSTRAINT [PK_Email_Messages_Investors_XREF] PRIMARY KEY CLUSTERED 
(
	[XREF_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Email_Messages_Investors_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Email_Messages_Investors_XREF_Email_Messages] FOREIGN KEY([Email_Message_ID])
REFERENCES [dbo].[Email_Messages] ([Email_Message_ID])
GO

ALTER TABLE [dbo].[Email_Messages_Investors_XREF] CHECK CONSTRAINT [FK_Email_Messages_Investors_XREF_Email_Messages]
GO

ALTER TABLE [dbo].[Email_Messages_Investors_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Email_Messages_Projects_XREF_Investors] FOREIGN KEY([Investor_ID])
REFERENCES [dbo].[Investors] ([Investor_ID])
GO

ALTER TABLE [dbo].[Email_Messages_Investors_XREF] CHECK CONSTRAINT [FK_Email_Messages_Projects_XREF_Investors]
GO

USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Email_Messages_Projects_XREF]    Script Date: 3/18/2016 4:07:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Email_Messages_Projects_XREF](
	[XREF_ID] [int] IDENTITY(1,1) NOT NULL,
	[Email_Message_ID] [int] NOT NULL,
	[Project_ID] [int] NOT NULL,
 CONSTRAINT [PK_Email_Messages_Projects_XREF] PRIMARY KEY CLUSTERED 
(
	[XREF_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Email_Messages_Projects_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Email_Messages_Projects_XREF_Email_Messages] FOREIGN KEY([Email_Message_ID])
REFERENCES [dbo].[Email_Messages] ([Email_Message_ID])
GO

ALTER TABLE [dbo].[Email_Messages_Projects_XREF] CHECK CONSTRAINT [FK_Email_Messages_Projects_XREF_Email_Messages]
GO

ALTER TABLE [dbo].[Email_Messages_Projects_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Email_Messages_Projects_XREF_Projects] FOREIGN KEY([Project_ID])
REFERENCES [dbo].[Projects] ([Project_ID])
GO

ALTER TABLE [dbo].[Email_Messages_Projects_XREF] CHECK CONSTRAINT [FK_Email_Messages_Projects_XREF_Projects]
GO

USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Email_Messages_Procs_XREF]    Script Date: 3/18/2016 4:07:39 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Email_Messages_Procs_XREF](
	[XREF_ID] [int] IDENTITY(1,1) NOT NULL,
	[Email_Message_ID] [int] NOT NULL,
	[Proc_ID] [int] NOT NULL,
 CONSTRAINT [PK_Email_Messages_Procs_XREF] PRIMARY KEY CLUSTERED 
(
	[XREF_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Email_Messages_Procs_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Email_Messages_Procs_XREF_Email_Messages] FOREIGN KEY([Email_Message_ID])
REFERENCES [dbo].[Email_Messages] ([Email_Message_ID])
GO

ALTER TABLE [dbo].[Email_Messages_Procs_XREF] CHECK CONSTRAINT [FK_Email_Messages_Procs_XREF_Email_Messages]
GO

ALTER TABLE [dbo].[Email_Messages_Procs_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Email_Messages_Procs_XREF_Procs] FOREIGN KEY([Proc_ID])
REFERENCES [dbo].[Procs] ([Proc_ID])
GO

ALTER TABLE [dbo].[Email_Messages_Procs_XREF] CHECK CONSTRAINT [FK_Email_Messages_Procs_XREF_Procs]
GO

USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Profile_Messages]    Script Date: 3/18/2016 4:08:31 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Profile_Messages](
	[Profile_Message_ID] [int] IDENTITY(1,1) NOT NULL,
	[Message_Text] [varchar](5000) NOT NULL,
	[Messenger_ID] [int] NOT NULL,
	[Messenger_Type] [int] NOT NULL,
	[DateTime_Created] [datetime] NOT NULL,
	[Viewed] [bit] NULL,
 CONSTRAINT [PK_Profile_Messages] PRIMARY KEY CLUSTERED 
(
	[Profile_Message_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Profile_Messages_Entrepreneurs_XREF]    Script Date: 3/18/2016 4:08:45 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Profile_Messages_Entrepreneurs_XREF](
	[XREF_ID] [int] IDENTITY(1,1) NOT NULL,
	[Entrepreneur_ID] [int] NOT NULL,
	[Profile_Message_ID] [int] NOT NULL,
 CONSTRAINT [PK_Profile_Messages_Entrepreneurs_XREF] PRIMARY KEY CLUSTERED 
(
	[XREF_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Profile_Messages_Investors_XREF]    Script Date: 3/18/2016 4:08:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Profile_Messages_Investors_XREF](
	[XREF_ID] [int] IDENTITY(1,1) NOT NULL,
	[Investor_ID] [int] NOT NULL,
	[Profile_Message_ID] [int] NOT NULL,
 CONSTRAINT [PK_Profile_Messages_Investors_XREF] PRIMARY KEY CLUSTERED 
(
	[XREF_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Profile_Messages_Projects_XREF]    Script Date: 3/18/2016 4:09:15 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Profile_Messages_Projects_XREF](
	[XREF_ID] [int] IDENTITY(1,1) NOT NULL,
	[Project_ID] [int] NOT NULL,
	[Profile_Message_ID] [int] NOT NULL,
 CONSTRAINT [PK_Profile_Messages_Projects_XREF] PRIMARY KEY CLUSTERED 
(
	[XREF_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- This will need to be run after the ASP.NET Identity tables are updated.

USE [ProudSourceDB]
GO

/****** Object:  Table [dbo].[Users_AspNetUsers_XREF]    Script Date: 3/18/2016 4:10:56 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users_AspNetUsers_XREF](
	[XREF_ID] [int] IDENTITY(1,1) NOT NULL,
	[AspNetUser_ID] [nvarchar](128) NOT NULL,
	[User_ID] [int] NOT NULL,
 CONSTRAINT [PK_Users_AspNetUsers_XREF] PRIMARY KEY CLUSTERED 
(
	[XREF_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Users_AspNetUsers_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Users_AspNetUsers_XREF_AspNetUsers] FOREIGN KEY([AspNetUser_ID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[Users_AspNetUsers_XREF] CHECK CONSTRAINT [FK_Users_AspNetUsers_XREF_AspNetUsers]
GO

ALTER TABLE [dbo].[Users_AspNetUsers_XREF]  WITH CHECK ADD  CONSTRAINT [FK_Users_AspNetUsers_XREF_Users] FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([User_ID])
GO

ALTER TABLE [dbo].[Users_AspNetUsers_XREF] CHECK CONSTRAINT [FK_Users_AspNetUsers_XREF_Users]
GO

CREATE PROCEDURE auth_User_ID_from_Identity_ID 
	-- Add the parameters for the stored procedure here
	@userIdentity NVARCHAR(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT UANU_XREF.[User_ID]
	FROM Users_AspNetUsers_XREF UANU_XREF
	WHERE UANU_XREF.[AspNetUser_ID] = @userIdentity

END
GO

CREATE PROCEDURE Entrepreneur_Create
	-- Add the parameters for the stored procedure here
	@User_ID INT, 
	@Entrepreneur_Name VARCHAR(255)
AS
BEGIN
    -- Insert statements for procedure here
	BEGIN TRY
		BEGIN TRAN Create_Entrepreneur_Entry
			DECLARE @New_Entrepreneur_ID INT
			INSERT INTO Entrepreneurs 
			(
				[User_ID], 
				[Created_Date], 
				[Name],
				[Last_Logon], 
				[Profile_Public], 
				[Deleted],
				[Verified]
			)
			(
				SELECT 
				@User_ID,
				GETDATE(),
				@Entrepreneur_Name,
				GETDATE(),
				'TRUE',
				0,
				0
			);
			SET @New_Entrepreneur_ID = SCOPE_IDENTITY();

			---- Create a financial account for this Entrepreneur profile
			--DECLARE @date DATETIME = GETDATE()
			--EXECUTE ProudSourceAccounting.dbo.sp_Create_Financial_Account @New_Entrepreneur_ID, 4

		COMMIT TRAN Create_Entrepreneur_Entry
		SELECT @New_Entrepreneur_ID;
		RETURN
	END TRY
	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('Entrepreneur_Create, ', ERROR_MESSAGE());

		RAISERROR (@err_msg, 16, 1);

		RETURN

	END CATCH
END
GO

CREATE PROCEDURE Investor_Create 
	-- Add the parameters for the stored procedure here
	@User_ID INT,
	@Investor_Name VARCHAR(255)
AS
BEGIN
    -- Insert statements for procedure here
	BEGIN TRY
		BEGIN TRAN Create_Investor_Entry
			DECLARE @New_Investor_ID INT;
			INSERT INTO Investors
			(
				[User_ID],
				[Created_Date],
				[Name],
				[Last_Logon],
				[Profile_Public],
				[Deleted],
				[Verified]
			)
			VALUES
			(
				@User_ID,
				GETDATE(),
				@Investor_Name,
				GETDATE(),
				'TRUE',
				'FALSE',
				'FALSE'
			);
			SET @New_Investor_ID = SCOPE_IDENTITY();

			DECLARE @date DATETIME = GETDATE()

			EXECUTE ProudSourceAccounting.dbo.sp_Create_Financial_Account @New_Investor_ID, 3

		COMMIT TRAN Create_Investor_Entry
		SELECT @New_Investor_ID;
		RETURN
	END TRY
	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('Investor_Create, ', ERROR_MESSAGE());

		RAISERROR (@err_msg, 16, 1);

		RETURN

	END CATCH
END
GO

CREATE PROCEDURE [dbo].[Investor_Update] 
	-- Add the parameters for the stored procedure here
	@Investor_ID INT,
	@Name VARCHAR(255),  
	@profile_public BIT,
	@profile_picture VARBINARY(MAX)
AS
BEGIN
    -- Insert statements for procedure here
	BEGIN TRY

		BEGIN TRAN Update_Investor_Entry

			-- Declare variables for potential use later
			DECLARE @new_image_id INT;
			DECLARE @image_id_pointer INT;

			-- Using COALESCE we can update with new values when they exist or keep the value already contained if the accepted parameter is null.
			UPDATE Investors
			SET [Name] = COALESCE(@Name, [Name]),
				[Profile_Public] = COALESCE(@profile_public, [Profile_Public])
			WHERE [Investor_ID] = @Investor_ID

			-- If the profile picture paramter is not empty then find if this account has pictures or not.
			IF @profile_picture IS NOT NULL
			BEGIN

				-- If this account does not have any images associated with it.
				IF (SELECT COUNT(*) FROM Images_Investors_XREF II WHERE II.[Investor_ID] = @Investor_ID) < 1
				BEGIN

					INSERT INTO Images
					(
						[Binary_Image],
						[Created_DateTime]
					)
					(
						SELECT
						@profile_picture,
						GETDATE()
					);

					-- This allows us to get the ID of a record that was just created in this session.
					SET @new_image_id = SCOPE_IDENTITY();

					INSERT INTO Images_Investors_XREF
					(
						[Investor_ID],
						[Image_ID]
					)
					(
						SELECT
						@Investor_ID,
						@new_image_id
					);

				END

				-- If this account does have imagges associated with it update the record that alread exists
				IF (SELECT COUNT(*) FROM Images_Investors_XREF II WHERE II.[Investor_ID] = @Investor_ID) > 0
				BEGIN

					SET @image_id_pointer = (SELECT TOP 1 [Image_ID] FROM Images_Investors_XREF WHERE [Investor_ID] = @Investor_ID ORDER BY [XREF_ID] ASC)
					UPDATE Images
					SET Images.[Binary_Image] = @profile_picture,
						Images.[Created_DateTime] = GETDATE()
					WHERE Images.[Image_ID] = @image_id_pointer

				END

			END

		COMMIT TRAN Update_Investor_Entry
		RETURN 0;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		RAISERROR (15004, -1, -1, 'Investor_Update');

	END CATCH

END
GO

CREATE PROCEDURE [dbo].[Proc_Create] 
	-- Add the parameters for the stored procedure here
	@Investor_ID INT,
	@Project_ID INT,
	@Performance_BeginDate DATETIME,
	@Performance_EndDate DATETIME,
	@Investment_Ammount MONEY,
	@Revenue_Percentage DECIMAL(18,0)
AS
BEGIN
    -- Insert statements for procedure here
	BEGIN TRY
		BEGIN TRAN Create_Proc_Entry
			DECLARE @New_Proc_ID INT;
			INSERT INTO Procs 
			(
				[Investor_ID],
				[Project_ID],
				[Created_Date],
				[Performance_BeginDate],
				[Performance_EndDate],
				[Investment_Ammount],
				[Revenue_Percentage],
				[Payment_Interval],
				[Project_Accepted],
				[Investor_Accepted],
				[Mutually_Accepted],
				[Active]
			)
			(
				SELECT
				@Investor_ID,
				@Project_ID,
				GETDATE(),
				@Performance_BeginDate,
				@Performance_EndDate,
				@Investment_Ammount,
				@Revenue_Percentage,
				1,
				'FALSE',
				'FALSE',
				'FALSE',
				'FALSE'
			);
			SET @New_Proc_ID = SCOPE_IDENTITY();
		COMMIT TRAN Create_Proc_Entry
		SELECT @New_Proc_ID;
		RETURN 0
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('PROC_Create, ', ERROR_MESSAGE());

		RAISERROR (@err_msg, 16, 1);

		RETURN
	END CATCH
END
GO

CREATE PROCEDURE [dbo].[Project_Create] 
	-- Add the parameters for the stored procedure here
	@Entrepreneur_ID INT, 
	@Project_Name VARCHAR(MAX),
	@Project_Description VARCHAR(MAX),
	@Investment_Goal MONEY
AS
BEGIN
    -- Insert statements for procedure here
	BEGIN TRY
		BEGIN TRAN Create_Project_Entry
			DECLARE @New_Project_ID INT;
			INSERT INTO Projects
			(
				[Entrepreneur_ID],
				[Created_date],
				[Name],
				[Description],
				[Investment_Goal],
				[Profile_Public],
				[Deleted]
			)
			(
				SELECT
				@Entrepreneur_ID,
				GETDATE(),
				@Project_Name,
				@Project_Description,
				@Investment_Goal,
				'FALSE',
				'FALSE'
			);
			SET @New_Project_ID = SCOPE_IDENTITY()

			SELECT @New_Project_ID

			EXECUTE ProudSourceAccounting.dbo.sp_Create_Financial_Account @New_Project_ID, 5

		COMMIT TRAN Create_Project_Entry

		RETURN

	END TRY
	BEGIN CATCH
		ROLLBACK TRAN

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = ERROR_MESSAGE();

		RAISERROR (@err_msg, 16, 1);

		RETURN

	END CATCH
END
GO

CREATE PROCEDURE sp_Delete_Project_Image 
	-- Add the parameters for the stored procedure here
	@Project_ID INT,
	@Image_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY

		BEGIN TRAN Delete_Project_Image

			DECLARE @xref_id INT;

			SET @xref_id = (SELECT [XREF_ID] FROM Images_Projects_XREF WHERE [Project_ID] = @Project_ID AND [Image_ID] = @Image_ID)

			DELETE FROM Images_Projects_XREF WHERE [XREF_ID] = @xref_id

			DELETE FROM Images WHERE [Image_ID] = @Image_ID

		COMMIT TRAN Delete_Project_Image

	END TRY
	BEGIN CATCH

		ROLLBACK TRAN

		RAISERROR (15012, -1, -1, 'sp_Delete_Project_Image');

	END CATCH
END
GO

CREATE PROCEDURE sp_DeleteDocument_Project 
	-- Add the parameters for the stored procedure here
	@Project_ID INT,
	@Document_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY

		BEGIN TRAN Delete_Project_Document

			DECLARE @xref_id INT

			SET @xref_id = (SELECT [XREF_ID] FROM Documents_Projects_XREF WHERE [Project_ID] = @Project_ID AND [Document_ID] = @Document_ID)

			DELETE FROM Documents_Projects_XREF WHERE [XREF_ID] = @xref_id

			DELETE FROM Documents WHERE [Document_ID] = @Document_ID

		COMMIT TRAN Delete_ProjectDocument

	END TRY
	BEGIN CATCH

		ROLLBACK TRAN

		RAISERROR (15012, -1, -1, 'sp_DeleteDocument_Project');

	END CATCH

END
GO

CREATE PROCEDURE sp_fail_Email_Message 
	-- Add the parameters for the stored procedure here
	@Email_Message_ID INT,
	@Failure_Message VARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Email_Messages
	SET [Error] = 'TRUE',
		[Error_Message] = @Failure_Message,
		[Processed_DateTime] = GETDATE()
	WHERE [Email_Message_ID] = @Email_Message_ID 
END
GO

CREATE PROCEDURE sp_get_Entrepreneur_Messages 
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT E.[Email_Message_ID], E.[Destination], E.[Origin_Address], E.[Subject], E.[Body], E.[Reference_ID], E.[Reference_Type]
	FROM Email_Messages E
	WHERE E.[Reference_Type] = 4 
		AND E.[Error] IS NULL
		AND E.[Error_Message] IS NULL
		AND E.[Outbound] = 'TRUE'
		AND E.[Inbound] <> 'TRUE'
		AND E.[Sent] IS NULL OR E.[Sent] = 'FALSE'
END
GO

CREATE PROCEDURE sp_get_EntrepreneurDetails 
	-- Add the parameters for the stored procedure here
	@Entrepreneur_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Entrepreneur_ID], [Name], [Profile_Public]
	FROM Entrepreneurs
	WHERE [Entrepreneur_ID] = @Entrepreneur_ID

	SELECT I.[Image_ID], I.[Binary_Image]
	FROM Images I
	JOIN Images_Entrepreneurs_XREF IE_XREF ON I.[Image_ID] = IE_XREF.[Image_ID]
	WHERE IE_XREF.[Entrepreneur_ID] = @Entrepreneur_ID
END
GO

CREATE PROCEDURE [dbo].[sp_get_EntrepreneurDetails_with_Projects] 
	-- Add the parameters for the stored procedure here
	@Entrepreneur_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT E.[Entrepreneur_ID], E.[Name], E.[Profile_Public], E.[Verified]
	FROM Entrepreneurs E
	WHERE E.[Entrepreneur_ID] = @Entrepreneur_ID

	SELECT I.[Image_ID], I.[Binary_Image]
	FROM Images I
	JOIN Images_Entrepreneurs_XREF IE_XREF ON I.[Image_ID] = IE_XREF.[Image_ID]
	WHERE IE_XREF.[Entrepreneur_ID] = @Entrepreneur_ID

	SELECT P.[Project_ID], P.[Name], P.[Profile_Public], P.[Investment_Goal]
	FROM Projects P
	WHERE P.[Entrepreneur_ID] = @Entrepreneur_ID

	SELECT P.[Project_ID], P.[Investment_Goal], SUM(T.[Amount]) AS 'Project_Balance'
	FROM Projects P
	FULL OUTER JOIN ProudSourceAccounting.dbo.Accounts A ON P.[Project_ID] = A.[Profile_Account_ID] AND A.[Profile_Type_ID] = 5
	JOIN ProudSourceAccounting.dbo.Transactions T ON A.[Account_ID] = T.[Account_ID]
	WHERE P.[Entrepreneur_ID] = @Entrepreneur_ID
	AND T.[Transaction_State] = 'PROCESSED'
	GROUP BY P.[Project_ID], P.[Investment_Goal]
END
GO

CREATE PROCEDURE sp_get_InvestorDetails 
	-- Add the parameters for the stored procedure here
	@Investor_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT I.[Investor_ID], I.[Name], I.[Profile_Public]
	FROM Investors I
	WHERE I.[Investor_ID] = @Investor_ID

	SELECT I.[Image_ID], I.[Binary_Image]
	FROM Images I
	JOIN Images_Investors_XREF II_XREF ON I.[Image_ID] = II_XREF.[Image_ID]
	WHERE II_XREF.[Investor_ID] = @Investor_ID;
END
GO

CREATE PROCEDURE [dbo].[sp_get_InvestorDetails_with_PROCs] 
	-- Add the parameters for the stored procedure here
	@Investor_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	-- Get investor account details
	SELECT I.[Name], I.[Profile_Public], I.[Verified]
	FROM Investors I
	WHERE I.[Investor_ID] = @Investor_ID

	-- Get image for investor profile.
	SELECT TOP 1 I.[Image_ID], I.[Binary_Image]
	FROM Images I
	JOIN Images_Investors_XREF II_XREF ON I.[Image_ID] = II_XREF.[Image_ID]
	WHERE II_XREF.[Investor_ID] = @Investor_ID

	-- Get a collection of PROC results related to this investor.
	SELECT P.[Proc_ID],
		P.[Investor_ID],
		P.[Project_ID],
		P.[Investment_Ammount],
		P.[Revenue_Percentage],
		P.[Active],
		Proj.[Name] AS 'Project_Name',
		E.[Name] AS 'Entrepreneur_Name'
	FROM Procs P
	JOIN Projects Proj ON P.[Project_ID] = Proj.[Project_ID]
	JOIN Entrepreneurs E ON Proj.[Entrepreneur_ID] = E.[Entrepreneur_ID]
	WHERE P.[Investor_ID] = @Investor_ID

	-- Get the account balance for the investor in USD
	SELECT SUM(T.[Amount]) AS 'USD_Balance'
	FROM ProudSourceAccounting.dbo.Accounts A
	JOIN ProudSourceAccounting.dbo.Transactions T ON A.[Account_ID] = T.[Account_ID]
	WHERE A.[Profile_Account_ID] = @Investor_ID
	AND A.[Profile_Type_ID] = 3
	AND T.[Transaction_State] = 'PROCESSED'
	AND T.[Currency_Type_ID] = 2

	-- Get the account balance for the investor in BTC
	SELECT SUM(T.[Amount]) AS 'BTC_Balance'
	FROM ProudSourceAccounting.dbo.Accounts A
	JOIN ProudSourceAccounting.dbo.Transactions T ON A.[Account_ID] = T.[Account_ID]
	WHERE A.[Profile_Account_ID] = @Investor_ID
	AND A.[Profile_Type_ID] = 3
	AND T.[Transaction_State] = 'PROCESSED'
	AND T.[Currency_Type_ID] = 1

	-- Get the pending transactionss for the financial account tied to this profile.
	SELECT T.[Description], T.[Amount], CT.[Currency]
	FROM ProudSourceAccounting.dbo.Accounts A
	JOIN ProudSourceAccounting.dbo.Transactions T ON A.[Account_ID] = T.[Account_ID]
	JOIN ProudSourceAccounting.dbo.Currency_Types CT ON T.[Currency_Type_ID] = CT.[Currency_Type_ID]
	WHERE A.[Profile_Account_ID] = @Investor_ID
	AND A.[Profile_Type_ID] = 3
	AND T.[Transaction_State] = 'PENDING'

	-- Get the Financial account id.
	SELECT A.[Account_ID]
	FROM ProudSourceAccounting.dbo.Accounts A
	WHERE A.[Profile_Account_ID] = @Investor_ID
	AND A.[Profile_Type_ID] = 3
END
GO

CREATE PROCEDURE sp_get_Investor_Messages 
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT E.[Email_Message_ID], E.[Destination], E.[Origin_Address], E.[Subject], E.[Body], E.[Reference_ID], E.[Reference_Type]
	FROM Email_Messages E
	WHERE E.[Reference_Type] = 3 
		AND E.[Error] IS NULL
		AND E.[Error_Message] IS NULL
		AND E.[Outbound] = 'TRUE'
		AND E.[Inbound] <> 'TRUE'
		AND E.[Sent] IS NULL OR E.[Sent] = 'FALSE'
END
GO

CREATE PROCEDURE sp_get_PROC_Messages 
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT E.[Email_Message_ID], E.[Destination], E.[Origin_Address], E.[Subject], E.[Body], E.[Reference_ID], E.[Reference_Type]
	FROM Email_Messages E
	WHERE E.[Reference_Type] = 6 
		AND E.[Error] IS NULL
		AND E.[Error_Message] IS NULL
		AND E.[Outbound] = 'TRUE'
		AND E.[Inbound] <> 'TRUE'
		AND E.[Sent] IS NULL OR E.[Sent] = 'FALSE'
END
GO

CREATE PROCEDURE sp_get_Project_Images 
	-- Add the parameters for the stored procedure here
	@Project_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT P.[Project_ID], I.[Image_ID], I.[Binary_Image] 
	FROM Projects P
	JOIN Images_Projects_XREF IP_XREF ON P.[Project_ID] = IP_XREF.[Project_ID]
	JOIN Images I ON IP_XREF.[Image_ID] = I.[Image_ID]
END
GO

CREATE PROCEDURE sp_get_Project_Messages 
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT E.[Email_Message_ID], E.[Destination], E.[Origin_Address], E.[Subject], E.[Body], E.[Reference_ID], E.[Reference_Type]
	FROM Email_Messages E
	WHERE E.[Reference_Type] = 5 
		AND E.[Error] IS NULL
		AND E.[Error_Message] IS NULL
		AND E.[Outbound] = 'TRUE'
		AND E.[Inbound] <> 'TRUE'
		AND E.[Sent] IS NULL OR E.[Sent] = 'FALSE'
END
GO

CREATE PROCEDURE [dbo].[sp_get_ProjectDetails] 
	-- Add the parameters for the stored procedure here
	@Project_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Name], [Description], [Investment_Goal], [Profile_Public]
	FROM Projects
	WHERE [Project_ID] = @Project_ID

	SELECT I.[Image_ID], I.[Binary_Image]
	FROM Images I
	JOIN Images_Projects_XREF IP_XREF ON I.[Image_ID] = IP_XREF.[Image_ID]
	WHERE IP_XREF.[Project_ID] = @Project_ID

	SELECT D.[Document_ID], D.[File_Name]
	FROM Documents D
	JOIN Documents_Projects_XREF DP_XREF ON D.[Document_ID] = DP_XREF.[Document_ID]
	WHERE DP_XREF.[Project_ID] = @Project_ID

	SELECT [Proc_ID], I.[Name] ,Procs.[Created_Date], [Performance_BeginDate], [Performance_EndDate], [Revenue_Percentage]
	FROM Procs 
	JOIN Investors I ON Procs.[Investor_ID] = I.[Investor_ID]
	WHERE [Project_ID] = @Project_ID

	SELECT SUM(T.[Amount]) AS 'USD_Balance'
	FROM ProudSourceAccounting.dbo.Accounts A
	JOIN ProudSourceAccounting.dbo.Transactions T ON A.[Account_ID] = T.[Account_ID]
	WHERE A.[Profile_Account_ID] = @Project_ID
	AND A.[Profile_Type_ID] = 5
	AND T.[Transaction_State] = 'PROCESSED'
	AND T.[Currency_Type_ID] = 2

	SELECT SUM(T.[Amount]) AS 'BTC_Balance'
	FROM ProudSourceAccounting.dbo.Accounts A
	JOIN ProudSourceAccounting.dbo.Transactions T ON A.[Account_ID] = T.[Account_ID]
	WHERE A.[Profile_Account_ID] = @Project_ID
	AND A.[Profile_Type_ID] = 5
	AND T.[Transaction_State] = 'PROCESSED'
	AND T.[Currency_Type_ID] = 1

	SELECT A.[Account_ID]
	FROM ProudSourceAccounting.dbo.Accounts A
	WHERE A.[Profile_Account_ID] = @Project_ID
	AND A.[Profile_Type_ID] = 5

	SELECT IE_XREF.[Entrepreneur_ID], I.[Binary_Image]
	FROM Images I
	JOIN Images_Entrepreneurs_XREF IE_XREF ON I.[Image_ID] = IE_XREF.[Image_ID]
	JOIN Projects P ON IE_XREF.[Entrepreneur_ID] = P.[Entrepreneur_ID]
	WHERE P.[Project_ID] = @Project_ID
END
GO

CREATE PROCEDURE sp_get_Trending_Projects 
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @BeginDate DATETIME
	SET @BeginDate = DATEADD(DAY, -21, GETDATE())
	DECLARE @EndDate DATETIME
	SET @EndDate = GETDATE()

	-- Get a set of data that is the projects and entrepreneur datum to display
	--SELECT P.[Project_ID], P.[Name], P.[Description], P.[Investment_Goal], E.[Entrepreneur_ID], E.[Name] AS 'Entrepreneur_Name' 
	--FROM Projects P
	--JOIN Entrepreneurs E ON P.[Entrepreneur_ID] = E.[Entrepreneur_ID]
	--WHERE P.[Created_Date] > @BeginDate
	--AND P.[Created_Date] < @EndDate
	--AND P.[Profile_Public] = 'TRUE'
	--AND E.[Profile_Public] = 'TRUE'

	SELECT TOP 9 P.[Project_ID], P.[Name], P.[Description], P.[Investment_Goal], E.[Entrepreneur_ID], E.[Name] AS 'Entrepreneur_Name' 
	FROM Projects P
	JOIN Entrepreneurs E ON P.[Entrepreneur_ID] = E.[Entrepreneur_ID]

END
GO

CREATE PROCEDURE sp_get_User_Messages 
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT E.[Email_Message_ID], E.[Destination], E.[Origin_Address], E.[Subject], E.[Body], E.[Reference_ID], E.[Reference_Type]
	FROM Email_Messages E
	WHERE E.[Reference_Type] = 1 
		AND E.[Error] IS NULL
		AND E.[Error_Message] IS NULL
		AND E.[Outbound] = 'TRUE'
		AND E.[Inbound] <> 'TRUE'
		AND E.[Sent] IS NULL OR E.[Sent] = 'FALSE'
END
GO

CREATE PROCEDURE sp_get_UserDetails_with_Accounts
	-- Add the parameters for the stored procedure here
	@User_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT U.[User_ID], U.[Email], U.[Email_Verified]
	FROM Users U
	WHERE U.[User_ID] = @User_ID

	SELECT I.[Image_ID], I.[Binary_Image]
	FROM Images I
	JOIN Images_Users_XREF IU_XREF ON I.[Image_ID] = IU_XREF.[Image_ID]
	WHERE IU_XREF.[User_ID] = @User_ID

	-- get entrepreneur stuff

	DECLARE @Entrepreneur_ID INT = (
		SELECT TOP 1 E.[Entrepreneur_ID]
		FROM Entrepreneurs E
		WHERE E.[User_ID] = @User_ID
	)

	SELECT E.[Entrepreneur_ID], E.[Name], E.[Profile_Public], E.[Verified]
	FROM Entrepreneurs E
	WHERE E.[Entrepreneur_ID] = @Entrepreneur_ID

	SELECT I.[Image_ID], I.[Binary_Image]
	FROM Images I
	JOIN Images_Entrepreneurs_XREF IE_XREF ON I.[Image_ID] = IE_XREF.[Image_ID]
	WHERE IE_XREF.[Entrepreneur_ID] = @Entrepreneur_ID

	SELECT P.[Project_ID], P.[Investment_Goal], SUM(T.[Amount]) AS 'Project_Balance'
	FROM Projects P
	JOIN ProudSourceAccounting.dbo.Accounts A ON P.[Project_ID] = A.[Profile_Account_ID] AND A.[Profile_Type_ID] = 5
	RIGHT OUTER JOIN ProudSourceAccounting.dbo.Transactions T ON A.[Account_ID] = T.[Account_ID]
	WHERE P.[Entrepreneur_ID] = @Entrepreneur_ID
	AND T.[Transaction_State] = 'PROCESSED'
	GROUP BY P.[Project_ID], p.[Investment_Goal]

	-- get investor stuff

	DECLARE @Investor_ID INT = (
		SELECT TOP 1 I.[Investor_ID]
		FROM Investors I
		WHERE I.[User_ID] = @User_ID
	)

	SELECT I.[Investor_ID], I.[Name], I.[Profile_Public], I.[Verified]
	FROM Investors I
	WHERE I.[Investor_ID] = @Investor_ID

	SELECT I.[Investor_ID], SUM(T.[Amount]) AS 'Investor_Balance'
	FROM Investors I
	JOIN ProudSourceAccounting.dbo.Accounts A ON I.[Investor_ID] = A.[Profile_Account_ID] 
	RIGHT OUTER JOIN ProudSourceAccounting.dbo.Transactions T ON A.[Account_ID] = T.[Account_ID]
	WHERE I.[Investor_ID] = @Investor_ID
	AND T.[Transaction_State] = 'PROCESSED'
	GROUP BY I.[Investor_ID], I.[Name], I.[Profile_Public], I.[Verified]

	SELECT T.[Transaction_ID], T.[Description], T.[Amount]
	FROM Investors I
	JOIN ProudSourceAccounting.dbo.Accounts A ON I.[Investor_ID] = A.[Profile_Account_ID] 
	RIGHT OUTER JOIN ProudSourceAccounting.dbo.Transactions T ON A.[Account_ID] = T.[Account_ID]
	WHERE I.[Investor_ID] = @Investor_ID
	AND T.[Transaction_State] = 'PENDING'

	SELECT I.[Image_ID], I.[Binary_Image]
	FROM Images I
	JOIN Images_Investors_XREF II_XREF ON I.[Image_ID] = II_XREF.[Image_ID]
	WHERE II_XREF.[Investor_ID] = @Investor_ID

	SELECT P.[Proc_ID], P.[Investment_Ammount], P.[Active]
	FROM Procs P
	WHERE P.[Investor_ID] = @Investor_ID

END
GO

CREATE PROCEDURE sp_get_UserEditDetails 
	-- Add the parameters for the stored procedure here
	@User_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Email]
	FROM Users 
	WHERE [User_ID] = @User_ID

	SELECT I.[Image_ID], I.[Binary_Image]
	FROM Images I
	JOIN Images_Users_XREF IU_XREF ON I.[Image_ID] = IU_XREF.[Image_ID]
	WHERE IU_XREF.[User_ID] = @User_ID
END
GO

CREATE PROCEDURE sp_GetPROC_Details 
	-- Add the parameters for the stored procedure here
	@PROC_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @Investor_ID INT;

	DECLARE @Entrepreneur_ID INT;

	SET @Investor_ID = (SELECT [Investor_ID] FROM Procs WHERE [Proc_ID] = @Proc_ID)

	SET @Entrepreneur_ID = (SELECT [Entrepreneur_ID] FROM Procs PR JOIN Projects P ON PR.[Project_ID] = P.[Project_ID]  WHERE [Proc_ID] = @Proc_ID)

	SELECT PR.[Investor_ID], 
		PR.[Project_ID], 
		E.[Entrepreneur_ID],
		PR.[Investment_Ammount], 
		PR.[Revenue_Percentage], 
		PR.[Active], 
		PR.[Date_Activated], 
		PR.[Project_Accepted], 
		PR.[Investor_Accepted], 
		PR.[Mutually_Accepted], 
		PR.[Date_Mutually_Accepted], 
		PR.[Performance_BeginDate], 
		PR.[Performance_EndDate],
		P.[Name] AS 'Project Name', 
		I.[Name] AS 'Investor Name', 
		E.[Name] AS 'Entrepreneur Name'
	FROM Procs PR
	JOIN Projects P ON PR.[Project_ID] = P.[Project_ID]
	JOIN Investors I ON PR.[Investor_ID] = I.[Investor_ID]
	JOIN Entrepreneurs E ON P.[Entrepreneur_ID] = E.[Entrepreneur_ID]
	WHERE PR.[Proc_ID] = @PROC_ID

	SELECT TOP 1 I.[Binary_Image]
	FROM Images_Investors_XREF II_XREF
	JOIN Images I ON II_XREF.[Image_ID] = I.[Image_ID]
	WHERE II_XREF.[Investor_ID] = @Investor_ID
	ORDER BY I.[Image_ID]

	SELECT TOP 1 I.[Binary_Image]
	FROM Images_Entrepreneurs_XREF IU_XREF
	JOIN Images I ON IU_XREF.[Image_ID] = I.[Image_ID]
	WHERE IU_XREF.[Entrepreneur_ID] = @Entrepreneur_ID
	ORDER BY I.[Image_ID]
	
	SELECT SUM(T.[Amount]) AS 'Balance'
	FROM ProudSourceAccounting.dbo.Transactions T
	JOIN ProudSourceAccounting.dbo.Accounts A ON T.[Account_ID] = A.[Account_ID]
	WHERE A.[Profile_Account_ID] = @Investor_ID
		AND A.[Profile_Type_ID] = 3
		AND T.[Transaction_State] = 'PROCESSED'

END
GO

CREATE PROCEDURE sp_insert_Email_Message 
	-- Add the parameters for the stored procedure here
	@Gmail_UID INT,
	@Rfc_Message_ID VARCHAR(998),
	@Origin VARCHAR(1024),
	@Destination VARCHAR(MAX),
	@Subject VARCHAR(MAX),
	@Body VARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @date_now DATETIME
	SET @date_now = GETDATE();

	INSERT INTO Email_Messages
	(
		[Gmail_UID],
		[Rfc_Message_ID],
		[Origin_Address],
		[Destination],
		[Inbound],
		[Subject],
		[Body],
		[Received]
	)
	(
		SELECT
		@Gmail_UID,
		@Rfc_Message_ID,
		@Origin,
		@Destination,
		'TRUE',
		@Subject,
		@Body,
		@date_now
	);
	SELECT SCOPE_IDENTITY();
END
GO

CREATE PROCEDURE sp_message_ProfileAccount 
	-- Add the parameters for the stored procedure here
	@Reciving_ID INT,
	@Reciving_Type INT,
	@Messenger_ID INT,
	@Reference_Type INT,
	@Message VARCHAR(5000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY

		BEGIN TRAN Insert_ProfileAccount_Message

			DECLARE @new_message_id INT;

			INSERT INTO Profile_Messages
			(
				[Message_Text],
				[Messenger_ID],
				[Messenger_Type],
				[DateTime_Created]
			)
			(
				SELECT
				@Message,
				@Messenger_ID,
				@Reference_Type,
				GETDATE()
			);

			SET @new_message_id = SCOPE_IDENTITY();

			-- Case for message going to a project account
			IF (@Reciving_Type = 5)
			BEGIN
				INSERT INTO Profile_Messages_Projects_XREF
				(
					[Project_ID],
					[Profile_Message_ID]
				)
				(
					SELECT
					@Reciving_ID,
					@new_message_id
				);
			END

			-- case for a message going to an entrepreneur account
			ELSE IF (@Reciving_Type = 4)
			BEGIN
				INSERT INTO Profile_Messages_Entrepreneurs_XREF
				(
					[Entrepreneur_ID],
					[Profile_Message_ID]
				)
				(
					SELECT
					@Reciving_ID,
					@new_message_id
				);
			END

			-- case for a message going to an investor account
			ELSE IF (@Reciving_Type = 3)
			BEGIN
				INSERT INTO Profile_Messages_Investors_XREF
				(
					[Investor_ID],
					[Profile_Message_ID]
				)
				(
					SELECT
					@Reciving_ID,
					@new_message_id
				);
			END

		COMMIT TRAN Insert_ProfileAccount_Message

		RETURN 0;

	END TRY
	BEGIN CATCH

		ROLLBACK TRAN

		RAISERROR (150030, -1, -1, 'sp_message_ProfileAccount');

	END CATCH
END
GO

CREATE PROCEDURE sp_process_User_Message 
	-- Add the parameters for the stored procedure here
	@Email_Message_ID INT,
	@Processed BIT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @date_now DATETIME;
	SET @Date_now = GETDATE();
	UPDATE Email_Messages
	SET [Sent] = @Processed,
		[Sent_DateTime] = @Date_now,
		[Processed] = @Processed,
		[Processed_DateTime] = @Date_now
	WHERE [Email_Message_ID] = @Email_Message_ID

END
GO

CREATE PROCEDURE sp_search_by_NAME_profiles 
	-- Add the parameters for the stored procedure here
	@KeyArg VARCHAR(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	-- select through Projects
	SET @KeyArg = LTRIM(@KeyArg);
	SET @KeyArg = RTRIM(@KeyArg);
	SET @KeyArg = '%' + @KeyArg + '%';

	SELECT [Project_ID], [Name], [Description], [Investment_Goal]
	FROM Projects
	WHERE [Name] LIKE @KeyArg
	AND [Profile_Public] = 'TRUE'

	-- select through Entrepreneurs
	SELECT E.[Entrepreneur_ID], E.[Name], I.[Image_ID], I.[Binary_Image]
	FROM Entrepreneurs E
	RIGHT OUTER JOIN Images_Entrepreneurs_XREF IE_XREF ON E.[Entrepreneur_ID] = IE_XREF.[Entrepreneur_ID]
	JOIN Images I ON IE_XREF.[Image_ID] = I.[Image_ID] 
	WHERE [Name] LIKE @KeyArg
	AND E.[Profile_Public] = 'TRUE'

	-- select through Investors
	SELECT I.[Investor_ID], I.[Name], Im.[Image_ID], Im.[Binary_Image]
	FROM Investors I
	RIGHT OUTER JOIN Images_Investors_XREF II_XREF ON I.[Investor_ID] = II_XREF.[Investor_ID]
	JOIN Images Im ON II_XREF.[Image_ID] = Im.[Image_ID]
	WHERE [Name] LIKE @KeyArg
	AND I.[Profile_Public] = 'TRUE'
END
GO

CREATE PROCEDURE sp_update_EntrepreneurAccount 
	-- Add the parameters for the stored procedure here
	@Entrepreneur_ID INT,
	@Name VARCHAR(255),
	@Profile_Public BIT,
	@Image_ID INT,
	@Binary_Image VARBINARY(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY

		BEGIN TRAN Update_Entrepreneur_Entry

			DECLARE @new_image_id INT;
			DECLARE @image_id_pointer INT;

			UPDATE Entrepreneurs
			SET [Name] = COALESCE(@Name, [Name]),
				[Profile_Public] = COALESCE(@Profile_Public, [Profile_Public])
			WHERE [Entrepreneur_ID] = @Entrepreneur_ID

			-- insert a new image if no images already exist
			IF (SELECT COUNT(*) FROM Images_Entrepreneurs_XREF WHERE [Entrepreneur_ID] = @Entrepreneur_ID) < 1
			BEGIN

				INSERT INTO Images
				(
					[Binary_Image],
					[Created_DateTime]
				)
				(
					SELECT
					@Binary_Image,
					GETDATE()
				);

				SET @new_image_id = SCOPE_IDENTITY();

				INSERT INTO Images_Entrepreneurs_XREF
				(
					[Entrepreneur_ID],
					[Image_ID]
				)
				(
					SELECT
					@Entrepreneur_ID,
					@new_image_id
				);

			END

			-- update an image if image results do come up for this account
			IF (SELECT COUNT(*) FROM Images_Entrepreneurs_XREF WHERE [Entrepreneur_ID] = @Entrepreneur_ID) > 0 AND @Image_ID > 0
			BEGIN

				SET @image_id_pointer = (SELECT TOP 1 I.[Image_ID] FROM Images I JOIN Images_Entrepreneurs_XREF IU_XREF ON I.[Image_ID] = IU_XREF.[Image_ID] WHERE IU_XREF.[Entrepreneur_ID] = @Entrepreneur_ID)
				UPDATE Images
				SET [Binary_Image] = @Binary_Image,
					[Created_DateTime] = GETDATE()
				WHERE [Image_ID] = @Image_ID

			END

		COMMIT TRAN Update_Entrepreneur_Entry
		RETURN 0;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		RAISERROR (15004, -1, -1, 'sp_update_UserAccount');

	END CATCH
END
GO

CREATE PROCEDURE sp_update_InvestorAccount 
	-- Add the parameters for the stored procedure here
	@Investor_ID INT,
	@Name VARCHAR(255),
	@Profile_Public BIT,
	@Image_ID INT,
	@Binary_Image VARBINARY(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY

		BEGIN TRAN Update_Investor_Entry
			
			DECLARE @new_image_id INT;
			DECLARE @image_id_pointer INT;

			UPDATE Investors
			SET [Name] = COALESCE(@Name, [Name]),
				[Profile_Public] = COALESCE(@Profile_Public, [Profile_Public])
			WHERE [Investor_ID] = @Investor_ID

			-- insert a new image if no images already exist
			IF (SELECT COUNT(*) FROM Images_Investors_XREF WHERE [Investor_ID] = @Investor_ID) < 1
			BEGIN

				INSERT INTO Images
				(
					[Binary_Image],
					[Created_DateTime]
				)
				(
					SELECT
					@Binary_Image,
					GETDATE()
				);

				SET @new_image_id = SCOPE_IDENTITY();

				INSERT INTO Images_Investors_XREF
				(
					[Investor_ID],
					[Image_ID]
				)
				(
					SELECT
					@Investor_ID,
					@new_image_id
				);

			END

			-- update an image if image results do come up for this account
			IF (SELECT COUNT(*) FROM Images_Investors_XREF WHERE [Investor_ID] = @Investor_ID) > 0 AND @Image_ID > 0
			BEGIN
				
				SET @image_id_pointer = (SELECT TOP 1 I.[Image_ID] FROM Images I JOIN Images_Investors_XREF II_XREF ON I.[Image_ID] = II_XREF.[Image_ID] WHERE II_XREF.[Investor_ID] = @Investor_ID)
				UPDATE Images
				SET [Binary_Image] = @Binary_Image,
					[Created_DateTime] = GETDATE()
				WHERE [Image_ID] = @Image_ID

			END

		COMMIT TRAN Update_Investor_Entry
		RETURN 0;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		RAISERROR (15004, -1, -1, 'sp_update_UserAccount');

	END CATCH
END
GO

CREATE PROCEDURE sp_update_ProjectAccount 
	-- Add the parameters for the stored procedure here
	@Project_ID INT,
	@Name VARCHAR(255),
	@Description VARCHAR(MAX),
	@Profile_Public BIT,
	@Investment_Goal MONEY
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Projects
	SET [Name] = COALESCE(@Name, [Name]),
		[Description] = COALESCE(@Description, [Description]),
		[Profile_Public] = COALESCE(@Profile_Public, [Profile_Public]),
		[Investment_Goal] = COALESCE(@Investment_Goal, [Investment_Goal])
	WHERE [Project_ID] = @Project_ID
END
GO

CREATE PROCEDURE [dbo].[sp_update_UserAccount] 
	-- Add the parameters for the stored procedure here
	@user INT,
	@Email VARCHAR(255),
	@Image_ID INT,
	@Image_Bytes VARBINARY(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY

		BEGIN TRAN Update_User_Entry

			-- Declare variables for potential use later
			DECLARE @new_image_id INT;
			DECLARE @image_id_pointer INT;

			UPDATE Users
			SET [Email] = COALESCE(@Email, [Email])
			WHERE [User_ID] = @user

			IF @Image_Bytes IS NOT NULL
			BEGIN

				-- If there are no images related to this account, insert this one.
				IF (SELECT COUNT(*) FROM Images_Users_XREF IU_XREF WHERE IU_XREF.[User_ID] = @user) < 1
				BEGIN

					INSERT INTO Images
					(
						[Binary_Image],
						[Created_DateTime]
					)
					(
						SELECT
						@Image_Bytes,
						GETDATE()
					);

					SET @new_image_id = SCOPE_IDENTITY();

					INSERT INTO Images_Users_XREF
					(
						[User_ID],
						[Image_ID]
					)
					(
						SELECT
						@user,
						@new_image_id
					);

				END

				-- If ther are images related to this user account, and the image id passed in is greater than 0, null ints get set to 0 in .net.
				IF (SELECT COUNT(*) FROM Images_Users_XREF IU_XREF WHERE IU_XREF.[User_ID] = @user) > 0 AND @Image_ID > 0
				BEGIN
					
					SET @image_id_pointer = (SELECT TOP 1 [Image_ID] FROM Images_Users_XREF WHERE [User_ID] = @user ORDER BY [XREF_ID] ASC)
					UPDATE Images
					SET [Binary_Image] = @Image_Bytes,
						[Created_DateTime] = GETDATE()
					WHERE [Image_ID] = @Image_ID

				END

			END

		COMMIT TRAN Update_User_Entry
		RETURN 0;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		RAISERROR (15004, -1, -1, 'sp_update_UserAccount');

	END CATCH

END
GO

CREATE PROCEDURE sp_updatePROC_Entrepreneur 
	-- Add the parameters for the stored procedure here
	@PROC_ID INT,
	@Performance_BeginDate DATETIME,
	@Performance_EndDate DATETIME,
	@Project_Accepted INT,
	@Revenue_Percentage DECIMAL(18,0)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- UPdate PROC record for entrepreneur where Proc_ID = @PROC_ID and where Active is false.
	UPDATE Procs
	SET [Performance_BeginDate] = COALESCE(@Performance_BeginDate, [Performance_BeginDate]),
		[Performance_EndDate] = COALESCE(@Performance_EndDate, [Performance_EndDate]),
		[Project_Accepted] = COALESCE(@Project_Accepted, [Project_Accepted]),
		[Revenue_Percentage] = COALESCE(@Revenue_Percentage, [Revenue_Percentage])
	WHERE [Proc_ID] = @PROC_ID AND [Active] = 'FALSE'
END
GO

CREATE PROCEDURE sp_updatePROC_Investor 
	-- Add the parameters for the stored procedure here
	@PROC_ID INT,
	@Performance_BeginDate DATETIME,
	@Performance_EndDate DATETIME,
	@Investor_Accepted INT,
	@Revenue_Percentage DECIMAL(18,0),
	@Investment_Ammount MONEY
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- UPdate PROC record for investor where Proc_ID = @PROC_ID and where Active is false.
	UPDATE Procs
	SET [Performance_BeginDate] = COALESCE(@Performance_BeginDate, [Performance_BeginDate]),
		[Performance_EndDate] = COALESCE(@Performance_EndDate, [Performance_EndDate]),
		[Investor_Accepted] = COALESCE(@Investor_Accepted, [Investor_Accepted]),
		[Revenue_Percentage] = COALESCE(@Revenue_Percentage, [Revenue_Percentage]),
		[Investment_Ammount] = COALESCE(@Investment_Ammount, [Investment_Ammount])
	WHERE [Proc_ID] = @PROC_ID AND [Active] = 'FALSE'
END
GO

CREATE PROCEDURE sp_Upload_Project_Image 
	-- Add the parameters for the stored procedure here
	@Project_ID INT,
	@Binary_Image VARBINARY(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY

		BEGIN TRAN Upload_Image
			
			DECLARE @new_image_id INT;

			--Insert the image into our image table
			INSERT INTO Images
			(
				[Binary_Image],
				[Created_DateTime]
			)
			(
				SELECT
				@Binary_Image,
				GETDATE()
			);
			--Get the Id of the record just inserted
			SET @new_image_id = SCOPE_IDENTITY();

			--Insert a record into our Images_Projects_XREF table to relate this image to the profile account.
			INSERT INTO Images_Projects_XREF
			(
				[Project_ID],
				[Image_ID]
			)
			(
				SELECT
				@Project_ID,
				@new_image_id
			);

		COMMIT TRAN Upload_Image

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		RAISERROR (150028, -1, -1, 'sp_Upload_Project_Image');

	END CATCH
END
GO

CREATE PROCEDURE sp_uploadDocument_Project 
	-- Add the parameters for the stored procedure here
	@Project_ID INT,
	@File_Name VARCHAR(255),
	@Mime_Type VARCHAR(255),
	@Binary_File VARBINARY(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY

		BEGIN TRAN Upload_Document

			DECLARE @new_document_id INT
			-- Insert a record with the binary file, it's name and its mime type into our Documents table
			INSERT INTO Documents
			(
				[Binary_File],
				[Create_DateTime],
				[File_Name],
				[Mime_Type]
			)
			(
				SELECT
				@Binary_File,
				GETDATE(),
				@File_Name,
				@Mime_Type
			);
			-- Retrive the Id of the record that has been created.
			SET @new_document_id = SCOPE_IDENTITY();
			-- Insert a record into our Documents_Projects_XREF table using the captured new Document id
			INSERT INTO Documents_Projects_XREF
			(
				[Project_ID],
				[Document_ID]
			)
			(
				SELECT
				@Project_ID,
				@new_document_id
			);

		COMMIT TRAN Upload_Document

	END TRY
	BEGIN CATCH

		ROLLBACK TRAN

		RAISERROR (15010, -1, -1, 'sp_uploadDocument_Project');

	END CATCH
END
GO

CREATE PROCEDURE User_Create 
	-- Add the parameters for the stored procedure here
	@Email VARCHAR(255),
	@AspNetUser_ID NVARCHAR(128),
	@Name VARCHAR(255)
AS
BEGIN
    -- Insert statements for procedure here
	BEGIN TRY
		BEGIN TRAN Create_User_Entry
			DECLARE @New_User_ID INT
			INSERT INTO Users
			(
				[Email],
				[Email_Verified],
				[Created_Date],
				[Last_Logon],
				[Deleted]
			)
			(
				SELECT
				@Email,
				'FALSE',
				GETDATE(),
				GETDATE(),
				'FALSE'
			);
			SET @New_User_ID = SCOPE_IDENTITY();
		
		-- now insert into a row into our User_AspNetUsers_XREF
		INSERT INTO Users_AspNetUsers_XREF (
			[AspNetUser_ID],
			[User_ID]
		)
		VALUES
		(
			@AspNetUser_ID,
			@New_User_ID
		)

		-- now insert an investor and entrepreneur account for the user.

		EXECUTE Investor_Create @New_User_ID, @Name

		EXECUTE Entrepreneur_Create @New_User_ID, @Name

		COMMIT TRAN Create_User_Entry

		RETURN

	END TRY
	BEGIN CATCH
		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('User_Create, ', ERROR_MESSAGE());

		RAISERROR (@err_msg, 16, 1);

		RETURN

	END CATCH
END
GO