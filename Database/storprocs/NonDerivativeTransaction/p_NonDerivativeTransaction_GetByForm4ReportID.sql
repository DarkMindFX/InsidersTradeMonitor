





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_NonDerivativeTransaction_GetByForm4ReportID', 'P') IS NOT NULL
DROP PROC [dbo].[p_NonDerivativeTransaction_GetByForm4ReportID]
GO

CREATE PROCEDURE [dbo].[p_NonDerivativeTransaction_GetByForm4ReportID]

	@Form4ReportID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[NonDerivativeTransaction] c 
				WHERE
					[Form4ReportID] = @Form4ReportID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[NonDerivativeTransaction] e
		WHERE 
			[Form4ReportID] = @Form4ReportID	

	END
	ELSE
		SET @Found = 0;
END
GO