SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Запись справочника форм собственности
-- =============================================
ALTER PROCEDURE [Goods_Card_New].[spg_setTypeOrg]			 
	@id int,
	@cName varchar(10),	
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

			IF EXISTS (select TOP(1) id from [dbo].[s_type_org] where id <>@id and LTRIM(RTRIM(LOWER([name]))) = LTRIM(RTRIM(LOWER(@cName)))) and @isAutoIncriments = 0
				BEGIN
					SELECT -1 as id;
					return;
				END

			IF @id = 0
				BEGIN
					INSERT INTO [dbo].[s_type_org]  ([name],isActive,id_Editor,DateEdit)
					VALUES (@cName,1,@id_user,GETDATE())

					SELECT  cast(SCOPE_IDENTITY() as int) as id
					return;
				END
			ELSE
				BEGIN

					IF NOT EXISTS (select id from [dbo].[s_type_org] where id  = @id)
						BEGIN
							set identity_insert dbo.[s_type_org] on;
							INSERT INTO [dbo].[s_type_org]  (id,[name],isActive,id_Editor,DateEdit)
							VALUES (@id,@cName,1,@id_user,GETDATE())
							set identity_insert dbo.[s_type_org] off;
							SELECT @id as id
							return;
						END
					ELSE
						BEGIN
							UPDATE [dbo].[s_type_org] 
							set		[name] = @cName,
									isActive=@isActive,
									id_Editor=@id_user,
									DateEdit=GETDATE()
							where id = @id
										
							SELECT @id as id
							return;
						END
				END
		END
	ELSE
		BEGIN
			IF @result = 0
				BEGIN
					
					IF NOT EXISTS(select TOP(1) id from [dbo].[s_type_org] where id = @id)
						BEGIN
							select -1 as id
							return;
						END

					
					IF EXISTS(select TOP(1) id from [dbo].[s_Proizvoditel] where id_type_org = @id)
						BEGIN
							select -2 as id
							return;
						END

					IF EXISTS(select TOP(1) id from [Caramel].[s_Post] p where  p.id_TypePost = @id)
						BEGIN
							select -2 as id
							return;
						END

					
					select 0 as id
					return;
				END
			ELSE
				BEGIN
					DELETE FROM [dbo].[s_type_org]  where id = @id
					RETURN
				END
		END
END TRY 
BEGIN CATCH 
	SELECT -9999 as id,ERROR_MESSAGE() as msg
	return;
END CATCH
	
END
