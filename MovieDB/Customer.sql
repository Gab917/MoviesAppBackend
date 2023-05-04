CREATE TABLE [dbo].[Customer]
(
	[CustomerId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(50) NULL, 
    [LastName] NVARCHAR(50) NULL, 
    [Age] INT NULL, 
    
)
