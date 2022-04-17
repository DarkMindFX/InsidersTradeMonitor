


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Form4Report_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_Form4Report_GetAll]
GO

CREATE PROCEDURE [dbo].[p_Form4Report_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Form4Report] e
END
GO