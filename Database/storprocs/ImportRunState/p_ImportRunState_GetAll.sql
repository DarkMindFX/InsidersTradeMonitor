


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRunState_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRunState_GetAll]
GO

CREATE PROCEDURE [dbo].[p_ImportRunState_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[ImportRunState] e
END
GO