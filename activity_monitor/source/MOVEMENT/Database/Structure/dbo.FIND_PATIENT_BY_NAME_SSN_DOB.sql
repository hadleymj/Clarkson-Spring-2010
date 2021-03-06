IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FIND_PATIENT_BY_NAME_SSN_DOB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FIND_PATIENT_BY_NAME_SSN_DOB]
GO
/****** Object:  StoredProcedure [dbo].[FIND_PATIENT_BY_NAME_SSN_DOB]    Script Date: 05/04/2007 13:16:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Christopher Pope
-- Create date: Nov 12 2006
-- Description:	Search for a patient by their name, ssn and date of birth.
-- =============================================
CREATE PROCEDURE FIND_PATIENT_BY_NAME_SSN_DOB 
	-- Add the parameters for the stored procedure here
	@name varchar(50), 
	@ssn char(4),
	@dob smalldatetime 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT patient_id
	FROM patient_subject_join	
	WHERE name = @name AND ssn4 = @ssn AND dob = @dob;
END

GO
GRANT EXECUTE ON [dbo].[FIND_PATIENT_BY_NAME_SSN_DOB] TO [movement_app]
GO
