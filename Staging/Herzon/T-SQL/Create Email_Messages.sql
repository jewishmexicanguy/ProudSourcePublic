/*
* Author:	Herzon Flores
* Date:		1/14/2016
* Document:	Create Email_Messages.sql
* Comment:	Executing this T-SQL will create the table Email_Messages in our ProudSourceDB DataBase.
* Objective:This table is meant to be general enough to hold all emails that will be processed by proudsource. 
*	It will house ingoing and outging emails, outgoing emails will begin as a refference ID and a reference type that refferences some record on another table when processed body, subject, destination, and origin fields will be filled in.
*	When the email is detected as sent succesfully the sent feild will be set to true, if an error happens the error field will be set to true.
*	Incoming emails will be handled similarly but in reverse. Incoming emails will have an additional string of characters after a + that google allows
*	we can use that to refference what Email_Message a response email is talking about. Incoming emails that do not have this will be treated as new.
*	The Email_Message_ID that the outgoing messge is however should also be included in the body of the message and made obvious to the user.
*/

USE ProudSourceDB;

GO

CREATE TABLE Email_Messages
(
	[Email_Message_ID] INT IDENTITY(1,1) NOT NULL,
	[Gmail_UID] INT,
	[Rfc_Message_ID] VARCHAR(998),
	[Origin_Address] VARCHAR(1024),
	[Destination] VARCHAR(MAX),
	[Inbound] BIT,
	[Outbound] BIT,
	[Response] BIT,
	[Subject] VARCHAR(MAX),
	[Body] VARCHAR(MAX),
	[Sent] BIT,
	[Sent_DateTime] DATETIME,
	[Processed] BIT,
	[Processed_DateTime] DATETIME,
	[Reference_ID] INT,
	[Reference_Type] INT,
	[Application_Created] BIT,
	[Appliction_ID] INT,
	[Error] BIT,
	[Error_Message] VARCHAR(MAX),
	CONSTRAINT PK_Email_Messages PRIMARY KEY CLUSTERED (Email_Message_ID)
);