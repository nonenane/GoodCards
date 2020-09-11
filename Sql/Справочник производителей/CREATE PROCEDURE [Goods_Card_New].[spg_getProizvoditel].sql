SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Получение списка производителей
-- =============================================
ALTER PROCEDURE [Goods_Card_New].[spg_getProizvoditel]		 	
AS
BEGIN
	SET NOCOUNT ON;


select
	p.id,
	p.inn,
	isnull(ltrim(rtrim(t.name))+' ','')+ltrim(rtrim(p.name)) as cName,
	p.isActive,
	p.id_type_org,
	ltrim(rtrim(p.name)) as nameForEdit,
	isnull(ltrim(rtrim(t.name)),'') as nameType

from 
	dbo.s_Proizvoditel p 
		left join dbo.s_type_org t  on t.id = p.id_type_org
END
