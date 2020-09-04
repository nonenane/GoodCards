SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Получение списка uni grp
-- =============================================
ALTER PROCEDURE [Goods_Card_New].[spg_getUniGrp]		 	
AS
BEGIN
	SET NOCOUNT ON;


select 
	g.id,
	ltrim(rtrim(g.uni_grp_name)) as cName,
	isnull(g.isActive,1) as isActive
from 
	dbo.s_uni_grp	g
		


END
