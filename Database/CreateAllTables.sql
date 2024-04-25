 
GO
/****** Object:  Table [dbo].[TransactionCode]    Script Date: 6/18/2022 2:20:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionCode](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Description] [nvarchar](250) NULL,
 CONSTRAINT [PK_TransactionCode] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
 
GO
/****** Object:  Table [dbo].[TransactionType]    Script Date: 6/18/2022 2:20:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionType](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Description] [nvarchar](250) NULL,
 CONSTRAINT [PK_TransactionType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
 
GO
/****** Object:  Table [dbo].[OwnershipType]    Script Date: 6/18/2022 2:20:56 PM ******/
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
 
GO
/****** Object:  Table [dbo].[EntityType]    Script Date: 6/18/2022 2:20:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntityType](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_EntityType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
 
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
 
GO
/****** Object:  Table [dbo].[ImportRunState]    Script Date: 6/20/2022 1:34:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImportRunState](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ImportRunState] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
 
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
 
GO
/****** Object:  Table [dbo].[User]    Script Date: 6/18/2022 2:20:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](250) NOT NULL,
	[PwdHash] [nvarchar](250) NOT NULL,
	[Salt] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[FriendlyName] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[User]  WITH NOCHECK ADD  CONSTRAINT [FK_User_User] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_User]
GO
