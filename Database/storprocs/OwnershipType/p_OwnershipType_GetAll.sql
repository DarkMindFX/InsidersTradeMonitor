


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OwnershipType_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_OwnershipType_GetAll]
GO

CREATE PROCEDURE [dbo].[p_OwnershipType_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[OwnershipType] e
END
GO