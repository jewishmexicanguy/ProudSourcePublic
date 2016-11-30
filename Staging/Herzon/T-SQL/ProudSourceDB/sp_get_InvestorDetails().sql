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
-- Create date: 1/24/2016
-- Description:	Retrive investor data to be displayed o nthe investor edit page.
-- =============================================
CREATE PROCEDURE sp_get_InvestorDetails 
	-- Add the parameters for the stored procedure here
	@Investor_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT I.[Investor_ID], I.[Name], I.[Profile_Public]
	FROM Investors I
	WHERE I.[Investor_ID] = @Investor_ID

	SELECT I.[Image_ID], I.[Binary_Image]
	FROM Images I
	JOIN Images_Investors_XREF II_XREF ON I.[Image_ID] = II_XREF.[Image_ID]
	WHERE II_XREF.[Investor_ID] = @Investor_ID;
END
GO
