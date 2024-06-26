 
GO
/****** Object:  Table [dbo].[ImportRunForm4Report]    Script Date: 6/20/2022 1:34:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImportRunForm4Report](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ImportRunID] [bigint] NOT NULL,
	[Form4ReportID] [bigint] NOT NULL,
	[TimeStarted] [datetime] NOT NULL,
	[TimeCompleted] [datetime] NULL,
 CONSTRAINT [PK_ImportRunForm4Report] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ImportRunForm4Report]  WITH NOCHECK ADD  CONSTRAINT [FK_ImportRunForm4Report_Form4Report] FOREIGN KEY([Form4ReportID])
REFERENCES [dbo].[Form4Report] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ImportRunForm4Report] CHECK CONSTRAINT [FK_ImportRunForm4Report_Form4Report]
GO
ALTER TABLE [dbo].[ImportRunForm4Report]  WITH NOCHECK ADD  CONSTRAINT [FK_ImportRunForm4Report_ImportRun] FOREIGN KEY([ImportRunID])
REFERENCES [dbo].[ImportRun] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ImportRunForm4Report] CHECK CONSTRAINT [FK_ImportRunForm4Report_ImportRun]
GO
