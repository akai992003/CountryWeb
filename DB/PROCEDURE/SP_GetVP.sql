SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
ALTER PROCEDURE SP_GetVP @VPtypes int
AS
/* 疫苗預約日期列表 */
-- 	declare @VPtypes int = 1;

	SELECT Id AS id, Head as head, Date1 as date1, Week as week
		, Cnt as cnt
		, CASE WHEN B.Cnt2 IS NULL THEN 0 ELSE B.Cnt2 END AS cnt2
	FROM dbo.VPDate A
		LEFT JOIN (
			SELECT VPId , COUNT(*) AS Cnt2
			FROM dbo.Covid19
			where vPId > 0
			GROUP BY vPId 
		) B ON A.id = B.VPId
	WHERE id > 0
	and A.VPtypes = @VPtypes
	order by id
GO