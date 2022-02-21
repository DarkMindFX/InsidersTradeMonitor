USE [InsidersTradeMonitor]
GO
/****** Object:  Table [dbo].[OwnershipType]    Script Date: 2/22/2022 12:18:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OwnershipType](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nchar](1) NOT NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_OwnershipType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
