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

DECLARE 
	@date_start datetime = dateadd(hour,6,cast(@date as datetime)),
	@date_end datetime = dateadd(hour,27,cast(@date as datetime))


select 
	trim(g.ean) as ean,
	g.price/100.0 as price,
	trim(g.name) as cName,
	l.FIO,
	g.s_time
from 
	dbo.goods_updates g
		left join dbo.ListUsers l on l.id = g.sender
where 
	--cast(g.s_time as date) = @date
	--@date_start<= g.s_time and g.s_time<=@date_end and
	 g.ActualRow = 1 and
	(@id_deps = 0 or g.id_departments = @id_deps)

END
