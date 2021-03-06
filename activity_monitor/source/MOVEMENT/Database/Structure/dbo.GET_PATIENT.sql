IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GET_PATIENT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GET_PATIENT]
GO
/****** Object:  StoredProcedure [dbo].[GET_PATIENT]    Script Date: 05/04/2007 13:16:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Michael Petito
-- Create date: Nov 2 2006
-- Description:	Retrieve all of a patient's information.
-- =============================================
CREATE PROCEDURE GET_PATIENT 
	-- Add the parameters for the stored procedure here
	@patientID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--MPP 11/10/06: modified to use patient_subject_join
	SELECT patient_id, name, address, contact_info, ssn4, created, sex, dob, handedness
	FROM patient_subject_join
	WHERE patient_id = @patientID	

	-- Error checking, do they already exist?
	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('The patient does not exists.', 16, 1);
		RETURN 0
	END
END

GO
GRANT EXECUTE ON [dbo].[GET_PATIENT] TO [movement_app]
GO
