/*
* Author:	Herzon Flores
* Date:		1/13/2016
* Document:	Create Investors.sql
* Comment:	Executing this T-SQL will create a Investors table in our ProudSourceDB DataBase
*/

USE ProudSourceDB;

GO

CREATE TABLE Investors
(
	[Investor_ID] INT IDENTITY(1,1),
	[User_ID] INT NOT NULL,
	[Created_Date] DATETIME NOT NULL,
	[Name] VARCHAR(255),
	[Last_Logon] DATETIME NOT NULL,
	[Profile_Public] BIT NOT NULL,
	[Deleted] BIT NOT NULL,
	[Date_Deleted] DATETIME,
	[Verified] BIT NOT NULL,
	[Date_Verified] DATETIME,
	CONSTRAINT PK_Investors PRIMARY KEY CLUSTERED (Investor_ID),
	CONSTRAINT FK_Investors_Users FOREIGN KEY (User_ID)
		REFERENCES Users (User_ID)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);