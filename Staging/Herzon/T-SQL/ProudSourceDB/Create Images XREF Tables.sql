/*
* Author:	Herzon Flores
* Date:		1/14/2016
* Document:	Create Images XREF Tables.sql
* Comment:	Executing this T-SQL will create XREF tables for Images between the other tables that will be related to them in our ProudSourceDB DataBase.
* Tables Created:
*	Images_Users_XREF,
*	Images_Entrepreneurs_XREF,
*	Images_Investors_XREF, 
*	Images_Projects_XREF
*/

USE ProudSourceDB;

GO

CREATE TABLE Images_Users_XREF
(
	[XREF_ID] INT IDENTITY(1,1) NOT NULL,
	[User_ID] INT NOT NULL,
	[Image_ID] INT NOT NULL,
	CONSTRAINT PK_Images_Users_XREF PRIMARY KEY CLUSTERED (XREF_ID),
	CONSTRAINT FK_Images_Users_XREF_Users FOREIGN KEY (User_ID)
		REFERENCES Users (User_ID),
	CONSTRAINT FK_Images_Users_XREF_Images FOREIGN KEY (Image_ID)
		REFERENCES Images (Image_ID)
);

CREATE TABLE Images_Entrepreneurs_XREF
(
	[XREF_ID] INT IDENTITY(1,1) NOT NULL,
	[Entrepreneur_ID] INT NOT NULL,
	[Image_ID] INT NOT NULL,
	CONSTRAINT PK_Images_Entrepreneurs_XREF PRIMARY KEY CLUSTERED (XREF_ID),
	CONSTRAINT FK_Images_Entrepreneurs_XREF_Entrepreneurs FOREIGN KEY (Entrepreneur_ID)
		REFERENCES Entrepreneurs (Entrepreneur_ID),
	CONSTRAINT FK_Images_Entrepreneurs_XREF_Images FOREIGN KEY (Image_ID)
		REFERENCES Images (Image_ID)
);

CREATE TABLE Images_Investors_XREF
(
	[XREF_ID] INT IDENTITY(1,1) NOT NULL,
	[Investor_ID] INT NOT NULL,
	[Image_ID] INT NOT NULL,
	CONSTRAINT PK_Images_Investors_XREF PRIMARY KEY CLUSTERED (XREF_ID),
	CONSTRAINT FK_Images_Investors_XREF_Investors FOREIGN KEY (Investor_ID)
		REFERENCES Investors (Investor_ID),
	CONSTRAINT FK_Images_Investors_XREF_Images FOREIGN KEY (Image_ID)
		REFERENCES Images (Image_ID)
);

CREATE TABLE Images_Projects_XREF
(
	[XREF_ID] INT IDENTITY(1,1) NOT NULL,
	[Project_ID] INT NOT NULL,
	[Image_ID] INT NOT NULL,
	CONSTRAINT PK_Images_Projects_XREF PRIMARY KEY CLUSTERED (XREF_ID),
	CONSTRAINT FK_Images_Projects_XREF_Projects FOREIGN KEY (Project_ID)
		REFERENCES Projects (Project_ID),
	CONSTRAINT FK_Images_Projects_XREF_Images FOREIGN KEY (Image_ID)
		REFERENCES Images (Image_ID)
);