USE [InsidersTradeMonitor]
GO
/****** Object:  Table [dbo].[Entity]    Script Date: 2/22/2022 12:18:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entity](
	[ID] [bigint] NOT NULL,
	[EntityTypeID] [bigint] NOT NULL,
	[CIK] [nvarchar](250) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[TradingSymbol] [nvarchar](50) NULL,
 CONSTRAINT [PK_Entity] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Entity]  WITH CHECK ADD  CONSTRAINT [FK_Entity_EntityType] FOREIGN KEY([EntityTypeID])
REFERENCES [dbo].[EntityType] ([ID])
GO
ALTER TABLE [dbo].[Entity] CHECK CONSTRAINT [FK_Entity_EntityType]
GO
