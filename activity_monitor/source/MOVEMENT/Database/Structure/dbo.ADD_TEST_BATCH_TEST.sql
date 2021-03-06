IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ADD_TEST_BATCH_TEST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ADD_TEST_BATCH_TEST]
GO
/****** Object:  StoredProcedure [dbo].[ADD_TEST_BATCH_TEST]    Script Date: 05/04/2007 13:16:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Michael Petito
-- Create date: Nov 14 2006
-- Description:	Adds a test to a test batch.
-- =============================================
CREATE PROCEDURE [dbo].[ADD_TEST_BATCH_TEST] 
	@batchID int,
	@sequence tinyint,
	@scriptID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO test_batch_tests (batch_id, sequence, script_id)
	VALUES (@batchID, @sequence, @scriptID)
END


GO
GRANT EXECUTE ON [dbo].[ADD_TEST_BATCH_TEST] TO [movement_app]
GO
