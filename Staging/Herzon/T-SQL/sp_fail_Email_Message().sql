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
-- Description:	Will mark an individual email message with the error bit set to true and with an error message
-- =============================================
CREATE PROCEDURE sp_fail_Email_Message 
	-- Add the parameters for the stored procedure here
	@Email_Message_ID INT,
	@Failure_Message VARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Email_Messages
	SET [Error] = 'TRUE',
		[Error_Message] = @Failure_Message,
		[Processed_DateTime] = GETDATE()
	WHERE [Email_Message_ID] = @Email_Message_ID 
END
GO
