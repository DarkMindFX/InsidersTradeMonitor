
/****** Object:  UserDefinedFunction [dbo].[fn_GetEntityIDByCIK]    Script Date: 4/25/2024 3:36:47 PM ******/
DROP FUNCTION IF EXISTS [dbo].[fn_GetEntityIDByCIK]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetEntityIDByCIK]    Script Date: 4/25/2024 3:36:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fn_GetEntityIDByCIK]
(
	@CIK NVARCHAR(50)
)
RETURNS BIGINT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result BIGINT

	-- Add the T-SQL statements to compute the return value here
	SELECT @Result = ID FROM dbo.Entity WHERE CIK = @CIK

	-- Return the result of the function
	RETURN @Result

END
GO

/****** Object:  UserDefinedFunction [dbo].[fn_GetEntityIDByTradingSymbol]    Script Date: 4/25/2024 3:36:47 PM ******/
DROP FUNCTION IF EXISTS [dbo].[fn_GetEntityIDByTradingSymbol]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetEntityIDByTradingSymbol]    Script Date: 4/25/2024 3:36:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[fn_GetEntityIDByTradingSymbol] 
(
	@TradingSymbol NVARCHAR(50)
)
RETURNS BIGINT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result BIGINT

	-- Add the T-SQL statements to compute the return value here
	SELECT @Result = ID FROM dbo.Entity WHERE TradingSymbol = @TradingSymbol

	-- Return the result of the function
	RETURN @Result

END
GO

/****** Object:  UserDefinedFunction [dbo].[fn_GetOwnershipTypeIDByCode]    Script Date: 4/25/2024 3:36:47 PM ******/
DROP FUNCTION IF EXISTS [dbo].[fn_GetOwnershipTypeIDByCode]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetOwnershipTypeIDByCode]    Script Date: 4/25/2024 3:36:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fn_GetOwnershipTypeIDByCode] 
(
	@Code NVARCHAR(1)
)
RETURNS BIGINT
AS
BEGIN
	
	DECLARE @Result AS BIGINT

	SELECT @Result = ID
	FROM dbo.OwnershipType
	WHERE Code = @Code

	-- Return the result of the function
	RETURN @Result

END
GO

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

/****** Object:  UserDefinedFunction [dbo].[fn_GetTransactionTypeIDByCode]    Script Date: 4/25/2024 3:36:47 PM ******/
DROP FUNCTION IF EXISTS [dbo].[fn_GetTransactionTypeIDByCode]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetTransactionTypeIDByCode]    Script Date: 4/25/2024 3:36:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fn_GetTransactionTypeIDByCode] 
(
	@Code NVARCHAR(1)
)
RETURNS BIGINT
AS
BEGIN
	
	DECLARE @Result AS BIGINT

	SELECT @Result = ID
	FROM dbo.TransactionType
	WHERE Code = @Code

	-- Return the result of the function
	RETURN @Result

END
GO
