


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_EntityType_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_EntityType_GetAll]
GO

CREATE PROCEDURE [dbo].[p_EntityType_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[EntityType] e
END
GO