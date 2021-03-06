IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FIND_PATIENT_BY_NAME]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FIND_PATIENT_BY_NAME]
GO
/****** Object:  StoredProcedure [dbo].[FIND_PATIENT_BY_NAME]    Script Date: 05/04/2007 13:16:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Michael Petito
-- Create date: Nov 11 2006
-- Description:	Find a patient by their name only.
-- =============================================
CREATE PROCEDURE FIND_PATIENT_BY_NAME 
	-- Add the parameters for the stored procedure here
	@name varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT patient.patient_id FROM patient
	WHERE patient.name = @name;
END

GO
GRANT EXECUTE ON [dbo].[FIND_PATIENT_BY_NAME] TO [movement_app]
GO
