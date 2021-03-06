USE [ProudSourceDB]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetPROC_Details]    Script Date: 5/16/2016 3:28:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_GetPROC_Details] 
	-- Add the parameters for the stored procedure here
	@PROC_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @Investor_ID INT;

	DECLARE @Entrepreneur_ID INT;

	SET @Investor_ID = (SELECT [Investor_ID] FROM Procs WHERE [Proc_ID] = @Proc_ID)

	SET @Entrepreneur_ID = (SELECT [Entrepreneur_ID] FROM Procs PR JOIN Projects P ON PR.[Project_ID] = P.[Project_ID]  WHERE [Proc_ID] = @Proc_ID)

	SELECT PR.[Investor_ID], 
		PR.[Project_ID], 
		E.[Entrepreneur_ID],
		PR.[Investment_Ammount], 
		PR.[Revenue_Percentage], 
		PR.[Active], 
		PR.[Date_Activated], 
		PR.[Project_Accepted], 
		PR.[Investor_Accepted], 
		PR.[Mutually_Accepted], 
		PR.[Date_Mutually_Accepted], 
		PR.[Performance_BeginDate], 
		PR.[Performance_EndDate],
		P.[Name] AS 'Project Name', 
		I.[Name] AS 'Investor Name', 
		E.[Name] AS 'Entrepreneur Name'
	FROM Procs PR
	JOIN Projects P ON PR.[Project_ID] = P.[Project_ID]
	JOIN Investors I ON PR.[Investor_ID] = I.[Investor_ID]
	JOIN Entrepreneurs E ON P.[Entrepreneur_ID] = E.[Entrepreneur_ID]
	WHERE PR.[Proc_ID] = @PROC_ID

	SELECT TOP 1 I.[Binary_Image]
	FROM Images_Investors_XREF II_XREF
	JOIN Images I ON II_XREF.[Image_ID] = I.[Image_ID]
	WHERE II_XREF.[Investor_ID] = @Investor_ID
	ORDER BY I.[Image_ID]

	SELECT TOP 1 I.[Binary_Image]
	FROM Images_Entrepreneurs_XREF IU_XREF
	JOIN Images I ON IU_XREF.[Image_ID] = I.[Image_ID]
	WHERE IU_XREF.[Entrepreneur_ID] = @Entrepreneur_ID
	ORDER BY I.[Image_ID]

	SELECT SUM(T.[Amount]) AS 'Balance'
	FROM ProudSourceAccounting.dbo.Transactions T
	JOIN ProudSourceAccounting.dbo.Accounts A ON T.[Account_ID] = A.[Account_ID]
	WHERE A.[Profile_Account_ID] = @Investor_ID
		AND A.[Profile_Type_ID] = 3
		AND T.[Transaction_State] = 'PROCESSED'

END
