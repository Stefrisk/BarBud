CREATE DATABASE [BarBud];  
GO  
  
USE [BarBud];  
GO  
  
CREATE LOGIN [barbud] WITH PASSWORD = 'bardev123';  
CREATE USER [barbud] FOR LOGIN [barbud];  
ALTER ROLE db_owner ADD MEMBER [barbud];  
GO
