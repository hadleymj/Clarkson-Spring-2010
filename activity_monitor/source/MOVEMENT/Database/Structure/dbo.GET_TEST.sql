IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GET_TEST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GET_TEST]
GO
/****** Object:  StoredProcedure [dbo].[GET_TEST]    Script Date: 05/04/2007 13:16:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Michael Petito
-- Create date: Nov 2 2006
-- Description:	Retrieve all of a test's information.
-- =============================================
CREATE PROCEDURE GET_TEST 
	-- Add the parameters for the stored procedure here
	@testID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--MPP 11/12/06: modified to return new attributes present in the test table
	SELECT test.*, patient_id
	FROM test
		INNER JOIN patient_subject_join ON test.subject_id = patient_subject_join.subject_id
	WHERE test_id = @testID

	-- Error checking, do they already exist?
	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('The test does not exists.', 16, 1);
		RETURN 0
	END
END

GO
GRANT EXECUTE ON [dbo].[GET_TEST] TO [movement_app]
GO
