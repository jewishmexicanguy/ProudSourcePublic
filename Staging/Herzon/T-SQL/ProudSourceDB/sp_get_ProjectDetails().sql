USE [ProudSourceDB]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_ProjectDetails]    Script Date: 5/10/2016 3:14:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_get_ProjectDetails] 
	-- Add the parameters for the stored procedure here
	@Project_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Name], [Description], [Investment_Goal], [Profile_Public]
	FROM Projects
	WHERE [Project_ID] = @Project_ID

	SELECT I.[Image_ID], I.[Binary_Image]
	FROM Images I
	JOIN Images_Projects_XREF IP_XREF ON I.[Image_ID] = IP_XREF.[Image_ID]
	WHERE IP_XREF.[Project_ID] = @Project_ID

	SELECT D.[Document_ID], D.[File_Name]
	FROM Documents D
	JOIN Documents_Projects_XREF DP_XREF ON D.[Document_ID] = DP_XREF.[Document_ID]
	WHERE DP_XREF.[Project_ID] = @Project_ID

	SELECT [Proc_ID], I.[Name] ,Procs.[Created_Date], [Performance_BeginDate], [Performance_EndDate], [Revenue_Percentage]
	FROM Procs 
	JOIN Investors I ON Procs.[Investor_ID] = I.[Investor_ID]
	WHERE [Project_ID] = @Project_ID

	SELECT SUM(T.[Amount]) AS 'USD_Balance'
	FROM ProudSourceAccounting.dbo.Accounts A
	JOIN ProudSourceAccounting.dbo.Transactions T ON A.[Account_ID] = T.[Account_ID]
	WHERE A.[Profile_Account_ID] = @Project_ID
	AND A.[Profile_Type_ID] = 5
	AND T.[Transaction_State] = 'PROCESSED'
	AND T.[Currency_Type_ID] = 2

	SELECT SUM(T.[Amount]) AS 'BTC_Balance'
	FROM ProudSourceAccounting.dbo.Accounts A
	JOIN ProudSourceAccounting.dbo.Transactions T ON A.[Account_ID] = T.[Account_ID]
	WHERE A.[Profile_Account_ID] = @Project_ID
	AND A.[Profile_Type_ID] = 5
	AND T.[Transaction_State] = 'PROCESSED'
	AND T.[Currency_Type_ID] = 1

	SELECT A.[Account_ID]
	FROM ProudSourceAccounting.dbo.Accounts A
	WHERE A.[Profile_Account_ID] = @Project_ID
	AND A.[Profile_Type_ID] = 5

	SELECT IE_XREF.[Entrepreneur_ID], I.[Binary_Image]
	FROM Images I
	JOIN Images_Entrepreneurs_XREF IE_XREF ON I.[Image_ID] = IE_XREF.[Image_ID]
	JOIN Projects P ON IE_XREF.[Entrepreneur_ID] = P.[Entrepreneur_ID]
	WHERE P.[Project_ID] = @Project_ID
END
