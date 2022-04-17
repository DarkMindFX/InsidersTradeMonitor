


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DerivativeTransaction_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_DerivativeTransaction_GetAll]
GO

CREATE PROCEDURE [dbo].[p_DerivativeTransaction_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[DerivativeTransaction] e
END
GO