IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GET_TEST_SAMPLES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GET_TEST_SAMPLES]
GO
/****** Object:  StoredProcedure [dbo].[GET_TEST_SAMPLES]    Script Date: 05/04/2007 13:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Christopher Pope
-- Create date: Oct 27 2006
-- Description:	Retrieve all the test samples for a given test.
-- =============================================
CREATE PROCEDURE [dbo].[GET_TEST_SAMPLES] 
	-- Add the parameters for the stored procedure here
	@testID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Check that the test exists already.
	IF NOT EXISTS ( SELECT * FROM test WHERE test_id = @testID)
	BEGIN
		RAISERROR('There is no record of the specified test.', 16, 1);
		RETURN
	END

	-- First, grab a count of the samples so the server can step
	-- through them.
	SElECT COUNT(*) FROM raw_test_data WHERE test_id = @testID;

	-- Grab all of the data...it's gonna be a lot!
	SELECT * FROM raw_test_data WHERE test_id = @testID;

END


GO
GRANT EXECUTE ON [dbo].[GET_TEST_SAMPLES] TO [movement_app]
GO
