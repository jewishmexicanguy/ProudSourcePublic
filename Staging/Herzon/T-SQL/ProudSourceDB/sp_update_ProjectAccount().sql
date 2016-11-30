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
-- Create date: 1/31/2016
-- Description:	Procedure updates a project's detials
-- =============================================
ALTER PROCEDURE sp_update_ProjectAccount 
	-- Add the parameters for the stored procedure here
	@Project_ID INT,
	@Name VARCHAR(255),
	@Description VARCHAR(MAX),
	@Profile_Public BIT,
	@Investment_Goal MONEY
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Projects
	SET [Name] = COALESCE(@Name, [Name]),
		[Description] = COALESCE(@Description, [Description]),
		[Profile_Public] = COALESCE(@Profile_Public, [Profile_Public]),
		[Investment_Goal] = COALESCE(@Investment_Goal, [Investment_Goal])
	WHERE [Project_ID] = @Project_ID
END
GO
