USE [Cimatec]
GO
CREATE TRIGGER TGR_COMPANY_INSERT
ON [dbo].[Bomix_Cliente] 
FOR INSERT
AS
BEGIN
    DECLARE
    @Recno  INT,
    @NomeFantasia  VARCHAR(20),
	@CNPJ  VARCHAR(14),
    @Email   VARCHAR(100)

    SELECT @Recno = [Recno]
      ,@NomeFantasia = [NomeFantasia]
      ,@CNPJ = [CNPJ]
      ,@Email = [Email] FROM inserted

    INSERT INTO [dbo].[COMPANY] 
	values (@NomeFantasia,@Email,@CNPJ,@Recno)

END
GO

CREATE TRIGGER TGR_COMPANY_UPDATE
ON [dbo].[Bomix_Cliente] 
FOR UPDATE
AS
BEGIN
    DECLARE
    @Recno  INT,
    @NomeFantasia  VARCHAR(20),
	@CNPJ  VARCHAR(14),
    @Email   VARCHAR(100)

    SELECT @Recno = [Recno]
      ,@NomeFantasia = [NomeFantasia]
      ,@CNPJ = [CNPJ]
      ,@Email = [Email] FROM inserted

	UPDATE [dbo].[COMPANY]  
	SET [NAME] = @NomeFantasia
      ,[EMAIL] = @Email
      ,[CNPJ] = @CNPJ
    WHERE [RECNO] = @Recno

END
GO

CREATE TRIGGER TGR_COMPANY_DELETE
ON [dbo].[Bomix_Cliente] 
FOR DELETE
AS
BEGIN
    DECLARE
    @Recno  INT

    SELECT @Recno = [Recno] FROM deleted

	DELETE [dbo].[COMPANY] WHERE [RECNO] = @Recno

END
GO