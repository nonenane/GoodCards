SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Запись связи ТУ и инв групп
-- =============================================
ALTER PROCEDURE [Goods_Card_New].[spg_setGrp1VsGrp2]			 
	@id_grp1 int,
	@id_grp2 int,
	@isDel int,
	@isAutoIncriments bit = 0
AS
BEGIN
	SET NOCOUNT ON;

BEGIN TRY 
	IF @isDel = 0
		BEGIN					
			IF NOT exists(select TOP(1) id from dbo.[grp1_vs_grp2]  where id_grp1 = @id_grp1 and id_grp2 = @id_grp2)
				BEGIN
					INSERT INTO [dbo].[grp1_vs_grp2]  (id_grp1,id_grp2)
					VALUES (@id_grp1,@id_grp2)

					SELECT @id_grp1 as id
					return;			
				END
		END
	ELSE
		BEGIN		
			DELETE FROM [dbo].[grp1_vs_grp2]  where id_grp1 = @id_grp1 and id_grp2 = @id_grp2
		END
END TRY 
BEGIN CATCH 
	SELECT -9999 as id
	return;
END CATCH
	
END
