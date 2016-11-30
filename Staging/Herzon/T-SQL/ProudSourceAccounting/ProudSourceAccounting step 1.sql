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
	CONSTRAINT PK_Currency_Types PRIMARY KEY CLUSTERED (Currency_Type_ID),
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
	[Outside_Transaction_ID] VARCHAR(250) NULL,
	[Outside_Financial_Platfrom] VARCHAR(250) NULL,
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
GO

CREATE PROCEDURE sp_Create_Financial_Account 
	-- Add the parameters for the stored procedure here
	@profile_account_id INT,
	@profile_type_id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY
		
		BEGIN TRAN Create_Account

			DECLARE @new_account_ID INT;

			INSERT INTO Accounts
			(
				[Profile_Account_ID],
				[Profile_Type_ID],
				[Date_Opened]
			)
			VALUES
			(
				@profile_account_id,
				@profile_type_id,
				GETDATE()
			)

			SET @new_account_ID = SCOPE_IDENTITY();

			SELECT @new_account_ID

		COMMIT TRAN Create_Account
		RETURN

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('sp_Create_Financial_Account, ', ERROR_MESSAGE());

		RAISERROR (@err_msg, 16, 1);

		RETURN

	END CATCH
END
GO

CREATE PROCEDURE sp_Insert_Financial_Transaction 
	-- Add the parameters for the stored procedure here
	@Account_ID INT,
	@Category_Type_ID INT,
	@Transaction_Type_ID INT,
	@Amount MONEY,
	@Currency_Type_ID INT,
	@Src_Account_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY
		
		BEGIN TRAN New_Transaction

			IF(@Account_ID IS NULL OR @Account_ID = 0)
			BEGIN

				RAISERROR ('sp_Insert_Financial_Transaction, @Account_ID is unacceptable', -1, -1);

			END

			IF(@Category_Type_ID IS NULL OR @Category_Type_ID = 0)
			BEGIN

				RAISERROR ('sp_Insert_Financial_Transaction, @Category_Type_ID is unacceptable', -1, -1);

			END

			IF(@Transaction_Type_ID IS NULL OR @Transaction_Type_ID = 0)
			BEGIN

				RAISERROR ('sp_Insert_Financial_Transaction, @Transaction_Type_ID  is unacceptable', -1, -1);

			END

			IF(@Currency_Type_ID IS NULL OR @Currency_Type_ID = 0)
			BEGIN

				RAISERROR ('sp_Insert_Financial_Transaction, @Currency_Type_ID is unacceptable', -1, -1);

			END

			IF(@Amount IS NULL OR @Account_ID = 0)
			BEGIN

				RAISERROR ('sp_Insert_Financial_Transaction, @Amount is unacceptable', -1, -1);

			END

			-- We need to get the description based on the category of this transaction
			DECLARE @category_description AS VARCHAR(250) = (SELECT [Description] FROM Category_Types WHERE [Category_ID] = @Category_Type_ID)

			-- We need to get the description based on the transaction type
			DECLARE @transaction_description AS VARCHAR(250) = (SELECT [Description] FROM Transaction_Types WHERE [Transaction_Type_ID] = @Transaction_Type_ID)

			-- Control flow based on whether this transaction originated from an internal account.
			IF(@Src_Account_ID IS NULL OR @Src_Account_ID = 0)
			BEGIN

				INSERT INTO Transactions
				(
					[Account_ID],
					[Category_Type_ID],
					[Transaction_Type_ID],
					[Date_of_Transaction],
					[Description],
					[Amount],
					[Current_Balance],
					[Transaction_State],
					[Currency_Type_ID]
				)
				VALUES 
				(
					@Account_ID,
					@Category_Type_ID,
					@Transaction_Type_ID ,
					GETDATE(),
					CONCAT(@category_description, ' : ', @transaction_description, ' : ', (SELECT GETDATE() AS VARCHAR)),
					@Amount,
					(
						SELECT SUM([Amount]) 
						FROM Transactions 
						WHERE [Account_ID] = @Account_ID 
						AND [Transaction_State] = 'PROCESSED'
					),
					'PENDING',
					@Currency_Type_ID
				)

				SELECT SCOPE_IDENTITY();

			END
			-- If it did record it in this record so we can keep track of all internal movements
			ELSE
			BEGIN

				INSERT INTO Transactions 
				(
					[Account_ID],
					[Category_Type_ID],
					[Transaction_Type_ID],
					[Date_of_Transaction],
					[Description],
					[Amount],
					[Current_Balance],
					[src_account_ID],
					[Transaction_State],
					[Currency_Type_ID]
				)
				VALUES 
				(
					@Account_ID,
					@Category_Type_ID,
					@Transaction_Type_ID ,
					GETDATE(),
					CONCAT(@category_description, ' : ', @transaction_description, ' : ', (SELECT GETDATE() AS VARCHAR)),
					@Amount,
					(
						SELECT SUM([Amount]) 
						FROM Transactions 
						WHERE [Account_ID] = @Account_ID 
						AND [Transaction_State] = 'Processed'
					),
					@Src_Account_ID,
					'PENDING',
					@Currency_Type_ID
				)

				SELECT SCOPE_IDENTITY();
			END

		COMMIT TRAN New_Transaction
		RETURN

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = ERROR_MESSAGE();

		RAISERROR (@err_msg, 16, 1);

		RETURN

	END CATCH
