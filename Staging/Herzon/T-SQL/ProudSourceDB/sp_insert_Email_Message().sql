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
-- Description:	This procedure will insert one Email message into our table for tracking and processing
-- =============================================
ALTER PROCEDURE sp_insert_Email_Message 
	-- Add the parameters for the stored procedure here
	@Gmail_UID INT,
	@Rfc_Message_ID VARCHAR(998),
	@Origin VARCHAR(1024),
	@Destination VARCHAR(MAX),
	@Subject VARCHAR(MAX),
	@Body VARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @date_now DATETIME
	SET @date_now = GETDATE();

	INSERT INTO Email_Messages
	(
		[Gmail_UID],
		[Rfc_Message_ID],
		[Origin_Address],
		[Destination],
		[Inbound],
		[Subject],
		[Body],
		[Received]
	)
	(
		SELECT
		@Gmail_UID,
		@Rfc_Message_ID,
		@Origin,
		@Destination,
		'TRUE',
		@Subject,
		@Body,
		@date_now
	);
	SELECT SCOPE_IDENTITY();
END
GO
