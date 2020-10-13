USE [dbase1]
GO
/****** Object:  StoredProcedure [Goods_Card_New].[spg_getGrp3]    Script Date: 29.09.2020 21:31:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-09-29
-- Description:	Получение товаров с учётом фильтров
-- =============================================
ALTER PROCEDURE [Goods_Card_New].[getFindTovar]
	@ean varchar(13) = null,
	@cName varchar(1024) = null,
	@id_prog int,
	@isNewGoods bit
AS
BEGIN
	SET NOCOUNT ON;

select 
	t.id_otdel,
	t.id_grp1,
	t.id_grp2,
	t.id_grp3,
	ltrim(rtrim(t.ean)) as ean,
	ltrim(rtrim(isnull(ltrim(rtrim(tt.cName))+' ','')+ltrim(rtrim(t.cname)))) as cname,
	t.rcena,
	ltrim(rtrim(g1.cname)) as nameGrp1,
	ltrim(rtrim(g2.cname)) as nameGrp2,
	ltrim(rtrim(g3.cName)) as nameGrp3,
	d.name as nameDep,
	t.ntypetovar
from 
	v_tovar t
		left join dbo.departments d on d.id = t.id_otdel
		left join dbo.s_grp1 g1 on g1.id = t.id_grp1
		left join dbo.s_grp2 g2 on g2.id = t.id_grp2
		left join dbo.s_grp3 g3 on g3.id = t.id_grp3
		left join dbo.s_TypeTovar tt on tt.id = t.ntypetovar
		left join dbo.s_rcena r on r.id_tovar = t.id_tovar and  r.isActual = 1-- r.id = (select top(1) rr.id from dbo.s_rcena rr where rr.id_tovar = t.id_tovar order by rr.tdate_n desc)
where
	(@isNewGoods = 0 and ((@ean is not null and @cName is not null and  ltrim(rtrim(t.ean)) like '%'+@ean+'%' and lower(ltrim(rtrim(t.cname))) like '%'+@cName+'%')
	OR 
	(@cName is null and  @ean is not null and ltrim(rtrim(t.ean)) like '%'+@ean+'%') 
	OR 
	(@ean is null and @cName is not null and lower(ltrim(rtrim(t.cname))) like '%'+@cName+'%' )))

	OR
	
	(@isNewGoods = 1 and t.id_grp1 in (select value from dbo.prog_config where id_value = 'ntg1' and id_prog = @id_prog))


END