END
GO

CREATE PROCEDURE [dbo].[sp_get_FinancialAccountDetails] 
	-- Add the parameters for the stored procedure here
	@Account_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT T.[Transaction_ID], T.[Amount], T.[Date_of_Transaction], T.[Description], CT.[Currency], T.[Transaction_State]
	FROM Accounts A 
    JOIN Transactions T ON A.[Account_ID] = T.[Account_ID]
    JOIN Currency_Types CT ON T.[Currency_Type_ID] = CT.[Currency_Type_ID]
    WHERE A.[Account_ID] = @Account_ID
		AND T.[Transaction_State] = 'PENDING'

    SELECT T.[Transaction_ID], T.[Amount], T.[Date_of_Transaction], T.[Description], CT.[Currency], T.[Transaction_State]
    FROM Accounts A
    JOIN Transactions T ON A.[Account_ID] = T.[Account_ID]
    JOIN Currency_Types CT ON T.[Currency_Type_ID] = CT.[Currency_Type_ID]
    WHERE A.[Account_ID] = @Account_ID
		AND T.[Transaction_State] <> 'PENDING'
END

INSERT INTO Transaction_Types
(
	[Description]
)
VALUES
(
	'Account Deposit'
)

INSERT INTO Transaction_Types
(
	[Description]
)
VALUES
(
	'Account Withdrawl'
)

INSERT INTO Transaction_Types
(
	[Description]
)
VALUES
(
	'Deposit Reversal'
)

INSERT INTO Transaction_Types
(
	[Description]
)
VALUES
(
	'Withdrawl Reversal'
)

INSERT INTO Profile_Types
(
	[Description]
)
VALUES
(
	'User'
);

INSERT INTO Profile_Types
(
	[Description]
)
VALUES
(
	'Financial'
);

INSERT INTO Profile_Types
(
	[Description]
)
VALUES
(
	'Investor'
);

INSERT INTO Profile_Types
(
	[Description]
)
VALUES
(
	'Entrepreneur'
);

INSERT INTO Profile_Types
(
	[Description]
)
VALUES
(
	'Project'
);

INSERT INTO Profile_Types
(
	[Description]
)
VALUES
(
	'PROC'
);

INSERT INTO Profile_Types
(
	[Description]
)
VALUES
(
	'Marketing'
);

INSERT INTO Profile_Types
(
	[Description]
)
VALUES
(
	'Customer Support'
);

INSERT INTO Profile_Types
(
	[Description]
)
VALUES
(
	'Job openings'
);

INSERT INTO Category_Types
(
	[Description]
)
VALUES
(
	'Incoming Bank Transfer'
)

INSERT INTO Category_Types
(
	[Description]
)
VALUES
(
	'Incoming Bitcoin Transfer'
)

INSERT INTO Category_Types
(
	[Description]
)
VALUES
(
	'Incoming Paypal Transfer'
)

INSERT INTO Category_Types
(
	[Description]
)
VALUES
(
	'Outgoing Bank Transfer'
)

INSERT INTO Category_Types
(
	[Description]
)
VALUES
(
	'Outgoing Bitcoin Transfer'
)

INSERT INTO Category_Types
(
	[Description]
)
VALUES
(
	'Outgoing Paypal Transfer'
)

INSERT INTO Category_Types
(
	[Description]
)
VALUES
(
	'PROC Investment'
)

INSERT INTO Category_Types
(
	[Description]
)
VALUES
(
	'PROC Payout'
)

INSERT INTO Category_Types
(
	[Description]
)
VALUES
(
	'PROC Reversal'
)

INSERT INTO Category_Types
(
	[Description]
)
VALUES
(
	'Account Correction'
)

INSERT INTO Category_Types
(
	[Description]
)
VALUES
(
	'Investment Fee'
)

INSERT INTO Category_Types
(
	[Description]
)
VALUES
(
	'Payout Fee'
)

INSERT INTO Currency_Types
(
	[Currency]
)
VALUES
(
	'BTC'
)

INSERT INTO Currency_Types
(
	[Currency]
)
VALUES
(
	'USD'
)