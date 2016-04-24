/*
* Author:	Herzon Flores
* Date:		1/14/2016
* Document:	Create Email_Messages XREF Tables.sql
* Comment:	Executing this T-SQL will create cross reference tables between Email_Messages and pertinant tables in our ProudSourceDB DataBase.
* Tables Created:
*	Email_Messages_Users_XREF,
*	Email_Messages_Entrepreneurs_XREF,
*	Email_Messages_Investors_XREF,
*	Email_Messages_Projects_XRERF,
*	Email_Messages_Procs_XREF
*/

USE ProudSourceDB;

GO

CREATE TABLE Email_Messages_Users_XREF
(
	[XREF_ID] INT IDENTITY(1,1) NOT NULL,
	[Email_Message_ID] INT NOT NULL,
	[User_ID] INT NOT NULL,
	CONSTRAINT PK_Email_Messages_Users_XREF PRIMARY KEY CLUSTERED (XREF_ID),
	CONSTRAINT FK_Email_Messages_Users_XREF_Email_Messages FOREIGN KEY (Email_Message_ID)
		REFERENCES Email_Messages (Email_Message_ID),
	CONSTRAINT FK_Email_Messages_Users_XREF_Users FOREIGN KEY (User_ID)
		REFERENCES Users (User_ID)
);

CREATE TABLE Email_Messages_Entrepreneurs_XREF
(
	[XREF_ID] INT IDENTITY(1,1) NOT NULL,
	[Email_Message_ID] INT NOT NULL,
	[Entrepreneur_ID] INT NOT NULL,
	CONSTRAINT PK_Email_Messages_Entrepreneurs_XREF PRIMARY KEY CLUSTERED (XREF_ID),
	CONSTRAINT FK_Email_Messages_Entrepreneurs_XREF_Email_Messages FOREIGN KEY (Email_Message_ID)
		REFERENCES Email_Messages (Email_Message_ID),
	CONSTRAINT FK_Email_Messages_Entrepreneurs_XREF_Entrepreneurs FOREIGN KEY (Entrepreneur_ID)
		REFERENCES Entrepreneurs (Entrepreneur_ID)
);

CREATE TABLE Email_Messages_Investors_XREF
(
	[XREF_ID] INT IDENTITY(1,1) NOT NULL,
	[Email_Message_ID] INT NOT NULL,
	[Investor_ID] INT NOT NULL,
	CONSTRAINT PK_Email_Messages_Investors_XREF PRIMARY KEY CLUSTERED (XREF_ID),
	CONSTRAINT FK_Email_Messages_Investors_XREF_Email_Messages FOREIGN KEY (Email_Message_ID)
		REFERENCES Email_Messages (Email_Message_ID),
	CONSTRAINT FK_Email_Messages_Projects_XREF_Investors FOREIGN KEY (Investor_ID)
		REFERENCES Investors (Investor_ID)
);

CREATE TABLE Email_Messages_Projects_XREF
(
	[XREF_ID] INT IDENTITY(1,1) NOT NULL,
	[Email_Message_ID] INT NOT NULL,
	[Project_ID] INT NOT NULL,
	CONSTRAINT PK_Email_Messages_Projects_XREF PRIMARY KEY CLUSTERED (XREF_ID),
	CONSTRAINT FK_Email_Messages_Projects_XREF_Email_Messages FOREIGN KEY (Email_Message_ID)
		REFERENCES Email_Messages (Email_Message_ID),
	CONSTRAINT FK_Email_Messages_Projects_XREF_Projects FOREIGN KEY (Project_ID)
		REFERENCES Projects (Project_ID)
);

CREATE TABLE Email_Messages_Procs_XREF
(
	[XREF_ID] INT IDENTITY(1,1) NOT NULL,
	[Email_Message_ID] INT NOT NULL,
	[Proc_ID] INT NOT NULL,
	CONSTRAINT PK_Email_Messages_Procs_XREF PRIMARY KEY CLUSTERED (XREF_ID),
	CONSTRAINT FK_Email_Messages_Procs_XREF_Email_Messages FOREIGN KEY (Email_Message_ID)
		REFERENCES Email_Messages (Email_Message_ID),
	CONSTRAINT FK_Email_Messages_Procs_XREF_Procs FOREIGN KEY (Proc_ID)
		REFERENCES Procs (Proc_ID) 
);