/*
* Author:	Herzon Flores
* Date:		1/13/2016
* Document:	Create Images.sql
* Comment:	Executing this T-SQL will create a Images table in our ProudSourceDB DataBase
*/

USE ProudSourceDB;

GO

CREATE TABLE Images
(
	[Image_ID] INT IDENTITY(1,1) NOT NULL,
	[Binary_Image] VARBINARY(MAX) NOT NULL,
	[Created_DateTime] DATETIME NOT NULL,
	CONSTRAINT PK_Images PRIMARY KEY (Image_ID),
);