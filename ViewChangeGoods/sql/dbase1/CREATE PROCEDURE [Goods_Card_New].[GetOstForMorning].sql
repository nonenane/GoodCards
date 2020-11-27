SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Получение остатков товаров на утро
-- =============================================
ALTER PROCEDURE [Goods_Card_New].[GetOstForMorning]		 	
	@date date,
	@listEan varchar(max)
AS
BEGIN
	SET NOCOUNT ON;


DECLARE @dttost date, @id_ttost int

select TOP(1) @dttost =dttost,@id_ttost = id from dbo.j_ttost where dttost<@date and promeg = 0 order by dttost desc

--select TOP(1) @dttost =dttost,@id_ttost = id from dbo.j_ttost where  id = 1817


select 
	id 
into 
	#idTovar 
from 
	dbo.s_tovar 
where 
	ltrim(rtrim(ean)) in (select value from dbo.StringToTable(@listEan,','))

select 
	trim(a.ean) as ean,
	sum(a.netto) as netto 
from(
select 
	trim(o.ean) as ean,
	sum(o.netto) as netto
from 
	dbo.j_tost t
		inner join dbo.j_ost o on o.id_tost = t.id
where 
	t.id_ttost = @id_ttost and ltrim(rtrim(o.ean)) in (select value from dbo.StringToTable(@listEan,','))
group by 
	o.ean

union all

select 	
	trim(t.ean) as ean,
	sum(a.netto) as netto
from(
select 
	p.id_tovar,
	sum(p.netto) as netto
from 
	dbo.j_allprihod a		
		inner join dbo.j_prihod p on p.id_allprihod = a.id 
		inner join #idTovar t on t.id = p.id_tovar
where 
	@dttost<=a.dprihod and a.dprihod<@date and a.id_operand in (1,3) 
group by p.id_tovar

UNION ALL

select 
	p.id_tovar,
	sum(-1.0*p.netto) as netto
from 
	dbo.j_allprihod a
		inner join dbo.j_otgruz p on p.id_allprihod = a.id
		inner join #idTovar t on t.id = p.id_tovar
where 
	@dttost<=a.dprihod and a.dprihod<@date and a.id_operand in (8)
group by p.id_tovar

UNION ALL

select 
	p.id_tovar,
	sum(-1.0*p.netto) as netto
from 
	dbo.j_allprihod a
		inner join dbo.j_vozvr p on p.id_allprihod = a.id 
		inner join #idTovar t on t.id = p.id_tovar
where 
	@dttost<=a.dprihod and a.dprihod<@date and a.id_operand in (2)
group by p.id_tovar

UNION ALL

select 
	p.id_tovar,
	sum(-1.0*p.netto) as netto
from 
	dbo.j_allprihod a
		inner join dbo.j_vozvkass p on p.id_allprihod = a.id 
		inner join #idTovar t on t.id = p.id_tovar
where 
	@dttost<=a.dprihod and a.dprihod<@date and a.id_operand in (7)
group by p.id_tovar

UNION ALL

select 
	p.id_tovar,
	sum(-1.0*p.netto) as netto
from 
	dbo.j_allprihod a
		inner join dbo.j_spis p on p.id_allprihod = a.id 
		inner join #idTovar t on t.id = p.id_tovar
where 
	@dttost<=a.dprihod and a.dprihod<@date and a.id_operand in (5)
group by p.id_tovar

UNION ALL

select 
	p.id_tovar,
	sum(-1.0*p.netto) as netto
from 
	dbo.j_realiz p 
	inner join #idTovar t on t.id = p.id_tovar
where 
	@dttost<=p.drealiz and p.drealiz<@date
group by p.id_tovar
)as a
	left join dbo.s_tovar t on t.id = a.id_tovar
	and ltrim(rtrim(t.ean)) in (select value from dbo.StringToTable(@listEan,','))
	and t.ean is not null
group by a.id_tovar,trim(t.ean)) as a
group by 
	a.ean

DROP TABLE #idTovar

END
