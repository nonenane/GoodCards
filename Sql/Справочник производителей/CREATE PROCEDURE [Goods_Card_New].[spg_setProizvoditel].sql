SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	«апись справочника производителей
-- =============================================
ALTER PROCEDURE [Goods_Card_New].[spg_setProizvoditel]			 
	@id int,
	@cName varchar(max),	
	@inn varchar(max),
	@id_type_org int,
	@isActive bit,
	@id_user int,
	@result int = 0,
	@isDel int,
	@isAutoIncriments bit = 0
AS
BEGIN
	SET NOCOUNT ON;

BEGIN TRY 
	IF @isDel = 0
		BEGIN		
			IF EXISTS (select TOP(1) id from [dbo].[s_Proizvoditel] where id <>@id and LTRIM(RTRIM(LOWER([name]))) = LTRIM(RTRIM(LOWER(@cName))) and inn  = @inn) and @isAutoIncriments = 0 
				BEGIN
					SELECT -1 as id;
					return;
				END

			IF @id = 0
				BEGIN
					INSERT INTO [dbo].[s_Proizvoditel]  ([name],[inn],[id_type_org],isActive,id_Editor,DateEdit)
					VALUES (@cName,@inn,@id_type_org,1,@id_user,GETDATE())

					SELECT  cast(SCOPE_IDENTITY() as int) as id
					return;
				END
			ELSE
				BEGIN
					if @isAutoIncriments = 0
						BEGIN
							UPDATE [dbo].[s_Proizvoditel] 
							set		[name] = @cName,
									[inn] = @inn,
									[id_type_org] = @id_type_org,
									isActive=@isActive,
									id_Editor=@id_user,
									DateEdit=GETDATE()
							where id = @id
										
							SELECT @id as id
							return;
						END
					ELSE
						BEGIN
							set identity_insert dbo.[s_Proizvoditel] on;
							INSERT INTO [dbo].[s_Proizvoditel]  (id,[name],[inn],[id_type_org],isActive,id_Editor,DateEdit)
							VALUES (@id,@cName,@inn,@id_type_org,1,@id_user,GETDATE())
							set identity_insert dbo.[s_Proizvoditel] off;
							SELECT @id as id
							return;
						END
				END
		END
	ELSE
		BEGIN
			IF @result = 0
				BEGIN
					
					IF NOT EXISTS(select TOP(1) id from [dbo].[s_Proizvoditel] where id = @id) and @isAutoIncriments = 0 
						BEGIN
							select -1 as id
							return;
						END
					
					IF EXISTS(select TOP(1) id from [dbo].s_Proizvodstvo where id_adress_proizvod in (select id from dbo.s_adress_proizvod where id_proizvoditel =  @id)) and @isAutoIncriments = 0 
						BEGIN
							select -2 as id
							return;
						END

					IF EXISTS(select TOP(1) id from [dbo].s_sertification where id_adress_proizvod in (select id from dbo.s_adress_proizvod where id_proizvoditel =  @id)) and @isAutoIncriments = 0 
						BEGIN
							select -2 as id
							return;
						END
					
					select 0 as id
					return;
				END
			ELSE
				BEGIN
					DELETE FROM [dbo].[s_Proizvoditel]  where id = @id
					RETURN
				END
		END
END TRY 
BEGIN CATCH 
	SELECT -9999 as id,ERROR_MESSAGE ( )   
	return;
END CATCH
	
END
