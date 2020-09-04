SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Запись инв групп
-- =============================================
ALTER PROCEDURE [Goods_Card_New].[spg_setGrp2]			 
	@id int,
	@cName varchar(max),	
	@id_otdel int,
	@id_unigrp int,
	@id_unit int,
	@specification bit,
	@skoroportovar bit,
	@NettoMax numeric(11,3),
	@DayMax int,
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
			IF EXISTS (select TOP(1) id from [dbo].[s_grp2] where id <>@id and LTRIM(RTRIM(LOWER(cname))) = LTRIM(RTRIM(LOWER(@cName))) and id_otdel = @id_otdel) and @isAutoIncriments = 0
				BEGIN
					SELECT -1 as id;
					return;
				END

			IF @id = 0			
				BEGIN
					INSERT INTO [dbo].[s_grp2]  (cname,id_otdel,id_unigrp,id_unit,specification,skoroportovar,NettoMax,DayMax,ldeystv,Store,observe,isWithMovements)
					VALUES (@cName,@id_otdel,@id_unigrp,@id_unit,@specification,@skoroportovar,@NettoMax,@DayMax,@isActive,0,0,0)

					set @id = cast(SCOPE_IDENTITY() as int)
					UPDATE dbo.s_grp2 set ngrp2 = @id where id = @id
					SELECT @id as id
					return;
				END
			ELSE
				BEGIN

				IF @isAutoIncriments = 1 AND NOT EXISTS (select id from dbo.s_grp2 where id = @id)
					BEGIN

					SET identity_insert [dbo].[s_grp2] ON;

					INSERT INTO [dbo].[s_grp2]  (id,cname,id_otdel,id_unigrp,id_unit,specification,skoroportovar,NettoMax,DayMax,ldeystv,Store,observe,isWithMovements,ngrp2)
					VALUES (@id,@cName,@id_otdel,@id_unigrp,@id_unit,@specification,@skoroportovar,@NettoMax,@DayMax,@isActive,0,0,0,@id)
					
					SET  identity_insert [dbo].[s_grp2] OFF;
					END
				ELSE
					BEGIN


						UPDATE [dbo].[s_grp2] 
						set		
							cname=@cName,
							id_otdel=@id_otdel,
							id_unigrp=@id_unigrp,
							id_unit=@id_unit,
							specification=@specification,
							skoroportovar=@skoroportovar,
							NettoMax=@NettoMax,
							DayMax=@DayMax,
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
					
					IF NOT EXISTS(select TOP(1) id from [dbo].[s_grp2] where id = @id)
						BEGIN
							select -1 as id
							return;
						END

					
					IF EXISTS(select TOP(1) id from [dbo].s_tovar where id_grp2 = @id)
						BEGIN
							select -2 as id
							return;
						END

					IF EXISTS(select TOP(1) id from [dbo].grp1_vs_grp2 where id_grp2 = @id)
						BEGIN
							select -2 as id
							return;
						END
					
					select 0 as id
					return;
				END
			ELSE
				BEGIN				
					DELETE FROM [dbo].[s_grp2]  where id = @id					
					RETURN
				END
		END
END TRY 
BEGIN CATCH 
	SELECT -9999 as id
	return;
END CATCH
	
END
