SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	«апись справочника производителей
-- =============================================
CREATE PROCEDURE [Goods_Card_New].[spg_setAdresProizvod]			 
	@id int,
	@id_proizvoditel int,
	@id_subject int,
	@cName varchar(max),	
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
			IF EXISTS (select TOP(1) id from [dbo].[s_adress_proizvod] where id <>@id and id_proizvoditel = @id_proizvoditel and id_subject = @id_subject)
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
		END
	ELSE
		BEGIN
			IF @result = 0
				BEGIN
					
					IF NOT EXISTS(select TOP(1) id from [dbo].[s_adress_proizvod] where id = @id)
						BEGIN
							select -1 as id
							return;
						END
					
					IF EXISTS(select TOP(1) id from [dbo].s_Proizvodstvo where id_adress_proizvod  =  @id)
						BEGIN
							select -2 as id
							return;
						END

					IF EXISTS(select TOP(1) id from [dbo].s_sertification where id_adress_proizvod  =  @id)
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
	SELECT -9999 as id
	return;
END CATCH
	
END
