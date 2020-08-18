SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	ѕолучение списка форм собственности
-- =============================================
CREATE PROCEDURE [Goods_Card_New].[spg_getTypeOrg]		 	
AS
BEGIN
	SET NOCOUNT ON;

	select 
		r.id,
		r.isActive,
		ltrim(rtrim(r.name)) as cName
	from 
		[dbo].[s_type_org] r	
END
