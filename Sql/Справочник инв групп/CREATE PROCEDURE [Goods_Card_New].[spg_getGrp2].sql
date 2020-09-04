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


select 
	g.id,
	g.id_otdel,
	g.id_unigrp,
	g.id_unit,
	ltrim(rtrim(g.cname)) as cName,
	ltrim(rtrim(d.name)) as nameDeps,
	ltrim(rtrim(u.uni_grp_name)) as nameUniGrp,
	ltrim(rtrim(un.cunit)) as nameUnit,
	g.specification,
	g.skoroportovar,
	g.NettoMax,
	cast(g.DayMax as int) as DayMax,
	g.ldeystv as isActive
from 
	dbo.s_grp2 g		
		left join dbo.departments d on  d.id = g.id_otdel
		left join dbo.s_uni_grp u on u.id = g.id_unigrp
		left join dbo.s_unit un on un.id = g.id_unit


END
