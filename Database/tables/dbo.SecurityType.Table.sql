USE [InsidersTradeMonitor]
GO
/****** Object:  Table [dbo].[SecurityType]    Script Date: 4/17/2022 1:11:19 PM ******/
DROP TABLE IF EXISTS [dbo].[SecurityType]
GO
/****** Object:  Table [dbo].[SecurityType]    Script Date: 4/17/2022 1:11:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SecurityType](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[SecurityTypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SecurityType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
