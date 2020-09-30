/****** Object:  StoredProcedure [Goods_Card_New].[spg_getGrp3]    Script Date: 29.09.2020 21:31:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-09-29
-- Description:	Добавление или удаление товара по переоценки
-- =============================================
CREATE PROCEDURE [Goods_Card_New].[setRcenaFuture]	
	@id_tovar int,
	@date date,
	@rcena numeric(15,2),
	@id_user int,
	@isDel bit
AS
BEGIN
	SET NOCOUNT ON;

IF @isDel = 0
	BEGIN

		UPDATE dbo.s_rcena SET isActual = 0  where id_tovar = @id_tovar

		INSERT INTO dbo.s_rcena (id_tovar,rcena,tdate_k,tdate_n,isActual,id_Creator,DateCreate)
			VALUES (@id_tovar,@rcena,null,GETDATE(),1,@id_user,GETDATE())

		INSERT INTO dbo.j_EditGoodsCard (id_tovar,flag,DateEdit,id_Editor)
			VALUES (@id_tovar,1,GETDATE(),@id_user)

		DELETE FROM dbo.s_rcena_future where id_tovar = @id_tovar and cast(tdate_n as date) = @date
		
	END
ELSE
	BEGIN
		DELETE FROM dbo.s_rcena_future where id_tovar = @id_tovar and cast(tdate_n as date) = @date
	END

END
