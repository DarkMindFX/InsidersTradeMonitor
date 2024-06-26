 
GO
/****** Object:  Table [dbo].[ImportRun]    Script Date: 6/20/2022 1:34:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImportRun](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[TimeStart] [datetime] NOT NULL,
	[TimeEnd] [datetime] NULL,
	[RequestJson] [nvarchar](1000) NOT NULL,
	[StateID] [bigint] NOT NULL,
 CONSTRAINT [PK_ImportRun] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ImportRun]  WITH NOCHECK ADD  CONSTRAINT [FK_ImportRun_ImportRunState] FOREIGN KEY([StateID])
REFERENCES [dbo].[ImportRunState] ([ID])
GO
ALTER TABLE [dbo].[ImportRun] CHECK CONSTRAINT [FK_ImportRun_ImportRunState]
GO
