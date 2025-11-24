CREATE DATABASE [BarBud];  
GO  
  
USE [BarBud];  
GO  
  
CREATE LOGIN [barbud] WITH PASSWORD = '65FmM%HJ3y@t#N^bV#@K';
CREATE USER [barbud] FOR LOGIN [barbud];  
ALTER ROLE db_owner ADD MEMBER [barbud];  
GO
