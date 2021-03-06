IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[patient_subject_join]'))
DROP VIEW [dbo].[patient_subject_join]
GO
/****** Object:  View [dbo].[patient_subject_join]    Script Date: 05/04/2007 13:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW dbo.patient_subject_join
AS
SELECT     dbo.patient.patient_id, dbo.patient.name, dbo.patient.address, dbo.patient.contact_info, dbo.patient.ssn4, dbo.patient_subject.patient_id AS Expr1, 
                      dbo.patient_subject.subject_id, dbo.subject.subject_id AS Expr2, dbo.subject.created, dbo.subject.sex, dbo.subject.dob, dbo.subject.handedness
FROM         dbo.patient INNER JOIN
                      dbo.patient_subject ON dbo.patient.patient_id = dbo.patient_subject.patient_id INNER JOIN
                      dbo.subject ON dbo.patient_subject.subject_id = dbo.subject.subject_id

GO
