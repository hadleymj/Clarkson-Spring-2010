IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GET_TEST_SCRIPT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GET_TEST_SCRIPT]
GO
/****** Object:  StoredProcedure [dbo].[GET_TEST_SCRIPT]    Script Date: 05/04/2007 13:16:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Christopher Pope
-- Create date: Oct 27 2006
-- Description:	Retrieve the script for a specified test id.
-- =============================================
CREATE PROCEDURE [dbo].[GET_TEST_SCRIPT] 
	-- Add the parameters for the stored procedure here
	@scriptID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Find the test script
	SELECT * FROM test_script WHERE script_id = @scriptID;
	
	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('The specified test script does not exist.', 16, 1);
	END
	
END


GO
GRANT EXECUTE ON [dbo].[GET_TEST_SCRIPT] TO [movement_app]
GO
