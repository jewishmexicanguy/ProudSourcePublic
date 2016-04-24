/*
* Author:	Herzon Flores
* Date:		2/3/2016
* Document: Create Profile_Messages and XREF tables.sql
* Comment:	Executing this T-SQL will create a Messages table in our ProudSourceDB DataBase and cross reference tables that tie with
*	Entrepreneurs, Investors and Projects.
*/

USE ProudSourceDB;

GO

CREATE TABLE Profile_Messages
(
	[Profile_Message_ID] INT IDENTITY(1,1) NOT NULL,
	[Message_Text] VARCHAR(5000) NOT NULL,
	[Messenger_ID] INT NOT NULL,
	[Messenger_Type] INT NOT NULL,
	[DateTime_Created] DATETIME NOT NULL,
	[Viewed] BIT,
	CONSTRAINT PK_Profile_Messages PRIMARY KEY CLUSTERED (Profile_Message_ID),
);

CREATE TABLE Profile_Messages_Entrepreneurs_XREF
(
	[XREF_ID] INT IDENTITY(1,1) NOT NULL,
	[Entrepreneur_ID] INT NOT NULL,
	[Profile_Message_ID] INT NOT NULL,
	CONSTRAINT PK_Profile_Messages_Entrepreneurs_XREF PRIMARY KEY CLUSTERED (XREF_ID),
);

CREATE TABLE Profile_Messages_Investors_XREF
(
	[XREF_ID] INT IDENTITY(1,1) NOT NULL,
	[Investor_ID] INT NOT NULL,
	[Profile_Message_ID] INT NOT NULL,
	CONSTRAINT PK_Profile_Messages_Investors_XREF PRIMARY KEY CLUSTERED (XREF_ID),
);

CREATE TABLE Profile_Messages_Projects_XREF
(
	[XREF_ID] INT IDENTITY(1,1) NOT NULL,
	[Project_ID] INT NOT NULL,
	[Profile_Message_ID] INT NOT NULL,
	CONSTRAINT PK_Profile_Messages_Projects_XREF PRIMARY KEY CLUSTERED (XREF_ID),
);