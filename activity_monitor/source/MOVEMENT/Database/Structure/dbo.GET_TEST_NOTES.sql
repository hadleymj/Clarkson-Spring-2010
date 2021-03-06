IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GET_TEST_NOTES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GET_TEST_NOTES]
GO
/****** Object:  StoredProcedure [dbo].[GET_TEST_NOTES]    Script Date: 05/04/2007 13:16:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Christopher Pope
-- Create date: Oct 31 2006
-- Description:	Retrieve all notes for a given test.
-- =============================================
CREATE PROCEDURE GET_TEST_NOTES 
	-- Add the parameters for the stored procedure here
	@testID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Error checking, do they already exist?
	IF NOT EXISTS ( SELECT * FROM test WHERE test_id = @testID)
	BEGIN
		RAISERROR('The specified test does not exist.', 16, 1);
		RETURN 0
	END

	-- TODO: Should we retrieve a note count first?
	-- SELECT COUNT(*) FROM test_note WHERE test_id = @testID;

	-- TODO: Do we want to resolve the user_id here?
	SELECT * FROM test_note WHERE test_id = @testID;
END

GO
GRANT EXECUTE ON [dbo].[GET_TEST_NOTES] TO [movement_app]
GO
