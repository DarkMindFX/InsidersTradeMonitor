
/****** Object:  StoredProcedure [dbo].[p_Util_TransactionCode_Populate]    Script Date: 4/25/2024 4:03:59 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[p_Util_TransactionCode_Populate]
GO
/****** Object:  StoredProcedure [dbo].[p_Util_TransactionCode_Populate]    Script Date: 4/25/2024 4:03:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[p_Util_TransactionCode_Populate]

AS
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @tblTemp AS TABLE (
		[ID] bigint,
		[Code] nvarchar(10),
		[Description] nvarchar(250)
	)

	INSERT INTO @tblTemp
	SELECT 1, 'P', 'Open market or private purchase of non-derivative or derivative security' UNION
	SELECT 2, 'S', 'Open market or private sale of non-derivative or derivative security' UNION
	SELECT 3, 'V', 'Transaction voluntarily reported earlier than required' UNION
	SELECT 4, 'A', 'Grant, award or other acquisition pursuant to Rule 16b-3(d) ' UNION
	SELECT 5, 'D', 'Disposition to the issuer of issuer equity securities pursuant to Rule 16b-3(e) ' UNION
	SELECT 6, 'F', 'Payment of exercise price or tax liability by delivering or withholding securities incident to the receipt, exercise or vesting of a security issued in accordance with Rule 16b-3' UNION
	SELECT 7, 'I', 'Discretionary transaction in accordance with Rule 16b-3(f) resulting in acquisition or disposition of issuer securities' UNION
	SELECT 8, 'M', 'Exercise or conversion of derivative security exempted pursuant to Rule 16b-3 ' UNION
	SELECT 9, 'C', 'Conversion of derivative security' UNION
	SELECT 10, 'E', 'Expiration of short derivative position' UNION
	SELECT 11, 'H', 'Expiration (or cancellation) of long derivative position with value received' UNION
	SELECT 12, 'O', 'Exercise of out-of-the-money derivative security' UNION
	SELECT 13, 'X', 'Exercise of in-the-money or at-the-money derivative security' UNION
	SELECT 14, 'G', 'Bona fide gift' UNION
	SELECT 15, 'L', 'Small acquisition under Rule 16a-6' UNION
	SELECT 16, 'W', 'Acquisition or disposition by will or the laws of descent and distribution' UNION
	SELECT 17, 'Z', 'Deposit into or withdrawal from voting trust ' UNION
	SELECT 18, 'J', 'Other acquisition or disposition (describe transaction)' UNION
	SELECT 19, 'K', 'Transaction in equity swap or instrument with similar characteristics' UNION
	SELECT 20, 'U', 'Disposition pursuant to a tender of shares in a change of control transaction ' 


	SET IDENTITY_INSERT dbo.TransactionCode ON; 

	MERGE dbo.TransactionCode as t 
	USING @tblTemp as s 
	ON(t.ID = s.ID)
	WHEN MATCHED 
	THEN UPDATE SET t.Code = s.Code, t.Description = s.Description
	WHEN NOT MATCHED BY TARGET
	THEN INSERT (ID, Code, Description) VALUES (s.ID, s.Code, s.Description)
	WHEN NOT MATCHED BY SOURCE 
	THEN DELETE 
	;

	SET IDENTITY_INSERT dbo.TransactionCode OFF; 
END
GO
