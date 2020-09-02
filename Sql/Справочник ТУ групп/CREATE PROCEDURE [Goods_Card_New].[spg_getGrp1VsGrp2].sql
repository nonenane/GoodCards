SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Получение списка Инв групп которые привязаны к ТУ группе
-- =============================================
ALTER PROCEDURE [Goods_Card_New].[spg_getGrp1VsGrp2]		 	
	@id_grp1 int 
AS
BEGIN
	SET NOCOUNT ON;

select 
	g2.id,
	ltrim(rtrim(g2.cname)) as cName
from 
	dbo.grp1_vs_grp2 g1
		inner join dbo.s_grp2 g2 on g2.id = g1.id_grp2
where 
	g1.id_grp1 = @id_grp1
order by g2.cname asc

END
