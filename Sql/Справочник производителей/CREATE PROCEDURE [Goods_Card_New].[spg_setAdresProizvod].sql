SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Запись справочника производителей
-- =============================================
ALTER PROCEDURE [Goods_Card_New].[spg_setAdresProizvod]			 
	@id int,
	@id_proizvoditel int,
	@id_subject int,
	@cName varchar(200),	
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
			IF EXISTS (select TOP(1) id from [dbo].[s_adress_proizvod] where id <> @id and id_proizvoditel = @id_proizvoditel and id_subject = @id_subject) and @isAutoIncriments = 0 
				BEGIN
					SELECT -1 as id;
					return;
				END

			IF @id = 0
				BEGIN
					INSERT INTO [dbo].[s_adress_proizvod]  (id_proizvoditel,id_subject,street,id_district,id_city,house,isActive,id_Editor,DateEdit)
					VALUES (@id_proizvoditel,@id_subject,@cName,NULL,NULL,NULL,1,@id_user,GETDATE())

					SELECT  cast(SCOPE_IDENTITY() as int) as id
					return;
				END
			ELSE
				BEGIN
					if EXISTS (select id from [dbo].[s_adress_proizvod] where id = @id) --@isAutoIncriments = 0
						BEGIN
							UPDATE [dbo].[s_adress_proizvod] 
							set		id_proizvoditel = @id_proizvoditel,
									id_subject = @id_subject,
									street = @cName,
									isActive=@isActive,
									id_Editor=@id_user,
									DateEdit=GETDATE()
							where id = @id
										
							SELECT @id as id
							return;
						END
					ELSE
						BEGIN
							set identity_insert dbo.[s_adress_proizvod] on;
							
							INSERT INTO [dbo].[s_adress_proizvod]  (id,id_proizvoditel,id_subject,street,id_district,id_city,house,isActive,id_Editor,DateEdit)
							VALUES (@id,@id_proizvoditel,@id_subject,@cName,NULL,NULL,NULL,1,@id_user,GETDATE())
							
							set identity_insert dbo.[s_adress_proizvod] off;

							SELECT @id as id
							return;
						END
				END
		END
	ELSE
		BEGIN
			IF @result = 0
				BEGIN
					
					IF NOT EXISTS(select TOP(1) id from [dbo].[s_adress_proizvod] where id = @id) and @isAutoIncriments = 0 
						BEGIN
							select -1 as id
							return;
						END
					
					IF EXISTS(select TOP(1) id from [dbo].s_Proizvodstvo where id_adress_proizvod  =  @id)and @isAutoIncriments = 0 
						BEGIN
							select -2 as id
							return;
						END

					IF EXISTS(select TOP(1) id from [dbo].s_sertification where id_adress_proizvod  =  @id)and @isAutoIncriments = 0 
						BEGIN
							select -2 as id
							return;
						END
					
					select 0 as id
					return;
				END
			ELSE
				BEGIN
					DELETE FROM [dbo].[s_adress_proizvod]  where id = @id
					RETURN
				END
		END
END TRY 
BEGIN CATCH 
	SELECT -9999 as id,ERROR_MESSAGE() as msg
	return;
END CATCH
	
END
