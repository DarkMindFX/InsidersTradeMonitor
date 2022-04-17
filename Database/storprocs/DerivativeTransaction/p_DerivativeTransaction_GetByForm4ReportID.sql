





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DerivativeTransaction_GetByForm4ReportID', 'P') IS NOT NULL
DROP PROC [dbo].[p_DerivativeTransaction_GetByForm4ReportID]
GO

CREATE PROCEDURE [dbo].[p_DerivativeTransaction_GetByForm4ReportID]

	@Form4ReportID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[DerivativeTransaction] c 
				WHERE
					[Form4ReportID] = @Form4ReportID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[DerivativeTransaction] e
		WHERE 
			[Form4ReportID] = @Form4ReportID	

	END
	ELSE
		SET @Found = 0;
END
GO