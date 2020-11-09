SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-11-04
-- Description:	Получение списка изменёных товаров по цене
-- =============================================
CREATE PROCEDURE [Goods_Card_New].[GetChangeGoodsPrice]		 	
	@date date
AS
BEGIN
	SET NOCOUNT ON;


BEGIN -- Получение данных по чекам

DECLARE 
	@date_start datetime = dateadd(hour,6,cast(@date as datetime)),
	@date_end datetime = dateadd(hour,27,cast(@date as datetime))

 --Подстановка нужного журнала касс под выборку
 declare @MONTH varchar(2)
 set @MONTH=(case when MONTH(@date_start)<10 then '0'+cast(MONTH(@date_start) as varchar(1)) else cast(MONTH(@date_start) as varchar(2)) end)
 declare @SQL nvarchar(MAX)
 declare @journal nvarchar(255)
 set @journal='[KassRealiz].[dbo].[journal_'+cast(YEAR(@date_start) as varchar)+'_'+@MONTH+']'  
 

 DECLARE @table table (ean varchar(13),[count] numeric(17,3),[price] numeric(17,2))


set @SQL = 
 '
 select
   j.terminal,
   j.doc_id into #used_docs
 from
   ' + @journal + ' j
 where
  convert(datetime, j.time) >= convert(datetime, ''' + convert(varchar,@date_start,120) + ''') and convert(datetime, j.time) <= convert(datetime, ''' + convert(varchar,@date_end,120) + ''') and op_code = 509


  select
   ltrim(rtrim(j.ean)) as ean,
   SUM(case when j.op_code = 505 then 1.0 else -1.0 end * j.count/1000.0) as count,
   j.price/100.0 as price   
 from	
   ' + @journal + ' j
	inner join #used_docs u on u.terminal = j.terminal and u.doc_id = j.doc_id
 where
  convert(datetime, j.time) >= convert(datetime, ''' + convert(varchar,@date_start,120) + ''') and convert(datetime, j.time) <= convert(datetime, ''' + convert(varchar,@date_end,120) + ''') and op_code in (505,507)
  group by j.ean,j.price
  order by j.ean asc

  DROP TABLE  #used_docs
 '

INSERT INTO @table
EXEC (@SQL)

END


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
	cast(0 as bit) as isReserv,
	g.priceAfter,
	g.priceBefore,
	isnull(t.count,0.0) as countForSell,
	0.0 as ostForMorning
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
	g.sender,
	g.price as priceAfter,
	g2.price as priceBefore	
from 
	#tmpGoods g
		left join #tmpGoods g2 on g2.ean = g.ean and g2.s_time =(select TOP(1) gg.s_time from #tmpGoods gg where gg.ean = g.ean and gg.s_time< g.s_time order by gg.s_time desc) 
			and (g2.price <> g.price)
where 
	g2.ean is not null


--UNION ALL

--select 
--	g.id_departments,
--	g.ean,
--	g.name as nameAtfer,
--	g2.name as nameBefore,
--	g.grp as grpAtfer,
--	g2.grp as grpBefore,
--	g.s_time as timeAfter,
--	g2.s_time as timeBefore,
--	g.dpt as dptAtfer,
--	g2.dpt as dptBefore,
--	g.tax as taxAtfer,
--	g2.tax as taxBefore,
--	g.sender,
--	g.price as priceAfter,
--	g2.price as priceBefore
--from 
--	(select ean,min(s_time) as s_time from #tmpGoods group by ean) as f
--		inner join #tmpGoods g on g.ean = f.ean and g.s_time = f.s_time
--		left join dbo.goods_updates g2 on g2.ean = g.ean and g2.s_time =(select TOP(1) gg.s_time from dbo.goods_updates gg where gg.ean = g.ean and gg.s_time< g.s_time order by gg.s_time desc) 
--			and (g2.price <> g.price)

--where 
--	g2.ean is not null
) as g 
	left join #tmpMainOrg m on m.nTypeOrg = g.dptAtfer
	left join #tmpMainOrg m2 on m2.nTypeOrg = g.dptBefore
	left join dbo.ListUsers l on l.id = g.sender
	left join @table t on t.ean = g.ean and t.price = g.priceBefore/100.0
order by 
	g.ean asc,g.taxAtfer desc

DROP TABLE #tmpGoods,#tmpMainOrg


END
