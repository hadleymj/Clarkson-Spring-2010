IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GET_PATIENT_NOTES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GET_PATIENT_NOTES]
GO
/****** Object:  StoredProcedure [dbo].[GET_PATIENT_NOTES]    Script Date: 05/04/2007 13:16:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Christopher Pope
-- Create date: Oct 31 2006
-- Description:	Retrieves all notes associated with a patient.
-- =============================================
CREATE PROCEDURE GET_PATIENT_NOTES 
	-- Add the parameters for the stored procedure here
	@patientID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Error checking, do they already exist?
	IF NOT EXISTS ( SELECT * FROM patient WHERE patient_id = @patientID)
	BEGIN
		RAISERROR('The patient does not exists.', 16, 1);
		RETURN 0
	END

	-- Grab the patient's notes in order of the most recent note first, oldest one last.
	-- TODO: Resolve user_id?
	SELECT * FROM patient_note WHERE patient_id = @patientID ORDER BY timestamp DESC;
END

GO
GRANT EXECUTE ON [dbo].[GET_PATIENT_NOTES] TO [movement_app]
GO
