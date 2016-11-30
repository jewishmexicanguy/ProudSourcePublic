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
-- Create date: 2/23/2016
-- Description:	This procedure will create a financial account
-- =============================================
ALTER PROCEDURE sp_Create_Financial_Account 
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
