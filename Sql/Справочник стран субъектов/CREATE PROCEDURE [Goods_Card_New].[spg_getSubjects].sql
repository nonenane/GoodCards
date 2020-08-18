SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Получение списка стран субъектов
-- =============================================
CREATE PROCEDURE [Goods_Card_New].[spg_getSubjects]		 	
AS
BEGIN
	SET NOCOUNT ON;

	select 
		r.id,
		r.isActive,
		ltrim(rtrim(r.cname)) as cName,
		r.kod_strany,
		r.russian
	from 
		[dbo].[s_Subjects] r	
END
