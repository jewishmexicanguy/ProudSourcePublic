USE [ProudSourceDB]
GO
/****** Object:  StoredProcedure [dbo].[Investor_Update]    Script Date: 1/22/2016 2:12:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Herzon Flores
-- Create date: 1/18/2016
-- Description:	Updates Investor records with data passed in through the client
-- =============================================
ALTER PROCEDURE [dbo].[Investor_Update] 
	-- Add the parameters for the stored procedure here
	@Investor_ID INT,
	@Name VARCHAR(255),  
	@profile_public BIT,
	@profile_picture VARBINARY(MAX)
AS
BEGIN
    -- Insert statements for procedure here
	BEGIN TRY

		BEGIN TRAN Update_Investor_Entry

			-- Declare variables for potential use later
			DECLARE @new_image_id INT;
			DECLARE @image_id_pointer INT;

			-- Using COALESCE we can update with new values when they exist or keep the value already contained if the accepted parameter is null.
			UPDATE Investors
			SET [Name] = COALESCE(@Name, [Name]),
				[Profile_Public] = COALESCE(@profile_public, [Profile_Public])
			WHERE [Investor_ID] = @Investor_ID

			-- If the profile picture paramter is not empty then find if this account has pictures or not.
			IF @profile_picture IS NOT NULL
			BEGIN

				-- If this account does not have any images associated with it.
				IF (SELECT COUNT(*) FROM Images_Investors_XREF II WHERE II.[Investor_ID] = @Investor_ID) < 1
				BEGIN

					INSERT INTO Images
					(
						[Binary_Image],
						[Created_DateTime]
					)
					(
						SELECT
						@profile_picture,
						GETDATE()
					);

					-- This allows us to get the ID of a record that was just created in this session.
					SET @new_image_id = SCOPE_IDENTITY();

					INSERT INTO Images_Investors_XREF
					(
						[Investor_ID],
						[Image_ID]
					)
					(
						SELECT
						@Investor_ID,
						@new_image_id
					);

				END

				-- If this account does have imagges associated with it update the record that alread exists
				IF (SELECT COUNT(*) FROM Images_Investors_XREF II WHERE II.[Investor_ID] = @Investor_ID) > 0
				BEGIN

					SET @image_id_pointer = (SELECT TOP 1 [Image_ID] FROM Images_Investors_XREF WHERE [Investor_ID] = @Investor_ID ORDER BY [XREF_ID] ASC)
					UPDATE Images
					SET Images.[Binary_Image] = @profile_picture,
						Images.[Created_DateTime] = GETDATE()
					WHERE Images.[Image_ID] = @image_id_pointer

				END

			END

		COMMIT TRAN Update_Investor_Entry
		RETURN 0;

	END TRY

	BEGIN CATCH

		ROLLBACK TRAN

		RAISERROR (15004, -1, -1, 'Investor_Update');

	END CATCH

END
