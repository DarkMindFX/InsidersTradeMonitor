 
GO
/****** Object:  Table [dbo].[Entity]    Script Date: 6/18/2022 2:20:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entity](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[EntityTypeID] [bigint] NOT NULL,
	[CIK] [int] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[TradingSymbol] [nvarchar](50) NULL,
	[IsMonitored] [bit] NOT NULL,
 CONSTRAINT [PK_Entity] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Entity] ADD  CONSTRAINT [DF_Entity_IsMonitored]  DEFAULT ((0)) FOR [IsMonitored]
GO
ALTER TABLE [dbo].[Entity]  WITH NOCHECK ADD  CONSTRAINT [FK_Entity_EntityType] FOREIGN KEY([EntityTypeID])
REFERENCES [dbo].[EntityType] ([ID])
GO
ALTER TABLE [dbo].[Entity] CHECK CONSTRAINT [FK_Entity_EntityType]
GO
