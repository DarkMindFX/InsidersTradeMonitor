
/****** Object:  UserDefinedFunction [dbo].[fn_GetTransactionCodeIDByCode]    Script Date: 4/25/2024 3:36:47 PM ******/
DROP FUNCTION IF EXISTS [dbo].[fn_GetTransactionCodeIDByCode]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetTransactionCodeIDByCode]    Script Date: 4/25/2024 3:36:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fn_GetTransactionCodeIDByCode] 
(
	@Code NVARCHAR(1)
)
RETURNS BIGINT
AS
BEGIN
	
	DECLARE @Result AS BIGINT

	SELECT @Result = ID
	FROM dbo.TransactionCode
	WHERE Code = @Code

	-- Return the result of the function
	RETURN @Result

END
GO
