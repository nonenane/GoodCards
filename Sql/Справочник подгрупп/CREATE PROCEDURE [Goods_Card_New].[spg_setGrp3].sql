SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Запись под групп
-- =============================================
ALTER PROCEDURE [Goods_Card_New].[spg_setGrp3]			 
	@id int,
	@cName varchar(50),	
	@id_otdel int,
	@isActive bit,	
	@result int = 0,
	@isDel int,
	@isAutoIncriments bit = 0
AS
BEGIN
	SET NOCOUNT ON;

BEGIN TRY 
	IF @isDel = 0
		BEGIN		
			IF EXISTS (select TOP(1) id from [dbo].[s_grp3] where id <>@id and LTRIM(RTRIM(LOWER(cName))) = LTRIM(RTRIM(LOWER(@cName))) and id_otdel = @id_otdel) and @isAutoIncriments = 0
				BEGIN
					SELECT -1 as id;
					return;
				END

			IF @id = 0			
				BEGIN
					INSERT INTO [dbo].[s_grp3]  (cName,id_otdel,ldeystv)
					VALUES (@cName,@id_otdel,@isActive)

					SELECT  cast(SCOPE_IDENTITY() as int) as id					
					return;
				END
			ELSE
				BEGIN
					
				IF @isAutoIncriments = 1 AND NOT EXISTS (select id from [dbo].[s_grp3] where id = @id)
					BEGIN

						SET identity_insert [dbo].[s_grp3] ON;

						INSERT INTO [dbo].[s_grp3]  (id,cName,id_otdel,ldeystv)
						VALUES (@id,@cName,@id_otdel,@isActive)
					
						SET  identity_insert [dbo].[s_grp3] OFF;
					END
				ELSE
					BEGIN
						UPDATE [dbo].[s_grp3] 
						set		
							cName=@cName,
							id_otdel=@id_otdel,
							ldeystv=@isActive
						where 
							id = @id
										
						SELECT @id as id
						return;
					END
				END
		END
	ELSE
		BEGIN
			IF @result = 0
				BEGIN
					
					IF NOT EXISTS(select TOP(1) id from [dbo].[s_grp3] where id = @id)
						BEGIN
							select -1 as id
							return;
						END

					
					IF EXISTS(select TOP(1) id from [dbo].s_tovar where id_grp3 = @id)
						BEGIN
							select -2 as id
							return;
						END
					
					select 0 as id
					return;
				END
			ELSE
				BEGIN				
					DELETE FROM [dbo].[s_grp3]  where id = @id					
					RETURN
				END
		END
END TRY 
BEGIN CATCH 
	SELECT -9999 as id,ERROR_MESSAGE() as msg
	return;
END CATCH
	
END
