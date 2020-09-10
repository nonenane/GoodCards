SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Запись Ту группы 
-- =============================================
ALTER PROCEDURE [Goods_Card_New].[spg_setGrp1]			 
	@id int,
	@cName varchar(12),	
	@id_otdel int,
	@id_nds  int,
	@ntypeorg int =null,
	@isCredit bit,
	@isWithSubGroups bit,
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
			IF EXISTS (select TOP(1) id from [dbo].[s_grp1] where id <>@id and LTRIM(RTRIM(LOWER([cname]))) = LTRIM(RTRIM(LOWER(@cName))) and id_otdel = @id_otdel)  and @isAutoIncriments = 0
				BEGIN
					SELECT -1 as id;
					return;
				END

			IF @isAutoIncriments = 0 and @id = 0
				BEGIN
					select @id =  isnull(max(id),0)+1 from dbo.s_grp1 where id_otdel = @id_otdel
				END

			IF NOT exists(select TOP(1) id from [dbo].[s_grp1] where id =@id)
				BEGIN
					INSERT INTO [dbo].[s_grp1]  (id,cname,id_otdel,id_nds,isCredit,isWithSubGroups,ldeystv,ntypeorg)
					VALUES (@id,@cName,@id_otdel,@id_nds,@isCredit,@isWithSubGroups,@isActive,@ntypeorg)
					
					SELECT @id as id
					return;
				END
			ELSE
				BEGIN
					UPDATE [dbo].[s_grp1] 
					set		
						cname=@cName,
						id_otdel=@id_otdel,
						id_nds= @id_nds,
						isCredit=@isCredit,
						isWithSubGroups = @isWithSubGroups,
						ldeystv=@isActive,
						ntypeorg = @ntypeorg
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
					
					IF NOT EXISTS(select TOP(1) id from [dbo].[s_grp1] where id = @id)
						BEGIN
							select -1 as id
							return;
						END

					
					IF EXISTS(select TOP(1) id from [dbo].s_tovar where id_grp1 = @id)
						BEGIN
							select -2 as id
							return;
						END

					IF EXISTS(select TOP(1) id from [dbo].[users_vs_grp1] p where  p.id_grp1 = @id)
						BEGIN
							select -2 as id
							return;
						END

					
					select 0 as id
					return;
				END
			ELSE
				BEGIN
					DELETE FROM dbo.grp1_vs_grp2 where id_grp1 = @id
					DELETE FROM [dbo].[s_grp1]  where id = @id					
					RETURN
				END
		END
END TRY 
BEGIN CATCH 
	SELECT -9999 as id,ERROR_MESSAGE() as msg
	return;
END CATCH
	
END
