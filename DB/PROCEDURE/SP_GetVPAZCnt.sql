SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE PROCEDURE SP_GetVPCnt @id INT
AS 
/* 疫苗預約剩餘可用人數查詢*/
	
-- 	declare @id int = 18;

	SELECT Id AS id, Head AS head, Date1 AS date1, Week AS Week, Cnt AS cnt,
			CASE WHEN B.Cnt2 IS NULL THEN 0 ELSE B.Cnt2 END AS cnt2
	FROM dbo.VPDate A
		LEFT JOIN (
			SELECT VPId, COUNT(*) AS Cnt2
			FROM dbo.Covid19
			where vPId > 0
			GROUP BY VPId
		) B ON A.Id = B.VPId
	WHERE A.Id = @id
GO
