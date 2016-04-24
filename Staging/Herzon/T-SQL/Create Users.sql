/*
* Author:	Herzon Flores
* Date:		1/13/2016
* Document:	Create Users.sql
* Comment:	Executing this T-SQL will create a Users table in our ProudSourceDB DataBase
*/

USE ProudSourceDB;

GO

CREATE TABLE Users
(
	[User_ID] INT IDENTITY(1,1),
	[Email] VARCHAR(255),
	[Email_Verified] BIT NOT NULL,
	[Created_Date] DATETIME NOT NULL,
	[Last_Logon] DATETIME NOT NULL,
	[Deleted] BIT NOT NULL,
	[Deleted_Date] DATETIME,
	CONSTRAINT PK_User_ID PRIMARY KEY CLUSTERED (User_ID)
);