


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_NonDerivativeTransaction_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_NonDerivativeTransaction_Insert]
GO

CREATE PROCEDURE [dbo].[p_NonDerivativeTransaction_Insert]
			@ID BIGINT,
			@Form4ReportID BIGINT,
			@TitleOfSecurity NVARCHAR(250),
			@TransactionDate DATE,
			@DeemedExecDate DATE,
			@TransactionCodeID BIGINT,
			@EarlyVoluntarilyReport BIT,
			@SharesAmount BIGINT,
			@TransactionTypeID BIGINT,
			@Price DECIMAL(20, 6),
			@AmountFollowingReport BIGINT,
			@OwnershipTypeID BIGINT,
			@NatureOfIndirectOwnership NVARCHAR(250)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[NonDerivativeTransaction]
	SELECT 
		@Form4ReportID,
		@TitleOfSecurity,
		@TransactionDate,
		@DeemedExecDate,
		@TransactionCodeID,
		@EarlyVoluntarilyReport,
		@SharesAmount,
		@TransactionTypeID,
		@Price,
		@AmountFollowingReport,
		@OwnershipTypeID,
		@NatureOfIndirectOwnership
	
	

	SELECT
		e.*
	FROM
		[dbo].[NonDerivativeTransaction] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Form4ReportID IS NOT NULL THEN (CASE WHEN e.[Form4ReportID] = @Form4ReportID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TitleOfSecurity IS NOT NULL THEN (CASE WHEN e.[TitleOfSecurity] = @TitleOfSecurity THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TransactionDate IS NOT NULL THEN (CASE WHEN e.[TransactionDate] = @TransactionDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @DeemedExecDate IS NOT NULL THEN (CASE WHEN e.[DeemedExecDate] = @DeemedExecDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TransactionCodeID IS NOT NULL THEN (CASE WHEN e.[TransactionCodeID] = @TransactionCodeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @EarlyVoluntarilyReport IS NOT NULL THEN (CASE WHEN e.[EarlyVoluntarilyReport] = @EarlyVoluntarilyReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SharesAmount IS NOT NULL THEN (CASE WHEN e.[SharesAmount] = @SharesAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TransactionTypeID IS NOT NULL THEN (CASE WHEN e.[TransactionTypeID] = @TransactionTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Price IS NOT NULL THEN (CASE WHEN e.[Price] = @Price THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @AmountFollowingReport IS NOT NULL THEN (CASE WHEN e.[AmountFollowingReport] = @AmountFollowingReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OwnershipTypeID IS NOT NULL THEN (CASE WHEN e.[OwnershipTypeID] = @OwnershipTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @NatureOfIndirectOwnership IS NOT NULL THEN (CASE WHEN e.[NatureOfIndirectOwnership] = @NatureOfIndirectOwnership THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO