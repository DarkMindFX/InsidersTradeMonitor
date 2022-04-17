


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_SecurityType_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_SecurityType_GetAll]
GO

CREATE PROCEDURE [dbo].[p_SecurityType_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[SecurityType] e
END
GO