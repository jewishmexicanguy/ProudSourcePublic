USE [ProudSourceDB]
GO
/****** Object:  StoredProcedure [dbo].[sp_update_UserAccount]    Script Date: 1/22/2016 3:45:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 1/22/2016
-- Description:	This procedure updates user account information.
-- =============================================
ALTER PROCEDURE [dbo].[sp_update_UserAccount] 
	-- Add the parameters for the stored procedure here
	@user INT,
	@Email VARCHAR(255),
	@Image_ID INT,
	@Image_Bytes VARBINARY(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY

		BEGIN TRAN Update_User_Entry

			-- Declare variables for potential use later
			DECLARE @new_image_id INT;
			DECLARE @image_id_pointer INT;

			UPDATE Users
			SET [Email] = COALESCE(@Email, [Email])
			WHERE [User_ID] = @user

			IF @Image_Bytes IS NOT NULL
			BEGIN

				-- If there are no images related to this account, insert this one.
				IF (SELECT COUNT(*) FROM Images_Users_XREF IU_XREF WHERE IU_XREF.[User_ID] = @user) < 1
				BEGIN

					INSERT INTO Images
					(
						[Binary_Image],
						[Created_DateTime]
					)
					(
						SELECT
						@Image_Bytes,
						GETDATE()
					);

					SET @new_image_id = SCOPE_IDENTITY();

					INSERT INTO Images_Users_XREF
					(
						[User_ID],
						[Image_ID]
					)
					(
						SELECT
						@user,
						@new_image_id
					);

				END

				-- If ther are images related to this user account, and the image id passed in is greater than 0, null ints get set to 0 in .net.
				IF (SELECT COUNT(*) FROM Images_Users_XREF IU_XREF WHERE IU_XREF.[User_ID] = @user) > 0 AND @Image_ID > 0
				BEGIN
					
					SET @image_id_pointer = (SELECT TOP 1 [Image_ID] FROM Images_Users_XREF WHERE [User_ID] = @user ORDER BY [XREF_ID] ASC)
					UPDATE Images
					SET [Binary_Image] = @Image_Bytes,
						[Created_DateTime] = GETDATE()
					WHERE [Image_ID] = @Image_ID

				END

			END

		COMMIT TRAN Update_User_Entry
		RETURN 0;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		RAISERROR (15004, -1, -1, 'sp_update_UserAccount');

	END CATCH

END
