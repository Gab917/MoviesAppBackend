CREATE TABLE [dbo].[Customer]
(
	[CustomerId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [FullName] NVARCHAR(50) NULL, 
    [EmailAddress] NVARCHAR(50) NULL, 
    [Age] INT NULL, 
    
)
