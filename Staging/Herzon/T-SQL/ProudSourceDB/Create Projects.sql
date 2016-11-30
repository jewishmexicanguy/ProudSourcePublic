/*
* Author:	Herzon Flores
* Date:		1/13/2016
* Document:	Create Projects.sql
* Comment:	Executing this T-SQL will create a Projects table in our ProudSourceDB DataBase
*/

USE ProudSourceDB;

GO

CREATE TABLE Projects 
(
	[Project_ID] INT IDENTITY(1,1) NOT NULL,
	[Entrepreneur_ID] INT NOT NULL,
	[Created_Date] DATETIME NOT NULL,
	[Name] VARCHAR(255),
	[Description] VARCHAR(MAX),
	[Investment_Goal] MONEY NOT NULL,
	[Profile_Public] BIT NOT NULL,
	[Deleted] BIT NOT NULL,
	[Date_Deleted] DATETIME,
	CONSTRAINT PK_Projects PRIMARY KEY CLUSTERED (Project_ID),
	CONSTRAINT FK_Projects_Entrepreneurs FOREIGN KEY (Entrepreneur_ID)
		REFERENCES Entrepreneurs (Entrepreneur_ID)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);