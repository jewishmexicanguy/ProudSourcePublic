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
-- Description:	This procedure gets details for this Entrepreneur account.
-- =============================================
ALTER PROCEDURE sp_get_EntrepreneurDetails 
	-- Add the parameters for the stored procedure here
	@Entrepreneur_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Entrepreneur_ID], [Name], [Profile_Public]
	FROM Entrepreneurs
	WHERE [Entrepreneur_ID] = @Entrepreneur_ID

	SELECT I.[Image_ID], I.[Binary_Image]
	FROM Images I
	JOIN Images_Entrepreneurs_XREF IE_XREF ON I.[Image_ID] = IE_XREF.[Image_ID]
	WHERE IE_XREF.[Entrepreneur_ID] = @Entrepreneur_ID
END
GO
