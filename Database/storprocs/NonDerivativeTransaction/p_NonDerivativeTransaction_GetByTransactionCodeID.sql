





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_NonDerivativeTransaction_GetByTransactionCodeID', 'P') IS NOT NULL
DROP PROC [dbo].[p_NonDerivativeTransaction_GetByTransactionCodeID]
GO

CREATE PROCEDURE [dbo].[p_NonDerivativeTransaction_GetByTransactionCodeID]

	@TransactionCodeID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[NonDerivativeTransaction] c 
				WHERE
					[TransactionCodeID] = @TransactionCodeID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[NonDerivativeTransaction] e
		WHERE 
			[TransactionCodeID] = @TransactionCodeID	

	END
	ELSE
		SET @Found = 0;
END
GO