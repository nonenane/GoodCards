SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Запись под групп
-- =============================================
CREATE PROCEDURE [Goods_Card_New].[spg_setGrp3]			 
	@id int,
	@cName varchar(max),	
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
			IF EXISTS (select TOP(1) id from [dbo].[s_grp3] where id <>@id and LTRIM(RTRIM(LOWER(cName))) = LTRIM(RTRIM(LOWER(@cName))) and id_otdel = @id_otdel)
				BEGIN
					SELECT -1 as id;
					return;
				END

			IF @id = 0
			--IF NOT exists(select TOP(1) id from dbo.[s_grp3] where id =@id)
				BEGIN
					INSERT INTO [dbo].[s_grp3]  (cName,id_otdel,ldeystv)
					VALUES (@cName,@id_otdel,@isActive)

					SELECT  cast(SCOPE_IDENTITY() as int) as id
					--SELECT @id as id
					return;
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
	ELSE
		BEGIN
			IF @result = 0
				BEGIN
					
					IF NOT EXISTS(select TOP(1) id from [dbo].[s_grp3] where id = @id)
						BEGIN
							select -1 as id
							return;
						END

					
					--IF EXISTS(select TOP(1) id from [dbo].s_tovar where id_grp1 = @id)
					--	BEGIN
					--		select -2 as id
					--		return;
					--	END

					--IF EXISTS(select TOP(1) id from [dbo].[users_vs_grp1] p where  p.id_grp1 = @id)
					--	BEGIN
					--		select -2 as id
					--		return;
					--	END

					
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
	SELECT -9999 as id
	return;
END CATCH
	
END
