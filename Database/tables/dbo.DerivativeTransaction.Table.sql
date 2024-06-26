 
GO
/****** Object:  Table [dbo].[DerivativeTransaction]    Script Date: 6/18/2022 2:20:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DerivativeTransaction](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Form4ReportID] [bigint] NOT NULL,
	[TitleOfDerivative] [nvarchar](250) NOT NULL,
	[ConversionExercisePrice] [decimal](20, 6) NOT NULL,
	[TransactionDate] [date] NOT NULL,
	[TransactionCodeID] [bigint] NOT NULL,
	[EarlyVoluntarilyReport] [bit] NOT NULL,
	[SharesAmount] [bigint] NULL,
	[DerivativeSecurityPrice] [decimal](20, 6) NULL,
	[TransactionTypeID] [bigint] NULL,
	[DateExercisable] [date] NULL,
	[ExpirationDate] [date] NULL,
	[UnderlyingTitle] [nvarchar](250) NOT NULL,
	[UnderlyingSharesAmount] [bigint] NOT NULL,
	[AmountFollowingReport] [bigint] NOT NULL,
	[OwnershipTypeID] [bigint] NOT NULL,
	[NatureOfIndirectOwnership] [nvarchar](250) NULL,
 CONSTRAINT [PK_DerivativeTransaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DerivativeTransaction] ADD  CONSTRAINT [DF_DerivativeTransaction_EarlyVoluntarilyReport]  DEFAULT ((0)) FOR [EarlyVoluntarilyReport]
GO
ALTER TABLE [dbo].[DerivativeTransaction]  WITH NOCHECK ADD  CONSTRAINT [FK_DerivativeTransaction_Form4Report] FOREIGN KEY([Form4ReportID])
REFERENCES [dbo].[Form4Report] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DerivativeTransaction] CHECK CONSTRAINT [FK_DerivativeTransaction_Form4Report]
GO
ALTER TABLE [dbo].[DerivativeTransaction]  WITH NOCHECK ADD  CONSTRAINT [FK_DerivativeTransaction_OwnershipType] FOREIGN KEY([OwnershipTypeID])
REFERENCES [dbo].[OwnershipType] ([ID])
GO
ALTER TABLE [dbo].[DerivativeTransaction] CHECK CONSTRAINT [FK_DerivativeTransaction_OwnershipType]
GO
ALTER TABLE [dbo].[DerivativeTransaction]  WITH NOCHECK ADD  CONSTRAINT [FK_DerivativeTransaction_TransactionCode] FOREIGN KEY([TransactionCodeID])
REFERENCES [dbo].[TransactionCode] ([ID])
GO
ALTER TABLE [dbo].[DerivativeTransaction] CHECK CONSTRAINT [FK_DerivativeTransaction_TransactionCode]
GO
ALTER TABLE [dbo].[DerivativeTransaction]  WITH NOCHECK ADD  CONSTRAINT [FK_DerivativeTransaction_TransactionType] FOREIGN KEY([TransactionTypeID])
REFERENCES [dbo].[TransactionType] ([ID])
GO
ALTER TABLE [dbo].[DerivativeTransaction] CHECK CONSTRAINT [FK_DerivativeTransaction_TransactionType]
GO
