IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ADD_TEST_SAMPLE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ADD_TEST_SAMPLE]
GO
/****** Object:  StoredProcedure [dbo].[ADD_TEST_SAMPLE]    Script Date: 05/04/2007 13:16:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Christopher Pope
-- Create date: Oct 31 2006
-- Description:	Insert a test sample for a given test.
-- =============================================
CREATE PROCEDURE [dbo].[ADD_TEST_SAMPLE] 
	-- Add the parameters for the stored procedure here
	@testID int, 
	@x int,
	@y int,
	@p int,
	@t int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT * FROM test WHERE test_id = @testID)
	BEGIN
		RAISERROR('The specified test does not exist.', 16, 1);
	END

	INSERT INTO raw_test_data(test_id, x, y, pressure, time)
	VALUES (@testID, @x, @y, @p, @t);

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('An error occurred while attempting to add a test sample for the test that appears to exists.' ,16, 1);
	END
END



GO
GRANT EXECUTE ON [dbo].[ADD_TEST_SAMPLE] TO [movement_app]
GO
