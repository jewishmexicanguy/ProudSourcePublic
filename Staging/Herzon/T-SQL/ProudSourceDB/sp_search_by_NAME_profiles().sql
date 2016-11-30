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
-- Create date: 2/11/2016
-- Description:	Stored procedure that gets Project, Entrepreneur and Investor Profile datum based off of a search query that will search for like names.
-- =============================================
CREATE PROCEDURE sp_search_by_NAME_profiles 
	-- Add the parameters for the stored procedure here
	@KeyArg VARCHAR(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	-- select through Projects
	SET @KeyArg = LTRIM(@KeyArg);
	SET @KeyArg = RTRIM(@KeyArg);
	SET @KeyArg = '%' + @KeyArg + '%';

	SELECT [Project_ID], [Name], [Description], [Investment_Goal]
	FROM Projects
	WHERE [Name] LIKE @KeyArg
	AND [Profile_Public] = 'TRUE'

	-- select through Entrepreneurs
	SELECT E.[Entrepreneur_ID], E.[Name], I.[Image_ID], I.[Binary_Image]
	FROM Entrepreneurs E
	RIGHT OUTER JOIN Images_Entrepreneurs_XREF IE_XREF ON E.[Entrepreneur_ID] = IE_XREF.[Entrepreneur_ID]
	JOIN Images I ON IE_XREF.[Image_ID] = I.[Image_ID] 
	WHERE [Name] LIKE @KeyArg
	AND E.[Profile_Public] = 'TRUE'

	-- select through Investors
	SELECT I.[Investor_ID], I.[Name], Im.[Image_ID], Im.[Binary_Image]
	FROM Investors I
	RIGHT OUTER JOIN Images_Investors_XREF II_XREF ON I.[Investor_ID] = II_XREF.[Investor_ID]
	JOIN Images Im ON II_XREF.[Image_ID] = Im.[Image_ID]
	WHERE [Name] LIKE @KeyArg
	AND I.[Profile_Public] = 'TRUE'
END
GO
