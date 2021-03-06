IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[metric]') AND type in (N'U'))
DROP TABLE [dbo].[metric]
GO
/****** Object:  Table [dbo].[metric]    Script Date: 05/04/2007 13:17:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[metric](
	[metric_id] [int] NOT NULL,
	[name] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_metric] PRIMARY KEY CLUSTERED 
(
	[metric_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 100) ON [PRIMARY]
) ON [PRIMARY]

GO
GRANT SELECT ON [dbo].[metric] TO [db_researcher]
GO
