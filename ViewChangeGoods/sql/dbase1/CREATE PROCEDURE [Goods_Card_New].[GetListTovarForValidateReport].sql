SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Получение списка товаров для сравнения цен по ТК и на кассах
-- =============================================
ALTER PROCEDURE [Goods_Card_New].[GetListTovarForValidateReport]		 	
		@id_deps int = 0
AS
BEGIN
	SET NOCOUNT ON;

DECLARE @date date = '2020-10-22' -- getdate();
	set @date = GETDATE()

DECLARE 
	@date_start datetime = dateadd(hour,6,cast(@date as datetime)),
	@date_end datetime = dateadd(hour,27,cast(@date as datetime))


select 
	r.id_tovar,
	r.rcena,
	r.DateCreate,
	r.id_Creator
INTO 
	#s_rcena 
from dbo.s_rcena r inner join( 
select 
	r.id_tovar,
	max(tdate_n) as tdate_n
from 
	dbo.s_rcena r 
group by r.id_tovar) as rg on rg.id_tovar = r.id_tovar and rg.tdate_n= r.tdate_n


select 
	r.id_tovar,
	trim(t.ean) as ean,	
	n.cname,
	r.rcena,
	r.DateCreate,
	l.FIO,
	t.id_otdel,
	d.name as nameDep,
	c.id as idPromo,

	--
	0.00 as priceKass,
	'' as FIOKass,
	cast(null as datetime) as dateKass

from 
	--dbo.s_rcena r 
	#s_rcena r
		inner join dbo.s_tovar t on t.id = r.id_tovar
		inner join dbo.s_ntovar n on n.id_tovar = t.id and n.ntypetovar not in (1,3) and n.isActual = 1	
		inner join dbo.s_grp1 g1 on g1.id = t.id_grp1 and g1.ldeystv = 1
		inner join dbo.departments d on d.id = t.id_otdel and d.ldeyst = 1
		left join dbo.ListUsers l on l.id = r.id_Creator
		left join requests.s_CatalogPromotionalTovars c on c.id_tovar = r.id_tovar
where 
	--cast(r.tdate_n as date) = @date 
	--@date_start<= r.tdate_n and r.tdate_n<=@date_end and 
	(@id_deps = 0 or t.id_otdel = @id_deps)	
	
DROP TABLE #s_rcena

END
