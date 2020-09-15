SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Получение списка подгрупп
-- =============================================
ALTER PROCEDURE [Goods_Card_New].[spg_getGrp3]		 	
AS
BEGIN
	SET NOCOUNT ON;


select 
	g.id,
	ltrim(rtrim(g.cName)) as cName,
	cast(g.id_otdel as int) as id_otdel,
	ltrim(rtrim(d.name)) as nameDeps,
	g.ldeystv as isActive
from 
	dbo.s_grp3 g
		left join dbo.departments d on  d.id = g.id_otdel
order by
	g.cName asc


END
