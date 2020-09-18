SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Получение списка пользователей кто использует инв. группу
-- =============================================
ALTER PROCEDURE [Goods_Card_New].[spg_getUserVsGrp1]	
	@id_grp1 int
AS
BEGIN
	SET NOCOUNT ON;

select distinct
	--isnull(ltrim(rtrim(k.lastname))+' ','')+isnull(ltrim(rtrim(k.firstname))+' ','')+isnull(ltrim(rtrim(k.secondname)),'') as FIO 
	ltrim(trim(l.FIO)) as FIO
from 
	dbo.users_vs_grp1  g
	 --inner join dbo.s_kadr k on k.id= g.id_users
	 inner join dbo.ListUsers l on l.id = g.id_users and l.IsActive = 1

where 
	g.id_grp1 = @id_grp1 and l.DateUnemploy is null


END
