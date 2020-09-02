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
	cast(nTypeOrg as int) as id ,ltrim(rtrim(Abbriviation)) as cName
from 
	dbo.s_MainOrg 
where 
	DateStart<=GETDATE() and GETDATE()<=DateEnd and isSeler = 1
order by 
	nTypeOrg asc

END
