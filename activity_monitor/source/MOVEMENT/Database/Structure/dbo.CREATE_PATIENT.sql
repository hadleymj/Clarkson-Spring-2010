IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CREATE_PATIENT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CREATE_PATIENT]
GO
/****** Object:  StoredProcedure [dbo].[CREATE_PATIENT]    Script Date: 05/04/2007 13:16:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Christopher Pope
-- Create date: Oct 26 2006
-- Description:	Create a patient in the database.
-- =============================================
CREATE PROCEDURE [dbo].[CREATE_PATIENT] 
	-- Add the parameters for the stored procedure here
	@name varchar(50),
	@ssn char(4), 
	@address text = NULL,
	@contact text = NULL,
	@sex char(1) = NULL,
	@dob smalldatetime = NULL,
	@handedness char(1) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @patid int;
	DECLARE @subid uniqueidentifier;

	-- Set a ROLLBACK POINT
	BEGIN TRANSACTION

    -- Insert a new patient record.
	INSERT INTO patient (name, address, contact_info, ssn4)
	VALUES (@name, @address, @contact, @ssn);

	IF @@error <> 0 
	BEGIN 
		ROLLBACK TRANSACTION
		RAISERROR('An error occurred while trying to create a new patient. Required fields are the patient name and last 4 digits of their social security number.',16,1);
		RETURN 
	END

	-- What was our last patient id?
	SELECT @patid = @@IDENTITY FROM patient;
	
	-- MPP 11/12/06: modified to support uniqueidentifiers for subjects
	-- What is the subject ID going to be?
	SELECT @subid = newid()
	
	-- Insert a new subject record.
	INSERT INTO subject(subject_id, created, sex, dob, handedness)
	VALUES (@subid, getutcdate(), @sex, @dob, @handedness);

	IF @@error <> 0 
	BEGIN 
		ROLLBACK TRANSACTION
		RAISERROR('An error occurred while trying to create a non-identifying subject record.',16,1);
		RETURN 
	END
	
	-- Establish connection between patient and subject.
	INSERT INTO patient_subject (patient_id, subject_id)
	VALUES (@patid, @subid);

	IF @@error <> 0 
	BEGIN 
		ROLLBACK TRANSACTION
		RAISERROR('The patient was created successfully but could not be correctly associated with a subject record.',16,1);
		RETURN 
	END

	-- Return the Subject Id.
	COMMIT TRANSACTION
	
	--MPP 11/2/06: return the new patient id
	RETURN @patid
END







GO
GRANT EXECUTE ON [dbo].[CREATE_PATIENT] TO [movement_app]
GO
