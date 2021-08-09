-- 疫苗預約記錄檔
CREATE TABLE dbo.Covid19 (
	Guid uniqueidentifier NULL,
	Id nvarchar(20) NULL,
	Name nvarchar(20) NULL,
	Birthday nvarchar(20) NULL DEFAULT (NULL),
	Phone nvarchar(20) NULL,
	VPId int NULL,
	Addr_country nvarchar(20) NULL DEFAULT (NULL),
	Addr_city nvarchar(20) NULL DEFAULT (NULL),
	Addr_detal nvarchar(50) NULL DEFAULT (NULL),
	Idtypes int NULL DEFAULT (NULL),
	VPtypes int NULL DEFAULT (NULL),
	CreateTime datetime NULL DEFAULT (NULL)
);
GO

create unique index idx1 on Covid19(Guid);
create clustered index idx2 on Covid19(Id);