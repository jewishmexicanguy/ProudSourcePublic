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
-- Create date: 1/20/2016
-- Description:	This procedure will select two sets both. The first wil be Entrepreneur record details and the second will be the set of rows returned fro Projects related to the Entrepreneur record.
-- =============================================
ALTER PROCEDURE sp_get_EntrepreneurDetails_with_Projects 
	-- Add the parameters for the stored procedure here
	@Entrepreneur_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT E.[Entrepreneur_ID], E.[Name], E.[Profile_Public], E.[Verified]
	FROM Entrepreneurs E
	WHERE E.[Entrepreneur_ID] = @Entrepreneur_ID

	SELECT I.[Image_ID], I.[Binary_Image]
	FROM Images I
	JOIN Images_Entrepreneurs_XREF IE_XREF ON I.[Image_ID] = IE_XREF.[Image_ID]
	WHERE IE_XREF.[Entrepreneur_ID] = @Entrepreneur_ID

	SELECT P.[Project_ID], P.[Name], P.[Profile_Public], P.[Investment_Goal]
	FROM Projects P
	WHERE P.[Entrepreneur_ID] = @Entrepreneur_ID

	SELECT P.[Project_ID], P.[Investment_Goal], SUM(T.[Amount]) AS 'Project_Balance'
	FROM Projects P
	FULL OUTER JOIN ProudSourceAccounting.dbo.Accounts A ON P.[Project_ID] = A.[Profile_Account_ID] AND A.[Profile_Type_ID] = 5
	JOIN ProudSourceAccounting.dbo.Transactions T ON A.[Account_ID] = T.[Account_ID]
	WHERE P.[Entrepreneur_ID] = @Entrepreneur_ID
	AND T.[Transaction_State] = 'PROCESSED'
	GROUP BY P.[Project_ID], P.[Investment_Goal]
END
GO
