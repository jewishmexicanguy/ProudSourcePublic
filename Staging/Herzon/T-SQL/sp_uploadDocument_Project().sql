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
-- Description:	Uploads a document file and relates it to the target project account.
-- =============================================
CREATE PROCEDURE sp_uploadDocument_Project 
	-- Add the parameters for the stored procedure here
	@Project_ID INT,
	@File_Name VARCHAR(255),
	@Mime_Type VARCHAR(255),
	@Binary_File VARBINARY(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY

		BEGIN TRAN Upload_Document

			DECLARE @new_document_id INT
			-- Insert a record with the binary file, it's name and its mime type into our Documents table
			INSERT INTO Documents
			(
				[Binary_File],
				[Create_DateTime],
				[File_Name],
				[Mime_Type]
			)
			(
				SELECT
				@Binary_File,
				GETDATE(),
				@File_Name,
				@Mime_Type
			);
			-- Retrive the Id of the record that has been created.
			SET @new_document_id = SCOPE_IDENTITY();
			-- Insert a record into our Documents_Projects_XREF table using the captured new Document id
			INSERT INTO Documents_Projects_XREF
			(
				[Project_ID],
				[Document_ID]
			)
			(
				SELECT
				@Project_ID,
				@new_document_id
			);

		COMMIT TRAN Upload_Document

	END TRY
	BEGIN CATCH

		ROLLBACK TRAN

		RAISERROR (15010, -1, -1, 'sp_uploadDocument_Project');

	END CATCH
END
GO
