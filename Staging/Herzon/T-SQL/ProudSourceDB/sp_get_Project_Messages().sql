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
-- Create date: 1/19/2016
-- Description:	This stored procedure will query for messages that need to be generated for User messages
-- =============================================
CREATE PROCEDURE sp_get_Project_Messages 
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT E.[Email_Message_ID], E.[Destination], E.[Origin_Address], E.[Subject], E.[Body], E.[Reference_ID], E.[Reference_Type]
	FROM Email_Messages E
	WHERE E.[Reference_Type] = 5 
		AND E.[Error] IS NULL
		AND E.[Error_Message] IS NULL
		AND E.[Outbound] = 'TRUE'
		AND E.[Inbound] <> 'TRUE'
		AND E.[Sent] IS NULL OR E.[Sent] = 'FALSE'
END
GO
