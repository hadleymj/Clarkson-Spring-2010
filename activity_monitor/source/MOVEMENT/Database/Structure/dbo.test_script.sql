IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[test_script]') AND type in (N'U'))
DROP TABLE [dbo].[test_script]
GO
/****** Object:  Table [dbo].[test_script]    Script Date: 05/04/2007 13:18:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[test_script](
	[script_id] [int] IDENTITY(1,1) NOT NULL,
	[type_id] [int] NOT NULL,
	[version] [int] NOT NULL,
	[timestamp] [smalldatetime] NOT NULL,
	[body] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_test_script] PRIMARY KEY CLUSTERED 
(
	[script_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

/****** Object:  Index [IX_test_script_type_version]    Script Date: 05/04/2007 13:18:27 ******/
CREATE NONCLUSTERED INDEX [IX_test_script_type_version] ON [dbo].[test_script] 
(
	[type_id] ASC,
	[version] DESC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
GRANT SELECT ON [dbo].[test_script] TO [db_researcher]
GO
ALTER TABLE [dbo].[test_script]  WITH CHECK ADD  CONSTRAINT [FK_test_script_test_script_type] FOREIGN KEY([type_id])
REFERENCES [dbo].[test_script_type] ([type_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[test_script] CHECK CONSTRAINT [FK_test_script_test_script_type]
GO
