USE [bookish]
GO

/****** Object:  Table [dbo].[Books]    Script Date: 08/12/2021 11:02:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Books](
	[BookID] [int] NOT NULL,
	[Title] [varchar](255) NOT NULL,
	[Genre] [varchar](255) NOT NULL,
	[NumberOfCopies] [int] NOT NULL,
	[ISBN] [varchar](13) NOT NULL,
	[AuthorID] [int] NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[BookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Books] FOREIGN KEY([AuthorID])
REFERENCES [dbo].[Authors] ([AuthorID])
GO

ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Books]
GO


#