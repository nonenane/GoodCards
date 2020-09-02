SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Получение списка НДС
-- =============================================
CREATE PROCEDURE [Goods_Card_New].[spg_getNds]		 	
AS
BEGIN
	SET NOCOUNT ON;


select
	cast(d.id as int) as id,
	ltrim(rtrim(d.nds)) as cName
from 
	dbo.s_nds d
END
