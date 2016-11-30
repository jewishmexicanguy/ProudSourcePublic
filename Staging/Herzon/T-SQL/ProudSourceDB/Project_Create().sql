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
-- Description:	This procedure will create an Project record
-- =============================================
ALTER PROCEDURE Project_Create 
	-- Add the parameters for the stored procedure here
	@Entrepreneur_ID INT, 
	@Project_Name VARCHAR(MAX),
	@Project_Description VARCHAR(MAX),
	@Investment_Goal MONEY
AS
BEGIN
    -- Insert statements for procedure here
	BEGIN TRY
		BEGIN TRAN Create_Project_Entry
			DECLARE @New_Project_ID INT;
			INSERT INTO Projects
			(
				[Entrepreneur_ID],
				[Created_date],
				[Name],
				[Description],
				[Investment_Goal],
				[Profile_Public],
				[Deleted]
			)
			(
				SELECT
				@Entrepreneur_ID,
				GETDATE(),
				@Project_Name,
				@Project_Description,
				@Investment_Goal,
				'FALSE',
				'FALSE'
			);
			SET @New_Project_ID = SCOPE_IDENTITY()

			SELECT @New_Project_ID

			EXECUTE ProudSourceAccounting.dbo.sp_Create_Financial_Account @New_Project_ID, 5

		COMMIT TRAN Create_Project_Entry

		RETURN

	END TRY
	BEGIN CATCH
		ROLLBACK TRAN

		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = ERROR_MESSAGE();

		RAISERROR (@err_msg, 16, 1);

		RETURN

	END CATCH
END
GO
