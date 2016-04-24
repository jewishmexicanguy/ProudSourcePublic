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
-- Description:	This procedure aquires the user Id from an identity id
-- =============================================
CREATE PROCEDURE auth_User_ID_from_Identity_ID 
	-- Add the parameters for the stored procedure here
	@userIdentity NVARCHAR(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT UANU_XREF.[User_ID]
	FROM Users_AspNetUsers_XREF UANU_XREF
	WHERE UANU_XREF.[AspNetUser_ID] = @userIdentity

END
GO
