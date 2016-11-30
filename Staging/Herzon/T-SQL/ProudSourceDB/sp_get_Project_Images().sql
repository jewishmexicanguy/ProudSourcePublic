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
-- Description:	Stored Procedure gets all of the images related to a project account
-- =============================================
CREATE PROCEDURE sp_get_Project_Images 
	-- Add the parameters for the stored procedure here
	@Project_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT P.[Project_ID], I.[Image_ID], I.[Binary_Image] 
	FROM Projects P
	JOIN Images_Projects_XREF IP_XREF ON P.[Project_ID] = IP_XREF.[Project_ID]
	JOIN Images I ON IP_XREF.[Image_ID] = I.[Image_ID]
END
GO
