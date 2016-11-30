/*
* Author:	Herzon Flores
* Date:		1/13/2016
* Document:	Create Entrepreneurs.sql
* Comment:	Executing this T-SQL will create a Entrepreneurs table in our ProudSourceDB DataBase
*/

USE ProudSourceDB;

GO

CREATE TABLE Entrepreneurs
(
	[Entrepreneur_ID] INT IDENTITY(1,1),
	[User_ID] INT NOT NULL,
	[Created_Date] DATETIME NOT NULL,
	[Name] VARCHAR(255),
	[Last_Logon] DATETIME NOT NULL,
	[Profile_Public] BIT NOT NULL,
	[Deleted] BIT NOT NULL,
	[Date_Deleted] DATETIME,
	[Verified] BIT NOT NULL,
	[Date_Verified] DATETIME,
	CONSTRAINT PK_Entrepreneurs PRIMARY KEY CLUSTERED (Entrepreneur_ID),
	CONSTRAINT FK_Entrepreneurs_Users FOREIGN KEY (User_ID)
		REFERENCES Users (User_ID)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);