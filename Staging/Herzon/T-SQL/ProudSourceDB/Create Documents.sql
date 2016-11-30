/*
* Author:	Herzon Flores
* Date:		1/13/2016
* Document:	Create Documents.sql
* Comment:	Executing this T-SQL will create a Documents table in our ProudSourceDB DataBase
*/

USE ProudSourceDB;

GO

CREATE TABLE Documents
(
	[Document_ID] INT IDENTITY(1,1) NOT NULL,
	[Binary_File] VARBINARY(MAX) NOT NULL,
	[Create_DateTime] DATETIME NOT NULL,
	[File_Name] VARCHAR(255) NOT NULL,
	[Mime_Type] VARCHAR(255) NOT NULL,
	CONSTRAINT PK_Documents PRIMARY KEY (Document_ID),
);