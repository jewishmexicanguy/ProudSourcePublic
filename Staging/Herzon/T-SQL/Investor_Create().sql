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
-- Create date: 1/14/2016
-- Description:	This procedure will create Investor records
-- =============================================
ALTER PROCEDURE Investor_Create 
	-- Add the parameters for the stored procedure here
	@User_ID INT,
	@Investor_Name VARCHAR(255)
AS
BEGIN
    -- Insert statements for procedure here
	BEGIN TRY
		BEGIN TRAN Create_Investor_Entry
			DECLARE @New_Investor_ID INT;
			INSERT INTO Investors
			(
				[User_ID],
				[Created_Date],
				[Name],
				[Last_Logon],
				[Profile_Public],
				[Deleted],
				[Verified]
			)
			VALUES
			(
				@User_ID,
				GETDATE(),
				@Investor_Name,
				GETDATE(),
				'FALSE',
				'FALSE',
				'FALSE'
			);
			SET @New_Investor_ID = SCOPE_IDENTITY();

			DECLARE @date DATETIME = GETDATE()

			EXECUTE ProudSourceAccounting.dbo.sp_Create_Financial_Account @New_Investor_ID, 3

		COMMIT TRAN Create_Investor_Entry
		SELECT @New_Investor_ID;
		RETURN
	END TRY
	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('Investor_Create, ', ERROR_MESSAGE());

		RAISERROR (@err_msg, 16, 1);

		RETURN

	END CATCH
END
GO
