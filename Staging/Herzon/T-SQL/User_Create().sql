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
-- Description:	Creates a user record
-- =============================================
ALTER PROCEDURE User_Create 
	-- Add the parameters for the stored procedure here
	@Email VARCHAR(255),
	@AspNetUser_ID NVARCHAR(128),
	@Name VARCHAR(255)
AS
BEGIN
    -- Insert statements for procedure here
	BEGIN TRY
		BEGIN TRAN Create_User_Entry
			DECLARE @New_User_ID INT
			INSERT INTO Users
			(
				[Email],
				[Email_Verified],
				[Created_Date],
				[Last_Logon],
				[Deleted]
			)
			(
				SELECT
				@Email,
				'FALSE',
				GETDATE(),
				GETDATE(),
				'FALSE'
			);
			SET @New_User_ID = SCOPE_IDENTITY();
		
		-- now insert into a row into our User_AspNetUsers_XREF
		INSERT INTO Users_AspNetUsers_XREF (
			[AspNetUser_ID],
			[User_ID]
		)
		VALUES
		(
			@AspNetUser_ID,
			@New_User_ID
		)

		-- now insert an investor and entrepreneur account for the user.

		EXECUTE Investor_Create @New_User_ID, @Name

		EXECUTE Entrepreneur_Create @New_User_ID, @Name

		COMMIT TRAN Create_User_Entry

		RETURN

	END TRY
	BEGIN CATCH
		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('User_Create, ', ERROR_MESSAGE());

		RAISERROR (@err_msg, 16, 1);

		RETURN

	END CATCH
END
GO
