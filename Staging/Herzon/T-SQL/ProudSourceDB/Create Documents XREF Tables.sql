/*
* Author:	Herzon Flores
* Date:		1/14/2016
* Document:	Create Documents XREF Tables.sql
* Comment:	Executing this T-SQL will create XREF tables for Documents between the other tables that will be related to them in our ProudSourceDB DataBase.
* Tables Created:
*	Documents_Entrepreneurs_XREF,
*	Documents_Investors_XREF, 
*	Documents_Projects_XREF
*/

USE ProudSourceDB;

GO

CREATE TABLE Documents_Entrepreneurs_XREF
(
	[XREF_ID] INT IDENTITY(1,1) NOT NULL,
	[Entrepreneur_ID] INT NOT NULL,
	[Document_ID] INT NOT NULL,
	CONSTRAINT PK_Documents_Entrepreneurs_XREF PRIMARY KEY CLUSTERED (XREF_ID),
	CONSTRAINT FK_Documents_Entrepreneurs_XREF_Entrepreneurs FOREIGN KEY (Entrepreneur_ID)
		REFERENCES Entrepreneurs (Entrepreneur_ID),
	CONSTRAINT FK_Documents_Entrepreneurs_XREF_Documents FOREIGN KEY (Document_ID)
		REFERENCES Documents (Document_ID)
);

CREATE TABLE Documents_Investors_XREF
(
	[XREF_ID] INT IDENTITY(1,1) NOT NULL,
	[Investor_ID] INT NOT NULL,
	[Document_ID] INT NOT NULL,
	CONSTRAINT PK_Documents_Investors_XREF PRIMARY KEY CLUSTERED (XREF_ID),
	CONSTRAINT FK_Documents_Investors_XREF_Investors FOREIGN KEY (Investor_ID)
		REFERENCES Investors (Investor_ID),
	CONSTRAINT FK_Documents_Investors_XREF_Documents FOREIGN KEY (Document_ID)
		REFERENCES Documents (Document_ID)
);

CREATE TABLE Documents_Projects_XREF
(
	[XREF_ID] INT IDENTITY(1,1) NOT NULL,
	[Project_ID] INT NOT NULL,
	[Document_ID] INT NOT NULL,
	CONSTRAINT PK_Documents_Projects_XREF PRIMARY KEY CLUSTERED (XREF_ID),
	CONSTRAINT FK_Documents_Projects_XREF_Projects FOREIGN KEY (Project_ID)
		REFERENCES Projects (Project_ID),
	CONSTRAINT FK_Documents_Projects_XREF_Documents FOREIGN KEY (Document_ID)
		REFERENCES Documents (Document_ID)
);