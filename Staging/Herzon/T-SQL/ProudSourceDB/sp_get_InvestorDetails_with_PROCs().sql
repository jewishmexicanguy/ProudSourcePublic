USE [ProudSourceDB]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_InvestorDetails_with_PROCs]    Script Date: 5/16/2016 3:41:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_get_InvestorDetails_with_PROCs] 
	-- Add the parameters for the stored procedure here
	@Investor_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	-- Get investor account details
	SELECT I.[Name], I.[Profile_Public], I.[Verified]
	FROM Investors I
	WHERE I.[Investor_ID] = @Investor_ID

	-- Get image for investor profile.
	SELECT TOP 1 I.[Image_ID], I.[Binary_Image]
	FROM Images I
	JOIN Images_Investors_XREF II_XREF ON I.[Image_ID] = II_XREF.[Image_ID]
	WHERE II_XREF.[Investor_ID] = @Investor_ID

	-- Get a collection of PROC results related to this investor.
	SELECT P.[Proc_ID],
		P.[Investor_ID],
		P.[Project_ID],
		P.[Investment_Ammount],
		P.[Revenue_Percentage],
		P.[Active],
		Proj.[Name] AS 'Project_Name',
		E.[Name] AS 'Entrepreneur_Name'
	FROM Procs P
	JOIN Projects Proj ON P.[Project_ID] = Proj.[Project_ID]
	JOIN Entrepreneurs E ON Proj.[Entrepreneur_ID] = E.[Entrepreneur_ID]
	WHERE P.[Investor_ID] = @Investor_ID

	-- Get the account balance for the investor in USD
	SELECT SUM(T.[Amount]) AS 'USD_Balance'
	FROM ProudSourceAccounting.dbo.Accounts A
	JOIN ProudSourceAccounting.dbo.Transactions T ON A.[Account_ID] = T.[Account_ID]
	WHERE A.[Profile_Account_ID] = @Investor_ID
	AND A.[Profile_Type_ID] = 3
	AND T.[Transaction_State] = 'PROCESSED'
	AND T.[Currency_Type_ID] = 2

	-- Get the account balance for the investor in BTC
	SELECT SUM(T.[Amount]) AS 'BTC_Balance'
	FROM ProudSourceAccounting.dbo.Accounts A
	JOIN ProudSourceAccounting.dbo.Transactions T ON A.[Account_ID] = T.[Account_ID]
	WHERE A.[Profile_Account_ID] = @Investor_ID
	AND A.[Profile_Type_ID] = 3
	AND T.[Transaction_State] = 'PROCESSED'
	AND T.[Currency_Type_ID] = 1

	-- Get the pending transactionss for the financial account tied to this profile.
	SELECT T.[Description], T.[Amount], CT.[Currency]
	FROM ProudSourceAccounting.dbo.Accounts A
	JOIN ProudSourceAccounting.dbo.Transactions T ON A.[Account_ID] = T.[Account_ID]
	JOIN ProudSourceAccounting.dbo.Currency_Types CT ON T.[Currency_Type_ID] = CT.[Currency_Type_ID]
	WHERE A.[Profile_Account_ID] = @Investor_ID
	AND A.[Profile_Type_ID] = 3
	AND T.[Transaction_State] = 'PENDING'

	-- Get the Financial account id.
	SELECT A.[Account_ID]
	FROM ProudSourceAccounting.dbo.Accounts A
	WHERE A.[Profile_Account_ID] = @Investor_ID
	AND A.[Profile_Type_ID] = 3
END
