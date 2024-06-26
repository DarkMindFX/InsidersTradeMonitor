 
GO
/****** Object:  Table [dbo].[NonDerivativeTransaction]    Script Date: 6/18/2022 2:20:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NonDerivativeTransaction](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Form4ReportID] [bigint] NOT NULL,
	[TitleOfSecurity] [nvarchar](250) NOT NULL,
	[TransactionDate] [date] NOT NULL,
	[DeemedExecDate] [date] NULL,
	[TransactionCodeID] [bigint] NULL,
	[EarlyVoluntarilyReport] [bit] NOT NULL,
	[SharesAmount] [bigint] NULL,
	[TransactionTypeID] [bigint] NULL,
	[Price] [decimal](20, 6) NOT NULL,
	[AmountFollowingReport] [bigint] NOT NULL,
	[OwnershipTypeID] [bigint] NULL,
	[NatureOfIndirectOwnership] [nvarchar](250) NULL,
 CONSTRAINT [PK_NonDerivativeTransaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[NonDerivativeTransaction] ADD  CONSTRAINT [DF_NonDerivativeTransaction_EarlyVoluntarilyRepor]  DEFAULT ((0)) FOR [EarlyVoluntarilyReport]
GO
ALTER TABLE [dbo].[NonDerivativeTransaction]  WITH NOCHECK ADD  CONSTRAINT [FK_NonDerivativeTransaction_Form4Report] FOREIGN KEY([Form4ReportID])
REFERENCES [dbo].[Form4Report] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NonDerivativeTransaction] CHECK CONSTRAINT [FK_NonDerivativeTransaction_Form4Report]
GO
ALTER TABLE [dbo].[NonDerivativeTransaction]  WITH NOCHECK ADD  CONSTRAINT [FK_NonDerivativeTransaction_OwnershipType] FOREIGN KEY([OwnershipTypeID])
REFERENCES [dbo].[OwnershipType] ([ID])
GO
ALTER TABLE [dbo].[NonDerivativeTransaction] CHECK CONSTRAINT [FK_NonDerivativeTransaction_OwnershipType]
GO
ALTER TABLE [dbo].[NonDerivativeTransaction]  WITH NOCHECK ADD  CONSTRAINT [FK_NonDerivativeTransaction_TransactionCode] FOREIGN KEY([TransactionCodeID])
REFERENCES [dbo].[TransactionCode] ([ID])
GO
ALTER TABLE [dbo].[NonDerivativeTransaction] CHECK CONSTRAINT [FK_NonDerivativeTransaction_TransactionCode]
GO
ALTER TABLE [dbo].[NonDerivativeTransaction]  WITH NOCHECK ADD  CONSTRAINT [FK_NonDerivativeTransaction_TransactionType] FOREIGN KEY([TransactionTypeID])
REFERENCES [dbo].[TransactionType] ([ID])
GO
ALTER TABLE [dbo].[NonDerivativeTransaction] CHECK CONSTRAINT [FK_NonDerivativeTransaction_TransactionType]
GO
