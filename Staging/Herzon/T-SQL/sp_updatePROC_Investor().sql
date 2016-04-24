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
-- Description:	Procedure that updates PROCs for an investor client.
-- =============================================
ALTER PROCEDURE sp_updatePROC_Investor 
	-- Add the parameters for the stored procedure here
	@PROC_ID INT,
	@Performance_BeginDate DATETIME,
	@Performance_EndDate DATETIME,
	@Investor_Accepted INT,
	@Revenue_Percentage DECIMAL(18,0),
	@Investment_Ammount MONEY
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- UPdate PROC record for investor where Proc_ID = @PROC_ID and where Active is false.
	UPDATE Procs
	SET [Performance_BeginDate] = COALESCE(@Performance_BeginDate, [Performance_BeginDate]),
		[Performance_EndDate] = COALESCE(@Performance_EndDate, [Performance_EndDate]),
		[Investor_Accepted] = COALESCE(@Investor_Accepted, [Investor_Accepted]),
		[Revenue_Percentage] = COALESCE(@Revenue_Percentage, [Revenue_Percentage]),
		[Investment_Ammount] = COALESCE(@Investment_Ammount, [Investment_Ammount])
	WHERE [Proc_ID] = @PROC_ID AND [Active] = 'FALSE'
END
GO
