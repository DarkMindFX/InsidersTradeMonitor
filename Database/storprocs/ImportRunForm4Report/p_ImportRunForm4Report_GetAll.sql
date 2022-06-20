


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRunForm4Report_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRunForm4Report_GetAll]
GO

CREATE PROCEDURE [dbo].[p_ImportRunForm4Report_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[ImportRunForm4Report] e
END
GO