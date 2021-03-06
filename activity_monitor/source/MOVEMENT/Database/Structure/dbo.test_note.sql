IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[test_note]') AND type in (N'U'))
DROP TABLE [dbo].[test_note]
GO
/****** Object:  Table [dbo].[test_note]    Script Date: 05/04/2007 13:18:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[test_note](
	[test_id] [int] NOT NULL,
	[timestamp] [smalldatetime] NOT NULL,
	[user_id] [int] NULL,
	[note] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Test Note] PRIMARY KEY CLUSTERED 
(
	[test_id] ASC,
	[timestamp] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
GRANT SELECT ON [dbo].[test_note] TO [db_researcher]
GO
ALTER TABLE [dbo].[test_note]  WITH CHECK ADD  CONSTRAINT [FK_Test Note_Test] FOREIGN KEY([test_id])
REFERENCES [dbo].[test] ([test_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[test_note] CHECK CONSTRAINT [FK_Test Note_Test]
GO
ALTER TABLE [dbo].[test_note]  WITH CHECK ADD  CONSTRAINT [FK_Test Note_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[test_note] CHECK CONSTRAINT [FK_Test Note_User]
GO
