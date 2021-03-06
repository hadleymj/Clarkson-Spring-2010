IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GET_LAST_TEST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GET_LAST_TEST]
GO
/****** Object:  StoredProcedure [dbo].[GET_LAST_TEST]    Script Date: 05/04/2007 13:16:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Christopher Pope
-- Create date: Oct 27 2006
-- Description:	Retrieves the last N tests for a given subject.
--				Tests are retreived in the order they were recorded,
--				beginning with the most recent test administered.
-- =============================================
CREATE PROCEDURE [dbo].[GET_LAST_TEST] 
	-- Add the parameters for the stored procedure here
	@patientID int, --MPP 11/12/06: modified to use patientID
	@numberOfTests int = 1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Error checking, do they already exist?
	IF NOT EXISTS (SELECT * FROM patient WHERE patient_id = @patientID)
	BEGIN
		RAISERROR('The specified subject does not exist.', 16, 1);
		RETURN
	END

	--MPP 11/12/06: modified to use patientID
	SET ROWCOUNT @numberOfTests
	SELECT test.* 
	FROM test
		INNER JOIN patient_subject ON test.subject_id = patient_subject.subject_id
	WHERE patient_subject.patient_id = @patientID

END


GO
GRANT EXECUTE ON [dbo].[GET_LAST_TEST] TO [movement_app]
GO
