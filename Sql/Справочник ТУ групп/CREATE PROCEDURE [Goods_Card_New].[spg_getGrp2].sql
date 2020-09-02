SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Получение списка инв. групп
-- =============================================
ALTER PROCEDURE [Goods_Card_New].[spg_getGrp2]		 	
AS
BEGIN
	SET NOCOUNT ON;

--create table #tmpMainOrg (nTypeOrg int,Abbriviation varchar(max))

--insert into #tmpMainOrg
--select nTypeOrg,Abbriviation from dbo.s_MainOrg where DateStart<=GETDATE() and GETDATE()<=DateEnd and isSeler = 1 order by nTypeOrg asc


select 
	g.id,
	--g.id_nds,
	g.id_otdel,
	ltrim(rtrim(g.cname)) as cName,
	--n.nds,
	ltrim(rtrim(d.name)) as nameDeps,
	--g.ntypeorg,
	--t.Abbriviation,
	g.ldeystv as isActive
	--g.isCredit,
	--g.isWithSubGroups
from 
	dbo.s_grp2 g
		--left join dbo.s_nds n on n.id = g.id_nds
		left join dbo.departments d on  d.id = g.id_otdel
		--left join #tmpMainOrg t on t.nTypeOrg = g.ntypeorg
		
--DROP TABLE #tmpMainOrg


END
