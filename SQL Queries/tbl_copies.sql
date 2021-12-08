USE [bookish]
GO

/****** Object:  Table [dbo].[Copies]    Script Date: 08/12/2021 11:02:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Copies](
	[CopyID] [int] NOT NULL,
	[BookID] [int] NOT NULL,
	[Available] [bit] NOT NULL,
	[DueDate] [date] NULL,
	[BorrowerID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CopyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Copies]  WITH CHECK ADD FOREIGN KEY([BookID])
REFERENCES [dbo].[Books] ([BookID])
GO

ALTER TABLE [dbo].[Copies]  WITH CHECK ADD FOREIGN KEY([BorrowerID])
REFERENCES [dbo].[Users] ([UserID])
GO


