-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 2/25/2016
-- Description:	Will insert a new financial transaction record with a state of pending
-- =============================================
ALTER PROCEDURE sp_Insert_Financial_Transaction 
	-- Add the parameters for the stored procedure here
	@Account_ID INT,
	@Category_Type_ID INT,
	@Transaction_Type_ID INT,
	@Amount MONEY,
	@Currency_Type_ID INT,
	@Src_Account_ID INT,
	@ThirdParty_Transaction_ID VARCHAR(250),
	@Financial_Platform VARCHAR(250)
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
					[Currency_Type_ID],
					[Outside_Transaction_ID],
					[Outside_Financial_Platform]
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
					@Currency_Type_ID,
					@ThirdParty_Transaction_ID,
					@Financial_Platform

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
