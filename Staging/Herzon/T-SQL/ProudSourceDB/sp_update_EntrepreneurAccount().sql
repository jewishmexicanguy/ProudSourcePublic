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
-- Create date: 1/26/2016
-- Description:	This procedure will update this entrepreneur account with data from the client.
-- =============================================
CREATE PROCEDURE sp_update_EntrepreneurAccount 
	-- Add the parameters for the stored procedure here
	@Entrepreneur_ID INT,
	@Name VARCHAR(255),
	@Profile_Public BIT,
	@Image_ID INT,
	@Binary_Image VARBINARY(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY

		BEGIN TRAN Update_Entrepreneur_Entry

			DECLARE @new_image_id INT;
			DECLARE @image_id_pointer INT;

			UPDATE Entrepreneurs
			SET [Name] = COALESCE(@Name, [Name]),
				[Profile_Public] = COALESCE(@Profile_Public, [Profile_Public])
			WHERE [Entrepreneur_ID] = @Entrepreneur_ID

			-- insert a new image if no images already exist
			IF (SELECT COUNT(*) FROM Images_Entrepreneurs_XREF WHERE [Entrepreneur_ID] = @Entrepreneur_ID) < 1
			BEGIN

				INSERT INTO Images
				(
					[Binary_Image],
					[Created_DateTime]
				)
				(
					SELECT
					@Binary_Image,
					GETDATE()
				);

				SET @new_image_id = SCOPE_IDENTITY();

				INSERT INTO Images_Entrepreneurs_XREF
				(
					[Entrepreneur_ID],
					[Image_ID]
				)
				(
					SELECT
					@Entrepreneur_ID,
					@new_image_id
				);

			END

			-- update an image if image results do come up for this account
			IF (SELECT COUNT(*) FROM Images_Entrepreneurs_XREF WHERE [Entrepreneur_ID] = @Entrepreneur_ID) > 0 AND @Image_ID > 0
			BEGIN

				SET @image_id_pointer = (SELECT TOP 1 I.[Image_ID] FROM Images I JOIN Images_Entrepreneurs_XREF IU_XREF ON I.[Image_ID] = IU_XREF.[Image_ID] WHERE IU_XREF.[Entrepreneur_ID] = @Entrepreneur_ID)
				UPDATE Images
				SET [Binary_Image] = @Binary_Image,
					[Created_DateTime] = GETDATE()
				WHERE [Image_ID] = @Image_ID

			END

		COMMIT TRAN Update_Entrepreneur_Entry
		RETURN 0;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		RAISERROR (15004, -1, -1, 'sp_update_UserAccount');

	END CATCH
END
GO
