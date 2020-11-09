SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Получение списка новых товаров
-- =============================================
CREATE PROCEDURE [Goods_Card_New].[GetNewGoods]		 	
	@date date
AS
BEGIN
	SET NOCOUNT ON;



select 
	m.nTypeOrg,
	m.Abbriviation
INTO 
	#tmpMainOrg
from 
	dbo.s_MainOrg m
where 
	m.DateStart<=@date and @date<=m.DateEnd

select 
	trim(g.ean) as ean,
	trim(g.name) as name,
	g.price,
	g.dpt,
	g.grp,
	g.tax,
	g.s_time,
	g.id_departments,
	g.ActualRow,
	g.sender
INTO 
	#tmpGoods
from 
	dbo.goods_updates g
where
	cast(g.s_time as date) = @date


select 
	g.id_departments,
	'' as nameDep,
	g.ean,
	g.nameAtfer,
	g.grpAtfer,
	'' as nameGrpAtfer,
	g.timeAfter,
	g.dptAtfer,
	g.taxAtfer,
	isnull(m.Abbriviation,'') as ulAfter,
	g.sender,
	isnull(l.FIO,'') as FIO,
	cast(0 as bit) as isReserv	
from (

select 
	g.id_departments,
	g.ean,
	g.name as nameAtfer,	
	g.grp as grpAtfer,
	g.s_time as timeAfter,
	g.dpt as dptAtfer,
	g.tax as taxAtfer,
	g.sender
from 
	(select ean,max(s_time) as s_time from #tmpGoods group by ean) as f
		inner join #tmpGoods g on g.ean = f.ean and g.s_time = f.s_time
		left join dbo.goods_updates g2 on trim(g2.ean) = g.ean and g2.s_time =(select TOP(1) gg.s_time from dbo.goods_updates gg where trim(gg.ean) = g.ean and gg.s_time< g.s_time order by gg.s_time desc)			
where 
	g2.ean is null
) as g 
	left join #tmpMainOrg m on m.nTypeOrg = g.dptAtfer
	left join dbo.ListUsers l on l.id = g.sender
order by 
	g.ean asc,g.taxAtfer desc

DROP TABLE #tmpGoods,#tmpMainOrg
END
