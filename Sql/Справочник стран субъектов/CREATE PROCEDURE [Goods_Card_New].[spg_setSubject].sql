SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	«апись справочника форм собственности
-- =============================================
CREATE PROCEDURE [Goods_Card_New].[spg_setSubject]			 
	@id int,
	@cName varchar(max),	
	@code varchar(150),
	@isActive bit,
	@id_user int,
	@result int = 0,
	@isDel int
AS
BEGIN
	SET NOCOUNT ON;

BEGIN TRY 
	IF @isDel = 0
		BEGIN
			DECLARE @isRu bit  = 0
				IF EXISTS (select top(1) id from dbo.prog_config where id_prog = 118 and value = @code and id_value = 'ciru') SET @isRu = 1

			IF EXISTS (select TOP(1) id from [dbo].[s_Subjects] where id <>@id and LTRIM(RTRIM(LOWER([cname]))) = LTRIM(RTRIM(LOWER(@cName))))
				BEGIN
					SELECT -1 as id;
					return;
				END

			IF @id = 0
				BEGIN
					INSERT INTO [dbo].[s_Subjects]  ([cname],[kod_strany],[isPersUniversam],[russian],isActive,id_Editor,DateEdit)
					VALUES (@cName,@code,0,@isRu,1,@id_user,GETDATE())

					SELECT  cast(SCOPE_IDENTITY() as int) as id
					return;
				END
			ELSE
				BEGIN
					UPDATE [dbo].[s_Subjects] 
					set		[cname] = @cName,
							[kod_strany] = @code,
							[russian] = @isRu,
							isActive=@isActive,
							id_Editor=@id_user,
							DateEdit=GETDATE()
					where id = @id
										
					SELECT @id as id
					return;
				END
		END
	ELSE
		BEGIN
			IF @result = 0
				BEGIN
					
					IF NOT EXISTS(select TOP(1) id from [dbo].[s_Subjects] where id = @id)
						BEGIN
							select -1 as id
							return;
						END

					
					IF EXISTS(select TOP(1) id from [dbo].s_adress_proizvod where id_subject = @id)
						BEGIN
							select -2 as id
							return;
						END

					IF EXISTS(select TOP(1) id from [dbo].s_tovar_subject where id_subject = @id)
						BEGIN
							select -2 as id
							return;
						END

					IF EXISTS(select TOP(1) p.id_kadr from [Personnel].[Documents] p where  p.Citizenship = @id)
						BEGIN
							select -2 as id
							return;
						END

					
					select 0 as id
					return;
				END
			ELSE
				BEGIN
					DELETE FROM [dbo].[s_Subjects]  where id = @id
					RETURN
				END
		END
END TRY 
BEGIN CATCH 
	SELECT -9999 as id
	return;
END CATCH
	
END
