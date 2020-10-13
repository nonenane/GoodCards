USE [dbase1]
GO
/****** Object:  StoredProcedure [Goods_Card_New].[spg_getGrp3]    Script Date: 29.09.2020 21:31:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-09-29
-- Description:	Получение списка товаров для переоценки
-- =============================================
ALTER PROCEDURE [Goods_Card_New].[getRcenaFuture]	
	@date date
AS
BEGIN
	SET NOCOUNT ON;


DECLARE  @ntypeorg int, @Abbriviation varchar(5)


select distinct a.id_tovar,a.date,g.ntypeorg,g.send into #tmp1  from (
select g.id_tovar,max(g.date) as date from [dbo].[goods_vs_firms] g  where g.date<=@date --and send = 1
GROUP BY g.id_tovar)  a inner join  [dbo].[goods_vs_firms] g  on g.date = a.date and g.id_tovar = a.id_tovar --and g.send = 1


select ntypeorg,f.id_departments INTO #tmp_FvD_ntypeorg 
from dbo.firms_vs_departments f
where f.DateStart<= @date AND @date <= f.DateEnd --and f.id_departments = @id_dep

select nTypeOrg,Abbriviation INTO #TMP_2 
from [ISI_SERVER].[dbase1].[dbo].[s_MainOrg] 
where isSeler = 1 and DateStart<=@date and @date<=DateEnd

select distinct t.id_tovar,cast(smo.nTypeOrg as int) as ntypeorg,t2.Abbriviation,tov.id_grp1,t.send INTO #tmp_tovar_GvF
from #tmp1 t
	LEFT JOIN dbo.s_tovar tov on tov.id = t.id_tovar
	LEFT JOIN #tmp_FvD_ntypeorg n on n.ntypeorg= t.ntypeorg
	LEFT JOIN #TMP_2 t2 on t2.nTypeOrg = n.ntypeorg
	LEFT JOIN dbo.s_SelectedMainOrg smo on smo.nTypeOrg = t2.nTypeOrg
	   	 
select distinct g.id,cast(smo.nTypeOrg as int) as ntypeorg,t2.Abbriviation INTO #tmp_tovar_Grp 
from dbo.s_grp1 g
	LEFT JOIN #tmp_FvD_ntypeorg n on n.ntypeorg= g.ntypeorg
	LEFT JOIN #TMP_2 t2 on t2.nTypeOrg = n.ntypeorg
	LEFT JOIN dbo.s_SelectedMainOrg smo on smo.nTypeOrg = t2.nTypeOrg
where --g.id_otdel = @id_dep and 
smo.nTypeOrg is not null

--select @ntypeorg= t.nTypeOrg, @Abbriviation = t.Abbriviation, f.id_departments 
select distinct t.nTypeOrg, t.Abbriviation, f.id_departments into #tmpLast
from dbo.firms_vs_departments f inner join #TMP_2 t on t.nTypeOrg = f.ntypeorg
where --f.id_departments = @id_dep and 
f.[default] = 1 and f.DateStart<=@date and @date<=f.DateEnd




select distinct
	cast(case when t.ntypetovar in (1,3) or  p.id is not null then 0 else 1 end as bit) as isSelect,
	 r.id_tovar,
	 case when t.ntypetovar in (1,3) then 0.00 else r.rcena end as rcena,
	 t.id_otdel,
	 ltrim(rtrim(t.ean)) as ean,
	 ltrim(rtrim(isnull(ltrim(rtrim(tt.cName))+' ','')+ltrim(rtrim(t.cname)))) as cname,
	 d.name as nameDep,
	 t.ntypetovar,
	 p.id as idPromo,
	 cast(n.nds as int) as nds,
	 t.id_grp1,
	 --0 as ntypeorg
	 isnull(gf.ntypeorg,isnull(gg.ntypeorg,cast(gl.nTypeOrg as int)))as ntypeorg

from 
	dbo.s_rcena_future r
		left join dbo.v_tovar t on t.id_tovar = r.id_tovar
		left join dbo.departments d on d.id = t.id_otdel
		left join dbo.s_TypeTovar tt on tt.id = t.ntypetovar
		left join requests.s_CatalogPromotionalTovars p on p.id_tovar = r.id_tovar
		left join dbo.s_nds n on n.id = t.id_nds
		left join #tmp_tovar_GvF gf on gf.id_tovar = t.id_tovar
		left join #tmp_tovar_Grp gg on gg.id = t.id_grp1
		left join #tmpLast gl on gl.id_departments = t.id_otdel
where 
	cast(tdate_n as date) = @date

DROP TABLE #tmp1,#tmp_FvD_ntypeorg,#TMP_2,#tmp_tovar_Grp,#tmp_tovar_GvF,#tmpLast

END
