


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_NonDerivativeTransaction_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_NonDerivativeTransaction_GetAll]
GO

CREATE PROCEDURE [dbo].[p_NonDerivativeTransaction_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[NonDerivativeTransaction] e
END
GO