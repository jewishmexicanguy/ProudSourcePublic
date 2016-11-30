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
-- Create date: 1/27/2016
-- Description:	Deletes an image tied to this project account
-- =============================================
CREATE PROCEDURE sp_Delete_Project_Image 
	-- Add the parameters for the stored procedure here
	@Project_ID INT,
	@Image_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY

		BEGIN TRAN Delete_Project_Image

			DECLARE @xref_id INT;

			SET @xref_id = (SELECT [XREF_ID] FROM Images_Projects_XREF WHERE [Project_ID] = @Project_ID AND [Image_ID] = @Image_ID)

			DELETE FROM Images_Projects_XREF WHERE [XREF_ID] = @xref_id

			DELETE FROM Images WHERE [Image_ID] = @Image_ID

		COMMIT TRAN Delete_Project_Image

	END TRY
	BEGIN CATCH

		ROLLBACK TRAN

		RAISERROR (15012, -1, -1, 'sp_Delete_Project_Image');

	END CATCH
END
GO
