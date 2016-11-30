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
-- Create date: 2/3/2016
-- Description:	Procedure inserts a message into our profile_messages table and into the appropriate XREF table to be presented to the account owner adequeatly
-- =============================================
CREATE PROCEDURE sp_message_ProfileAccount 
	-- Add the parameters for the stored procedure here
	@Reciving_ID INT,
	@Reciving_Type INT,
	@Messenger_ID INT,
	@Reference_Type INT,
	@Message VARCHAR(5000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY

		BEGIN TRAN Insert_ProfileAccount_Message

			DECLARE @new_message_id INT;

			INSERT INTO Profile_Messages
			(
				[Message_Text],
				[Messenger_ID],
				[Messenger_Type],
				[DateTime_Created]
			)
			(
				SELECT
				@Message,
				@Messenger_ID,
				@Reference_Type,
				GETDATE()
			);

			SET @new_message_id = SCOPE_IDENTITY();

			-- Case for message going to a project account
			IF (@Reciving_Type = 5)
			BEGIN
				INSERT INTO Profile_Messages_Projects_XREF
				(
					[Project_ID],
					[Profile_Message_ID]
				)
				(
					SELECT
					@Reciving_ID,
					@new_message_id
				);
			END

			-- case for a message going to an entrepreneur account
			ELSE IF (@Reciving_Type = 4)
			BEGIN
				INSERT INTO Profile_Messages_Entrepreneurs_XREF
				(
					[Entrepreneur_ID],
					[Profile_Message_ID]
				)
				(
					SELECT
					@Reciving_ID,
					@new_message_id
				);
			END

			-- case for a message going to an investor account
			ELSE IF (@Reciving_Type = 3)
			BEGIN
				INSERT INTO Profile_Messages_Investors_XREF
				(
					[Investor_ID],
					[Profile_Message_ID]
				)
				(
					SELECT
					@Reciving_ID,
					@new_message_id
				);
			END

		COMMIT TRAN Insert_ProfileAccount_Message

		RETURN 0;

	END TRY
	BEGIN CATCH

		ROLLBACK TRAN

		RAISERROR (150030, -1, -1, 'sp_message_ProfileAccount');

	END CATCH
END
GO
