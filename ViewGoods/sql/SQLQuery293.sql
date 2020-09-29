DECLARE 
	@ean varchar(13) = '20',
	@cName varchar(1024) = '����'

	exec [Goods_Card_New].[getFindTovar] @ean,@cName,118,1

select 
	t.id_otdel,
	t.id_grp1,
	t.id_grp2,
	t.id_grp3,
	t.ean,
	t.cname,
	t.rcena,
	g1.cname as nameGrp1,
	g2.cname as nameGrp2,
	g3.cName as nameGrp3,
	d.name as nameDep
from 
	v_tovar t
		left join dbo.departments d on d.id = t.id_otdel
		left join dbo.s_grp1 g1 on g1.id = t.id_grp1
		left join dbo.s_grp2 g2 on g2.id = t.id_grp2
		left join dbo.s_grp3 g3 on g3.id = t.id_grp3
		left join dbo.s_rcena r on r.id_tovar = t.id_tovar and r.id = (select top(1) rr.id from dbo.s_rcena rr where rr.id_tovar = t.id_tovar order by rr.tdate_n desc)
where
	(@ean is not null and @cName is not null and  ltrim(rtrim(t.ean)) like '%'+@ean+'%' and lower(ltrim(rtrim(t.cname))) like '%'+@cName+'%')
	OR 
	(@cName is null and  @ean is not null and ltrim(rtrim(t.ean)) like '%'+@ean+'%') 
	OR 
	(@ean is null and @cName is not null and lower(ltrim(rtrim(t.cname))) like '%'+@cName+'%' )