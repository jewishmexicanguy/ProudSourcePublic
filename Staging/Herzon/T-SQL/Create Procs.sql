/*
* Author:	Herzon Flores
* Date:		1/13/2016
* Document:	Create Procs.sql
* Comment:	Executing this T-SQL will create a Procs table in our ProudSourceDB DataBase
*/

USE ProudSourceDB;

GO

CREATE TABLE Procs
(
	[Proc_ID] INT IDENTITY(1,1) NOT NULL,
	[Investor_ID] INT NOT NULL,
	[Project_ID] INT NOT NULL,
	[Created_Date] DATETIME NOT NULL,
	[Performance_BeginDate] DATETIME NOT NULL,
	[Performance_EndDate] DATETIME NOT NULL,
	[Investment_Ammount] MONEY NOT NULL,
	[Revenue_Percentage] DECIMAL NOT NULL,
	[Active] BIT NOT NULL,
	[Date_Activated] DATETIME,
	[Expired] BIT,
	[Project_Accepted] BIT NOT NULL,
	[Investor_Accepted] BIT NOT NULL,
	[Mutually_Accepted] BIT NOT NULL,
	[Date_Mutually_Accepted] DATETIME,
	[Payment_Interval] INT,
	CONSTRAINT PK_Procs PRIMARY KEY CLUSTERED (Proc_ID),
	CONSTRAINT FK_Procs_Investors FOREIGN KEY (Investor_ID)
		REFERENCES Investors (Investor_ID),
	CONSTRAINT FK_Procs_Projects FOREIGN KEY (Project_ID)
		REFERENCES Projects (Project_ID)
);