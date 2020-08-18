SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Получение списка производителей
-- =============================================
CREATE PROCEDURE [Goods_Card_New].[spg_getAdressProizvodVsProizvod]		 	
	@id_proizvoditel int
AS
BEGIN
	SET NOCOUNT ON;


select
	a.id,
	a.id_subject,
	a.street,
	isnull(ltrim(rtrim(s.cname)),'') as cName,
	a.id_proizvoditel
from  
	dbo.s_adress_proizvod a 		
			left join dbo.s_Subjects s on s.id = a.id_subject
where 
	a.id_proizvoditel = @id_proizvoditel
END
