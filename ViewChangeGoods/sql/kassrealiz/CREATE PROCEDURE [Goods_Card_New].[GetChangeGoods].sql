SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Получение списка изменёных товаров
-- =============================================
ALTER PROCEDURE [Goods_Card_New].[GetChangeGoods]		 	
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
	g.nameBefore,
	g.grpAtfer,
	g.grpBefore,
	'' as nameGrpAtfer,
	'' as nameGrpBefore,
	g.timeAfter,
	g.timeBefore,
	g.dptAtfer,
	g.dptBefore,
	g.taxAtfer,
	g.taxBefore,
	isnull(m.Abbriviation,'') as ulAfter,
	isnull(m2.Abbriviation,'') as ulBefore,
	g.sender,
	isnull(l.FIO,'') as FIO,
	cast(0 as bit) as isReserv
from (
select 
	g.id_departments,
	g.ean,
	g.name as nameAtfer,
	g2.name as nameBefore,
	g.grp as grpAtfer,
	g2.grp as grpBefore,
	g.s_time as timeAfter,
	g2.s_time as timeBefore,
	g.dpt as dptAtfer,
	g2.dpt as dptBefore,
	g.tax as taxAtfer,
	g2.tax as taxBefore,
	g.sender
from 
	#tmpGoods g
		left join #tmpGoods g2 on g2.ean = g.ean and g2.s_time =(select TOP(1) gg.s_time from #tmpGoods gg where gg.ean = g.ean and gg.s_time< g.s_time order by gg.s_time desc) 
			and (g2.name <> g.name or g2.dpt <> g.dpt or g2.grp <> g.grp  or g2.tax <> g.tax)
where 
	g2.ean is not null


UNION ALL

select 
	g.id_departments,
	g.ean,
	g.name as nameAtfer,
	g2.name as nameBefore,
	g.grp as grpAtfer,
	g2.grp as grpBefore,
	g.s_time as timeAfter,
	g2.s_time as timeBefore,
	g.dpt as dptAtfer,
	g2.dpt as dptBefore,
	g.tax as taxAtfer,
	g2.tax as taxBefore,
	g.sender
from 
	(select ean,min(s_time) as s_time from #tmpGoods group by ean) as f
		inner join #tmpGoods g on g.ean = f.ean and g.s_time = f.s_time
		left join dbo.goods_updates g2 on trim(g2.ean) = g.ean and g2.s_time =(select TOP(1) gg.s_time from dbo.goods_updates gg where trim(gg.ean) = g.ean and gg.s_time< g.s_time order by gg.s_time desc) 
			and (g2.name <> g.name or g2.dpt <> g.dpt or g2.grp <> g.grp  or g2.tax <> g.tax )

where 
	g2.ean is not null
) as g 
	left join #tmpMainOrg m on m.nTypeOrg = g.dptAtfer
	left join #tmpMainOrg m2 on m2.nTypeOrg = g.dptBefore
	left join dbo.ListUsers l on l.id = g.sender
order by 
	g.ean asc,g.taxAtfer desc

DROP TABLE #tmpGoods,#tmpMainOrg
END
