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
-- Create date: 2/3/2016
-- Description:	Procedure that uploads an image for this account.
-- =============================================
ALTER PROCEDURE sp_Upload_Project_Image 
	-- Add the parameters for the stored procedure here
	@Project_ID INT,
	@Binary_Image VARBINARY(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY

		BEGIN TRAN Upload_Image
			
			DECLARE @new_image_id INT;

			--Insert the image into our image table
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
			--Get the Id of the record just inserted
			SET @new_image_id = SCOPE_IDENTITY();

			--Insert a record into our Images_Projects_XREF table to relate this image to the profile account.
			INSERT INTO Images_Projects_XREF
			(
				[Project_ID],
				[Image_ID]
			)
			(
				SELECT
				@Project_ID,
				@new_image_id
			);

		COMMIT TRAN Upload_Image

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		RAISERROR (150028, -1, -1, 'sp_Upload_Project_Image');

	END CATCH
END
GO
