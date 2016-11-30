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
-- Description:	Gets info to display on user edit page
-- =============================================
CREATE PROCEDURE sp_get_UserEditDetails 
	-- Add the parameters for the stored procedure here
	@User_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Email]
	FROM Users 
	WHERE [User_ID] = @User_ID

	SELECT I.[Image_ID], I.[Binary_Image]
	FROM Images I
	JOIN Images_Users_XREF IU_XREF ON I.[Image_ID] = IU_XREF.[Image_ID]
	WHERE IU_XREF.[User_ID] = @User_ID
END
GO
