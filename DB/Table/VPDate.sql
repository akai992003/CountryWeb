-- 疫苗預約日期檔
CREATE TABLE dbo.VPDate (
	Id int NOT NULL,
	Head nvarchar(20) NULL,
	Date1 date NULL DEFAULT (NULL),
	Week nvarchar(20) NULL,
	VPtypes int NULL DEFAULT (NULL),
	Cnt int NULL
);
GO

create unique index idx1 on VPDate(Id);
create clustered index idx2 on VPDate(Id,VPtypes);
create index idx3 on VPDate(Date1);