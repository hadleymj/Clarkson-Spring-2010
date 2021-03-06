IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SET_PATIENT_ADDRESS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SET_PATIENT_ADDRESS]
GO
/****** Object:  StoredProcedure [dbo].[SET_PATIENT_ADDRESS]    Script Date: 05/04/2007 13:17:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Michael Petito
-- Create date: Nov 16 2006
-- Description:	Set the patient's address.
-- =============================================
CREATE PROCEDURE SET_PATIENT_ADDRESS
	-- Add the parameters for the stored procedure here
	@patientID int, 
	@address text
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE patient 
	SET address = @address
	WHERE patient_id = @patientID;
	
	-- Error checking, do they already exist?
	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('The patient does not exists.', 16, 1);
		RETURN 0
	END
END

GO
GRANT EXECUTE ON [dbo].[SET_PATIENT_ADDRESS] TO [movement_app]
GO
