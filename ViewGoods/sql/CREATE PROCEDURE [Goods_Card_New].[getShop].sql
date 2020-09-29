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
-- Description:	Получение списка магазинов
-- =============================================
CREATE PROCEDURE [Goods_Card_New].[getShop]		 	
AS
BEGIN
	SET NOCOUNT ON;


select 
	s.id,s.cName,s.isActive
from 
	dbo.s_Shop s


END
