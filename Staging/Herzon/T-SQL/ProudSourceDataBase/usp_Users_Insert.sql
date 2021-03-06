USE [ProudSourceDataBase]
GO
/****** Object:  StoredProcedure [dbo].[usp_User_Insert]    Script Date: 5/22/2016 9:14:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 5/20/2016
-- Description:	This will register a new record on ourn Users table which will represent a new user.
-- =============================================
ALTER PROCEDURE [dbo].[usp_Users_Insert] 
	-- Add the parameters for the stored procedure here
	@UserName NVARCHAR(256), 
	@PasswordHash NVARCHAR(256),
	@Id NVARCHAR(MAX),
	@Name NVARCHAR(256)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY
		BEGIN TRANSACTION
			INSERT INTO Users
            (
				[Id],
                [PasswordHash],
                [EmailConfirmed],
                [PhoneNumberconfirmed],
                [TwoFactorEnabled],
                [LockoutEnabled],
                [AccessFailedCount],
                [UserName],
                [Name]
            )
            VALUES
            (
				@Id,
                @PasswordHash,
				0,
                0,
				0,
                0,
                0,
                @UserName,
                @Name
            )
		COMMIT
	END TRY
	BEGIN CATCH
		
		ROLLBACK TRAN

		DECLARE @err_msg VARCHAR(MAX) = CONCAT('usp_User_Insert, ', ERROR_MESSAGE());

		RAISERROR (@err_msg, 16, 1);

	END CATCH
		
END
