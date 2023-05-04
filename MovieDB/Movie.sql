CREATE TABLE [dbo].[Movie]
(
	[MovieId]INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(100) NULL,
    [Genre] NVARCHAR(50) NULL, 
    [ReleaseDate] DATETIME NULL,
    
)
