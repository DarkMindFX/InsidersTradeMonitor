


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRun_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRun_GetAll]
GO

CREATE PROCEDURE [dbo].[p_ImportRun_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[ImportRun] e
END
GO