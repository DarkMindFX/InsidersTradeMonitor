 
GO
/****** Object:  Table [dbo].[Form4Report]    Script Date: 6/18/2022 2:20:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Form4Report](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[IssuerID] [bigint] NOT NULL,
	[ReporterID] [bigint] NOT NULL,
	[ReportID] [nvarchar](50) NOT NULL,
	[IsOfficer] [bit] NOT NULL,
	[IsDirector] [bit] NOT NULL,
	[Is10PctHolder] [bit] NOT NULL,
	[IsOther] [bit] NOT NULL,
	[OtherText] [nvarchar](250) NULL,
	[OfficerTitle] [nvarchar](50) NULL,
	[Date] [date] NOT NULL,
	[DateSubmitted] [date] NOT NULL,
 CONSTRAINT [PK_Form4Report] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Form4Report]  WITH NOCHECK ADD  CONSTRAINT [FK_Form4Report_Issuer] FOREIGN KEY([IssuerID])
REFERENCES [dbo].[Entity] ([ID])
GO
ALTER TABLE [dbo].[Form4Report] CHECK CONSTRAINT [FK_Form4Report_Issuer]
GO
ALTER TABLE [dbo].[Form4Report]  WITH NOCHECK ADD  CONSTRAINT [FK_Form4Report_Reporter] FOREIGN KEY([ReporterID])
REFERENCES [dbo].[Entity] ([ID])
GO
ALTER TABLE [dbo].[Form4Report] CHECK CONSTRAINT [FK_Form4Report_Reporter]
GO
