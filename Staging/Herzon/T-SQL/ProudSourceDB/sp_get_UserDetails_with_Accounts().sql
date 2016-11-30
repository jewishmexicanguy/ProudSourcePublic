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
-- Create date: 1/21/2016
-- Description:	This procedure will retrive user account details with a collection of investor and entrepreneur accounts
-- =============================================
ALTER PROCEDURE sp_get_UserDetails_with_Accounts
	-- Add the parameters for the stored procedure here
	@User_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT U.[User_ID], U.[Email], U.[Email_Verified]
	FROM Users U
	WHERE U.[User_ID] = @User_ID

	SELECT I.[Image_ID], I.[Binary_Image]
	FROM Images I
	JOIN Images_Users_XREF IU_XREF ON I.[Image_ID] = IU_XREF.[Image_ID]
	WHERE IU_XREF.[User_ID] = @User_ID

	-- get entrepreneur stuff

	DECLARE @Entrepreneur_ID INT = (
		SELECT TOP 1 E.[Entrepreneur_ID]
		FROM Entrepreneurs E
		WHERE E.[User_ID] = @User_ID
	)

	SELECT E.[Entrepreneur_ID], E.[Name], E.[Profile_Public], E.[Verified]
	FROM Entrepreneurs E
	WHERE E.[Entrepreneur_ID] = @Entrepreneur_ID

	SELECT I.[Image_ID], I.[Binary_Image]
	FROM Images I
	JOIN Images_Entrepreneurs_XREF IE_XREF ON I.[Image_ID] = IE_XREF.[Image_ID]
	WHERE IE_XREF.[Entrepreneur_ID] = @Entrepreneur_ID

	SELECT P.[Project_ID], P.[Investment_Goal], SUM(T.[Amount]) AS 'Project_Balance'
	FROM Projects P
	JOIN ProudSourceAccounting.dbo.Accounts A ON P.[Project_ID] = A.[Profile_Account_ID] AND A.[Profile_Type_ID] = 5
	RIGHT OUTER JOIN ProudSourceAccounting.dbo.Transactions T ON A.[Account_ID] = T.[Account_ID]
	WHERE P.[Entrepreneur_ID] = @Entrepreneur_ID
	AND T.[Transaction_State] = 'PROCESSED'
	GROUP BY P.[Project_ID], p.[Investment_Goal]

	-- get investor stuff

	DECLARE @Investor_ID INT = (
		SELECT TOP 1 I.[Investor_ID]
		FROM Investors I
		WHERE I.[User_ID] = @User_ID
	)

	SELECT I.[Investor_ID], I.[Name], I.[Profile_Public], I.[Verified]
	FROM Investors I
	WHERE I.[Investor_ID] = @Investor_ID

	SELECT I.[Investor_ID], SUM(T.[Amount]) AS 'Investor_Balance'
	FROM Investors I
	JOIN ProudSourceAccounting.dbo.Accounts A ON I.[Investor_ID] = A.[Profile_Account_ID] 
	RIGHT OUTER JOIN ProudSourceAccounting.dbo.Transactions T ON A.[Account_ID] = T.[Account_ID]
	WHERE I.[Investor_ID] = @Investor_ID
	AND T.[Transaction_State] = 'PROCESSED'
	GROUP BY I.[Investor_ID], I.[Name], I.[Profile_Public], I.[Verified]

	SELECT T.[Transaction_ID], T.[Description], T.[Amount]
	FROM Investors I
	JOIN ProudSourceAccounting.dbo.Accounts A ON I.[Investor_ID] = A.[Profile_Account_ID] 
	RIGHT OUTER JOIN ProudSourceAccounting.dbo.Transactions T ON A.[Account_ID] = T.[Account_ID]
	WHERE I.[Investor_ID] = @Investor_ID
	AND T.[Transaction_State] = 'PENDING'

	SELECT I.[Image_ID], I.[Binary_Image]
	FROM Images I
	JOIN Images_Investors_XREF II_XREF ON I.[Image_ID] = II_XREF.[Image_ID]
	WHERE II_XREF.[Investor_ID] = @Investor_ID

	SELECT P.[Proc_ID], P.[Investment_Ammount], P.[Active]
	FROM Procs P
	WHERE P.[Investor_ID] = @Investor_ID

END
GO
