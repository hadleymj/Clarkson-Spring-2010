IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GET_POPULATION_DATA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GET_POPULATION_DATA]
GO
/****** Object:  StoredProcedure [dbo].[GET_POPULATION_DATA]    Script Date: 05/04/2007 13:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Christopher Pope
-- Create date: Oct 27 2006
-- Description:	Retrieve the populated data for a specified test type.
-- =============================================
CREATE PROCEDURE GET_POPULATION_DATA 
	-- Add the parameters for the stored procedure here
	@testType varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- TODO: What exactly is this supposed to retrieve?
	RAISERROR('NO IMPLEMENTATION', 16, 1);

END

GO
GRANT EXECUTE ON [dbo].[GET_POPULATION_DATA] TO [movement_app]
GO
