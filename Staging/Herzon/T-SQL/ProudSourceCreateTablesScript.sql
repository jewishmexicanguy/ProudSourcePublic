USE [ProudSourceNEW]
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

USE [ProudSourceNEW]
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

USE [ProudSourceNEW]
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

USE [ProudSourceNEW]
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

USE [ProudSourceNEW]
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

USE [ProudSourceNEW]
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

USE [ProudSourceNEW]
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

USE [ProudSourceNEW]
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

USE [ProudSourceNEW]
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

USE [ProudSourceNEW]
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

USE [ProudSourceNEW]
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

USE [ProudSourceNEW]
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

USE [ProudSourceNEW]
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

USE [ProudSourceNEW]
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

USE [ProudSourceNEW]
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

USE [ProudSourceNEW]
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

USE [ProudSourceNEW]
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

USE [ProudSourceNEW]
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

USE [ProudSourceNEW]
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

USE [ProudSourceNEW]
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

USE [ProudSourceNEW]
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

USE [ProudSourceNEW]
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

USE [ProudSourceNEW]
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

USE [ProudSourceNEW]
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

USE [ProudSourceNEW]
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


