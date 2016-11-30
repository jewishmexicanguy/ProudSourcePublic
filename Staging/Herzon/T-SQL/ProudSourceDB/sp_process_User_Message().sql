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
-- Description:	This procedure will handle updating records where a message has been sent and processed
-- =============================================
CREATE PROCEDURE sp_process_User_Message 
	-- Add the parameters for the stored procedure here
	@Email_Message_ID INT,
	@Processed BIT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @date_now DATETIME;
	SET @Date_now = GETDATE();
	UPDATE Email_Messages
	SET [Sent] = @Processed,
		[Sent_DateTime] = @Date_now,
		[Processed] = @Processed,
		[Processed_DateTime] = @Date_now
	WHERE [Email_Message_ID] = @Email_Message_ID

END
GO
