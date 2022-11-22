USE [InsidersTradeMonitor]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetOwnershipTypeIDByCode]    Script Date: 11/22/2022 10:33:18 AM ******/
DROP FUNCTION [dbo].[fn_GetOwnershipTypeIDByCode]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetOwnershipTypeIDByCode]    Script Date: 11/22/2022 10:33:18 AM ******/
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
