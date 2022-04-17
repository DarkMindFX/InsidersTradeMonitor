


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DerivativeTransaction_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_DerivativeTransaction_Update]
GO

CREATE PROCEDURE [dbo].[p_DerivativeTransaction_Update]
			@ID BIGINT,
			@Form4ReportID BIGINT,
			@TitleOfDerivative NVARCHAR(250),
			@ConversionExercisePrice DECIMAL(20, 6),
			@TransactionDate DATE,
			@TransactionCodeID BIGINT,
			@EarlyVoluntarilyReport BIT,
			@SharesAmount BIGINT,
			@DerivativeSecurityPrice DECIMAL(20, 6),
			@TransactionTypeID BIGINT,
			@DateExercisable DATE,
			@ExpirationDate DATE,
			@UnderlyingTitle NVARCHAR(250),
			@UnderlyingSharesAmount BIGINT,
			@AmountFollowingReport BIGINT,
			@OwnershipTypeID BIGINT,
			@NatureOfIndirectOwnership NVARCHAR(250)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[DerivativeTransaction]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[DerivativeTransaction]
		SET
									[Form4ReportID] = IIF( @Form4ReportID IS NOT NULL, @Form4ReportID, [Form4ReportID] ) ,
									[TitleOfDerivative] = IIF( @TitleOfDerivative IS NOT NULL, @TitleOfDerivative, [TitleOfDerivative] ) ,
									[ConversionExercisePrice] = IIF( @ConversionExercisePrice IS NOT NULL, @ConversionExercisePrice, [ConversionExercisePrice] ) ,
									[TransactionDate] = IIF( @TransactionDate IS NOT NULL, @TransactionDate, [TransactionDate] ) ,
									[TransactionCodeID] = IIF( @TransactionCodeID IS NOT NULL, @TransactionCodeID, [TransactionCodeID] ) ,
									[EarlyVoluntarilyReport] = IIF( @EarlyVoluntarilyReport IS NOT NULL, @EarlyVoluntarilyReport, [EarlyVoluntarilyReport] ) ,
									[SharesAmount] = IIF( @SharesAmount IS NOT NULL, @SharesAmount, [SharesAmount] ) ,
									[DerivativeSecurityPrice] = IIF( @DerivativeSecurityPrice IS NOT NULL, @DerivativeSecurityPrice, [DerivativeSecurityPrice] ) ,
									[TransactionTypeID] = IIF( @TransactionTypeID IS NOT NULL, @TransactionTypeID, [TransactionTypeID] ) ,
									[DateExercisable] = IIF( @DateExercisable IS NOT NULL, @DateExercisable, [DateExercisable] ) ,
									[ExpirationDate] = IIF( @ExpirationDate IS NOT NULL, @ExpirationDate, [ExpirationDate] ) ,
									[UnderlyingTitle] = IIF( @UnderlyingTitle IS NOT NULL, @UnderlyingTitle, [UnderlyingTitle] ) ,
									[UnderlyingSharesAmount] = IIF( @UnderlyingSharesAmount IS NOT NULL, @UnderlyingSharesAmount, [UnderlyingSharesAmount] ) ,
									[AmountFollowingReport] = IIF( @AmountFollowingReport IS NOT NULL, @AmountFollowingReport, [AmountFollowingReport] ) ,
									[OwnershipTypeID] = IIF( @OwnershipTypeID IS NOT NULL, @OwnershipTypeID, [OwnershipTypeID] ) ,
									[NatureOfIndirectOwnership] = IIF( @NatureOfIndirectOwnership IS NOT NULL, @NatureOfIndirectOwnership, [NatureOfIndirectOwnership] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'DerivativeTransaction was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[DerivativeTransaction] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Form4ReportID IS NOT NULL THEN (CASE WHEN e.[Form4ReportID] = @Form4ReportID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TitleOfDerivative IS NOT NULL THEN (CASE WHEN e.[TitleOfDerivative] = @TitleOfDerivative THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ConversionExercisePrice IS NOT NULL THEN (CASE WHEN e.[ConversionExercisePrice] = @ConversionExercisePrice THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TransactionDate IS NOT NULL THEN (CASE WHEN e.[TransactionDate] = @TransactionDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TransactionCodeID IS NOT NULL THEN (CASE WHEN e.[TransactionCodeID] = @TransactionCodeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @EarlyVoluntarilyReport IS NOT NULL THEN (CASE WHEN e.[EarlyVoluntarilyReport] = @EarlyVoluntarilyReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SharesAmount IS NOT NULL THEN (CASE WHEN e.[SharesAmount] = @SharesAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @DerivativeSecurityPrice IS NOT NULL THEN (CASE WHEN e.[DerivativeSecurityPrice] = @DerivativeSecurityPrice THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TransactionTypeID IS NOT NULL THEN (CASE WHEN e.[TransactionTypeID] = @TransactionTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @DateExercisable IS NOT NULL THEN (CASE WHEN e.[DateExercisable] = @DateExercisable THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ExpirationDate IS NOT NULL THEN (CASE WHEN e.[ExpirationDate] = @ExpirationDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UnderlyingTitle IS NOT NULL THEN (CASE WHEN e.[UnderlyingTitle] = @UnderlyingTitle THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UnderlyingSharesAmount IS NOT NULL THEN (CASE WHEN e.[UnderlyingSharesAmount] = @UnderlyingSharesAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @AmountFollowingReport IS NOT NULL THEN (CASE WHEN e.[AmountFollowingReport] = @AmountFollowingReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OwnershipTypeID IS NOT NULL THEN (CASE WHEN e.[OwnershipTypeID] = @OwnershipTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @NatureOfIndirectOwnership IS NOT NULL THEN (CASE WHEN e.[NatureOfIndirectOwnership] = @NatureOfIndirectOwnership THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO