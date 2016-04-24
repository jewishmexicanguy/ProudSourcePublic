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
-- Description:	This stored procedure will create Entrepreneur records
-- =============================================
ALTER PROCEDURE Entrepreneur_Create
	-- Add the parameters for the stored procedure here
	@User_ID INT, 
	@Entrepreneur_Name VARCHAR(255)
AS
BEGIN
    -- Insert statements for procedure here
	BEGIN TRY
		BEGIN TRAN Create_Entrepreneur_Entry
			DECLARE @New_Entrepreneur_ID INT
			INSERT INTO Entrepreneurs 
			(
				[User_ID], 
				[Created_Date], 
				[Name],
				[Last_Logon], 
				[Profile_Public], 
				[Deleted],
				[Verified]
			)
			(
				SELECT 
				@User_ID,
				GETDATE(),
				@Entrepreneur_Name,
				GETDATE(),
				0,
				0,
				0
			);
			SET @New_Entrepreneur_ID = SCOPE_IDENTITY();

			---- Create a financial account for this Entrepreneur profile
			--DECLARE @date DATETIME = GETDATE()
			--EXECUTE ProudSourceAccounting.dbo.sp_Create_Financial_Account @New_Entrepreneur_ID, 4

		COMMIT TRAN Create_Entrepreneur_Entry
		SELECT @New_Entrepreneur_ID;
		RETURN
	END TRY
	BEGIN CATCH

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('Entrepreneur_Create, ', ERROR_MESSAGE());

		RAISERROR (@err_msg, 16, 1);

		RETURN

	END CATCH
END
GO
