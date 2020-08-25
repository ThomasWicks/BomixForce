DECLARE @rec int, @email varchar(100); 
DECLARE company_cursor CURSOR FOR     
SELECT Recno, Email 
FROM [Cimatec].[dbo].[Bomix_Cliente];    
  
OPEN company_cursor    
  
FETCH NEXT FROM company_cursor     
INTO @rec,@email    
  
WHILE @@FETCH_STATUS = 0    
BEGIN    
    UPDATE [Cimatec].[dbo].[COMPANY]
	SET [RECNO] = @rec
	WHERE [EMAIL] = @email
      
    FETCH NEXT FROM company_cursor     
	INTO @rec,@email  
   
END     
CLOSE company_cursor;    
DEALLOCATE company_cursor;  
