USE [ProudSourceDB]
GO
/****** Object:  StoredProcedure [dbo].[Investor_Create]    Script Date: 5/16/2016 5:26:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Investor_Create] 
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
				'TRUE',
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
