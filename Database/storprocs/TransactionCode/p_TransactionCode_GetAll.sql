


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_TransactionCode_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_TransactionCode_GetAll]
GO

CREATE PROCEDURE [dbo].[p_TransactionCode_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[TransactionCode] e
END
GO