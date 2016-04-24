/*
* Author:	Herzon Flores
* Date:		2/23/2016
* Document:	Create Accounting Tables.sql
* Comment:	Executing this T-SQL will create accounting tables and pertinant tables in our ProudSourceAccounting.
* Tables Created:
*	Profile_Types,
*	Accounts,
*	Category,
*	Transaction_Types,
*	Transactions
*/

USE ProudSourceAccounting;

GO

CREATE TABLE Profile_Types
(
	[Profile_Type_ID] INT IDENTITY(1,1) NOT NULL,
	[Description] VARCHAR(300) NOT NULL,
	CONSTRAINT PK_Profile_Type_ID PRIMARY KEY CLUSTERED (Profile_Type_ID)
);

CREATE TABLE Accounts
(
	[Account_ID] INT IDENTITY(1,1) NOT NULL,
	[Profile_Account_ID] INT NOT NULL,
	[Profile_Type_ID] INT NOT NULL,
	[Date_Opened] DATETIME NOT NULL,
	[Date_Closed] DATETIME NULL,
	[Current_Balance] MONEY NULL,
	CONSTRAINT PK_Accounts PRIMARY KEY CLUSTERED (Account_ID),
	CONSTRAINT FK_Accounts_Profile_Types FOREIGN KEY (Profile_Type_ID)
		REFERENCES Profile_Types (Profile_Type_ID)
);

CREATE TABLE Category_Types
(
	[Category_ID] INT IDENTITY(1,1) NOT NULL,
	[Description] VARCHAR(1000) NOT NULL,
	CONSTRAINT PK_Category PRIMARY KEY CLUSTERED (Category_ID)
);

CREATE TABLE Transaction_Types
(
	[Transaction_Type_ID] INT IDENTITY(1,1) NOT NULL,
	[Description] VARCHAR(1000) NOT NULL
	CONSTRAINT PK_Transaction_Types PRIMARY KEY CLUSTERED (Transaction_Type_ID),
);

CREATE TABLE Currency_Types
(
	[Currency_Type_ID] INT IDENTITY(1,1) NOT NULL,
	[Currency] VARCHAR(200) NOT NULL,
);

CREATE TABLE Transactions
(
	[Transaction_ID] INT IDENTITY(1,1) NOT NULL,
	[Account_ID] INT NOT NULL,
	[Category_Type_ID] INT NOT NULL,
	[Transaction_Type_ID] INT NOT NULL,
	[Currency_Type_ID] INT NOT NULL,
	[Date_of_Transaction] DATETIME NOT NULL,
	[Description] VARCHAR(8000) NOT NULL,
	[Amount] MONEY NOT NULL,
	[Current_Balance] MONEY NULL,
	[src_account_ID] INT NULL,
	[Transaction_State] VARCHAR(250) NOT NULL,
	CONSTRAINT PK_Transactions PRIMARY KEY CLUSTERED (Transaction_ID),
	CONSTRAINT FK_Transactions_Accounts FOREIGN KEY (Account_ID)
		REFERENCES Accounts (Account_ID),
	CONSTRAINT FK_Transactions_Category_Types FOREIGN KEY (Category_Type_ID)
		REFERENCES Category_Types (Category_ID),
	CONSTRAINT FK_Transactions_Transaction_Types FOREIGN KEY (Transaction_Type_ID)
		REFERENCES Transaction_Types (Transaction_Type_ID),
	CONSTRAINT FK_Transactions_Currency_Types FOREIGN KEY (Currency_Type_ID)
		REFERENCES Currency_Types (Currency_Type_ID)
);