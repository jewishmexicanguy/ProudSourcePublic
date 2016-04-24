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
-- Description:	This stored procedure will return a collection of trendy projects to display info about.
-- =============================================
ALTER PROCEDURE sp_get_Trending_Projects 
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @BeginDate DATETIME
	SET @BeginDate = DATEADD(DAY, -21, GETDATE())
	DECLARE @EndDate DATETIME
	SET @EndDate = GETDATE()

	-- Get a set of data that is the projects and entrepreneur datum to display
	--SELECT P.[Project_ID], P.[Name], P.[Description], P.[Investment_Goal], E.[Entrepreneur_ID], E.[Name] AS 'Entrepreneur_Name' 
	--FROM Projects P
	--JOIN Entrepreneurs E ON P.[Entrepreneur_ID] = E.[Entrepreneur_ID]
	--WHERE P.[Created_Date] > @BeginDate
	--AND P.[Created_Date] < @EndDate
	--AND P.[Profile_Public] = 'TRUE'
	--AND E.[Profile_Public] = 'TRUE'

	SELECT TOP 9 P.[Project_ID], P.[Name], P.[Description], P.[Investment_Goal], E.[Entrepreneur_ID], E.[Name] AS 'Entrepreneur_Name' 
	FROM Projects P
	JOIN Entrepreneurs E ON P.[Entrepreneur_ID] = E.[Entrepreneur_ID]

END
GO
