SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Получение списка ЮЛ
-- =============================================
ALTER PROCEDURE [Goods_Card_New].[spg_getNTypeOrg]		 	
AS
BEGIN
	SET NOCOUNT ON;


select 
	cast(m.nTypeOrg as int) as id ,ltrim(rtrim(m.Abbriviation)) as cName
from 
	dbo.s_MainOrg m
		inner join dbo.s_SelectedMainOrg  o on o.nTypeOrg = m.nTypeOrg
where 
	m.DateStart<=GETDATE() and GETDATE()<=m.DateEnd and m.isSeler = 1
order by 
	m.nTypeOrg asc

END
