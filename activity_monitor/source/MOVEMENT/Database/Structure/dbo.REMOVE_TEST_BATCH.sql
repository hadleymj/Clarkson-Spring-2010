IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[REMOVE_TEST_BATCH]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[REMOVE_TEST_BATCH]
GO
/****** Object:  StoredProcedure [dbo].[REMOVE_TEST_BATCH]    Script Date: 05/04/2007 13:17:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Michael Petito
-- Create date: Nov 14 2006
-- Description:	Remove a single test batch.
-- =============================================
CREATE PROCEDURE [dbo].[REMOVE_TEST_BATCH] 
	@batchID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE FROM test_batch
	WHERE batch_id = @batchID

	-- Error checking, do they already exist?
	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('The test batch does not exist.', 16, 1);
		RETURN 0
	END
END



GO
GRANT EXECUTE ON [dbo].[REMOVE_TEST_BATCH] TO [movement_app]
GO
