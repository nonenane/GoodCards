DECLARE 
	@ean varchar(13) = '20',
	@cName varchar(1024) = 'соси'


select 
	* 
from 
	v_tovar t  
	left join dbo.s_rcena r on r.id_tovar = t.id_tovar
where
	(@ean is not null and @cName is not null and  ltrim(rtrim(t.ean)) like '%'+@ean+'%' and lower(ltrim(rtrim(t.cname))) like '%'+@cName+'%')
	OR 
	(@cName is null and  @ean is not null and ltrim(rtrim(t.ean)) like '%'+@ean+'%') 
	OR 
	(@ean is null and @cName is not null and lower(ltrim(rtrim(t.cname))) like '%'+@cName+'%' )