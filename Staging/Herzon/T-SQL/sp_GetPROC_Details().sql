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
-- Create date: 1/28/2016
-- Description:	Procedure gets the details of a PROC agreement to display for logged in clients of our site.
-- =============================================
ALTER PROCEDURE sp_GetPROC_Details 
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

END
GO
